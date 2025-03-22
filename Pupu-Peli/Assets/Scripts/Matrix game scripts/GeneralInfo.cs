using NUnit.Framework;
using System.Collections.Generic;
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

    public int sliderPaddingBottom;

    public List<IcoValueSlider> valueSliderList;

    public void ActivateInfoPanel(IcoListObject icoListObj)
    {
        generalInfoPanel.SetActive(true);

        if (activeInfoItem != null && !FindAnyObjectByType<OutputPanel>().IsObjectInList(activeInfoItem.gameObject)) { activeInfoItem.isActive = false; }

        icoListObj.isActive = true;

        activeInfoItem = icoListObj;

        generalInfoText.text = icoListObj.icoData.name;

        // Display other icoObject values in a meter in a 0 to 100 range
        GenerateValueSliders(icoListObj);

    }

    public void GenerateValueSliders(IcoListObject icoObj)
    {
        if (valueSliderList == null || valueSliderList.Count == 0)
        {
            Debug.Log("Generating new general info slider values");
            float sliderObjHeight = valueSliderPrefab.GetComponent<RectTransform>().rect.height;

            GameObject newValueSlider = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
            newValueSlider.transform.localPosition = valueSliderPos1.localPosition;
            newValueSlider.GetComponent<IcoValueSlider>().SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);

            GameObject newValueSlider2 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
            newValueSlider2.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, -sliderObjHeight - sliderPaddingBottom, 0);
            newValueSlider2.GetComponent<IcoValueSlider>().SetSliderValues("Weight", (int)icoObj.icoData.minWeight, (int)icoObj.icoData.maxWeight, (int)icoObj.icoWeight);

            GameObject newValueSlider3 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
            newValueSlider3.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 2, 0);
            newValueSlider3.GetComponent<IcoValueSlider>().SetSliderValues("Height", (int)icoObj.icoData.minHeight, (int)icoObj.icoData.maxHeight, (int)icoObj.icoHeight);

            GameObject newValueSlider4 = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
            newValueSlider4.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 3, 0);
            newValueSlider4.GetComponent<IcoValueSlider>().SetSliderValues("Productivity", icoObj.icoData.minProductivity, icoObj.icoData.maxProductivity, icoObj.icoProductivity);

            valueSliderList.Add(newValueSlider.GetComponent<IcoValueSlider>());
            valueSliderList.Add(newValueSlider2.GetComponent<IcoValueSlider>());
            valueSliderList.Add(newValueSlider3.GetComponent<IcoValueSlider>());
            valueSliderList.Add(newValueSlider4.GetComponent<IcoValueSlider>());
        }
        else
        {
            Debug.Log("Setting new general info slider values");
            valueSliderList[0].SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);
            valueSliderList[1].SetSliderValues("Weight", (int)icoObj.icoData.minWeight, (int)icoObj.icoData.maxWeight, (int)icoObj.icoWeight);
            valueSliderList[2].SetSliderValues("Height", (int)icoObj.icoData.minHeight, (int)icoObj.icoData.maxHeight, (int)icoObj.icoHeight);
            valueSliderList[3].SetSliderValues("Productivity", icoObj.icoData.minProductivity, icoObj.icoData.maxProductivity, icoObj.icoProductivity);
        }
    }
}
