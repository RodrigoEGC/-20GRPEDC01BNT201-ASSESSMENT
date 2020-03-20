using BirthdayLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayLibrary.Repositories
{
    public class BirthdayRepositories: IBirthdayDB
    {
        private readonly string _connectionString;
        public BirthdayRepositories(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("MdfConnectionString");
        }
        
        

        public void Insert(BirthdayModel birthdayModel)
        {
            var cmdText = "INSERT INTO Birthday" +
                "		(Nome, Sobrenome, DataNascimento)" +
                "VALUES	(@nome, @sobrenome, @dataNascimento);";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@nome", SqlDbType.VarChar).Value = birthdayModel.Nome;
                sqlCommand.Parameters
                    .Add("@sobrenome", SqlDbType.VarChar).Value = birthdayModel.Sobrenome;
                sqlCommand.Parameters
                    .Add("@dataNascimento", SqlDbType.DateTime).Value = birthdayModel.DataNascimento;
                sqlConnection.Open();

                var resutScalar = sqlCommand.ExecuteScalar();
            }
        }

        public void Update(BirthdayModel birthdayModel)
        {
            var cmdText = "UPDATE Birthday SET Nome = @nome, Sobrenome = @sobrenome, DataNascimento = @dataNascimento WHERE Id = @id";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@nome", SqlDbType.VarChar).Value = birthdayModel.Nome;
                sqlCommand.Parameters
                    .Add("@sobrenome", SqlDbType.VarChar).Value = birthdayModel.Sobrenome;
                sqlCommand.Parameters
                    .Add("@dataNascimento", SqlDbType.DateTime).Value = birthdayModel.DataNascimento;
                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = birthdayModel.Id;
                
                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }
        public BirthdayModel GetById(int Id)
        {
           BirthdayModel birthdayModel = new BirthdayModel();

            var sqlQuery = "SELECT * FROM Birthday WHERE Id = " + Id;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
                using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;

                    sqlConnection.Open();

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        var idColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Id));
                        var nomeColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Nome));
                        var sobrenomeColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Sobrenome));
                        var dataNascimentoColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.DataNascimento));
                        while (reader.Read())
                        {
                            birthdayModel.Id = reader.GetFieldValue<int>(idColumnIndex);
                            birthdayModel.Nome = reader.GetFieldValue<string>(nomeColumnIndex);
                            birthdayModel.Sobrenome = reader.GetFieldValue<string>(sobrenomeColumnIndex);
                            birthdayModel.DataNascimento = reader.GetFieldValue<DateTime>(dataNascimentoColumnIndex);
                        }
                    }
                }

            }
            return birthdayModel;
        }

        public IEnumerable<BirthdayModel> GetAll()
        {
            var cmdText = $"SELECT " +
                $"Id as '{nameof(BirthdayModel.Id)}', " +
                $"Nome as '{nameof(BirthdayModel.Nome)}', " +
                $"Sobrenome as '{nameof(BirthdayModel.Sobrenome)}', " +
                $"DataNascimento as '{nameof(BirthdayModel.DataNascimento)}' " +
                $"FROM Birthday ORDER BY DataNascimento DESC";

            var birthdayList = new List<BirthdayModel>();

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    var idColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Id));
                    var nomeColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Nome));
                    var sobrenomeColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.Sobrenome));
                    var dataNascimentoColumnIndex = reader.GetOrdinal(nameof(BirthdayModel.DataNascimento));
                    while (reader.Read())
                    {
                        var id = reader.GetFieldValue<int>(idColumnIndex);
                        var nome = reader.GetFieldValue<string>(nomeColumnIndex);
                        var sobrenome = reader.GetFieldValue<string>(sobrenomeColumnIndex);
                        var dataNascimento = reader.GetFieldValue<DateTime>(dataNascimentoColumnIndex);
                        
                        var entry = new BirthdayModel
                        {
                            Id = id,
                            Nome = nome,
                            Sobrenome = sobrenome,
                            DataNascimento = dataNascimento
                        };
                        birthdayList.Add(entry);
                    }
                }
            }

            return birthdayList;
        }


        public void Delete(int id)
        {
            var sql = "DELETE FROM Birthday WHERE Id = @id";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(sql, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();
            }
        }

        public List<BirthdayModel> Buscar(int porPagina, int paginaCorreta)
        {
            var pagination = new List<BirthdayModel>();
            var offset = 1;
            if (paginaCorreta > 1)
            {
                offset = (porPagina * (paginaCorreta - 1)) + 1;
            }
            var sql = "DECLARE @Limit INT = " + porPagina + ", @Offset INT = " + offset + "; WITH resultado As(SELECT *, ROW_NUMBER() OVER(ORDER BY ID) AS Linha FROM Birthday WHERE Id is not null) SELECT * FROM resultado WHERE linha >= @Offset AND linha<@Offset +@limit";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(sql, sqlConnection)) //já faz o close
            {
                sqlConnection.Open();
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagination.Add((new BirthdayModel()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString()
                        }));
                    }
                }
                return pagination;
            }
        }

        public int Total()
        {
            var sql = "SELECT count(*) FROM Birthday";
            var total = 0;

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(sql, sqlConnection)) //já faz o close
            {
                sqlConnection.Open();
                total = Convert.ToInt32(sqlCommand.ExecuteScalar());
                
            }
            return total;

        }
    }
}
