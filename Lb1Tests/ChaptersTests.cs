using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1Sem2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using static Lab1Sem2.Book;
using System.Reflection.Metadata;


namespace Lab1Sem2Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void PrintInfo_PrintsInfoCorrectly()
        {
            // Arrange
            Book book = new Book();
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                book.author = "Author Name";
                book.name = "Book Title";
                book.content = new List<Chapters>
            {
                new Chapters { Name = "Chapter 1", Pages = 10 },
                new Chapters { Name = "Chapter 2", Pages = 15 }
            };

                book.PrintInfo();

                string consoleOutput = stringWriter.ToString();
                Assert.IsTrue(consoleOutput.Contains("Автор: Author Name"));
                Assert.IsTrue(consoleOutput.Contains("Название: Book Title"));
                Assert.IsTrue(consoleOutput.Contains("Количество глав: 2"));
                Assert.IsTrue(consoleOutput.Contains("Chapter 1: 10 страниц"));
                Assert.IsTrue(consoleOutput.Contains("Chapter 2: 15 страниц"));
            }
        }

        [TestMethod()]
        public void EnterPublishing_ValidInput()
        {
            Book book = new Book();
            using (StringReader stringReader = new StringReader("Publishing Name\n2023\n9785123456789\n"))
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetIn(stringReader);
                Console.SetOut(stringWriter);

                book.EnterPublishing();

                Assert.AreEqual("Publishing Name", book.publishing);
                string consoleOutput = stringWriter.ToString();
                Assert.IsTrue(consoleOutput.Contains("Введите издательство:"));
                Assert.IsTrue(consoleOutput.Contains("Введите год издания:"));
                Assert.IsTrue(consoleOutput.Contains("Введите ISBN (в формате 9785XXXXXXXXX"));
            }
        }

        [TestMethod()]
        public void del_Test()
        {
            Book book = new Book();
            book.content = new List<Chapters>
        {
            new Chapters { Name = "Chapter 1", Pages = 10 },
            new Chapters { Name = "Chapter 2", Pages = 15 },
            new Chapters { Name = "Chapter 3", Pages = 20 }
        };

            int indexToDelete = 1;

            book.del(indexToDelete);

            Assert.AreEqual(2, book.content.Count);
            Assert.AreEqual("Chapter 1", book.content[0].Name);
            Assert.AreEqual("Chapter 3", book.content[1].Name);
        }

        [TestMethod()]
        public void edit_Test()
        {
            Book book = new Book();
            book.content = new List<Chapters>
        {
            new Chapters { Name = "Chapter 1", Pages = 10 },
            new Chapters { Name = "Chapter 2", Pages = 15 },
            new Chapters { Name = "Chapter 3", Pages = 20 }
        };

            int indexToEdit = 1;
            string newName = "Modified Chapter";
            int newPages = 25;

            book.edit(indexToEdit, newName, newPages);

            Chapters editedChapter = book.content[indexToEdit];
            Assert.AreEqual(newName, editedChapter.Name); 
            Assert.AreEqual(newPages, editedChapter.Pages);
        }

        [TestMethod()]
        public void newElement_Test()
        {
            Book book = new Book();
            book.content = new List<Chapters>
        {
            new Chapters { Name = "Chapter 1", Pages = 10 }
        };

            string newName = "New Chapter";
            int newPages = 15;

            book.newElement(newName, newPages);

            Assert.AreEqual(2, book.content.Count);
            Chapters newChapter = book.content[1];
            Assert.AreEqual(newName, newChapter.Name);
            Assert.AreEqual(newPages, newChapter.Pages);
        }

        [TestMethod()]
        public void printVec_Test()
        {
            Book book = new Book();
            book.content = new List<Chapters>
        {
            new Chapters { Name = "Chapter 1", Pages = 10 },
            new Chapters { Name = "Chapter 2", Pages = 15 },
            new Chapters { Name = "Chapter 3", Pages = 20 }
        };

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                book.printVec();

                string consoleOutput = stringWriter.ToString();

                Assert.IsTrue(consoleOutput.Contains("Chapter 1: 10 страниц;"));
                Assert.IsTrue(consoleOutput.Contains("Chapter 2: 15 страниц;"));
                Assert.IsTrue(consoleOutput.Contains("Chapter 3: 20 страниц;"));
            }
        }

        [TestMethod()]
        public void getPAgesTotal_Test()
        {
            Book book = new Book();
            book.content = new List<Chapters>
            {
            new Chapters { Pages = 10 },
            new Chapters { Pages = 5 },
            new Chapters { Pages = 15 },
            };

            int result = book.getPAgesTotal();
            Assert.AreEqual(30, result);
        }

        [TestMethod()]
        public void setContent_Test()
        {
            Book book = new Book();
            using (StringReader stringReader = new StringReader("2\nChapter1\n10\nChapter2\n15\n"))
            {
                Console.SetIn(stringReader);

                book.setContent();

                Assert.AreEqual(2, book.content.Count);
                Assert.AreEqual("Chapter1", book.content[0].Name);
                Assert.AreEqual(10, book.content[0].Pages);
                Assert.AreEqual("Chapter2", book.content[1].Name);
                Assert.AreEqual(15, book.content[1].Pages);
            }
        }

        [TestMethod()]
        public void PerChapter_Test()
        {
            Book book = new Book();

            int total_pages;
            int contentCount = 2;

            total_pages = book.getPAgesTotal();

            Assert.AreEqual(total_pages / contentCount, book.PerChapter());
        }

        [TestMethod()]
        public void Publish_Test1()
        {
            Book book = new Book();
            string pubName = "New Publisher";
            int pubYear = 2023;
            string newISBN = "9785000000000";

            short result = book.Publish(pubName, pubYear, newISBN);

            Assert.AreEqual(0, result);
        }
        [TestMethod()]
        public void Publish_Test2()
        {
            Book book = new Book();
            string pubName = "Existing Publisher";
            int pubYear = 2022;
            string newISBN = "aaa";

            short result = book.Publish(pubName, pubYear, newISBN);

            Assert.AreEqual(-1, result);
        }
        [TestMethod()]
        public void ToString_Test()
        {
            Book book = new Book();
            BookType testType = BookType.SOFT_COVER;
            Assert.AreEqual(book.ToString(testType), "Мягкая обложка");
        }


    }
}

