using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour {
    #region Attributs
    public Itemslist wantedItem;
    public int quantity;
    #endregion
    private void OnTriggerEnter(Collider obj)
    {
        GetItem(); 
    }
    void GetItem()
    {
        Item item = Global.json.GetItemByName(wantedItem);
        item.quantity = quantity;
        Global.inventoryManager.Additem(item);
    }
}
