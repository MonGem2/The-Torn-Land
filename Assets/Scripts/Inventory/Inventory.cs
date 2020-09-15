using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player _player;
    public static List<Item> Items = new List<Item>();
    public List<Item> Equips;
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _originalParent;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _equipParent;
    [SerializeField] private Transform _infoPanel;
    public static int _capacity = 40;

    ///public Button InvButton;


    private void Start()
    {
        //InvButton.onClick.AddListener(ClickBtn);
        //this._draggingParent.gameObject.SetActive(false);
        Items.Capacity = _capacity;

        (_container as RectTransform).sizeDelta =  new Vector2 (0, (float)(107 + (_capacity / 10 - 1) * 105.3));

        for (int i = 0; i < /*UnityEngine.Random.Range(5, */_capacity-5/* + 1)*/; i++)
        {
            Items.Add(ScriptableObject.CreateInstance("Item") as Item);
            Items[i].Data = new ItemData() { type = ItemType.Disposable };
            Items[i].Name = "PFNSDHKFHDS";
        }

        Items.Add(new Item() { Data = new ItemData() { type = ItemType.Boots }, Name = "PFNSDHKFHDS" });
        Items.Add(new Item() { Data = new ItemData() { type = ItemType.Bracers }, Name = "PFNSDHKFHDS" });
        Items.Add(new Item() { Data = new ItemData() { type = ItemType.Armor }, Name = "PFNSDHKFHDS" });
        Items.Add(new Item() { Data = new ItemData() { type = ItemType.Pants }, Name = "PFNSDHKFHDS" });
        Items.Add(new Item() { Data = new ItemData() { type = ItemType.Pants }, Name = "PFNSDHKFHDS" });

        foreach (var item in Items)
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_originalParent,_draggingParent, _equipParent);
            cell.Render(item);

            cell._item.player = _player;
            cell.Ejection += Destroyer;
            cell.Deselection += InfoHide;
            cell.Selection += InfoSet;
            cell.Using += UseItem;
        }

        transform.parent.parent.gameObject.SetActive(false);

    }

    //void ClickBtn()
    //{
    //    this._draggingParent.gameObject.active = !this._draggingParent.gameObject.active;
    //    Debug.Log("Inventory");
    //}

    private void OnEnable()
    {
        Render(Items);
    }

    public void Render(List <Item> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);


        foreach (var item in items) 
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_originalParent, _draggingParent, _equipParent);
            cell.Render(item);

            cell._item.player = _player;
            cell.Ejection += Destroyer;
            cell.Deselection += InfoHide;
            cell.Selection += InfoSet;
            cell.Using += UseItem;
        }
    }

    private void Destroyer(object sender, EventArgs e)
    {
            Destroy(((InventoryCell)sender).gameObject);
            //Items.Remove();
    }

    private void InfoSet(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(!_infoPanel.gameObject.activeInHierarchy);
        if (_infoPanel.gameObject.activeInHierarchy)
        {
            _infoPanel.transform.position = (sender as InventoryCell).transform.position;
            if (_infoPanel.transform.localPosition.y < -100)
            {
                _infoPanel.transform.localPosition = new Vector2(_infoPanel.transform.localPosition.x, -100);
            }
            if (_infoPanel.transform.localPosition.x > 1000)
            {
                _infoPanel.transform.localPosition = new Vector2(1000, _infoPanel.transform.localPosition.y);
            }
            GameObject.Find("NameItem").GetComponent<Text>().text = (sender as InventoryCell)._item.Name;
            GameObject.Find("ImageItem").GetComponent<Image>().sprite = (sender as InventoryCell)._item.UIIcon;
            GameObject.Find("TypeItem").GetComponent<Text>().text = (sender as InventoryCell)._item.Data.type.ToString();
            GameObject.Find("DescrItem").GetComponent<Text>().text = (sender as InventoryCell)._item.Description;

            foreach (Transform child in GameObject.Find("EffectsItem").GetComponent<GridLayoutGroup>().transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var effect in (sender as InventoryCell)._item.Data.Effects)
            {
                GameObject NewObj = new GameObject();
                Image NewImage = NewObj.AddComponent<Image>();
                NewImage.sprite = Resources.Load<Sprite>(effect.ico);
                NewObj.GetComponent<RectTransform>().SetParent(GameObject.Find("EffectsItem").GetComponent<GridLayoutGroup>().transform);
                NewObj.SetActive(true);
            }
        }
    }


    private void InfoHide(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(false);
    }

    private void ThrowItem(object sender, EventArgs e)
    {

        Debug.LogWarning("Qwerpop");

    }
    private void UseItem(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(false);
        if ((sender as InventoryCell)._item.Use())
        {
            if ((sender as InventoryCell)._item.Data.type > (ItemType)1
                && (sender as InventoryCell).transform.parent != _equipParent
                .Find((sender as InventoryCell)._item.Data.type.ToString() + "BackCell"))
            {
                if (_equipParent.Find((sender as InventoryCell)
                    ._item.Data.type.ToString() + "BackCell").childCount == 0)
                {
                    (sender as InventoryCell).transform.SetParent(_equipParent.Find((sender as InventoryCell)
                        ._item.Data.type.ToString() + "BackCell"));
                    (sender as InventoryCell).transform.localPosition = new Vector3(0, 0, 0);
                    Equips.Add((sender as InventoryCell)._item);
                    Items.Remove((sender as InventoryCell)._item);
                }
                else
                {
                    _equipParent.Find((sender as InventoryCell)
                        ._item.Data.type.ToString() + "BackCell").GetChild(0).SetParent(_originalParent);
                    (sender as InventoryCell).transform.SetParent(_equipParent.Find((sender as InventoryCell)
                        ._item.Data.type.ToString() + "BackCell"));
                    (sender as InventoryCell).transform.localPosition = new Vector3(0, 0, 0);
                    Equips.Remove(_equipParent.Find((sender as InventoryCell)
                        ._item.Data.type.ToString() + "BackCell").GetChild(0).GetComponent<InventoryCell>()._item);
                    Equips.Add((sender as InventoryCell)._item);
                    Items.Remove((sender as InventoryCell)._item);
                    Items.Add(_equipParent.Find((sender as InventoryCell)
                        ._item.Data.type.ToString() + "BackCell").GetChild(0).GetComponent<InventoryCell>()._item);
                }
            }
            else if ((sender as InventoryCell).transform.parent == _equipParent
                .Find((sender as InventoryCell)._item.Data.type.ToString() + "BackCell"))
            {
                (sender as InventoryCell).transform.SetParent(_originalParent);
                (sender as InventoryCell).transform.localPosition = new Vector3(0, 0, 0);
                Items.Add((sender as InventoryCell)._item);
                Equips.Remove((sender as InventoryCell)._item);
            }
            else if ((sender as InventoryCell)._item.Data.type == ItemType.Disposable)
            {
                Items.Remove((sender as InventoryCell)._item);
                Destroyer(sender, e);
            }

        }

    }

}
