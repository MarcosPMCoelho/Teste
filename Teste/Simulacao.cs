using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public static class Simulacao
    {
        public static void Simulacao1()
        {
            //Caso #1
            //Operação Custo unitário Quantidade Imposto Pago Explicação
            //buy 10.00 100 0 Comprar ações não paga imposto

            //Operação Custo unitário Quantidade Imposto Pago Explicação
            //sell 15.00 50 0 Valor total menor do que R$ 20000
            //sell 15.00 50 0 Valor total menor do que R$ 20000
            //Entrada:

            //[{"operation":"buy", "unit-cost":10.00, "quantity": 100},
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 50},
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 50}]

            //Saída:

            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]";

            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq01.Json");
            sw.Write(json);
            sw.Close();

            Console.WriteLine("Caso #1 - ok");
            
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq01.Json"));
        }

        public static void Simulacao2()
        {
            //Case #2
            //buy 10.00 10000 0 Comprar ações não paga imposto

            //sell 20.00 5000 10000
            //Lucro de R$ 50000: 20 % do lucro corresponde a R$ 10000 e não possui prejuízo anterior
            //sell 5.00 5000 0 Prejuízo de R$ 25000: não paga imposto
            //Entrada:

            //[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 5000},
            //{ "operation":"sell", "unit-cost":5.00, "quantity": 5000}]

            //Saída:

            //[{ "tax": 0.0},{ "tax": 10000.0},{ "tax": 0.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]";

            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq02.Json");
            sw.Write(json);
            sw.Close();

            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq02.Json"));
        }

        public static void Simulacao1_2()
        {

            //Case #1 + Case #2
            //Quando a aplicação recebe duas linhas, elas devem ser lidadas como duas simulações independentes. O
            //programa não deve carregar o estado obtido do processamento da primeira entrada para as outras
            //execuções.
            //Input:
            //[{"operation":"buy", "unit-cost":10.00, "quantity": 100},
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 50},

            //{ "operation":"sell", "unit-cost":15.00, "quantity": 50}]
            //[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 5000},
            //{ "operation":"sell", "unit-cost":5.00, "quantity": 5000}]

            //Output:
            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0}]
            //[{"tax": 0.0},{ "tax": 10000.0},{ "tax": 0.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]" +
                            "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]";


            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq1_2.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq1_2.Json"));
        }

        public static void Simulacao3()
        {
            //Caso #3

            //Operação

            //Custo
            //unitário

            //Quantidade

            //Imposto
            //Pago

            //Explicação

            //buy 10.00 10000 0 Comprar ações não paga imposto
            //sell 5.00 5000 0 Prejuízo de R$ 25000: não paga imposto

            //sell 20.00 3000 1000

            //Lucro de R$ 30000: Deve deduzir prejuízo de R$
            //25000 e paga 20% de R$ 5000 em imposto (R$
            //1000)

            //Entrada:

            //[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":5.00, "quantity": 5000},
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 3000}]
            //Saída:

            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 1000.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]";


            Console.WriteLine("Caso #3 - ok");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq3.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq3.Json"));

        }

        public static void Simulacao4()
        {
            //Caso #4

            //Operação

            //Custo
            //unitário

            //Quantidade

            //Imposto
            //Pago

            //Explicação

            //buy 10.00 10000 0 Comprar ações não paga imposto
            //buy 25.00 5000 0 Comprar ações não paga imposto

            //sell 15.00 10000 0

            //Considerando preço médio ponderado de R$ 15
            //((10×10000 + 25×5000) ÷ 15000) não teve lucro
            //nem prejuízo

            //Entrada:

            //[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"buy", "unit-cost":25.00, "quantity": 5000},
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 10000}]

            //Saída:

            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]";


            Console.WriteLine("Caso #4");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq4.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq4.Json"));

        }

        public static void Simulacao5()
        {
            //Caso #5
            //Operação
            //Custo
            //unitário
            //Quantidade
            //Imposto
            //Pago
            //Explicação
            //buy 10.00 10000 0 Comprar ações não paga imposto
            //buy 25.00 5000 0 Comprar ações não paga imposto
            //sell 15.00 10000 0
            //Considerando preço médio ponderado de R$ 15
            //((10×10000 + 25×5000) ÷ 15000) não teve lucro
            //nem prejuízo
            //            sell 25.00 5000 10000
            //Considerando preço médio ponderado de R$ 15
            //lucro de R$ 50000: paga 20 % de R$ 50000 em
            //imposto(R$ 10000)
            //Entrada:
            //[{"operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"buy", "unit-cost":25.00, "quantity": 5000},
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":25.00, "quantity": 5000}]
            //Saída:
            //[{"tax": 0.0},{"tax": 0.0},{"tax": 0.0},{"tax": 10000.0}]
            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}, " +
                            "{ \"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 5000}]";
            Console.WriteLine("Caso #5 - ok");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq5.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq5.Json"));
        }

        public static void Simulacao6()
        {
            //Caso #6
            //Operação  Custo unitário  Quantidade  Imposto Pago     Explicação
            //buy       10.00           10000       0                Comprar ações não paga imposto
            //sell 2.00 5000 0 Perda de R$ 40000: valor total é menor do que R$ 20000, mas devemos deduzir o prejuízo independe disso
            //sell 20.00 2000 0 Lucro de R$ 20000: se deduzir o prejuízo, seu lucro é zero.Você ainda tem R$ 20000 de prejuízo para deduzir dos próximos lucros
            //Lucro de R$ 20000: se deduzir o prejuízo, seu  lucro é zero.Agora não tem mais prejuízo para deduzir dos próximos lucros sell 25.00 1000 3000
            // Lucro de R$ 15000 e sem prejuízos: paga 20 % de R$ 15000 em imposto(R$ 3000)


            //Entrada:
            // [{ "operation":"buy", "unit-cost":10.00, "quantity": 10000}, --Comprar ações não paga imposto
            //{ "operation":"sell", "unit-cost":2.00, "quantity": 5000},    --Perda de R$ 40000: valor total é menor do que R$ 20000, mas devemos deduzir o prejuízo independe disso
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 2000}, --0 Lucro de R$ 20000: se deduzir o prejuízo, seu lucro é zero.Você ainda tem R$ 20000 de prejuízo para deduzir dos próximos lucros
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 2000}, --se deduzir o prejuízo, seu  lucro é zero.Agora não tem mais prejuízo para deduzir dos próximos lucros sell 25.00 1000 3000
            //{ "operation":"sell", "unit-cost":25.00, "quantity": 1000}] --Lucro de R$ 15000 e sem prejuízos: paga 20 % de R$ 15000 em imposto(R$ 3000)
            //Saída:
            //[{"tax": 0.0},{"tax": 0.0},{"tax": 0.0},{"tax": 0.0},{"tax": 3000.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},  " +
                        "{ \"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}]";

            Console.WriteLine("Caso #6 - ok");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq6.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq6.Json"));
        }

        public static void Simulacao7()
        {
            //[{ "operation":"buy", "unit-cost":10.00, "quantity": 10000},  //tax 0
            //{ "operation":"sell", "unit-cost":2.00, "quantity": 5000}, //tax 0
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 2000}, //tax 0
            //{ "operation":"sell", "unit-cost":20.00, "quantity": 2000}, //tax 0
            //{ "operation":"sell", "unit-cost":25.00, "quantity": 1000}, //tax 3000
            //{ "operation":"buy", "unit-cost":20.00, "quantity": 10000}, //tax 0
            //{ "operation":"sell", "unit-cost":15.00, "quantity": 5000}, //tax 0
            //{ "operation":"sell", "unit-cost":30.00, "quantity": 4350}, //tax 3700
            //{ "operation":"sell", "unit-cost":30.00, "quantity": 650}] // tax 0

            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0},{ "tax": 3000.0},
            //{ "tax": 0.0},{ "tax": 0.0},{ "tax": 3700.0},{ "tax": 0.0}]


            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                        "{ \"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}," +
                        "{ \"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 5000}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 4350}," +
                        "{ \"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 650}]";

            Console.WriteLine("Caso #7");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq7.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq7.Json"));
        }

        public static void Simulacao8()
        {
            //Caso #8
            //[{ "operation":"buy", "unit-cost":10.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":50.00, "quantity": 10000},
            //{ "operation":"buy", "unit-cost":20.00, "quantity": 10000},
            //{ "operation":"sell", "unit-cost":50.00, "quantity": 10000}]

            //[{ "tax": 0.0},{ "tax": 80000.0},{ "tax": 0.0},{ "tax": 60000.0}]

            string json = "[{ \"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, " +
                     "{ \"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}," +
                     "{ \"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}," +
                     "{ \"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}]";

            Console.WriteLine("Caso #8");
            // Console.WriteLine(GanhoCapital.Ganho.Executar(json));
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq8.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq8.Json"));
        }

        internal static void Simulacao9()
        {
            //[{ "operation": "buy", "unit-cost": 5000.00, "quantity": 10},
            //{ "operation": "sell", "unit-cost": 4000.00, "quantity": 5},
            //{ "operation": "buy", "unit-cost": 15000.00, "quantity": 5},
            //{ "operation": "buy", "unit-cost": 4000.00, "quantity": 2},
            //{ "operation": "buy", "unit-cost": 23000.00, "quantity": 2},
            //{ "operation": "sell", "unit-cost": 20000.00, "quantity": 1},
            //{ "operation": "sell", "unit-cost": 12000.00, "quantity": 10},
            //{ "operation": "sell", "unit-cost": 15000.00, "quantity": 3}]


            //[{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0},{ "tax": 0.0},
            //{ "tax": 0.0},{ "tax": 1000.0},{ "tax": 2400.0}]

            string json = "[{ \"operation\": \"buy\", \"unit-cost\": 5000.00, \"quantity\": 10}, " +
                          "{ \"operation\": \"sell\", \"unit-cost\": 4000.00, \"quantity\": 5}, " +
                          "{ \"operation\": \"buy\", \"unit-cost\": 15000.00, \"quantity\": 5}, " +
                          "{ \"operation\": \"buy\", \"unit-cost\": 4000.00, \"quantity\": 2}, " +
                          "{ \"operation\": \"buy\", \"unit-cost\": 23000.00, \"quantity\": 2}, " +
                          "{ \"operation\": \"sell\", \"unit-cost\": 20000.00, \"quantity\": 1}, " +
                          "{ \"operation\": \"sell\", \"unit-cost\": 12000.00, \"quantity\": 10}, " +
                          "{ \"operation\": \"sell\", \"unit-cost\": 15000.00, \"quantity\": 3}]";


            Console.WriteLine("Caso #9");
            StreamWriter sw = new StreamWriter(@"D:\Arquivos\Arq9.Json");
            sw.Write(json);
            sw.Close();
            Console.WriteLine(GanhoCapital.Ganho.ExecutarArq(@"D:\Arquivos\Arq9.Json"));
        }


      
    }
}
