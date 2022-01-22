using System.Collections.Generic;

[System.Serializable]
public class TablePlayRounds
{
    private List<PlayRound> _playRounds;

    public PlayRound Initial => new PlayRound();

    public int Count => _playRounds.Count;

    public TablePlayRounds()
    {
        _playRounds = new List<PlayRound>();
    }

    public void StartGame() => _playRounds = new List<PlayRound>();

    public void SaveRound(PlayRound round) => _playRounds.Add(round);

    public PlayRound GetRound(int number) => _playRounds[number];
}