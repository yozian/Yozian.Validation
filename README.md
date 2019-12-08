# Features

* Fluent Validation with strong type

# Partial field update example


```csharp


    Book book = new Book()
            {
                Id = -1,
                Name = "The Phenix Project",
                Author = null,
                SerialNo = "123456789",
                Version = 1
            };

    Validation
        .Entry(book)
        .NotAllowedFor(x => string.IsNullOrEmpty(x.SerialNo), "SerialNo is REQUIRED")
        .OnlyAcceptFor(x => x.SerialNo.Length.Equals(10), "SerialNo length should be 10")
        .NotNullOrEmpty(x => x.Name)
        .GreaterThan(x => x.Id, 0, null, "{0} should be bigger!")
        // conditional validation (this wont process because id is less than 0)
        .OnlyAcceptForWhen(x => x.Id > 0, x => !string.IsNullOrEmpty(x.Name), "Name should not be empty!")
        // conditional validation (this would process because id is less than 0)
        .NotAllowedForWhen(x => x.Id < 0, x => x.Id == -1, "Id should not be negative")
        .ThrowAllErrorsIfPresents();

    // or get first error
    // .GetFirstError()

    // or throw all erros
    // .ThrowAllErrorsIfPresents();

    // get first error message
    // .GetFirstError()

    //  get all error messages
    // .GetAggregateErros()
    // .GetAggregateErrosAsString()


    class Book
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string SerialNo { get; set; }
        public int Version { get; set; }

    }


```
