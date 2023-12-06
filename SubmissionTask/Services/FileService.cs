using SubmissionTask.Interfaces;
using System.Diagnostics;

namespace SubmissionTask.Services;

public class FileService : IFileService
{
    private readonly string _filePath = @"C:\Programmering\EC\CSharp\SubmissionTask\content.json";

    public bool SaveToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }
    public string LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (var sr = new StreamReader(_filePath))
                {
                    return sr.ReadToEnd();
                }
                    
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
