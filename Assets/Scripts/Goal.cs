using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int nextLevelNumber;
    public GameObject gmLevelsManager;

    private LevelsManager levelsManager;
    private UIManager uiManager;

    private void Start()
    {
        levelsManager = gmLevelsManager.GetComponent<LevelsManager>();
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && levelsManager.GetLevelLoadingState()==false)
        {
            uiManager.SetFinishMenuEnabled(levelsManager.currentLevel, levelsManager.shootCount, levelsManager.levelResult);
        }
    }
}
