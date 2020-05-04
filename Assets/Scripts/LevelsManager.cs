using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public GameObject obstacleManager;
    public CameraController cameraController;
    public Level[] levels;

    public int currentLevel = 0;
    public int shootCount = 0;
    public LevelResults levelResult;

    private GameObject player;
    private PuckController puckController;

    private bool firstLoad = true;
    private bool levelIsLoading = false;

    private void Start()
    {
        currentLevel = PlayerStats.CurrentLevel;

        player = GameObject.FindGameObjectWithTag("Player");
        puckController = player.GetComponent<PuckController>();
        puckController.ShootsEvents += AddShootCount;

        LoadLevel(currentLevel);
    }


    public void LoadLevel(int lvl)
    {
        levelIsLoading = true;
        Debug.Log("Wykonuje sie");
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        playerRb.velocity = Vector3.zero;

        playerRb.isKinematic = true;

        player.transform.position = levels[lvl].transform.position;

        Quaternion lvlRotation = Quaternion.Euler(levels[lvl].transform.eulerAngles);

        player.transform.rotation = lvlRotation;

        playerRb.isKinematic = false;


        PlayerStats.SetLevelShootCount(currentLevel, shootCount);
        shootCount = 0;

        currentLevel = lvl;
        PlayerStats.CurrentLevel = lvl;

        obstacleManager.GetComponent<ObstaclesManager>().LevelChanged(currentLevel);
        cameraController.LevelChanged(currentLevel);

        DebugLevelsShootCount();

        levelIsLoading = false;
    }

    public bool GetLevelLoadingState()
    {
        return levelIsLoading;
    }

    public void AddShootCount()
    {
        shootCount++;

        if(shootCount <= levels[currentLevel].levelResultThreshold[2])
        {
            levelResult = LevelResults.Gold;
            Debug.Log("Przypianie działa gold");
        }
        else if(shootCount <= levels[currentLevel].levelResultThreshold[1] && shootCount > levels[currentLevel].levelResultThreshold[2])
        {
            levelResult = LevelResults.Silver;
            Debug.Log("Przypianie działa silver");
        }
        else
        {
            levelResult = LevelResults.Bronze;
            Debug.Log("Przypianie działa bronze");
        }
    }
    private void DebugLevelsShootCount()
    {
        int[] levelShootCount = PlayerStats.GetLevelShootCountArray();
        int count = 0;
        foreach(int i in levelShootCount)
        {
            if(i != 0)
            {
                Debug.Log("On level: " + count + " Player make " + i + " shoots");
            }
            count++;
        }
    }
}
