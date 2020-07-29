using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject, IItem
{
    public string Name => _name;

    public string Description => _descr;

    public Sprite UIIcon => _uiIcon;

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private Sprite _uiIcon;

    public IItem Clone()
    {
        Item item = new Item();
        item = this;
        item._name = _name;
        item._descr = _descr;
        item._uiIcon = _uiIcon;

        return item;
    }
}
