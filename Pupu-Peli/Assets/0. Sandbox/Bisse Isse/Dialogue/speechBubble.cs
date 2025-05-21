using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class speechBubble : MonoBehaviour
{
    SpriteRenderer sr;
    TextMeshPro text;

    // Worldspacecanvas addition
    public Transform mainCamera;
    public Transform obj;
    public Transform worldSpaceCanvas;
    public Vector3 offset;


    public string str;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();

        //Setup("Hello world!!!!!!");

        // Worldspacecanvas addition
        mainCamera = Camera.main.transform;
        obj = transform.parent;
        transform.SetParent(worldSpaceCanvas);
    }

    private void Update()
    {
        Setup(str);

        transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.position.y, transform.position.z) - new Vector3(0, mainCamera.transform.position.y, mainCamera.transform.position.z));
        transform.position = obj.position + offset;
    }

    void Setup(string _text)
    {
        // Set the text and update mesh just in case
        text.SetText(_text);
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false); //Check how big the text box is

        Vector2 padding = new Vector2(2f, 2f);
        sr.size = textSize + padding; //Set the speech bubble size plus some padding
    }
}