using GanhoCapital;
using GanhoCapital.Entity;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
namespace Leitura
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string path = args[0];
            var result = GanhoCapital.Ganho.ExecutarArq(path);
            Console.WriteLine(result);
        }
    }
}
