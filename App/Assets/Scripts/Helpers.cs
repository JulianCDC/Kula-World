using System;

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
        return System.IO.File.Exists(path);
    }

    private static string GenerateNewPathWithNumber(string path, int number)
    {
        string newPath = path + "_(" + number + ")";
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