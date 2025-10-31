# Phonebook API

API REST de **Lista Telefônica** construída com **.NET 8**, **MongoDB**, **MediatR** e **FluentValidation**.  
Inclui estrutura em camadas, documentação Swagger, testes unitários e execução via **Docker Compose**.

## ✨ Recursos
- CRUD completo de **contatos** (`/contatos`)
- **MediatR** (Commands/Queries + Handlers) para desacoplamento
- **FluentValidation** para validação de entrada
- **MongoDB** como banco de dados
- **Swagger** para explorar/testar endpoints
- **Testes** com xUnit e Moq
- Pronto para Docker (API + Mongo)

## 🗂️ Estrutura
```
PhonebookApi/
  Phonebook.Api/            # Camada de apresentação (controllers, Program)
  Phonebook.Application/    # DTOs, Validators, MediatR (Commands/Queries/Handlers)
  Phonebook.Domain/         # Entidades de domínio (Contact, Address)
  Phonebook.Infrastructure/ # Mongo settings/context/repository
  Phonebook.Tests/          # Testes unitários (xUnit + Moq)
  PhonebookApi.sln
  docker-compose.yml
  README.md
```

## ⚙️ Requisitos
- **.NET SDK 8.0+**
- **Docker** (opcional, recomendado para subir Mongo rapidamente)

## ▶️ Como rodar

### Opção A) Docker Compose (recomendado)
Sobe **Mongo** e **API** juntos:
```bash
docker compose up --build
```
- Swagger: **http://localhost:8080/swagger**
- A API se conecta ao Mongo pelo host `mongo` (rede do compose).

### Opção B) Local (API) + Docker (Mongo)
1) Suba apenas o Mongo:
```bash
docker run -d --name mongo -p 27017:27017 mongo:7
# Se a porta estiver ocupada, use -p 27018:27017 e ajuste a connection string para mongodb://localhost:27018
```
2) Rode a API:
```bash
dotnet restore
dotnet build
dotnet run --project Phonebook.Api
```
- Swagger: porta exibida no console (ex.: `http://localhost:5181/swagger`)

## 🔧 Configuração
`Phonebook.Api/appsettings.json` (local):
```json
{
  "MongoSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "phonebook_db",
    "ContactsCollectionName": "contacts"
  }
}
```
No **Docker Compose**, a API já usa:
```
MongoSettings__ConnectionString = mongodb://mongo:27017
MongoSettings__DatabaseName     = phonebook_db
MongoSettings__ContactsCollectionName = contacts
```

> Dica: para trocar portas no host, ajuste `ports` no `docker-compose.yml` (ex.: `"8081:8080"`).

## 📚 Endpoints

Base: `http://localhost:8080` (Compose) ou a porta local mostrada no console.

### Criar
`POST /contatos` — retorna **201 Created** + corpo do contato
```json
{
  "nome": "Ana Souza",
  "telefone": "1199999-0000",
  "email": "ana@exemplo.com",
  "dataNascimento": "1995-05-10T00:00:00Z",
  "enderecos": [
    { "logradouro": "Rua A", "numero": "123", "cidade": "SP", "estado": "SP", "cep": "01000-000" }
  ]
}
```

### Listar
`GET /contatos` — **200 OK** + array

### Obter por id
`GET /contatos/{id}` — **200** ou **404**

### Atualizar (completo)
`PUT /contatos/{id}` — **204** ou **404**

### Atualizar (parcial)
`PATCH /contatos/{id}` — **204** ou **404**  
> Nesta implementação, o PATCH reaproveita o mesmo DTO do PUT.

### Excluir
`DELETE /contatos/{id}` — **204** ou **404**

## ✅ Validações (FluentValidation)
- `Nome`: obrigatório  
- `Telefone`: obrigatório  
- `Email`: formato válido  
- `Enderecos`: **mínimo 1** endereço  
Erros retornam **400 Bad Request** com mensagens.

## 🧪 Testes
Execute todos os testes:
```bash
dotnet test
```
Cobertura:
- `CreateContactHandlerTests` (MediatR handler com repositório mockado)
- `ContactValidatorsTests` (regras do FluentValidation)

## 🔍 Roteiro de teste manual (rápido)
1) **POST** `/contatos` (guarde o `id` retornado)  
2) **GET** `/contatos`  
3) **GET** `/contatos/{id}`  
4) **PUT** `/contatos/{id}`  
5) **PATCH** `/contatos/{id}`  
6) **DELETE** `/contatos/{id}`  
7) **GET** `/contatos/{id}` → deve retornar **404**  
8) **POST** inválido → **400** com mensagens do FluentValidation

## 🏗️ Decisões técnicas
- **Camadas**: Domain (entidades), Application (DTOs/Validators/MediatR), Infrastructure (Mongo), Api (Controllers/DI).
- **MediatR** separa Controllers (entrada HTTP) da lógica de caso de uso (Handlers), facilitando testes e manutenção.
- **MongoDB** simplifica o CRUD e não exige migrações.
- **FluentValidation** centraliza regras de entrada e mensagens.

## 🧭 Próximos passos (sugestões)
- **Paginação** e **filtros** (ex.: `GET /contatos?nome=ana&email=exemplo.com&page=1&pageSize=20`)
- **Autenticação JWT** (Bearer) e autorização por escopo
- **Logging/Observabilidade** (Serilog, OpenTelemetry)
- **CI** no GitHub Actions (build + test)
- **Health checks** (liveness/readiness)
- **Índices MongoDB** (nome/email) para acelerar buscas

## 🧰 Troubleshooting
- **Porta 27017 ocupada (Mongo)**  
  Use outra porta: `-p 27018:27017` e altere a connection string para `mongodb://localhost:27018`.
- **API não conecta no Mongo (Compose)**  
  `MongoSettings__ConnectionString` deve ser `mongodb://mongo:27017` (nome do serviço, **não** `localhost`).
- **Falha de build**  
  Rode `dotnet restore`, confirme .NET 8 (`dotnet --info`).
- **Swagger inacessível**  
  Verifique a porta mapeada (Compose: `8080:8080`) e o ambiente (`Development`).

## 📄 Licença
Uso livre para fins de avaliação técnica.
