# Features

* Fluent Validation with strong type

# Partial field update example


```csharp

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




    class Book
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string SerialNo { get; set; }
        public int Version { get; set; }

    }


```
