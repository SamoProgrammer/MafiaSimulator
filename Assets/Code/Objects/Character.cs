using System.Collections;
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
    protected NavMeshAgent characterAgent;
    protected Vector3 characterDestination;
    protected bool movementEnabled;

    protected virtual void Start()
    {

        characterAgent = GetComponent<NavMeshAgent>();
        gameCamera = FindFirstObjectByType<Camera>().gameObject;
        InvokeRepeating("UpdateDestination", 1f, 1f);
    }

    protected virtual void Update()
    {
        if (health == 0)
        {
            return;
        }
        PerformUpdate();

    }


    protected virtual void PerformUpdate()
    {

    }


    private void UpdateDestination()
    {
        if (movementEnabled)
        {
            characterAgent.SetDestination(characterDestination);
        }
    }

    [ContextMenu("DeathMethod")]
    public void OnCharacterDeath()
    {
        if (characterAgent.isActiveAndEnabled)
        {
            characterAgent.isStopped = true;
            characterAgent.enabled = false;
            CancelInvoke("UpdateDestination");

            Doctor doctor = GameObject.FindGameObjectWithTag("Doctor").GetComponent<Doctor>();
            if (doctor && !doctor.charactersToHeal.Contains(this))
            {
                doctor.charactersToHeal.Add(this);
            }
            transform.Rotate(0, 0, 90f);
        }

    }

    private void OnMouseDown()
    {

        gameCamera.GetComponent<UIScript>().SetUi(this);

    }

    public void ReviveCharacter()
    {
        health = 100;
        characterAgent.enabled = true;
        characterAgent.isStopped = false;

        InvokeRepeating("UpdateDestination", 1f, 1f);

        transform.rotation = Quaternion.identity;
        PerformUpdate();
    }
}
