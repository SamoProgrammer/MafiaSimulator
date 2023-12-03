using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;


class Assassin : Character
{
    List<GameObject> charactersOnSight = new List<GameObject>();
    NavMeshAgent myNavmeshAgent;

    private void Start()
    {
        myNavmeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        if (charactersOnSight.Count != 0)
        {
            myNavmeshAgent.SetDestination(charactersOnSight.First().transform.position);
        }
        else
        {
            SearchingForVictim();
        }
        if (ReachedVictim())
        {
            KillCharacter();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Worker" || other.transform.tag == "Miner" || other.transform.tag == "Investor")
        {
            charactersOnSight.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Worker" || other.transform.tag == "Miner" || other.transform.tag == "Investor")
        {
            charactersOnSight.Remove(other.gameObject);
        }
    }

    public void KillCharacter()
    {
        GameObject characterToKill = charactersOnSight.First();
        Character character = characterToKill.GetComponent<Character>();
        character.health = 0;
        charactersOnSight.Remove(characterToKill);

    }

    public bool ReachedVictim()
    {
        if (transform.position == charactersOnSight.First().transform.position)
        {
            return true;
        }
        return false;
    }

    public void SearchingForVictim()
    {

    }
}

