using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    public Text withoutDeathsText, killsCountText;
    public GameObject levelsPanel, controlsPanel;

    void Start()
    {
        withoutDeathsText.text = $"Количество уровней подряд без смерти\n{PlayerPrefs.GetInt("Without Deaths", 0).ToString()}";
        killsCountText.text = $"Количество устранений\n{PlayerPrefs.GetInt("Kills Count", 0).ToString()}";
    }

    public void ShowLevels()
    {
        levelsPanel.SetActive(true);
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    public void Back()
    {
        controlsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void FirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SecondLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ThirdLevel()
    {
        SceneManager.LoadScene(3);
    }
}
