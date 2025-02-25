using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour, IDropHandler
{
    public GameObject scrollView;
    public GameObject scrollViewContent;

    public List<Transform> inputPositions;

    public List<GameObject> inputObjects;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped into input!");

        if(DragManager.Instance.dragObject.GetComponent<IcoListObject>() != null )
        {
            // Does not contain [!]
            if (!inputObjects.Contains(DragManager.Instance.dragObject)) 
            {
                Debug.Log("Received input: " + DragManager.Instance.dragObject);
                inputObjects.Add(DragManager.Instance.dragObject);

                DragManager.Instance.dragObject.GetComponent<IcoListObject>().gameObject.transform.parent = inputPositions[inputObjects.Count - 1];
                DragManager.Instance.dragObject.GetComponent<IcoListObject>().newPosition = new Vector3(0, 0, 0);
            }
        }
    }

    public void CheckObjectList()
    {
        Debug.Log("Checking input objects!");

        for (int i = 0; i < inputObjects.Count; i++)
        {
            Debug.Log(inputPositions[i].childCount == 0);
            if (inputPositions[i].childCount == 0)
            {
                inputObjects.RemoveAt(i);
            }
        }
    }
}
