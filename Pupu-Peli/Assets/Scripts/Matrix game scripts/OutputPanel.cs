using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputPanel : MonoBehaviour, IDropHandler
{
    public TaskManager TaskManager;

    public GameObject scrollView;
    public GameObject scrollViewContent;

    public Transform firstOutputPosition;

    public List<GameObject> outputObjects;

    public int rowMax;

    public int row = 0, col = 0;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into input!");
        if (DragManager.Instance.dragObject == null) { return; }

        if (DragManager.Instance.dragObject.GetComponent<IcoListObject>() != null)
        {
            if (!outputObjects.Contains(DragManager.Instance.dragObject)) // Does not contain [!]
            {
                Debug.Log("Received input: " + DragManager.Instance.dragObject);
                outputObjects.Add(DragManager.Instance.dragObject);

                DragManager.Instance.dragObject.GetComponent<IcoListObject>().setToNewPosition = true;
                DragManager.Instance.dragObject.GetComponent<IcoListObject>().dragging = false;
                DragManager.Instance.dragObject.GetComponent<IcoListObject>().isActive = true;
                SetObjectToPosition(DragManager.Instance.dragObject);
                DragManager.Instance.dragObject = null;
            }
        }
    }

    public void SetObjectToPosition(GameObject inputObj)
    {

        float icoObjWidth = inputObj.GetComponent<RectTransform>().rect.width;
        float icoObjHeight = inputObj.GetComponent<RectTransform>().rect.height;

        inputObj.transform.SetParent(scrollViewContent.transform, true);

        Vector3 tempPos = firstOutputPosition.transform.localPosition;

        Debug.Log("row: " + row + "\n" + "col: " + col);
        Debug.Log("New inputObj localPos: " + inputObj.transform.localPosition);

        tempPos += new Vector3(icoObjWidth * row * inputObj.GetComponent<RectTransform>().localScale.x, icoObjHeight * col * inputObj.GetComponent<RectTransform>().localScale.y, 0);

        inputObj.GetComponent<IcoListObject>().MoveToPos(tempPos);

        if (row + 1 == rowMax) // End of the row
        {
            Debug.Log("Adding column!!");
            //col--;
            row = 0;
        }
        else
        {
            Debug.Log("Adding row!!");
            row++;
        }

    }

    // Probably redundant now
    public void CheckObjectList()
    {
        Debug.Log("Checking input objects!");

        for (int i = 0; i < outputObjects.Count; i++)
        {
            Debug.Log(firstOutputPosition.childCount == 0);
            if (firstOutputPosition.childCount == 0)
            {
                outputObjects.RemoveAt(i);

                // Set object into the input panel again in correct positions!

                //row--;
                //col--; ?
            }
        }
    }

    [ContextMenu("Test Input panel submit")]
    public void SubmitInput()
    {

        if (outputObjects.Count == 0) { Debug.LogError("No input given!!!"); return; }
        // Check if given ico objects are correct
        // Have a list of tasks (ScriptableObjects?) where one is chosen randomly and check against it

        List<IcoListObject> icoListObjects = new List<IcoListObject>();

        for (int i = 0; i < outputObjects.Count; i++)

        {
            icoListObjects.Add(outputObjects[i].GetComponent<IcoListObject>());
        }

        if(TaskManager.CompareSumbittedValues(icoListObjects))
        {
            Debug.Log("CORRECT OUTPUT!");
        }
        else
        {

            Debug.Log("WRONG OUTPUT!");
        }

    }
}
