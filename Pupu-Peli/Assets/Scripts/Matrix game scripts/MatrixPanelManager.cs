using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MatrixPanelManager : MonoBehaviour
{
    public Transform matrixPanelTransform;
    public GameObject matrixContentPanel;

    // Starting point for populating content panel
    public Transform firstIcoObjectPosition;

    public GameObject icoObjectPrefab;

    // This list keeps track of already created objects
    public List<GameObject> icoGameObjects;

    public List<ScriptableObject> icoScriptObjects;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add function for populating content section
        Debug.Log(firstIcoObjectPosition);
        Debug.Log(icoScriptObjects.Count - 1);
        PopulateMatrixContent(icoScriptObjects);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopulateMatrixContent(List<ScriptableObject> icoObjectList)
    {
        // Populate content panel based on given list

        float firstPositionX = firstIcoObjectPosition.localPosition.x;
        float firstPositionY = firstIcoObjectPosition.localPosition.y;

        float icoObjWidth = icoObjectPrefab.GetComponent<RectTransform>().rect.width;
        float icoObjHeight = icoObjectPrefab.GetComponent<RectTransform>().rect.height;

        int listCount = icoObjectList.Count - 1;

        float icoObjListSqrt = math.round(math.sqrt(listCount));

        Debug.Log("sqrt: " + icoObjListSqrt);

        int row = 0, col = 0;

        for (int i = 0; i < listCount; i++)
        {
            GameObject newIcoObj = Instantiate(icoObjectPrefab);

            newIcoObj.transform.SetParent(matrixContentPanel.transform, false);

            icoGameObjects.Add(newIcoObj);

            newIcoObj.GetComponent<IcoListObject>().icoData = icoObjectList[i];
            // Set ico object position 
            // 1st is up-left and then 2 to the right and then move down a row


            Debug.Log("firstPositionX: " + firstPositionX);

            Debug.Log("firstPositionX * row: " + firstPositionX + firstPositionX * row);

            Debug.Log("firstPositionY: " + firstPositionY);

            Debug.Log("firstPositionY * row: " + firstPositionY + firstPositionY * -col);

            newIcoObj.transform.localPosition = firstIcoObjectPosition.transform.localPosition;

            newIcoObj.transform.localPosition += new Vector3(icoObjWidth * row, icoObjHeight * col, 0);

            Debug.Log(i / icoObjListSqrt);

            if (i != 0 && row + 1 == icoObjListSqrt) // End of the row
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

        }
    }
}
