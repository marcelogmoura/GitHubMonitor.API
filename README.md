# 🚀 GitHubMonitor API

**Avaliação de Desenvolvimento Backend Júnior em .NET**

Este projeto foi desenvolvido como parte de uma avaliação prática para a vaga de Desenvolvedor Backend. O objetivo é demonstrar proficiência em tecnologias e boas práticas de desenvolvimento web com .NET 8, além de ir além dos requisitos propostos no teste e requisitos "não obrigatórios".

---

### 📝 Visão Geral do Projeto

A **GitHubMonitor API** é uma API RESTful robusta e escalável, construída em **ASP.NET Core 8.0**. Sua principal funcionalidade é integrar-se com a **API pública do GitHub**, permitindo a busca de repositórios por nome de usuário e a persistência dessas informações em um banco de dados **MongoDB**.

Este projeto se destaca por:

* ✅ **Integração segura com APIs externas:** Conecta-se à API pública do GitHub para buscar e processar dados de repositórios.
* ✅ **Persistência de dados eficiente:** Armazena informações relevantes em um banco de dados moderno (MongoDB).
* ✅ **Segurança robusta:** Implementa **autenticação e autorização com JWT**.
* ✅ **Qualidade de código:** É totalmente testável com **testes de integração** que garantem a estabilidade da aplicação.
* ✅ **Boas práticas de arquitetura:** Segue padrões como **Clean Architecture** e **DDD**, garantindo um código limpo e de fácil manutenção.

Além de atender aos requisitos básicos da avaliação, a solução demonstra conhecimento em:

* **Arquitetura e Padrões de Design:** Utiliza **Clean Architecture** e **DDD** para separar responsabilidades.
* **Segurança:** Implementa **autenticação e autorização com JWT**.
* **Testes de Qualidade:** Inclui **testes de integração** para garantir a estabilidade e o funcionamento de ponta a ponta da API.

---

### 💻 Tecnologias e Padrões de Design

* **Linguagem:** C# 12
* **Framework:** ASP.NET Core 8.0
* **Banco de Dados:** MongoDB
* **Contêineres:** Docker e Docker Compose
* **Padrões de Arquitetura:** Clean Architecture / Domain-Driven Design (DDD)
* **Princípios:** SOLID
* **Validação:** FluentValidation
* **Segurança:** JWT (JSON Web Tokens)
* **Testes:** xUnit, FluentAssertions e Bogus
* **Documentação:** Swagger/OpenAPI

---

### 📁 Estrutura da Solução

O projeto está organizado em quatro camadas, seguindo o padrão de **Clean Architecture**:

* `GitHubMonitor.API/` (Camada de Apresentação): Ponto de entrada da aplicação, contendo os **Controllers** e a configuração de injeção de dependência e autenticação.
* `GitHubMonitor.Domain/` (Camada de Domínio): Onde se encontra a **lógica de negócio**, entidades, DTOs e validações, independente das tecnologias de infraestrutura.
* `GitHubMonitor.Infra.Data/` (Camada de Infraestrutura): Responsável pela **persistência dos dados**, contendo a implementação dos repositórios para o **MongoDB**.
* `GitHubMonitor.Tests/` (Camada de Testes): Contém os **testes de integração** que validam o fluxo completo da aplicação.

![Arquitetura](https://i.postimg.cc/1RWbjccn/arch.jpg)

---

### ▶️ Instruções de Configuração e Execução

Para rodar o projeto localmente, siga os passos abaixo:

#### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)
* [MongoDB Compass](https://www.mongodb.com/products/compass) (Opcional, para visualizar os dados)

#### Passos
1.  **Clone o Repositório:**

    ```bash
    git clone [URL-DO-SEU-REPOSITORIO]
    cd [pasta-do-projeto]
    ```

2.  **Inicie o Contêiner do MongoDB:**

    Navegue até a raiz da sua solução (onde o arquivo `docker-compose.yml` está localizado) e execute:

    ```bash
    docker-compose up -d
    ```

3.  **Configurações da API:**

    No arquivo `appsettings.Development.json` do projeto `GitHubMonitor.API`, configure a string de conexão para o seu banco de dados local:

    ```json
    "MongoDBSettings": {
      "ConnectionString": "mongodb://root:root@localhost:27017",
      "DatabaseName": "GitHubMonitorDB"
    }
    ```

4.  **Execute a Aplicação:**

    Abra a solução no Visual Studio e execute o projeto `GitHubMonitor.API`. O **Swagger UI** será aberto no navegador.

---

### 🔑 Autenticação e Autorização

O projeto implementa autenticação e autorização com JWT, seguindo o fluxo abaixo:

1.  **Login:** Acesse o endpoint `POST /api/Auth/login` no Swagger e use as credenciais de teste para obter um token JWT:

    ```json
    {
      "email": "admin@email.com",
      "password": "Admin123"
    }
    ```

2.  **Acesso a Endpoints Protegidos:** No Swagger, clique no cadeado de segurança (**Authorization**) e cole o token no formato `Bearer SEU_TOKEN_AQUI`.

    ![Tela de Login do Swagger](https://i.postimg.cc/9QQCsFs1/Screenshot-1.jpg)

---

### 🖼️ Documentação e Endpoints

A documentação da API é gerada automaticamente pelo Swagger. Aqui você pode ver o endpoint protegido `GitHub/{username}`, que busca e persiste os dados de repositórios.

![Endpoint protegido no Swagger](https://i.postimg.cc/63LP9CvZ/Screenshot-3.jpg)

---

### 🧪 Testes de Integração

O projeto inclui testes de integração para validar as funcionalidades de ponta a ponta. Eles garantem que o fluxo de login e a busca de repositórios com o token estão funcionando como esperado.

![Resultado dos Testes de Integração](https://i.postimg.cc/VNzLfWS9/test.jpg)

**Execute os testes:**

No Visual Studio, vá para `Teste > Gerenciador de Testes` e clique em **"Executar Todos os Testes"**.

---

**Autor:** Marcelo Moura

**Contato:**  

📧 **Email:** mgmoura@gmail.com  

🔗 **LinkedIn:** https://www.linkedin.com/in/marcelogmoura/
