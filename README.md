🚀 GitHubMonitor API
Avaliação de Desenvolvimento Backend Júnior em .NET
Este projeto foi desenvolvido como parte de uma avaliação prática para a vaga de Desenvolvedor Backend Júnior. O objetivo é demonstrar proficiência em tecnologias e boas práticas de desenvolvimento web com .NET 8.

📝 Visão Geral do Projeto
A GitHubMonitor API é uma API RESTful construída em ASP.NET Core 8.0. Sua principal funcionalidade é integrar-se com a API pública do GitHub para buscar dados de repositórios de um usuário e persistir essas informações em um banco de dados. Além disso, o projeto implementa funcionalidades extras como autenticação, autorização e testes de integração, conforme sugerido nos requisitos.

💻 Tecnologias e Padrões de Design
Linguagem: C# 12

Framework: ASP.NET Core 8.0

Banco de Dados: MongoDB (utilizado em um contêiner Docker para o ambiente de desenvolvimento)

Contêineres: Docker e Docker Compose

Padrões de Arquitetura: Clean Architecture / Domain-Driven Design (DDD)

Princípios: SOLID (aplicados na organização das camadas, interfaces e injeção de dependência)

Validação: FluentValidation

Segurança: Autenticação e Autorização com JWT (JSON Web Tokens)

Testes: Testes de Integração com xUnit, FluentAssertions e Bogus

Documentação: Swagger/OpenAPI

📁 Estrutura da Solução
O projeto está organizado em quatro camadas, seguindo o padrão de Clean Architecture:

GitHubMonitor.API (Camada de Apresentação): Ponto de entrada da aplicação. Contém os Controllers da API e a configuração da injeção de dependência e autenticação.

GitHubMonitor.Domain (Camada de Domínio): Coração da aplicação. Contém a lógica de negócio, entidades, DTOs, interfaces de serviço e validações. É independente das tecnologias de infraestrutura.

GitHubMonitor.Infra.Data (Camada de Infraestrutura): Responsável pela persistência dos dados. Contém a implementação dos repositórios e a classe de contexto para o MongoDB.

GitHubMonitor.Tests (Camada de Testes): Contém os testes de integração que validam o fluxo completo da aplicação.

▶️ Instruções de Configuração e Execução
Para rodar o projeto localmente, siga os passos abaixo:

Pré-requisitos
.NET 8 SDK

Docker Desktop

MongoDB Compass (Opcional, para visualizar os dados)

Passos
Clone o Repositório:

git clone [URL-DO-SEU-REPOSITORIO]
cd [pasta-do-projeto]


Inicie o Contêiner do MongoDB:
Navegue até a raiz da sua solução (onde o arquivo docker-compose.yml está localizado) e execute o seguinte comando para iniciar o contêiner do MongoDB:

docker-compose up -d


Configurações da API:

No arquivo appsettings.Development.json do projeto GitHubMonitor.API, configure a string de conexão para o seu banco de dados local:

"MongoDBSettings": {
  "ConnectionString": "mongodb://root:root@localhost:27017",
  "DatabaseName": "GitHubMonitorDB"
}


A string de conexão foi configurada para o ambiente de teste e desenvolvimento.

Execute a Aplicação:

Abra a solução no Visual Studio e execute o projeto GitHubMonitor.API. O Swagger UI será aberto no navegador.

🔑 Autenticação e Autorização
O projeto implementa autenticação e autorização com JWT. O fluxo é o seguinte:

Login:

Acesse o endpoint POST /api/Auth/login no Swagger.

Use as credenciais de teste para obter um token JWT:

{
  "email": "admin@email.com",
  "password": "Admin123"
}


A API retornará um token JWT.

Acesso a Endpoints Protegidos:

No Swagger, clique no cadeado de segurança (Authorization).

Cole o token obtido no formato Bearer SEU_TOKEN_AQUI.

Agora, você pode acessar o endpoint GET /api/GitHub/{username}.

### 🧪 Testes de Integração

![Resultado dos Testes de Integração](https://i.postimg.cc/VNzLfWS9/test.jpg)

O projeto inclui testes de integração para validar as funcionalidades de ponta a ponta.

Execute os testes:

No Visual Studio, vá para Teste > Gerenciador de Testes.

Clique em "Executar Todos os Testes".

Os testes irão validar o fluxo de login e a busca de repositórios com o token, garantindo que a API está funcionando como esperado.
