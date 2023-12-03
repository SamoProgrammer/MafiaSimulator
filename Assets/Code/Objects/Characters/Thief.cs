using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using UnityEngine;


public class Thief : Character
{
    private float time;
    private bool stealCooldown = false;
    public Character characterToStealMoney;
=======
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

    private void Start()
    {
        availableBuildings = workplaces.GetComponentsInChildren<Building>();
        PickTarget();

    }
>>>>>>> fc32d43dfe05024726d41e696d07f780e5546a90

    protected override void Update()
    {
        base.Update();
<<<<<<< HEAD
        // simple timer mechanism
        StealMoney();
        StealCooldown();


    }


    public void StealMoney()
    {
        if (!stealCooldown)
        {
            if (characterToStealMoney != null)
            {
                if (transform.position == characterToStealMoney.transform.position)
                {
                    characterToStealMoney.money -= 5;
                }
            }
        }

    }

    public void StealCooldown()
    {
        if (stealCooldown)
        {
            time += Time.deltaTime;
            if ((int)time == 5)
            {
                stealCooldown = false;
                time = 0;
            }
        }
=======
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
        characterDestination = targetBuilding.buildingPosition;

    }
    public void StealMoney(Building building)
    {
        building.money -= stealAmount;
>>>>>>> fc32d43dfe05024726d41e696d07f780e5546a90
    }
}
