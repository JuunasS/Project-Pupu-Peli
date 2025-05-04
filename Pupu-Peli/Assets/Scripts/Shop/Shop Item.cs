using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public ShopManager shopManager;
    public GameObject shopGameObject;

    // Shop Item UI Variables

    public Image itemImage;
    public TMP_Text itemName;
    public TMP_Text itemDesc;

    // Other
    public bool isBought;
    public Button buyButton;
    public TMP_Text boughtText;

    public void SetShopItemData(ShopItemScriptObject item)
    {
        itemImage.sprite = item.image;
        itemName.text = item.name;
        itemDesc.text = item.description;
    }

    public void Buy()
    {
        // check if player has enough coins
        // Remove coins from player if possible and continue
        shopGameObject.SetActive(true);

        // Call function in shop manager to mark item as bought when generated again?
        buyButton.enabled = false;
        boughtText.enabled = true;

        isBought = true;
    }
}
