using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("General Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Modifiers")]
    public bool affectHP;
    public bool affectMP, affectStr, affectDef;
    public int amountToChange;

    [Header("Power Increases")]
    public int weaponPwr;
    public int armorPwr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
