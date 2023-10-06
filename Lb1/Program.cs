using System;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

//Модульные тесты 

namespace Lb1
{
    public class Chapters
    {
        private string name;
        private int pages;
        public Chapters()
        {
            this.name = "";
            this.pages = 0;
        }
        public Chapters(string name, int pages)
        {
            this.name = name;
            this.pages = pages;
        }
        public int getPages()
        {
            return pages;
        }
        public string getName()
        {
            return name;
        }
        public void setName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException("Название главы не может быть пустым!");
            this.name = newName;
        }
        public void setPages(int newPages)
        {
            if (newPages < 0) throw new ArgumentOutOfRangeException("Количество страниц не может быть отрицательным!");
            this.pages = newPages;
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
        private string Name;
        private string ISBN;
        private string Publishing;
        private int Year;
        private BookType Type;
        private List<Chapters> Content;

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
            Type = type;
            Content = new List<Chapters>(content);
        }

        public string getAuthor()
        {
            return Author;
        }

        public string getName()
        {
            return Name;
        }

        public string getISBN()
        {
            return ISBN;
        }

        public int getYear()
        {
            return Year;
        }

        public string getPubHouse()
        {
            return Publishing;
        }

        public List<Chapters> getContent()
        {
            return Content;
        }


        public BookType getType()
        {
            return Type;
        }

        public int getPAgesTotal()
        {
            int total_pages = 0;
            foreach (Chapters i in Content)
                total_pages += i.getPages();
            return total_pages;
        }

        public void setPubHouse(string newPubHouse) 
        {
            if (string.IsNullOrEmpty(newPubHouse))
                throw new ArgumentNullException("Название издательства не может быть пустым!");
            Publishing = newPubHouse;
        }

        public void setYear(int newYear) 
        {
            if (newYear < 1563)
                throw new ArgumentOutOfRangeException("Некорректное значение года издания!");
            Year = newYear;
        }

        public void setISBT(string newISBN)
        {
            if (newISBN.Length != 13)
                throw new ArgumentException("Некоректный ISBN!");
            foreach (char i in newISBN) if (!char.IsDigit(i))
                    throw new ArgumentException("Некорректный ISBN!");
            ISBN = newISBN;
        }
      

        public void SetAuthor(string newAuthor)
        {
            if (!string.IsNullOrEmpty(newAuthor))
            {
                this.Author = newAuthor;
            }
            else
            {
                throw new ArgumentException("Имя автора не может быть пустым!");
            }

            for (int i = 0; i < newAuthor.Length; i++)
            {
                if (char.IsDigit(newAuthor[i]))
                {
                    throw new ArgumentException("Имя автора не может состоять из цифр!");
                }
            }
        }

       
        public void setName(string newName)
        {
            if(!string.IsNullOrEmpty(newName))
                    this.Name = newName;
            else
                throw new Exception("Имя книги не может быть пустым!");
        }

        public void setContent()
        {
            string s;
            short N;
            string name;
            int pages;
            Chapters ch;
            ch = new Chapters();

            Console.Write("Введите количество глав: "); 
            N = short.Parse(Console.ReadLine());
            
            if(N<0)
            {
                throw new Exception("Количество глав не может быть отрицательным");
            }

            for(int i = 0; i < N; i++)
            {
                try
                {
                    Console.Write("Введите название главы: ");
                    name = Console.ReadLine();
                    foreach (Chapters c in this.Content)
                    {
                        if (c.getName() == name)
                        {
                            throw new Exception("Названия глав не могут повторяться!");
                        }
                    }
                      
                    ch.setName(name);

                    Console.WriteLine("Введите количество страниц в главе: ");
                    pages = int.Parse(Console.ReadLine());

                    if (Content.Count != 0 && pages == 0)
                        throw new Exception("Количество страниц не может быть меньше количества глав!");

                    ch.setPages(pages);
                    Content.Add(ch);
                }

                catch(Exception err)
                {
                    Console.WriteLine(err.Message);
                    i--;
                    continue;
                }  
            }

        }

        public void setContent(List<Chapters> content)
        {
            Content = content.ToList();
        }

        public void setType(BookType newType) 
        {
            Type = newType;
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
            if(!string.IsNullOrEmpty(getPubHouse()))
            {
                Console.WriteLine("Книга уже была издана!");
                return 0;
            }
            try
            {
                setPubHouse(pubName);
                setISBT(newISBN);
                setYear(pubYear);
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

            if(!string.IsNullOrEmpty(getAuthor()))
            {
                Console.WriteLine("Данные уже были введены!");
                return;
            }

            while(f)
            {
                try
                {
                    Console.WriteLine("Введите автора:");
                    author = Console.ReadLine();
                    SetAuthor(author);
                    Console.WriteLine("Введите название:");
                    name = Console.ReadLine();
                    setName(name);
                    Console.WriteLine("Выберите тип книги: \n1)Твердая обложка\n2)Мягкая обложка\n3)Электронная\nВыбор: ");
                    choice = char.Parse(Console.ReadLine());
                    if (choice < '1' || choice > '3')
                        throw new Exception("Некорректное значение типа!");
                    switch (choice)
                    {
                        case '1':
                            setType(BookType.HARD_COVER);
                            break;
                        case '2':
                            setType(BookType.SOFT_COVER);
                            break;
                        case '3':
                            setType(BookType.EBOOK);
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
                catch(Exception e)
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
                    Console.WriteLine($"{i.getName()}: {i.getPages()} страниц");
                Console.WriteLine($"Количество страниц: {getPAgesTotal()}");
                Console.WriteLine($"Среднеее количество страниц в главе: {PerChapter()}");
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
                    Console.WriteLine($"{i.getName()}: {i.getPages()} страниц");
                Console.WriteLine($"Количество страниц: {getPAgesTotal()}");
                Console.WriteLine($"Среднеее количество страниц в главе: {PerChapter()}");
                Console.WriteLine($"Тип: {ToString(Type)}");
            }
        }

        public void EnterPublishing()
        {
            bool f = true;
            int em = 0;
            if (!string.IsNullOrEmpty(getPubHouse()))
            {
                Console.WriteLine("Книга уже была издана!");
                return;
            }
            while(f)
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
            ch.setName(newName);
            ch.setPages(newPages);
            Content[index]  = ch;
        }
        public void newElement(string newName, int newPages)
        {
            Chapters ch;
            ch =new Chapters();
            ch.setName(newName);
            ch.setPages(newPages);
            Content.Add(ch);
        }
        public void printVec()
        {
            foreach(Chapters i in Content)
            {
                Console.WriteLine($"{i.getName()}: {i.getPages()} страниц;");
            }
        }
    }   


    class Program
    {
        static void Main(string[] args)
        {
            Book b4 = new Book();
            b4.EnterInfo();
            Console.WriteLine();
            Console.WriteLine();
            b4.PrintInfo();
        }
    }
}