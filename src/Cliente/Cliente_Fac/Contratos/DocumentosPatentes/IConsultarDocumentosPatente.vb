Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DocumentosPatentes
    Interface IConsultarDocumentosPatente
        Inherits IPaginaBaseFac
        Property DocumentosPatente() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace