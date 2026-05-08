using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

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
        if (item.Name == "Aged Brie")
        {
            IncreaseItemQuality(item);

            if (item.SellIn <= 0)
            {
                IncreaseItemQuality(item);
            }
        }
        
        if (item.Name is "Backstage passes to a TAFKAL80ETC concert")
        {
            UpdateBackstagePasses(item);
        }
        
        if (item.Name != "Sulfuras, Hand of Ragnaros" && item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            DecrementQuality(item);
        }


        if (item.SellIn <= 0)
        {
            if (item.Name == "Aged Brie")
            {
            }
            else
            {
                if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                }
                else
                {
                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        DecrementQuality(item);
                    }
                }
            }
        }
        
        DecreaseItemSellIn(item);
    }

    private static void DecreaseItemSellIn(Item item)
    {
        if (item.Name != "Sulfuras, Hand of Ragnaros")
        {
            item.SellIn -= 1;
        }
    }

    private static void UpdateBackstagePasses(Item item)
    {
        IncreaseItemQuality(item);

        if (item.SellIn < 11)
        {
            IncreaseItemQuality(item);
        }

        if (item.SellIn < 6)
        {
            IncreaseItemQuality(item);
        }

        if (item.SellIn <= 0)
        {
            SetItemToZeroQuality(item);
        }        
    }

    private static void SetItemToZeroQuality(Item item)
    {
        item.Quality = 0;
    }

    private static void IncreaseItemQuality(Item item)
    {
        if (item.Quality < 50)
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