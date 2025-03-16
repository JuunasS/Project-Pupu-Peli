using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public MatrixTaskScriptObject currentTask;

    public GameObject taskPanel;
    public TMP_Text taskText;

    public GameObject TaskValueSliderPrefab;

    public TaskValueSlider taskValueSlider1;

    private void Start()
    {
        taskValueSlider1.SetSliderValues("Age", currentTask.minAge, currentTask.maxAge);
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
}
