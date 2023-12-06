using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Police : Character
{
    public PoliceStates policeState = PoliceStates.Patroling;
    [SerializeField] int bribeCoolDown;
    float bribeCooldownTimer = 0;
    [SerializeField] GameObject patrolWaypointsGameObject;
    [SerializeField] GameObject prisonInputGameObject;
    [SerializeField] int bribeAmount = 50;
    GameObject suspect;
    Thief thiefScript;
    Assassin assassinScript;
    List<Transform> patrolWaypoints;
    List<GameObject> suspects;
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
        if (other.tag == "Thief" || other.tag == "Bribe" || other.tag == "Assassin")
        {
            if (other.tag == "Assassin")
            {
                assassinScript = other.GetComponent<Assassin>();
                if (assassinScript.assasinState == AssassinStates.Killing)
                {
                    if (policeState == PoliceStates.Patroling)
                    {
                        policeState = PoliceStates.Chasing;
                        suspect = other.GameObject();
                    }
                }
            }
            else if (other.tag == "Thief" || other.tag == "Bribe")
            {
                thiefScript = other.GetComponent<Thief>();
                if (thiefScript.tag == "Bribe" && thiefScript.money > bribeAmount)
                {
                    if (bribeCooldownTimer > bribeCoolDown)
                    {
                        money += bribeAmount;
                        thiefScript.money -= bribeAmount;
                        bribeCooldownTimer = 0;
                    }
                    else
                    {
                        if (thiefScript.thiefState == ThiefStates.StealingMoney || thiefScript.thiefState == ThiefStates.GoingToStealMoney)
                        {
                            if (policeState == PoliceStates.Patroling)
                            {
                                policeState = PoliceStates.Chasing;
                                suspect = other.GameObject();
                            }
                        }
                    }

                }
                else
                {
                    if (thiefScript.thiefState == ThiefStates.StealingMoney || thiefScript.thiefState == ThiefStates.GoingToStealMoney || assassinScript.assasinState == AssassinStates.Killing)
                    {
                        if (policeState == PoliceStates.Patroling)
                        {
                            policeState = PoliceStates.Chasing;
                            suspect = other.GameObject();
                        }
                    }
                }
            }


        }
    }

    void Chase()
    {
        characterAgent.SetDestination(suspect.transform.position);
        if (Vector3.Distance(transform.position, suspect.transform.position) < 1f)
        {

            policeState = PoliceStates.EscortingSuspectToPrison;
            if (suspect.tag == "Assassin")
            {
                assassinScript.assasinState = AssassinStates.Arrested;
            }
            else
            {

                thiefScript.thiefState = ThiefStates.CapturedByPolice;
            }

        }
    }


    void Arrest()
    {
        suspect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
        characterAgent.SetDestination(prisonInputGameObject.transform.position);
        if (Vector3.Distance(transform.position, prisonInputGameObject.transform.position) < 1f)
        {
            if (suspect.tag == "Assassin")
            {
                assassinScript.assasinState = AssassinStates.InPrison;
            }
            else
            {
                thiefScript.thiefState = ThiefStates.InPrison;
            }
            policeState = PoliceStates.Patroling;
        }
    }

    void Patrol()
    {
        characterAgent.SetDestination(patrolWaypoints[patrolIndex].transform.position);

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
