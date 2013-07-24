Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacBancos
    Interface IConsultarFacBanco
        Inherits IPaginaBaseFac
        Property FacBanco() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        Property Moneda() As Object

        Property Monedas() As Object

        Property Publica As String

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace