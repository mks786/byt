Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaProformas
    Interface IConsultarFacFacturaProformasTodas
        Inherits IPaginaBaseFac
        Property FacFacturaProformaFiltrar() As Object

        ReadOnly Property FacFacturaProformaSeleccionado() As Object

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
        WriteOnly Property CbpM() As Double

        WriteOnly Property Cba() As Double
        WriteOnly Property CbaM() As Double

        WriteOnly Property Cdp() As Double
        WriteOnly Property CdpM() As Double

        WriteOnly Property Cda() As Double
        WriteOnly Property CdaM() As Double

        WriteOnly Property Cbr() As Double
        WriteOnly Property CbrM() As Double

        WriteOnly Property Cbf() As Double
        WriteOnly Property CbfM() As Double

        WriteOnly Property Cdr() As Double
        WriteOnly Property CdrM() As Double

        WriteOnly Property Cdf() As Double
        WriteOnly Property CdfM() As Double

        Property Auto() As String

        Property Inicial() As String


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace