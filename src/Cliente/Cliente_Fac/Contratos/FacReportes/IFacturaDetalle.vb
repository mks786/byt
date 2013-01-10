Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IFacturaDetalle
        Inherits IPaginaBaseFac

        Sub Mensaje(ByVal mensaje__1 As String)

        Property FechaInicio() As String

        Property FechaFin() As String

    End Interface
End Namespace