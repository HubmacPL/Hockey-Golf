using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject puck;

    public float cameraRotationSpeed = 5.0f;

    public GameObject[] levelCamDirManagers;

    private GameObject currentCamDirManager;
    private LevelCamDirectionsManager lcdm;
    private DirectionPoint[] directionPoints;

    public DirectionPoint currentDirPoint;


    private void Start()
    {
        puck = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = puck.transform.position;
        Camera.main.transform.LookAt(puck.transform.position);

        List<float> pointsDistances = new List<float>();

        for (int i = 0; i < lcdm.dirPoints.Count; i++) 
        {
            pointsDistances.Add(Vector3.Distance(transform.position, lcdm.dirPoints[i].transform.position));
        }
        pointsDistances.Sort();

        for(int i=0; i < lcdm.dirPoints.Count; i++)
        {
            if(pointsDistances[0] == Vector3.Distance(transform.position, lcdm.dirPoints[i].transform.position))
            {
                if(lcdm.dirPoints[i].GetComponent<DirectionPoint>().isLast == false)
                {
                    currentDirPoint = lcdm.dirPoints[i].GetComponent<DirectionPoint>();
                }
                else
                {
                    currentDirPoint = lcdm.dirPoints[i-1].GetComponent<DirectionPoint>();
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, currentDirPoint.lookRotation, cameraRotationSpeed*Time.deltaTime);

        Debug.DrawRay(transform.position, Vector3.forward * 0.5f, Color.red);
        Debug.DrawRay(transform.position, Vector3.forward * -0.5f, Color.red);

        Debug.DrawRay(transform.position, Vector3.right * 0.5f, Color.red);
        Debug.DrawRay(transform.position, Vector3.right * -0.5f, Color.red);

    }

    public void LevelChanged(int loadedLevel)
    {
        currentCamDirManager = levelCamDirManagers[loadedLevel];
        lcdm = currentCamDirManager.GetComponent<LevelCamDirectionsManager>();
        directionPoints = currentCamDirManager.GetComponentsInChildren<DirectionPoint>();
        currentDirPoint = directionPoints[0];
    }
}
