using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


class Assassin : Character
{
    List<GameObject> charactersOnSight = new List<GameObject>();
    NavMeshAgent myNavmeshAgent;
    public AssassinStates assasinState = AssassinStates.Idle;
    [SerializeField] int killCooldown;
    [SerializeField] GameObject citizens;
    GameObject[] citizensArray;
    List<GameObject> people = new List<GameObject>();
    Character victimScript;
    GameObject victim;
    System.Random random;
    float killTimer;

    protected override void Start()
    {
        base.Start();
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        citizensArray = GetChildObjects(citizens);
        foreach (var citizen in citizensArray)
        {
            if (citizen.tag == "Worker" || citizen.tag == "Miner" || citizen.tag == "Investor")
            {
                people.Add(citizen);
            }
        }
        random = new System.Random();

    }

    protected override void Update()
    {

        if (assasinState == AssassinStates.Idle)
        {
            killTimer += Time.deltaTime;
        }
        if (killTimer == killCooldown)
        {

            victim = people[random.Next(0, people.Count)];
            victimScript = victim.GetComponent<Character>();
            if (victimScript.health > 0 && assasinState == AssassinStates.Idle)
            {
                killTimer = 0;
                assasinState = AssassinStates.Killing;

            }

        }
        if (assasinState == AssassinStates.Killing)
        {
            KillCharacter();
        }

    }





    public void KillCharacter()
    {

        myNavmeshAgent.SetDestination(victim.transform.position);
        if (Vector3.Distance(transform.position, victim.transform.position) < 1)
        {
            victimScript.health = 0;
            assasinState = AssassinStates.Idle;
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
            childObjects[i - 1] = childTransforms[i].gameObject.GetComponent<GameObject>();
        }

        return childObjects;
    }
}

