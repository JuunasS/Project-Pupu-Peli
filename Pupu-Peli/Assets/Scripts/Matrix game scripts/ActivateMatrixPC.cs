using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ActivateMatrixPC : MonoBehaviour
{

    public GameObject MatrixCanvasObject;
    public int waitBeforeExit;

    private async void Update()
    {
        if (MatrixCanvasObject & Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO:  Check if player has completed the taks or timer has run out!!!
            // Show given score and exit after pressing esc again?
            // Or exit after a few seconds
            FindAnyObjectByType<MatrixGameManager>().GameEndOnExit();

            await Task.Delay(waitBeforeExit * 1000); // milliseconds -> seconds

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
