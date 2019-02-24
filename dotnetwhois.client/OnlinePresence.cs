namespace dotnetwhois
{
    public interface IOnlinePresence
    {
        OnlinePresenceKey OnlinePresenceKey { get; }

        string BuildUrlFromUsername(string username);
    }

    public enum OnlinePresenceKey
    {
        GitHub = 1,
        StackOverflow = 2,
        DevTo = 3
    }
}