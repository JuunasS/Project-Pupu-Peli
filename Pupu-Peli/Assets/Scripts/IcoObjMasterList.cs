using System.Collections.Generic;
using UnityEngine;

public class IcoObjMasterList : MonoBehaviour
{

    public List<ScriptableObject> icoScriptableObjects;

    public List<ScriptableObject> getIcoScriptObjects()
    {
        // Add randomization based on wanted list-size paramenter and objectives?

        return icoScriptableObjects;
    }
}
