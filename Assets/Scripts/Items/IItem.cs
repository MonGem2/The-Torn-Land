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
    public List<State> Effects;
    public ItemType type;

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