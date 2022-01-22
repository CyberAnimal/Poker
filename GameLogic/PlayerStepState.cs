using System.Collections.Generic;

[System.Serializable]
public class PlayerStepState
{
    private readonly static Dictionary<ActionState, Dictionary<Trigger, ActionState>> _playerstate = 
        new Dictionary<ActionState, Dictionary<Trigger, ActionState>>()
    {
        [ActionState.MB] = new Dictionary<Trigger, ActionState>
        {
            [Trigger.CheckOrCall] = ActionState.ContinuePlay
        },
        [ActionState.BB] = new Dictionary<Trigger, ActionState>
        {
            [Trigger.CheckOrCall] = ActionState.ContinuePlay
        },
        [ActionState.ContinuePlay] = new Dictionary<Trigger, ActionState>
        {
            [Trigger.CheckOrCall] = ActionState.ContinuePlay,
            [Trigger.RaiseOrAllin] = ActionState.RaiseRate,
            [Trigger.Fold] = ActionState.Stop
        },
        [ActionState.RaiseRate] = new Dictionary<Trigger, ActionState> { },
        [ActionState.Stop] = new Dictionary<Trigger, ActionState> { }
    };

    public static Dictionary<Trigger, ActionState> GetTrigger(ActionState state) => _playerstate[state];
}

public enum ActionState
{
    MB,
    BB,
    ContinuePlay,
    RaiseRate,
    Stop
}

public enum Trigger
{
    CheckOrCall,
    RaiseOrAllin,
    Fold
}

public static class ExtensionMethods
{
    public static ActionState SwitchState(this ActionState thisState, Trigger trigger)
    {
        if (trigger == Trigger.Fold) thisState = ActionState.Stop;
        if (trigger == Trigger.CheckOrCall) thisState = ActionState.ContinuePlay;
        if (trigger == Trigger.RaiseOrAllin) thisState = ActionState.RaiseRate;

        return thisState;
    }
}
