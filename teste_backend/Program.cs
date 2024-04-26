using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

string connectionString = "Server=localhost;Database=testebackend;Uid=root;Pwd=;";

using (MySqlConnection connection = new MySqlConnection(connectionString))
{
    connection.Open();

    // Verificar se a tabela está vazia
    string countQuery = "SELECT COUNT(*) FROM cliente";
    MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
    int count = Convert.ToInt32(countCommand.ExecuteScalar());

    if (count == 0)
    {
        // Se a tabela estiver vazia, inserir dados fictícios
        string insertQuery = "INSERT INTO cliente (NomeCompleto, DataNascimento, TipoPessoa, CPF_CNPJ, Email, Endereco) " +
                             "VALUES (@NomeCompleto, @DataNascimento, @TipoPessoa, @CPFCNPJ, @Email, @Endereco)";

        var dadosFicticios = new[]
                            {
                                new { NomeCompleto = "Fulano de Tal", DataNascimento = new DateTime(1990, 1, 1), TipoPessoa = "F", CPFCNPJ = "123.456.789-00", Email = "fulano@example.com", Endereco = "Rua Exemplo, 123" },
                                new { NomeCompleto = "Beltrano da Silva", DataNascimento = new DateTime(1985, 5, 15), TipoPessoa = "J", CPFCNPJ = "12.345.678/0001-99", Email = "beltrano@example.com", Endereco = "Avenida Teste, 456" },
                                new { NomeCompleto = "Ciclano Oliveira", DataNascimento = new DateTime(1995, 10, 30), TipoPessoa = "F", CPFCNPJ = "987.654.321-00", Email = "ciclano@example.com", Endereco = "Praça do Exemplo, 789" }
                            };

        // Inserir dados fictícios
        foreach (var dados in dadosFicticios)
        {
            using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@NomeCompleto", dados.NomeCompleto);
                command.Parameters.AddWithValue("@DataNascimento", dados.DataNascimento);
                command.Parameters.AddWithValue("@TipoPessoa", dados.TipoPessoa);
                command.Parameters.AddWithValue("@CPFCNPJ", dados.CPFCNPJ);
                command.Parameters.AddWithValue("@Email", dados.Email);
                command.Parameters.AddWithValue("@Endereco", dados.Endereco);

                command.ExecuteNonQuery();
            }
        }
    }
}


app.Run();
