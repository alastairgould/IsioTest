using System.Collections.Generic;
using GildedRoseKata.ItemUpdates;

namespace GildedRoseKata;

public class GildedRose
{
    private static readonly IUpdateItem Default = new StandardItemUpdate();

    private static readonly Dictionary<string, IUpdateItem> ItemUpdaters = new()
    {
        ["Aged Brie"] = new AgedBrieUpdate(),
        ["Backstage passes to a TAFKAL80ETC concert"] = new BackstagePassesUpdate(),
        ["Sulfuras, Hand of Ragnaros"] = new SulfurasUpdate(),
        ["Conjured"] = new StandardItemUpdate(decayMultiplier: 2),
    };

    private IList<Item> Items;

    public GildedRose(IList<Item> items)
    {
        Items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var update = ItemUpdaters.GetValueOrDefault(item.Name, Default);
            update.Update(item);
        }
    }
}
