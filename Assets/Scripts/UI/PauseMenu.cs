using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false;
    public GameObject menu, settings, gameOver;
    public Slider soundSlider, sensitivitySlider;
    public static event EndGame.GameEnded onGameQuit;
	// Use this for initialization
	void Start () {
        soundSlider.onValueChanged.AddListener(ChangeSound);
        sensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
        soundSlider.value = GameSettings.instance.Sound;
        sensitivitySlider.value = GameSettings.instance.Sensitivity;
	}
    private void OnDestroy()
    {
        soundSlider.onValueChanged.RemoveListener(ChangeSound);
        sensitivitySlider.onValueChanged.RemoveListener(ChangeSensitivity);
    }
    void ChangeSound(float f)
    {
        GameSettings.instance.Sound = f;
    }
    void ChangeSensitivity(float f)
    {
        GameSettings.instance.Sensitivity = f;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex >= 1)
        {
            //  if the game is not paused... then pause it
            if (!isPaused && !(menu.activeInHierarchy || settings.activeInHierarchy))
                PauseGame();
            //  Else unpause the game
            else
                ResumeGame();
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        isPaused = false;
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void LoadMainMenuScene()
    {
        if (onGameQuit != null)
            onGameQuit();

        SceneManager.LoadScene(0);
    }
    public void SaveSettings()
    {
        GameSettings.instance.Save();
    }
}
