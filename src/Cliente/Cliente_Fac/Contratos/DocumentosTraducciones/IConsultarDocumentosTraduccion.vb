Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DocumentosTraducciones
    Interface IConsultarDocumentosTraduccion
        Inherits IPaginaBaseFac
        Property DocumentosTraduccion() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace