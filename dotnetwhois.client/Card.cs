namespace dotnetwhois
{
    using System.Collections.Generic;

    public partial class Card
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public List<CardLine> CardLines { get; set; }
    }

    public class CardLine
    {
        public string Label { get; set; }

        public string Contents { get; set; }
    }
}