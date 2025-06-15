using GanhoCapital.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;

namespace GanhoCapital
{
    public class Ganho
    {
        public string Executar(string operationsJson)
        {
            StringBuilder sRetorno = new StringBuilder();

            var taxasTotais = new List<Taxas>();

            decimal valorMedia = 0;

            // Divide a string em arrays JSON individuais
            var matches = System.Text.RegularExpressions.Regex.Matches(operationsJson, @"\[[^\[\]]*\]");
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var arrayJson = match.Value;
                var loperations = JsonSerializer.Deserialize<List<Capital>>(arrayJson);
                var taxas = ProcessarTaxas(loperations);


                sRetorno.Append(JsonSerializer.Serialize(taxas, new JsonSerializerOptions { WriteIndented = true }));
            }

            return sRetorno.ToString();
        }

        private List<Taxas> ProcessarTaxas(List<Capital>? loperations)
        {
            //O percentual de imposto pago é de 20% sobre o lucro obtido na operação. 
            //            Ou seja, o imposto vai ser pago quando há uma operação de venda cujo preço é superior ao preço médio ponderado de compra.
            //Para determinar se a operação resultou em lucro ou prejuízo, você pode calcular o preço médio
            //ponderado, então quando você compra ações você deve recalcular o preço médio ponderado
            //utilizando essa fórmula: nova - media - ponderada = ((quantidade - de - acoes - atual * media - ponderada -
            //atual) + (quantidade - de - acoes - compradas * valor - de - compra)) / (quantidade - de - acoes - atual +
            //quantidade - de - acoes - compradas).Por exemplo, se você comprou 10 ações por R$ 20,00, vendeu 5,
            //depois comprou outras 5 por R$ 10,00, a média ponderada é((5 x 20.00) +(5 x 10.00)) / (5 + 5)
            //= 15.00.
            //            Prejuízos acontecem quando você vende ações a um valor menor do que o preço médio ponderado de
            //compra.Neste caso, nenhum imposto deve ser pago e você deve subtrair o prejuízo dos lucros
            //seguintes, antes de calcular o imposto.
            //Você não paga nenhum imposto e não deve deduzir o lucro obtido dos prejuízos acumulados se o valor
            //total da operação(custo unitário da ação x quantidade) for menor ou igual a R$ 20000, 00.Use o valor
            //total da operação e não o lucro obtido para determinar se o imposto deve ou não ser pago.E não se
            //esqueça de deduzir o prejuízo dos lucros seguintes.
            //Nenhum imposto é pago em operações de compra.
            //Você pode assumir que nenhuma operação vai vender mais ações do que você tem naquele momento.

            //if (loperations.Count> 0)
            //    loperations.Clear();

            //loperations.Add(new Capital { operation = "buy", unitcost = 20.00m, quantity = 10 });   
            //loperations.Add(new Capital { operation = "sell", unitcost = 30.00m, quantity = 5 });
            //loperations.Add(new Capital { operation = "buy", unitcost = 10.00m, quantity = 5 });
            //var resultado = CalcularMediaPonderada(loperations);

            decimal valorAcumulado = 0;
            decimal valor = 0;

            //decimal novamediaPonderada = CalcularMediaPonderada(loperations);
            
            int qtdAcoesAtual = 0;
            decimal precomedio = 0;
            decimal lucro = 0;
            decimal prejuizo = 0;
            decimal txprejuizo = 0;

            //List<Capital> lCompra = new List<Capital>();

            List<Taxas> taxas = new List<Taxas>();  
            List<Taxas> taxasVenda = new List<Taxas>();    

            List<Capital> lMediaPonderada = new List<Capital>();

            foreach (Capital item in loperations)
            {
                valor =  (decimal)item.unitcost * item.quantity;

                if (item.operation == "buy")
                {
                    if (qtdAcoesAtual == 0)
                    {
                        //lCompra.Clear();
                        lMediaPonderada.Clear();
                        valorAcumulado = 0;
                    }

                    qtdAcoesAtual = qtdAcoesAtual + item.quantity;
                    valorAcumulado = valorAcumulado + valor;
                    item.tax = 0;
                    taxas.Add(new Taxas { tax = item.tax });
                    //lucro = valor;

                    //lCompra.Add(item);
                    lMediaPonderada.Add(item);
                    precomedio = CalcularMediaPonderada(lMediaPonderada);
                }
                else if (item.operation == "sell")
                {
                    qtdAcoesAtual = qtdAcoesAtual - item.quantity;
                    if (qtdAcoesAtual < 0)
                        throw new Exception("Você não pode vender mais do que tem.");


                    precomedio = CalcularMediaPonderada(lMediaPonderada);

                    if (item.quantity * item.unitcost <= 20000)
                    {
                        if (item.unitcost < precomedio)
                            prejuizo = prejuizo + ((precomedio - item.unitcost) * item.quantity);

                        item.tax = 0;
                        taxas.Add(new Taxas { tax = item.tax });
                        continue;
                    }

                    
                    if (item.unitcost > precomedio)
                    {

                        lucro = (item.unitcost - precomedio) * item.quantity;
                        if (lucro - prejuizo <= 0)
                        {
                            item.tax = 0;
                            prejuizo = prejuizo - lucro;
                            taxas.Add(new Taxas { tax = item.tax });
                            continue;
                        }

                        item.tax = (lucro - prejuizo) * 0.2m; //20% do lucro
                        prejuizo = 0;
                       

                    }
                    else if (item.unitcost < precomedio)
                    {
                        //prejuizo
                        prejuizo = (precomedio - item.unitcost) * item.quantity;
                        if (taxasVenda.Count > 0)
                        {
                            //subtrai o prejuizo do lucro acumulado
                            decimal totalLucroAcumulado = taxas.Sum(t => t.tax ?? 0);
                            totalLucroAcumulado -= prejuizo;
                            item.tax = 0; // totalLucroAcumulado * 0.2m; //20% do lucro restante
                        }
                        else
                        {
                            txprejuizo = txprejuizo + ((item.unitcost - precomedio) * item.quantity) * 0.2m;
                            item.tax = 0;
                            taxasVenda.Add(new Taxas { tax = item.tax });
                        }
                    }
                    else
                    {
                        item.tax = 0; //sem lucro nem prejuizo
                    }

                    taxas.Add(new Taxas { tax = item.tax });

                }
                else
                {
                    throw new Exception("Operação inválida: " + item.operation);
                }
                
            }
            
            return taxas;
        }
        public decimal CalcularMediaPonderada(List<Capital> operacoes)
        {
            /*
             * 
             * 
             * Para determinar se a operação resultou em lucro ou prejuízo, você pode calcular o preço médio
                ponderado, então quando você compra ações você deve recalcular o preço médio ponderado

                utilizando essa fórmula: nova-media-ponderada = ((quantidade-de-acoes-atual * media-ponderada-
                atual) + (quantidade-de-acoes-compradas * valor-de-compra)) / (quantidade-de-acoes-atual +

                quantidade-de-acoes-compradas) . Por exemplo, se você comprou 10 ações por R$ 20,00, vendeu 5,
                depois comprou outras 5 por R$ 10,00, a média ponderada é ((5 x 20.00) + (5 x 10.00)) / (5 + 5)
                = 15.00 .
             * 
             * */

            decimal totalInvestido = 0;
            int totalAcoes = 0;

            
            foreach (var op in operacoes)
            {
                if (op.operation == "buy")
                {
                    totalInvestido += op.unitcost * op.quantity;
                    totalAcoes += op.quantity;
                }
                else if (op.operation == "sell")
                {
                    totalInvestido += (op.unitcost * op.quantity * (-1));
                    totalAcoes -= op.quantity;
                }
            }
            decimal mediaPonderada = totalAcoes > 0 ? totalInvestido / totalAcoes : 0;
            mediaPonderada = Math.Round(mediaPonderada, 2, MidpointRounding.AwayFromZero); // Arredondar para duas casas decimais
            return mediaPonderada;
        }



    }
}
