using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class Investor : Character
{
    private float time;
    public InvestorStates investorStates = InvestorStates.Investing;


    protected override void Update()
    {
        base.Update();
        // simple timer mechanism
        Invest();
    }


    // adding 5 percent to character money in every 5 seconds
    private void Invest()
    {
        if (health == 100)
        {
            time += Time.deltaTime;
            if ((int)time == 5)
            {
                this.money += this.money * 5 / 100;
                Debug.Log(this.money);
                time = 0;
            }
        }
    }



}
