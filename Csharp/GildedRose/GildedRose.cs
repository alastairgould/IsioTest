using System.Collections.Generic;
using GildedRoseKata.ItemUpdates;

namespace GildedRoseKata;

public class GildedRose
{
    private static readonly ItemRegistry ItemRegistry = new();

    private IList<Item> Items;

    public GildedRose(IList<Item> items)
    {
        Items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var update = ItemRegistry.FindUpdater(item);
            update.Update(item);
        }
    }
}
