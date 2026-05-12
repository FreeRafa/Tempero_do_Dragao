# 🐉 Tempero do Dragão

Plataforma web de gestão de receitas culinárias, construída com **ASP.NET Core Razor Pages** e **ADO.NET puro**. Permite aos utilizadores partilhar, descobrir, comentar e avaliar receitas.

---

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Modelos de Dados](#modelos-de-dados)
- [Funcionalidades](#funcionalidades)
- [Como Executar](#como-executar)
- [Estrutura do Projeto](#estrutura-do-projeto)

---

## Sobre o Projeto

O **Tempero do Dragão** é uma plataforma web de receitas que permite aos utilizadores registarem-se, submeterem as suas receitas com imagens, comentar e avaliar receitas de outros utilizadores, e guardar as suas favoritas.

O projeto utiliza **ADO.NET puro** para acesso à base de dados, sem recurso a ORM — todo o SQL é escrito à mão, com controlo total sobre as queries, transações e mapeamento de dados.

---

## Tecnologias

- [.NET / ASP.NET Core Razor Pages](https://dotnet.microsoft.com/)
- [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/) com `Microsoft.Data.SqlClient`
- SQL Server (LocalDB em desenvolvimento)
- Padrão Repository + Service Layer
- Sessões para autenticação
- Upload de imagens local (Docker-ready via volume)

---

## Arquitetura

O projeto segue uma arquitetura em camadas:

┌─────────────────────────────────────────────────────────┐
│        Razor Pages          │  ← Interface web (.cshtml + .cshtml.cs)
├─────────────────────────────────────────────────────────┤
│         Services            │  ← Lógica de negócio
├─────────────────────────────────────────────────────────┤
│        Repositories         │  ← Acesso à base de dados (SQL escrito à mão)
├─────────────────────────────────────────────────────────┤
│    ADO.NET / SqlClient      │  ← Ligação direta ao SQL Server
├─────────────────────────────────────────────────────────┤
│       Base de Dados         │
└─────────────────────────────────────────────────────────┘

A classe base `Repository<T>` guarda a referência à `DbConnectionFactory`. Cada repositório implementa as suas próprias queries SQL com `SqlConnection`, `SqlCommand` e `SqlDataReader`.

Operações complexas como criar ou editar receitas com ingredientes usam `SqlTransaction` para garantir atomicidade.

---

## Modelos de Dados

### Relações entre entidades

User ──────────< Recipe >────── Category
│                │ └────────── Difficulty
│                │
├──< Comment >───┘
├──< Rating  >───┘
└──< Favorite>───┘
│
RecipeIngredient
│       │
Ingredient  Measurement

### Entidades principais

| Entidade | Descrição |
|---|---|
| `User` | Utilizador da plataforma (pode ser admin) |
| `Recipe` | Receita com nome, método, tempo, dificuldade e imagem |
| `Category` | Categoria da receita (ex: Sobremesa, Sopa) |
| `Difficulty` | Nível de dificuldade (ex: Fácil, Médio, Difícil) |
| `Ingredient` | Ingrediente individual |
| `Measurement` | Unidade de medida (ex: g, ml, colher) |
| `RecipeIngredient` | Relação entre receita, ingrediente e quantidade |
| `Comment` | Comentário de um utilizador a uma receita |
| `Rating` | Avaliação (1–5 estrelas) de uma receita |
| `Favorite` | Receita guardada como favorita por um utilizador |

---

## Funcionalidades

### Utilizadores
- Registo e login com sessão
- Verificação de email duplicado
- Controlo de acesso por papel (utilizador / admin)

### Receitas
- Criação, edição e remoção de receitas
- Upload de imagem por receita (JPG, PNG, WEBP)
- Pesquisa por nome, categoria, dificuldade ou utilizador
- Página de detalhe com hero de imagem, ingredientes e modo de preparação

### Interações
- **Comentários** — publicar e remover comentários por receita
- **Avaliações** — classificar receitas de 1 a 5 estrelas (uma por utilizador por receita)
- **Favoritos** — guardar e remover receitas favoritas, sem duplicados

---

## Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server ou SQL Server LocalDB

### Passos

```bash
# 1. Clonar o repositório
git clone https://github.com/teu-utilizador/tempero-do-dragao.git
cd tempero-do-dragao

# 2. Configurar a connection string em appsettings.json
# "ConnectionStrings": { "DefaultConnection": "Server=...;Database=Tempero_do_Dragao;..." }

# 3. Criar a base de dados
# Executar o ficheiro SQL/CreateDatabase.sql no SQL Server Management Studio ou Azure Data Studio

# 4. Executar a aplicação
dotnet run
```

A aplicação ficará disponível em `https://localhost:5001` por defeito.

---

## Estrutura do Projeto

Tempero_do_Dragao/
│
├── Model/                  # Entidades do domínio
│   ├── User.cs
│   ├── Recipe.cs
│   ├── Category.cs
│   ├── Difficulty.cs
│   ├── Ingredient.cs
│   ├── Measurement.cs
│   ├── RecipeIngredient.cs
│   ├── Comment.cs
│   ├── Rating.cs
│   └── Favorite.cs
│
├── Repositories/           # Acesso à base de dados (ADO.NET)
│   ├── Repository.cs           ← Base abstrata com DbConnectionFactory
│   ├── RecipeRepository.cs     ← Queries com JOIN e SqlTransaction
│   ├── UserRepository.cs
│   ├── CommentRepository.cs
│   ├── FavoriteRepository.cs
│   ├── RatingRepository.cs
│   └── SimpleRepositories.cs   ← Category, Difficulty, Ingredient, Measurement
│
├── Services/               # Lógica de negócio
│   ├── RecipeService.cs
│   ├── UserService.cs
│   ├── CommentService.cs
│   ├── FavoriteService.cs
│   ├── RatingService.cs
│   ├── ImageService.cs         ← Upload e eliminação de imagens
│   └── SimpleService.cs        ← Category, Difficulty, Ingredient, Measurement
│
├── Pages/                  # Razor Pages
│   ├── Recipes/
│   │   ├── Index.cshtml        ← Listagem de receitas
│   │   ├── Detail.cshtml       ← Detalhe, comentários, avaliação, favorito
│   │   ├── Create.cshtml       ← Criar receita com imagem
│   │   └── Edit.cshtml         ← Editar receita com imagem
│   ├── Auth/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   └── Favorites.cshtml
│
├── Data/
│   └── DbConnectionFactory.cs  ← Singleton de ligação ao SQL Server
│
├── SQL/
│   └── CreateDatabase.sql      ← Script completo de criação da BD
│
└── wwwroot/
└── uploads/
└── recipes/  

---

## Notas de Desenvolvimento

- Todo o acesso à base de dados é feito com **ADO.NET puro** — sem ORM, sem migrations, SQL escrito à mão.
- A criação e edição de receitas com ingredientes é feita dentro de uma **`SqlTransaction`** para garantir atomicidade.
- As avaliações estão limitadas a valores entre **1 e 5**, validadas na camada de serviço e com `CHECK constraint` na base de dados.
- Os favoritos não permitem duplicados — verificação feita antes de inserir.
- As imagens são guardadas em `wwwroot/uploads/recipes/` com nome gerado por `Guid`. O caminho relativo é guardado na base de dados.
- O projeto está preparado para **Docker** — o diretório de imagens pode ser montado como volume externo sem alterações ao código.

---

*Projeto académico desenvolvido em C# com ASP.NET Core Razor Pages.*