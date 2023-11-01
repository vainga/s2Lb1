using System;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Lab1Sem2
{
    public class Chapters
    {
        private string name;
        public string Name
        {
            get { return name; }
            set {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Название главы не может быть пустым!");
                name = value;
            }
        }
        private int pages;
        public int Pages
        {
            get {return pages;}
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Количество страниц не может быть отрицательным!");
                pages = value;
            }
        }
        public Chapters()
        {
            name = "";
            pages = 0;
        }
        public Chapters(string name, int pages)
        {
            this.name = name;
            this.pages = pages;
        }
    }

    public class Book
    {
        public enum BookType
        {
            UNDEFINED,
            HARD_COVER,
            SOFT_COVER,
            EBOOK
        }

        private string Author;
        public string author
        {
            get { return Author; }
            set {
                if (!string.IsNullOrEmpty(value))
                {
                    Author = value;
                }
                else
                {
                    throw new ArgumentException("Имя автора не может быть пустым!");
                }

                for (int i = 0; i < value.Length; i++)
                {
                    if (char.IsDigit(value[i]))
                    {
                        throw new ArgumentException("Имя автора не может состоять из цифр!");
                    }
                }
            }
        }
        private string Name;
        public string name
        {
            get { return Name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Name = value;
                else
                    throw new Exception("Имя книги не может быть пустым!");
            }
        }
        private string ISBN;
        public string isbn
        {
            get { return ISBN; }
            set
            {
                if (value.Length != 13)
                    throw new ArgumentException("Некоректный ISBN!");
                foreach (char i in value) if (!char.IsDigit(i))
                        throw new ArgumentException("Некорректный ISBN!");
                ISBN = value;
            }
        }
        private string Publishing;
        public string publishing
        {
            get { return Publishing; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Название издательства не может быть пустым!");
                Publishing = value;
            }
        }
        private int Year;
        public int year
        {
            get { return Year; }
            set
            {
                if (value < 1563)
                    throw new ArgumentOutOfRangeException("Некорректное значение года издания!");
                Year = value;
            }
        }
        private BookType Type;
        public BookType type
        {
            get { return Type; }
            set
            {
                Type = value;
            }
        }
        private List<Chapters> Content;
        public List<Chapters> content
        {
            get
            {
                return Content;
            }

            set
            {
                Content = value.ToList();
            }
        }

        public Book()
        {
            Author = "";
            Name = "";
            Publishing = "";
            ISBN = "";
            Type = BookType.UNDEFINED;
            Content = new List<Chapters>();
            Year = 0;
        }

        public Book(string author, string name, string publishing, string iSBN, int year, BookType type, List<Chapters> content)
        {
            Author = author;
            Name = name;
            ISBN = iSBN;
            Publishing = publishing;
            Year = year;
            Type = type;
            Content = new List<Chapters>(content);
        }

        public Book(string author, string name, BookType type, List<Chapters> content)
        {
            Author = author;
            Name = name;
            ISBN = "";
            Publishing = "";
            Year = 0;
            Type = type;
            Content = new List<Chapters>(content);
        }

        public int getPAgesTotal()
        {
            int total_pages = 0;
            foreach (Chapters i in Content)
                total_pages += i.Pages;
            return total_pages;
        }

        public void setContent()
        {
            string s;
            short N;
            string name;
            int pages;
            Chapters ch;

            Console.Write("Введите количество глав: ");
            N = short.Parse(Console.ReadLine());

            if (N < 0)
            {
                throw new Exception("Количество глав не может быть отрицательным");
            }

            for (int i = 0; i < N; i++)
            {
                try
                {
                    ch = new Chapters();
                    Console.Write("Введите название главы: ");
                    name = Console.ReadLine();
                    foreach (Chapters c in Content)
                    {
                        if (c.Name == name)
                        {
                            throw new Exception("Названия глав не могут повторяться!");
                        }
                    }
                    //ch
                    ch.Name = name;

                    Console.WriteLine("Введите количество страниц в главе: ");
                    pages = int.Parse(Console.ReadLine());

                    if (pages <= 0 || pages > 65535)
                        throw new Exception("Некорректное количество страниц!");

                    if (Content.Count != 0 && pages == 0)
                        throw new Exception("Количество страниц не может быть меньше количества глав!");

                    ch.Pages = pages;
                    Content.Add(ch);
                }

                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    i--;
                    continue;
                }
            }

        }

        public double PerChapter()
        {
            int total_pages = getPAgesTotal();
            if (total_pages != 0)
                return total_pages / Content.Count;
            else return 0;
        }

        public short Publish(string pubName, int pubYear, string newISBN)
        {
            if (!string.IsNullOrEmpty(publishing))
            {
                Console.WriteLine("Книга уже была издана!");
                return 0;
            }
            try
            {
                publishing = pubName;
                isbn = newISBN;
                year = pubYear;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return -1;
            }
            return 0;
        }


        public void EnterInfo()
        {
            bool f = true;
            char choice;
            string author, name;
            List<Chapters> Content;

            int k = 0;
            string nN;
            int nP;

            if (!string.IsNullOrEmpty(this.author))
            {
                Console.WriteLine("Данные уже были введены!");
                return;
            }

            while (f)
            {
                try
                {
                    Console.WriteLine("Введите автора:");
                    author = Console.ReadLine();
                    this.author = author;
                    Console.WriteLine("Введите название:");
                    name = Console.ReadLine();
                    this.name = name;
                    Console.WriteLine("Выберите тип книги: \n1)Твердая обложка\n2)Мягкая обложка\n3)Электронная\nВыбор: ");
                    choice = char.Parse(Console.ReadLine());
                    if (choice < '1' || choice > '3')
                        throw new Exception("Некорректное значение типа!");
                    switch (choice)
                    {
                        case '1':
                            type = BookType.HARD_COVER;
                            break;
                        case '2':
                            type = BookType.SOFT_COVER;
                            break;
                        case '3':
                            type = BookType.EBOOK;
                            break;
                    }
                    setContent();
                    Console.WriteLine("Издать книгу? Y/N");
                    choice = char.Parse(Console.ReadLine());
                    if (choice == 'Y')
                        EnterPublishing();
                    else if (choice == 'N')
                        f = false;
                    else throw new Exception("Некорректное значение!");
                    Console.WriteLine("Желаете ли вы удалить какую-либо главу? Y/N:");
                    choice = char.Parse(Console.ReadLine());
                    if (choice == 'Y')
                    {
                        Console.WriteLine("Введите номер главы:");
                        k = int.Parse(Console.ReadLine());
                        del(k - 1);
                    }
                    else if (choice == 'N')
                        f = false;
                    else throw new Exception("Некорректное значение!");
                    Console.WriteLine("Желаете ли вы редактировать какую-либо главу? Y/N");
                    choice = char.Parse(Console.ReadLine());
                    if (choice == 'Y')
                    {
                        Console.Write("Введите номер главы: ");
                        k = int.Parse(Console.ReadLine());
                        Console.Write("Введите новое название: ");
                        nN = Console.ReadLine();
                        Console.Write("Введите новое кол-во страниц: ");
                        nP = int.Parse(Console.ReadLine());
                        edit(k - 1, nN, nP);
                    }
                    else if (choice == 'N')
                        f = false;
                    else throw new Exception("Некорректное значение!");
                    Console.WriteLine("Желаете ли вы добавить новую главу? Y/N");
                    choice = char.Parse(Console.ReadLine());
                    if (choice == 'Y')
                    {
                        Console.Write("Введите название: ");
                        nN = Console.ReadLine();
                        Console.Write("Введите кол-во страниц: ");
                        nP = int.Parse(Console.ReadLine());
                        newElement(nN, nP);
                    }
                    else if (choice == 'N') f = false;
                    else throw new Exception("Некорректное значение!");
                    f = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    f = true;
                }
            }
        }

        public void PrintInfo()
        {
            if (Author.Length == 0)
            {
                Console.WriteLine("Значения не заданы!");
            }
            else if (!(Publishing.Length == 0))
            {
                Console.WriteLine("Информация о книге:");
                Console.WriteLine($"Автор: {Author}");
                Console.WriteLine($"Название: {Name}");
                Console.WriteLine($"Издательство: {Publishing}");
                Console.WriteLine($"ISBN: {ISBN}");
                Console.WriteLine($"Количество глав: {Content.Count}");
                Console.WriteLine($"Список глав:");
                foreach (Chapters i in Content)
                    Console.WriteLine($"{i.Name}: {i.Pages} страниц");
                Console.WriteLine($"Количество страниц: {getPAgesTotal()}");
                Console.WriteLine($"Среднее количество страниц в главе: {PerChapter()}");
                Console.WriteLine($"Год издания: {Year}");
                Console.WriteLine($"Тип: {ToString(Type)}");
            }
            else
            {
                Console.WriteLine("Информация о книге:");
                Console.WriteLine($"Автор: {Author}");
                Console.WriteLine($"Название: {Name}");
                Console.WriteLine($"Количество глав: {Content.Count}");
                Console.WriteLine($"Список глав:");
                foreach (Chapters i in Content)
                    Console.WriteLine($"{i.Name}: {i.Pages} страниц");
                Console.WriteLine($"Количество страниц: {getPAgesTotal()}");
                Console.WriteLine($"Среднее количество страниц в главе: {PerChapter()}");
                Console.WriteLine($"Тип: {ToString(Type)}");
            }
        }

        public void EnterPublishing()
        {
            bool f = true;
            int em = 0;
            if (!string.IsNullOrEmpty(publishing))
            {
                Console.WriteLine("Книга уже была издана!");
                return;
            }
            while (f)
            {
                try
                {
                    string pubsh, ISBN, type;
                    int year;
                    Console.WriteLine("Введите издательство:");
                    pubsh = Console.ReadLine();
                    Console.WriteLine("Введите год издания:");
                    year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите ISBN (в формате 9785XXXXXXXXX");
                    ISBN = Console.ReadLine();
                    if (Publish(pubsh, year, ISBN) == -1)
                        f = true;
                    else f = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    publishing = "";
                    year = 0;
                    isbn = "";
                    f = true;
                }
            }
        }

        public string ToString(BookType type)
        {
            switch (type)
            {
                case BookType.HARD_COVER:
                    return "Твердая обложка";
                case BookType.SOFT_COVER:
                    return "Мягкая обложка";
                case BookType.EBOOK:
                    return "Электронная";
                default:
                    return "Неизвестно";
            }

        }
        public void del(int k)
        {
            Content.RemoveAt(k);
        }
        public void edit(int index, string newName, int newPages)
        {
            Chapters ch;
            ch = new Chapters();
            ch.Name = newName;
            ch.Pages = newPages;
            Content[index] = ch;
        }
        public void newElement(string newName, int newPages)
        {
            Chapters ch;
            ch = new Chapters();
            ch.Name = newName;
            ch.Pages = newPages;
            Content.Add(ch);
        }
        public void printVec()
        {
            foreach (Chapters i in Content)
            {
                Console.WriteLine($"{i.Name}: {i.Pages} страниц;");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //Book b4 = new Book();
            //b4.EnterInfo();
            List<Chapters> chapters = new List<Chapters>();
            Chapters ch1 = new Chapters("Начало 1", 50);
            Chapters ch2 = new Chapters("Середина 2", 150);
            Chapters ch3 = new Chapters("Конец 3", 70);
            chapters.Add(ch1);
            chapters.Add(ch2);
            chapters.Add(ch3);
            Book Testbook = new Book("Иванов Иван", "Моя первая книга", Book.BookType.EBOOK, chapters);
            Testbook.PrintInfo();
            Console.WriteLine("///////////////////////////////////////////////////////////////////");
            Testbook.Publish("ACT", 2010, "9785999999999");
            Testbook.PrintInfo();
            Console.WriteLine("///////////////////////////////////////////////////////////////////");
            Testbook.del(2);
            Testbook.edit(1, "Новая середина", 100);
            Testbook.newElement("Конец 3", 50);
            Testbook.PrintInfo();
            Console.WriteLine("///////////////////////////////////////////////////////////////////");
        }
    }
}
