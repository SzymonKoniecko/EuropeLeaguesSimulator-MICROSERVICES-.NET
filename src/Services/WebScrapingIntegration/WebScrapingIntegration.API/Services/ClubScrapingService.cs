using HtmlAgilityPack;
using System.Reflection;
using System.Text.RegularExpressions;
using WebScrapingIntegration.API.Entities;
using WebScrapingIntegration.API.Interfaces;
using WebScrapingIntegration.API.Models;

namespace WebScrapingIntegration.API.Services
{
    public class ClubScrapingService : IClubScrapingService
    {
        private readonly ILogger<ClubScrapingService> _logger;
        private readonly IWebScrapingProcessesRepository _webScrapingProcessesRepository;

        public ClubScrapingService(
            ILogger<ClubScrapingService> logger,
            IWebScrapingProcessesRepository webScrapingProcessesRepository)
        {
            _logger = logger;
            _webScrapingProcessesRepository = webScrapingProcessesRepository;
        }
        public IEnumerable<ClubDetails> GetClubDetailsByScraping(string query)
        {
            WebScrapingProces webScrapingProces = _webScrapingProcessesRepository.GetScrapingProcesByGivenQuery(query);
            if (webScrapingProces != null)
            {
                throw new Exception($"For given query = {query} - searching has been proceeded");
            }
            else
            {
                webScrapingProces = new()
                {
                    Id = Guid.NewGuid(),
                    NameOfMethod = MethodBase.GetCurrentMethod().Name,
                    GivenQuery = query,
                    IsCompleted = false,
                    StartTime = DateTime.Now
                };
                _webScrapingProcessesRepository.LogInformationAboutScraping(webScrapingProces);
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
                        if (htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/caption") != null)
                        {
                            clubDetails.FullName = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[1]/caption").InnerText;
                        }
                        else if (htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[2]/caption") != null)
                        {
                            clubDetails.FullName = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"mw-content-text\"]/div[1]/table[2]/caption").InnerText;
                        }
                        if (tr.FirstChild.HasClass("infobox-image"))
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
                    clubDetails.SetNoContentIfNull();
                    result.Add(clubDetails);
                    clubDetails = new();
                    Thread.Sleep(100);
                }
                webScrapingProces.EndTime = DateTime.Now;
                webScrapingProces.IsCompleted = true;
                _webScrapingProcessesRepository.UpdateLog(webScrapingProces);
                return result;
            }

        }
        private static string ReplaceNonTextWithSpaces(string input, bool isManager)
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
