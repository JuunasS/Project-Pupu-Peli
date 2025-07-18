using UnityEngine;
using UnityEngine.UI;

public class ChangeMapImage : MonoBehaviour
{
    public Image mapImage;
    public Sprite defaultImage;

    private void Awake()
    {
        mapImage.sprite = defaultImage;
    }

    public void SetMapImage(Sprite newMapImage)
    {
        mapImage.sprite = newMapImage;
    }
}
