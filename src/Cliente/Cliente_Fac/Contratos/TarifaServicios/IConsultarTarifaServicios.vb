Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports Trascend.Bolet.Cliente.Ayuda
Namespace Contratos.TarifaServicios
    Interface IConsultarTarifaServicios
        Inherits IPaginaBaseFac
        Property TarifaServicioFiltrar() As Object

        Property Tarifa() As Object

        Property Tarifas() As Object

        ReadOnly Property TarifaServicioSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As String

        ReadOnly Property Tasa() As String

        ReadOnly Property Mont_Us() As String

        ReadOnly Property Mont_Bs() As String

        ReadOnly Property Mont_Bf() As String

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace