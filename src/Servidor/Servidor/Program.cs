using System;
using System.Runtime.Remoting;

namespace Trascend.Bolet.Servidor
{
    public class Program
    {
        private const string _archivo = "Trascend.Bolet.Servidor.exe.config";

        [STAThread]
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(_archivo, false);
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
