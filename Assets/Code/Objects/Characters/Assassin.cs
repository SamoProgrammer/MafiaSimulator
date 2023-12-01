using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Assassin : Character
{
    public void KillCharacter(Character character)
    {
        character.health = 0;
        OnCharacterDeath();
    }
}

