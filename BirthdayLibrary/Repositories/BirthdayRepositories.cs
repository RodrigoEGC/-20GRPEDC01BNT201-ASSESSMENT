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
            var cmdText = "UPDATE Birthday" +
                "SET	(Nome, Sobrenome, DataNascimento)" +
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
        public IEnumerable<BirthdayModel> GetById(int? Id)
        {
            var sqlQuery = "SELECT * FROM Birthday WHERE Id = " + Id;

            var birthdayList = new List<BirthdayModel> ();

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
                    
            }
            return birthdayList;
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

        BirthdayModel IBirthdayDB.GetById(int? Id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
