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
    public void StopDrag()
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

            //change le slot
            endSlot.changeItem(startSlot.currentitem);

            //change start slot image
            startSlot.changeItem(item);
            }
    }
    #endregion

    #region items
    public void Additem (Item item)
        {
            if (item == null) return;

        Slot currentSlot = slotlist.SingleOrDefault(
            p => p.currentitem != null
            && p.currentitem.name == item.name
            && p.currentitem.quantity + item.quantity <= maxItemsInSameSlot
            
            );

        //l'inventaire contient un item
        if (currentSlot != null)
        {
            //incremente la quantité
            currentSlot.currentitem.quantity += item.quantity;
        }

        else
        {
            currentSlot = slotlist.Where(p => p.currentitem == null).First();
            
            if (currentSlot == null)
            {
                print("Votre Inventaire est plein");
            }

            currentSlot.currentitem = item;
            currentSlot.RefreshImage();
        }

        //save item in playerpref.
        item.quantity = currentSlot.currentitem.quantity;
        Global.save.SaveItem(currentSlot.id, item);
        

        currentSlot.refreshQuantity();


    }
    #endregion
}
