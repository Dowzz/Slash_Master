using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour {
    public string itemName;
    public int quantity;
    public string image;
    public ItemTypes type;

    private void OnTriggerEnter(Collider obj)
    {
        BuildItem(); 
        


    }
    void BuildItem()
    {
        Item item01 = new Item();
        item01.name = itemName;
        item01.itemType = type;
        item01.quantity = quantity;
        item01.image = image;
     
        Global.inventoryManager.Additem(item01);
    }
}
