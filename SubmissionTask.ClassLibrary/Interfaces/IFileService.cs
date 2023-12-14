namespace SubmissionTask.Interfaces;

///<summary>
/// Representerar ett interface för filhantering, 
/// innehåller en metod för att spara till fil och en för att ladda från fil.
///</summary>
public interface IFileService
{
    bool SaveToFile(string content, string _filePath);
    string LoadFromFile(string _filePath);
}