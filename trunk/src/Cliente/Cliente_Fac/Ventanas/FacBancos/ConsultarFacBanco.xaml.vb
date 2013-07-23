﻿Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacBancos
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacBancos
Imports Diginsoft.Bolet.Cliente.Fac.Contratos

Namespace Ventanas.FacBancos
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarFacBanco
        Inherits Page
        Implements IConsultarFacBanco



        Private _presentador As PresentadorConsultarFacBanco
        Private _cargada As Boolean

#Region "IConsultarFacBanco"

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


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.FacBancos.IConsultarFacBanco.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtDoc_Esp.IsEnabled = value
                Me._txtDoc_Ingl.IsEnabled = value
                Me._txtId.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.FacBancos.IConsultarFacBanco.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal FacBanco As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarFacBanco(Me, FacBanco)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarFacBanco, "Eliminar FacBanco", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                Me._presentador.Eliminar()
            End If
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

        Public Property FacBanco As Object Implements Contratos.FacBancos.IConsultarFacBanco.FacBanco
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace