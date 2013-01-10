Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IFacturasDigitales
        Inherits IPaginaBaseFac

        Sub Mensaje(ByVal mensaje__1 As String)

        ReadOnly Property Fecha1() As Date?

        ReadOnly Property Fecha2() As Date?

        ReadOnly Property TipoMoneda() As String

        ReadOnly Property TipoFactura() As String

        ReadOnly Property MayorMenor() As Object

        'WriteOnly Property CrystalViewer() As Object

    End Interface
End Namespace