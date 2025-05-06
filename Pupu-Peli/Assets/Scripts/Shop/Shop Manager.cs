using System;
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

    // For keeping track of which gameobject belongs to which shop item
    [SerializeField]
    private ShopItemID[] shopItemIDArray;
    public Dictionary<string, GameObject> shopItemObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        shopItemObjects.Clear();    
        for(int i = 0; i < shopItemIDArray.Length; i++)
        {
            shopItemObjects.Add(shopItemIDArray[i].ID, shopItemIDArray[i].obj);
        }
    }

    public void Start()
    {
        GenerateShopItems();
    }

    public void GenerateShopItems()
    {
        float shopObjWidth = shopItemPrefab.GetComponent<RectTransform>().rect.width;
        float shopObjHeight = shopItemPrefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0;  i < shopItemData.Count; i++)
        {
            GameObject newShopItem = Instantiate(shopItemPrefab);


            newShopItem.transform.SetParent(shopPanel.transform, false);

            // Set new shop item data
            newShopItem.GetComponent<ShopItem>().SetShopItemData(shopItemData[i], shopItemObjects[shopItemData[i].ID]);

            newShopItem.transform.localPosition = shopItemPositionZero.transform.localPosition;

            newShopItem.transform.localPosition += new Vector3(shopObjWidth * row + (100 * row), shopObjHeight * col + (100 * col), 0);
            
            if (i != 0 && row + 1 == 4) // End of the row
            {
                Debug.Log("Adding column!!");
                col--;
                row = 0;
            }
            else
            {
                Debug.Log("Adding row!!");
                row++;
            }
        }
    }
}


[Serializable]
public class ShopItemID
{
    public string ID;
    public GameObject obj;
}

