using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncoverPuck : MonoBehaviour
{
    public Material defaultWallsMaterial;
    public Material transparentWallsMaterial;

    private int rayLayer;
    private List<GameObject> coverPuckWalls = new List<GameObject>();

    private void Start()
    {
        rayLayer = LayerMask.GetMask("Walls");
    }

    private void Update()
    {
        Vector3 rayDirection = (gameObject.transform.position - Camera.main.transform.position);

        float rayDistance = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position) + 1.0f;

        Ray ray = new Ray(Camera.main.transform.position, rayDirection);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, rayLayer))
        {
            hit.collider.gameObject.GetComponent<MeshRenderer>().material = transparentWallsMaterial;

            if(!coverPuckWalls.Contains(hit.collider.gameObject))
            {
                coverPuckWalls.Add(hit.collider.gameObject);
            }
        }
        else
        {
            if(coverPuckWalls.Count != 0)
            {
                foreach (GameObject gm in coverPuckWalls)
                {
                    gm.GetComponent<MeshRenderer>().material = defaultWallsMaterial;
                }
                coverPuckWalls.Clear();
            }
        }


    }
}
