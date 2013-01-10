Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IFacFacturacionLoteVieja
        Inherits IPaginaBaseFac

        Property desde() As String

        Property hasta() As String

        ReadOnly Property Tipo() As Integer

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace