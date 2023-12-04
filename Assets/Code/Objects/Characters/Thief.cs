using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Thief : Character
{

    [SerializeField] int stealAmount = 2;
    [SerializeField] GameObject workplaces;
    public ThiefStates thiefState = ThiefStates.GoingToStealMoney;
    public GameObject prisonOutput;
    public GameObject prison;
    [SerializeField] float jailTime = 30;
    Building[] availableBuildings;
    NavMeshAgent myAgent;
    float pickTimer = 0;
    float stealTimer = 0;
    float jailTimer = 0;
    Building targetBuilding;

    protected override void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        availableBuildings = GetChildObjects(workplaces);
        PickTarget();


    }

    protected override void Update()
    {
        if (thiefState != ThiefStates.CapturedByPolice && thiefState != ThiefStates.InPrison)
        {
            pickTimer += Time.deltaTime;
            stealTimer += Time.deltaTime;
            if (pickTimer > 60)
            {
                pickTimer = 0;
                PickTarget();

            }

            if (Vector3.Distance(transform.position, targetBuilding.transform.position) < 1f)
            {
                if (stealTimer > 5)
                {
                    stealTimer = 0;
                    StealMoney(targetBuilding);
                    Debug.Log($"{money} - {targetBuilding.money}");
                }
            }
            else
            {
                myAgent.SetDestination(targetBuilding.buildingPosition);
            }
        }

        if (thiefState == ThiefStates.InPrison)
        {
            myAgent.SetDestination(prison.transform.position);
            jailTimer += Time.deltaTime;
            if (jailTimer == jailTime)
            {
                transform.position = prisonOutput.transform.position;
                thiefState = ThiefStates.GoingToStealMoney;
            }
        }
    }


    void PickTarget()
    {
        var random = new System.Random();
        targetBuilding = availableBuildings[random.Next(availableBuildings.Length)];
        characterDestination = targetBuilding.buildingPosition;
        thiefState = ThiefStates.GoingToStealMoney;

    }
    public void StealMoney(Building building)
    {
        thiefState = ThiefStates.StealingMoney;
        building.money -= stealAmount;
        money += stealAmount;
    }

    Building[] GetChildObjects(GameObject parent)
    {
        // GetComponentsInChildren also includes the parent itself, so we filter it out
        Transform[] childTransforms = parent.GetComponentsInChildren<Transform>(true);

        // Convert Transform array to GameObject array
        Building[] childObjects = new Building[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            childObjects[i - 1] = childTransforms[i].gameObject.GetComponent<Building>();
        }

        return childObjects;
    }
}
