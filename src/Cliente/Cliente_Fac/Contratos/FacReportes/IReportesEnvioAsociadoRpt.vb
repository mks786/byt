Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IReportesEnvioAsociadoRpt
        Inherits IPaginaBaseFac
        WriteOnly Property CrystalViewer() As Object

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace