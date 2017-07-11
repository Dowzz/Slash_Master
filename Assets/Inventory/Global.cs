using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    #region Attributs
    public static SlotManager slotManager;
    public static InventoryManager inventoryManager;
    public RectTransform InventoryPanel;
    public static Jsonreader json; 
    public RectTransform EquipPanel;
    public static GameObject canvas;
    bool menuIsActive { get; set; }

    #endregion

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");    
    }

    void Start()
    {
        //debug Création d'item
        slotManager.CreatSlots();
        

        GameObject slot1 = GameObject.Find("Slot1");
        slot1.GetComponent<Slot>().changeItem(json.GetItemByName(Itemslist.ManaPotion));




        /*GameObject slot2 = GameObject.Find("Slot2");
        Slot Myslot2 = slot2.GetComponent<Slot>();
        Consumable consumable = new Consumable();
        consumable.name = "Potion";
        consumable.image = "drink";
        consumable.quantity = 5;
        consumable.itemType = ItemTypes.Consumable;
        Myslot2.item = consumable;
        Myslot2.RefreshImage();
        slot.refreshQuantity();
        InventoryPanel.gameObject.SetActive(false);*/

        InventoryPanel.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuIsActive = !menuIsActive;
            InventoryPanel.gameObject.SetActive(menuIsActive);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            
        }
    }
}
public enum Itemslist
{
    HealthPotion, ManaPotion
}

public enum ItemTypes
    // type d'item 
{
    None, Weapon, Consumable, Armor, Ring, Pendant
}
