using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public CourseSelectManager courseSelectManager;
    //Main Menu buttons
    [Header("Main Menu Buttons")]
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject creditsButton;
    public GameObject exitButton;

    //Pages
    [Header("Pages")]
    public GameObject coursesPage;
    public GameObject mainPage;

    public void OnPlayButtonClick()
    {
        coursesPage.SetActive(true);
        mainPage.SetActive(false);
        courseSelectManager.SetupCoursePage(0);

    }
    public void OnOptionsButtonClick()
    {

    }
    public void OnCreditsButtonClick()
    {

    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void OnLevelButtonClick(Text buttonText)
    {
        int textNumber = int.Parse(buttonText.text) - 1;
        PlayerStats.CurrentLevel = textNumber;
        Debug.Log("Loading scene: " + textNumber);
    }
}
