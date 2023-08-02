using data_access;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        SalesManager m = new();

        m.ShowAllSales(new DateOnly(2000, 1, 1), new DateOnly(2020, 1, 1));
       
    }
}