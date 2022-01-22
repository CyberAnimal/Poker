using System.Threading.Tasks;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
	public abstract PlayerAction Action { get; }

	public abstract PlayerBank Bank { get; }

	public abstract CardsInHand Cards { get; }

    public abstract SelfPlayerPosition Position { get; set; }

	public abstract bool IsActive();

	public abstract Rate Rate { get; }

    public abstract void TakeCards(Card firstCard, Card secondCard);

    public abstract Task<(bool isPlayerMoved, uint betMoney)> MakeStep(int moneyForCall);
}
