namespace dotnetwhois
{
    using System;
    using System.IO;
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
            var cardData = string.Join(Environment.NewLine,cardDataLines);
            var card = JsonConvert.DeserializeObject<Card>(cardData);
            return card;
        }
    }
}