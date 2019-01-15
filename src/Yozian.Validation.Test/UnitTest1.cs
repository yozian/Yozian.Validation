using System.Linq;
using NUnit.Framework;
using Yozian.Validation.Test.TestMaterial;

namespace Yozian.Validation.Test
{
    public class Tests
    {
        private Book book = new Book()
        {
            Id = -1,
            Name = "The Phenix Project",
            Author = null,
            SerialNo = "123456789",
            Version = 1
        };


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_BasicValidation()
        {
            Assert.Throws<ValidationException>(() =>
            {
                Validation
                    .Entry(book)
                    .NotAllowedFor((x) => string.IsNullOrEmpty(x.Author), $"{nameof(Book.Author)} should not be empty")
                    .ThrowErrorIfPresents();
            });

            Assert.Throws<ValidationException>(() =>
            {
                Validation
                    .Entry(book)
                    .NotAllowedFor(string.IsNullOrEmpty(book.Author), $"{nameof(Book.Author)} should not be empty")
                    .ThrowErrorIfPresents();
            });


            Assert.Throws<ValidationException>(() =>
            {
                Validation
                    .Entry(book)
                    .OnlyAcceptFor((x) => x.Id > 0, $"{nameof(Book.Id)} should greater than 0")
                    .ThrowErrorIfPresents();
            });

            Assert.Throws<ValidationException>(() =>
            {
                Validation
                    .Entry(book)
                    .OnlyAcceptFor(book.Id > 0, $"{nameof(Book.Id)} should greater than 0")
                    .ThrowErrorIfPresents();
            });

            Assert.Pass();
        }

        [Test]
        public void Test_FluentValidation()
        {
            Assert.Throws<AggregateValidationException>(() =>
            {
                try
                {
                    Validation
                       .Entry(book)
                       .NotAllowedFor(x => string.IsNullOrEmpty(x.SerialNo), "SerialNo is REQUIRED")
                       .OnlyAcceptFor(x => x.SerialNo.Length.Equals(10), "SerialNo length should be 10")
                       .NotNullOrEmpty(x => x.Name)
                       .GreaterThan(x => x.Id, 0, null, "{0} should be bigger!")
                       .ThrowErrorIfPresents();

                    // or throw all erros
                    // .ThrowAllErrorsIfPresents();

                    // get first error message
                    // .GetFirstError()

                    //  get all error messages
                    // .GetAggregateErros()
                    // .GetAggregateErrosAsString()
                }
                catch (AggregateValidationException ex)
                {
                    Assert.AreEqual(2, ex.ValidationErrors.Count());
                    throw;
                }

            });

            Assert.Pass();
        }
    }
}