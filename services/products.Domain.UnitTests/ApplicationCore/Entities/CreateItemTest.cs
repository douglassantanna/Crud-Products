using products.Domain.Itens.Entities;
using products.Domain.Itens.Exceptions;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateItemTest
{


    [Fact]
    public async void Create_item_with_success()
    {
        var newItem = new Item(name: "pencil", price: 10);
        Assert.Equal("pencil", newItem.Name);
        Assert.Equal(10, newItem.Price);
    }

    [Fact]
    public void WhenCreatingItem_WithNoName_ThrowExcepiton()
    {   
        var exception = Assert.Throws<ItemException>(() => new Item("", 10));
    }
}