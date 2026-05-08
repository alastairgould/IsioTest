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
    
    [Theory]
    [InlineData("foo")]
    public void QualityDegradesTwiceAsFast_WhenPastSellByDate(string name)
    {
        var (app, items) = CreateGildedRose([GenericItem(name: name, quality: 10, sellIn:0)]);
        
        app.UpdateQuality();
        
        Assert.Equal(8, items[0].Quality);
    }
    
    [Theory]
    [InlineData(2, 0)]
    [InlineData(0, 1)] //Checking that there's a guard for double degradation rule
    public void QualityNeverGoesBelowZero_WhenADayPasses(int sellIn, int quality)
    {
        var (app, items) = CreateGildedRose([GenericItem(sellIn: sellIn, quality: quality)]);

        app.UpdateQuality();

        Assert.Equal(0, items[0].Quality);
    }
    
    [Fact]
    public void AgedBrieIncreasesByOne_WhenWithinSellByDate()
    {
        var (app, items) = CreateGildedRose([AgedBrie(sellIn: 2)]);
        
        app.UpdateQuality();
        
        Assert.Equal(11, items[0].Quality);
    }
    
    [Fact]
    public void AgedBrieIncreasesByTwo_WhenNotWithinSellByDate()
    {
        var (app, items) = CreateGildedRose([AgedBrie(sellIn: 0)]);
        
        app.UpdateQuality();
        
        Assert.Equal(12, items[0].Quality);
    }
    
    private (GildedRose, IList<Item>) CreateGildedRose(IList<Item> Items) => (new GildedRose(Items), Items);
   
    private Item GenericItem(string name = "foo", int sellIn = 2, int quality = 0) => new() { Name = name, SellIn = sellIn, Quality = quality };

    private Item AgedBrie(int sellIn = 1, int quality = 10) => new() { Name = "Aged Brie", SellIn = sellIn, Quality = quality };

}