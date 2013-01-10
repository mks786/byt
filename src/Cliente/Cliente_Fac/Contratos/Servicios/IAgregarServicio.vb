Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Servicios
    Interface IAgregarServicio
        Inherits IPaginaBaseFac
        Property Servicio() As Object

        ReadOnly Property Tipo() As Char
        ReadOnly Property Localidad() As Char
        ReadOnly Property EstructurasMultiples() As String

        Sub Mensaje(ByVal mensaje__1 As String)

    End Interface
End Namespace