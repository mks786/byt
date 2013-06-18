Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacTarifas
    Interface IConsultarFacTarifas
        Inherits IPaginaBaseFac
        Property FacTarifaFiltrar() As Object

        ReadOnly Property FacTarifaSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As Integer

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace