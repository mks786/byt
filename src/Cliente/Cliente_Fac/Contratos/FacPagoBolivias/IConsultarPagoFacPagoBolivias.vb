Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacPagoBolivias
    Interface IConsultarPagoFacPagoBolivias
        Inherits IPaginaBaseFac
        Property FacPagoBoliviaFiltrar() As Object

        ReadOnly Property FacPagoBoliviaSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer

        Property BancosPag() As Object

        Property BancoPag() As Object

        ReadOnly Property FormaPago() As Char

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace