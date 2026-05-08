namespace GildedRoseKata;

internal interface IUpdateItem
{
    const int MaxQuality = 40;

    void Update(Item item);

    static void IncreaseQuality(Item item)
    {
        if (item.Quality < MaxQuality)
        {
            item.Quality += 1;
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
