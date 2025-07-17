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
    public int price;
    public TMP_Text priceText;
    public Button buyButton;
    public TMP_Text boughtText;

    public void SetShopItemData(ShopItemScriptObject item, GameObject gameObject)
    {
        itemImage.sprite = item.image;
        //itemName.text = item.name;
        itemDesc.text = item.description;
        priceText.text = "[ " + item.price.ToString() + "€ ]";
        if (item.price <= 0)
        {
            priceText.text = "[ Free ]";
        }
        this.price = item.price;
        shopGameObject = gameObject;
    }

    public void Buy()
    {
        // check if player has enough coins
        CoinPurse playerCoins = FindFirstObjectByType<CoinPurse>();

        if (playerCoins != null)
        {
            if (playerCoins.currentMoney >= price)
            {
                playerCoins.currentMoney -= price;

                // Remove coins from player if possible and continue
                shopGameObject.SetActive(true);

                // Call function in shop manager to mark item as bought when generated again?
                priceText.gameObject.SetActive(false);
                buyButton.gameObject.SetActive(false);
                boughtText.gameObject.SetActive(true);

                isBought = true;
            } else
            {
                Debug.LogError("Not enough coins!");
            }
        }
    }
}
