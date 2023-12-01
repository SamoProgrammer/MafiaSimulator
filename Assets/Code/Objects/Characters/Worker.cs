using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;


public class Worker : Character
{
    public void Work()
    {
        this.money += 5;
    }
}
