Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaProformas
    Interface IProformaaFactura
        Inherits IPaginaBaseFac
        Property FacFacturaProformaFiltrar() As Object

        ReadOnly Property FacFacturaProformaSeleccionado() As Object

        ReadOnly Property FechaFactura() As String

        ReadOnly Property Id() As String

        Property Resultados() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        WriteOnly Property VerTipo() As String

        Property Rechazar() As String

        'Property Bancos() As Object

        'Property Banco() As Object

        'Property Idioma() As Object

        'Property Idiomas() As Object

        'Property Moneda() As Object

        'Property Monedas() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        WriteOnly Property Mba() As Double

        WriteOnly Property Cba() As Double

        WriteOnly Property Mda() As Double

        WriteOnly Property Cda() As Double

        'WriteOnly Property Cbr() As Double

        'WriteOnly Property Cbf() As Double

        'WriteOnly Property Cdr() As Double

        'WriteOnly Property Cdf() As Double


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer

        Property Campos() As Object

        Property CampoSeleccionado() As Object

        Property Ordenamientos() As Object

        Property Ordenamiento() As Object

    End Interface
End Namespace