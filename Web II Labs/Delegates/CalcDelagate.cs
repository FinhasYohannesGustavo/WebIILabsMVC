namespace Web_II_Labs.Delegates
{
    public class CalcDelagate
    {
        /*Testing Delagates*/

        public static double Add(double x, double y)
        {
            return x + y;
        }
        public static double Subtract(double x, double y)
        {
            return x - y;
        }

        public static double Multiply(double x, double y)
        {
            return x * y;
        }

        public static double Divide(double x, double y)
        {
            if(y!=0)
            {
                return (x / y);

            }
            else
            {
                return -1;
            }
        }

        public delegate double CalcDelegateMethod(double x, double y);

        public static double Calc(double x, double y, CalcDelegateMethod func)
        {
            return func(x, y);
        }
        public static void Main(string[] args)
        {
            CalcDelegateMethod add=Add;
            CalcDelegateMethod subtract=Subtract;
            CalcDelegateMethod mul=Multiply;
            CalcDelegateMethod div =Divide;
     
            Console.WriteLine("add gives: " + Calc(10, 15,add));
            Console.WriteLine("subtract gives: " + Calc(10, 15, subtract));
            Console.WriteLine("Multiply gives: "+ Calc(10, 15, mul));
            Console.WriteLine("Divide gives: " + Calc(10, 15, div));

        }
    }
}
