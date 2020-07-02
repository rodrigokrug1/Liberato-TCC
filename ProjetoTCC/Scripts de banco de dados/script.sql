--TipoChave
CREATE TABLE [dbo].[TipoChave](
	[Tipo] [varchar](11) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [tipoChave_unica] UNIQUE NONCLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO TIPOCHAVE (Tipo, Descricao, Inativo) VALUES('Facção', 'Facções do clube', 0)
INSERT INTO TIPOCHAVE (Tipo, Descricao, Inativo) VALUES('Graduação', 'Graduação dos membros', 0)
INSERT INTO TIPOCHAVE (Tipo, Descricao, Inativo) VALUES('Mensalidade', 'Mensalidade dos membros', 0)

--Chaves
CREATE TABLE [dbo].[Chaves](
	[Chave] [varchar](11) NOT NULL,
	[Tipo] [varchar](11) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[Inativo] [bit] NULL,
	[GeraConta] [bit] NULL,
	[ValorSugerido] [decimal](18, 0) NULL,
	[DtVencimentoSugerida] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Chave] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [chave_unica] UNIQUE NONCLUSTERED 
(
	[Chave] ASC,
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Chaves]  WITH CHECK ADD FOREIGN KEY([Tipo])
REFERENCES [dbo].[TipoChave] ([Tipo])
GO

--Contas
CREATE TABLE [dbo].[Contas](
	[Conta] [char](3) NOT NULL,
	[Tipo] [varchar](11) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[Juro] [float] NULL,
	[Multa] [float] NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Conta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [conta_unica] UNIQUE NONCLUSTERED 
(
	[Conta] ASC,
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contas]  WITH CHECK ADD FOREIGN KEY([Tipo])
REFERENCES [dbo].[TipoChave] ([Tipo])
GO

--Motos
CREATE TABLE [dbo].[Motos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](20) NULL,
	[Modelo] [varchar](30) NULL,
	[Cilindrada] [char](4) NULL,
	[Ano] [char](4) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--Faccoes
CREATE TABLE [dbo].[Faccoes](
	[Chave] [varchar](11) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[CEP] [char](8) NULL,
	[Endereco] [varchar](80) NULL,
	[Numero] [varchar](5) NULL,
	[Compl] [varchar](5) NULL,
	[Bairro] [varchar](40) NULL,
	[Cidade] [varchar](40) NULL,
	[UF] [varchar](2) NULL,
	[Pais] [varchar](20) NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Chave] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Faccoes]  WITH CHECK ADD FOREIGN KEY([Chave])
REFERENCES [dbo].[Chaves] ([Chave])
GO

ALTER TABLE [dbo].[Faccoes]  WITH CHECK ADD FOREIGN KEY([Chave])
REFERENCES [dbo].[Chaves] ([Chave])
GO

--Membros
CREATE TABLE [dbo].[Membros](
	[Matricula] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Graduacao] [varchar](11) NULL,
	[Faccao] [varchar](11) NULL,
	[DtNascimento] [datetime] NULL,
	[DtIngresso] [datetime] NULL,
	[Nacionalidade] [varchar](20) NULL,
	[Apelido] [varchar](20) NULL,
	[CEP] [char](8) NULL,
	[Endereco] [varchar](80) NULL,
	[Numero] [varchar](5) NULL,
	[Compl] [varchar](5) NULL,
	[Bairro] [varchar](40) NULL,
	[Cidade] [varchar](40) NULL,
	[UF] [varchar](2) NULL,
	[Pais] [varchar](20) NULL,
	[RG] [char](10) NULL,
	[CPF] [char](11) NULL,
	[CNH] [char](11) NULL,
	[DtExpedicaoCNH] [datetime] NULL,
	[Email] [varchar](50) NULL,
	[Telefone] [char](10) NULL,
	[Celular] [char](11) NULL,
	[NomePai] [varchar](50) NULL,
	[NomeMae] [varchar](50) NULL,
	[TipoSanguineo] [varchar](3) NULL,
	[FatorRH] [varchar](3) NULL,
	[Motocicleta] [int] NULL,
	[Ano] [char](4) NULL,
	[Inativo] [bit] NULL,
	[Ass] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CPF] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CNH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Membros]  WITH CHECK ADD FOREIGN KEY([Motocicleta])
REFERENCES [dbo].[Motos] ([Id])
GO

ALTER TABLE [dbo].[Membros]  WITH CHECK ADD FOREIGN KEY([Faccao])
REFERENCES [dbo].[Faccoes] ([Chave])
GO

