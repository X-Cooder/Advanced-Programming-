using System;
using System.Collections.Generic;

namespace AdvancedProgramming
{
    // Delegate for outputting results
    public delegate void OnResult(string result);

    // Delegate for reading input
    public delegate string InputProvider();

    interface Operation
    {
        void Perform();
    }

    class SumOperation : Operation
    {
        private readonly OnResult _onResult;
        private readonly InputProvider _input;

        public SumOperation(OnResult onResult, InputProvider input)
        {
            _onResult = onResult;
            _input = input;
        }

        public void Perform()
        {
            int first = int.Parse(_input());
            int second = int.Parse(_input());
            _onResult((first + second).ToString());
        }
    }

    class DifOperation : Operation
    {
        private readonly OnResult _onResult;
        private readonly InputProvider _input;

        public DifOperation(OnResult onResult, InputProvider input)
        {
            _onResult = onResult;
            _input = input;
        }

        public void Perform()
        {
            int first = int.Parse(_input());
            int second = int.Parse(_input());
            _onResult((first - second).ToString());
        }
    }

    class ExitOperation : Operation
    {
        private readonly Action _exitAction;

        public ExitOperation(Action exitAction)
        {
            _exitAction = exitAction;
        }

        public void Perform()
        {
            _exitAction();
        }
    }

    internal class Program
    {
        private static bool runs = true;

        private static void Main(string[] args)
        {
            OnResult printResult = Console.WriteLine;
            InputProvider input = Console.ReadLine;

            var commands = new Dictionary<string, Operation>
            {
                ["sum"] = new SumOperation(printResult, input),
                ["dif"] = new DifOperation(printResult, input),
                ["exit"] = new ExitOperation(() => runs = false) 
            };

            while (runs)
            {
                string? command = Console.ReadLine();
                if (command != null && commands.TryGetValue(command, out Operation? op))
                {
                    op.Perform();
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }
        }
    }
}
