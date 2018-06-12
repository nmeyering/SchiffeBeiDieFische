using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleServer
{
    enum Mode
    {
        server,
        client
    } 
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Mode mode=Mode.server;
            if (args.Length>0)
            {                
                switch (args[0])
                {
                    case "-server":
                        mode = Mode.server;
                        break;
                    case "-client":
                        mode = Mode.client;
                        break;
                    default:
                        Console.WriteLine("false argument");
                        return;                        
                }
            }
            while (true)
            {
                Console.WriteLine("started in mode " + mode.ToString());
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                Console.WriteLine("out -> " + input);
            }
        }
    }
}
