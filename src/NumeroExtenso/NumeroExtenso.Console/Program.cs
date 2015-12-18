namespace NumeroExtenso.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("{0}\t\t{1}", 1.1M, 1.1M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 23.61M, 23.61M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 20M, 20M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 100M, 100M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 11000000M, 11000000M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 111000000M, 111000000M.ToExtenso());
            System.Console.WriteLine("{0}\t\t{1}", 999000000M, 999000000M.ToExtenso());
        }
    }
}
