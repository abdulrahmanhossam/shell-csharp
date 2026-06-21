class Program
{
    static void Main()
    {
        string[] validBuiltinCommand = ["echo", "exit", "type"];
        string pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        string[] paths = pathEnv.Split(Path.PathSeparator);

        while (true)
        {
            Console.Write("$ ");

            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
                continue;

            if (command == "exit" || command.StartsWith("exit "))
            {
                break;
            }
            else if (command!.StartsWith("echo "))
            {

                Console.WriteLine(command[5..]);
            }
            else if (command.StartsWith("type "))
            {
                string target = command[5..];

                if (validBuiltinCommand.Contains(target))
                {
                    Console.WriteLine($"{target} is a shell builtin");
                }
                else
                {
                    string foundPath = null!;

                    foreach (var path in paths)
                    {
                        string fullPath = Path.Combine(path, target);

                        if (File.Exists(fullPath))
                        {
                            var fileMode = File.GetUnixFileMode(fullPath);

                            bool isExecutable = fileMode.HasFlag(UnixFileMode.UserExecute) ||
                                                fileMode.HasFlag(UnixFileMode.GroupExecute) ||
                                                fileMode.HasFlag(UnixFileMode.OtherExecute);

                            if (isExecutable)
                            {
                                foundPath = fullPath;
                                break;
                            }
                        }
                    }
                    if (foundPath != null)
                    {
                        Console.WriteLine($"{target} is {foundPath}");
                    }
                    else
                    {
                        Console.WriteLine($"{target}: not found");
                    }
                }

            }
            else
            {
                Console.WriteLine($"{command}: command not found");
            }
        }
    }
}
