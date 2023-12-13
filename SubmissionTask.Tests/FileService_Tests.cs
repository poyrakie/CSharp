using Newtonsoft.Json;
using SubmissionTask.Interfaces;
using SubmissionTask.Models;
using SubmissionTask.Services;

namespace SubmissionTask.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveToFileShould_SaveJsonContactListToFile_ThenReturnTrue()
    {
        // Arrange
        List<IContact> testList = [];
        IFileService fileService = new FileService();
        IContact contact = new Contact();
        contact.FirstName = "Test";
        contact.LastName = "Test";
        contact.Email = "Test";
        contact.PhoneNumber = "Test";
        contact.City = "Test";
        contact.Road = "Test";
        contact.HouseNumber = "Test";
        contact.PostalCode = "Test";
        testList.Add(contact);

        var json = JsonConvert.SerializeObject(testList, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
        });


        // Act
        bool result = fileService.SaveToFile(json);


        // Assert
        Assert.True(result);
    }
    [Fact]
    public void LoadFromFileShould_LoadContactListFromFile_ThenReturnListInString()
    {
        // Arrange
        IFileService fileService = new FileService();

        // Act
        var content = fileService.LoadFromFile();

        // Assert
        Assert.NotNull(content);
    }
    //[Fact]
    //public void LoadFromFileShould_ReturnNull_WhenFileDoesNotExist()
    //{
    //    // Arrange
    //    IFileService fileService = new FileService();

    //    // Act
    //    var content = fileService.LoadFromFile();

    //    // Assert
    //    Assert.Null(content);
    //}
}
