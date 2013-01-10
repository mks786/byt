Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.ChequeRecidos
    Interface IConsultarChequeRecidos
        Inherits IPaginaBaseFac
        Property ChequeRecidoFiltrar() As Object

        ReadOnly Property ChequeRecidoSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer

        ReadOnly Property Fecha() As String

        ReadOnly Property FechaDeposito() As String

        ReadOnly Property FechaReg() As String

        Property Asociados() As Object

        Property Asociado() As Object

        Property Bancos() As Object

        Property Banco() As Object

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