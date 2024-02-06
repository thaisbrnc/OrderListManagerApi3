using Moq;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Tests;

public class GroupRepositoryTests
{
    private readonly Mock<Database> _database = new Mock<Database>();

    public GroupRepositoryTests()
    {
    }

    [Theory]
    [InlineData("Higiene")]
    [InlineData("Verduras")]
    public void AddGroupTest_GrupoJaAdicionado_Failure(string name)
    {
        //arrange
        GroupRepository groupRepository = new GroupRepository(new GroupDto(), _database.Object);
        Group group = new Group() { Description = name };
        _database.Object.groups.Add(group);

        //act
        string retorno = groupRepository.Add(name);

        //assert
        Assert.Matches($"Grupo '{name}' já consta na lista.", retorno);
    }

    [Theory]
    [InlineData("Higiene")]
    [InlineData("Verduras")]
    public void AddGroupTest_GrupoAdicionadoComSucesso_Succes(string name)
    {
        //arrange
        GroupRepository groupRepository = new GroupRepository(new GroupDto(), _database.Object);

        //act
        string retorno = groupRepository.Add(name);

        //assert
        Assert.Matches($"Grupo '{name}' cadastrado com sucesso.", retorno);
    }

}
