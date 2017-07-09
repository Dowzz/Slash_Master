using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public bool dragEnable = false;
    public Slot endSlot;
    public GameObject draggableItem;

    private Slot startSlot;
    private Image draggableImage;
    private Vector3 dragPosition;

    private void Awake()
    {
        Global.inventoryManager = this;
        draggableImage = draggableItem.GetComponent<Image>();
    }
    private void Update()
    {
        if (dragEnable) setdragItemPosition();
        
    }
    #region Drag & Drop
    public void StartDrag(Slot slot)
    {
        //startdrag du currentslot
        if (dragEnable) return;
        

        draggableImage.enabled = true;
        dragEnable = true;
        startSlot = slot;

        draggableImage.sprite = Resources.Load<Sprite>("PNG/Items/" + slot.item.image);
    }
    public void stopDrag()
    {
        //stop drag du current slot
        
        draggableImage.sprite = null;
        draggableImage.enabled = false;

        if (endSlot != null)  changeSlotItem();

        dragEnable = false;
    }

    void setdragItemPosition()
    {
        Vector3 offset = new Vector3(20, -20, 0);
        //Positionnement du draggable
        dragPosition = Input.mousePosition - Global.canvas.GetComponent<RectTransform>().localPosition;
        draggableItem.GetComponent<RectTransform>().localPosition = dragPosition + offset;

        
    }

    void changeSlotItem()
    {
        if (startSlot == endSlot) return;
        if (startSlot.item == null) return;

        //check item type
        ItemTypes itemType = startSlot.item.itemType;
        ItemTypes slotType = endSlot.availableItemType;

        if (slotType == ItemTypes.None || (slotType != ItemTypes.None && itemType == slotType))
        { 
            //change le slot
            endSlot.item = startSlot.item;
            endSlot.RefreshImage();
            //change start slot image
            startSlot.item = null;
            startSlot.RefreshImage();
            }
    }
    #endregion
}
