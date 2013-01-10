Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.DesgloseServicios
    Interface IConsultarDesgloseServicios
        Inherits IPaginaBaseFac
        Property DesgloseServicioFiltrar() As Object

        Property Servicio() As Object

        Property Servicios() As Object

        ReadOnly Property DesgloseServicioSeleccionado() As Object

        Property Resultados() As Object

        ReadOnly Property Pporc() As String

        Property Id() As Char

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace