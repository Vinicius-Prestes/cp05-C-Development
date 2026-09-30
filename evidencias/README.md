# 📸 Evidências de Testes da API

Esta pasta armazena as capturas de tela (prints) demonstrando o funcionamento de cada endpoint da **CampusEventos API** via Swagger UI.

---

## 📋 Mapeamento das Evidências

| Imagem | Endpoint | Método | Descrição | Status Code |
|---|---|---|---|---|
| [`categoria-get.png`](./categoria-get.png) | `/api/v1/categorias` | `GET` | Listagem de todas as categorias com contagem de eventos | `200 OK` |
| [`categoria-getID.png`](./categoria-getID.png) | `/api/v1/categorias/{id}` | `GET` | Consulta de categoria por ID | `200 OK` |
| [`categoria-post.png`](./categoria-post.png) | `/api/v1/categorias` | `POST` | Cadastro de nova categoria | `201 Created` |
| [`categoria-put.png`](./categoria-put.png) | `/api/v1/categorias/{id}` | `PUT` | Atualização de categoria existente | `200 OK` |
| [`categoria-delete.png`](./categoria-delete.png) | `/api/v1/categorias/{id}` | `DELETE` | Exclusão de categoria sem eventos vinculados | `204 No Content` |
| [`eventos-get.png`](./eventos-get.png) | `/api/v1/eventos` | `GET` | Listagem geral de eventos cadastrados | `200 OK` |
| [`eventos-getID.png`](./eventos-getID.png) | `/api/v1/eventos/{id}` | `GET` | Consulta de evento por ID | `200 OK` |
| [`eventos-post.png`](./eventos-post.png) | `/api/v1/eventos` | `POST` | Cadastro de novo evento com chave estrangeira | `201 Created` |
| [`eventos-put.png`](./eventos-put.png) | `/api/v1/eventos/{id}` | `PUT` | Atualização dos dados de um evento | `200 OK` |
| [`eventos-delete.png`](./eventos-delete.png) | `/api/v1/eventos/{id}` | `DELETE` | Exclusão de um evento | `204 No Content` |
