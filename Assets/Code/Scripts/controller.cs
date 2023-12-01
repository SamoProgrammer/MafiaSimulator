using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controller : MonoBehaviour
{
    [SerializeField] GameObject destinition;
    NavMeshAgent myAgent;
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        myAgent.SetDestination(destinition.transform.position);
    }


}
