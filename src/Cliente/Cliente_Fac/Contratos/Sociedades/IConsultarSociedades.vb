Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Sociedades
    Interface IConsultarSociedades
        Inherits IPaginaBaseFac
        Property SociedadFiltrar() As Object

        ReadOnly Property SociedadSeleccionado() As Object

        Property Resultados() As Object

        Property Id() As Integer

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer

    End Interface
End Namespace