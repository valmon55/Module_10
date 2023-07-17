namespace CS_Basic.Task_2
{
    public interface ILogger
    {
        void Event(string message);
        void Error(string message);
        void Query(string message);
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
        public void Query(string message) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
    }

    public interface ISum 
    {
        double Sum(double a, double b, int precision);
    }
    public class Calculator : ISum
    {
        ILogger logger { get; }

        public Calculator(ILogger logger)
        {
            this.logger = logger;
        }

        public double Sum(double a, double b, int precision)
        {
            logger.Event($"Результат = {Math.Round(a + b, precision)}");
            return (a + b);
        }
    }
    public class Keyboard
    {
        ILogger logger { get; }

        public Keyboard(ILogger logger)
        {
            this.logger = logger;
        }
        public double a { get; private set; }
        public double b { get; private set; }

        public int precision { get; private set; }

        public delegate double TwoNumEnter(double a, double b, int precision);

        public event TwoNumEnter TwoNumEnterEvent;

        public void GetTwoNumbers()
        {
            int prec_a;
            int prec_b;
            int max_prec;
            while (true)
            {
                try
                {
                    a = NumberEntered(out prec_a);
                    b = NumberEntered(out prec_b);
                    max_prec = prec_a > prec_b ? prec_a : prec_b;
                    TwoNumEnterEvent?.Invoke(a, b, max_prec);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
        public double NumberEntered(out int precision)
        {
            precision = 0;
            logger.Query("Введите число: ");
            string? s = Console.ReadLine();
            string[] l = s.Split(",",StringSplitOptions.TrimEntries);
            precision = l.Length > 1 ? l[1].Length : 0;
            return Convert.ToDouble(s);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            Calculator calc = new Calculator(logger);
            Keyboard keyboard = new Keyboard(logger);
            keyboard.TwoNumEnterEvent += calc.Sum;

            keyboard.GetTwoNumbers();

            Console.ReadKey();
        }

    }
}