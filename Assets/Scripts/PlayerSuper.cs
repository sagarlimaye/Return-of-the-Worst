using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSuper : MonoBehaviour
{

    public float StartingSuper = 0;

    public float CurrentSuper;
    public Slider SuperSlider;
    public Text SuperSliderText;

    void Start()
    {
        CurrentSuper = StartingSuper;
        SuperSlider.value = Mathf.Clamp(CurrentSuper, 0, 100);
        SuperSliderText.text = Mathf.Clamp(CurrentSuper, 0, 100).ToString();

    }

    public void changeSuper(float diff) //should be positive 10 or negative 100
    {
        CurrentSuper = Mathf.Clamp(CurrentSuper + diff, 0, 100);
        SuperSlider.value = CurrentSuper;
        SuperSliderText.text = CurrentSuper.ToString();

    }
    public bool canSuper()
    {
        if (PlayerAttributes.instance.UnlimitedSuper)
            return true;
        if (CurrentSuper >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
