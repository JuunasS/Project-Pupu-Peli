using UnityEngine;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject dragObject;

    public void SetDragObject(GameObject obj)
    {
        dragObject = obj;
    }
}
