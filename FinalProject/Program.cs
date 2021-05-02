using System;
using System.IO;
using System.Collections.Generic;

namespace FinalProject {
    public class ErrorHandling {
        private int _lineNumber;
        private string _type;
        private string _badString;
        private string _reasonForError;
        public LinkedList<ErrorHandling> exceptionsList = new LinkedList<ErrorHandling>();
        public ErrorHandling(string type, int lineNumber, string badString, string reasonForError) {
            _type = type;
            _lineNumber = lineNumber;
            _badString = badString;
            _reasonForError = reasonForError;
            exceptionsList.AddLast(this);
            this.log_error();
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

        public virtual void parse_array_to_var(string[] parsestring, int lineCount) {
            ErrorHandling error = null;   
            switch (parsestring[TYPE]) {
                case BOOK:
                    type = "Book";
                    break;
                case MAGAZINE:
                    type = "Magazine";
                    break;
                case MOVIE:
                    type = "Movie";
                    break;
                default:
                    error = new ErrorHandling(
                            "undefined", lineCount, String.Join(",", parsestring), 
                            $"Type is invalid. Must be either a book ({BOOK}), movie ({MOVIE}), or magazine ({MAGAZINE}).");
                    break;
                }
                

            title = parsestring[TITLE];
            copyrightYear = Int32.Parse(parsestring[COPYRIGHT_YEAR]);
        }


        public static void read_data() {
            //code from class github
            string fileName = "testcase.txt";
            StreamReader streamReader = new StreamReader(fileName);
            string inputLine = streamReader.ReadLine();
            int lineCount = 1;
            while (null != inputLine) {
                if (inputLine == "") {
                    inputLine = streamReader.ReadLine();
                    lineCount += 1;
                    continue;
                }
                Program.to_array(inputLine, lineCount);
                inputLine = streamReader.ReadLine();
                lineCount += 1;
            }
        }

        public static int number_of_records() {
            int count = 0;
            foreach(var record in mediaStorage) {
                count += 1;
            }
            return count;
        }

        public static int oldest_copyright_year() {
            int oldest = Int32.MaxValue;
            foreach(var record in mediaStorage) {
                if (record.copyrightYear < oldest) {
                    oldest = record.copyrightYear;
                }
            }
            return oldest;
        }

        public static int newest_copyright_year() {
            int youngest = 0;
            foreach(var record in mediaStorage) {
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

        public override void parse_array_to_var(string[] input, int lineCount = 0) {
            base.parse_array_to_var(input, this.lineNumber);
            numberOfPages = Int32.Parse(input[NUMBER_OF_PAGES]);
            author = input[AUTHOR];
        }


    }

    class Magazine : Media {
        const int EDITOR = 3;
        string editor;
        public override void parse_array_to_var(string[] input, int lineCount = 0) {
            base.parse_array_to_var(input, this.lineNumber);
            editor = input[EDITOR];
        }


    }

    class Movie : Media {
        const int LENGTH_IN_MINUTES = 3, RELEASE_DATE = 4;
        int lengthInMinutes;
        DateTime releaseDate;
        public override void parse_array_to_var(string[] input, int lineCount = 0) {
            base.parse_array_to_var(input, this.lineNumber);
            lengthInMinutes = Int32.Parse(input[LENGTH_IN_MINUTES]);
            releaseDate = DateTime.Parse(input[RELEASE_DATE]);
        }
    }



    public static class Program {
        

        public static void to_array(string input, int line) {
            ///This method takes the read string and parses it a string arrray. 
            ///It also passes the data to its type specific class

            string[] tempArray = input.Split(",");
            try {
                string title_of_media = tempArray[0];
                switch (title_of_media) {
                    case Media.BOOK:
                        Book bookObj = new Book();
                        bookObj.lineNumber = line;
                        bookObj.parse_array_to_var(tempArray, line);
                        Media.mediaStorage.AddLast(bookObj);
                        break;
                    case Media.MAGAZINE:
                        Magazine magObj = new Magazine();
                        magObj.lineNumber = line;
                        magObj.parse_array_to_var(tempArray, line);
                        Media.mediaStorage.AddLast(magObj);
                        break;
                    case Media.MOVIE:
                        Movie movObj = new Movie();
                        movObj.lineNumber = line;
                        movObj.parse_array_to_var(tempArray, line);
                        Media.mediaStorage.AddLast(movObj);
                        break;
                }
            }
            catch {
                ErrorHandling error = new ErrorHandling("Type", line, "x", "y");
            }

        }


        static void Main(string[] args) {
            Media.read_data();
            Console.WriteLine(Media.newest_copyright_year());
            Console.WriteLine(Media.oldest_copyright_year());
            Console.WriteLine(Media.number_of_records());

            
        }
    }
}
