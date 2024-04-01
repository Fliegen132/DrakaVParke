using _2048Figure.Architecture.ServiceLocator;

public class ViewModelScore : IService
{
    private ViewScore _viewScore;
    private ModelScore _modelScore;
    public ViewModelScore()
    {
        _modelScore = new ModelScore();
        _viewScore = ServiceLocator.current.Get<ViewScore>();
    }

    public void UpdateScore()
    {
        _modelScore.AddScore(1);
        _viewScore.UpdateScore(_modelScore.GetScore());
    }
}
