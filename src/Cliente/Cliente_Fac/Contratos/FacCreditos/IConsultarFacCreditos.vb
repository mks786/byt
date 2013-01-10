Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacCreditos
    Interface IConsultarFacCreditos
        Inherits IPaginaBaseFac
        Property FacCreditoFiltrar() As Object

        ReadOnly Property FacCreditoSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer

        ReadOnly Property FechaCredito() As String

        ReadOnly Property Id() As String

        ReadOnly Property CreditoSent() As String

        Property Bancos() As Object

        Property Banco() As Object

        Property Idioma() As Object

        Property Idiomas() As Object

        Property Moneda() As Object

        Property Monedas() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace