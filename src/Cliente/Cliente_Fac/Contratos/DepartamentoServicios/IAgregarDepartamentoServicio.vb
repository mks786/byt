Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DepartamentoServicios
    Interface IAgregarDepartamentoServicio
        Inherits IPaginaBaseFac
        Property DepartamentoServicio() As Object

        Property Id() As Object

        Property Servicio() As Object

        Property Ids() As Object

        Property Servicios() As Object

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace