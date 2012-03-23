using System;
using System.Runtime.Remoting;
using Trascend.Bolet.Servicios;
using Trascend.Bolet.Servicios.Implementacion;

namespace Trascend.Bolet.Servidor
{
    public class Program
    {
        private const string _archivo = "Trascend.Bolet.Servidor.exe.config";

        [STAThread]
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure(_archivo, false);
            Console.WriteLine("Se han cargado " + ConfiguracionServicios.CargarUsuarios() + " sesiones de archivo XML");
            Console.WriteLine("Configuracion cargada...");
            Console.ReadLine();

        }
    }
}
