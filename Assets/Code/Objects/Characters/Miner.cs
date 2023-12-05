using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Miner : Character
{
    private float time;
    public MinerStates minerState = MinerStates.Mining;
    public Building mine;

    protected override void Update()
    {
        base.Update();
        Mining();
    }

    public void Mining()
    {
        if (health == 100)
        {
            if (mine.money > 0)
            {
                time += Time.deltaTime;
                if ((int)time == 7)
                {
                    money += 10;
                    mine.money -= 10;
                    time = 0;
                }
            }

        }
    }
}
