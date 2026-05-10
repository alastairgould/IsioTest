using static GildedRoseKata.IUpdateItem;

namespace GildedRoseKata.ItemUpdates.Modifiers;

internal class ConjouredModifier(IUpdateItem inner) : IUpdateItem
{
    private IUpdateItem _inner = inner;

    public void Update(Item item)
    {
        var qualityBefore = item.Quality;

        _inner.Update(item);

        var change = qualityBefore - item.Quality;

        DecreaseQuality(item, change);
    }
}