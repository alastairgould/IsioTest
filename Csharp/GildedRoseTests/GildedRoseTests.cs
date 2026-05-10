using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTests
{
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
    [InlineData(0, 1)]  //Checking that there's a guard for double degradation rule
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
    
    [Fact]
    public void SulfurasSellInDateNeverChanges_WhenADayPasses()
    {
        var (app, items) = CreateGildedRose([Sulfuras(sellIn: 5)]);
        
        app.UpdateQuality();
        
        Assert.Equal(5, items[0].SellIn);
    } 
    
    [Fact]
    public void SulfurasQualityNeverChanges_WhenADayPasses()
    {
        var (app, items) = CreateGildedRose([Sulfuras(quality: 5)]);
        
        app.UpdateQuality();
        
        Assert.Equal(5, items[0].Quality);
    } 
       
    [Fact]
    public void BackstagePassQualityDropsToZero_WhenPastConcertDate()
    {
        var (app, items) = CreateGildedRose([BackstagePass(sellIn: 0)]);
        
        app.UpdateQuality();
        
        Assert.Equal(0, items[0].Quality);
        Assert.Equal(-1, items[0].SellIn);;
    }
    
    [Theory]
    [InlineData("Aged Brie", 1, 40)]
    [InlineData("Aged Brie", 0, 39)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 7, 40)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 40)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 39)]
    public void QualityNeverGoesAbove40_WhenADayPasses(string name, int sellIn, int quality)
    {
        var (app, items) = CreateGildedRose([new Item { Name = name, SellIn = sellIn, Quality = quality }]);

        app.UpdateQuality();

        Assert.Equal(40, items[0].Quality);
    }

    [Fact]
    public void BackstagePassQualityIncreasesBy1_WhenThereAre8DaysOrMore()
    {
        var (app, items) = CreateGildedRose([BackstagePass(quality: 5, sellIn: 8)]);
        
        app.UpdateQuality();
        
        Assert.Equal(6, items[0].Quality);
    }
    
    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert")]
    [InlineData("Backstage passes to a ACDC concert")]
    public void BackstagePassQualityIncreasesBy3_WhenThereAre7DaysOrLess(string name)
    {
        var (app, items) = CreateGildedRose([BackstagePass(quality: 5, sellIn: 7, name: name)]);
        
        app.UpdateQuality();
        
        Assert.Equal(8, items[0].Quality);
    }
    
    [Fact]
    public void BackstagePassQualityIncreasesBy4_WhenThereAre2DaysOrLess()
    {
        var (app, items) = CreateGildedRose([BackstagePass(sellIn: 2, quality: 4)]);
        
        app.UpdateQuality();
        
        Assert.Equal(8, items[0].Quality);
    }
    
    [Theory]
    [InlineData(8)]
    [InlineData(7)]
    [InlineData(2)]
    public void BackstagePassQualityDoesNotExceed40Cap_WhenWithinThreshold(int sellIn)
    {
        var (app, items) = CreateGildedRose([BackstagePass(sellIn: sellIn, quality: 40)]);

        app.UpdateQuality();

        Assert.Equal(40, items[0].Quality);
    }

    [Fact]
    public void VipBackstagePassQualityIncreasesBy2_WhenThereAre8DaysOrMore()
    {
        var (app, items) = CreateGildedRose([VipBackstagePass(sellIn: 8, quality: 5)]);

        app.UpdateQuality();

        Assert.Equal(7, items[0].Quality);
    }

    [Fact]
    public void VipBackstagePassQualityIncreasesBy6_WhenThereAre7DaysOrLess()
    {
        var (app, items) = CreateGildedRose([VipBackstagePass(sellIn: 7, quality: 5)]);

        app.UpdateQuality();

        Assert.Equal(11, items[0].Quality);
    }

    [Fact]
    public void VipBackstagePassQualityIncreasesBy8_WhenThereAre2DaysOrLess()
    {
        var (app, items) = CreateGildedRose([VipBackstagePass(sellIn: 2, quality: 5)]);

        app.UpdateQuality();

        Assert.Equal(13, items[0].Quality);
    }

    [Fact]
    public void VipBackstagePassQualityDropsToZero_WhenPastConcertDate()
    {
        var (app, items) = CreateGildedRose([VipBackstagePass(sellIn: 0, quality: 20)]);

        app.UpdateQuality();

        Assert.Equal(0, items[0].Quality);
    }

    [Theory]
    [InlineData(8)]
    [InlineData(7)]
    [InlineData(2)]
    public void VipBackstagePassQualityDoesNotExceed40Cap_WhenWithinThreshold(int sellIn)
    {
        var (app, items) = CreateGildedRose([VipBackstagePass(sellIn: sellIn, quality: 40)]);

        app.UpdateQuality();

        Assert.Equal(40, items[0].Quality);
    }

    [Fact]
    public void ConjuredQualityDegradesByTwo_WhenNotPastSellByDate()
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: 5, quality: 10)]);

        app.UpdateQuality();

        Assert.Equal(8, items[0].Quality);
    }

    [Fact]
    public void ConjuredQualityDegradesByFour_WhenPastSellByDate()
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: 0, quality: 10)]);

        app.UpdateQuality();

        Assert.Equal(6, items[0].Quality);
    }
    
    [Fact]
    public void ConjuredIsIgnored_WhenQualityIsChangedPositively()
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: 5, quality: 10, name: "Conjured Aged Brie")]);

        app.UpdateQuality();

        Assert.Equal(11, items[0].Quality);
    }

    [Theory]
    [InlineData(5, 1)]
    [InlineData(0, 3)]
    public void ConjuredQualityNeverGoesBelowZero_WhenADayPasses(int sellIn, int quality)
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: sellIn, quality: quality)]);

        app.UpdateQuality();

        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void IceCreamQualityDegradesByThree_WhenNotPastSellByDate()
    {
        var (app, items) = CreateGildedRose([IceCream(sellIn: 5, quality: 10)]);

        app.UpdateQuality();

        Assert.Equal(7, items[0].Quality);
    }

    [Fact]
    public void IceCreamQualityDegradesBySix_WhenPastSellByDate()
    {
        var (app, items) = CreateGildedRose([IceCream(sellIn: 0, quality: 10)]);

        app.UpdateQuality();

        Assert.Equal(4, items[0].Quality);
    }

    [Theory]
    [InlineData(5, 2)]
    [InlineData(0, 5)]
    public void IceCreamQualityNeverGoesBelowZero_WhenADayPasses(int sellIn, int quality)
    {
        var (app, items) = CreateGildedRose([IceCream(sellIn: sellIn, quality: quality)]);

        app.UpdateQuality();

        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void ConjuredIceCreamQualityDegradesBySix_WhenNotPastSellByDate()
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: 5, quality: 10, name: "Conjured Ice Cream")]);

        app.UpdateQuality();

        Assert.Equal(4, items[0].Quality);
    }

    [Fact]
    public void ConjuredIceCreamQualityDegradesByTwelve_WhenPastSellByDate()
    {
        var (app, items) = CreateGildedRose([Conjured(sellIn: 0, quality: 20, name: "Conjured Ice Cream")]);

        app.UpdateQuality();

        Assert.Equal(8, items[0].Quality);
    }

    [Fact]
    public void NameStartingWithConjuredButNoSpace_IsTreatedAsStandardItem()
    {
        var (app, items) = CreateGildedRose([new Item { Name = "ConjuredFoo", SellIn = 5, Quality = 10 }]);

        app.UpdateQuality();

        Assert.Equal(9, items[0].Quality);
    }

    [Fact]
    public void ConjuredSulfuras_RemainsUnchanged()
    {
        var (app, items) = CreateGildedRose([new Item { Name = "Conjured Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 }]);

        app.UpdateQuality();

        Assert.Equal(80, items[0].Quality);
        Assert.Equal(5, items[0].SellIn);
    }

    [Fact]
    public void UnknownItem_DegradesAtStandardRate()
    {
        var (app, items) = CreateGildedRose([new Item { Name = "+5 Dexterity Vest", SellIn = 5, Quality = 10 }]);

        app.UpdateQuality();

        Assert.Equal(9, items[0].Quality);
    }

    private (GildedRose, IList<Item>) CreateGildedRose(IList<Item> Items) => (new GildedRose(Items), Items);

    private Item GenericItem(string name = "foo", int sellIn = 2, int quality = 0) => new() { Name = name, SellIn = sellIn, Quality = quality };

    private Item AgedBrie(int sellIn = 1, int quality = 10) => new() { Name = "Aged Brie", SellIn = sellIn, Quality = quality };

    private Item Sulfuras(int sellIn = 5, int quality = 5) => new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = quality };

    private Item BackstagePass(int sellIn = 5, int quality = 5, string name = "Backstage passes to a TAFKAL80ETC concert") => new() { Name = name, SellIn = sellIn, Quality = quality };

    private Item VipBackstagePass(int sellIn = 5, int quality = 5) => new() { Name = "Backstage passes to VIP Area", SellIn = sellIn, Quality = quality };

    private Item IceCream(int sellIn = 5, int quality = 10) => new() { Name = "Ice Cream", SellIn = sellIn, Quality = quality };

    private Item Conjured(int sellIn = 5, int quality = 10, string name = "Conjured Item") => new() { Name = name, SellIn = sellIn, Quality = quality };
}