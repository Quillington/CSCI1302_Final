using System;
using System.IO;

namespace FinalProject {

    public static class Program {

        const string LOG_DATA_NAME = "logdata.txt";

        public static void read_data() {
            // half code from class github
            const string fileName = "testcase.txt";
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

        static void create_obj(string input, int line) {

            bool create_obj_bool(string inputInside) {
                string[] tempArray = inputInside.Split(",");
                string type = tempArray[0];
                Media media;

                switch (type) {
                    case Media.BOOK:
                        media = new Book();
                        break;
                    case Media.MAGAZINE:
                        media = new Magazine();
                        break;
                    case Media.MOVIE:
                        media = new Movie();
                        break;
                    default:
                        return false;
                }
                media.lineNumber = line;
                Media.mediaStorage.Add(media);
                media.parse_array_to_var(tempArray);
                return true;
            }

            if (!create_obj_bool(input)) {
                ErrorHandling.error_create_and_add_list(
                            "Unknown", line, input, $"Type is invalid. " +
                            $"Must be either a book ({Media.BOOK}), magazine ({Media.MAGAZINE}), or movie ({Media.MOVIE}).");
            }
        }

        static void print_data_log() {
            if (File.Exists(LOG_DATA_NAME)) {
                File.Delete(LOG_DATA_NAME);
            }
            // pretty much stole this from the class github.
            StreamWriter writer = new StreamWriter(LOG_DATA_NAME);
            try {
                writer.WriteLine($"Newest Copyright Year is {Media.newest_copyright_year()}.");
                writer.WriteLine($"Oldest Copyright Year is {Media.oldest_copyright_year()}.");
                writer.WriteLine($"Number of Total Records is {Media.number_of_records()}.");
                writer.WriteLine($"Median Copyright Year is {Media.median_copyright_year()}.");
                writer.WriteLine($"Total Book Pages from Recorded Books is {Media.total_pages()}.");
            }
            catch {
                ErrorHandling.didLogDataFail = true;
            }
            finally {
                writer.Close();
            }

        }
        static void error_alert() {
            if (ErrorHandling.exceptionsList.Count > 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n\nIt is recommened you check the error log ({ErrorHandling.LOG_ERROR_NAME}) " +
                    $"which contains {ErrorHandling.exceptionsList.Count} error(s).\n" +
                    $"Any data with unparsable errors has been discarded.\n");
                Console.ResetColor();
            }
        }
        static void data_log_alert() {
            if (ErrorHandling.didLogDataFail) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("This information is unsaved due to a file saving error!\n");
                Console.ResetColor();
            }
            Console.Write($"This information is saved in the file {LOG_DATA_NAME}.");
        }
        public static void prettify_and_write() {
            // there's probably a better library for this but...

            // alignment is based on how long "Newest Copyright Year" is on the left 
            // and a big int on the right, so need to fix border and alignment 
            // if something longer is added.
            const string TOP_BOT_BORDER = "  +------------------------+---------+";
            const string STRING_FORMAT_HEADER = "{0, -34}";
            const string STRING_FORMAT_BODY = "  |{0, -24}|{1, -9}|";

            static void header_color(string text) {
                // found color stuff on stack overflow
                Console.Write("  |");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(String.Format(STRING_FORMAT_HEADER, text));
                Console.ResetColor();
                Console.WriteLine("|");
            }

            Console.WriteLine(TOP_BOT_BORDER);
            header_color("MANDATORY DATA");
            Console.WriteLine(TOP_BOT_BORDER);
            Console.WriteLine(String.Format(STRING_FORMAT_BODY, "Newest Copyright Year", Media.newest_copyright_year()));
            Console.WriteLine(String.Format(STRING_FORMAT_BODY, "Oldest Copyright Year", Media.oldest_copyright_year()));
            Console.WriteLine(String.Format(STRING_FORMAT_BODY, "Number of Records", Media.number_of_records()));
            Console.WriteLine(TOP_BOT_BORDER);
            header_color("OTHER FUN STUFF");
            Console.WriteLine(TOP_BOT_BORDER);
            Console.WriteLine(String.Format(STRING_FORMAT_BODY, "Median Copyright Year", Media.median_copyright_year()));
            Console.WriteLine(String.Format(STRING_FORMAT_BODY, "Total Book Pages", Media.total_pages()));
            Console.WriteLine(TOP_BOT_BORDER);
            data_log_alert();
            error_alert();
        }


        static void Main(string[] args) {
            read_data();
            prettify_and_write();
            print_data_log();
            ErrorHandling.log_error();
        }
    }
}

