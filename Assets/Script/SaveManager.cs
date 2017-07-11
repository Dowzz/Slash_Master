using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    #region Attributs;

    #endregion
    void Awake()
    {
        Global.save = this;  
    }
    #region Save Items
    public Item GetItemBySlot(int slotId)
    {
        int quantity = PlayerPrefs.GetInt(string.Format("item_Slot(0)_Qte", slotId), 0);
        string itemName = PlayerPrefs.GetString(string.Format("Item_Slot(0)", slotId), null);

        //no item
        if (itemName == null) return null;


        Itemslist iList = (Itemslist)System.Enum.Parse(typeof(Itemslist), itemName);
        Item item = Global.json.GetItemByName(Itemslist.HealthPotion);

        return item;
    }
    public void SaveItem (int slotid, Item item)
    {
        //verifier si l'item existe deja
        /* Item Existitem = GetItemBySlot(slotid);

         if (Existitem != null) item.quantity += Existitem.quantity;*/

        PlayerPrefs.SetString(string.Format("item_Slot(0)", slotid), item.name);
        PlayerPrefs.SetInt(string.Format("item_Slot(0)_Qte", slotid), item.quantity);
    }
    #endregion
}
