Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.Servicios
    Interface IConsultarServicios
        Inherits IPaginaBaseFac
        Property ServicioFiltrar() As Object

        ReadOnly Property ServicioSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        'ReadOnly Property Tipo() As Char
        'ReadOnly Property Localidad() As Char
        'ReadOnly Property EstructurasMultiples() As String
        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace