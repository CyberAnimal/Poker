using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour
{
    public abstract void CanStart();

    public abstract Task<(bool isPlayerMoved, uint betMoney, Rate updatedRate)> CanStep(int moneyForCall);
}