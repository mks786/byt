Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacReportes
    Interface IFacFacturacionPendienteVieja
        Inherits IPaginaBaseFac

        Sub Mensaje(ByVal mensaje__1 As String)

        Property FechaCorte() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        Property Asociados2() As Object

        Property Asociado2() As Object

        Property idAsociadoFiltrar2() As String

        Property NombreAsociado2() As String

        Property NombreAsociadoFiltrar2() As String

        ' WriteOnly Property CrystalViewer() As Object

    End Interface
End Namespace