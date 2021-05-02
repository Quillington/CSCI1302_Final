using System;
using System.Collections.Generic;

public class Media {

    const int TYPE = 0, TITLE = 1, COPYRIGHT_YEAR = 2;
    public const string BOOK = "1", MAGAZINE = "2", MOVIE = "3";
    public string type;
    public string title;
    public int copyrightYear;
    private int _lineNumber;
    public int lineNumber {
        get {
            return _lineNumber;
        }
        set {
            _lineNumber = value;
        }
    }
    public static LinkedList<Media> mediaStorage = new LinkedList<Media>();
    //this linked list stores all the Media objects so functions can be called at will
    //as opposed to just when they are read in the file

    public virtual void parse_array_to_var(string[] parseString) {
        void parse_type(string[] parseStringInside) {
            switch (parseStringInside[TYPE]) {
                case BOOK:
                    type = "Book";
                    break;
                case MAGAZINE:
                    type = "Magazine";
                    break;
                case MOVIE:
                    type = "Movie";
                    break;
            }
        }
        void parse_title(string[] parseStringInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", parseStringInside), parseStringInside[TITLE], "title");
            try {
                title = parseStringInside[TITLE];
            }
            catch {
                ErrorHandling error = new ErrorHandling(
                            type, lineNumber, String.Join(",", parseStringInside), $"Type is invalid. " +
                            $"Must be either a book ({Media.BOOK}), movie ({Media.MOVIE}), " +
                            $"or magazine ({Media.MAGAZINE}).", this);
            }
        }
        void parse_copyright_year(string[] parseStringInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", parseStringInside), parseStringInside[COPYRIGHT_YEAR], "copyright year");
            try {
                copyrightYear = Int32.Parse(parseStringInside[COPYRIGHT_YEAR]);
            }
            catch {
                ErrorHandling error = new ErrorHandling(
                    type, lineNumber, String.Join(",", parseStringInside),
                    "Copyright year field is invalid.", this);
            }
        }
        parse_type(parseString);
        parse_title(parseString);
        parse_copyright_year(parseString);
    }

    public static int number_of_records() {
        int count = 0;
        foreach (var record in mediaStorage) {
            count += 1;
        }
        return count;
    }

    public static int oldest_copyright_year() {
        int oldest = Int32.MaxValue;
        foreach (var record in mediaStorage) {
            if (record.copyrightYear < oldest) {
                oldest = record.copyrightYear;
            }
        }
        return oldest;
    }

    public static int newest_copyright_year() {
        int youngest = 0;
        foreach (var record in mediaStorage) {
            if (record.copyrightYear > youngest) {
                youngest = record.copyrightYear;
            }
        }
        return youngest;
    }

}

class Book : Media {
    const int NUMBER_OF_PAGES = 3, AUTHOR = 4;
    int numberOfPages;
    string author;

    public override void parse_array_to_var(string[] input) {
        base.parse_array_to_var(input);
        void parse_number_of_pages(string[] inputInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", inputInside), inputInside[AUTHOR], "author");
            try {
                numberOfPages = Int32.Parse(inputInside[NUMBER_OF_PAGES]);
            }
            catch {
                ErrorHandling error = new ErrorHandling("number of pages", lineNumber, String.Join(",", inputInside), 
                    "Number of pages field is invalid.", this);
            }
        }
        void parse_author(string[] inputInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", inputInside), inputInside[NUMBER_OF_PAGES], "number of pages");
            try {
                author = input[AUTHOR];
            }
            catch {
                ErrorHandling error = new ErrorHandling("author", lineNumber, String.Join(",", inputInside), 
                    "Author field is invalid.", this);
            }
        }
        parse_number_of_pages(input);
        parse_author(input);
    }


}

class Magazine : Media {
    const int EDITOR = 3;
    string editor;
    public override void parse_array_to_var(string[] input) {
        base.parse_array_to_var(input);
        void parse_number_of_pages(string[] inputInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", inputInside), inputInside[EDITOR], "editor");
            try {
                editor = input[EDITOR];
            }
            catch {
                ErrorHandling error = new ErrorHandling("number of pages", lineNumber, String.Join(",", inputInside), 
                    "Editor field is invalid.", this);
            }
        }
        parse_number_of_pages(input);
    }


}

class Movie : Media {
    const int LENGTH_IN_MINUTES = 3, RELEASE_DATE = 4;
    int lengthInMinutes;
    DateTime releaseDate;
    public override void parse_array_to_var(string[] input) {
        base.parse_array_to_var(input);
        void parse_length_in_minutes(string[] inputInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", inputInside), inputInside[LENGTH_IN_MINUTES], "length in minutes", this);
            try {
                lengthInMinutes = Int32.Parse(input[LENGTH_IN_MINUTES]);
            }
            catch {
                ErrorHandling error = new ErrorHandling("number of pages", lineNumber, String.Join(",", inputInside), 
                    "Editor field is invalid.", this);
            }
        }
        void parse_release_date(string[] inputInside) {
            //ErrorHandling.error_missing_data(this, String.Join(",", inputInside), inputInside[RELEASE_DATE], "release date", this);
            try {
                releaseDate = DateTime.Parse(input[RELEASE_DATE]);
            }
            catch {
                ErrorHandling error = new ErrorHandling("number of pages", lineNumber, String.Join(",", inputInside), 
                    "Editor field is invalid.", this);
            }
        }
        parse_length_in_minutes(input);
        parse_release_date(input);       
    }
}
