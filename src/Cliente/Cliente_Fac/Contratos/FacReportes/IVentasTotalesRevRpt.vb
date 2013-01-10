Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IVentasTotalesRevRpt
        Inherits IPaginaBaseFac

        'WriteOnly Property CrystalViewer() As Object

        Property FechaInicio As String

        Property FechaFin As String

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace