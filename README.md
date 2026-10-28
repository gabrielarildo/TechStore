# TechStore — Sistema de Gerenciamento de Loja de Informática

## Descrição do Projeto

O **TechStore** é um sistema web desenvolvido com **ASP.NET Core MVC** para o gerenciamento de uma loja de informática. O sistema permite o cadastro, listagem, visualização, edição e exclusão (CRUD) de **categorias** e **produtos**, com relacionamento entre as entidades, funcionalidade de pesquisa por nome, filtro por categoria e persistência de dados em **SQL Server** utilizando **Entity Framework Core**.

Este projeto foi desenvolvido como atividade prática do curso **F-Devs**, com o objetivo de demonstrar conhecimentos em C#, ASP.NET Core MVC, Entity Framework Core, SQL Server, operações CRUD, relacionamento entre entidades e pesquisa de dados.

## Tecnologias Utilizadas

- **C#** e **ASP.NET Core MVC** (.NET 10)
- **Entity Framework Core** com provedor para SQL Server
- **SQL Server** como banco de dados relacional
- **Razor Views** para renderização das páginas
- **HTML5, CSS3, JavaScript** e **Bootstrap 5** para a interface
- Validações com **Data Annotations** e **jQuery Validation**

## Funcionalidades Implementadas

### Categorias (CRUD completo)
- Cadastro de novas categorias (nome e descrição opcional)
- Listagem de categorias com contagem de produtos vinculados
- Visualização dos detalhes de uma categoria
- Edição de categorias existentes
- Exclusão de categorias (bloqueada se houver produtos vinculados)

### Produtos (CRUD completo)
- Cadastro de produtos (nome, descrição, preço, estoque e categoria)
- Listagem de produtos com nome da categoria exibida
- Visualização dos detalhes de um produto
- Edição de produtos existentes
- Exclusão de produtos

### Relacionamento entre Entidades
- Cada produto pertence a uma categoria (chave estrangeira)
- Uma categoria pode possuir vários produtos
- Dropdown para seleção de categoria nos formulários de produto
- Exibição do nome da categoria na listagem de produtos
- Restrição de exclusão de categoria com produtos vinculados (mensagem clara ao usuário)

### Pesquisa e Filtros
- Barra de pesquisa por nome do produto
- Filtro por categoria (dropdown)
- Combinação de pesquisa por nome com filtro de categoria
- Limpeza de filtros para retorno à listagem completa
- Consulta de dados reais via Entity Framework Core

### Interface e Experiência do Usuário
- Menu de navegação entre Início, Produtos e Categorias
- Dashboard inicial com indicadores (total de produtos, categorias e itens em estoque)
- Tabelas organizadas com ações de visualizar, editar e excluir
- Formulários com validações no servidor e no cliente
- Mensagens de sucesso e erro (TempData)
- Confirmação antes de exclusões
- Layout responsivo (computadores, tablets e celulares)
- Identidade visual consistente inspirada em lojas de tecnologia

## Estrutura Geral do Projeto

```
TechStore/
├── Controllers/
│   ├── HomeController.cs          # Dashboard e página de erro
│   ├── CategoriasController.cs    # CRUD de categorias
│   └── ProdutosController.cs      # CRUD de produtos com pesquisa/filtro
├── Data/
│   └── AppDbContext.cs            # Contexto Entity Framework Core
├── Migrations/
│   └── InitialCreate              # Migration inicial do banco de dados
├── Models/
│   ├── Categoria.cs               # Entidade Categoria
│   ├── Produto.cs                 # Entidade Produto
│   └── ErrorViewModel.cs          # Modelo de erro
├── Views/
│   ├── Home/
│   │   └── Index.cshtml           # Dashboard
│   ├── Categorias/
│   │   ├── Index.cshtml           # Listagem
│   │   ├── Details.cshtml         # Detalhes
│   │   ├── Create.cshtml          # Cadastro
│   │   ├── Edit.cshtml            # Edição
│   │   └── Delete.cshtml          # Exclusão
│   ├── Produtos/
│   │   ├── Index.cshtml           # Listagem com pesquisa/filtro
│   │   ├── Details.cshtml         # Detalhes
│   │   ├── Create.cshtml          # Cadastro
│   │   ├── Edit.cshtml            # Edição
│   │   └── Delete.cshtml          # Exclusão
│   └── Shared/
│       ├── _Layout.cshtml         # Layout mestre
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot/
│   ├── css/site.css               # Estilos personalizados
│   └── js/site.js                 # JavaScript personalizado
├── appsettings.json               # Configurações (string de conexão)
├── Program.cs                     # Configuração da aplicação
└── TechStore.csproj               # Projeto e dependências
```

