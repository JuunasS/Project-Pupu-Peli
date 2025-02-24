using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{

    Vector3 mouseDragStartPos;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - mouseDragStartPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDragStartPos = Input.mousePosition - transform.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function for reseting position of windows
}
