using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    Vector3 mouseDragStartPos;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - mouseDragStartPos;

        // This sets the parent object as last sibling so remember to use an empty object for containing panel and it's controller
        transform.parent.SetAsLastSibling();
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
