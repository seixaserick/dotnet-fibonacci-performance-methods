

class Fibonacci
{

    static int[] dp = new int[0];  // public int array to store previous fibonacci calculations (like a inMemory cache)
    static int interactionsCountSimpleMethod;
    static int interactionsCountTailRecursiveMethod;
    static int interactionsCountDynamicProgrammingMethod;

    public static void Main()
    {


        DateTime start;
        int result;
        TimeSpan duration;


        //Calling Fibonacci calculation comparing 3 different methods for Fib(42) until Fib(38)   
        for (int n = 42; n >= 38; n--)
        {
            interactionsCountDynamicProgrammingMethod = 0;
            interactionsCountTailRecursiveMethod = 0;
            interactionsCountSimpleMethod = 0;

            Console.WriteLine( Environment.NewLine + $"Processing Fibonacci({n}) ...");
           

            //CalculateFibonacciSimpleMethod() --> Slow, recursive, exponential, intuitive
            start = DateTime.Now;
            result = CalculateFibonacciSimpleMethod(n);
            duration = DateTime.Now - start;
            Console.WriteLine($"CalculateFibonacciSimpleMethod                              ({n}) is ---> {result}. Execution time: {duration.TotalMilliseconds.ToString().PadLeft(15)}ms and {interactionsCountSimpleMethod} interactions");


            //CalculateFibonacciDynamicProgrammingMethod() --> fast, flat, linear, intuitive, cached for subsequent requests
            start = DateTime.Now;
            result = CalculateFibonacciDynamicProgrammingMethod(n);
            duration = DateTime.Now - start;
            Console.WriteLine($"CalculateFibonacciDynamicProgrammingMethod                  ({n}) is ---> {result}. Execution time: {duration.TotalMilliseconds.ToString().PadLeft(15)}ms and {interactionsCountDynamicProgrammingMethod} interactions");


            //CalculateFibonacciTailRecursiveMethod() --> fast, linear, recursive, less intuitive
            start = DateTime.Now;
            result = CalculateFibonacciTailRecursiveMethod(n);
            duration = DateTime.Now - start;
            Console.WriteLine($"CalculateFibonacciTailRecursiveMethod                       ({n}) is ---> {result}. Execution time: {duration.TotalMilliseconds.ToString().PadLeft(15)}ms and {interactionsCountTailRecursiveMethod} interactions");

        }


    }




    public static int CalculateFibonacciSimpleMethod(int n)
    {

        interactionsCountSimpleMethod++; //used to show in the log how many loops this recursive method did

        if (n < 2) { return n; }

        //Simple, recursive, exponential and the bad way to do Fibonacci calculation
        return CalculateFibonacciSimpleMethod(n - 1) + CalculateFibonacciSimpleMethod(n - 2);

    }

    public static int CalculateFibonacciDynamicProgrammingMethod(int n)
    {
        interactionsCountDynamicProgrammingMethod++; //used to show in the log how many loops this method did

        if (n == 0) return 0;

        // If previous calculation greater than "n" was already stored in the array, returns the stored Fibonacci calculation from memory array
        if (dp.Length > n && dp[n] > 0)
        {
            return dp[n]; // return from memory array
        }


        dp = new int[n + 1];

        //base cases
        dp[0] = 0;
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            interactionsCountDynamicProgrammingMethod++; //used to show in the log how many loops this method did

            dp[i] = dp[i - 1] + dp[i - 2];
            if (dp[i] < 0) //happens when int+int excceed int maxValeu and C# returns a negative val
            {
                throw new Exception($"Fibonacci {i}th results an invalid calculation because it is too long to be stored in an integer variable");
            }
        }

        return dp[n];
    }

   
    
    public static int CalculateFibonacciTailRecursiveMethod(int n, int previous = 0, int current = 1)
    {

        interactionsCountTailRecursiveMethod++; //used to show in the log how many loops this recursive method did

        if (n == 0) { return previous; }
        if (n == 1) { return current; }

        return CalculateFibonacciTailRecursiveMethod(n - 1, current, previous + current);
    }
}