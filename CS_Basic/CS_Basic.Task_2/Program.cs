namespace CS_Basic.Task_2
{
    public interface ILogger
    {
        void Event(string message);
        void Error(string message);
    }
    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }

        public void Event(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
        }
    }

    public interface ISum 
    {
        double Sum(double a, double b);
    }
    public class Calculator : ISum
    {
        ILogger logger { get; }

        public Calculator(ILogger logger)
        {
            this.logger = logger;
        }

        public double Sum(double a, double b)
        {
            logger.Event($"Результат = {a + b}");
            Console.WriteLine($"Результат = {a + b}");
            return (a + b);
        }
    }
    public class Keyboard
    {
        public double a { get; private set; }
        public double b { get; private set; }

        public delegate double TwoNumEnter(double a, double b);

        public event TwoNumEnter TwoNumEnterEvent;

        public void GetTwoNumbers()
        {
            while (true)
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
        public double NumberEntered()
        {
            Console.Write("Введите число: ");
            return Convert.ToDouble(Console.ReadLine());
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

            Console.ReadKey();
        }

    }
}