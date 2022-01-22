using System;
using System.Threading.Tasks;

public class PlayerAiAction : PlayerAction
{
    public override void CanStart()
    {
        throw new NotImplementedException();
    }

    public override async Task<(bool isPlayerMoved, uint betMoney, Rate updatedRate)> CanStep(int moneyForCall)
    {
        throw new NotImplementedException();
    }
}