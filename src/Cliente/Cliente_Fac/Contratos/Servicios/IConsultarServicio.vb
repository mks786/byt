Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Servicios
    Interface IConsultarServicio
        Inherits IPaginaBaseFac
        Property Servicio() As Object
        WriteOnly Property HabilitarCampos() As Boolean
        ReadOnly Property GetTipo() As Char
        ReadOnly Property GetLocalidad() As Char
        ReadOnly Property GetEstructurasMultiples() As String
        WriteOnly Property SetTipo() As String
        WriteOnly Property SetLocalidad() As String
        WriteOnly Property SetEstructurasMultiples() As String
        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace