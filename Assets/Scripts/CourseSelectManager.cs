using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CourseSelectManager : MonoBehaviour
{
    public Text courseTitle;
    public string[] courseTitles;
    public GameObject leftArrow, rightArrow;
    public Text[] levelButtonsTexts = new Text[9];
    public int coursesAmount = 2;
    [Header("Arrows Colors")]
    public Color activeArrow;
    public Color inactiveArrow;

    private int currentCourseSelect = 0;

    public void SetupCoursePage(int course)
    {
        currentCourseSelect = course;
        if (currentCourseSelect == 0)
        {
            leftArrow.GetComponent<Image>().color = inactiveArrow;
            rightArrow.GetComponent<Image>().color = activeArrow;
        }
        else if (currentCourseSelect > 0 && currentCourseSelect < coursesAmount - 1)
        {
            leftArrow.GetComponent<Image>().color = activeArrow;
            rightArrow.GetComponent<Image>().color = activeArrow;
        }
        else if (currentCourseSelect == coursesAmount - 1) 
        {
            leftArrow.GetComponent<Image>().color = activeArrow;
            rightArrow.GetComponent<Image>().color = inactiveArrow;
        }

        switch(currentCourseSelect)
        {
            case 0:
                courseTitle.text = courseTitles[0];

                int i = 1;
                foreach(Text txt in levelButtonsTexts)
                {
                    string number = i.ToString();

                    txt.text = number;
                    i++;
                }

                break;
            case 1:
                courseTitle.text = courseTitles[1];

                int j = 10;
                foreach(Text txt in levelButtonsTexts)
                {
                    string number = j.ToString();

                    txt.text = number;
                    j++;
                }
                break;
        }
    }
    public void OnArrowClick(int dir) //0-left 1-right
    {
        if(dir == 0)
        {
            if(leftArrow.GetComponent<Image>().color == activeArrow)
            {
                currentCourseSelect--;
                SetupCoursePage(currentCourseSelect);
            }
        }
        else
        {
            if(rightArrow.GetComponent<Image>().color == activeArrow)
            {
                currentCourseSelect++;
                SetupCoursePage(currentCourseSelect);
            }
        }
    }
}
