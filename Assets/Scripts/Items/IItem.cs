using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    Player player { get; set; }
    int ID { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Sprite UIIcon { get; set; }
    ItemData Data { get; set; }


    bool Use();
    IItem Clone();
}


public class ItemData
{
    public List<State> Effects = new List<State>();
    public ItemType type = ItemType.Unused;

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