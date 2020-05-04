using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelResults
{
    Bronze,
    Silver,
    Gold
}

public class UIManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject loadLevelScreen;
    public GameObject inGameMenu;

    private bool MenuEnabled = false;
    private AudioSource[] audioSources;

    public GameObject gmLevelsManager;
    private LevelsManager levelsManager;

    [Header("Level Finished Menu")]
    public GameObject levelFinished;
    public Text finishedText;
    public Text shootCountText;
    public Text resultText;
    public Color[] resultTextColors = new Color[3];


    private void Start()
    {
        levelsManager = gmLevelsManager.GetComponent<LevelsManager>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        audioSources = player.GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenuEnabled(!MenuEnabled);
        }
    }

    public void SetMenuEnabled(bool enable)
    {

        if (enable == true && levelFinished.active == false)
        {
            MenuEnabled = true;
            Time.timeScale = 0.0f;
            SetPauseScreenEnable(true);
            SetInGameUIEnable(false);

            foreach (AudioSource ac in audioSources)
            {
                ac.enabled = false;
            }
        }
        else if(enable == false && levelFinished.activeSelf == true)
        {
            MenuEnabled = false;
            Time.timeScale = 1.0f;
            SetPauseScreenEnable(false);
            SetInGameUIEnable(false);

            foreach (AudioSource ac in audioSources)
            {
                ac.enabled = false;
            }
        }
        else if(enable == false && levelFinished.active == false)
        {
            MenuEnabled = false;
            Time.timeScale = 1.0f;
            SetPauseScreenEnable(false);
            SetInGameUIEnable(true);
            foreach (AudioSource ac in audioSources)
            {
                ac.enabled = true;
            }
        }
    }
    public void SetFinishMenuEnabled(int currentLevel, int shootCount, LevelResults result)
    {
        currentLevel++;
        finishedText.text = currentLevel.ToString();
        shootCountText.text = shootCount.ToString();

        switch(result)
        {
            case LevelResults.Bronze:
                resultText.color = resultTextColors[0];
                resultText.text = "BRONZE";
                Debug.Log("No i nadpisuje tekst na bronz");
                break;
            case LevelResults.Silver:
                resultText.color = resultTextColors[1];
                resultText.text = "SILVER";
                Debug.Log("No i nadpisuje tekst na srebro");
                break;
            case LevelResults.Gold:
                resultText.color = resultTextColors[2];
                resultText.text = "GOLD";
                Debug.Log("No i nadpisuje tekst na zloto");
                break;
        }
        foreach(AudioSource ac in audioSources)
        {
            ac.enabled = false;
        }
        inGameMenu.SetActive(false);
        levelFinished.SetActive(true);
        Time.timeScale = 0;
    }
    public void SetFinishedMenuDisabled()
    {
        finishedText.text = "";
        shootCountText.text = "";
        resultText.text = "";
        resultText.color = resultTextColors[0];


        Time.timeScale = 1;

        foreach (AudioSource ac in audioSources)
        {
            ac.enabled = true;
        }

        inGameMenu.SetActive(true);
        levelFinished.SetActive(false);
    }
    public void RepeatLevel()
    {
        SetFinishedMenuDisabled();

        levelsManager.LoadLevel(levelsManager.currentLevel);
    }
    public void LoadNextLevel()
    {
        SetFinishedMenuDisabled();
        levelsManager.LoadLevel(levelsManager.currentLevel + 1);
    }


    public void SetPauseScreenEnable(bool enable)
    {
        pauseScreen.SetActive(enable);
    }
    public void SetLoadScreenEnable(bool enable)
    {
        loadLevelScreen.SetActive(enable);
    }
    public void SetInGameUIEnable(bool enable)
    {
        inGameMenu.SetActive(enable);
    }

    public void LoadLevel(Text level)
    {
        levelsManager.LoadLevel(int.Parse(level.text));
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
