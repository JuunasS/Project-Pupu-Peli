using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class IcoListObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    Vector3 mouseDragStartPos;

    Vector3 returnPos;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag window");
        transform.position = Input.mousePosition - mouseDragStartPos;
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        transform.localPosition = returnPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDragStartPos = Input.mousePosition - transform.position;
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        returnPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
