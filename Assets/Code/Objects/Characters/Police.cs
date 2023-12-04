using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Police : Character
{
    UIScript ui;
    public PoliceStates policeState = PoliceStates.Patroling;
    NavMeshAgent myAgent;
    [SerializeField] int bribeCoolDown;
    [SerializeField] GameObject patrolWaypointsParent;
    [SerializeField] GameObject prisonInput;
    [SerializeField] GameObject prison;
    float bribeTimer = 0;
    public int bribeAmount = 50;
    GameObject suspect;
    Thief thiefScript;
    GameObject[] patrolWaypoints;
    List<GameObject> suspects;
    int patrolIndex = 0;


    protected override void Start()
    {

        myAgent = GetComponent<NavMeshAgent>();
        patrolWaypoints = GetChildObjects(patrolWaypointsParent);

    }


    protected override void Update()
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
        if (other.transform.tag == "Thief" || other.transform.tag == "Bribe")
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
    }

    void Chase()
    {
        myAgent.SetDestination(suspect.transform.position);
        if (Vector3.Distance(transform.position, suspect.transform.position) < 1f)
        {

            policeState = PoliceStates.EscortingSuspectToPrison;
            thiefScript.thiefState = ThiefStates.CapturedByPolice;

        }
    }


    void Arrest()
    {
        suspect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
        myAgent.SetDestination(prisonInput.transform.position);
        if (Vector3.Distance(transform.position, prisonInput.transform.position) < 1f)
        {

            thiefScript.thiefState = ThiefStates.InPrison;
            policeState = PoliceStates.Patroling;
        }
    }

    void Patrol()
    {
        myAgent.SetDestination(patrolWaypoints[patrolIndex].transform.position);

        // Check if the distance is very small to consider it reached the waypoint
        if (Vector3.Distance(transform.position, patrolWaypoints[patrolIndex].transform.position) < 1f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolWaypoints.Length)
            {
                patrolIndex = 0;
            }
        }
    }

    GameObject[] GetChildObjects(GameObject parent)
    {
        // GetComponentsInChildren also includes the parent itself, so we filter it out
        Transform[] childTransforms = parent.GetComponentsInChildren<Transform>(true);

        // Convert Transform array to GameObject array
        GameObject[] childObjects = new GameObject[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            childObjects[i - 1] = childTransforms[i].gameObject;
        }

        return childObjects;
    }
}
