Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DepartamentoServicios
    Interface IConsultarDepartamentoServicio
        Inherits IPaginaBaseFac
        Property DepartamentoServicio() As Object

        Property Id() As Object

        Property Servicio() As Object

        Property Ids() As Object

        Property Servicios() As Object

        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace