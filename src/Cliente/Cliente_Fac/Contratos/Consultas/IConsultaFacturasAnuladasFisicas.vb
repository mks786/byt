Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.Consultas
    Interface IConsultaFacturasAnuladasFisicas
        Inherits IPaginaBaseFac


        ReadOnly Property FacAnuladaFisicaSeleccionado() As Object



        Property Resultados() As Object



        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView

        WriteOnly Property Count As Integer
    End Interface
End Namespace