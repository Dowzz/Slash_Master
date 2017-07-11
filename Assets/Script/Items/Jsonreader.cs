using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Jsonreader : MonoBehaviour {

    #region Attributs
    public Object jsonfile;
    #endregion

    private void Awake()
    {
        Global.json = this;
    }

    #region Methods
    public Item GetItemByName(Itemslist name)
    {
        string jsonString = jsonfile.ToString();

        JSONNode json = JSON.Parse(jsonString);
        JSONNode currentJsonItem = json[name.ToString()];

        if (currentJsonItem == null) return null;

        Item item = new Item()
        {
            name = currentJsonItem["name"].Value,
            description = currentJsonItem["description"].Value,
            level = int.Parse(currentJsonItem["level"].Value),
            image = currentJsonItem["image"].Value,
            quantity = 0,
            itemType = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), currentJsonItem["type"].Value)
    };

        
        
        return item;
    }
    #endregion
}
