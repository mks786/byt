Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacPagoBolivias
    Interface IConsultarFacPagoBolivias
        Inherits IPaginaBaseFac
        Property FacPagoBoliviaFiltrar() As Object

        ReadOnly Property FacPagoBoliviaSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer

        ReadOnly Property FechaBanco() As String

        'ReadOnly Property FechaPago() As String

        ReadOnly Property FechaReg() As String

        Property Asociados() As Object

        Property Asociado() As Object

        Property BancosRec() As Object

        Property BancoRec() As Object

        Property idAsociadoFiltrar() As String

        Property NombreAsociado() As String

        Property NombreAsociadoFiltrar() As String

        ReadOnly Property TipoPago() As Char
        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace