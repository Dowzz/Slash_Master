using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    public Weapon()
    {
        this.itemType = ItemTypes.Weapon;
        this.level = 5;
        this.description = "Une Arme De bonne qualité";
        this.rarity = "Ordinaire";
    }
}
