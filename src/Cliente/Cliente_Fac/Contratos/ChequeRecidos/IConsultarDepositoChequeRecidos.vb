Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.ChequeRecidos
    Interface IConsultarDepositoChequeRecidos
        Inherits IPaginaBaseFac
        Property ChequeRecidoFiltrar() As Object

        ReadOnly Property ChequeRecidoSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer



        ReadOnly Property FechaDeposito() As String

        Property Tmonto() As Double

        Property NReg() As Integer

        Property Bancos() As Object

        Property Banco() As Object

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace