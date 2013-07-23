Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacBancos
    Interface IAgregarFacBanco
        Inherits IPaginaBaseFac
        Property FacBanco() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property Publica As String

        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace