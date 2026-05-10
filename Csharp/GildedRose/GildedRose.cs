using System.Collections.Generic;
using GildedRoseKata.ItemUpdate;

namespace GildedRoseKata;

public class GildedRose(IList<Item> items)
{
    private readonly ItemRegistry ItemRegistry = new();

    public void UpdateQuality()
    {
        foreach (var item in items)
        {
            var update = ItemRegistry.FindUpdater(item);
            update.Update(item);
        }
    }
}
