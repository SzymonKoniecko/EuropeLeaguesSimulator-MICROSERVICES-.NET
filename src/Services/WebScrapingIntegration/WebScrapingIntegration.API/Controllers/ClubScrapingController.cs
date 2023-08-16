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
        public async Task<ActionResult<IEnumerable<ClubDetails>>> GetTeamByName([FromQuery] string query)
        {
            string baseWikiUrl = "https://en.wikipedia.org";
            string url = "https://en.wikipedia.org/wiki/List_of_top-division_football_clubs_in_UEFA_countries";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            List<ClubDetails> result = new List<ClubDetails>();
            List<string> clubsHref = htmlDocument.DocumentNode
                .SelectNodes("//tbody//tr//th//a")
                .Where(node => node.InnerText.Contains(query))
                .Select(th => th.GetAttributeValue("href", "")).ToList();
            foreach (var href in clubsHref)
            {
                _logger.LogInformation(href);
                //string href = item.GetAttributeValue("href", "");
                var clubHtml = httpClient.GetStringAsync(baseWikiUrl + href).Result;
                htmlDocument.LoadHtml(clubHtml);
                var trNodesOfClub = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody//tr");
                ClubDetails clubDetails = new();
                clubDetails.Logo = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr[1]/td/span/a/img").GetAttributeValue("src", "");
                foreach (var tr in trNodesOfClub)
                {
                    if (tr.FirstChild.InnerText == "Full name")
                    {
                        clubDetails.FullName = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                    else if (tr.FirstChild.InnerText == "Nickname(s)")
                    {
                        clubDetails.NickName = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                    else if (tr.FirstChild.InnerText == "Manager" || tr.FirstChild.InnerHtml == "Head coach")
                    {
                        clubDetails.Manager = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, true);
                    }
                    else if (tr.FirstChild.InnerText == "League")
                    {
                        clubDetails.League = ReplaceNonTextWithSpaces(tr.FirstChild.NextSibling.InnerText, false);
                    }
                }
                result.Add(clubDetails);
                Thread.Sleep(1000);
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
