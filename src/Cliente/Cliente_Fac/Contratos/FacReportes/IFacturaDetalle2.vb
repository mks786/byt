Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IFacturaDetalle
        Inherits IPaginaBaseFac

        Sub Mensaje(ByVal mensaje__1 As String)

        ReadOnly Property Fecha1() As Object

        ReadOnly Property Fecha2() As Object

    End Interface
End Namespace