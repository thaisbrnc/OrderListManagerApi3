using Moq;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Tests;

public class UnitTestGroupRepository
{
    private Mock<Database> _database = new Mock<Database>();

    public UnitTestGroupRepository()
    {
        
    }

    [Theory]
    [InlineData("Higiene")]
    [InlineData("higiene")]
    public void AddGroupTest_GrupoJaAdicionado_Failure(string name)
    {
        //arrange
        GroupRepository groupRepository = new GroupRepository(new GroupDto(), _database.Object);
        Group group = new Group() { Description = name };


        //Setup
        _database.Setup(d => d.groups.Add(group));

        //act
        string retorno = groupRepository.Add(name);

        //assert
        Assert.Matches($"Grupo '{name}' já consta na lista.", retorno);
    }
}
