using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Worker : Character
{
    private float time;
    private List<House> housesToWork;
    private House houseToWork;
    private WorkerStates workerState = WorkerStates.GoingToHouseForWork;

    protected override void Start()
    {
        base.Start();
        housesToWork = FindObjectsOfType<House>().ToList();
    }

    protected override void Update()
    {
        base.Update();
        Work();

    }

    public void Work()
    {
        if (health == 100)
        {
            if (workerState == WorkerStates.Working)
            {
                time += Time.deltaTime;
                if ((int)time == 20)
                {
                    money += 20;
                    time = 0;
                    workerState = WorkerStates.GoingToHouseForWork;
                }
            }
            else if (workerState == WorkerStates.GoingToHouseForWork)
            {
                houseToWork = housesToWork[Random.Range(0, housesToWork.Count)];
                characterDestination = houseToWork.transform.position;
                if (transform.position == houseToWork.transform.position)
                {
                    workerState = WorkerStates.Working;
                }
            }
        }


    }


}
