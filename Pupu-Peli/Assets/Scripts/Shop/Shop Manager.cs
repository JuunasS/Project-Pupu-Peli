using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public List<ShopItemScriptObject> shopItemData;

    public GameObject shopPanel;
    public Transform shopItemPositionZero;

    public int row, col, maxRow;


    public GameObject shopItemPrefab;
    public List<GameObject> shopItems;

    public void GenerateShopItems()
    {
        float shopObjWidth = shopItemPrefab.GetComponent<RectTransform>().rect.width;
        float shopObjHeight = shopItemPrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0;  i < shopItemData.Count; i++)
        {
            GameObject newShopItem = Instantiate(shopItemPrefab);


            newShopItem.transform.SetParent(shopPanel.transform, false);

            shopItems.Add(newShopItem);

            // Set new shop item data
            newShopItem.GetComponent<ShopItem>().SetShopItemData(shopItemData[i]);

            newShopItem.transform.localPosition = shopItemPositionZero.transform.localPosition;
            newShopItem.transform.localPosition += new Vector3(shopObjWidth * row, shopObjHeight * col, 0);


            /*
            if (i != 0 && row + 1 == icoObjListSqrt) // End of the row
            {
                Debug.Log("Adding column!!");
                col--;
                row = 0;
            }
            else
            {
                Debug.Log("Adding row!!");
                row++;
            }*/
        }
    }
}
