Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Imports System.Data

Namespace Contratos.FacAsociadoMarcaPatentes
    Interface IConsultarFacVistaFacturacionCxpInternas
        Inherits IPaginaBaseFac
        'Property FacGestionFiltrar() As Object
        Property Resultados() As Object

        Property FacVistaFacCxpInternaSeleccionado() As Object

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer

        Sub ExportarListView(ByVal datos As DataTable)

        Sub Mensaje(mensaje As String, tipo As Integer)

    End Interface
End Namespace