using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject shopGameObject;

    public bool isBought;


    public void Buy()
    {
        shopGameObject.SetActive(true);

        isBought = true;
    }
}
