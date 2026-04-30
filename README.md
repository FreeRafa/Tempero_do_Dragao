# 🐉 Tempero do Dragão

API REST para gestão de receitas culinárias, construída com **ASP.NET Core** e **Entity Framework Core**. Permite aos utilizadores partilhar, descobrir e avaliar receitas.

---

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Tecnologias](#tecnologias)
- [Arquitetura](#arquitetura)
- [Modelos de Dados](#modelos-de-dados)
- [Funcionalidades](#funcionalidades)
- [Como Executar](#como-executar)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Endpoints](#endpoints)

---

## Sobre o Projeto

O **Tempero do Dragão** é uma plataforma de receitas que permite aos utilizadores registarem-se, submeterem as suas receitas, comentar e avaliar receitas de outros utilizadores, e guardar as suas favoritas.

---

## Tecnologias

- [.NET / ASP.NET Core](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- SQL Server (ou outro provider configurado no `AppDbContext`)
- Padrão Repository + Service Layer

---

## Arquitetura

O projeto segue uma arquitetura em camadas:

```
┌─────────────────────────────┐
│        Controllers          │  ← Recebe os pedidos HTTP
├─────────────────────────────┤
│         Services            │  ← Lógica de negócio
├─────────────────────────────┤
│        Repositories         │  ← Acesso à base de dados
├─────────────────────────────┤
│      Entity Framework       │  ← ORM
├─────────────────────────────┤
│       Base de Dados         │
└─────────────────────────────┘
```

A classe base `Repository<T>` fornece as operações CRUD genéricas. Cada entidade tem o seu repositório específico para queries mais complexas.

---

## Modelos de Dados

### Relações entre entidades

```
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
```

### Entidades principais

| Entidade | Descrição |
|---|---|
| `User` | Utilizador da plataforma (pode ser admin) |
| `Recipe` | Receita com nome, método, tempo e dificuldade |
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
- Registo e login
- Verificação de email duplicado
- Perfil com histórico de receitas, comentários e favoritos

### Receitas
- Criação, edição e remoção de receitas
- Pesquisa por nome, categoria, dificuldade ou utilizador
- Estado de publicação (`Status`)

### Interações
- **Comentários** — adicionar, editar e remover comentários por receita ou utilizador
- **Avaliações** — classificar receitas de 1 a 5 estrelas (uma avaliação por utilizador por receita)
- **Favoritos** — guardar e remover receitas favoritas, sem duplicados

---

## Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (ou outro provider compatível)

### Passos

```bash
# 1. Clonar o repositório
git clone https://github.com/teu-utilizador/tempero-do-dragao.git
cd tempero-do-dragao

# 2. Configurar a connection string em appsettings.json
# "ConnectionStrings": { "DefaultConnection": "Server=...;Database=TemperoDb;..." }

# 3. Aplicar as migrações
dotnet ef database update

# 4. Executar a aplicação
dotnet run
```

A API ficará disponível em `https://localhost:5001` por defeito.

---

## Estrutura do Projeto

```
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
├── Repositories/           # Acesso à base de dados
│   ├── Repository.cs           ← Base genérica
│   ├── RecipeRepository.cs
│   ├── UserRepository.cs
│   ├── CommentRepository.cs
│   ├── FavoriteRepository.cs
│   ├── RatingRepository.cs
│   └── SimpleRepositorie.cs    ← Category, Difficulty, Ingredient, Measurement
│
├── Services/               # Lógica de negócio
│   ├── RecipeService.cs
│   ├── UserService.cs
│   ├── CommentService.cs
│   ├── FavoriteService.cs
│   ├── RatingService.cs
│   └── SimpleService.cs        ← Category, Difficulty, Ingredient, Measurement
│
└── Data/
    └── AppDbContext.cs     # Contexto do Entity Framework
```

---

## Endpoints

> Documentação completa disponível via Swagger em `/swagger` após executar o projeto.

### Exemplos

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/api/users/register` | Registar utilizador |
| `POST` | `/api/users/login` | Login |
| `GET` | `/api/recipes` | Listar todas as receitas |
| `GET` | `/api/recipes/{id}` | Detalhe de uma receita |
| `POST` | `/api/recipes` | Criar receita |
| `GET` | `/api/recipes/search?name=` | Pesquisar por nome |
| `POST` | `/api/ratings` | Avaliar receita |
| `POST` | `/api/favorites` | Adicionar aos favoritos |
| `GET` | `/api/comments/recipe/{id}` | Comentários de uma receita |

---

## Notas de Desenvolvimento

- As avaliações estão limitadas a valores entre **1 e 5**, validadas na camada de serviço e com `CHECK constraint` na base de dados.
- Os favoritos não permitem duplicados — verificação feita antes de inserir.
- O campo `Status` nas entidades `User` e `Recipe` pode ser usado para soft delete ou moderação de conteúdo.

---

*Projeto desenvolvido em C# com ASP.NET Core.*
