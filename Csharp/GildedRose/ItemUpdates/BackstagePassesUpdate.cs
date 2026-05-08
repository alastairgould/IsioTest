using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class BackstagePassesUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        if (item.SellIn <= 2)
        {
            IncreaseQuality(item, 4);
        }
        else if (item.SellIn <= 7)
        {
            IncreaseQuality(item, 3);
        }
        else
        {
            IncreaseQuality(item, 1);
        }

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }

        item.SellIn -= 1;
    }
}