## Pré-requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) ou superior
- [SQL Server](https://www.microsoft.com/pt-br/sql-server) (Express, Developer ou qualquer edição compatível)
- Um navegador web moderno

## Configuração da Conexão com SQL Server

1. Copie o arquivo `TechStore/appsettings.example.json` para `TechStore/appsettings.json`.
2. Abra o `appsettings.json` e ajuste a string de conexão com as credenciais do seu SQL Server:

```json
"ConnectionStrings": {
  "TechStoreConnection": "Server=localhost;Database=TechStore;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;"
}
```

- Substitua `SEU_USUARIO` e `SUA_SENHA` pelas credenciais do seu SQL Server.
- Se preferir autenticação do Windows, use: `Trusted_Connection=True;` em vez de usuário e senha.
- `TrustServerCertificate=True` permite conexão sem certificado SSL válido (útil em desenvolvimento).

> **Importante:** Nunca expor senhas, credenciais ou outros dados sensíveis no código ou no GitHub. O arquivo `appsettings.json` está no `.gitignore` e não será versionado. Utilize variáveis de ambiente ou o Secret Manager em ambientes de produção.

## Comandos para Execução

### 1. Restaurar dependências

```bash
dotnet restore
```

### 2. Criar a migration inicial (caso ainda não exista)

```bash
dotnet ef migrations add InitialCreate
```

### 3. Aplicar migrations ao banco de dados

```bash
dotnet ef database update
```

> O banco de dados `TechStore` será criado automaticamente no SQL Server com as tabelas `Categorias` e `Produtos`.

### 4. Compilar o projeto

```bash
dotnet build
```

### 5. Executar o sistema

```bash
dotnet run
```

Após executar, acesse: `https://localhost:5001` ou `http://localhost:5000` (conforme exibido no terminal).

## Persistência dos Dados

Os dados são persistidos no SQL Server e permanecem salvos após reiniciar a aplicação, pois o Entity Framework Core gerencia as tabelas e registros no banco de dados configurado.

## Boas Práticas Adotadas

- Padrão MVC com separação clara entre Models, Controllers e Views
- Configuração adequada do `DbContext` e injeção de dependência
- Migrations para versionamento e estruturação do banco de dados
- Validações no servidor (`ModelState.IsValid`) e no cliente (jQuery Validation)
- Tratamento de exceções (ex.: concorrência em atualizações)
- Uso de `TempData` para mensagens de feedback ao usuário
- `ValidateAntiForgeryToken` em formulários POST
- Nenhuma funcionalidade fora do escopo (sem login, pagamentos ou carrinho)
- Nenhum dado fictício utilizado para simular operações reais

## Limitações do Ambiente

- Não foi possível aplicar a migration ao SQL Server nem executar testes de ponta a ponta, pois **o SQL Server não está disponível neste ambiente de desenvolvimento**. A string de conexão está configurada para `localhost` com autenticação do Windows.
- A compilação do projeto foi realizada com sucesso (0 erros).
- Os testes de CRUD, relacionamento, pesquisa, validações e interface devem ser realizados em um ambiente com SQL Server instalado e configurado.
