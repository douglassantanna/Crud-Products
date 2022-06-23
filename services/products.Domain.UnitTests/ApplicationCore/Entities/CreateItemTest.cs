using Moq;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Handlers;
using products.Domain.Itens.Interfaces;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateItemTest
{
    private string _itemName = "Item criado";
    private double _itemPrice = 100;

    [Fact]
    public async void Create_item_with_success()
    {
        // Arrange
        var itemRepository = new Mock<IItemRepository>();
        var addItem = new CreateItemCommand(_itemName, _itemPrice);
        itemRepository.Setup(x => x.CreateAsync(It.IsAny<Item>())).Verifiable();
        var addItemCommandHandler = new CreateItemCommandHandler(itemRepository.Object);

        // Act
        var itemResult = await addItemCommandHandler.Handle(addItem, new CancellationToken());

        // Assert
        itemRepository.Verify(x => x.CreateAsync(It.IsAny<Item>()), Times.Once);
        Assert.NotNull(itemResult);
        Assert.NotEmpty(addItem.Name);
        
        Assert.Equal(addItem.Name, itemResult.Message);
    }
}