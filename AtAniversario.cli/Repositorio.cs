using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace AtAniversario.cli
{
    class Repositorio
    {
        private static string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\AniversarioAmigosDB.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }
        public static IEnumerable<Amigo> BuscarTodosOsAmigos()
        {
            string nomeDoArquivo = ObterNomeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            //identificar cada amigo
            string[] amigos = resultado.Split(';');

            List<Amigo> amigosList = new List<Amigo>();

            for (int i = 0; i < amigos.Length - 1; i++)
            {
                string[] dadosDoAmigo = amigos[i].Split(',');

                //identificar cada dado do amigo
                string nome = dadosDoAmigo[0];
                string sobrenome = dadosDoAmigo[1];
                DateTime dataNascimento = Convert.ToDateTime(dadosDoAmigo[2]);
                DateTime dataCadastro = Convert.ToDateTime(dadosDoAmigo[3]);


                //preencher a classe funcionario com esses dados
                Amigo amigo = new Amigo(nome, sobrenome, dataNascimento, dataCadastro);

                //adicionar em uma lista esse funcionario
                amigosList.Add(amigo);
            }

            //retornar a lista
            return amigosList;
        }
        public static void CadastrarAmigo(Amigo amigo)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string formato = $"{amigo.Nome},{amigo.Sobrenome},{amigo.DataDeNascimento.ToString()},{amigo.DataDeCadastro.ToString()};";

            File.AppendAllText(nomeDoArquivo, formato);
        }
        public static IEnumerable<Amigo> BuscarTodosOsAmigos(string nome)
        {
            //SEE: https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/
            return (from x in BuscarTodosOsAmigos()
                    where x.Nome.Contains(nome)
                    orderby x.Nome
                    select x);
        }
        public static IEnumerable<Amigo> BuscarTodosOsAmigos(DateTime dataNascimento)
        {
            //SEE: https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/
            return (from x in BuscarTodosOsAmigos()
                    where x.DataDeNascimento == dataNascimento
                    orderby x.Nome
                    select x);
        }

    }


}
    

