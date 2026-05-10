using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata.ItemUpdate.ItemUpdaters;
using GildedRoseKata.ItemUpdate.Modifiers;

namespace GildedRoseKata.ItemUpdate;

public class ItemRegistry
{
    private static readonly IUpdateItem DefaultUpdater = new StandardItemUpdate();

    private readonly Dictionary<string, IUpdateItem> Cache = new(StringComparer.OrdinalIgnoreCase);

    private readonly Dictionary<string, IUpdateItem> ItemUpdaters = new(StringComparer.OrdinalIgnoreCase);

    public ItemRegistry()
    {
        RegisterItem("Aged Brie", new AgedBrieUpdate());
        RegisterItem("Backstage passes", new BackstagePassesUpdate());
        RegisterItem("Backstage passes to VIP Area", new VipBackstagePassesUpdate());
        RegisterItem("Sulfuras, Hand of Ragnaros", new SulfurasUpdate());
        RegisterItem("Ice Cream", new IceCreamUpdate());
    }

    public IUpdateItem FindUpdater(Item item)
    {
        if (Cache.TryGetValue(item.Name, out var cachedUpdater))
        {
            return cachedUpdater;
        }

        const string conjuredPrefix = "Conjured ";
        var isConjured = item.Name.StartsWith(conjuredPrefix , StringComparison.OrdinalIgnoreCase);
        var name = isConjured ? item.Name.Substring(conjuredPrefix.Length) : item.Name;

        var bestMatchKey = ItemUpdaters.Keys
            .OrderByDescending(k => k.Length)
            .FirstOrDefault(k => name.Equals(k, StringComparison.OrdinalIgnoreCase)
                              || name.StartsWith(k + " ", StringComparison.OrdinalIgnoreCase));

        var strategy = bestMatchKey != null ? ItemUpdaters[bestMatchKey] : DefaultUpdater;
        var updater = isConjured ? new ConjuredModifier(strategy) : strategy;
        return Cache[item.Name] = updater;
    }

    private void RegisterItem(string name, IUpdateItem updater) => ItemUpdaters[name] = updater;
}