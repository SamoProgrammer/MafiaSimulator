using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Thief : Character
{
    [SerializeField] int stealAmount = 10;
    [SerializeField] GameObject workplacesGameObject;
    public ThiefStates thiefState = ThiefStates.GoingToStealMoney;
    [SerializeField] GameObject prisonExitGameObject;
    [SerializeField] GameObject prisonEnterGameObject;
    [SerializeField] GameObject prisonGameObject;
    [SerializeField] int jailTime = 30;
    float jailTimer = 0;
    List<Building> availableBuildings;
    float stealTimer = 0;
    [SerializeField] float stealTime = 30;
    private Building targetBuilding;
    private bool hasAHouseToSteal = false;

    protected override void Start()
    {
        base.Start();
        availableBuildings = workplacesGameObject.GetComponentsInChildren<Building>().ToList();
        PickTarget();


    }

    protected override void PerformUpdate()
    {
        if (thiefState != ThiefStates.CapturedByPolice && thiefState != ThiefStates.InPrison)
        {
            stealTimer += Time.deltaTime;
            if (hasAHouseToSteal)
            {
                if (stealTimer < stealTime)
                {
                    stealTimer = 0;
                    StealMoney(targetBuilding);
                }

            }
            else
            {
                PickTarget();
                hasAHouseToSteal = true;
            }

        }

        if (thiefState == ThiefStates.CapturedByPolice)
        {
            characterDestination = prisonEnterGameObject.transform.position;
        }

        if (thiefState == ThiefStates.InPrison)
        {
            transform.position = prisonGameObject.transform.position;
            jailTimer += Time.deltaTime;
            if (jailTimer > jailTime)
            {
                transform.position = prisonExitGameObject.transform.position;
                thiefState = ThiefStates.GoingToStealMoney;
            }
        }
    }


    void PickTarget()
    {
        targetBuilding = availableBuildings[Random.Range(0, availableBuildings.Count)];
        characterDestination = targetBuilding.buildingPosition;
        thiefState = ThiefStates.GoingToStealMoney;

    }

    public void StealMoney(Building building)
    {
        characterDestination = targetBuilding.buildingPosition;
        if (Vector3.Distance(transform.position, targetBuilding.transform.position) < 1f)
        {
            thiefState = ThiefStates.StealingMoney;
            building.money -= stealAmount;
            money += stealAmount;
            hasAHouseToSteal = false;

        }
    }

}
