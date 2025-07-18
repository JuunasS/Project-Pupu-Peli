using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

// This is used to get how many big the generated matrix is
// StageOne = 3x3, StageTwo = 4x4, etc...
public enum MatrixGameProgression
{
    StageOne = 9,
    StageTwo = 16,
    StageThree = 25,
    StageFour = 36,
}

public class MatrixGameManager : MonoBehaviour
{
    public GameObject computerCanvas;
    public RectTransform matrixPanelRectTransform;
    public GameObject matrixContentPanel;

    // Starting point for populating content panel
    public Transform firstIcoObjectPosition;

    public GameObject icoObjectPrefab;
    public float icoObjPaddingY = 150f;
    public float icoObjPaddingX = 50f;

    // This list keeps track of already created objects
    public List<GameObject> icoGameObjects;

    public IcoObjMasterList icoMasterList;
    public List<IcoScriptObject> icoScriptObjects;

    public MatrixGameProgression matrixProgression;

    public MatrixScoreManager scoreManager;
    public GameObject gameEndScreen;
    public TMP_Text gameEndText;
    public TMP_Text gameEndScoreText;

    public CoinSpawner coinSpawner;


    public static event Action MatrixGameEnd;


    private void OnEnable()
    {
        icoScriptObjects = icoMasterList.getIcoScriptObjects();
        ResetMatrixIcoObjects();
        gameEndScreen.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("MatrixPanelManager Start!!");
        //icoScriptObjects = icoMasterList.getIcoScriptObjects();

        // Add function for populating content section
        Debug.Log(firstIcoObjectPosition);
        Debug.Log(icoScriptObjects.Count - 1);
        //PopulateMatrixContent(icoScriptObjects);
    }


    public void PopulateMatrixContent(List<IcoScriptObject> sIcoObjectList)
    {
        // TODO clear existing objects if there are any!!

        // Populate content panel based on given list

        float firstPositionX = firstIcoObjectPosition.localPosition.x;
        float firstPositionY = firstIcoObjectPosition.localPosition.y;

        float icoObjWidth = icoObjectPrefab.GetComponent<RectTransform>().rect.width;
        float icoObjHeight = icoObjectPrefab.GetComponent<RectTransform>().rect.height;


        //Debug.Log("matrixProgression: " + matrixProgression);

        int listCount = (int)matrixProgression; //sIcoObjectList.Count - 1

        float icoObjListSqrt = math.round(math.sqrt(listCount));

        //Debug.Log("sqrt: " + icoObjListSqrt);

        int row = 0, col = 0;


        for (int i = 0; i < listCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, sIcoObjectList.Count);

            Debug.Log("randomIndex: " + randomIndex);

            GameObject newIcoObj = Instantiate(icoObjectPrefab);

            newIcoObj.transform.SetParent(matrixContentPanel.transform, false);

            icoGameObjects.Add(newIcoObj);

            newIcoObj.GetComponent<IcoListObject>().SetIcoData(sIcoObjectList[randomIndex]); //i
            newIcoObj.GetComponent<IcoListObject>().currentParentApp = this.gameObject;

            // Set ico object position 
            // 1st is up-left and then 2 to the right and then move down a row

            /*
            Debug.Log("firstPositionX: " + firstPositionX);
            Debug.Log("firstPositionX * row: " + firstPositionX + firstPositionX * row);
            Debug.Log("firstPositionY: " + firstPositionY);
            Debug.Log("firstPositionY * row: " + firstPositionY + firstPositionY * -col);
            */

            newIcoObj.transform.localPosition = firstIcoObjectPosition.transform.localPosition;
            newIcoObj.transform.localPosition += new Vector3(icoObjWidth * row, icoObjHeight * col, 0);

            newIcoObj.GetComponent<IcoListObject>().StartSwapLoop();

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

        // Set Matrix panel width and height based on the generatex ico objects!

        Debug.Log("matrixPanelRectTransform.rect.size!!");
        matrixPanelRectTransform.sizeDelta = new Vector2(icoObjListSqrt * icoObjWidth + icoObjPaddingX, icoObjListSqrt * icoObjHeight + icoObjPaddingY);

    }

    public void ResetMatrixIcoObjects()
    {
        for (int i = 0; i < icoGameObjects.Count; i++)
        {
            Destroy(icoGameObjects[i]);
        }
        icoGameObjects.Clear();

        PopulateMatrixContent(icoScriptObjects);

    }


    public async void GameEndOnTime()
    {
        gameEndScreen.SetActive(true);
        gameEndText.text = "Timer ran out!";
        gameEndScoreText.text = "Score: " + scoreManager.GetScore().ToString();

        MatrixGameEnd?.Invoke();

        await Task.Delay(3000);
        computerCanvas.SetActive(false);
        DispenseReward();
    }

    public async void GameEndOnExit()
    {
        gameEndScreen.SetActive(true);
        gameEndText.text = "Exiting application!";
        gameEndScoreText.text = "Score: " + scoreManager.GetScore().ToString();

        MatrixGameEnd?.Invoke();

        await Task.Delay(3000);
        DispenseReward();
    }

    public void DispenseReward()
    {

        // Spawn coins for player based on score!
        coinSpawner.BeginSpawningCoins(scoreManager.currentScore);
        scoreManager.currentScore = 0;

        // Coins should make satisfying sounds when hitting ground and when being collected
    }

}
