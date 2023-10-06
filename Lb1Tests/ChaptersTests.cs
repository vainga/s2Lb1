using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lb1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using static Lb1.Book;
using System.Reflection.Metadata;

namespace Lb1.Tests
{
    [TestClass()]
    public class ChaptersTests
    {

        [TestMethod()]
        public void setName_Test()
        {
            Chapters chapter = new Chapters();
            string emptyName = "";

            Assert.ThrowsException<ArgumentNullException>(() => chapter.setName(emptyName));
        }

        [TestMethod()]
        public void setPages_Test()
        {
            Chapters chapter = new Chapters();
            int negativePages = -5;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => chapter.setPages(negativePages));
        }



        [TestMethod()]
        public void setPubHouse_Test()
        {
            Book book = new Book();
            string emptyName = "";

            Assert.ThrowsException<ArgumentNullException>(() => book.setPubHouse(emptyName));
        }

        [TestMethod()]
        public void setYear_Test()
        {
            Book book = new Book();
            int falseYear = 1000;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => book.setYear(falseYear));
        }

        [TestMethod()]
        public void SetISBT_Test()
        {
            Book book = new Book();
            string falseISBT = "1231238";

            Assert.ThrowsException<ArgumentException>(() => book.setISBT(falseISBT));
        }

        [TestMethod()]
        public void SetAuthor_Test()
        {
            Book book = new Book();
            string falseAuthor1 = "";
            string falseAuthor2 = "";

            Assert.ThrowsException<ArgumentException>(() => book.setISBT(falseAuthor1));
            Assert.ThrowsException<ArgumentException>(() => book.setISBT(falseAuthor2));
        }

        [TestMethod()]
        public void setBookName_Test()
        {
            Book book = new Book();
            string falseName = "";

            Assert.ThrowsException<Exception>(() => book.setName(falseName));
        }
        

        [TestMethod()]
        public void setType_Test()
        {
            Book book = new Book();
            BookType newType = new BookType();

            newType = BookType.HARD_COVER;

            book.setType(newType);

            Assert.AreEqual(newType, book.getType());
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