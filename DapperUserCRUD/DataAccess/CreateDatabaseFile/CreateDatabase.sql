CREATE DATABASE DapperUserCRUD

CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Login NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    CreatedAt DATETIME NOT NULL
);

INSERT INTO Users (Id, Login, Password, Email, Age, CreatedAt) 
VALUES 
    ('9F887F2D-8DA5-4C7F-B245-36B0B2E24E0A', 'example_login1', 'password1', 'example1@example.com', 30, '2024-05-01T12:00:00'),
    ('1D1EFA8A-81B5-4792-AD7F-C26E6E922CD0', 'example_login2', 'password2', 'example2@example.com', 25, '2024-05-02T12:00:00'),
    ('D8A7E298-8C18-4A7B-B5F7-0E3B6FBAD418', 'example_login3', 'password3', 'example3@example.com', 35, '2024-05-03T12:00:00'),
    ('2D6FAEB3-57B3-4E18-AD28-879300ADBF5E', 'example_login4', 'password4', 'example4@example.com', 28, '2024-05-04T12:00:00');