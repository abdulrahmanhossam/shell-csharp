class Program
{
    static void Main()
    {
        var validCommand = new[] { "echo", "exit", "type" };

        while (true)
        {
            Console.Write("$ ");

            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
                continue;

            if (command == "exit" || command.StartsWith("exit "))
                break;
            else if (command!.StartsWith("echo "))
                Console.WriteLine(command[5..]);
            else if (command.StartsWith("type "))
            {
                string target = command[5..];
                if (validCommand.Contains(target))
                    Console.WriteLine($"{target} is a shell builtin");
                else
                    Console.WriteLine($"{target}: not found");
            }

            else
                Console.WriteLine($"{command}: command not found");
        }
    }
}