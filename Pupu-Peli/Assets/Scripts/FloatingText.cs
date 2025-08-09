using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public Transform mainCamera;
    public Transform obj;
    public Transform worldSpaceCanvas;

    public Vector3 offset;

    public TMP_Text textComponent;
    public string text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main.transform;
        obj = transform.parent;
        worldSpaceCanvas = GameObject.Find("WorldSpaceCanvas").transform;
        textComponent = this.GetComponent<TMP_Text>();

        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
        this.transform.position = obj.position + offset;
    }
}
