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
		Nome = 'Axl', Sobrenome = 'Rose', DataNascimento = '1965-07-06'
	WHERE
		Id = 10;

DECLARE @Limit INT = 5,
		@Offset INT = 1;
WITH resultado As
	(
		SELECT *, ROW_NUMBER() OVER (ORDER BY ID) AS Linha
		FROM Birthday WHERE Id is not null
	)
	SELECT *
		FROM resultado
	WHERE linha >= @Offset
		AND linha < @Offset + @limit