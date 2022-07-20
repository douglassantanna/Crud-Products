using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateItemTest
{


    [Fact]
    public void Create_item_with_success()
    {
        var newItem = new Item(name: "pencil", price: 10);
        Assert.Equal("pencil", newItem.Name);
        Assert.Equal(10, newItem.Price);
    }

    [Fact]
    public void WhenCreatingItem_WithNoName_ThrowExcepiton()
    {   
        var exception = Assert.Throws<CustomException>(() => new Item("", 10));
    }
    [Fact]
    public void WhenCreatingItem_WithNoPrice_ThrowExcepiton()
    {   
        var exception = Assert.Throws<CustomException>(() => new Item("Caneta", 0));
    }
}