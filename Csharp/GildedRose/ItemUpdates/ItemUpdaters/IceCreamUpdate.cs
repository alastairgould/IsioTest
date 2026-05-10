using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class IceCreamUpdate : IUpdateItem
{
    public void Update(Item item)
    {
        var decayRate = item.SellIn <= 0 ? 6 : 3;
        DecreaseQuality(item, decayRate);
        item.SellIn -= 1;
    }
}
