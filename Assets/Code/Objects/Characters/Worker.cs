using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Worker : Character
{
    private float workTimer;
    private bool hasAHouseToWork = false;
    public GameObject workplaces;
    private List<Building> housesToWork;
    private Building houseToWork;
    public WorkerStates workerState = WorkerStates.GoingToHouseForWork;

    protected override void Start()
    {
        base.Start();
        housesToWork = workplaces.GetComponentsInChildren<Building>().ToList();

    }

    protected override void PerformUpdate()
    {
        Work();
    }

    public void Work()
    {

        if (workerState == WorkerStates.Working)
        {
            workTimer += Time.deltaTime;
            if (workTimer > 20)
            {
                workTimer = 0;
                money += 20;
                hasAHouseToWork = false;
                workerState = WorkerStates.GoingToHouseForWork;
            }
        }
        else if (workerState == WorkerStates.GoingToHouseForWork)
        {
            if (!hasAHouseToWork)
            {
                houseToWork = housesToWork[Random.Range(0, housesToWork.Count)];
                hasAHouseToWork = true;
                Vector3 housePositionToWork = houseToWork.transform.position - new Vector3(-0.5f, 0, -0.5f);
                characterDestination = housePositionToWork;
                if (Vector3.Distance(transform.position, housePositionToWork) < 1.5f)
                {
                    workerState = WorkerStates.Working;
                }
            }

        }



    }


}
