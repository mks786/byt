Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacGestiones
    Interface IConsultarFacGestiones
        Inherits IPaginaBaseFac
        Property FacGestionFiltrar() As Object

        ReadOnly Property FacGestionSeleccionado() As Object

        'ReadOnly Property FacGestion() As String

        Property Resultados() As Object

        'Property Bancos() As Object

        'Property Banco() As Object

        'Property Idioma() As Object

        'Property Idiomas() As Object

        'Property Moneda() As Object

        'Property Monedas() As Object

        Property Asociados() As Object

        Property Asociado() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        Property Medio() As Object

        Property Medios() As Object

        Property Concepto() As Object

        Property Conceptos() As Object

        ReadOnly Property FechaGestion() As String

        Property Id() As String

        Property Inicial() As String

        Property Observacion() As String

        'Property ConceptoRespuesta() As Object

        'Property ConceptoRespuestas() As Object


        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace