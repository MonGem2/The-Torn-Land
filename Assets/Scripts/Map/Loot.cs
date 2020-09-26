using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public Transform LootView;
    public List<Item> Items = new List<Item>();
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _originalParent;
    [SerializeField] private Transform _draggingParent;
    [SerializeField] private Transform _equipParent;
    [SerializeField] private Transform _infoPanel;
    [SerializeField] private Text NameItemW;
    [SerializeField] private Image ImageItemW;
    [SerializeField] private Text TypeItemW;
    [SerializeField] private Text DescrItemW;
    [SerializeField] private Text EffectsItemW;

    ///public Button InvButton;


    private void Start()
    {
        //InvButton.onClick.AddListener(ClickBtn);
        //this._draggingParent.gameObject.SetActive(false);
        
        Destroy(this.gameObject, 300);

    }

    //void ClickBtn()
    //{
    //    this._draggingParent.gameObject.active = !this._draggingParent.gameObject.active;
    //    Debug.Log("Inventory");
    //}


    public void Render(List<Item> items)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);

        foreach (var item in items)
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_originalParent, _draggingParent, _equipParent);
            cell.Render(item);
            cell._item = item;
            cell.isIn = false;

            cell.Ejection += Destroyer;
            cell.Deselection += InfoHide;
            cell.Selection += InfoSet;
            cell.Using += TakeItem;
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
            NameItemW.text = (sender as InventoryCell)._item.Name;
            ImageItemW.sprite = (sender as InventoryCell)._item.UIIcon;
            TypeItemW.text = (sender as InventoryCell)._item.type.ToString();
            DescrItemW.text = (sender as InventoryCell)._item.Description;
            Debug.LogWarning("//TODO: Effects inventory");
        }
    }


    private void InfoHide(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(false);
    }

    private void TakeItem(object sender, EventArgs e)
    {
        _infoPanel.gameObject.SetActive(false);
        if (Inventory.Items.Count < Inventory._capacity)
        {
            Inventory.Items.Add((sender as InventoryCell)._item);
            Items.Remove((sender as InventoryCell)._item);
            Destroyer(sender, e);
        }
        if (Items.Count <= 0)
            Destroy(this.gameObject);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LootView.gameObject.SetActive(true);
            Debug.Log("Heeey");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _infoPanel.gameObject.SetActive(false);
            LootView.gameObject.SetActive(false);
            Debug.Log("Heeey11");
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Heey1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
