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

    // Double click variables
    private float firstLeftClickTime;
    private float timeBetweenClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;

    // UI variables
    public TMP_Text icoText;
    public Image icoImg;
    public float padding;

    // Ico Object Data
    public IcoScriptObject icoData;
    public float swapTimeMax;
    public float swapTimeMin;

    // If is active this icoObject will not be swapped (Swap timer will be reset/paused)
    public bool isActive;

    // IcoObject values
    // If value is left null it wont be displayed in general info
    public int icoAge;
    public float icoWeight;
    public float icoHeight;
    public int icoProductivity; // Needed?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent;
        returnPos = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /* Update is called once per frame
    void Update()
    {
    }
    */

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag window");
        transform.position = Input.mousePosition - mouseDragStartPos;
        transform.SetAsLastSibling();
        dragging = true;
        isActive = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended");
        dragging = false;
        canvasGroup.blocksRaycasts = true;

        // Check if is hovering over output panel and set it there if so
        if (setToNewPosition)
        {
            Debug.Log("Drag to new position");
            setToNewPosition = false;
            isActive = true;
        }
        else
        {
            // Return to matrix
            Debug.Log("Drag back to original position");
            this.transform.parent = originalParent;
            StartCoroutine(LerpToLocalVector3(returnPos, returnDuration));
            DragManager.Instance.SetDragObject(null);
            FindAnyObjectByType<OutputPanel>().RemoveObjectFromList(this.gameObject);

            if (FindAnyObjectByType<GeneralInfo>().activeInfoItem == null || FindAnyObjectByType<GeneralInfo>().activeInfoItem != this)
            {
                isActive = false;
            }
        }
    }

    public void MoveToPos(Vector3 pos, float duration)
    {
        Debug.Log("MoveToPos: " + pos);
        StartCoroutine(LerpToLocalVector3(pos, duration));
    }

    public IEnumerator LerpToLocalVector3(Vector3 pos, float duration)
    {
        Debug.Log("Lerping to new position");
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            //Debug.Log("Lerp 1: " + timeElapsed);
            float t = timeElapsed / duration;
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
        if (Input.GetMouseButtonDown(0))
        {

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
                FindAnyObjectByType<GeneralInfo>().ActivateInfoPanel(this);
                isActive = true;
                //TODO: Need to add activation for nearest icoobject to lock them in later stages?

                break;
            }
            yield return null;
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }

    // Generates random ico values for this object based on given minimum and maximum values
    public void SetIcoData(IcoScriptObject newIcoData)
    {
        this.icoData = newIcoData;
        this.icoText.text = newIcoData.icoName;
        this.icoImg.sprite = newIcoData.icoImg;

        // Generate icoObject values!

        icoAge = Random.Range(icoData.minAge, icoData.maxAge);
        icoWeight = Random.Range(icoData.minWeight, icoData.maxWeight);
        icoHeight = Random.Range(icoData.minHeight, icoData.maxHeight);
        icoProductivity = Random.Range(icoData.minProductivity, icoData.maxProductivity);
    }

    // Starts object data swapping coroutine
    public void StartSwapLoop()
    {
        StartCoroutine(SwapIcoObj());
    }

    // Swaps icoData of this object after randomized swap time and starts the coroutine again
    public IEnumerator SwapIcoObj()
    {
        //Debug.Log("SwapIcoObjs coroutine started :" + this.icoData.name);
        float swapTime = Random.Range(swapTimeMin, swapTimeMax);

        float timeElapsed = 0;


        while (timeElapsed < swapTime) 
        {

            if (isActive)
            {
                //TODO: Ajoitus jatkuu vai resetoituu?
                timeElapsed = 0;
            }
            else
            {
                float t = timeElapsed / returnDuration;
                timeElapsed += Time.deltaTime;
            }


            yield return null;

        }

        IcoScriptObject newIcoData = IcoObjMasterList.Instance.GetRandomIcoScriptObj();
        this.SetIcoData(newIcoData);
        StartCoroutine(SwapIcoObj());
    }

}
