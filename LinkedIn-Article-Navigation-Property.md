Navigation Properties in EF Core, the thing that confused me at first

While working on a small Library System API in DEPI, I kept seeing properties like this:

public int AuthorId { get; set; }
public Author Author { get; set; }

and I asked myself why do I need both?

So what is a navigation property?
Simply, it is a property in your entity that points to another entity (or a list of them), so you can move from one object to the related one in C# without writing joins yourself.

In my project:
- A Book has one Author, so Book has `Author Author`
- An Author has many books, so Author has `ICollection<Book> Books`
- Book and Borrower are many to many, so both of them have `ICollection<Loan> Loans` and Loan has `Book` and `Borrower`

AuthorId vs Author
- AuthorId is the foreign key, it is the actual column in the database
- Author is the navigation property, it is not a column, EF uses it to load the related object

You can set only AuthorId when creating a book and it works fine, you don't need to load the whole author.

Types
- Reference navigation: points to one object (Book.Author)
- Collection navigation: points to a list (Author.Books)

Loading the related data
Navigation properties are not loaded by default, you get null or an empty list if you don't ask for them. You have 3 ways:
1. Eager loading with Include: `context.Books.Include(b => b.Author)`
2. Lazy loading: needs the Proxies package and virtual properties, it loads when you touch the property (careful, it can make a lot of queries)
3. Explicit loading: `context.Entry(book).Reference(b => b.Author).LoadAsync()`

Mistakes I made
- Returning the entity directly from the API with its navigations gave me a cycle (Author has Books, each Book has Author...). Use DTOs.
- Forgetting the Include and wondering why Author is null
- Including everything everywhere, which makes the queries heavy for nothing

That's it, small thing but once you get it, the relationships in EF Core make a lot more sense.

#EFCore #DotNet #WebAPI #DEPI #Backend
