using System;
using System.IO;
using System.Collections.Generic;

public static class Helpers
{
    public static string GetAvailableFilePath(string path)
    {
        if (CheckIfFileExists(path))
        {
            return GenerateNewPathWithNumber(path, 0);
        }
        else
        {
            return path;
        }
    }

    private static bool CheckIfFileExists(string path)
    {
        return File.Exists(path);
    }

    private static string GenerateNewPathWithNumber(string path, int number)
    {
        string fileExtension = Path.GetExtension(path);
        string newPath = Path.ChangeExtension(path, null);
        newPath = newPath + "_(" + number + ")" + fileExtension;
        if (CheckIfFileExists(newPath))
        {
            return GenerateNewPathWithNumber(path, number + 1);
        }

        return newPath;
    }

    public static string FormatFileName(string fileName)
    {
        string newString = fileName.Replace(" ", "_");
        return newString;
    }
}