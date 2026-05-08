using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const int MaxQuality = 40;

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
        if (item.Name is "Aged Brie")
        {
            UpdateAgingBrie(item);
            return;
        }

        if (item.Name is "Backstage passes to a TAFKAL80ETC concert")
        {
            UpdateBackstagePasses(item);
            return;
        }

        if (item.Name is "Sulfuras, Hand of Ragnaros")
        {
            return;
        }
        
        StandardItem(item);
    }

    private static void StandardItem(Item item)
    {
        DecrementQuality(item);

        if (item.SellIn <= 0)
        {
            DecrementQuality(item);
        }

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

    private static void DecrementQuality(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality -= 1;
        }
    }
}