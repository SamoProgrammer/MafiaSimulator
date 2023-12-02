using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class Investor : Character
{
    float time;


    private void Update()
    {
        time += Time.deltaTime;
        Invest();
    }



    private void Invest()
    {

        if (time < 5.1 && time > 4.9)
        {

            time = 0;
            this.money += this.money * 5 / 100;
            Debug.Log(this.money);
        }
    }



}
