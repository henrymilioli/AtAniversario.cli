using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AtAniversario.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            AmigoDoDia();
            MainMenu();
        }

        private static void AmigoDoDia()
        {
            Console.WriteLine(" # Aniversáriantes De Hoje #");

            foreach (var amigo in Repositorio.BuscarTodosOsAmigos())
            {
                if (DateTime.Now.Month == amigo.DataDeNascimento.Month && DateTime.Now.Day == amigo.DataDeNascimento.Day)
                {
                    Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                    Console.WriteLine($"Idade: {((DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12) - 1}");
                    Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                    Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("");
                }

            }

            Console.WriteLine("");
        }

        public static void MainMenu()
        {
            Console.WriteLine(" #CADASTRO DE AMIGOS# ");
            Console.WriteLine("");
            Console.WriteLine("Selecione uma operação");
            Console.WriteLine("1 -> Cadastrar Amigo");
            Console.WriteLine("2 -> Consultar Amigos Cadastrados");
            Console.WriteLine("3 -> Pesquisar Aniversario De Amigo Pelo Nome");
            Console.WriteLine("4 -> Pesquisar Pela Data");
            Console.WriteLine("5 -> Excluir Amigo");
            Console.WriteLine("6 -> Editar Amigo");
            Console.WriteLine("7 -> Sair");

            Console.WriteLine("Informe a opção desejada:");
            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1':
                    Console.WriteLine(" # Amigos Cadastrados # ");
                    Console.WriteLine("");
                    CadastrarAmigo(); break;

                case '2':
                    Console.WriteLine(" # Cadastrar Amigos # ");
                    ConsultarAmigo(); break;
                case '3':
                    Console.WriteLine(" # Pesquisar por Nome # ");
                    ConsultarAmigoPeloNome(); break;
                case '4':
                    Console.WriteLine(" # Pesquisar por data de Nascimento # ");
                    ConsultarAmigoPelaData(); break;
                case '5':
                    Console.WriteLine(" # Excluir Amigo # ");
                    ExcluirAmigo(); break;
                case '6':
                    Console.WriteLine(" # Editar Amigo # ");
                    EditarAmigo(); break;
                case '7':
                    Console.WriteLine("Até logo! Obrigado por usar o gerenciador de aniversários TABAJARA!!"); break;
                default:
                    Console.WriteLine("Opção Inválida,tente novamente!");
                    Console.WriteLine(" ");
                    MainMenu(); break;
            }
        }
        public static void CadastrarAmigo()
        {
            Console.Clear();

            Console.WriteLine(" # Cadastro de Amigo #");
            Console.WriteLine("");
            Console.WriteLine("Digite o Nome:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o Sobrenome:");
            string sobrenome = Console.ReadLine();
            Console.WriteLine("Digite a data do aniverário no formato (DD/MM/YYYY)");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());

            Amigo amigo = new Amigo();
            amigo.Nome = nome;
            amigo.Sobrenome = sobrenome;
            amigo.DataDeNascimento = dataNascimento;
            amigo.DataDeCadastro = DateTime.Now;

            //Storage.IncluirAmigo(amigo);
            Repositorio.CadastrarAmigo(amigo);
            ContinuarCadastro();
        }
        public static void ContinuarCadastro()
        {
            Console.WriteLine("Deseja cadastrar outro amigo?");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            string Escolha = Console.ReadLine();

            if (Escolha == "1")
            {

                CadastrarAmigo();
            }
            else if (Escolha == "2")
            {
                Console.Clear();
                MainMenu();

            }
            else
            {
                Console.WriteLine("Opção Inválida");
            }
        }
        public static void ConsultarAmigo()
        {
            foreach (var amigo in Repositorio.BuscarTodosOsAmigos())
            {

                Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                Console.WriteLine($"Idade: {((DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12) - 1}");
                Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");

                int dia = amigo.DataDeNascimento.Day;
                int mes = amigo.DataDeNascimento.Month;
                if (DateTime.Today.Month < amigo.DataDeNascimento.Month)
                {
                    DateTime proximoNiver = new DateTime(DateTime.Today.Year, mes, dia);

                    double resultado = proximoNiver.Subtract(DateTime.Today).TotalDays;

                    Console.WriteLine($"Seu proximo aniversário é em {resultado} dias");
                    Console.WriteLine("");
                }
                else
                {
                    DateTime proximoNiver = new DateTime(DateTime.Today.Year + 1, mes, dia);

                    double resultado = proximoNiver.Subtract(DateTime.Today).TotalDays;

                    Console.WriteLine($"Seu proximo aniversário é em {resultado} dias");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Pressione Qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public static void ConsultarAmigoPeloNome()
        {
            Console.WriteLine("Entre com o nome que deseja buscar:");
            string nome = Console.ReadLine();


            var amigosEncontrados = Repositorio.BuscarTodosOsAmigos(nome);

            int qtdamigosEncontrados = amigosEncontrados.Count();

            if (qtdamigosEncontrados > 0)
            {
                foreach (var amigo in amigosEncontrados)
                {
                    Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                    Console.WriteLine($"Idade: {((DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12) - 1}");
                    Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                    Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum amigo encontrado com esse nome!!!");
            }


            Console.WriteLine("");
            Console.WriteLine("Pressione Qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }

        public static void ConsultarAmigoPelaData()
        {
            Console.WriteLine("Entre com a data que deseja buscar:");
            DateTime data = DateTime.Parse(Console.ReadLine());


            var amigosEncontrados = Repositorio.BuscarTodosOsAmigos(data);

            int qtdamigosEncontrados = amigosEncontrados.Count();

            if (qtdamigosEncontrados > 0)
            {

                foreach (var amigo in amigosEncontrados)
                {
                    Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                    Console.WriteLine($"Idade: {((DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12) - 1}");
                    Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                    Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");
                }

            }
            else
            {
                Console.WriteLine("Nenhum amigo encontrado para esta data!!!");
            }

            Console.WriteLine("");
            Console.WriteLine("Pressione Qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        public static void ExcluirAmigo()
        {
            Console.WriteLine("Entre com o nome do amigo que deseja excluir:");
            Console.WriteLine("");
            string nome = Console.ReadLine();

            var amigosEncontrados = Repositorio.BuscarTodosOsAmigos(nome);

            int qtdamigosEncontrados = amigosEncontrados.Count();

            if (qtdamigosEncontrados > 0)
            {
                Console.WriteLine("Amigos Encontradas:");
                Console.WriteLine("");
                foreach (var amigo in amigosEncontrados)
                {
                    Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                    Console.WriteLine($"Idade: {(DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12 - 1}");
                    Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                    Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Nenhum amigo encontrado!!");
                Console.WriteLine("");
                ExcluirAmigo();
            }

            Console.WriteLine("Para continuar digite o SOBRENOME do amigo que deseja excluir!");
            string sobrenome2 = Console.ReadLine();

            foreach (var amigo in amigosEncontrados)
            {
                if (amigo.Sobrenome == sobrenome2)
                {
                    Repositorio.Excluir(amigo.Nome);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Pressione Qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();            
            MainMenu();
        }
        public static void EditarAmigo()
        {
            Console.WriteLine("Digite com o nome do amigo que deseja editar:");
            Console.WriteLine("");
            string nome = Console.ReadLine();

            var amigosEncontrados = Repositorio.BuscarTodosOsAmigos(nome);

            int qtdamigosEncontrados = amigosEncontrados.Count();

            if (qtdamigosEncontrados > 0)
            {
                Console.WriteLine("Amigos Encontrados:");
                Console.WriteLine("");
                foreach (var amigo in amigosEncontrados)
                {
                    Console.WriteLine($"{amigo.Nome} {amigo.Sobrenome}");
                    Console.WriteLine($"Idade: {(DateTime.Now - amigo.DataDeNascimento).Days / 30 / 12 - 1}");
                    Console.WriteLine($"Data de Nascimento: {amigo.DataDeNascimento:dd/MM/yyyy}");
                    Console.WriteLine($"Data de Cadastro: {amigo.DataDeCadastro:dd/MM/yyyy}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Nenhum amigo encontrado!!");
                Console.WriteLine("");
                EditarAmigo();
            }

            Console.WriteLine("Para continuar, digite novamente o SOBRENOME do amigo que deseja Editar!");
            string escolha2 = Console.ReadLine();

            foreach (var amigo in amigosEncontrados)
            {
                if (amigo.Sobrenome == escolha2)
                {
                    Repositorio.Excluir(amigo.Nome);

                }
                else
                {
                    Console.WriteLine("Nenhum amigo encontrado com esse SOBRENOME!!");
                    Console.WriteLine("");
                    EditarAmigo();
                }
            }

            Console.WriteLine(" # Cadastrando Amigo # ");
            Console.WriteLine("");
            Console.WriteLine("Digite o Nome:");
            string newNome = Console.ReadLine();
            Console.WriteLine("Digite o Sobrenome:");
            string newSobrenome = Console.ReadLine();
            Console.WriteLine("Digite a data do aniversario (DD/MM/YYYY)");
            DateTime newDataNascimento = DateTime.Parse(Console.ReadLine());

            Amigo amigo1 = new Amigo();
            amigo1.Nome = newNome;
            amigo1.Sobrenome = newSobrenome;
            amigo1.DataDeNascimento = newDataNascimento;
            amigo1.DataDeCadastro = DateTime.Now;

            Repositorio.CadastrarAmigo(amigo1);
            Console.WriteLine("");
            Console.WriteLine("Pressione Qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();            
            MainMenu();

        }

    }


    public class Amigo
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public DateTime DataDeCadastro { get; set; }

        public Amigo()
        {
            DataDeCadastro = DateTime.Now;
        }

        public Amigo(string nome, string sobrenome, DateTime dataDeNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeNascimento = dataDeNascimento;
        }
        public Amigo(string nome, string sobrenome, DateTime dataDeNascimento, DateTime dataCadastro)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeNascimento = dataDeNascimento;
            DataDeCadastro = dataCadastro;

        }
        
    }

}


    





/*private static void OperacaoConsultarAmigoMaisNovo()
        {
            Amigos.Max(x => x.DataDeNascimento);
            Amigo amigo = BuscarAmigoMaisNovo(Amigos);
            Console.WriteLine("O amigo mais novo é: " + amigo.Nome);
        }

        public static Amigo BuscarAmigoMaisNovo(List<Amigo> amigos)
        {
            Amigos.Max(x => x.DataDeNascimento);
                       
        }*/
