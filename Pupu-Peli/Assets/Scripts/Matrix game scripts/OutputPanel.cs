using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputPanel : MonoBehaviour, IDropHandler
{
    public GameObject scrollView;
    public GameObject scrollViewContent;

    public Transform firstInputPosition;

    public List<GameObject> inputObjects;

    public int rowMax;

    public int row = 0, col = 0;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into input!");
        if (DragManager.Instance.dragObject == null) { return; }

        if (DragManager.Instance.dragObject.GetComponent<IcoListObject>() != null)
        {            if (!inputObjects.Contains(DragManager.Instance.dragObject)) // Does not contain [!]
            {
                Debug.Log("Received input: " + DragManager.Instance.dragObject);

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

        Vector3 tempPos = firstInputPosition.transform.localPosition;

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

        for (int i = 0; i < inputObjects.Count; i++)
        {
            Debug.Log(firstInputPosition.childCount == 0);
            if (firstInputPosition.childCount == 0)
            {
                inputObjects.RemoveAt(i);

                // Set object into the input panel again in correct positions!

                //row--;
                //col--; ?
            }
        }
    }

    [ContextMenu("Test Input panel submit")]
    public void SubmitInput()
    {

        if (inputObjects.Count == 0) { Debug.LogError("No input given!!!"); return; }
        // Check if given ico objects are correct
        // Have a list of tasks (ScriptableObjects?) where one is chosen randomly and check against it

    }
}
