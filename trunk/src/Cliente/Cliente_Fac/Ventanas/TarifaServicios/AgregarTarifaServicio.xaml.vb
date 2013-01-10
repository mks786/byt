﻿Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.TarifaServicios
    Partial Public Class AgregarTarifaServicio
        Inherits Page
        Implements IAgregarTarifaServicio

        Private _presentador As PresentadorAgregarTarifaServicio
        Private _cargada As Boolean

#Region "IAgregarTarifaServicio"

        Public Property EstaCargada() As Boolean Implements IPaginaBaseFac.EstaCargada
            Get
                Return Me._cargada
            End Get
            Set(ByVal value As Boolean)
                Me._cargada = value
            End Set
        End Property

        Public Property Tarifa() As Object Implements Contratos.TarifaServicios.IAgregarTarifaServicio.Tarifa
            Get
                Return Me._cbxTarifa.SelectedItem
            End Get
            Set(ByVal value As Object)
                Me._cbxTarifa.SelectedItem = value
            End Set
        End Property

        Public Property Tarifas() As Object Implements Contratos.TarifaServicios.IAgregarTarifaServicio.Tarifas
            Get
                Return Me._cbxTarifa.DataContext
            End Get
            Set(ByVal value As Object)
                Me._cbxTarifa.DataContext = value
            End Set
        End Property

        Public Sub FocoPredeterminado() Implements IPaginaBaseFac.FocoPredeterminado
            Me._txtId.Focus()
        End Sub

        Public Property TarifaServicio() As Object Implements Contratos.TarifaServicios.IAgregarTarifaServicio.TarifaServicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property

        Public Sub Mensaje(ByVal mensaje__1 As String) Implements Contratos.TarifaServicios.IAgregarTarifaServicio.Mensaje
            MessageBox.Show(mensaje__1, "Error", MessageBoxButton.OK, MessageBoxImage.[Error])
        End Sub


#End Region

        Public Sub New()
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorAgregarTarifaServicio(Me)
        End Sub

        Private Sub _btnCancelar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Cancelar()
        End Sub

        Private Sub _btnAceptar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Aceptar()
        End Sub

        Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not EstaCargada Then
                Me._presentador.CargarPagina()
                EstaCargada = True
            End If
        End Sub

        Public Sub _btnLimpiar_Click()
            Me._presentador.Limpiar()
        End Sub

    End Class
End Namespace
