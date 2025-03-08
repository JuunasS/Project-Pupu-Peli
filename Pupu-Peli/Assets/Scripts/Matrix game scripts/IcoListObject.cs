using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IcoListObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    // Drag and drop variables
    public CanvasGroup canvasGroup;
    private Transform originalParent;

    public Vector3 mouseDragStartPos;
    public bool dragging = false;

    public Vector3 returnPos;
    public float returnDuration = 0.5f;

    public Vector3 newPosition;
    public bool setToNewPosition = false;
    
    // If is active this icoObject will not be swapped (Swap timer will be reset/paused)
    public bool isActive;

    // Double click variables
    private float firstLeftClickTime;
    private float timeBetweenClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;


    // Ico Object Data
    public ScriptableObject icoData;

    // UI variables
    public TMP_Text icoText;
    public Image icoImg;

    public float padding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent;
        returnPos = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag window");
        transform.position = Input.mousePosition - mouseDragStartPos;
        transform.SetAsLastSibling();
        dragging = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        dragging = false;
        canvasGroup.blocksRaycasts = true;

        // Check if is hovering over input panel and set it there if so
        if (setToNewPosition)
        {
            Debug.Log("Drag back to new position");
            setToNewPosition = false;
            //StartCoroutine(LerpToLocalVector3(newPosition));
        }
        else
        {
            // Return to matrix
            Debug.Log("Drag back to original position");
            this.transform.parent = originalParent;
            StartCoroutine(LerpToLocalVector3(returnPos));
            DragManager.Instance.SetDragObject(null);
            FindAnyObjectByType<OutputPanel>().CheckObjectList();
        }
    }

    public void MoveToPos(Vector3 pos)
    {
        Debug.Log("MoveToPos: " + pos);
        StartCoroutine(LerpToLocalVector3(pos));
    }

    public IEnumerator LerpToLocalVector3(Vector3 pos)
    {
        Debug.Log("Lerping to new position");
        float timeElapsed = 0;

        while (timeElapsed < returnDuration)
        {
            //Debug.Log("Lerp 1: " + timeElapsed);
            float t = timeElapsed / returnDuration;
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, t);
            timeElapsed += Time.deltaTime;

            //Debug.Log("Lerp 2, dragging: " + dragging);
            if (dragging) { break; }

            yield return null;  
        }

        Debug.Log("Lerp 3");
        if (!dragging) { transform.localPosition = pos; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0)) { 
        
            leftClickNum++;
        }
        if (leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleClick());
        }

        mouseDragStartPos = Input.mousePosition - transform.position;
        DragManager.Instance.SetDragObject(this.gameObject);
    }

    public IEnumerator DetectDoubleClick()
    {
        isTimeCheckAllowed = false;

        while (Time.time < firstLeftClickTime + timeBetweenClick)
        {
            if (leftClickNum == 2)
            {
                Debug.Log("Double Click on ico: " + this.gameObject.name);
                FindAnyObjectByType<GeneralInfo>().ActivateInfoPanel(icoData.name);
                break;
            }
            yield return null;
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }

    public void SetIcoData(IcoScriptObject newIcoData)
    {
        this.icoData = newIcoData;
        this.icoText.text = newIcoData.icoName;
        this.icoImg.sprite = newIcoData.icoImg;
    }

    public IEnumerator SwapIcoObj()
    {

        if(this.isActive) // TODO: Add timer check!!!
        {
            yield return null;
        }

        // Add randomized timer that is stopped when object is clicked
        // No need to generate new prefab, instead replace icodata and put new data into object 
        // GeneralInfo panel will pull data everytime double click event happens

        this.icoData = IcoObjMasterList.Instance.GetRandomIcoScriptObj();
    }

}
