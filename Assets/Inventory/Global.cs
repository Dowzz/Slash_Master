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
    public static SaveManager save;
    public static int DEFAULT_MAX_ITEM = 99;

    #endregion

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");    
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();

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
    HealthPotion, ManaPotion, BasicSword
}

public enum ItemTypes
    // type d'item 
{
    None, Weapon, Consumable, Armor, Ring, Pendant
}
