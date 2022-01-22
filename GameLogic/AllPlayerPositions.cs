using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectFactory))]
public class AllPlayerPositions : MonoBehaviour
{
    [SerializeField] private GameObjectFactory _factory;
    [SerializeField] private PlayerHuman _playerHuman;
    [SerializeField] private PlayerAi _playerAi;

    private List<Player> _players;
    public List<Player> Players => _players;

    private void Start()
    {
        _factory = gameObject.GetComponent<GameObjectFactory>();
    }

    public Position GetCurentPosition(int number) =>
        _players[number].Position.Current;

    public AllPlayerPositions(int playersCount) => InitialPlayers(playersCount);

    public void CorrectionPlayersList()
    {
        List<Player> players = new List<Player>(_players.Count);

        for (int i = 0; i < _players.Count; i++)
            players[i] = _players[i];

        _players.Clear();

        _players = new List<Player>();

        for (int i = 0; i < players.Count; i++)
            if (players[i].IsActive())
                _players.Add(players[i]);

        ShiftPositions();
    }

    private void InitialPlayers(int playersCount)
    {
        _players = new List<Player>(playersCount);

        var random = new Unity.Mathematics.Random();
        int playerPosition = random.NextInt(0, _players.Count);

        for (int i = 0; i < playersCount; i++)
        {
            if (i == playerPosition)
            {
                PlayerHuman playerHuman = _factory.CreateGameObjectInstance(_playerHuman);
                _players[i] = playerHuman as Player;

                continue;
            }

            PlayerAi playerAi = _factory.CreateGameObjectInstance(_playerAi);
            _players[i] = playerAi;
        }

        RecordPositions(_players);
    }

    private void RecordPositions(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            SelfPlayerPosition curentPosition = new SelfPlayerPosition(i);

            players[i].Position = curentPosition;
        }
    }

    private void ShiftPositions()
    {
        List<Player> players = new List<Player>(_players.Count);

        for (int i = 0, j = -1; i < _players.Count; i++, j++)
        {
            if (j == -1)
                j = _players.Count;

            players[j] = _players[i];
        }

        _players.Clear();

        _players = new List<Player>(players.Count);

        for (int i = 0; i < players.Count; i++)
        {
            _players[i] = players[i];
            _players[i].Position.GetEnumerator();
        }
    }
}
