using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemStorage
{
    protected override Vector3 ItemPosOffset(int intemCount) => new Vector3(0, intemCount, 0);
}
