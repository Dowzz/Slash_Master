using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour{
    public String identity;
    public Item item;
    public int id;
    public ItemTypes availableItemType;


    private Image image;
    private Text quantityText;
    private Tooltip tooltip;
    

    private void Start()
    {
        tooltip = GameObject.Find("Canvas").GetComponent<Tooltip>();
        refreshQuantity();
    }
    void Awake()
    {
        identity = GetComponent<Slot>().identity;
        image = transform.GetChild(0).GetComponent<Image>();
        RefreshImage();
        quantityText = transform.Find("Quantity").GetComponent<Text>();
    }

    public void RefreshImage()
    {
        if (item == null)
        {
            image.sprite = Resources.Load<Sprite>("PNG/CharInventory/" + identity);
            return;
        }

        image.sprite = Resources.Load<Sprite>("PNG/Items/" + item.image);
    }

    public void MouseDown()
    {
        //clique pour deplacer un objet
        if (item == null) return;

        Global.inventoryManager.StartDrag(this);
    }

    public void MouseUp()
    {
        //pour lacher l'objet ou on veut
        Global.inventoryManager.stopDrag();
    }
    public void MouseEnter()
    {
        if (Global.inventoryManager.dragEnable)
            Global.inventoryManager.endSlot = this;

        if (item == null) return;
        tooltip.Activate(item);

    }
    public void MouseExit()
    {
        tooltip.Deactivate();
        if (Global.inventoryManager.dragEnable) Global.inventoryManager.endSlot = null;
        
    }
    public void refreshQuantity()
    {
        if (item == null || quantityText == null)
        {
            quantityText.text = string.Empty;
            return;
        }
        quantityText.text = (item.quantity <= 0) ? string.Empty : item.quantity.ToString();
    }
}
