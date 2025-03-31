using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputPanel : MonoBehaviour, IDropHandler
{
    public TaskManager TaskManager;
    public MatrixScoreManager ScoreManager;

    public GameObject scrollView;
    public GameObject scrollViewContent;

    public Transform firstOutputPosition;

    public List<GameObject> outputObjects;

    public int rowMax;

    public int row = 0, col = 0;

    public float setToPositionSpeed;
    public float rePositionSpeed;

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
        if (row == rowMax) { inputObj.GetComponent<IcoListObject>().MoveToPos(inputObj.GetComponent<IcoListObject>().returnPos, inputObj.GetComponent<IcoListObject>().returnDuration); return; }


        float icoObjWidth = inputObj.GetComponent<RectTransform>().rect.width;
        float icoObjHeight = inputObj.GetComponent<RectTransform>().rect.height;

        inputObj.transform.SetParent(scrollViewContent.transform, true);

        Vector3 tempPos = firstOutputPosition.transform.localPosition;

        Debug.Log("row: " + row + "\n" + "col: " + col);
        Debug.Log("New inputObj localPos: " + inputObj.transform.localPosition);

        tempPos += new Vector3(icoObjWidth * row * inputObj.GetComponent<RectTransform>().localScale.x, icoObjHeight * col * inputObj.GetComponent<RectTransform>().localScale.y, 0);

        inputObj.GetComponent<IcoListObject>().MoveToPos(tempPos, setToPositionSpeed);

        row++;


    }

    public void RemoveObjectFromList(GameObject obj)
    {
        outputObjects.Remove(obj);
        row--;
        if (row < 0) { row = 0; }

        // Reposition all objects in outputPanel!
        RepositionOutputListObjects();
    }

    public bool IsObjectInList(GameObject obj)
    {
        if (outputObjects.Contains(obj)) { return true; }

        return false;
    }

    public void RepositionOutputListObjects()
    {
        int tempRow = 0;
        for (int i = 0; i < outputObjects.Count; i++)
        {
            float icoObjWidth = outputObjects[i].GetComponent<RectTransform>().rect.width;
            float icoObjHeight = outputObjects[i].GetComponent<RectTransform>().rect.height;

            Vector3 tempPos = firstOutputPosition.transform.localPosition;

            Debug.Log("row: " + row + "\n" + "col: " + col);
            Debug.Log("New inputObj localPos: " + outputObjects[i].transform.localPosition);

            tempPos += new Vector3(icoObjWidth * tempRow * outputObjects[i].GetComponent<RectTransform>().localScale.x, 0, 0);

            outputObjects[i].GetComponent<IcoListObject>().MoveToPos(tempPos, rePositionSpeed);

            tempRow++;
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

        if (TaskManager.CompareSumbittedValues(icoListObjects))
        {
            Debug.Log("CORRECT OUTPUT!");
            // Give points based on time and amount of correct objects
            // Replace all matrix icoObjects and player continues game?
            // Create new points system?
            ScoreManager.AddScore(icoListObjects.Count * 50);
            
        }
        else
        {
            Debug.Log("WRONG OUTPUT!");
        }

        // Clear & reset panels
        for (int i = 0; i < outputObjects.Count; i++) {
        
            Destroy(outputObjects[i]);
        }
        outputObjects.Clear();
        FindAnyObjectByType<MatrixGameManager>().ResetMatrixIcoObjects();
        row = 0;
    }
}
