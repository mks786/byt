Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacTarifas
    Interface IConsultarFacTarifa
        Inherits IPaginaBaseFac
        Property FacTarifa() As Object
        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace