using UnityEngine;
using System.Collections.Generic;
using Kuroneko.UtilityDelivery;

public abstract class PopupList<T, E> : Popup where T : PopupItem<E>
{
  [SerializeField] private RectTransform popupHolder;
  [SerializeField] private PopupItem<E> samplePopupItem;

  protected List<PopupItem<E>> _items = new();
  protected List<E> _data = new();

  protected override void Start() 
  {
    base.Start();
    TryInstantiateItems();
  }

  protected abstract List<E> GetData();

  protected void TryInstantiateItems() 
  {
    _data = GetData();
    int itemsToCreate = _data.Count - _items.Count;

    if (itemsToCreate > 0) {
      samplePopupItem.gameObject.SetActiveFast(true);
      for (int i = 0; i < itemsToCreate; ++i) {
        PopupItem<E> item = Instantiate(samplePopupItem, popupHolder);
        _items.Add(item);
      }
    }

    foreach (PopupItem<E> item in _items)
      item.Hide();

    samplePopupItem.gameObject.SetActiveFast(false);
  }

  protected void ShowPopupItems() 
  {
    base.ShowPopup();
    TryInstantiateItems();

    for (int i = 0; i < _data.Count; ++i) 
    {
      _items[i].Init(_data[i]);
      _items[i].Show();
    }
  }
}
