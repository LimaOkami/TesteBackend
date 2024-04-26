using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // Configurações de conexão do banco
        
        private string _connectionString = "Server=localhost;Database=testebackend;Uid=root;Pwd=;";

        // Inicio da requisição GET
        
        [HttpGet("{id}")]
        public IActionResult ObterDadosPorId(int id)
        {
            try
            {
                Dictionary<string, object> dados = new Dictionary<string, object>();

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT NomeCompleto, DataNascimento, TipoPessoa, CPF_CNPJ, Email, Endereco FROM cliente WHERE id = @Id";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dados["NomeCompleto"] = reader.GetString("NomeCompleto");
                                dados["DataNascimento"] = reader.GetDateTime("DataNascimento").ToString("yyyy-MM-dd");
                                dados["TipoPessoa"] = reader.GetString("TipoPessoa");
                                dados["CPFCNPJ"] = reader.GetString("CPF_CNPJ");
                                dados["Email"] = reader.GetString("Email");
                                dados["Endereco"] = reader.GetString("Endereco");
                            }
                        }
                    }
                }

                if (dados.Count == 0)
                {
                    return StatusCode(404, $"Nenhum resultado encontrado para esse ID");
                }

                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

         // Inicio da requisição POST
         
        [HttpPost]
        public IActionResult CriarCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO cliente (NomeCompleto, DataNascimento, TipoPessoa, CPF_CNPJ, Email, Endereco) " +
                                 "VALUES (@NomeCompleto, @DataNascimento, @TipoPessoa, @CPFCNPJ, @Email, @Endereco)";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NomeCompleto", cliente.NomeCompleto);
                        command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                        command.Parameters.AddWithValue("@TipoPessoa", cliente.TipoPessoa);
                        command.Parameters.AddWithValue("@CPFCNPJ", cliente.CPFCNPJ);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Endereco", cliente.Endereco);

                        command.ExecuteNonQuery();
                    }
                }

                return CreatedAtAction(nameof(ObterDadosPorId), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        public class Cliente
        {
            // Inicio das validações Obrigatórias do metodo POST
            
            public int Id { get; set; } // Id será preenchido automaticamente pelo banco de dados
            [Required(ErrorMessage = "O campo NomeCompleto é obrigatório.")]
            public string NomeCompleto { get; set; }
            [Required(ErrorMessage = "O campo DataNascimento é obrigatório.")]
            public DateTime DataNascimento { get; set; }
            [Required(ErrorMessage = "O campo TipoPessoa é obrigatório.")]
            [RegularExpression("^(F|J)$", ErrorMessage = "O campo TipoPessoa deve ser 'F' (Física) ou 'J' (Jurídica).")]
            public string TipoPessoa { get; set; }
            [Required(ErrorMessage = "O campo CPFCNPJ é obrigatório.")]
            [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$", ErrorMessage = "O CPF/CNPJ deve estar no formato xxx.xxx.xxx-xx ou xx.xxx.xxx/xxxx-xx.")]
            public string CPFCNPJ { get; set; }
            [Required(ErrorMessage = "O campo Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "O Email deve ser um endereço de email válido.")]
            public string Email { get; set; }
            [Required(ErrorMessage = "O campo Endereco é obrigatório.")]
            public string Endereco { get; set; }
        }
    }
}
