Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacRecursos
    Interface IConsultarFacRecursos
        Inherits IPaginaBaseFac
        Property FacRecursoFiltrar() As Object

        ReadOnly Property FacRecursoSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace