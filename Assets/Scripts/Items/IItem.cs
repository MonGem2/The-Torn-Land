using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string ID { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Sprite UIIcon { get; set; }
    ItemData Data { get; set; }


    bool Use();
    IItem Clone();
}


public class ItemData
{
    public int ID;
    public List<State> Effects;
    public ItemType type;
    public string Sprite;
    public int spriteN=-1;
    public string Decription;

}

public enum ItemType
{
    Unused,
    Disposable,
    Helmet,
    Necklace,
    Armor,
    Backpack,
    Weapon,
    Bracers,
    Belt,
    Ring,
    Pants,
    Boots

}