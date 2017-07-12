using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    #region Attributs

    #endregion
    private void Awake()
    {
        Global.save = this;
        //PlayerPrefs.DeleteAll();
    }
    #region Save Item

    public Item GetItemBySlot(Slot slot)
    {
        string prefName = string.Empty;
        string QtyName = string.Empty;

        switch (slot.availableItemType)
        {
            case ItemTypes.None:
                prefName = string.Format("item_Slot{0}", slot.id);
                QtyName = string.Format("item_Slot{0}Qte", slot.id);
                break;
            case ItemTypes.Weapon:
                prefName = string.Format("Weaponitem_Slot{0}", slot.transform.name);
                QtyName = string.Format("Weaponitem_Slot{0}Qte", slot.transform.name);
                break;
            case ItemTypes.Consumable: break;
            case ItemTypes.Ring: break;
            case ItemTypes.Pendant: break;
            case ItemTypes.Armor: break;
        }

        if (string.IsNullOrEmpty(prefName) || string.IsNullOrEmpty(QtyName)) return null;

        int quantity = PlayerPrefs.GetInt(QtyName, 0);
        string ItemName = PlayerPrefs.GetString(prefName, null);
        

        if (string.IsNullOrEmpty(ItemName)) return null;

        Itemslist iList = (Itemslist)System.Enum.Parse(typeof(Itemslist), ItemName);

        Item item = Global.json.GetItemByName(iList);
        item.quantity = quantity;

        return item;
    }
    public void SaveItem(Slot slot, Item item, ItemTypes type)
    {
        string prefName = string.Empty;
        string QtyName = string.Empty;

        switch (type)
        {
            case ItemTypes.None:
                prefName = string.Format("item_Slot{0}", slot.id);
                QtyName = string.Format("item_Slot{0}Qte", slot.id);
                break;
            case ItemTypes.Weapon:
                prefName = string.Format("Weaponitem_Slot{0}", slot.transform.name);
                QtyName = string.Format("Weaponitem_Slot{0}Qte", slot.transform.name);
                break;
            case ItemTypes.Consumable: break;
            case ItemTypes.Ring: break;
            case ItemTypes.Pendant: break;
            case ItemTypes.Armor: break;
        }

        if (string.IsNullOrEmpty(prefName) || string.IsNullOrEmpty(QtyName)) return;
  
        //Save
        PlayerPrefs.SetString(prefName, item.jsonItem.ToString());
        PlayerPrefs.SetInt(QtyName, item.quantity);
    }

    public void DeleteItem(Slot slot, ItemTypes type)
    {
        string prefName = string.Empty;
        string QtyName = string.Empty;

        switch (type)
        {
            case ItemTypes.None: 
                prefName = string.Format("item_Slot{0}", slot.id);
                QtyName = string.Format("item_Slot{0}_Qte", slot.id);
                break;
            case ItemTypes.Weapon:
                prefName = string.Format("Weaponitem_Slot{0}", slot.transform.name);
                QtyName = string.Format("Weaponitem_Slot{0}_Qte", slot.transform.name);
                break;
            case ItemTypes.Consumable: break;
            case ItemTypes.Ring: break;
            case ItemTypes.Pendant: break;
            case ItemTypes.Armor: break;
        }
        PlayerPrefs.DeleteKey(prefName);
        PlayerPrefs.DeleteKey(QtyName);
    }

    #endregion
}
