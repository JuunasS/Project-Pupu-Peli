using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class IcoListObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    public Vector3 mouseDragStartPos;
    public bool dragging = false;

    public Vector3 returnPos;
    public float returnDuration = 0.5f;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag window");
        transform.position = Input.mousePosition - mouseDragStartPos;
        transform.SetAsLastSibling();
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        //transform.localPosition = returnPos;
        dragging = false;
        StartCoroutine(ReturnToStart());
    }

    public IEnumerator ReturnToStart()
    {
        float timeElapsed = 0;

        while (timeElapsed < returnDuration)
        {
            float t = timeElapsed / returnDuration;
            transform.localPosition = Vector3.Lerp(transform.localPosition, returnPos, t);
            timeElapsed += Time.deltaTime;
            if (dragging) { break; }

            yield return null;  
        }

        if (!dragging) { transform.localPosition = returnPos; }
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
