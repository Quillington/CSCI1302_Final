using System;
using System.IO;
using System.Collections.Generic;

public class ErrorHandling {
    private int _lineNumber;
    private string _type;
    private string _badString;
    private string _reasonForError;
    public LinkedList<ErrorHandling> exceptionsList = new LinkedList<ErrorHandling>();
    public ErrorHandling(string type, int lineNumber, string badString, string reasonForError, Media obj = null) {
        _type = type;
        _lineNumber = lineNumber;
        _badString = badString;
        _reasonForError = reasonForError;
        Media.mediaStorage.Remove(obj);
        exceptionsList.AddLast(this);
        log_error();
    }
    public static void error_missing_data(Media obj, string parseStringFull, string parseStringSegment, string missing_thing) {
        if (parseStringSegment == "") {
            ErrorHandling error = new ErrorHandling(obj.type, obj.lineNumber, parseStringFull,
                $"The following is missing from the string: {missing_thing}.");
        }
    }
    public string error_return() {
        string returnString = "";
        foreach (var item in exceptionsList) {
            returnString +=
                $"Error parsing {_type} entry on line {_lineNumber}. \n" +
                $"\t{_badString} \n" +
                $"{_reasonForError}\n";
        }
        return returnString;
    }

    public void log_error() {
        Console.WriteLine(error_return());
    }
}
