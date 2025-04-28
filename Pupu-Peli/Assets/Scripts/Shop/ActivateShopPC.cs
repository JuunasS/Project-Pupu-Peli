using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ActivateShopPC : MonoBehaviour
{

    public GameObject ShopCanvasObject;

    private async void Update()
    {
        if (ShopCanvasObject & Input.GetKeyDown(KeyCode.Escape))
        {
            ShopCanvasObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ShopCanvasObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Show interact popup

        // Add if statement for when player presses interact button to open canvas and disable collider (activate when exiting canvas)
    }
}
