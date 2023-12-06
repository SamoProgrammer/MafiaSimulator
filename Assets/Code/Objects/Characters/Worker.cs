using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Worker : Character
{
    private float workTimer;
    [SerializeField] float workTime = 20;
    private bool hasAHouseToWork = false;
    [SerializeField] GameObject workplaces;
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
            if (workTimer > workTime)
            {
                workTimer = 0;
                money += 20;
                hasAHouseToWork = false;
                movementEnabled = false;
                workerState = WorkerStates.GoingToHouseForWork;
            }
        }
        else if (workerState == WorkerStates.GoingToHouseForWork)
        {
            if (hasAHouseToWork)
            {
                Vector3 housePositionToWork = houseToWork.transform.position - new Vector3(-0.5f, 0, -0.5f);
                characterDestination = housePositionToWork;
                movementEnabled = true;
                Debug.Log(characterDestination);
                if (Vector3.Distance(transform.position, housePositionToWork) < 1.5f)
                {
                    workerState = WorkerStates.Working;
                }
            }
            else
            {
                houseToWork = housesToWork[Random.Range(0, housesToWork.Count)];
                hasAHouseToWork = true;

            }

        }



    }


}
