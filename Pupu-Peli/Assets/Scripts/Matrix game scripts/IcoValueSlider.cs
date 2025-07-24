using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IcoValueSlider : MonoBehaviour
{
    public GameObject valuePanel;

    public TMP_Text valNameText;
    public Slider valSlider;

    public TMP_Text valueText;

    public TMP_Text valueSliderText;
    public TMP_Text minValueText;
    public TMP_Text maxValueText;

    public void SetSliderValues(string valueName, int minValue, int maxValue, int val)
    {
        valuePanel.SetActive(true);
        valueText.gameObject.SetActive(false);

        valNameText.text = valueName;

        valSlider.minValue = minValue;
        valSlider.maxValue = maxValue;
        valSlider.value = val;

        minValueText.text = minValue.ToString();
        maxValueText.text = maxValue.ToString();
        valueSliderText.text = val.ToString();
    }

    public void SetSliderValues(string valueName, string vText)
    {
        valuePanel.SetActive(false);
        valueText.gameObject.SetActive(true);
        valNameText.text = valueName;
        valueText.text = vText;
    }
}
