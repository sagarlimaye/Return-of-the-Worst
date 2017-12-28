using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Slider soundSlider, sensitivitySlider;

	public void QuitGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
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

}
