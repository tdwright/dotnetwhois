namespace dotnetwhois
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using Newtonsoft.Json;

    public interface ICardLoader
    {
        Card LoadCard();
    }

    public class LocalCardLoader : ICardLoader
    {
        private readonly string Filename = "../card.json"; 

        public Card LoadCard()
        {
            var cardDataLines = File.ReadAllLines(Filename);
            return CardHelpers.CardFromLineArray(cardDataLines);
        }
    }

    public class GithubCardLoader : ICardLoader
    {
        private const string UrlTemplate = @"https://raw.githubusercontent.com/USERNAME/dotnetwhois/master/card.json";
        private readonly string _username;

        public GithubCardLoader(string username)
        {
            _username = username;
        }

        public Card LoadCard()
        {
            var gitHubUrl = UrlTemplate.Replace("USERNAME", _username);
            var sr = new StreamReader(new HttpClient().GetStreamAsync(gitHubUrl).Result);

            string line = null;
            var lines = new List<string>();
            while((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return CardHelpers.CardFromLineArray(lines.ToArray());
        }
    }

    public static class CardHelpers
    {
        public static Card CardFromLineArray(string[] cardDataLines)
        {
            var cardData = string.Join(Environment.NewLine, cardDataLines);
            var card = JsonConvert.DeserializeObject<Card>(cardData);
            return card;
        }
    }
}