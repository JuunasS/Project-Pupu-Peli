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
        //worldSpaceCanvas = GameObject.FindWithTag("WorldSpaceCanvas").transform;

        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.position.y, transform.position.z) - new Vector3(0, mainCamera.transform.position.y, mainCamera.transform.position.z));
        transform.position = obj.position + offset;
    }
}
