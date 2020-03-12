CREATE TABLE Birthday(
	Id INT IDENTITY(0,1) PRIMARY KEY,
	Nome VARCHAR(15),
	Sobrenome VARCHAR(20),
	DataNascimento DATE
);

INSERT INTO Birthday (Nome, Sobrenome, DataNascimento) 
VALUES ('Ozzy', 'Osbourne', '1948-12-03');

SELECT * FROM Birthday;

DROP TABLE Birthday;

SELECT Id as Id, Nome as Nome, Sobrenome as Sobrenome FROM Birthday ORDER BY DataNascimento DESC

UPDATE Birthday
	SET 
		Nome = 'Saul', Sobrenome = 'Hudson', DataNascimento = '1965-07-06'
	WHERE
		Id = 1;