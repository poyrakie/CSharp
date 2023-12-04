namespace SubmissionTask.Interfaces;

public interface IFileService
{
    bool SaveToFile(string filePath, string content);
    string LoadFromFile(string filePath);
}