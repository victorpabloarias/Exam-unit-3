using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_unit_3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Functions are popping
            Console.WriteLine(Square(10.5));
            Console.WriteLine(InchesToMillimeters(5));
            Console.WriteLine(Root(9));
            Console.WriteLine(Cube(5));
            Console.WriteLine(CircleArea(5));
            Console.WriteLine(Greet("Victor!"));
            #endregion

            #region Flatten those numbers
            string jsonPath = @"D:\Software\Dev\Classgap\VictorAssignment\example_files\arrays.json"; //Adjust to local
            string jsonString = File.ReadAllText(jsonPath);
            List<object> array = JsonConvert.DeserializeObject<List<object>>(jsonString);
            List<int> flattenedArray = FlattenArray(array);
            Console.WriteLine("[{0}]", string.Join(", ", flattenedArray));
            #endregion

            #region Left and right up and down, away we go.
            string nodesPath = @"D:\Software\Dev\Classgap\VictorAssignment\example_files\nodes.json"; //Adjust to local
            string nodesString = File.ReadAllText(nodesPath);
            Node nodes = JsonConvert.DeserializeObject<Node>(nodesString);


            Dictionary<string, int> nodeStats = AnalyzeNodes(nodes);
            Console.WriteLine($"Sum = {nodeStats["Sum"]}");
            Console.WriteLine($"Deepest Level = {nodeStats["DeepestLevel"]}");
            Console.WriteLine($"Number of nodes = {nodeStats["Nodes"]}");

            #endregion

            #region My books they are a mess.
            string booksPath = @"D:\Software\Dev\Classgap\VictorAssignment\example_files\books.json"; //Adjust to local
            string booksString = File.ReadAllText(booksPath);
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(booksString);


            var booksStartingWithThe = FilterStartWord(books, "The");
            Console.WriteLine("Books starting with 'The':");
            printAllBooks(booksStartingWithThe);

            var booksByAuthorsWithTInName = FilterAuthorLastName(books, 't');
            Console.WriteLine("Books written by authors with a 't' in their name");
            printAllBooks(booksByAuthorsWithTInName);

            var booksWrittenAfter1992 = FilterBooksAfterYear(books, 1992);
            Console.WriteLine("Books written after 1992");
            printAllBooks(booksWrittenAfter1992);

            var booksWrittenBefore2004 = FilterBooksBeforeYear(books, 2004);
            Console.WriteLine("Books written before 2004");
            printAllBooks(booksWrittenBefore2004);

            string author = "Terry Pratchett";
            var isbnNumbersByAuthor = GetIsbnNumbersByAuthor(books, author);
            Console.WriteLine($"ISBN numbers from author: {author}");
            foreach (string isbn in isbnNumbersByAuthor)
            {
                Console.WriteLine(isbn);
            }

            var sortByAscending = true;
            var sortedBooksAlphabetically = SortBooksAlphabetically(books, sortByAscending);
            Console.WriteLine("Books alphabetically sorted: ");
            printAllBooks(sortedBooksAlphabetically);

            var sortedBooksChronologically = SortBooksChronologically(books, sortByAscending);
            Console.WriteLine("Books chronologically sorted: ");
            printAllBooks(sortedBooksChronologically);

            var booksGroupedByLastName = GroupBooksByAuthorLastName(books);
            foreach (var kvp in booksGroupedByLastName)
            {
                Console.WriteLine($"\nAuthor last name: {kvp.Key}");
                foreach (var book in kvp.Value)
                {
                    Console.WriteLine($"Title: {book.Title}, Publication Year: {book.Publication_year}, Author: {book.Author}");
                }
            }


            var booksGroupedByFirstName = GroupBooksByAuthorFirstName(books);
            foreach (var kvp in booksGroupedByFirstName)
            {
                Console.WriteLine($"\nAuthor first name: {kvp.Key}");
                foreach (var book in kvp.Value)
                {
                    Console.WriteLine($"Title: {book.Title}, Publication Year: {book.Publication_year}, Author: {book.Author}");
                }
            }

            #endregion


            Console.ReadKey();
        }


        #region Functions are popping (func)
        public static double Square(double num)
        {
            return num * num;
        }

        public static double InchesToMillimeters(int inches)
        {
            return inches * 25.4;
        }
        public static double Root(double num)
        {
            return Math.Sqrt(num);
        }

        public static int Cube(int num)
        {
            return num * num * num;
        }

        public static double CircleArea(double radius)
        {
            return Math.PI * radius * radius;
        }

        public static string Greet(string name)
        {
            return "Hello, " + name + "!";
        }
        #endregion

        #region Flatten those numbers (func)
        public static List<int> FlattenArray(List<object> array)
        {
            List<int> result = new List<int>();
            FlattenHelper(array, result);
            return result;
        }

        private static void FlattenHelper(List<object> array, List<int> result)
        {
            foreach (var item in array)
            {
                if (item is int)
                {
                    result.Add((int)item);
                }
                else if (item is List<object>)
                {
                    FlattenHelper((List<object>)item, result);
                }
            }
        }
        #endregion

        #region Left and right up and down, away we go. (func)

        public static Dictionary<string, int> AnalyzeNodes(Node node)
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            int sum = CalculateSum(node);
            int deepestLevel = CalculateDeepestLevel(node);
            int nodesCount = CountNodes(node);

            results.Add("Sum", sum);
            results.Add("DeepestLevel", deepestLevel);
            results.Add("Nodes", nodesCount);

            return results;
        }

        public static int CalculateSum(Node node)
        {
            if (node == null)
                return 0;

            return node.value + CalculateSum(node.left) + CalculateSum(node.right);
        }

        public static int CalculateDeepestLevel(Node node)
        {
            if (node == null)
                return 0;

            int leftDepth = CalculateDeepestLevel(node.left);
            int rightDepth = CalculateDeepestLevel(node.right);

            return Math.Max(leftDepth, rightDepth) + 1;
        }

        public static int CountNodes(Node node)
        {
            if (node == null)
                return 0;

            return 1 + CountNodes(node.left) + CountNodes(node.right);
        }

        #endregion

        #region My books they are a mess. (func)

        public static List<Book> FilterStartWord(List<Book> books, string searchQuery)
        {
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Title.StartsWith(searchQuery))
                {
                    result.Add(book);
                }
            }
            return result;
        }

        public static List<Book> FilterAuthorLastName(List<Book> books, char query)
        {
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Author.ToLower().Contains(query))
                {
                    result.Add(book);
                }
            }
            return result;
        }

        public static List<Book> FilterBooksAfterYear(List<Book> books, int year)
        {
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Publication_year > year)
                {
                    result.Add(book);
                }
            }
            return result;
        }

        public static List<Book> FilterBooksBeforeYear(List<Book> books, int year)
        {
            List<Book> result = new List<Book>();
            foreach (var book in books)
            {
                if (book.Publication_year < year)
                {
                    result.Add(book);
                }
            }
            return result;
        }

        public static List<string> GetIsbnNumbersByAuthor(List<Book> books, string authorName)
        {
            List<string> result = new List<string>();
            foreach (var book in books)
            {
                if (book.Author == authorName)
                {
                    result.Add(book.Isbn);
                }
            }
            return result;
        }

        public static List<Book> SortBooksAlphabetically(List<Book> books, bool ascending)
        {
            if (ascending)
            {
                books.Sort((book1, book2) => string.Compare(book1.Title, book2.Title));
            }
            else
            {
                books.Sort((book1, book2) => string.Compare(book2.Title, book1.Title));
            }
            return books;
        }

        public static List<Book> SortBooksChronologically(List<Book> books, bool ascending)
        {
            if (ascending)
            {
                books.Sort((book1, book2) => book1.Publication_year.CompareTo(book2.Publication_year));
            }
            else
            {
                books.Sort((book1, book2) => book2.Publication_year.CompareTo(book1.Publication_year));
            }
            return books;
        }

        public static Dictionary<string, List<Book>> GroupBooksByAuthorLastName(List<Book> books)
        {
            Dictionary<string, List<Book>> result = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                string[] nameParts = book.Author.Split();
                string lastName = nameParts[nameParts.Length - 1];
                if (!result.ContainsKey(lastName))
                {
                    result[lastName] = new List<Book>();
                }
                result[lastName].Add(book);
            }
            return result;
        }

        public static Dictionary<string, List<Book>> GroupBooksByAuthorFirstName(List<Book> books)
        {
            Dictionary<string, List<Book>> result = new Dictionary<string, List<Book>>();
            foreach (var book in books)
            {
                string[] nameParts = book.Author.Split();
                string firstName = nameParts[0];
                if (!result.ContainsKey(firstName))
                {
                    result[firstName] = new List<Book>();
                }
                result[firstName].Add(book);
            }
            return result;
        }

        public static void printAllBooks(List<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Publication Year: {book.Publication_year}, Author: {book.Author}, ISBN: {book.Isbn}");
            }
        }

        #endregion

    }
}
