using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTriggerMoveToCharacter : ItemTriggerMove
{
    public override bool Trigger(ItemStorage inventory)
    {
        if (inventory.IsCanTakeItem() && _storage.IsCanGiveItem())
        {
            inventory.TakeItem(_storage.GiveItems());
            return true;
        }

        return false;
    }
}
