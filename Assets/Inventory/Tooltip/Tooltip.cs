using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    #region Attributs
    private Item item;
    private string data;
    private GameObject tooltip;
    private Slot slot;
    #endregion

    private void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }
    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }
    public void Activate(Item item)
    {
        this.item = item;
        setInformations();
        //constructDataString();
        tooltip.SetActive(true);
    }
    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    /*public void constructDataString()
    {
        data =  "<color=#ffffff>" + item.name + item.level + "</color>\n" + item.description + "\n" +"Quantité :" + item.quantity + "Rareté :" +item.rarity + "";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }*/
    public void setInformations()
    {
        Text txt = null;

        //Change the Name
        txt = tooltip.transform.Find("ItemName").GetComponent<Text>();
        tooltip.transform.parent = Global.canvas.transform;
        if (txt != null) txt.text = item.name;

        //Change the Type
        txt = tooltip.transform.Find("ItemType").GetComponent<Text>();
        tooltip.transform.parent = Global.canvas.transform;
        if (txt != null)
        {
            switch (item.itemType)
            {
                case ItemTypes.None:
                    txt.text = "Diver";
                    break;
                case ItemTypes.Weapon:
                    txt.text = "Arme";
                    break;
                case ItemTypes.Consumable:
                    txt.text = "Consommable";
                    break;
                case ItemTypes.Armor:
                    txt.text = "Armure";
                    break;
                case ItemTypes.Ring:
                    txt.text = "Anneau";
                    break;
                case ItemTypes.Pendant:
                    txt.text = "Amulette";
                    break;
            }
        }

        //change the level 
        txt = tooltip.transform.Find("ItemLevel").GetComponent<Text>();
        tooltip.transform.parent = Global.canvas.transform;
        if (txt != null) txt.text ="Niv." + item.level.ToString();

        //change the description 
        txt = tooltip.transform.Find("ItemDescription").GetComponent<Text>();
        tooltip.transform.parent = Global.canvas.transform;
        if (txt != null) txt.text = item.description;
    }


}
