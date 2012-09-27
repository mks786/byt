using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente
{
    class App : Application
    {
        private const string _ventanaInicial = "Ventanas/Logines/Login.xaml";
        private const string _estiloPrincipal = "Estilos/EstiloPrincipal.xaml";
        private const string _archivo = "Trascend.Bolet.Cliente.exe.config";

        [STAThread]
        public static void Main()
        {
            RemotingConfiguration.Configure(_archivo, false);

            App app = new App();
            app.StartupUri = new Uri(_ventanaInicial, UriKind.Relative);
            ResourceDictionary estilo = new ResourceDictionary();
            estilo.Source = new Uri(_estiloPrincipal, UriKind.Relative);
            app.Resources.MergedDictionaries.Add(estilo);
            app.Run();
        }
    }
}
