using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Police : Character
{
    public PoliceStates policeState = PoliceStates.Patroling;
    [SerializeField] int bribeCoolDown;
    [SerializeField] GameObject patrolWaypointsParent;
    [SerializeField] GameObject prisonInput;
    float bribeTimer = 0;
    public int bribeAmount = 50;
    GameObject suspect;
    Thief thiefScript;
    Assassin assassinScript;
    List<Transform> patrolWaypoints;
    List<GameObject> suspects;
    int patrolIndex = 0;


    protected override void Start()
    {

        base.Start();
        patrolWaypoints = patrolWaypointsParent.GetComponentsInChildren<Transform>().ToList();

    }


    protected override void PerformUpdate()
    {
        bribeTimer += Time.deltaTime;
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
                    if (bribeTimer > bribeCoolDown)
                    {
                        money += bribeAmount;
                        thiefScript.money -= bribeAmount;
                        bribeTimer = 0;
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
        characterAgent.SetDestination(prisonInput.transform.position);
        if (Vector3.Distance(transform.position, prisonInput.transform.position) < 1f)
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

    // GameObject[] GetChildObjects(GameObject parent)
    // {
    //     // GetComponentsInChildren also includes the parent itself, so we filter it out
    //     Transform[] childTransforms = parent.GetComponentsInChildren<Transform>(true);

    //     // Convert Transform array to GameObject array
    //     GameObject[] childObjects = new GameObject[childTransforms.Length - 1];
    //     for (int i = 1; i < childTransforms.Length; i++)
    //     {
    //         childObjects[i - 1] = childTransforms[i].gameObject;
    //     }

    //     return childObjects;
    // }
}
