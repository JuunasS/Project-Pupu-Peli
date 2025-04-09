using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputPanel : MonoBehaviour, IDropHandler
{
    public TaskManager TaskManager;
    public MatrixScoreManager ScoreManager;

    public GameObject scrollViewContent;

    public Transform firstOutputPosition;

    public List<GameObject> outputObjects;

    public int rowMax;

    public int row = 0, col = 0;

    public float setToPositionSpeed;
    public float rePositionSpeed;

    public float scoreDelay;

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
        if (!outputObjects.Contains(obj)) { return; }
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
    public void SubmitOutput()
    {
        StartCoroutine(CheckOutput());
    }

    public IEnumerator CheckOutput()
    {
        List<IcoListObject> icoListObjects = new List<IcoListObject>();

        for (int i = 0; i < outputObjects.Count; i++)
        {
            icoListObjects.Add(outputObjects[i].GetComponent<IcoListObject>());
        }

        int tempScore = 0;
        for (int i = 0; i < icoListObjects.Count; i++)
        {
            if (TaskManager.CompareSumbittedValue(icoListObjects[i]))
            {
                // Scoring animation (Balatro-like scoring?)
                // Shake and show score addition -> "+50"
                icoListObjects[i].StartShakeAnim();
                tempScore += 50;
                yield return new WaitForSeconds(scoreDelay);
            }
            else
            {
                icoListObjects[i].StartShakeAnim();
                Debug.Log("WRONG OUTPUT!");
                // Shake and show that output was incorrect and set score to 0
                tempScore = 0;
                yield return new WaitForSeconds(scoreDelay);
                break;
            }
        }
        ScoreManager.AddScore(tempScore);


        // Clear & reset panels
        for (int i = 0; i < outputObjects.Count; i++)
        {
            Destroy(outputObjects[i]);
        }
        outputObjects.Clear();
        FindAnyObjectByType<MatrixGameManager>().ResetMatrixIcoObjects();
        row = 0;
    }


    [ContextMenu("TestShakeAnim")]
    public void TestShakeAnim()
    {

        List<IcoListObject> icoListObjects = new List<IcoListObject>();

        for (int i = 0; i < outputObjects.Count; i++)
        {
            icoListObjects.Add(outputObjects[i].GetComponent<IcoListObject>());
        }

        for (int i = 0; i < icoListObjects.Count; i++)
        {
            icoListObjects[i].StartShakeAnim();

        }

    }
}

