public class PlayerSteps
{
    private PlayerStepState _playerStepsState;
    
    private Step _selfStep;
    public Step SelfStep => _selfStep;

    public PlayerSteps()
    {
        _selfStep = Step.One;
        _playerStepsState = new PlayerStepState();
    }
}

public enum Step 
{ 
    One, 
    Two,
    Three,
    Four,
    Five, 
    Six, 
    Seven, 
    Eight, 
    Nine, 
    FinishRound 
}