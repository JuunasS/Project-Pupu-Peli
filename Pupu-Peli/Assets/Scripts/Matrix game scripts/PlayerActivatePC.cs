using UnityEngine;

public class PlayerActivatePC : MonoBehaviour
{

    public GameObject MatrixCanvasObject;

    private void Update()
    {
        if (MatrixCanvasObject & Input.GetKeyDown(KeyCode.Escape))
        {
            MatrixCanvasObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            MatrixCanvasObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Show interact popup

        // Add if statement for when player presses interact button to open canvas and disable collider (activate when exiting canvas)
    }
}
