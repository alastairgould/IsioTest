using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class AgedBrieUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        IncreaseQuality(item);

        if (item.SellIn <= 0)
        {
            IncreaseQuality(item);
        }

        item.SellIn -= 1;
    }
}
