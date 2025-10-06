# GamePlatform.Pagamentos

## 📋 Sobre o Projeto
**GamePlatform.Pagamentos** é uma API RESTful desenvolvida em **.NET 8.0**, responsável pelo gerenciamento e processamento de pagamentos na plataforma GamePlatform.  
Essa API faz parte da arquitetura baseada em **microsserviços**, cuidando das operações de iniciação, confirmação, consulta e histórico de pagamentos dos usuários.

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Arquitetura Hexagonal (Ports & Adapters)**, promovendo separação clara entre regras de negócio, integrações externas e interfaces:

- **GamePlatform.Pagamentos.Api**: Camada de apresentação que expõe os endpoints REST.
- **GamePlatform.Pagamentos.Application**: Casos de uso e lógica de aplicação referentes ao fluxo de pagamentos.
- **GamePlatform.Pagamentos.Domain**: Entidades de domínio, regras de negócio e contratos de portas.
- **GamePlatform.Pagamentos.Infrastructure**: Implementação de adaptadores para persistência, mensageria, gateways de pagamento, etc.
- **GamePlatform.Pagamentos.Tests**: Testes unitários e de integração.

---

## 🚀 Como Executar

### Pré-requisitos
- .NET SDK 8.0 ou superior  
- Banco de dados (por exemplo: PostgreSQL)  
- Event Bus (ex: Azure Service Bus) configurado  
- IDE compatível (Visual Studio, JetBrains Rider ou VS Code)

### Passos para Execução

1. Clone o repositório:
```bash
git clone https://github.com/gameplatform-team/GamePlatform.Pagamentos.git
```

2. Navegue até a pasta do projeto:
```bash
cd GamePlatform.Pagamentos
```

3. Restaure as dependências:
```bash
dotnet restore
```

4. Execute a aplicação:
```bash
cd Adapters/Driving/Apis/GamePlatform.Pagamentos.Api
```
```bash
dotnet run
```

A API estará disponível em `http://localhost:8082`.

Você pode executar as requisições através do Swagger: `http://localhost:8082/swagger/index.html`.

## 🧩 Principais Funcionalidades
- Iniciação e confirmação de pagamentos
- Consulta de status e histórico de pagamentos
- Integração com gateways externos de pagamento
- Publicação de eventos em filas (mensageria)
- Camada hexagonal para fácil adaptação de integrações

## 🧪 Executando os Testes

Para executar os testes unitários:
```bash
dotnet test
```

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 8.0
- C# 12.0
- Docker
- Event Bus (Azure Service Bus)
- Banco de Dados relacional
- Arquitetura Hexagonal
- Testes Unitários (xUnit)

## 📦 Estrutura da Solução

```plaintext
GamePlatform.Pagamentos/
├── Adapters/
│   ├── Driven/
│   │   └── Infrastructure/
│   │       └── GamePlatform.Pagamentos.Infrastructure/   # Adaptadores (banco, mensageria, gateways)
│   └── Driving/
│       └── Apis/
│           └── GamePlatform.Pagamentos.Api/              # API endpoints e configurações
├── Core/
│   ├── Application/
│   │   └── GamePlatform.Pagamentos.Application/          # Casos de uso e lógica de aplicação
│   └── Domain/
│       └── GamePlatform.Pagamentos.Domain/               # Entidades, contratos e portas
└── Tests/
    └── GamePlatform.Pagamentos.Tests/                    # Testes unitários e integração
```

## 🔄 CI/CD

O projeto utiliza GitHub Actions para automação de CI/CD, incluindo:
- Build e testes automatizados
- Build e push de imagem Docker
- Deploy automatizado no Azure Container Apps
