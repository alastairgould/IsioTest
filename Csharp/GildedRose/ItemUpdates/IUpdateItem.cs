namespace GildedRoseKata;

internal interface IUpdateItem
{
    const int MaxQuality = 40;

    void Update(Item item);

    static void IncreaseQuality(Item item, int amount = 1)
    {
        item.Quality += amount;
        if (item.Quality > MaxQuality)
        {
            item.Quality = MaxQuality;
        }
    }

    static void DecreaseQuality(Item item, int amount)
    {
        item.Quality -= amount;
        if (item.Quality < 0)
        {
            item.Quality = 0;
        }
    }
}
