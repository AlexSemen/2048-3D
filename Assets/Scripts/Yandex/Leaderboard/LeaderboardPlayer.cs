public class LeaderboardPlayer
{
    public LeaderboardPlayer(string rank, string name, string score)
    {
        Rank = rank;
        Name = name;
        Score = score;
    }

    public string Rank { get; private set; }
    public string Name { get; private set; }
    public string Score { get; private set; }
}
