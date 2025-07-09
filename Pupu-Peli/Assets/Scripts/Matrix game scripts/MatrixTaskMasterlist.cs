using System.Collections.Generic;
using UnityEngine;

public class MatrixTaskMasterlist : MonoBehaviour
{
    public static MatrixTaskMasterlist Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<MatrixTaskScriptObject> matrixTaskScriptableObjects;

    public List<MatrixTaskScriptObject> GetTaskScriptObjects()
    {
        // Add randomization based on wanted list-size paramenter and objectives?

        return matrixTaskScriptableObjects;
    }

    public MatrixTaskScriptObject GetRandomMatrixTaskScriptObj()
    {
        int randomIndex = Random.Range(0, matrixTaskScriptableObjects.Count);
        MatrixTaskScriptObject randomObj = matrixTaskScriptableObjects[randomIndex];

        return randomObj;
    }
}
