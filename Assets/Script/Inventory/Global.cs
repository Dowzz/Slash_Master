using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    #region Attributs
    public static SlotManager slotManager;
    public static InventoryManager inventoryManager;
    public RectTransform InventoryPanel;
    public static Jsonreader json;
    public static SaveManager save;
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
        slotManager.LoadItems();

        GameObject slot1 = GameObject.Find("Slot1");
        slot1.GetComponent<Slot>().changeItem(json.GetItemByName(Itemslist.ManaPotion));
        
        //Hide Inventory
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
