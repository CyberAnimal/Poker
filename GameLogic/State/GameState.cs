using System.Collections.Generic;

[System.Serializable]
public class GameState
{
    private MovesChanger _movesChanger;
    private State _presentState;
    public State PresentState => _presentState;

    private List<Player> _activePlayers;
    public List<Player> ActivePlayers => _activePlayers;

    public GameState(MovesChanger movesChanger, int playersCount)
    {
        _movesChanger = movesChanger;

        _movesChanger.Initialize(playersCount);
    }

    public void StartRound()
    {
        _presentState = State.PreFlop;
        _movesChanger.CanNewRound();
        _activePlayers = _movesChanger.PlayersMove();
    }

    public void Flop()
    {
        _presentState = State.Flop;
        _activePlayers = _movesChanger.PlayersMove();
    }

    public void Tern()
    {
        _presentState = State.Tern;
        _activePlayers = _movesChanger.PlayersMove();
    }

    public void River()
    {
        _presentState = State.River;
        _activePlayers = _movesChanger.PlayersMove();
    }

    public void OpenHands()
    {
        _presentState = State.OpenHands;
        _activePlayers = _movesChanger.PlayersMove();
    }
}

