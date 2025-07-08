# TodoApi - Guia de Execução

Este projeto é uma API para gerenciamento de tarefas (ToDo) desenvolvida em ASP.NET Core. Abaixo você encontra instruções para executar o projeto localmente utilizando Docker Compose ou Docker padrão.

---

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download) (opcional, apenas para rodar sem Docker)
- [Docker]
- [DockerCompose] (opcional, mas recomendado)

---

## Executando com Docker Compose

1. **Abra o terminal na pasta raiz do projeto** (onde está o arquivo `compose.yaml`).

2. **Execute o comando abaixo:**

   ```
   docker-compose up
   ```

   > Isso irá construir a imagem e subir o container da API na porta `5000`.

3. **Acesse a API:**

   - URL base: [http://localhost:5000/swagger]
---

## Executando com Docker

1. **Abra o terminal na pasta raiz do projeto**.

2. **Construa a imagem Docker:**

   ```
   docker build -t todoapi:dev -f ToDo-Api/Dockerfile .
   ```

3. **Execute o container:**

   ```
   docker run -d -p 5000:8080 --name todoapi todoapi:dev
   ```

4. **Acesse a API:**

 - URL base: [http://localhost:5000/swagger]
---

## Endpoints Principais

- `GET    /api/tasks` — Lista todas as tarefas (com filtros opcionais de status e datas)
- `POST   /api/tasks` — Cria uma nova tarefa
- `PUT    /api/tasks` — Atualiza uma tarefa existente
- `DELETE /api/tasks/{id}` — Remove uma tarefa pelo ID

---

## Observações

- O banco de dados utilizado é **InMemory**, ou seja, os dados são perdidos ao reiniciar a aplicação.
---