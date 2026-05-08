using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates;

internal class StandardItemUpdate : IUpdateItem
{
    private readonly int decayMultiplier;

    public StandardItemUpdate(int decayMultiplier = 1)
    {
        this.decayMultiplier = decayMultiplier;
    }

    public void Update(Item item)
    {
        var decayRate = (item.SellIn <= 0 ? 2 : 1) * decayMultiplier;
        DecreaseQuality(item, decayRate);
        item.SellIn -= 1;
    }
}
