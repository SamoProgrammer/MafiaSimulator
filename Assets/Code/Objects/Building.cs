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