ALTER TABLE [dbo].[Membros]  WITH CHECK ADD FOREIGN KEY([Graduacao])
REFERENCES [dbo].[Chaves] ([Chave])
GO

--Arquivos
--CREATE TABLE [dbo].[Arquivos](
--	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
--	[Matricula] [int] NOT NULL,
--	[Documento] [varbinary](max) FILESTREAM  NULL,
--	[Foto] [varbinary](max) FILESTREAM  NULL,
--	[Ass] [varchar](100) NULL,
--PRIMARY KEY CLUSTERED 
--(
--	[Id] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] FILESTREAM_ON [FILESTREAM]
--) ON [PRIMARY] FILESTREAM_ON [FILESTREAM]
--GO

--ALTER TABLE [dbo].[Arquivos]  WITH CHECK ADD FOREIGN KEY([Matricula])
--REFERENCES [dbo].[Membros] ([Matricula])
--GO

--Usuarios
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
	[Login] [varchar](50) NULL,
	[Senha] [varchar](100) NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--FormaPagamento
CREATE TABLE [dbo].[FormaPagamento](
	[Tipo] [char](1) NOT NULL,
	[Descricao] [varchar](20) NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--Prestacoes
CREATE TABLE [dbo].[Prestacoes](
	[Nrprest] [int] IDENTITY(1,1) NOT NULL,
	[Matricula] [int] NULL,
	[Conta] [char](3) NULL,
	[Chave] [varchar](11) NULL,
	[Sequencia] [char](6) NULL,
	[Valor] [decimal](18, 0) NULL,
	[ValorCalculado] [decimal](18, 0) NULL,
	[DtVencimento] [datetime] NULL,
	[DtPagamento] [datetime] NULL,
	[Situacao] [char](1) NULL,
	[FormaPagamento] [char](1) NULL,
	[Obs] [varchar](100) NULL,
	[Ass] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Nrprest] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [prest_unica] UNIQUE NONCLUSTERED 
(
	[Matricula] ASC,
	[Conta] ASC,
	[Chave] ASC,
	[Sequencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prestacoes]  WITH CHECK ADD FOREIGN KEY([Chave])
REFERENCES [dbo].[Chaves] ([Chave])
GO

ALTER TABLE [dbo].[Prestacoes]  WITH CHECK ADD FOREIGN KEY([Conta])
REFERENCES [dbo].[Contas] ([Conta])
GO

ALTER TABLE [dbo].[Prestacoes]  WITH CHECK ADD FOREIGN KEY([FormaPagamento])
REFERENCES [dbo].[FormaPagamento] ([Tipo])
GO

ALTER TABLE [dbo].[Prestacoes]  WITH CHECK ADD FOREIGN KEY([Matricula])
REFERENCES [dbo].[Membros] ([Matricula])
GO

--Parametros
CREATE TABLE [dbo].[Parametros](
	[RazaoSocial] [varchar](50) NULL,
	[Clube] [varchar](20) NULL,
	[Sigla] [char](4) NULL,
	[CNPJ] [char](14) NOT NULL,
	[CEP] [char](8) NULL,
	[Endereco] [varchar](80) NULL,
	[Numero] [varchar](5) NULL,
	[Compl] [varchar](5) NULL,
	[Bairro] [varchar](40) NULL,
	[Cidade] [varchar](40) NULL,
	[UF] [varchar](2) NULL,
	[Pais] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CNPJ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--Gestao
CREATE TABLE [dbo].[Gestao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[Periodo] [varchar](9) NULL,
	[GestaoAtual] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--Cargos
CREATE TABLE [dbo].[Cargos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](30) NULL,
	[Inativo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


--Diretoria
CREATE TABLE [dbo].[Diretoria](
	[Matricula] [int] NOT NULL,
	[Cargo] [int] NULL,
	[Gestao] [int] NULL,
	[Faccao] [varchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[Matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Diretoria]  WITH CHECK ADD FOREIGN KEY([Cargo])
REFERENCES [dbo].[Cargos] ([Id])
GO

ALTER TABLE [dbo].[Diretoria]  WITH CHECK ADD FOREIGN KEY([Matricula])
REFERENCES [dbo].[Membros] ([Matricula])
GO

ALTER TABLE [dbo].[Diretoria]  WITH CHECK ADD FOREIGN KEY([Faccao])
REFERENCES [dbo].[Faccoes] ([Chave])
GO

ALTER TABLE [dbo].[Diretoria]  WITH CHECK ADD FOREIGN KEY([Gestao])
REFERENCES [dbo].[Gestao] ([Id])
GO