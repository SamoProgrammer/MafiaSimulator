using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Thief : Character
{

    [SerializeField] int stealAmount = 2;
    [SerializeField] GameObject workplaces;
    Building[] availableBuildings;
    float pickTimer = 0;
    float stealTimer = 0;
    Building targetBuilding;

    protected override void Start()
    {
        base.Start();
        availableBuildings = workplaces.GetComponentsInChildren<Building>();
        PickTarget();

    }

    protected override void Update()
    {
        base.Update();
        pickTimer += Time.deltaTime;
        stealTimer += Time.deltaTime;
        if (pickTimer > 60)
        {
            pickTimer = 0;
            PickTarget();

        }
        if (this.transform.position == targetBuilding.transform.position)
        {
            if (stealTimer > 5)
            {
                StealMoney(targetBuilding);
            }
        }
    }


    void PickTarget()
    {
        var random = new System.Random();
        targetBuilding = availableBuildings[random.Next(availableBuildings.Length)];
        movementEnabled=true;
        characterDestination = targetBuilding.buildingPosition;

    }
    public void StealMoney(Building building)
    {
        building.money -= stealAmount;
    }
}
