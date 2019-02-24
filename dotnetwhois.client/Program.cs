using System;

namespace dotnetwhois
{
    class Program
    {
        static void Main(string[] args)
        {
            ICardLoader loader;
            if(args.Length == 0)
            {
                // Try to load from local solution
                loader = new LocalCardLoader();
            }
            else
            {
                // Try to load from a GitHub profile
                loader = new LocalCardLoader();
            }
            var card = loader.LoadCard();
            Console.Write(card);
        }
    }
}
