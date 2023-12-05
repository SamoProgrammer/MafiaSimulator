using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Worker : Character
{
    private float time;
    private float pickTimer;
    private bool hasAHouseToWork = false;
    [SerializeField] GameObject workplaces;
    private List<Building> housesToWork;
    private Building houseToWork;
    public WorkerStates workerState = WorkerStates.GoingToHouseForWork;

    protected override void Start()
    {
        base.Start();
        housesToWork = GetChildObjects(workplaces);

    }

    protected override void Update()
    {
        base.Update();
        Work();

    }

    List<Building> GetChildObjects(GameObject parent)
    {
        // GetComponentsInChildren also includes the parent itself, so we filter it out
        Transform[] childTransforms = parent.GetComponentsInChildren<Transform>(true);

        // Convert Transform array to GameObject array
        Building[] childObjects = new Building[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            childObjects[i - 1] = childTransforms[i].gameObject.GetComponent<Building>();
        }

        return childObjects.ToList();
    }

    public void Work()
    {
        if (health == 100)
        {
            if (workerState == WorkerStates.Working)
            {
                time += Time.deltaTime;
                print((int)time);
                if ((int)time == 20)
                {
                    money += 20;
                    time = 0;
                    workerState = WorkerStates.GoingToHouseForWork;
                }
                else
                {
                    hasAHouseToWork = false;
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
                    movementEnabled = true;
                    if (Vector3.Distance(transform.position, housePositionToWork) < 1.5f)
                    {
                        workerState = WorkerStates.Working;
                    }
                }

            }
        }


    }


}
