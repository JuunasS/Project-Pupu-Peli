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
        GameObject newValueSlider = Instantiate(valueSliderPrefab, generalInfoPanel.transform, false);
        /*
        newValueSlider.transform.SetParent(generalInfoPanel.transform, true);

        */
        newValueSlider.transform.position = valueSliderPos1.position;


        newValueSlider.GetComponent<IcoValueSlider>().SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);
    }
}
