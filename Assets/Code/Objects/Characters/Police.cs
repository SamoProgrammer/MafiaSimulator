using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Police : Character
{
    public PoliceStates policeState = PoliceStates.Patroling;
    [SerializeField] public int bribeCoolDown;
    [SerializeField] public int bribeAmount = 50;
    public float bribeCooldownTimer = 0;
    [SerializeField] GameObject patrolWaypointsGameObject;
    [SerializeField] GameObject prisonInputGameObject;
    public GameObject suspect;
    public Thief thiefScript;
    public Assassin assassinScript;
    List<Transform> patrolWaypoints;
    int patrolIndex = 0;


    protected override void Start()
    {

        base.Start();
        patrolWaypoints = patrolWaypointsGameObject.GetComponentsInChildren<Transform>().ToList();

    }


    protected override void PerformUpdate()
    {
        bribeCooldownTimer += Time.deltaTime;
        if (policeState == PoliceStates.Patroling)
        {
            Patrol();
        }
        else if (policeState == PoliceStates.Chasing)
        {
            Chase();
        }
        else if (policeState == PoliceStates.EscortingSuspectToPrison)
        {
            Arrest();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (policeState == PoliceStates.Chasing)
        {
            if (other.tag == assassinScript.tag)
            {
                assassinScript.assasinState = AssassinStates.Arrested;
                policeState = PoliceStates.EscortingSuspectToPrison;
            }
            else if (other.tag == thiefScript.tag)
            {
                thiefScript.thiefState = ThiefStates.CapturedByPolice;
                policeState = PoliceStates.EscortingSuspectToPrison;
            }
        }
        else if (policeState == PoliceStates.EscortingSuspectToPrison && other.tag == "Prison")
        {

            if (suspect.tag == assassinScript.tag)
            {
                assassinScript.assasinState = AssassinStates.InPrison;
            }
            else if (suspect.tag == thiefScript.tag)
            {
                thiefScript.thiefState = ThiefStates.InPrison;
            }
            policeState = PoliceStates.Patroling;
        }
    }

    void Chase()
    {
        characterDestination = suspect.transform.position;

    }




    void Arrest()
    {
        suspect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
        characterDestination = prisonInputGameObject.transform.position;
    }

    void Patrol()
    {
        characterDestination = patrolWaypoints[patrolIndex].transform.position;

        // Check if the distance is very small to consider it reached the waypoint
        if (Vector3.Distance(transform.position, patrolWaypoints[patrolIndex].transform.position) < 1f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolWaypoints.Count)
            {
                patrolIndex = 0;
            }
        }
    }
}
