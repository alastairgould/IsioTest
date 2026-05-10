using static GildedRoseKata.ItemUpdate.IUpdateItem;

namespace GildedRoseKata.ItemUpdate.ItemUpdaters;

internal class StandardItemUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        var decayRate = item.SellIn <= 0 ? 2 : 1;
        DecreaseQuality(item, decayRate);
        item.SellIn -= 1;
    }
}
