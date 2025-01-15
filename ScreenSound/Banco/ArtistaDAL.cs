using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco
{
    public class ArtistaDAL
    {

        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            /* A instrução using faz com que aconteça um gerenciamento da execução por escopo, sendo assim,
            assim que o escopo no qual foi usado a conexão for encerrado, a conexão também será. */
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "select * from Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                // Converte para string o nome obtido pelo data reader, entre colchetes se refere à variável sql da tabela
                string nomeArtista = Convert.ToString(dataReader["Nome"]);

                string bioArtista = Convert.ToString(dataReader["Bio"]);
                int idArtista = Convert.ToInt32(dataReader["Id"]);

                Artista artista = new Artista(nomeArtista, bioArtista) { Id = idArtista };

                lista.Add(artista);
            }
            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas pela query: {retorno}");
        }

    }
}
