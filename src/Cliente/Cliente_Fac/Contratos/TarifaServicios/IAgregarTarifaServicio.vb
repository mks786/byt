Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.TarifaServicios
    Interface IAgregarTarifaServicio
        Inherits IPaginaBaseFac
        Property TarifaServicio() As Object

        Property Tarifa() As Object

        Property Tarifas() As Object

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace