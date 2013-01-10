Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacInternacionales
    Interface IFacInternacionalRegistro
        Inherits IPaginaBaseFac

        Property FacInternacional() As Object

        Sub Mensaje(ByVal mensaje__1 As String)

        Property Fecha() As Object

        Property FechaRecepcion() As Object

        Property Pais() As Object

        Property Paises() As Object

        'Property Usuario() As Object

        'Property Usuarios() As Object

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

        ReadOnly Property GetTipoPago() As Char

        WriteOnly Property SetTipoPago() As String

        'Property B1() As Boolean

        'Property B2() As Boolean

        'Property B3() As Boolean

        ' WriteOnly Property CrystalViewer() As Object

    End Interface
End Namespace