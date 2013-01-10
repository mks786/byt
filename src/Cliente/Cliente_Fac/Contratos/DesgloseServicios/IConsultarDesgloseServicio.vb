Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DesgloseServicios
    Interface IConsultarDesgloseServicio
        Inherits IPaginaBaseFac
        Property DesgloseServicio() As Object

        Property Servicio() As Object

        Property Servicios() As Object

        WriteOnly Property HabilitarCampos() As Boolean

        ReadOnly Property GetTipoDesgSer() As Char

        WriteOnly Property SetTipoDesgSer() As String
        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace