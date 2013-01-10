Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacGestiones
    Interface IAgregarFacGestion
        Inherits IPaginaBaseFac

        Property Id() As Integer?

        Property FacGestion() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property Medio() As Object

        Property Medios() As Object

        Property TipoCliente() As Object

        Property TipoClientes() As Object

        Property Concepto() As Object

        Property Conceptos() As Object

        Property ConceptoRespuesta() As Object

        Property ConceptoRespuestas() As Object

        'Property Banco() As Object

        'Property Bancos() As Object

        Property Cartas() As Object

        Property Carta() As Object

        Property idCartaFiltrar() As String

        Property NombreCarta() As String

        Property NombreCartaFiltrar() As String

        Property FechaCartaFiltrar() As String

        Property Cartas_2() As Object

        Property Carta_2() As Object

        Property idCarta_2Filtrar() As String

        Property NombreCarta_2() As String

        Property NombreCarta_2Filtrar() As String

        Property FechaCarta_2Filtrar() As String


        Property MensajeErrorFacGestion() As String

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace