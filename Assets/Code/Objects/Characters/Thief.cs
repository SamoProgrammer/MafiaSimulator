using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class Thief : Character
{
    private float time;
    private bool stealCooldown = false;
    public Character characterToStealMoney;

    protected override void Update()
    {
        base.Update();
        // simple timer mechanism
        StealMoney();
        StealCooldown();


    }


    public void StealMoney()
    {
        if (!stealCooldown)
        {
            if (characterToStealMoney != null)
            {
                if (transform.position == characterToStealMoney.transform.position)
                {
                    characterToStealMoney.money -= 5;
                }
            }
        }

    }

    public void StealCooldown()
    {
        if (stealCooldown)
        {
            time += Time.deltaTime;
            if ((int)time == 5)
            {
                stealCooldown = false;
                time = 0;
            }
        }
    }
}
