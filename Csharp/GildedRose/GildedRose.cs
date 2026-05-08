using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const int MaxQuality = 40;
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Conjured = "Conjured";

    private IList<Item> Items;

    public GildedRose(IList<Item> items)
    {
        this.Items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            UpdateItemQuality(item);
        }
    }

    private static void UpdateItemQuality(Item item)
    {
        if (item.Name is AgedBrie)
        {
            UpdateAgingBrie(item);
            return;
        }

        if (item.Name is BackstagePasses)
        {
            UpdateBackstagePasses(item);
            return;
        }

        if (item.Name is Sulfuras)
        {
            return;
        }

        if (item.Name is Conjured)
        {
            StandardItem(item, decayMultiplier: 2);
            return;
        }

        StandardItem(item);
    }

    private static void StandardItem(Item item, int decayMultiplier = 1)
    {
        var decayRate = (item.SellIn <= 0 ? 2 : 1) * decayMultiplier;
        DecreaseItemQuality(item, decayRate);
        item.SellIn -= 1;
    }

    private static void UpdateAgingBrie(Item item)
    {
        IncreaseItemQuality(item);

        if (item.SellIn <= 0)
        {
            IncreaseItemQuality(item);
        }

        item.SellIn -= 1;
    }

    private static void UpdateBackstagePasses(Item item)
    {
        IncreaseItemQuality(item);

        if (item.SellIn <= 7)
        {
            IncreaseItemQuality(item);
            IncreaseItemQuality(item);
        }

        if (item.SellIn <= 2)
        {
            IncreaseItemQuality(item);
        }

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }

        item.SellIn -= 1;
    }

    private static void IncreaseItemQuality(Item item)
    {
        if (item.Quality < MaxQuality)
        {
            item.Quality += 1;
        }
    }

    private static void DecreaseItemQuality(Item item, int amount)
    {
        item.Quality -= amount;
        
        if (item.Quality < 0)
        {
            item.Quality = 0;
        }
    }
}