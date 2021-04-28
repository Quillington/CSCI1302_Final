using System;
using System.IO;
using System.Collections.Generic;

namespace FinalProject {

    public class Media {

        const int TYPE = 0, TITLE = 1, COPYRIGHT_YEAR = 2;
        public const string BOOK = "1", MAGAZINE = "2", MOVIE = "3";
        public string type;
        public string title;
        public int copyrightYear;
        public static LinkedList<Media> mediaStorage = new LinkedList<Media>();
        //this linked list stores all the Media objects so functions can be called at will
        //as opposed to just when they are read in the file

        public virtual void parse_array_to_var(string[] input) {
            switch (input[TYPE]) {
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
            title = input[TITLE];
            copyrightYear = Int32.Parse(input[COPYRIGHT_YEAR]);

        }
        public static void read_data() {
            //code from class github
            string fileName = "testcase.txt";
            StreamReader streamReader = new StreamReader(fileName);
            string inputLine = streamReader.ReadLine();
            while (null != inputLine) {
                Program.to_array(inputLine);
                inputLine = streamReader.ReadLine();
            }
        }

        public static void test_parse() {
            //all error stuff goes here, test wrapper for parse_array_to_var
        }

        public static int number_of_records() {
            int count = 0;
            foreach(var record in mediaStorage) {
                count += 1;
            }
            return count;
        }

        public static int oldest_copyright_year() {
            int oldest = 0;
            foreach(var record in mediaStorage) {
                if (record.copyrightYear > oldest) {
                    oldest = record.copyrightYear;
                }
            }
            return oldest;
        }

        public static int newest_copyright_year() {
            int youngest = Int32.MaxValue;
            foreach(var record in mediaStorage) {
                if (record.copyrightYear < youngest) {
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
            numberOfPages = Int32.Parse(input[NUMBER_OF_PAGES]);
            author = input[AUTHOR];
        }


    }

    class Magazine : Media {
        const int EDITOR = 3;
        string editor;
        public override void parse_array_to_var(string[] input) {
            base.parse_array_to_var(input);
            editor = input[EDITOR];
        }


    }

    class Movie : Media {
        const int LENGTH_IN_MINUTES = 3, RELEASE_DATE = 4;
        int lengthInMinutes;
        DateTime releaseDate;
        public override void parse_array_to_var(string[] input) {
            base.parse_array_to_var(input);
            lengthInMinutes = Int32.Parse(input[LENGTH_IN_MINUTES]);
            releaseDate = DateTime.Parse(input[RELEASE_DATE]);
        }
    }



    public static class Program {
        

        public static void to_array(string input) {
            ///This method takes the read string and parses it a string arrray. 
            ///It also passes the data to its type specific class

            string[] tempArray = input.Split(",");
            try {
                string title_of_media = tempArray[0];
                switch (title_of_media) {
                    case Media.BOOK:
                        Book bookObj = new Book();
                        bookObj.parse_array_to_var(tempArray);
                        Media.mediaStorage.AddLast(bookObj);
                        break;
                    case Media.MAGAZINE:
                        Magazine magObj = new Magazine();
                        magObj.parse_array_to_var(tempArray);
                        Media.mediaStorage.AddLast(magObj);
                        break;
                    case Media.MOVIE:
                        Movie movObj = new Movie();
                        movObj.parse_array_to_var(tempArray);
                        Media.mediaStorage.AddLast(movObj);
                        break;
                }//might change parse to error stuff that includes parse inside it
            }
            catch {
                //log 
            }

        }

        public static void parse_overhead() {

        }


        static void Main(string[] args) {
            Media.read_data();
            Console.WriteLine(Media.newest_copyright_year());
            Console.WriteLine(Media.oldest_copyright_year());
            Console.WriteLine(Media.number_of_records());

            
        }
    }
}
