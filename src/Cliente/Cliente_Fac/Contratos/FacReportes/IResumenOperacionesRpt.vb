Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IResumenOperacionesRpt
        Inherits IPaginaBaseFac

        Sub Mensaje(ByVal mensaje__1 As String)

        Property Moneda() As Object

        Property Monedas() As Object

        Property FechaInicio As String

        Property FechaFin As String

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'WriteOnly Property CrystalViewer() As Object

    End Interface
End Namespace