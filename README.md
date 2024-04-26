# API de Gerenciamento de Clientes
Este é um projeto de API desenvolvido em C# usando o framework ASP.NET Core para gerenciamento de clientes. Ele permite a criação e a recuperação de dados de clientes armazenados em um banco de dados MySQL.

## Pré-requisitos
Antes de iniciar, certifique-se de ter os seguintes requisitos:

- .NET Core SDK instalado na sua máquina.
- Um servidor MySQL disponível para conexão.
- Um ambiente de desenvolvimento integrado (IDE) de sua escolha, como Visual Studio ou Visual Studio Code.

## Configuração do Banco de Dados
Este projeto está configurado para se conectar a um banco de dados MySQL. Antes de executá-lo, você precisa criar um banco de dados e configurar a string de conexão com suas credenciais de acesso ao banco de dados.

Exemplo de configuração do banco de dados no arquivo ClienteController.cs e Program.cs
```
private string _connectionString = "Server=localhost;Database=testebackend;Uid=root;Pwd=;";
```

É necessário também criar uma tabela com os seguintes campos:

- Nome completo
- Data de nascimento
- Tipo Pessoa
- CPF/CNPJ 
- E-mail
- Endereço

## Instalação e Execução
Para executar este projeto em sua máquina, siga estas etapas:

1. Clone este repositório ou baixe o código-fonte.
2. Abra o projeto em sua IDE.
3. Certifique-se de que a configuração do banco de dados esteja correta.


## Instalando dependência
É necessario realizar a instalação do seguinte pacote:
```
dotnet add package MySql.Data
```

## Uso da API
A API fornece os seguintes endpoints:

- GET /api/cliente/{id}: Retorna os dados de um cliente específico com base no ID fornecido.
- POST /api/cliente: Cria um novo cliente com os dados fornecidos.

## Exemplo de Requisição POST
```
POST /api/cliente
Content-Type: application/json

{
  "NomeCompleto": "Fulano de Tal",
  "DataNascimento": "1990-01-01",
  "TipoPessoa": "F",
  "CPFCNPJ": "123.456.789-00",
  "Email": "fulano@example.com",
  "Endereco": "Rua Exemplo, 123"
}
```

Após todas as etapas execute o projeto.

