Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Correspondencias
    Interface IConsultarCorrespondencia
        Inherits IPaginaBaseFac
        Property Correspondencia() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace