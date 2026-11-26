-- Session 1 has no seeding yet (it comes in Session 2) and Book has no endpoints
-- so run this after Update-Database to have some books to test the loans

USE LibrarySystem_Session1;

INSERT INTO Authors (Name, BirthDate) VALUES
('Naguib Mahfouz', '1911-12-11'),
('Robert C. Martin', '1952-12-05');

INSERT INTO Books (Title, ISBN, AuthorId) VALUES
('Palace Walk', '9780385264662', 1),
('Children of the Alley', '9780385264730', 1),
('Clean Code', '9780132350884', 2);

INSERT INTO Borrowers (Name, MembershipDate) VALUES
('Omar Hamza', '2025-01-15');
