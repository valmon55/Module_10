namespace CS_Basic.Task_1
{
    public interface ISum /*ISum<T> where T : Int32, Double,Decimal*/
    {
        int Sum(int a, int b);
    }
    public class Calculator : ISum
    {
        public int Sum(int a, int b)
        {
            Console.WriteLine($"Результат = {a + b}");
            return (a + b);
        }
    }
    public class Keyboard
    {
        public int a { get; private set; }
        public int b { get; private set; }

        public delegate int TwoNumEnter(int a, int b);
        //public Func<int, int, int> TwoNumEnter; //??
        public event TwoNumEnter TwoNumEnterEvent;

        public void GetTwoNumbers()
        {
            while (true /*(Console.ReadKey()).Key != ConsoleKey.Escape*/)
            {
                try
                {
                    a = NumberEntered();
                    b = NumberEntered();
                    TwoNumEnterEvent?.Invoke(a, b);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public int NumberEntered()
        {
            Console.Write("Введите число: ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Keyboard keyboard = new Keyboard();
            keyboard.TwoNumEnterEvent += calc.Sum;

            keyboard.GetTwoNumbers();


            //try
            //{
            //    Console.WriteLine($"Результат = { calc.Sum(keyboard.NumberEntered(), keyboard.NumberEntered())}");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            Console.ReadKey();
        }

    }
}