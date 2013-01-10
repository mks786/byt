Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.Etiquetas
    Interface IConsultarEtiquetas
        Inherits IPaginaBaseFac
        Property EtiquetaFiltrar() As Object

        ReadOnly Property EtiquetaSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace