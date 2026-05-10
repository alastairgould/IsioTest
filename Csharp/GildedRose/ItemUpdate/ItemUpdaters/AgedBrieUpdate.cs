using static GildedRoseKata.ItemUpdate.IUpdateItem;

namespace GildedRoseKata.ItemUpdate.ItemUpdaters;

internal class AgedBrieUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        var increaseRate = item.SellIn <= 0 ? 2 : 1;
        IncreaseQuality(item, increaseRate);
        item.SellIn -= 1;
    }
}
