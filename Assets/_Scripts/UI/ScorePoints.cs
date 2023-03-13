namespace UI
{
    public class ScorePoints : IScorePoints
    {
        public int Points { get; set; }
        public void AddPoints(int points) => Points += points;
    }
}
