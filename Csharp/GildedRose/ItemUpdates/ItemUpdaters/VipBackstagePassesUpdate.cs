using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class VipBackstagePassesUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        if (item.SellIn <= 2)
        {
            IncreaseQuality(item, 8);
        }
        else if (item.SellIn <= 7)
        {
            IncreaseQuality(item, 6);
        }
        else
        {
            IncreaseQuality(item, 2);
        }

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }

        item.SellIn -= 1;
    }
}
