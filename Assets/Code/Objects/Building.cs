using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class Building : MonoBehaviour
{
    public int money;
    public Vector3 buildingPosition;


    private void Start()
    {
        buildingPosition = transform.position;
    }
}
