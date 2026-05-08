using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class BackstagePassesUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        IncreaseQuality(item);

        if (item.SellIn <= 7)
        {
            IncreaseQuality(item);
            IncreaseQuality(item);
        }

        if (item.SellIn <= 2)
        {
            IncreaseQuality(item);
        }

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }

        item.SellIn -= 1;
    }
}
