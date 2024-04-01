public class ModelScore
{
    private int _score;

    public void AddScore(int score)
    {
        _score += score;
    }
    public int GetScore() => _score;
}
