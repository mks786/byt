﻿Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
'Imports Trascend.Bolet.Cliente.Ayuda
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacAnualidades
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacAnualidades
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.FacAnualidades
    ''' <summary>
    ''' Interaction logic for ConsultarObjetos.xaml
    ''' </summary>
    Partial Public Class ConsultarFacAnualidades
        Inherits Page
        Implements IConsultarFacAnualidades

        Private _CurSortCol As GridViewColumnHeader = Nothing
        Private _CurAdorner As SortAdorner = Nothing
        Private _presentador As PresentadorConsultarFacAnualidades
        Private _cargada As Boolean

#Region "IConsultarFacAnualidades"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._txtId.Focus()
        End Sub

        Public Property FacAnualidadFiltrar() As Object Implements Contratos.FacAnualidades.IConsultarFacAnualidades.FacAnualidadFiltrar
            Get
                Return Me._splFiltro.DataContext
            End Get
            Set(ByVal value As Object)
                Me._splFiltro.DataContext = value
            End Set
        End Property

        Public ReadOnly Property FacAnualidadSeleccionado() As Object Implements Contratos.FacAnualidades.IConsultarFacAnualidades.FAcAnualidadSeleccionado
            Get
                Return Me._lstResultados.SelectedItem
            End Get
        End Property


        Public Property Resultados() As Object Implements Contratos.FacAnualidades.IConsultarFacAnualidades.Resultados
            Get
                Return Me._lstResultados.DataContext
            End Get
            Set(ByVal value As Object)
                Me._lstResultados.DataContext = value
            End Set
        End Property

        Public Property Id() As String Implements Contratos.FacAnualidades.IConsultarFacAnualidades.Id
            Get
                Return Me._txtId.Text
            End Get
            Set(ByVal value As String)
                Me._txtId.Text = value
            End Set
        End Property

        Public Property CurSortCol() As System.Windows.Controls.GridViewColumnHeader Implements Contratos.FacAnualidades.IConsultarFacAnualidades.CurSortCol
            Get
                Return _CurSortCol
            End Get
            Set(ByVal value As GridViewColumnHeader)
                _CurSortCol = value
            End Set
        End Property

        Public Property CurAdorner() As SortAdorner Implements Contratos.FacAnualidades.IConsultarFacAnualidades.CurAdorner
            Get
                Return _CurAdorner
            End Get
            Set(ByVal value As SortAdorner)
                _CurAdorner = value
            End Set
        End Property

        Public Property ListaResultados() As System.Windows.Controls.ListView Implements Contratos.FacAnualidades.IConsultarFacAnualidades.ListaResultados
            Get
                Return Me._lstResultados
            End Get
            Set(ByVal value As ListView)
                Me._lstResultados = value
            End Set
        End Property

#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False

            Me._presentador = New PresentadorConsultarFacAnualidades(Me)
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            Else
                Me._presentador.ActualizarTitulo()
            End If
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnConsultar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._btnConsultar.Focus()
            Me._presentador.Consultar()
        End Sub

        Private Sub _lstResultados_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            If Me._lstResultados.SelectedItem IsNot Nothing Then
                Me._presentador.IrConsultarFacAnualidad()
            End If
        End Sub

        Private Sub _Ordenar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.OrdenarColumna(TryCast(sender, GridViewColumnHeader))
        End Sub

        Public WriteOnly Property Count As Integer Implements Contratos.FacAnualidades.IConsultarFacAnualidades.Count
            Set(ByVal value As Integer)
                _lblHits.Text = value
            End Set
        End Property
    End Class
End Namespace
