using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;

    public Quaternion lookRotation;
    public Vector3 direction;

    private void Start()
    {
        
    }
    private void Update()
    {
        direction = (target.position - transform.position).normalized;

        direction.y = 0;

        lookRotation = Quaternion.LookRotation(direction);


        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

}
