using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Doctor : Character
{
    public void HealCharacter(Character character)
    {
        character.health = 100;
    }
}
