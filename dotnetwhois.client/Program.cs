using System;

namespace dotnetwhois
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length> 0){
                ICardLoader loader;
                if(args[0].ToLowerInvariant().Substring(0,2) == "-l")
                {
                    // Try to load from local solution
                    loader = new LocalCardLoader();
                }
                else
                {
                    // Try to load from a GitHub profile
                    loader = new GithubCardLoader(args[0]);
                }
                var card = loader.LoadCard();
                Console.Write(card);
            }
            else
            {
                HelpText();
            }
        }

        static void HelpText()
        {
            var lines = new string[]
            {
                "dotnetwhois",
                "USAGE",
                "Load card from GitHub:",
                "dotnet whois tdwright",
                "Load card from local project (for testing):",
                "dotnet whois -l"
            };

            foreach(var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
