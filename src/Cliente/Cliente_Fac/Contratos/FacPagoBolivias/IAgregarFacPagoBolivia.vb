Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacPagoBolivias
    Interface IAgregarFacPagoBolivia
        Inherits IPaginaBaseFac
        Property FacPagoBolivia() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property BancoRec() As Object

        Property BancosRec() As Object

        ReadOnly Property TipoPago() As Char

        Property Cartas() As Object

        Property Carta() As Object

        Property idCartaFiltrar() As String

        Property NombreCarta() As String

        Property NombreCartaFiltrar() As String

        Property FechaCartaFiltrar() As String

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace