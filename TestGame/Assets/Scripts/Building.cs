using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingType;
    [SerializeField] ItemStorage storageToCreatedItems;

    [SerializeField] ItemStorage storageToNeededItems;
    [SerializeField] ItemStorageInBuilding storageInBuilding;

    private float timeToProduct = 0;

    private bool production = false;
    private bool moveNeededItems = false;

    private bool IsThereNeededItems()
    {
        foreach (ItemSO itemType in buildingType.NeedToProduction)
        {
            if (!storageToNeededItems.IsCanGiveItem(itemType))
                return false;
        }
        return true;
    }

    private void Update() => Production();

    private void Production()
    {
        if (!production && !moveNeededItems)
        {
            if (IsThereNeededItems())
            {
                MoveNeededItems();
                moveNeededItems = true;
            }
            else
            {
                string text = "Production " + buildingType.name + " Stopped due to lack of product";
                if (!UIMesseges.Instance.Texts.Contains(text))
                    UIMesseges.Instance.Texts.Add(text);
            }
        }

        if (!production && moveNeededItems)
        {
            if (storageInBuilding.IsCompleteMoveAllNeededItems(buildingType.NeedToProduction))
            {
                storageInBuilding.DestroyAllNeededItems();
                production = true;
                moveNeededItems = false;
            }
        }

        if (production)
        {
            if (storageToCreatedItems.IsCanTakeItem())
            {
                timeToProduct += Time.deltaTime;
                if (timeToProduct > buildingType.TimeToProduct)
                {
                    ItemGameObject.Create(buildingType.Product, this, storageToCreatedItems);
                    timeToProduct = 0;
                    production = false;
                    moveNeededItems = false;
                }
            }
            else
            {
                string text = buildingType.name + " production stopped due to filling";

                if (!UIMesseges.Instance.Texts.Contains(text))
                    UIMesseges.Instance.Texts.Add(text);
            }
        }
    }

    private void MoveNeededItems()
    {
        foreach (ItemSO itemType in buildingType.NeedToProduction)
        {
            storageInBuilding.TakeItem(storageToNeededItems.GiveItem(itemType));
        }
    }
}
