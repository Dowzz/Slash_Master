using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;

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
        constructDataString();
        tooltip.SetActive(true);
    }
    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void constructDataString()
    {
        data =  "<color=#0473f0>" + item.name + "</color>\n\n" + item.quantity + "";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

}
