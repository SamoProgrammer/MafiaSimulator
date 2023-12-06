using UnityEngine;


public class Investor : Character
{
    private float workTimer;
    public InvestorStates investorStates = InvestorStates.Investing;


    protected override void PerformUpdate()
    {
        // simple workTimer mechanism
        Invest();
    }


    // adding 5 percent to character money in every 10 seconds
    private void Invest()
    {
        workTimer += Time.deltaTime;
        if (workTimer > 10)
        {
            this.money += this.money * 5 / 100;
            workTimer = 0;
        }
    }



}
