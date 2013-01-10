Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Impuestos
    Interface IConsultarImpuestos
        Inherits IPaginaBaseFac
        Property ImpuestoFiltrar() As Object

        ReadOnly Property ImpuestoSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As DateTime

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace