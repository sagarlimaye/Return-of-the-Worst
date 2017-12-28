using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[Serializable]
public class SettingsData
{
    public Resolution resolution;
    public int masterSound;
    public int sensitvity; 
}

public class GameSettings : MonoBehaviour {

    public bool debugMode;
    public static GameSettings instance = null;
    private float sound = 5.0f;
    private float sensitivity = 5.0f;

    public delegate void VolChangeHandler(float v);
    public static event VolChangeHandler OnVolumeChanged;
    public delegate void SensitvityChangeHandler(float s);
    public static event SensitvityChangeHandler OnSensitivityChanged;

    public float Sensitivity
    {
        get
        {
            return sensitivity;
        }

        set
        {
            sensitivity = value;
            if (OnSensitivityChanged != null)
                OnSensitivityChanged(value);
        }
    }
    public float Sound
    {
        get
        {
            return sound;
        }

        set
        {
            sound = value;
            if (OnVolumeChanged != null)
                OnVolumeChanged(value);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    void Awake()
    {
	    //Check if instance already exists
	    if (instance == null)
		    //if not, set instance to this
		    instance = this;
	    //If instance already exists and it's not this:
	    else if (instance != this)
		    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a SettingsManager.
		    Destroy(gameObject);    
	    //Sets this to not be destroyed when reloading scene
	    DontDestroyOnLoad(instance);
        Load();
    }
    public void Load()
    {
        // Load settings from disk
        Sound = PlayerPrefs.GetFloat("sound");
        Sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }
    public void Save()
    {
        // Save settings to disk
        PlayerPrefs.SetFloat("sound", sound);
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }
}