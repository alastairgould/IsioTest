using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates.Modifiers;

internal class ConjuredModifier(IUpdateItem inner) : IUpdateItem
{
    public void Update(Item item)
    {
        var qualityBefore = item.Quality;

        inner.Update(item);

        var change = qualityBefore - item.Quality;

        if (change > 0)
        {
            DecreaseQuality(item, change);
        }
    }
}