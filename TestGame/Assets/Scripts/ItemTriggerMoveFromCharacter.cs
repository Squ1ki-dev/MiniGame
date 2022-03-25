using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTriggerMoveFromCharacter : ItemTriggerMove
{
    [SerializeField] List <ItemSO> itemTypes;

    public override bool Trigger(ItemStorage inventory)
    {
        foreach (ItemSO itemType in itemTypes)
        {
            if (_storage.IsCanTakeItem() && inventory.IsCanGiveItem(itemType))
            {
                _storage.TakeItem(inventory.GiveItems(itemType));
                return true;
            }
        }
        return false;
    }
}
