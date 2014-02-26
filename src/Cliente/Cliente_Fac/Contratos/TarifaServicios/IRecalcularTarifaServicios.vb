Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.TarifaServicios
    Public Interface IRecalcularTarifaServicios
        Inherits IPaginaBaseFac

        Property TasaCambio As String

        Sub Mensaje(mensaje As String, tipoMensaje As Integer)

    End Interface

End Namespace
