using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour {

	public Text resolutionText;
	public Slider resolutionSlider;
    public Button applyButton;
    public delegate void ResolutionChangeHandler(Resolution r);
    public static event ResolutionChangeHandler OnResolutionChanged;

    void Start()
	{
        resolutionSlider.maxValue = Screen.resolutions.Length;
        Resolution current = Screen.currentResolution;
        for(int i=0; i<Screen.resolutions.Length; i++)
        {
            if(current.width == Screen.resolutions[i].width && current.height == Screen.resolutions[i].height)
            {
                resolutionSlider.value = i;
                break;
            }
        }
        applyButton.interactable = false;
        
	}

    public void ChangeResolution()
	{
		Resolution[] resolutions = Screen.resolutions;
        int sliderValue = (int)resolutionSlider.value;
        Screen.SetResolution(resolutions[sliderValue].width, resolutions[sliderValue].height, true);
        if (OnResolutionChanged != null)
            OnResolutionChanged(resolutions[sliderValue]);
    }
    public void ChangeSliderText()
	{
		Resolution[] resolutions = Screen.resolutions;
        int sliderValue = (int)resolutionSlider.value;
        resolutionText.text = resolutions[sliderValue] + "x" + resolutions[sliderValue];
	}
}
