﻿Imports System.Windows.Controls
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.FacFacturaAnuladas
    Interface IConsultarDepositoFacFacturaAnuladas
        Inherits IPaginaBaseFac
        Property FacFacturaAnuladaFiltrar() As Object

        ReadOnly Property FacFacturaAnuladaSeleccionado() As Object

        Property Resultados() As Object

        'Property Id() As Integer



        ReadOnly Property FechaDeposito() As String

        Property Tmonto() As Double

        Property NReg() As Integer

        Property Bancos() As Object

        Property Banco() As Object

        'ReadOnly Property Region() As String

        Property CurSortCol() As GridViewColumnHeader

        Property CurAdorner() As SortAdorner

        Property ListaResultados() As ListView
    End Interface
End Namespace