using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour {
    #region Attributs
    public bool dragEnable = false;
    public Slot endSlot;
    public GameObject draggableItem;

    private Slot startSlot;
    private Image draggableImage;
    private Vector3 dragPosition;
    public List<Slot> slotlist = new List<Slot>();
    public int maxItemsInSameSlot;

    #endregion

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

        draggableImage.sprite = Resources.Load<Sprite>("PNG/Items/" + slot.currentitem.image);
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
        if (startSlot.currentitem == null) return;

        //check item type
        ItemTypes itemType = startSlot.currentitem.itemType;
        ItemTypes slotType = endSlot.availableItemType;

        
        if (slotType == ItemTypes.None || (slotType != ItemTypes.None && itemType == slotType))
        {
            Item item = endSlot.currentitem;

            if (ChekItemInSlot(startSlot, endSlot.currentitem) && ChekItemInSlot(endSlot, startSlot.currentitem))
            {
                //change le slot
                endSlot.changeItem(startSlot.currentitem);
                //save item
                Global.save.SaveItem(endSlot, startSlot.currentitem, endSlot.availableItemType);

                //change start slot image
                startSlot.changeItem(item);
                if (item != null)
                    Global.save.SaveItem(startSlot, item, startSlot.availableItemType);
                else
                    Global.save.DeleteItem(startSlot, startSlot.availableItemType);
            }
        } 

           

        startSlot = null;
        endSlot = null;
    }
    bool ChekItemInSlot(Slot slot, Item item)
    {
        if (item == null) return true;
        if (slot.availableItemType == ItemTypes.None) return true;
        return (slot.availableItemType == item.itemType) ? true : false;
    }
    #endregion

    #region items
    public bool Additem (Item item)
        {
            if (item == null) return false;

        Slot currentSlot = slotlist.SingleOrDefault(
            p => p.currentitem != null
            && p.currentitem.name == item.name
            && p.currentitem.quantity + item.quantity <= maxItemsInSameSlot
            
            );

        //l'inventaire contient un item
        if (currentSlot != null)
        {
            //incremente la quantité
            if (currentSlot.currentitem.quantity < item.max)
                currentSlot.currentitem.quantity += item.quantity;
            else return false;
        }

        else
        {
            currentSlot = slotlist.Where(p => p.currentitem == null).First();
            
            if (currentSlot == null)
            {
                print("Votre Inventaire est plein");
                return false;
            }

            currentSlot.currentitem = item;
            currentSlot.RefreshImage();
        }
        item.quantity = currentSlot.currentitem.quantity;
        Global.save.SaveItem(currentSlot, item, currentSlot.availableItemType);
        

        currentSlot.refreshQuantity();

        return true;


    }
    #endregion
}
