using SubmissionTask.ClassLibrary.Interfaces;
using System.Diagnostics;

namespace SubmissionTask.ClassLibrary.Services;

///<summary>
/// Service för filhantering, implementerar interface IFileService.
/// Ansvarar för att spara och ladda data till och från en specifik fil med hårdkodad sökväg
///</summary>
public class FileService : IFileService
{
    ///<summary>
    /// sparar innehåll till den angivna filen ovan. Returnerar bool baserat på operationens framgång.
    ///</summary>
    public bool SaveToFile(string content, string _filePath)
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

    ///<summary>
    /// Laddar innehåll från den angivna filen ovan. 
    /// Raturnerar null om fil ej exsisterar eller något oväntat fel uppstår.
    /// Returnerar innehållet i filen i string format om fil exsisterar.
    ///</summary>
    public string LoadFromFile(string _filePath)
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
