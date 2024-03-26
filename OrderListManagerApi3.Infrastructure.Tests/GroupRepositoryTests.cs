using Moq;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Tests;

public class GroupRepositoryTests
{
    private readonly Mock<IJsonLocalFileGenerator> _fileGeneratorMock;
    private List<Group> _groups;
    public GroupRepositoryTests()
    {
        _fileGeneratorMock = new Mock<IJsonLocalFileGenerator>();
    }

    [Theory]
    [InlineData("Higiene")]
    [InlineData("Verduras")]
    public void AddGroupTest_GrupoJaAdicionado_Failure(string name)
    {
        //arrange
        var json = _fileGeneratorMock.Object;
        GroupRepository groupRepository = new GroupRepository(new GroupDto(), json, _groups);
        Group group = new Group() { Description = name };
        _groups.Add(group);

        //setup
        _fileGeneratorMock.Setup(j => j.Deserialize());
        _fileGeneratorMock.Setup(j => j.Serialize());

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
        GroupRepository groupRepository = new GroupRepository(new GroupDto(), _fileGeneratorMock.Object, _groups);

        //act
        string retorno = groupRepository.Add(name);

        //assert
        Assert.Matches($"Grupo '{name}' cadastrado com sucesso.", retorno);
    }

}
