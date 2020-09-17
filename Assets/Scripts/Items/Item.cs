using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject, IItem
{
    public string Name { get { return _name; } set { _name = value; } }

    public string Description { get { return _descr; } set { _descr = value; } }

    public Sprite UIIcon { get { return _uiIcon; } set { _uiIcon = value; } }

    public string ID { get { return _id; } set { _id = value; } }

    public ItemData Data { get { return _data; } set { _data = value; } }

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private string _id;
    [SerializeField] private ItemData _data;


    public bool Use()
    {

        return true;
    }


    public IItem Clone()
    {
        Item item = new Item();
        item = this;
        item._name = _name;
        item._descr = _descr;
        item._uiIcon = _uiIcon;
        item.Data = _data;

        return item;
    }
}
