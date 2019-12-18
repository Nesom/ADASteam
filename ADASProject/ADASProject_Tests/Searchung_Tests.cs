using NUnit.Framework;
using ADASProject;
using ADASProject.Products;
using Searching;
using System.Collections.Generic;

namespace Searching_Tests
{
    public class Tests
    {
        private List<Product> products = new List<Product>()
            {
                new Product(1, "Apple", "Just Apple"),
                new Product(2, "Apple 4", "Simple Apple 4"),
                new Product(3, "Apple 4 Max Pro", "Not simple apple 4"),
                new Product(4, "Tomato", "Just Tomato"),
                new Product(5, "Blue Tomato", "Not Red Tomato")
            };

        [Test]
        public void ShortName()
        {
            var answer = Searcher.Search("Apple", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void TwoWrongLetters()
        {
            var answer = Searcher.Search("Appel", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void IncompleteWord()
        {
            var answer = Searcher.Search("App", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void TwoWrongLettersAgain()
        {
            var answer = Searcher.Search("Aprla", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void SmallLetter()
        {
            var answer = Searcher.Search("apple", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void BigLetters()
        {
            var answer = Searcher.Search("APPLE", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void LetterAndNumber()
        {
            var answer = Searcher.Search("Appl4", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }

        [Test]
        public void WithoutTwoLetters()
        {
            var answer = Searcher.Search("Blue Toma", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithSpaces()
        {
            var answer = Searcher.Search(" Tomato        ", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithSpaceAndwithAnotherChar()
        {
            var answer = Searcher.Search(" Tomato:", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithTwoDifferentChars()
        {
            var answer = Searcher.Search("-Tomato:", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithThreeDifferentChars()
        {
            var answer = Searcher.Search("+-Tomato:", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithSpacesAndChar()
        {
            var answer = Searcher.Search(" Tomato        -", products);
            Assert.AreEqual(new List<int> { 4, 5 }, answer);
        }

        [Test]
        public void WithAnotherLetterAndChar()
        {
            var answer = Searcher.Search("Appr2", products);
            Assert.AreEqual(new List<int> { 1, 2, 3 }, answer);
        }
    }
}