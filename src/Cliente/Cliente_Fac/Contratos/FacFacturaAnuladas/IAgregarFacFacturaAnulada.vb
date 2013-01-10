Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaAnuladas
    Interface IAgregarFacFacturaAnulada
        Inherits IPaginaBaseFac
        Property FacFacturaAnulada() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        Property Secuencia() As String

        Property Secuencia2() As String

        Property Control() As String

        Property Detalle() As String

        Property Detalle2() As String

        ReadOnly Property BDesg As Boolean

        ReadOnly Property Factura() As String
        'Property Persona() As Object

        'Property Personas() As Object
        ReadOnly Property Localidad() As Char

        ReadOnly Property cpro() As String

        WriteOnly Property ActivaDesactiva() As Boolean

        Property Motivos() As Object

        Property Motivos2() As Object

        Property Motivo() As Object

        Property Motivo2() As Object

        Property MensajeErrorCobro() As String

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace