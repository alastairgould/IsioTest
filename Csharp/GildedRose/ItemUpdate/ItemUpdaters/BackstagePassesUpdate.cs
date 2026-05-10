using static GildedRoseKata.ItemUpdate.IUpdateItem;

namespace GildedRoseKata.ItemUpdate.ItemUpdaters;

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
            IncreaseQuality(item);
        }

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }

        item.SellIn -= 1;
    }
}
