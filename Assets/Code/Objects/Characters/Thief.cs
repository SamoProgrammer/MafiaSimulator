using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Thief : Character
{
    public void StealMoney(Character character){
        character.money -= 5;
    }
}
