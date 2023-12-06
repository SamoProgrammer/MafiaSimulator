using UnityEngine;


public class Investor : Character
{
    private float workTimer;
    [SerializeField] float workTime = 10;
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
        if (workTimer > workTime)
        {
            this.money += this.money * 5 / 100;
            workTimer = 0;
        }
    }



}
