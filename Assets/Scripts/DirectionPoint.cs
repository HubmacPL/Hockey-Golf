using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPoint : MonoBehaviour
{
    public GameObject targetPoint;
    public bool isLast;

    public Vector3 direction;
    public Quaternion lookRotation;

    private void Start()
    {
        if(!isLast)
        {
            direction = (targetPoint.transform.position - transform.position).normalized;

            direction.y = 0;


            lookRotation = Quaternion.LookRotation(direction);

            lookRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y-90, lookRotation.eulerAngles.z);
        }
    }
}
