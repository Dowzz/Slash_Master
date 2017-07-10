using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {
    public static SlotManager slotManager;
    public static InventoryManager inventoryManager;
    public RectTransform InventoryPanel;
    public RectTransform EquipPanel;
    public static GameObject canvas;
    bool menuIsActive { get; set; }

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        
    }

    void Start()
    {
        slotManager.CreatSlots();

        GameObject slot1 = GameObject.Find("Slot1");
        Slot slot = slot1.GetComponent<Slot>();
        Weapon weapon = new Weapon();
        weapon.name = "bread";
        weapon.image = "bread";
        slot.item = weapon;
        slot.RefreshImage();
        slot.refreshQuantity();

        GameObject slot2 = GameObject.Find("Slot2");
        Slot Myslot2 = slot2.GetComponent<Slot>();
        Consumable consumable = new Consumable();
        consumable.name = "Potion";
        consumable.image = "drink";
        consumable.quantity = 5;
        Myslot2.item = consumable;
        Myslot2.RefreshImage();
        slot.refreshQuantity();
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

public enum ItemTypes
{
    None, Weapon, Consumable, Armor, Ring, Pendant
}
