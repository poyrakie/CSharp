using SubmissionTask.Interfaces;
using SubmissionTask.Services;

namespace SubmissionTask.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveToFileShould_ReturnTrue_IfFilePathExists()
    {
        // Arrange

        IFileService fileService = new FileService();
        string filePath = @"C:\Programmering\EC\CSharp\SubmissionTask\test.txt";
        string content = "Test content";

        // Act
        bool result = fileService.SaveToFile(content, filePath);


        // Assert
        Assert.True(result);
    }
    [Fact]
    public void SaveToFileShould_ReturnFalse_IfFilePathDoesNotExists()
    {
        // Arrange

        IFileService fileService = new FileService();
        string filePath = @$"C:\Programmering\EC\CSharp\SubmissionTask\{Guid.NewGuid()}\test.txt";
        string content = "Test content";

        // Act
        bool result = fileService.SaveToFile(content, filePath);


        // Assert
        Assert.False(result);
    }
    [Fact]
    public void LoadFromFileShould_LoadContentFromFile_ThenReturnString()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"C:\Programmering\EC\CSharp\SubmissionTask\test.txt";

        // Act
        var content = fileService.LoadFromFile(filePath);

        // Assert
        Assert.NotNull(content);
    }
    [Fact]
    public void LoadFromFileShould_ReturnNull_WhenFileDoesNotExist()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @$"C:\Programmering\EC\CSharp\SubmissionTask\{Guid.NewGuid()}\test.txt";

        // Act
        var content = fileService.LoadFromFile(filePath);

        // Assert
        Assert.Null(content);
    }
}
