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

    protected virtual void Start()
    {

        characterAgent = GetComponent<NavMeshAgent>();
        gameCamera = FindFirstObjectByType<Camera>().gameObject;
        StartCoroutine(UpdateDestination());
    }

    protected virtual void Update()
    {
        if (health == 0)
        {
            return;
        }
        PerformUpdate();

    }

    // handle updates in this method instead of Update method beacause of being able to exit method if health is zero
    protected virtual void PerformUpdate()
    {

    }

    // update character position every 0.7 sec because it doesnt support game object
    private IEnumerator UpdateDestination()
    {
        while (characterDestination != null)
        {
            characterAgent.SetDestination(characterDestination);
            yield return new WaitForSeconds(0.7f);
        }
    }

    [ContextMenu("DeathMethod")]
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
