using System.Collections.Generic;
using System.Linq;
using GildedRoseKata.ItemUpdates.Modifiers;

namespace GildedRoseKata.ItemUpdates;

public class ItemRegistry
{
    private static readonly IUpdateItem DefaultUpdater = new StandardItemUpdate();

    private static readonly Dictionary<string, IUpdateItem> Cache = new();
    
    private readonly Dictionary<string, IUpdateItem> ItemUpdaters = new();

    public ItemRegistry()
    {
        RegisterItem("Aged Brie", new AgedBrieUpdate());
        RegisterItem("Backstage passes", new BackstagePassesUpdate());
        RegisterItem("Sulfuras, Hand of Ragnaros", new SulfurasUpdate());
    }

    public IUpdateItem FindUpdater(Item item)
    {
        if (Cache.TryGetValue(item.Name, out var cachedUpdater))
        {
            return cachedUpdater;
        }
        
        var isConjured = item.Name.StartsWith("Conjured");

        var name = isConjured ? item.Name.Substring("Conjured".Length).Trim() : item.Name.Trim();
        
        var bestMatchKey = ItemUpdaters.Keys
            .FirstOrDefault(k => name.StartsWith(k));

        var strategy = bestMatchKey != null ? ItemUpdaters[bestMatchKey] : DefaultUpdater;
        var updater = isConjured ? new ConjuredModifier(strategy) : strategy;
        return Cache[item.Name] = updater; 
    } 
    
    private void RegisterItem(string name, IUpdateItem updater) => ItemUpdaters[name] = updater;
    
}