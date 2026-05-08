using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
{
    [Fact]
    public void ExampleTest()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("fixme", Items[0].Name);
    }
    
    [Theory]
    [InlineData("foo")]
    [InlineData("Aged Brie")]
    [InlineData("Backstage passes to a TAFKAL80ETC concert")]
    public void SellInDecreasesByOne_WhenADayPasses(string name)
    {
        var (app, items) = CreateGildedRose([GenericItem(name, sellIn: 2)]);
        
        app.UpdateQuality();
        
        Assert.Equal(1, items[0].SellIn);
    }
  
    [Theory]
    [InlineData("foo")]
    public void QualityDegradesByOne_WhenNotPastSellByDate(string name)
    {
        var (app, items) = CreateGildedRose([GenericItem(name, quality: 10)]);
        
        app.UpdateQuality();
        
        Assert.Equal(9, items[0].Quality);
    }
    
    private (GildedRose, IList<Item>) CreateGildedRose(IList<Item> Items) => (new GildedRose(Items), Items);
   
    private Item GenericItem(string name = "foo", int sellIn = 2, int quality = 0) => new() { Name = name, SellIn = sellIn, Quality = quality };

}