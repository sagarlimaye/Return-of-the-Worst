using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {
    public Text text;

    public void ChangeText(float sliderValue)
    {
        text.text = sliderValue.ToString();
        
    }
}
