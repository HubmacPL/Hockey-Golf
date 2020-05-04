using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject levelsManager;

    private List<GameObject> obstaclesList;

    private void Awake()
    {
        GameObject[] obstaclesInScene = GameObject.FindGameObjectsWithTag("Obstacle");
        obstaclesList = new List<GameObject>(obstaclesInScene);
    }
    private void Start()
    {
    }

    public void LevelChanged(int lvl)
    {
        foreach(GameObject gm in obstaclesList)
        {
            gm.GetComponentInChildren<Obstacle>().LevelLoaded(lvl);
        }
    }
}
