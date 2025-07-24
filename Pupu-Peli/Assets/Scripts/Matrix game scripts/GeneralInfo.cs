using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GeneralInfo : MonoBehaviour, IDropHandler
{
    public GameObject generalInfoPanel;
    public TMP_Text generalInfoText;

    public IcoListObject activeInfoItem;
    public Transform infoItemPosition;
    public float itemSetSpeed = 1f;

    public GameObject valueSliderPanel;
    public GameObject valueSliderPrefab;
    public Transform valueSliderPos1;

    public int sliderPaddingBottom;
    public int PanelSizeOffsetY = 50;

    public List<IcoValueSlider> valueSliderList;

    private Vector2 originalSize;

    private void Awake()
    {
        originalSize = generalInfoPanel.GetComponent<RectTransform>().sizeDelta;
    }

    private void OnEnable()
    {
        ResetInfoPanel();
    }

    private void OnDisable()
    {
        // Remove activeInfoItem
        if (activeInfoItem != null)
        {
            activeInfoItem.setToNewPosition = false;
            activeInfoItem.OnEndDrag(null);
        }
        ResetInfoPanel();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into input!");
        if (DragManager.Instance.dragObject == null) { return; }

        if (DragManager.Instance.dragObject.GetComponent<IcoListObject>() != null)
        {
            if (activeInfoItem == null)
            {
                Debug.Log("Received input: " + DragManager.Instance.dragObject);
                activeInfoItem = DragManager.Instance.dragObject.GetComponent<IcoListObject>();

                activeInfoItem.setToNewPosition = true;
                activeInfoItem.dragging = false;
                activeInfoItem.isActive = true;
                activeInfoItem.currentParentApp = this.gameObject;
                SetObjectToPosition(activeInfoItem.gameObject);
                ActivateInfoPanel(activeInfoItem);
                DragManager.Instance.dragObject = null;
            }
        }
    }

    public void SetObjectToPosition(GameObject inputObj)
    {
        inputObj.transform.SetParent(infoItemPosition, true);
        inputObj.GetComponent<IcoListObject>().MoveToPos(infoItemPosition.localPosition, itemSetSpeed);

    }

    public void RemoveActiveInfoItem(IcoListObject obj)
    {
        if (activeInfoItem != null)
        {
            if (activeInfoItem == obj)
            {
                activeInfoItem = null;
                ResetInfoPanel();
            }
        }
    }

    public void ActivateInfoPanel(IcoListObject icoListObj)
    {
        generalInfoPanel.SetActive(true);

        if (activeInfoItem != null && !FindAnyObjectByType<OutputPanel>().IsObjectInList(activeInfoItem.gameObject)) { activeInfoItem.isActive = false; }

        icoListObj.isActive = true;

        activeInfoItem = icoListObj;

        //generalInfoText.gameObject.SetActive(true);
        //generalInfoText.text = icoListObj.icoData.name;

        // Display other icoObject values in a meter in a 0 to 100 range
        GenerateValueSliders(icoListObj);

    }

    public void GenerateValueSliders(IcoListObject icoObj)
    {
        if (valueSliderList == null || valueSliderList.Count == 0)
        {
            valueSliderPanel.SetActive(true);
            Debug.Log("Generating new general info slider values");
            float sliderObjHeight = valueSliderPrefab.GetComponent<RectTransform>().rect.height;


            GameObject nameValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            nameValue.transform.localPosition = valueSliderPos1.localPosition;
            nameValue.GetComponent<IcoValueSlider>().SetSliderValues("Name", icoObj.icoData.icoName);

            GameObject ageValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            ageValue.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, -sliderObjHeight - sliderPaddingBottom, 0);
            ageValue.GetComponent<IcoValueSlider>().SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);

            GameObject weightValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            weightValue.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 2, 0); ;
            weightValue.GetComponent<IcoValueSlider>().SetSliderValues("Weight", (int)icoObj.icoData.minWeight, (int)icoObj.icoData.maxWeight, (int)icoObj.icoWeight);

            GameObject heightValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            heightValue.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 3, 0);
            heightValue.GetComponent<IcoValueSlider>().SetSliderValues("Height", (int)icoObj.icoData.minHeight, (int)icoObj.icoData.maxHeight, (int)icoObj.icoHeight);

            GameObject productivityValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            productivityValue.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 4, 0);
            productivityValue.GetComponent<IcoValueSlider>().SetSliderValues("Productivity", icoObj.icoData.minProductivity, icoObj.icoData.maxProductivity, icoObj.icoProductivity);

            GameObject locationValue = Instantiate(valueSliderPrefab, valueSliderPanel.transform, false);
            locationValue.transform.localPosition = valueSliderPos1.localPosition + new Vector3(0, (-sliderObjHeight - sliderPaddingBottom) * 5, 0);
            locationValue.GetComponent<IcoValueSlider>().SetSliderValues("Location", icoObj.icoData.icoLocation.location + ": " + icoObj.icoData.icoLocation.GetPlaces());

            valueSliderList.Add(nameValue.GetComponent<IcoValueSlider>());
            valueSliderList.Add(ageValue.GetComponent<IcoValueSlider>());
            valueSliderList.Add(weightValue.GetComponent<IcoValueSlider>());
            valueSliderList.Add(heightValue.GetComponent<IcoValueSlider>());
            valueSliderList.Add(productivityValue.GetComponent<IcoValueSlider>());
            valueSliderList.Add(locationValue.GetComponent<IcoValueSlider>());
        }
        else
        {
            Debug.Log("Setting new general info slider values");
            valueSliderList[0].SetSliderValues("Name", icoObj.icoData.icoName);
            valueSliderList[1].SetSliderValues("Age", icoObj.icoData.minAge, icoObj.icoData.maxAge, icoObj.icoAge);
            valueSliderList[2].SetSliderValues("Weight", (int)icoObj.icoData.minWeight, (int)icoObj.icoData.maxWeight, (int)icoObj.icoWeight);
            valueSliderList[3].SetSliderValues("Height", (int)icoObj.icoData.minHeight, (int)icoObj.icoData.maxHeight, (int)icoObj.icoHeight);
            valueSliderList[4].SetSliderValues("Productivity", icoObj.icoData.minProductivity, icoObj.icoData.maxProductivity, icoObj.icoProductivity);
            valueSliderList[5].SetSliderValues("Location", icoObj.icoData.icoLocation.location + ": " + icoObj.icoData.icoLocation.GetPlaces());
        }
        infoItemPosition.transform.parent.SetAsLastSibling();

        RectTransform temp = generalInfoPanel.GetComponent<RectTransform>();
        temp.sizeDelta = new Vector2(temp.sizeDelta.x, temp.sizeDelta.y + valueSliderList.Count * valueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y + valueSliderList.Count * PanelSizeOffsetY);
    }

    public void ResetInfoPanel()
    {
        for (int i = valueSliderList.Count - 1; i >= 0; i--)
        {
            IcoValueSlider temp = valueSliderList[i];
            valueSliderList.Remove(temp);
            Destroy(temp.gameObject);
        }
        valueSliderPanel.SetActive(false);
        generalInfoText.gameObject.SetActive(false);
        generalInfoPanel.GetComponent<RectTransform>().sizeDelta = originalSize;
    }
}
