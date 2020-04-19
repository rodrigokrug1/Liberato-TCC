CREATE TABLE TipoChave(
Tipo VARCHAR(11) NOT NULL PRIMARY KEY,
Descricao VARCHAR(30),
Inativo BIT
CONSTRAINT tipoChave_unica UNIQUE (Tipo)
);

CREATE TABLE Chaves(
Chave VARCHAR(11) NOT NULL PRIMARY KEY,
Tipo VARCHAR(11) NOT NULL,
Descricao VARCHAR(30),
GeraConta BIT,
Inativo BIT
CONSTRAINT chave_unica UNIQUE (chave, Tipo)
FOREIGN KEY(Tipo) REFERENCES tipoChave(Tipo)
);

CREATE TABLE Contas(
Conta CHAR(3) NOT NULL PRIMARY KEY,
Tipo VARCHAR(11) NOT NULL,
Descricao VARCHAR(30),
Juro FLOAT,
Multa FLOAT,
Inativo BIT
CONSTRAINT conta_unica UNIQUE (Conta, Chave)
FOREIGN KEY(TipoChave) REFERENCES TipoChave(Tipo)
);

CREATE TABLE Motos(
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Marca VARCHAR(20),
Modelo VARCHAR(30),
Cilindrada CHAR(4),
Ano CHAR(4)
);

CREATE TABLE Faccoes(
Chave VARCHAR(11) NOT NULL PRIMARY KEY,
Descricao VARCHAR(30),
CEP CHAR(8),
Endereco VARCHAR(80),
Numero VARCHAR(5),
Compl VARCHAR(5),
Bairro VARCHAR(40),
Cidade VARCHAR(40),
UF VARCHAR(2),
Pais VARCHAR(20),
Inativo BIT
FOREIGN KEY(Faccao) REFERENCES Chaves(Chave)
);

CREATE TABLE Membros(
Matricula INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Nome VARCHAR(50),
Graduacao VARCHAR(11),
Faccao VARCHAR(11),
DtNascimento DATETIME,
DtIngresso DATETIME,
Nacionalidade VARCHAR(20),
Apelido VARCHAR(20),
CEP CHAR(8),
Endereco VARCHAR(80),
Numero VARCHAR(5),
Compl VARCHAR(5),
Bairro VARCHAR(40),
Cidade VARCHAR(40),
UF VARCHAR(2),
Pais VARCHAR(20),
RG char(10) UNIQUE,
CPF char(11) UNIQUE,
CNH char(11) UNIQUE,
DtExpedicaoCNH DATETIME,
Email VARCHAR(50),
Telefone CHAR(10),
Celular CHAR(11),
NomePai VARCHAR(50),
NomeMae VARCHAR(50),
TipoSanguineo VARCHAR(3),
FatorRH VARCHAR(3),
Motocicleta INT,
Ano CHAR(4),
Inativo BIT,
Ass VARCHAR(100)
FOREIGN KEY (Motocicleta) REFERENCES Motos(Id),
FOREIGN KEY (Graduacao) REFERENCES Chaves(Chave),
FOREIGN KEY (Faccao) REFERENCES Faccoes(Chave)
);

/*
	Para criar a tabela arquivos é necessário configurar o FILESTREAM
	Como configurar: https://www.youtube.com/watch?v=o0KNTkXSj7c
*/
create table Arquivos(
Id UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL PRIMARY KEY,
Matricula INT NOT NULL,
Documento VARBINARY(MAX) FILESTREAM NULL,
Foto VARBINARY(MAX) FILESTREAM NULL,
Ass VARCHAR(100)
FOREIGN KEY(Matricula) REFERENCES Membros(Matricula)
);

CREATE TABLE Usuario(
Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
Nome VARCHAR(100),
Login VARCHAR(50),
Senha VARCHAR(100),
Inativo BIT
);

CREATE TABLE FormaPagamento(
Tipo CHAR(1) NOT NULL PRIMARY KEY,
Descricao VARCHAR(20),
Inativo BIT
);

CREATE TABLE Prestacoes(
Nrprest INT IDENTITY(1,1) PRIMARY KEY,
Matricula INT,
Conta CHAR(3),
Chave VARCHAR(11),
Sequencia CHAR(7),
Valor DECIMAL,
ValorCalculado DECIMAL,
DtVencimento DATETIME,
DtPagamento DATETIME,
Situacao CHAR(1),
FormaPagamento CHAR(1),
Obs VARCHAR(100),
Ass VARCHAR(100)
CONSTRAINT prest_unica UNIQUE (NrPrest, Matricula)
FOREIGN KEY(Matricula) REFERENCES Membros(Matricula),
FOREIGN KEY(FormaPagamento) REFERENCES FormaPagamento(Tipo),
FOREIGN KEY(Chave) REFERENCES Chaves(Chave),
FOREIGN KEY(Conta) REFERENCES Contas(Conta)
);

CREATE TABLE Parametros(
Clube VARCHAR(30),
CNPJ CHAR(14) PRIMARY KEY,
CEP CHAR(8),
Endereco VARCHAR(80),
Numero VARCHAR(5),
Compl VARCHAR(5),
Bairro VARCHAR(40),
Cidade VARCHAR(40),
UF VARCHAR(2),
Pais VARCHAR(20)
);

-- Se der tempo eu faço!
CREATE TABLE Gestao(
Id int IDENTITY(1,1) PRIMARY KEY,
Descricao VARCHAR(30),
Periodo VARCHAR(9),
GestaoAtual BIT
);

CREATE TABLE Cargos(
Id INT IDENTITY(1,1) PRIMARY KEY,
Descricao VARCHAR(30),
Inativo BIT
);

CREATE TABLE Diretoria(
Matricula INT PRIMARY KEY,
Cargo INT,
Gestao INT,
Faccao VARCHAR(11)
FOREIGN KEY (Matricula) REFERENCES Membros(Matricula),
FOREIGN KEY (Faccao) REFERENCES Faccoes(Chave),
FOREIGN KEY (Gestao) REFERENCES Gestao(Id),
FOREIGN KEY (Cargo) REFERENCES Cargos(Id)
);