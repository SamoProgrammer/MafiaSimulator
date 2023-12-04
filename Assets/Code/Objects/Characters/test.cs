using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject kossher;
    void Start()
    {
        transform.position = kossher.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
