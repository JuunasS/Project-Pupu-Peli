using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public MatrixTaskScriptObject currentTask;

    public GameObject taskPanel;
    public TMP_Text taskText;

    public GameObject taskValueSliderPrefab;
    public Transform firstTaskSliderPos;

    public TaskValueSlider taskValueSlider1;

    public MatrixTimer matrixTimer;

    public bool taskSlidersGenerated = false;

    private void OnEnable()
    {
        //taskValueSlider1.SetSliderValues("Age", currentTask.minAge, currentTask.maxAge);
        GenerateTaskSliders();

        // Start matrix game timer!!
        matrixTimer.StartMatrixTimer();
    }

    public void GenerateTaskSliders()
    {
        if (taskSlidersGenerated) { return; }
        int col = 0;

        if (currentTask.checkAge)
        {
            // Generate age task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, -25 - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Age", currentTask.minAge, currentTask.maxAge);
            col++;
        }

        if (currentTask.checkWeight)
        {
            // Generate weight task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, -50 - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Weight", (int)currentTask.minWeight, (int)currentTask.maxWeight);
            col++;
        }

        if (currentTask.checkHeight)
        {
            // Generate height task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, -75 - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Height", (int)currentTask.minHeight, (int)currentTask.maxHeight);
            col++;
        }

        if (currentTask.checkProductivity)
        {
            // Generate productivity task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, -100 - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Productivity", currentTask.minProductivity, currentTask.maxProductivity);
            col++;
        }

        taskPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(0, taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);
        taskSlidersGenerated = true;
    }

    public void SetTaskToPanel()
    {
        // Todo: set tasks to task panel based on game progression!


    }

    // Compares given icoObjects values to the current task requirements
    // If check-boolean is true compares the value given value
    // If all values are withing the requirements returns true
    public bool CompareSumbittedValues(List<IcoListObject> icoObjects)
    {
        for (int i = 0; i < icoObjects.Count; i++)
        {
            // Animations to indicate values check?
            Debug.Log("icoObjects[i].icoAge " + icoObjects[i].icoAge);
            Debug.Log("!currentTask.checkAge: " + currentTask.checkAge);
            Debug.Log("!currentTask.CompareAgeVal(icoObjects[i].icoAge " + !currentTask.CompareAgeVal(icoObjects[i].icoAge));
            if (currentTask.checkAge && !currentTask.CompareAgeVal(icoObjects[i].icoAge)) { return false; }
            if (currentTask.checkWeight && !currentTask.CompareWeightVal(icoObjects[i].icoWeight)) { return false; }
            if (currentTask.checkHeight && !currentTask.CompareHeightVal(icoObjects[i].icoHeight)) { return false; }
            if (currentTask.checkProductivity && !currentTask.CompareProductivityVal(icoObjects[i].icoProductivity)) { return false; }
        }

        return true;
    }

    public bool CompareSumbittedValue(IcoListObject icoObject)
    {
        // Animations to indicate values check?
        Debug.Log("icoObjects[i].icoAge " + icoObject.icoAge);
        Debug.Log("!currentTask.checkAge: " + currentTask.checkAge);
        Debug.Log("!currentTask.CompareAgeVal(icoObjects[i].icoAge " + !currentTask.CompareAgeVal(icoObject.icoAge));
        if (currentTask.checkAge && !currentTask.CompareAgeVal(icoObject.icoAge)) { return false; }
        if (currentTask.checkWeight && !currentTask.CompareWeightVal(icoObject.icoWeight)) { return false; }
        if (currentTask.checkHeight && !currentTask.CompareHeightVal(icoObject.icoHeight)) { return false; }
        if (currentTask.checkProductivity && !currentTask.CompareProductivityVal(icoObject.icoProductivity)) { return false; }

        return true;
    }
}
