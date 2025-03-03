using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour, IDropHandler
{
    public GameObject scrollView;
    public GameObject scrollViewContent;

    public Transform firstInputPosition;

    public List<GameObject> inputObjects;


    public int row = 0, col = 0;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into input!");
        if (DragManager.Instance.dragObject == null) { return; }
        if (DragManager.Instance.dragObject.GetComponent<IcoListObject>() != null)
        {
            // Does not contain [!]
            if (!inputObjects.Contains(DragManager.Instance.dragObject))
            {
                Debug.Log("Received input: " + DragManager.Instance.dragObject);

                DragManager.Instance.dragObject.GetComponent<IcoListObject>().setToNewPosition = true;
                //DragManager.Instance.dragObject.GetComponent<IcoListObject>().gameObject.transform.SetParent(firstInputPosition);
                //DragManager.Instance.dragObject.GetComponent<IcoListObject>().newPosition = new Vector3(0, 0, 0);
                DragManager.Instance.dragObject.GetComponent<IcoListObject>().dragging = false;
                SetObjectToPosition(DragManager.Instance.dragObject);
            }
        }
    }

    public void SetObjectToPosition(GameObject inputObj)
    {
        // Populate content panel based on given list

        float icoObjWidth = inputObj.GetComponent<RectTransform>().rect.width;
        float icoObjHeight = inputObj.GetComponent<RectTransform>().rect.height;

        inputObj.transform.SetParent(scrollViewContent.transform, true);

        Vector3 tempPos = firstInputPosition.transform.localPosition;

        tempPos += new Vector3(icoObjWidth * row, icoObjHeight * col, 0);

        Debug.Log("NEw inputObj localPos: " + inputObj.transform.localPosition);

        //inputObj.transform.localPosition += new Vector3(icoObjWidth * row, icoObjHeight * col, 0);

        inputObj.GetComponent<IcoListObject>().MoveToPos(tempPos);


        if (row + 1 == 3) // End of the row
        {
            Debug.Log("Adding column!!");
            col--;
            row = 0;
        }
        else
        {
            Debug.Log("Adding row!!");
            row++;
        }


        // Set Matrix panel width and height based on the generatex ico objects!

        Debug.Log("matrixPanelRectTransform.rect.size!!");
        /*
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(3 * icoObjWidth + 50, this.GetComponent<RectTransform>().sizeDelta.y * icoObjHeight + 50);
        scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().sizeDelta.x, scrollView.GetComponent<RectTransform>().sizeDelta.x * icoObjHeight + 50);
        */


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
            }
        }
    }

    [ContextMenu("Test Input panel submit")]
    private void SubmitInput()
    {

        if (inputObjects.Count == 0) { Debug.LogError("No input given!!!"); return; }
        // Check if given ico objects are correct
    }
}
