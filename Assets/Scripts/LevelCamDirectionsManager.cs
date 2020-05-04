using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamDirectionsManager : MonoBehaviour
{
    public List<GameObject> dirPoints;
    public int level;

    private void Start()
    {
        foreach(Transform child in transform)
        {
            dirPoints.Add(child.gameObject);
        }
    }
}
