using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralInfo : MonoBehaviour
{
    public GameObject generalInfoPanel;
    public TMP_Text generalInfoText;

    public IcoListObject activeInfoItem;

    public GameObject valueSliderPrefab;
    public Transform valueSliderPos1;


    public void ActivateInfoPanel(IcoListObject icoListObj)
    {
        generalInfoPanel.SetActive(true);

        if(activeInfoItem != null ) { activeInfoItem.isActive = false; }

        icoListObj.isActive = true;

        activeInfoItem = icoListObj;

        generalInfoText.text = icoListObj.icoData.name;

        // Display other icoObject values in a meter in a 0 to 100 range
        GenerateValueSliders(icoListObj);

    }

    public void GenerateValueSliders(IcoListObject icoObj)
    {
        float sliderObjHeight = valueSliderPrefab.GetComponent<RectTransform>().rect.height;

        GameObject newValueSlider = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
        newValueSlider.transform.localPosition = valueSliderPos1.localPosition;
        newValueSlider.GetComponent<IcoValueSlider>().SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);
/*
        newValueSlider.transform.SetParent(generalInfoPanel.transform, true);
        */

        GameObject newValueSlider2 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
        newValueSlider2.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, -sliderObjHeight, 0);
        newValueSlider2.GetComponent<IcoValueSlider>().SetSliderValues("Weight", (int) icoObj.icoData.minWeight, (int) icoObj.icoData.maxWeight, (int) icoObj.icoWeight);

        GameObject newValueSlider3 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
        newValueSlider3.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, -sliderObjHeight *2, 0);
        newValueSlider3.GetComponent<IcoValueSlider>().SetSliderValues("Height", (int) icoObj.icoData.minHeight, (int) icoObj.icoData.maxHeight, (int) icoObj.icoHeight);

        GameObject newValueSlider4 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
        newValueSlider4.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, -sliderObjHeight * 3, 0);
        newValueSlider4.GetComponent<IcoValueSlider>().SetSliderValues("Productivity", icoObj.icoData.minProductivity, icoObj.icoData.maxProductivity, icoObj.icoProductivity);
    }
}
