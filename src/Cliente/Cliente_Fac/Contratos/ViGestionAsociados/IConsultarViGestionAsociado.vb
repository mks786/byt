Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Contratos
Namespace Contratos.ViGestionAsociados
    Interface IConsultarViGestionAsociado
        Inherits IPaginaBase
        Property ViGestionAsociado() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace