using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MafiaSimulator.Code.Objects
{

}
public class Character : MonoBehaviour
{
    public int health = 100;
    public int money = 0;
    private NavMeshAgent characterAgent;
    public Vector3 characterDestination;

    protected virtual void Start()
    {
        characterAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {

        OnCharacterDeath();
    }

    public void MoveCharacter()
    {
        characterAgent.SetDestination(characterDestination);
    }

    public void OnCharacterDeath()
    {
        if (health == 0)
        {
            Doctor doctor = GameObject.FindGameObjectWithTag("Doctor").GetComponent<Doctor>();
            doctor.charactersToHeal.Add(this);
        }
    }
}
