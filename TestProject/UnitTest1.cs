namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            List<GanhoCapital.Entity.Capital> lista = new();

            lista.Add(new() { operation = "buy", unitcost = 20.00m, quantity = 10 });
            lista.Add(new() { operation = "sell", unitcost = 20.00m, quantity = 5 });
            lista.Add(new() { operation = "buy", unitcost = 10.00m, quantity = 5 });

            GanhoCapital.Ganho calcular = new ();
            decimal valor = calcular.CalcularMediaPonderada(lista);

            Assert.Equal(15m, valor);
        }
    }
}
