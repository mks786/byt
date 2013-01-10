Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.DepartamentoServicios
    Interface IConsultarDepartamentoServicios
        Inherits IPaginaBaseFac
        Property DepartamentoServicioFiltrar() As Object

        Property GetSetId() As Object

        Property GetSetIds() As Object

        Property Servicios() As Object

        Property Servicio() As Object

        ReadOnly Property DepartamentoServicioSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace