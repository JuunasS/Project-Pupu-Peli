using System.Collections.Generic;
using UnityEngine;

public class ShopItemMasterList : MonoBehaviour
{

    public Dictionary<ShopItem, bool> shopItemAvailableStatus;
    public List<ShopItemScriptObject> shopItemData;


    // Data that should be found from file:
    // Name of object
    // Object description
    // Filepath to image?
    // boolean value for item availability (isBought?)

    public void SetNewShopItemData(List<ShopItemScriptObject> newData)
    {

    }

    // TODO: functions for writing and retrieving shop item data to/from files
    public void GetShopItemDataFromFile()
    {

    }

    public void WriteShopItemDataToFile()
    {

    }
}

