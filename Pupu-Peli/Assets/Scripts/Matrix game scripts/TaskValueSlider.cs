using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskValueSlider : MonoBehaviour
{
    public TMP_Text valNameText;
    public Slider minValSlider;
    public Slider maxValSlider;

    public TMP_Text minValueText;
    public TMP_Text maxValueText;

   public void SetSliderMaxAndMinValues(int min, int max)
    {
        // Set both sliders min and max values so that setting slider values works even with different mins and maxes


        maxValSlider.minValue = min;
        minValSlider.maxValue = max;


        minValSlider.minValue = min;
        minValSlider.maxValue = max;
    }

    public void SetSliderValues(string valueName, int minValue, int maxValue)
    {
        valNameText.text = valueName;

        minValSlider.minValue = 0;
        minValSlider.maxValue = 100;
        minValSlider.value = minValue;

        maxValSlider.minValue = 0;
        minValSlider.maxValue = 100;
        maxValSlider.value = (maxValue - 100) * -1;

        minValueText.text = minValue.ToString();
        maxValueText.text = maxValue.ToString();
        //valueText.text = val.ToString();
    }
}

