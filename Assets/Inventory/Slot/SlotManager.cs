using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour {

    public int colsCount = 0;
    public int rowsCount = 0;
    public GameObject slotPrefab;
    public Vector3 defaultslotPosition;
    public int slotOffset = 0;

    private Vector3 slotPosition;
    

    private void Awake()
    {
        Global.slotManager = this;
        slotPosition = defaultslotPosition;
    }

    public void CreatSlots()
    {
        if (slotPrefab == null) Debug.LogError("slotPrefab ne peut pas être nul.");

        int slotCount = 1;

        for (int i = 0; i < colsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                //création slot
                GameObject currentSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                currentSlot.transform.parent = transform;
                currentSlot.name = "Slot" + slotCount;
                Slot slot = currentSlot.GetComponent<Slot>();
                slot.id = slotCount;
                Global.inventoryManager.slotlist.Add(slot);

                
                //positionnement du premier slot
                RectTransform rectTransform = currentSlot.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector3(slotPosition.x, slotPosition.y);

                // décalage pour la création de la grille
                slotPosition.x += slotOffset;

                //ajouter des lignes

                if (slotCount > 1 && slotCount % rowsCount == 0)
                {
                    slotPosition.y -= slotOffset;
                    slotPosition.x = defaultslotPosition.x;
                }

                //numero de slot
                slotCount++;

            }
        }
    }

}
