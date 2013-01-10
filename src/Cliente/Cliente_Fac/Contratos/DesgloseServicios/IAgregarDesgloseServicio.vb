Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DesgloseServicios
    Interface IAgregarDesgloseServicio
        Inherits IPaginaBaseFac
        Property DesgloseServicio() As Object

        Property Servicio() As Object

        Property Servicios() As Object

        ReadOnly Property TipoDesgSer() As Char

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace