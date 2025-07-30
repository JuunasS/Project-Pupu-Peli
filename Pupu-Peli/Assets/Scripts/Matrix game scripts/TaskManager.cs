using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public MatrixTaskMasterlist taskMasterList;
    public MatrixTaskScriptObject currentTask;

    public GameObject taskPanel;
    public TMP_Text taskText;

    public GameObject taskValueSliderPrefab;
    public Transform firstTaskSliderPos;

    public TaskValueSlider taskValueSlider1;

    public MatrixTimer matrixTimer;

    public bool taskSlidersGenerated = false;

    private Vector2 currentPanelSizeOffSet;


    private void OnEnable()
    {
        //taskValueSlider1.SetSliderValues("Age", currentTask.minAge, currentTask.maxAge);
        Debug.Log("Setting random tasks!");
        SetRandomCurrentTask();
        GenerateTaskSliders();

        // Start matrix game timer!!
        matrixTimer.StartMatrixTimer();
    }

    public void SetRandomCurrentTask()
    {
        currentTask = taskMasterList.GetRandomMatrixTaskScriptObj();
    }

    public void GenerateTaskSliders()
    {
        
        if (taskSlidersGenerated)
        {
            taskPanel.GetComponent<RectTransform>().sizeDelta -= currentPanelSizeOffSet;
            TaskValueSlider[] sliders = GetComponentsInChildren<TaskValueSlider>();
            for (int i = sliders.Length - 1; 0 <= i; i--)
            {
                Destroy(sliders[i].gameObject);
            }
        }

        int col = 0;

        if (currentTask.checkAge)
        {
            // Generate age task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, (-50 * col) - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Age", currentTask.minAge, currentTask.maxAge);
            col++;
        }

        if (currentTask.checkWeight)
        {
            // Generate weight task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, (-50 * col) - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Weight", (int)currentTask.minWeight, (int)currentTask.maxWeight);
            col++;
        }

        if (currentTask.checkHeight)
        {
            // Generate height task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, (-50 * col) - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Height", (int)currentTask.minHeight, (int)currentTask.maxHeight);
            col++;
        }

        if (currentTask.checkProductivity)
        {
            // Generate productivity task slider
            GameObject tempTaskSlider = Instantiate(taskValueSliderPrefab);

            tempTaskSlider.transform.SetParent(taskPanel.transform, false);

            tempTaskSlider.transform.localPosition = firstTaskSliderPos.localPosition + new Vector3(0, (-50 * col) - taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);

            tempTaskSlider.GetComponent<TaskValueSlider>().SetSliderValues("Productivity", currentTask.minProductivity, currentTask.maxProductivity);
            col++;
        }

        currentPanelSizeOffSet = new Vector2(0, (50 * col) + taskValueSliderPrefab.GetComponent<RectTransform>().sizeDelta.y * col);
        taskPanel.GetComponent<RectTransform>().sizeDelta += currentPanelSizeOffSet;
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
