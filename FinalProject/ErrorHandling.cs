using System;
using System.Collections.Generic;
using System.IO;

public class ErrorHandling {
    private int _lineNumber;
    private string _type;
    private string _badString;
    private string _reasonForError;

    public static bool didLogDataFail = false;
    public const string LOG_ERROR_NAME = "logerror.txt";
    public static List<ErrorHandling> exceptionsList = new List<ErrorHandling>();

    public ErrorHandling(string type, int lineNumber, string badString, string reasonForError, Media obj = null) {
        _type = type;
        _lineNumber = lineNumber;
        _badString = badString;
        _reasonForError = reasonForError;
    }
    public static void error_create_and_add_list(string type, int lineNumber, string badString, string reasonForError, Media obj = null) {
        ErrorHandling error = new ErrorHandling(type, lineNumber, badString, reasonForError, obj);
        exceptionsList.Add(error);
        Media.mediaStorage.Remove(obj);
    }

    string error_return() {
        string returnString = "";
            returnString +=
                $"Error parsing {_type} entry on line {_lineNumber}. \n" +
                $"\t{_badString} \n" +
                $"{_reasonForError}\n\n";
        return returnString;
    }
    static void error_console() {
        Console.WriteLine("The log file was unable to be created. \n" +
            "Due to this, the errors are printed in console" +
            "as an emergency.");
        foreach (ErrorHandling error in exceptionsList) {
            Console.WriteLine(error.error_return());
        }
    }
    public static void log_error() {
        if (File.Exists(LOG_ERROR_NAME)) {
            File.Delete(LOG_ERROR_NAME);
        }
        StreamWriter writer = new StreamWriter(LOG_ERROR_NAME);
        try {
            if (didLogDataFail) {
                writer.Write($"Logging statistic data failed and data is not properly" +
                    $"backed up to a file.\n\n");
            }
            foreach (ErrorHandling error in exceptionsList) {
                writer.Write(error.error_return());
            }
        }
        catch {
            error_console();
        }
        finally {
            writer.Close();
        }     
    }
}
