Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.TarifaServicios
    Interface IConsultarTarifaServicio
        Inherits IPaginaBaseFac
        Property TarifaServicio() As Object

        Property Tarifa() As Object

        Property Tarifas() As Object

        WriteOnly Property HabilitarCampos() As Boolean

        'Property Region() As String 

        Property TextoBotonModificar() As String
    End Interface
End Namespace