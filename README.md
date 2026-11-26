# Session 1 - CRUD with EF Core, DTOs, Async

Project 2: Library System (Author, Book, Borrower, Loan).

- Full CRUD (Create, Update, Delete, GetAll, GetById) for Author, Borrower and Loan. Book is the first entity so it is not included
- Controllers use the DbContext directly, DTOs are mapped by hand, everything is async
- Relationships by convention: Author 1 => * Book, Book * <=> * Borrower through Loan

## Run
1. `Update-Database` in Package Manager Console (creates `LibrarySystem_Session1`)
2. Run `SeedBooks.sql` in SSMS to add some books (no seeding yet in this session)
3. Run the project and open `/swagger`

Also in this folder: the LinkedIn article about Navigation Property. The DB diagram screenshot is taken from SSMS.
