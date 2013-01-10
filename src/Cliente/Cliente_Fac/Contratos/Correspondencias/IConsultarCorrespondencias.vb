Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.Correspondencias
    Interface IConsultarCorrespondencias
        Inherits IPaginaBaseFac
        Property CorrespondenciaFiltrar() As Object

        ReadOnly Property CorrespondenciaSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace