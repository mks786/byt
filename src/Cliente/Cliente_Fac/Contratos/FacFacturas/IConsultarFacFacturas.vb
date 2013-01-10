Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturas
    Interface IConsultarFacFacturas
        Inherits IPaginaBaseFac
        Property FacFacturaFiltrar() As Object

        ReadOnly Property FacFacturaSeleccionado() As Object

        ReadOnly Property FechaFactura() As String

        ReadOnly Property Id() As String

        Property Resultados() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        'Property Bancos() As Object

        'Property Banco() As Object

        'Property Idioma() As Object

        'Property Idiomas() As Object

        'Property Moneda() As Object

        'Property Monedas() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        WriteOnly Property Cbp() As Double

        WriteOnly Property Cba() As Double

        WriteOnly Property Cdp() As Double

        WriteOnly Property Cda() As Double

        WriteOnly Property Cbr() As Double

        WriteOnly Property Cbf() As Double

        WriteOnly Property Cdr() As Double

        WriteOnly Property Cdf() As Double


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace