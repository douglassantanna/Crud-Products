using Moq;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Handlers;
using products.Domain.Itens.Interfaces;

namespace products.Domain.Tests.Itens;

public class CreateItemTest
{
    [Fact]
    public async void Create_item_with_success()
    {
        // Arrange
        var itemRepository = new Mock<IItemRepository>();
        var addItem = new CreateItemCommand("", 100);
        itemRepository.Setup(x => x.CreateAsync(It.IsAny<Item>())).Verifiable();
        // itemRepository.Setup(x => x.CreateAsync(It.IsAny<Item>())).Returns(Task.FromResult(addItem));
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