namespace SubmissionTask.Interfaces;

public interface IFileService
{
    bool SaveToFile(string content);
    string LoadFromFile();
}