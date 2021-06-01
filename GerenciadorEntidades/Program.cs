using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GerenciadorEntidades
{
    class Program
    {
        private const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
        private static Dictionary<string, DateTime> listaAmigos = new Dictionary<string, DateTime>();

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string opcao;
            do
            {
                ExibirMenuPrincipal();

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        PesquisarAmigos();
                        break;
                    case "2":
                        AdicionarAmigo();
                        break;
                    case "3":
                        Console.Write("Saindo do programa... ");
                        break;
                    default:
                        Console.Write("Opcao inválida! Escolha uma opção válida. ");
                        break;
                }

                Console.WriteLine(pressioneQualquerTecla);
                Console.ReadKey();
            }
            while (opcao != "3");
        }

        static void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("*** Gerenciador de Amigos *** ");
            Console.WriteLine("1 - Pesquisar Amigos:");
            Console.WriteLine("2 - Adicionar Amigos:");
            Console.WriteLine("3 - Sair:");
            Console.WriteLine("\nEscolha uma das opções acima: ");
        }

        static void PesquisarAmigos()
        {
            Console.WriteLine("Informe o nome do amigo que deseja pesquisar:");
            var termoDePesquisa = Console.ReadLine();
            var amigosEncontrados = listaAmigos.Where(x => x.Key.ToLower().Contains(termoDePesquisa.ToLower()))
                                               .ToList();

            if (amigosEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados dos amigos encontrados:");
                for (var index = 0; index < amigosEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {amigosEncontrados[index].Key}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= amigosEncontrados.Count)
                {
                    Console.Write($"Opção inválida! ");
                    return;
                }

                if (indexAExibir < amigosEncontrados.Count)
                {
                    var amigo = amigosEncontrados[indexAExibir];

                    Console.WriteLine("Dados do amigo:");
                    Console.WriteLine($"Nome: {amigo.Key}");
                    Console.WriteLine($"Data Aniversário: {amigo.Value:dd/MM/yyyy}");

                    var qtdeDiasParaOProximoAniversario = CalcularQtdeDiasProximoAniversario(amigo.Value);
                    Console.Write(ObterMensagemAniversario(qtdeDiasParaOProximoAniversario));
                }
            }
            else
            {
                Console.Write("Não foi encontrado nenhum amigo! ");
            }
        }

        static void AdicionarAmigo()
        {
            Console.WriteLine("Informe o nome do amigo que deseja adicionar:");
            var nomeAmigo = Console.ReadLine();

            Console.WriteLine("Informe a data de anivesário do amigo (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out var dataAniversarioAmigo))
            {
                Console.Write($"Data inválida! Dados descartados! ");
                return;
            }

            Console.WriteLine("Os dados estão corretos?");
            Console.WriteLine($"Nome: {nomeAmigo}");
            Console.WriteLine($"Data aniversario: {dataAniversarioAmigo:dd/MM/yyyy}");
            Console.WriteLine("1 - Sim \n2 - Não");

            var opcaoParaAdicionar = Console.ReadLine();

            if (opcaoParaAdicionar == "1")
            {
                listaAmigos.Add(nomeAmigo, dataAniversarioAmigo);
                Console.Write($"Dados adicionados com sucesso! ");
            }
            else if (opcaoParaAdicionar == "2")
            {
                Console.Write($"Dados descartados! ");
            }
            else
            {
                Console.Write($"Opção inválida! ");
            }
        }

        static double CalcularQtdeDiasProximoAniversario(DateTime dataAnivesario)
        {
            var dataAtual = DateTime.Now.Date;
            var diferencaEmAnos = dataAtual.Year - dataAnivesario.Year;
            var aniversarioAnoAtual = dataAnivesario.AddYears(diferencaEmAnos);
            var qtdeDiasParaAniversario = aniversarioAnoAtual - dataAtual;
            return qtdeDiasParaAniversario.Days;
        }

        static string ObterMensagemAniversario(double qtdeDias)
        {
            if (double.IsNegative(qtdeDias))
                return $"Este amigo já fez aniversário neste ano! ";
            else if (qtdeDias.Equals(0d))
                return $"Este amigo faz aniversário hoje! ";
            else
                return $"Faltam {qtdeDias:N0} dia(s) para o aniversário deste amigo! ";
        }
    }
}
