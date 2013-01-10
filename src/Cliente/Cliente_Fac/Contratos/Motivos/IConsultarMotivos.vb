Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Motivos
    Interface IConsultarMotivos
        Inherits IPaginaBaseFac
        Property MotivoFiltrar() As Object

        ReadOnly Property MotivoSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As Integer

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace