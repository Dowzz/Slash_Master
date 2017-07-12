using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour{
    #region Attributs
    public String identity;
    public Item currentitem;
    public int id;
    public ItemTypes availableItemType;


    private Image image;
    private Text quantityText;
    private Tooltip tooltip;

    #endregion

    private void Start()
    {
        tooltip = GameObject.Find("Canvas").GetComponent<Tooltip>();
        refreshQuantity();
    }
    void Awake()
    {
        identity = GetComponent<Slot>().identity;
        image = this.transform.Find("ImageItem").GetComponent<Image>();
        RefreshImage();
        quantityText = transform.Find("Quantity").GetComponent<Text>();
    }

    #region Trigger
    public void MouseDown()
    {
        //clique pour deplacer un objet
        if (currentitem == null) return;

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

        if (currentitem == null) return;
        tooltip.Activate(currentitem);

    }
    public void MouseExit()
    {
        tooltip.Deactivate();
        if (Global.inventoryManager.dragEnable) Global.inventoryManager.endSlot = null;
        
    }

    #endregion

    #region refresh

    public void changeItem(Item item)
    {
        currentitem = item;
        RefreshImage();
        refreshQuantity();
    }

    public void refreshQuantity()
    {
        if (currentitem == null || quantityText == null)
        {
            quantityText.text = string.Empty;
            return;
        }
        quantityText.text = (currentitem.quantity <= 0) ? string.Empty : currentitem.quantity.ToString();
    }
    public void RefreshImage()
    {
        if (currentitem == null)
        {
            image.sprite = Resources.Load<Sprite>("PNG/CharInventory/" + identity);
            return;
        }

        image.sprite = Resources.Load<Sprite>("PNG/Items/" + currentitem.image);
    }
    #endregion
}
