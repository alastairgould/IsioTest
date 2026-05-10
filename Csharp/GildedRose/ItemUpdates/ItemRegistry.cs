using System.Collections.Generic;

namespace GildedRoseKata.ItemUpdates;

public class ItemRegistry
{
    private static readonly IUpdateItem Default = new StandardItemUpdate();

    private static readonly Dictionary<string, IUpdateItem> ItemUpdaters = new();

    public ItemRegistry()
    {
        Register("Aged Brie", new AgedBrieUpdate());
        Register("Backstage passes to a TAFKAL80ETC concert", new BackstagePassesUpdate());
        Register("Sulfuras, Hand of Ragnaros", new SulfurasUpdate());
        Register("Conjured", new StandardItemUpdate(decayMultiplier: 2));
    }

    public IUpdateItem FindUpdater(Item item) => ItemUpdaters.GetValueOrDefault(item.Name, Default);
    
    private void Register(string name, IUpdateItem updater) => ItemUpdaters[name] = updater;
}