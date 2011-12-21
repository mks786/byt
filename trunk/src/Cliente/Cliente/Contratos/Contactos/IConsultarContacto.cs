﻿using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Contactos
{
    interface IConsultarContacto : IPaginaBase
    {
        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }

        object Contacto { get; set; }

        string getDepartamento { get; }

        string setDepartamento { set; }

        string setFuncion { set; }

        string getFuncion { get; }

        string getCorrespondencia { get; }

        string setCorrespondencia { set; }
        
        void mensaje(string mensaje);
    }
}