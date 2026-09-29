# 🎓 CampusEventos API

> **Disciplina:** C# Software Development  
> **Tema:** Livre — Gestão de Eventos Acadêmicos e Workshops  
> **Tecnologias:** C# .NET 10, ASP.NET Core Web API, Entity Framework Core, Oracle Database  

---

## 👥 Integrantes do Grupo

| RM | Nome Completo |
|---|---|
| RM99999 | Nome do Aluno 1 |
| RM99999 | Nome do Aluno 2 |
| RM99999 | Nome do Aluno 3 |
| RM99999 | Nome do Aluno 4 |
| RM99999 | Nome do Aluno 5 |

*(Substitua os dados acima pelos RMs e nomes dos integrantes do seu grupo)*

---

## 📌 Contexto do Projeto

### O que é?
A **CampusEventos API** é uma API RESTful desenvolvida para gerenciar o ciclo de vida de eventos acadêmicos, palestras, workshops e hackathons realizados em ambiente universitário ou corporativo.

### Qual problema resolve?
A organização de eventos acadêmicos frequentemente sofre com a falta de centralização: informações desatualizadas sobre locais e horários, dificuldade em controlar a capacidade das salas/laboratórios e falta de categorização das atividades. A API resolve esse problema oferecendo um catálogo padronizado onde organizadores podem criar, consultar, atualizar e cancelar eventos com controle de vagas e categorização estruturada.

### Para quem é destinado?
- **Coordenadores e organizadores de eventos acadêmicos:** para cadastrar salas, horários, capacidade e descrições das atividades.
- **Alunos e participantes:** para consultar a agenda de workshops, palestras e conferências disponíveis no campus.

---

## 🗄️ Banco de Dados

- **SGBD:** Oracle Database
- **ORM:** Entity Framework Core (`Oracle.EntityFrameworkCore` v10.23)
- **Tabelas Mapeadas:**
  - `TB_CATEGORIAS`: Armazena as categorias dos eventos (ex: Palestra, Workshop, Hackathon).
  - `TB_EVENTOS`: Armazena os eventos, com relacionamento de chave estrangeira (`CategoriaId`) para `TB_CATEGORIAS`.

---

## ⚙️ Configuração das Credenciais do Oracle

Para garantir a segurança das credenciais e permitir que qualquer avaliador teste a aplicação utilizando seu próprio usuário do Oracle (por exemplo, a conta da FIAP), a connection string no arquivo `appsettings.json` está com campos para preenchimento.

