Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaAnuladas
    Interface IConsultarFacFacturaAnuladas
        Inherits IPaginaBaseFac
        Property FacFacturaAnuladaFiltrar() As Object

        ReadOnly Property FacFacturaAnuladaSeleccionado() As Object

        ReadOnly Property FechaCobro() As String

        ReadOnly Property Id() As String

        Property Resultados() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property Bancos() As Object

        Property Banco() As Object

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView
    End Interface
End Namespace