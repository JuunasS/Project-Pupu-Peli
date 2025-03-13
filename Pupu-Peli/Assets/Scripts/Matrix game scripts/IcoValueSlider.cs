using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IcoValueSlider : MonoBehaviour
{
    public TMP_Text valNameText;
    public Slider valSlider;


    public TMP_Text valueText;
    public TMP_Text minValueText;
    public TMP_Text maxValueText;

    public void SetSliderValues(string valueName, int minValue, int maxValue, int val)
    {
        valNameText.text = valueName;

        valSlider.minValue = minValue;
        valSlider.maxValue = maxValue;
        valSlider.value = val;

        minValueText.text = minValue.ToString();
        maxValueText.text = maxValue.ToString();
        valueText.text = val.ToString();
    }
}
