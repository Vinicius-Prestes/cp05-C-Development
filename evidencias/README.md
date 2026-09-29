# 📸 Evidências de Testes da API

Esta pasta é destinada ao armazenamento das capturas de tela (prints) demonstrando o funcionamento de cada endpoint da **CampusEventos API** via Swagger UI, Postman ou Insomnia.

---

## 📋 Checklist de Prints Recomendados

| Arquivo | Endpoint | Método | Descrição / Teste Realizado | Status Code Esperado |
|---|---|---|---|---|
| `01_swagger_visao_geral.png` | `/swagger` | - | Visão geral da interface do Swagger com todos os endpoints mapeados | `200 OK` |
| `02_post_categoria.png` | `/api/v1/categorias` | `POST` | Criação de uma nova categoria com payload JSON | `201 Created` |
| `03_get_todas_categorias.png` | `/api/v1/categorias` | `GET` | Listagem de todas as categorias cadastradas | `200 OK` |
| `04_get_categoria_por_id.png` | `/api/v1/categorias/{id}` | `GET` | Consulta de uma categoria específica por ID | `200 OK` |
| `05_put_categoria.png` | `/api/v1/categorias/{id}` | `PUT` | Atualização dos dados de uma categoria | `200 OK` |
| `06_post_evento.png` | `/api/v1/eventos` | `POST` | Cadastro de um novo evento vinculado a uma categoria existente | `201 Created` |
| `07_get_todos_eventos.png` | `/api/v1/eventos` | `GET` | Listagem geral de eventos | `200 OK` |
| `08_get_eventos_filtro.png` | `/api/v1/eventos?termo=...`| `GET` | Filtro de eventos por termo ou categoria | `200 OK` |
| `09_get_evento_por_id.png` | `/api/v1/eventos/{id}` | `GET` | Consulta de detalhes de um evento específico por ID | `200 OK` |
| `10_put_evento.png` | `/api/v1/eventos/{id}` | `PUT` | Atualização dos dados de um evento | `200 OK` |
| `11_delete_evento.png` | `/api/v1/eventos/{id}` | `DELETE` | Remoção de um evento cadastrado | `204 No Content` |
| `12_delete_categoria.png` | `/api/v1/categorias/{id}` | `DELETE` | Remoção de categoria sem eventos vinculados | `204 No Content` |
| `13_erro_validacao_bad_request.png` | `/api/v1/eventos` | `POST` | Teste com dados inválidos (ex: sem título ou categoria inexistente) | `400 Bad Request` |
| `14_erro_nao_encontrado_not_found.png` | `/api/v1/eventos/999` | `GET` | Teste de consulta com ID inexistente | `404 Not Found` |

---

## 💡 Como Capturar e Salvar as Imagens

1. Inicie a API com `dotnet run --project CampusEventos.Api`.
2. Acesse `http://localhost:5161/swagger` no navegador (ou utilize a collection no Postman).
3. Execute as requisições na ordem sugerida acima ("Try it out" -> "Execute").
4. Tire um print da tela com a resposta (`Response Body` e `Response Code`).
5. Salve a imagem nesta pasta (`evidencias/`) com os nomes descritos na tabela acima.
