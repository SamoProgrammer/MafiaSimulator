using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


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
            Vector3 characterPositionToHeal = characterToHeal.transform.position - new Vector3(-0.5f, 0, -0.5f);
            characterDestination = characterPositionToHeal;
            movementEnabled = true;

            if (Vector3.Distance(transform.position, characterPositionToHeal) < 1.5f)
            {
                characterToHeal.health = 100;
                characterToHeal.transform.Rotate(0, 0, -90f);
            }
            charactersToHeal.Remove(characterToHeal);
        }
        else
        {
            doctorState = DoctorStates.Idle;
        }
    }
}
