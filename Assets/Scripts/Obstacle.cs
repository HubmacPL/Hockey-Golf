using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObstacleMoveMode
{
    Static,
    Rotation,
    CheckpointMove
}

public class Obstacle : MonoBehaviour
{
    [Header("For all modes")]
    public ObstacleMoveMode obstacleMoveMode;
    public float moveSpeed;
    public int worksAtLevel;

    public bool enabled = false;
    [Header("For rotation")]
    public Vector3 rotationAxis;

    [Header("For Check point move")]
    public GameObject[] checkPoints;
    private int moveToCheckpoint = 0;


    private void FixedUpdate()
    {
        ObstacleMove();
    }

    private void ObstacleMove()
    {
        if(enabled)
        {
            switch (obstacleMoveMode)
            {
                case ObstacleMoveMode.Static:
                    break;
                case ObstacleMoveMode.Rotation:
                    RotationMove();
                    break;
                case ObstacleMoveMode.CheckpointMove:
                    CheckPointMove();
                    break;
            }
        }
    }

    private void RotationMove()
    {
        transform.Rotate(rotationAxis, moveSpeed);
    }
    private void CheckPointMove()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            if(i == moveToCheckpoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, checkPoints[i].transform.position, moveSpeed * Time.deltaTime);
            }
        }
        if(transform.position == checkPoints[moveToCheckpoint].transform.position)
        {
            if (moveToCheckpoint == checkPoints.Length - 1)
            {
                moveToCheckpoint = 0;
            }
            else
            {
                moveToCheckpoint++;
            }
        }
    }

    public void LevelLoaded(int lvl)
    {
        if(lvl == worksAtLevel)
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }
}