### Passo 1: Abrir o arquivo de configuração
Abra o arquivo [`CampusEventos.Api/appsettings.json`](file:///c:/Users/labsfiap/Desktop/CP05%20-%20C/CampusEventos.Api/appsettings.json):

```json
{
  "ConnectionStrings": {
    "OracleConnection": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id=INSIRA_SEU_RM_AQUI;Password=INSIRA_SUA_SENHA_AQUI;"
  }
}
```

### Passo 2: Inserir seus dados
Substitua:
- `INSIRA_SEU_RM_AQUI` pelo seu usuário do Oracle (ex: `RM12345`).
- `INSIRA_SUA_SENHA_AQUI` pela sua senha do Oracle.

> **Nota:** Caso utilize outro servidor Oracle local ou em nuvem, você pode alterar o `HOST`, `PORT` e `SERVICE_NAME` conforme sua necessidade.

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
- [.NET SDK 10](https://dotnet.microsoft.com/download) instalado.
- Ferramenta global do EF Core (caso queira rodar comandos `dotnet ef`):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Passo a Passo

1. **Restaurar dependências:**
   ```bash
   dotnet restore
   ```

2. **Aplicar as Migrations no Banco Oracle:**
   Após configurar suas credenciais no `appsettings.json`, execute:
   ```bash
   dotnet ef database update --project CampusEventos.Api
   ```
   *(Esse comando criará automaticamente as tabelas `TB_CATEGORIAS` e `TB_EVENTOS` com os dados iniciais de seed no seu schema do Oracle)*.

3. **Compilar a solução:**
   ```bash
   dotnet build
   ```

4. **Executar a API:**
   ```bash
   dotnet run --project CampusEventos.Api
   ```

5. **Acessar a documentação interativa (Swagger UI):**
   Abra o navegador no endereço:
   - 👉 **http://localhost:5161/swagger**
   - ou **https://localhost:7188/swagger**

---

## 🔄 Versionamento da API

A API adota versionamento explícito via URL utilizando o pacote `Asp.Versioning.Mvc`, seguindo o padrão `/api/v{version}/[controller]`.

- Versão atual: **v1**
- Exemplos de rotas:
  - `/api/v1/categorias`
  - `/api/v1/eventos`

---

## 🛣️ Endpoints Disponíveis

### 📂 Categorias (`/api/v1/categorias`)

| Método | Rota | Descrição | Status Codes |
|---|---|---|---|
| `GET` | `/api/v1/categorias` | Lista todas as categorias cadastradas com total de eventos vinculados | `200 OK` |
| `GET` | `/api/v1/categorias/{id}` | Busca os dados de uma categoria específica por ID | `200 OK`, `404 Not Found` |
| `POST` | `/api/v1/categorias` | Cadastra uma nova categoria | `201 Created`, `400 Bad Request` |
| `PUT` | `/api/v1/categorias/{id}` | Atualiza o nome, descrição ou status ativo de uma categoria | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/v1/categorias/{id}` | Exclui uma categoria (apenas se não houver eventos vinculados a ela) | `204 No Content`, `400 Bad Request`, `404 Not Found` |

### 📅 Eventos (`/api/v1/eventos`)

| Método | Rota | Descrição | Status Codes |
|---|---|---|---|
| `GET` | `/api/v1/eventos` | Lista todos os eventos ordenados por data. Permite filtros opcionais por `termo` (busca em título/descrição) e `categoriaId` | `200 OK` |
| `GET` | `/api/v1/eventos/{id}` | Busca os dados detalhados de um evento específico por ID | `200 OK`, `404 Not Found` |
| `POST` | `/api/v1/eventos` | Cria um novo evento (valida existência da categoria e capacidade positiva) | `201 Created`, `400 Bad Request` |
| `PUT` | `/api/v1/eventos/{id}` | Atualiza os dados de um evento existente | `200 OK`, `400 Bad Request`, `404 Not Found` |
| `DELETE` | `/api/v1/eventos/{id}` | Exclui um evento do sistema | `204 No Content`, `404 Not Found` |

---

## 📦 Exemplos de Payloads (JSON)

### Criar Categoria (`POST /api/v1/categorias`)
```json
{
  "nome": "Minicurso",
  "descricao": "Cursos rápidos e práticos para estudantes"
}
```

### Criar Evento (`POST /api/v1/eventos`)
```json
{
  "titulo": "Workshop de C# e Entity Framework",
  "descricao": "Capacitação prática em APIs REST com .NET 10 e Oracle",
  "dataHora": "2026-10-25T19:00:00",
  "local": "Laboratório 502 - Campus Paulista",
  "capacidadeMaxima": 45,
  "preco": 0.0,
  "categoriaId": 1
}
```

---

## 🗃️ Migrations Documentadas

A migração inicial foi gerada utilizando o Entity Framework Core Tools e encontra-se na pasta `CampusEventos.Api/Migrations`:

- `20260929214810_InitialCreate.cs`: Contém as instruções DDL para criação das tabelas `TB_CATEGORIAS` e `TB_EVENTOS`, chaves primárias, chave estrangeira com restrição de deleção e inserção de dados iniciais (*seed*).

Comando utilizado para criação:
```bash
dotnet ef migrations add InitialCreate --project CampusEventos.Api
```

---

## 📸 Evidências de Testes

As capturas de tela demonstrando o funcionamento de cada endpoint da API via Swagger / Postman estão disponíveis na pasta:
- 📁 [`evidencias/`](./evidencias/)

Consulte o arquivo [`evidencias/README.md`](./evidencias/README.md) para verificar o checklist completo dos testes realizados.
