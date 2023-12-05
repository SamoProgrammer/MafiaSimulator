using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace MafiaSimulator.Code.Objects
{

}
public class Character : MonoBehaviour
{
    private GameObject gameCamera;

    public int health = 100;
    public int money = 0;
    private NavMeshAgent characterAgent;
    private Animator animator;
    protected Vector3 characterDestination;
    protected bool movementEnabled = false;

    protected virtual void Start()
    {

        characterAgent = GetComponent<NavMeshAgent>();
        gameCamera = FindFirstObjectByType<Camera>().gameObject;
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
            // animator.SetBool("isWalking", true);
        }
        // else if (!movementEnabled)
        // {
        //     animator.SetBool("isWalking", false);

        // }
    }

    public void OnCharacterDeath()
    {
        if (health == 0)
        {
            Doctor doctor = GameObject.FindGameObjectWithTag("Doctor").GetComponent<Doctor>();
            doctor.charactersToHeal.Add(this);

            transform.Rotate(0, 0, 90f);
        }
    }

    private void OnMouseDown()
    {

        gameCamera.GetComponent<UIScript>().SetUi(this);

    }
}
