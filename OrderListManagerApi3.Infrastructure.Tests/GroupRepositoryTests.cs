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
        _groups = new();
    }

    [Theory]
    [InlineData("Higiene")]
    [InlineData("Verduras")]
    public void AddGroupTest_GroupAlreadyExists_Failure(string name)
    {
        try
        {
            //arrange
            var group = new Group() { Description = name };
            _groups.Add(group);

            //setup
            _fileGeneratorMock.Setup(j => j.Serialize(_groups));
            _fileGeneratorMock.Setup(j => j.Deserialize()).Returns(_groups);
            var groupRepository = new GroupRepository(new GroupDto(), _fileGeneratorMock.Object);

            //act
            var group2 = groupRepository.Add(name);
            //Assert.Fail("Exception should be thrown");
        }
        catch(Exception ex)
        {
            //assert
            Assert.Matches($"Erro: Grupo '{name}' já consta na lista.", ex.Message);
        }
    }

    [Theory]
    [InlineData("Limpeza")]
    [InlineData("Doces")]
    public void AddGroupTest_AddedSuccessfully_Succes(string name)
    {
        //arrange
        var group1 = new GroupDto() { Description = name };

        //setup
        _fileGeneratorMock.Setup(j => j.Serialize(_groups));
        _fileGeneratorMock.Setup(j => j.Deserialize()).Returns(_groups);
        var groupRepository = new GroupRepository(new GroupDto(), _fileGeneratorMock.Object);

        //act
        var group2 = groupRepository.Add(name);

        //assert
        Assert.Equal(group1.Description, group2.Description);
    }

}