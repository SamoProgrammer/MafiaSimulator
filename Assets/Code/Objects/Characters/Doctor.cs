using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Doctor : Character
{
    public List<Character> charactersToHeal = new List<Character>();
    public DoctorStates doctorState = DoctorStates.Idle;

    protected override void PerformUpdate()
    {
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
            if (Vector3.Distance(transform.position, characterPositionToHeal) < 1.5f)
            {
                characterToHeal.ReviveCharacter();
            }
            charactersToHeal.Remove(characterToHeal);
            doctorState = DoctorStates.Idle;
        }

    }
}
