using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderProgress : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text text;
    public void OnSliderValueChanged(float value)
    {
        text.text = value.ToString("0.0");
    }
}
