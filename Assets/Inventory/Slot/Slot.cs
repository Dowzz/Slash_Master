using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public Item item;
    private Image image;
    private Tooltip tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("Canvas").GetComponent<Tooltip>();
    }
    void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();   
        Weapon weapon = new Weapon();
        weapon.name = "bread";
        weapon.image = "bread";
        this.item = weapon;
        LoadImage();

    }

    void LoadImage()
    {
        if (item == null || image== null) return;

        image.sprite = Resources.Load<Sprite>("PNG/" + item.image);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
