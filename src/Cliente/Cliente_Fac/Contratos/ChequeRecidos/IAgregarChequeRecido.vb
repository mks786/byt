Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.ChequeRecidos
    Interface IAgregarChequeRecido
        Inherits IPaginaBaseFac
        Property ChequeRecido() As Object

        Property Asociado() As Object

        Property Asociados() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'Property Persona() As Object

        'Property Personas() As Object

        Property BancoG() As Object

        Property BancoGs() As Object



        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace