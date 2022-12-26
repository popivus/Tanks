using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider hpSlider;
    public GameObject aim, pausePanel, deathPanel, reloadImage, winText;
    public bool paused = false, isDead = false;
    public Text ammoText, killsText;

    public static UIController instanse;
    public KeyCode pauseButton = KeyCode.Escape;

    public void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        killsText.text = $"{GameManager.instanse.killsCount} из {GameManager.instanse.enemiesOnMap} устранений";
        hpSlider.value = MoveController.instanse.hp;
        aim.transform.position = Input.mousePosition;
        reloadImage.transform.position = Input.mousePosition;
        if (Input.GetKeyUp(pauseButton) && !isDead)
        {
            PauseGame(true);
        }
    }

    private void PauseGame(bool isPause)
    {
        pausePanel.SetActive(isPause);
        Time.timeScale = isPause ? 0f : 1f;
        paused = isPause;
        Cursor.visible = isPause;
        aim.SetActive(!isPause);
        reloadImage.SetActive(false);
    }

    public void ContinueGame()
    {
        PauseGame(false);
    }

    public void RestartGame()
    {
        PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        PauseGame(false);
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    public void SetAmmoText(string ammo)
    {
        ammoText.text = ammo;
    }

    public void ReloadUI(bool isReloading)
    {
        aim.SetActive(!isReloading);
        reloadImage.SetActive(isReloading);
    }

    public void DeathUI()
    {
        PlayerPrefs.SetInt("Without Deaths", 0);
        Time.timeScale = 0f;
        paused = true;
        Cursor.visible = true;
        aim.SetActive(false);
        reloadImage.SetActive(false);
        deathPanel.SetActive(true);
    }

    public void FinishLevel()
    {
        winText.SetActive(true);
        if (SceneManager.GetActiveScene().buildIndex != 3) StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
        else StartCoroutine(LoadNextLevel(0));
    }

    private IEnumerator LoadNextLevel(int levelNumber)
    {
        yield return new WaitForSeconds(3f);
        PlayerPrefs.SetInt("Without Deaths", PlayerPrefs.GetInt("Without Deaths", 0) + 1);
        Cursor.visible = true;
        SceneManager.LoadScene(levelNumber);
    }
}
