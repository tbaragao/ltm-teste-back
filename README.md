Projeto LTM Teste
===================

Projeto desenvolvido como teste para LTM/Mootit

----------

Apresentação
-------------
Api desenvolvida em .NET Core 1.1 e Identity Server 4

#### Iniciando
- Clonar o repositório
- Abrir *LTM.Teste.sln* no Visual Studio (preferencialmente 2017)
- Esperar a instalação (se for no VS17) automática das dependências
- Verificar os projetos iniciais na propriedades da solution e setar o *LTM.Teste.Api* e *LTM.Teste.SSO.Api* como *startup projects*.

#### Dependências
A Api e o SSO trabalham em conjunto, ambos precisarão estar rodando ao mesmo tempo, assim como o projeto do SPA - [Acessar projeto front](https://github.com/brunoseco/ltm-teste-front). 

> **Atenção:**
O projeto da API precisará estar rodando na porta `:8122` e o SSO na porta `:4000`

#### Estrutura

- Api Restful (.net core 1.1)
- Application - Gerencia toda a orquestração e injeções de dependências da aplicação
- Dto
- Domain
	- Entity
	- Service
	- Validation
- Filter 
- Enums 
- Data - Acesso a dados e mapeamento das entidades
- Summary - Classes de sumário para auxílio do frontend
- Crosscuting - Implementações específicas do projeto
- Common - Projetos compartilhados
	-  Mail
	- Cache
	- Validation
	- Cripto
	- Outras classes bases
- SSO - oAuth e OIDC (Identity Server 4)
- Tests - Teste unitários (xUnit)

O projeto está modelado e desenvolvido utilizando

 - Asp.Net Core 1.1.
 - Entity Framework.
 - Automapper.
 - Domain Driven Design (Camadas e modelo de domínio).
 - Identity Server 4.
 - Swagger (Documentação).
> Existem outros projetos que não foram citados, mas subindo e debugando a aplicação veremos que temos várias camadas trabalhando para o funcionamento da aplicação.

#### SSO - Autenticação
Todo o gerenciamento de acesso, segurança da aplicação está focado neste projeto, utilizando Identity Server 4 (.net core).
O acesso inicialmente é feito pelo SPA, onde é configurado parâmetros em que só é possível o login se vier redirecionado do próprio SPA, podendo ser utilizado login e senha - provisoriamente está fixo na aplicação - quanto também acessar por uma conta do Google. Ambos processos utilizando oAuth 2.0.

> **Acesso**
> Login *administrador*
> Senha *123456*
> Ou utilize uma conta do Google.

O token *(padrão JWT)* fornecido pelo IDS4 será enviado nos headers das requisições `Bearer 1234-xxx-789-yyy` e validado ao acessar a Api.

Para conferir o que é enviado no token visualize a classe `Config.cs` no projeto do SSO.

#### Swagger - Documentação
Ao subir a Api, seus endpoints - e uma futura documentação - poderão ser acessadas por `http://localhost:8122/swagger/`, e o mapeamento de toda Api está disponível para consulta.

Poderão ser feitos os requests pelo Swagger também

 - Acessar o endpoint
 - Clicar em Authorize
 - Irá pedir um token, ele poderá ser acesso pelo SPA, no painel abaixo do nome do usuário logado, terá sua role (clicando será copiado para o clipboard o seu token).
 - Poderá também acessar via cookies e recuperar o valor da chave `ACCESS_TOKEN`
 - No input do Swagger precisa inserir o token no formato `Bearer xxxx-xxxx-xxxx`

#### Banco de dados

Banco modelo em SQL Server.

O script do banco poderá ser usado o abaixo, será criado um banco com o nome `LTM.Teste` uma tabela `Produto`, segue abaixo.

Não estará disponível uma carga dos dados, pois pelo SPA será possível realizar um CRUD completo para inserção.

    CREATE DATABASE [LTM.Teste]
    GO
    
    USE [LTM.Teste]
    GO
    /****** Object:  Table [dbo].[Produto]    Script Date: 21/10/2017 20:20:01 ******/
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
    SET ANSI_PADDING ON
    GO
    CREATE TABLE [dbo].[Produto](
    	[Id] [int] IDENTITY(1,1) NOT NULL,
    	[Nome] [varchar](150) NOT NULL,
    	[Descricao] [varchar](300) NULL,
    	[QtdeMinima] [decimal](18, 0) NOT NULL,
    	[Valor] [decimal](18, 2) NOT NULL,
    	[Ativo] [bit] NOT NULL,
    	[UserCreateId] [int] NOT NULL,
    	[UserCreateDate] [datetime] NOT NULL,
    	[UserAlterId] [int] NULL,
    	[UserAlterDate] [datetime] NULL,
     CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
    (
    	[Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
    
    GO
    SET ANSI_PADDING OFF
    GO

#### Testes
Existe um projeto de testes unitários onde valida alguma condições e estados inviduais das entidades
