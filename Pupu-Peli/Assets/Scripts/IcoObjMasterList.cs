using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IcoObjMasterList : MonoBehaviour
{

    public static IcoObjMasterList Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public List<IcoScriptObject> icoScriptableObjects;

    public List<IcoScriptObject> getIcoScriptObjects()
    {
        // Add randomization based on wanted list-size paramenter and objectives?

        return icoScriptableObjects;
    }

    public ScriptableObject GetRandomIcoScriptObj()
    {
        int randomIndex = UnityEngine.Random.Range(0, icoScriptableObjects.Count);
        ScriptableObject randomObj = icoScriptableObjects[randomIndex];

        return randomObj;
    }
}
