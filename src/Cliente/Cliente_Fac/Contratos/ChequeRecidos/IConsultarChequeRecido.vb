Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.ChequeRecidos
    Interface IConsultarChequeRecido
        Inherits IPaginaBaseFac
        Property ChequeRecido() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property BancoG() As Object

        Property Monto() As Double

        Property BancoGs() As Object
        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace