﻿using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebScrapingIntegration.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScrapingIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubScrapingController : ControllerBase
    {
        private readonly ILogger<ClubScrapingController> _logger;

        public ClubScrapingController(ILogger<ClubScrapingController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Get Team Details via Wiki
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ClubDetails>>> GetClubByName([FromQuery] string query)
        {
            string baseWikiUrl = "https://en.wikipedia.org";
            string url = "https://en.wikipedia.org/wiki/List_of_top-division_football_clubs_in_UEFA_countries";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            ClubDetails clubDetails = new();
            List<ClubDetails> result = new List<ClubDetails>();
            List<string> clubsHref = htmlDocument.DocumentNode
                .SelectNodes("//tbody//tr//th//a")
                .Where(node => node.InnerText.Contains(query))
                .Select(th => th.GetAttributeValue("href", "")).ToList();
            foreach (var href in clubsHref)
            {
                _logger.LogWarning($"Started search for {query} in {href}");
                //string href = item.GetAttributeValue("href", "");
                var clubHtml = httpClient.GetStringAsync(baseWikiUrl + href).Result;
                htmlDocument.LoadHtml(clubHtml);
                var trNodesOfClub = htmlDocument.DocumentNode.SelectNodes("//table[@class=\"infobox vcard\"]/tbody//tr").Take(6);
                foreach (var tr in trNodesOfClub)
                {
                    if (tr.FirstChild.InnerText == "Full name")
                    {
                        clubDetails.FullName = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                    else if (tr.FirstChild.HasClass("infobox-image"))
                    {
                        clubDetails.LogoUrl = tr.FirstChild.FirstChild.FirstChild.GetAttributeValue("href", "");
                    }
                    else if (tr.FirstChild.InnerText == "Nickname(s)")
                    {
                        clubDetails.NickName = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                    else if (tr.FirstChild.InnerHtml == "Manager" || tr.FirstChild.InnerHtml == "Head coach")
                    {
                        clubDetails.Manager = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, true);
                    }
                    else if (tr.FirstChild.InnerHtml == "League")
                    {
                        clubDetails.League = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                    else if (tr.FirstChild.InnerText == "Ground" || tr.FirstChild.InnerText == "Stadium")
                    {
                        clubDetails.StadiumFullName = tr.FirstChild.NextSibling.FirstChild.GetAttributeValue("title", "");
                        string stadiumHref = tr.FirstChild.NextSibling.FirstChild.GetAttributeValue("href", "");

                        var stadiumHtml = httpClient.GetStringAsync(baseWikiUrl + stadiumHref).Result;
                        htmlDocument.LoadHtml(stadiumHtml);
                        _logger.LogWarning($"Started search for stadium: {clubDetails.StadiumFullName}");
                        var trsOfStadium = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody//tr");
                        var trsOfStadium2ndPattern = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[2]/tbody");
                        if (trsOfStadium != null)
                        {
                            foreach (var trStadium in trsOfStadium)
                            {
                                if (trStadium.FirstChild.HasClass("infobox-image"))
                                {
                                    clubDetails.StadiumImageUrl = trStadium.FirstChild.FirstChild.FirstChild.GetAttributeValue("src", "");
                                }
                                else if (trStadium.FirstChild.InnerText == "Capacity")
                                {
                                    clubDetails.StadiumCapacity = trStadium.FirstChild.NextSibling.FirstChild.InnerText;
                                }
                            }
                        }
                        else if (trsOfStadium2ndPattern != null)
                        {
                            foreach (var trStadium in trsOfStadium2ndPattern)
                            {
                                if (trStadium.FirstChild.HasClass("infobox-image"))
                                {
                                    clubDetails.StadiumImageUrl = trStadium.FirstChild.FirstChild.FirstChild.GetAttributeValue("src", "");
                                }
                                else if (trStadium.FirstChild.InnerText == "Capacity")
                                {
                                    clubDetails.StadiumCapacity = trStadium.FirstChild.NextSibling.FirstChild.InnerText;
                                }
                            }
                        }
                    }
                }
                clubDetails.Manager = "ToFix";
                clubDetails.League = "ToFix";
                result.Add(clubDetails);
                clubDetails = new();
                Thread.Sleep(100);
            }
            return Ok(result);
        }
        static string ReplaceNonTextWithSpaces(string input, bool isManager)
        {
            char[] nonTextChars = { '&', '#', ';' };
            string pattern = @"\d+";
            if (isManager)
            {
                input = Regex.Replace(input, pattern, "");
            }
            foreach (char nonTextChar in nonTextChars)
            {
                input = input.Replace(nonTextChar, ' ');
            }
            input = input.TrimEnd();
            return input;
        }
    }
}
