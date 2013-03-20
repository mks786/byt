Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacPagoBolivias
    Interface IConsultarFacPagoBolivia
        Inherits IPaginaBaseFac
        Property FacPagoBolivia() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        Property Asociado() As Object

        Property Asociados() As Object

        Property BancoRec() As Object

        Property BancosRec() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String
        'Property Region() As String 
        ReadOnly Property TipoPago() As Char

        Property Cartas() As Object

        Property Carta() As Object


        Property Banco() As Object

        Property Bancos() As Object

        Property idCartaFiltrar() As String

        Property NombreCarta() As String

        Property NombreCartaFiltrar() As String

        Property FechaCartaFiltrar() As String

        WriteOnly Property SetFormaPago() As String

        WriteOnly Property SetTipoPago() As String

        ReadOnly Property GetFormaPago() As Char

        Property TextoBotonModificar() As String
    End Interface
End Namespace