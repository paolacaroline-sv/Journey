# Journey 

Journey é uma API para gerenciamento de viagens e atividades, construído com **.NET 9.0** e seguindo os princípios de **Clean Architecture**. 

O projeto permite que usuários:
- Criem e gerenciem suas viagens com datas de início e fim
- Organizem atividades dentro de cada viagem
- Rastreiem o status de cada atividade (pendente/concluída)
- Removam viagens ou atividades quando necessário

O projeto tem como principal objetivo explorar práticas modernas de arquitetura de software em .NET, com ênfase na aplicação do Princípio da Responsabilidade Única (SRP), dos conceitos de SOLID e dos fundamentos de Domain-Driven Design (DDD).


## Tecnologias Utilizadas

- **.NET** — Framework
- **Entity Framework Core** — ORM para persistência
- **SQLite** — Banco de dados
- **FluentValidation** — Validação de modelos
- **AutoMapper** — Mapeamento de objetos (DTO/Entity)
- **Swagger/OpenAPI** — Documentação interativa de API


## Funcionalidades Principais

- **CRUD Completo de Viagens** — Criar, listar, buscar por ID e deletar viagens
- **Gerenciamento de Atividades** — Registrar atividades vinculadas a viagens
- **Rastreamento de Status** — Marcar atividades como concluídas ou pendentes
- **Validação de Domínio** — Usar FluentValidation para regras de negócio
- **Tratamento Centralizado de Exceções** — ExceptionFilter reutilizável
- **Mapeamento de Objetos** — AutoMapper para conversão DTO ↔ Entity
- **Operações Assíncronas** — Async/Await em use cases e endpoints
- **Documentação Interativa** — Swagger/OpenAPI integrado
- **Persistência com SQLite** — Banco de dados local e portável
- **Arquitetura em Camadas** — Separação clara de responsabilidades


## Pré-Requisitos

Antes de começar, certifique-se de que você tem instalado:

- **.NET SDK 9.0** ou superior
- **Git** para clonar o repositório
- **Editor/IDE** recomendado: [Visual Studio Code](https://code.visualstudio.com/)
- **Navegador web** (para acessar Swagger UI)


## Instalação & Setup

### 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/paolacaroline-sv/journey
cd journey
```

### 2️⃣ Restaurar Dependências

```bash
dotnet restore
```

### 3️⃣ Compilar o Projeto

```bash
dotnet build
```

### 4️⃣ O Banco de Dados

O projeto utiliza um banco de dados SQLite para armazenar as informações e já inclui algumas viagens pré-configuradas, facilitando a exploração das funcionalidades desde o início.

📁 **Caminho do banco:**
`src/Journey.Infrastructure/JourneyDatabase.db`

🔎 **Visualização dos dados:**
Ferramentas como **DBeaver** ou **DB Browser for SQLite** podem ser usadas para visualizar o banco de dados, mas seu uso é opcional.


### 5️⃣ Executar a Aplicação

```bash
dotnet run --project src/Journey.Api
```

A API estará disponível em: **`http://localhost:5000`**


## Documentação Swagger

### Acessar Documentação Interativa (Swagger UI)

Abra o navegador e visite:

```
http://localhost:5000
```

Você verá a documentação completa de todos os endpoints com a opção de testar diretamente pela interface.

## Agradecimentos

Desenvolvido como parte da jornada de aprendizado em Clean Architecture e .NET. Agradeço ao professor pela orientação e a você por dedicar seu tempo para explorar este projeto.

---

<div align="center">


[↑ Voltar ao topo ⭐](#-journey-api)

</div>
