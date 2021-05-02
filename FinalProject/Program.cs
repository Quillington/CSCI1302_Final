using System;
using System.IO;
using System.Collections.Generic;

namespace FinalProject {

    public static class Program {

        public static void read_data() {
            //half code from class github
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
                create_obj(inputLine, lineCount);
                inputLine = streamReader.ReadLine();
                lineCount += 1;
            }
        }

        public static void create_obj(string input, int line) {
            ///This method is a wrapper function because I can't use default
            ///in order to do error stuff.
            bool create_obj_bool(string inputInside) { 
            
                string[] tempArray = inputInside.Split(",");
                string title_of_media = tempArray[0];
                switch (title_of_media) {
                    case Media.BOOK:
                        Book bookObj = new Book();
                        bookObj.lineNumber = line;
                        Media.mediaStorage.AddLast(bookObj);
                        bookObj.parse_array_to_var(tempArray);
                        break;
                    case Media.MAGAZINE:
                        Magazine magObj = new Magazine();
                        magObj.lineNumber = line;
                        magObj.parse_array_to_var(tempArray);
                        Media.mediaStorage.AddLast(magObj);
                        break;
                    case Media.MOVIE:
                        Movie movObj = new Movie();
                        movObj.lineNumber = line; 
                        movObj.parse_array_to_var(tempArray);
                        Media.mediaStorage.AddLast(movObj);
                        break;
                    default:
                        return false;
                }
                return true;
            }
            if (!create_obj_bool(input)) {
                ErrorHandling error = new ErrorHandling(
                            "Unknown", line, input, $"Type is invalid. " +
                            $"Must be either a book ({Media.BOOK}), magazine ({Media.MAGAZINE}), or movie ({Media.MOVIE}).");
            }
            else {
            }
        }


        static void Main(string[] args) {
            read_data();
            Console.WriteLine(Media.newest_copyright_year());
            Console.WriteLine(Media.oldest_copyright_year());
            Console.WriteLine(Media.number_of_records());

            
        }
    }
}
