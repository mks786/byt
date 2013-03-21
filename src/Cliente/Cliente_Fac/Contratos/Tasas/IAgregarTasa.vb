Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Tasas
    Interface IAgregarTasa
        Inherits IPaginaBaseFac
        Property Tasa() As Object
        Sub Mensaje(ByVal mensaje__1 As String)

        Property Moneda As String
    End Interface
End Namespace