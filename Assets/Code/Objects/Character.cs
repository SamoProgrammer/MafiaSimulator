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
    private Animator animator;
    protected Vector3 characterDestination;
    protected bool movementEnabled = false;

    protected virtual void Start()
    {
        characterAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        MoveCharacter();
        OnCharacterDeath();
    }

    public void MoveCharacter()
    {
        if (movementEnabled)
        {
            characterAgent.SetDestination(characterDestination);
            animator.SetBool("isWalking",true);
        }
        else if (!movementEnabled)
        {

        }
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
