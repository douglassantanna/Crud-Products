using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateItemTest
{


    [Fact]
    public void Create_item_with_success()
    {
        Category category = new("computer");
        var newItem = new Item(name: "pencil", price: 10, category: category);
        Assert.Equal("pencil", newItem.Name);
        Assert.Equal(10, newItem.Price);
    }

    [Fact]
    public void WhenCreatingItem_WithNoName_ThrowExcepiton()
    {
        Category category = new("computer");
        var exception = Assert.Throws<CustomException>(() => new Item("", 10, category));
    }
    [Fact]
    public void WhenCreatingItem_WithNoPrice_ThrowExcepiton()
    {
        Category category = new("computer");
        var exception = Assert.Throws<CustomException>(() => new Item("Caneta", 0, category));
    }
}