using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    [SerializeField] private Transform pointToMoveItem;

    [SerializeField] protected int sizeInventory = 10;

    protected int itemsCount = 0;

    [SerializeField] protected List<ItemGameObject> items;

    public bool IsCanGiveItem() => items.Count > 0;
    public bool IsCanGiveItem(ItemSO itemType) => items.Exists(x => x.ItemType == itemType);
    public bool IsCanTakeItem() => itemsCount < sizeInventory;

    public ItemGameObject GiveItems()
    {
        if (!IsCanGiveItem()) return null;


        ItemGameObject item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);

        itemsCount--;

        OffsetObjects();

        return item;
    }

    public ItemGameObject GiveItems(ItemSO itemType)
    {
        if (!IsCanGiveItem(itemType)) return null;

        ItemGameObject item = items.Find(x => x.ItemType == itemType);
        items.Remove(item);

        itemsCount--;

        OffsetObjects();

        return item;
    }

    protected virtual Vector3 ItemPosOffset(int itemsCount) => new Vector3(itemsCount % 5, 0, itemsCount / 5);

    public void TakeItem(ItemGameObject item)
    {
        item.SetMove(pointToMoveItem, this, ItemPosOffset(itemsCount));

        itemsCount++;

        OffsetObjects();
    }

    public void MoveToStorageComplete(ItemGameObject item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            OffsetObjects();
        }
    }

    private void OffsetObjects()
    {
        int count = 0;
        foreach (ItemGameObject item in items)
        {
            item.SetMove(pointToMoveItem, this, ItemPosOffset(count));
            count++;
        }
    }
}
