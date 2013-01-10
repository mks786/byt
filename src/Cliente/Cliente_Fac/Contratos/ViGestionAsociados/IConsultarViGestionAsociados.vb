Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.ViGestionAsociados
    Interface IConsultarViGestionAsociados
        Inherits IPaginaBaseFac
        Property ViGestionAsociadoFiltrar() As Object

        ReadOnly Property ViGestionAsociadoSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer

        ReadOnly Property FechaUltima() As String

        Property NCantidad() As String

        Property NSaldo() As String

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