using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code thanks to Brackeys tutorials (https://www.youtube.com/watch?v=0HwZQt94uHQ)
public class ScreenFade : MonoBehaviour {

    public Texture2D fadeTexture;
    public float fadeSpeed;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    // Use this for initialization
    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.b, GUI.color.g, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }
    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }
}
