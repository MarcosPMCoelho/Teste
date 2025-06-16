using GanhoCapital.Entity;
using Newtonsoft.Json;
using System.Text;


namespace GanhoCapital
{


    public static class Ganho
    {
       
        public static string ExecutarArq(string arq)
        {
            string json = File.ReadAllText(arq);

            int inicio = 0;
            StringBuilder sb = new StringBuilder();

          
            if (json.Split('[').Length - 1 > 1)
            {
                for (int i = 0; i < json.Split('[').Length; i++)
                {
                    string ret = json.Substring(inicio, json.IndexOf(']') + 1);
                    var loperations2 = JsonConvert.DeserializeObject<List<Capital>>(ret);
                    var taxas2 = ProcessarTaxas(loperations2);
                    sb.Append(JsonConvert.SerializeObject(taxas2));

                    json = json.Substring(ret.Length);
                   
                }
                return sb.ToString();

            }
            var loperations = JsonConvert.DeserializeObject<List<Capital>>(json);

            var taxas = ProcessarTaxas(loperations);

            string retorno = JsonConvert.SerializeObject(taxas);

            return retorno;
        }

        

        private static List<Taxas> ProcessarTaxas(List<Capital>? loperations)
        {
            decimal valorAcumulado = 0;
            decimal valor = 0;

            
            int qtdAcoesAtual = 0;
            decimal precomedio = 0;
            decimal lucro = 0;
            decimal prejuizo = 0;
            decimal txprejuizo = 0;
            decimal tax = 0;

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
                        lMediaPonderada.Clear();
                        valorAcumulado = 0;
                    }

                    qtdAcoesAtual = qtdAcoesAtual + item.quantity;
                    valorAcumulado = valorAcumulado + valor;
                    taxas.Add(new Taxas { taxa =0 });

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

                        taxas.Add(new Taxas { taxa = 0 });
                        continue;
                    }

                    
                    if (item.unitcost > precomedio)
                    {

                        lucro = (item.unitcost - precomedio) * item.quantity;
                        if (lucro - prejuizo <= 0)
                        {

                            prejuizo = prejuizo - lucro;
                            taxas.Add(new Taxas { taxa = 0 });
                            continue;
                        }

                        tax = (lucro - prejuizo) * 0.2m; 
                        prejuizo = 0;
                       

                    }
                    else if (item.unitcost < precomedio)
                    {
                        prejuizo = (precomedio - item.unitcost) * item.quantity;
                        if (taxasVenda.Count > 0)
                        {
                            decimal totalLucroAcumulado = taxas.Sum(t => t.taxa ?? 0);
                            totalLucroAcumulado -= prejuizo;
                            tax = 0; 
                        }
                        else
                        {
                            txprejuizo = txprejuizo + ((item.unitcost - precomedio) * item.quantity) * 0.2m;
                            tax = 0;
                            taxasVenda.Add(new Taxas { taxa = tax });
                        }
                    }
                    else
                    {
                        tax = 0; 
                    }

                    taxas.Add(new Taxas { taxa = tax });

                }
                else
                {
                    throw new Exception("Operação inválida: " + item.operation);
                }
                
            }
            
            return taxas;
        }
        public static decimal CalcularMediaPonderada(List<Capital> operacoes)
        {
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
            mediaPonderada = Math.Round(mediaPonderada, 2, MidpointRounding.AwayFromZero); 
            return mediaPonderada;
        }



    }
}
