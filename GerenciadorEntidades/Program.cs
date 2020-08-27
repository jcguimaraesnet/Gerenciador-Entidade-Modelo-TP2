using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GerenciadorEntidades
{
    class Program
    {
        private const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
        private static Dictionary<string, DateTime> listaEntidades = new Dictionary<string, DateTime>();

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("*** Gerenciador de [Entidades] *** ");
                Console.WriteLine("1 - Pesquisar [entidades]:");
                Console.WriteLine("2 - Adicionar [entidade]:");
                Console.WriteLine("3 - Sair:");
                Console.WriteLine("\nEscolha uma das opções acima: ");

                opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    Console.WriteLine("Informe [o campo string ou parte do campo string] [da entidade] que deseja pesquisar:");
                    var termoDePesquisa = Console.ReadLine();
                    var entidadesEncontradas = listaEntidades.Where(x => x.Key.ToLower().Contains(termoDePesquisa.ToLower()))
                                                             .ToList();

                    if (entidadesEncontradas.Count > 0)
                    {
                        Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados [das entidades] encontrados:");
                        for (var index = 0; index < entidadesEncontradas.Count; index++)
                            Console.WriteLine($"{index} - {entidadesEncontradas[index].Key}");

                        if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                        {
                            Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            continue;
                        }

                        if (indexAExibir < entidadesEncontradas.Count)
                        {
                            var entidade = entidadesEncontradas[indexAExibir];

                            Console.WriteLine("Dados [da entidade]");
                            Console.WriteLine($"[campo string]: {entidade.Key}");
                            Console.WriteLine($"[campo DateTime]: {entidade.Value:dd/MM/yyyy}");

                            //realizar algum calculo com o campo DateTime da entidade e exibir o resultado do cálculo
                            //exemplos:
                            //data aniversario: quantidade de dias que faltam para o proximo aniversário do médico (em relação a data atual)
                            //data compra carro: há quantos anos o carro foi comprado (em relação a data atual)
                            //data de nomeação do prefeito: Quantidade de dias de gestão do prefeito até a data atual

                            Console.Write(pressioneQualquerTecla);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Não foi encontrado nenhuma [entidade]! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "2")
                {
                    Console.WriteLine("Informe [campo string] da [entidade] que deseja adicionar:"); //ex: uma informação do tipo string: nome medico, nome carro, nome prefeito
                    var campoDoTipoStringDaEntidade = Console.ReadLine();

                    Console.WriteLine("Informe [campo DateTime da entidade] (formato dd/MM/yyyy):"); //uma informação do tipo DateTime: data de aniversário do médico, data de compra do carro, data de nomeação do prefeito
                    if (!DateTime.TryParse(Console.ReadLine(), out var campoDoTipoDateTimeDaEntidade))
                    {
                        Console.WriteLine($"Data inválida! Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Os dados estão corretos?");
                    Console.WriteLine($"[Campo string da entidade]: {campoDoTipoStringDaEntidade}");
                    Console.WriteLine($"[Campo DateTime da entidade]: {campoDoTipoDateTimeDaEntidade:dd/MM/yyyy}");
                    Console.WriteLine("1 - Sim \n2 - Não");

                    var opcaoParaAdicionar = Console.ReadLine();

                    if (opcaoParaAdicionar == "1")
                    {
                        listaEntidades.Add(campoDoTipoStringDaEntidade, campoDoTipoDateTimeDaEntidade);
                        Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else if (opcaoParaAdicionar == "2")
                    {
                        Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "3")
                {
                    Console.Write("Saindo do programa... ");
                }
                else if (opcao != "3")
                {
                    Console.Write($"Opcao inválida! Escolha uma opção válida. {pressioneQualquerTecla}");
                    Console.ReadKey();
                }

            } while (opcao != "3");
        }
    }
}
