using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Doctor : Character
{
    public List<Character> charactersToHeal = new List<Character>();
    public DoctorStates doctorState = DoctorStates.Idle;

    protected override void Update()
    {
        base.Update();
        HealCharacter();

    }

    public void HealCharacter()
    {
        if (charactersToHeal.Count != 0)
        {
            Character characterToHeal = charactersToHeal.First();
            doctorState = DoctorStates.MovingToHeal;
            characterDestination = characterToHeal.transform.position;

            if (transform.position == characterToHeal.transform.position)
            {
                characterToHeal.health = 100;
            }
            charactersToHeal.Remove(characterToHeal);
        }
        else
        {
            doctorState = DoctorStates.Idle;
        }
    }
}
