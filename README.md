# Back-end .NET

Este projeto implementa uma API REST para gerenciar o faturamento de clientes utilizando .NET Core 8 e Entity Framework Core. A API oferece funcionalidades para CRUD de clientes e produtos, além de controle e importação de dados de billing.

## Funcionalidades 🛠️

- **Customer: CRUD**
  - Permite criar, ler, atualizar e deletar clientes.
  - Campos:
    - `Id`
    - `Name`
    - `Email`
    - `Address`

- **Products: CRUD**
  - Permite criar, ler, atualizar e deletar produtos.
  - Campos:
    - `Id`
    - `Nome do produto`

- **Controle de Conferência e Importação de Billing**
  - Verifica a existência de clientes e produtos e insere registros de billing e billingLines no banco de dados local.
  - Se um cliente ou produto estiver ausente, retorna um erro informando a necessidade de criar o registro faltante.
  - Implementa tratamento de exceções para lidar com falhas e interrupções de serviço nas APIs externas.

## Tecnologias Utilizadas 🔧

- **.NET Core 8**
- **Entity Framework Core (EF Core)**

## Configuração e Execução

1. **Clone o Repositório**
   ```bash
   git clone <URL do repositório>
2. **Navegue até o Diretório do Projeto WebApi**
   ```bash
   cd <nome-do-projeto>
3. **Restaure as Dependências**
   ```bash
   dotnet restore
4. **Execute a Aplicação**
   ```bash
   dotnet run
5. **Acesse a API**
    - A API estará disponível em http://localhost:5000.
    - Utilize Swagger para testar as APIs em http://localhost:5000/swagger.
