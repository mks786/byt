Imports System.Windows
Imports System.Windows.Controls
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Servicios
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.Servicios
Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Ventanas.Servicios
    ''' <summary>
    ''' Interaction logic for ConsultarObjeto.xaml
    ''' </summary>
    Partial Public Class ConsultarServicio
        Inherits Page
        Implements IConsultarServicio



        Private _presentador As PresentadorConsultarServicio
        Private _cargada As Boolean

#Region "IConsultarServicio"

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

        'Combox
        Public ReadOnly Property GetTipo() As Char Implements IConsultarServicio.GetTipo
            Get
                If Not String.IsNullOrEmpty(Me._cbxItipo.Text) Then
                    Return (Me._cbxItipo.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public ReadOnly Property GetLocalidad() As Char Implements IConsultarServicio.GetLocalidad
            Get
                If Not String.IsNullOrEmpty(Me._cbxLocal.Text) Then
                    Return (Me._cbxLocal.Text)(0)
                Else
                    Return " "c
                End If
            End Get
        End Property

        Public ReadOnly Property GetEstructurasMultiples() As String Implements IConsultarServicio.GetEstructurasMultiples
            Get
                If Not String.Equals("", Me._cbxCodmult.Text) Then
                    Return DirectCast(Me._cbxCodmult.Text, String)
                End If
                Return ""
            End Get
        End Property

        Public WriteOnly Property SetTipo() As String Implements IConsultarServicio.SetTipo
            Set(ByVal value As String)
                Me._cbxItipo.Text = value
            End Set
        End Property

        Public WriteOnly Property SetLocalidad() As String Implements IConsultarServicio.SetLocalidad
            Set(ByVal value As String)
                Me._cbxLocal.Text = value
            End Set
        End Property

        Public WriteOnly Property SetEstructurasMultiples() As String Implements IConsultarServicio.SetEstructurasMultiples
            Set(ByVal value As String)
                Me._cbxCodmult.Text = value
            End Set
        End Property


        Public WriteOnly Property HabilitarCampos() As Boolean Implements Contratos.Servicios.IConsultarServicio.HabilitarCampos
            Set(ByVal value As Boolean)
                Me._txtId.IsEnabled = value
                Me._txtCod_Cont.IsEnabled = value
                Me._txtDetalle_Esp.IsEnabled = value
                Me._txtDetalle_Ing.IsEnabled = value
                Me._cbxItipo.IsEnabled = value
                Me._cbxLocal.IsEnabled = value
                Me._chkItidoc.IsEnabled = value
                Me._chkItraduc.IsEnabled = value
                Me._chkAnual.IsEnabled = value
                Me._txtdetalles_esp.IsEnabled = value
                Me._txtdetalle_ing.IsEnabled = value
                Me._cbxCodmult.IsEnabled = value
                Me._txtXreferencia.IsEnabled = value
                Me._chkImult.IsEnabled = value
                Me._chkImodpr.IsEnabled = value
                Me._chkRecursos.IsEnabled = value
                Me._chkMaterial.IsEnabled = value
                Me._chkAimpuesto.IsEnabled = value
                Me._chkDesg.IsEnabled = value
                Me._chkPrec.IsEnabled = value
                Me._txtDetalles_Ing.IsEnabled = value
            End Set
        End Property


        Public Property TextoBotonModificar() As String Implements Contratos.Servicios.IConsultarServicio.TextoBotonModificar
            Get
                Return Me._txbModificar.Text
            End Get
            Set(ByVal value As String)
                Me._txbModificar.Text = value
            End Set
        End Property

#End Region

        Public Sub New(ByVal Servicio As Object)
            InitializeComponent()
            Me._cargada = False
            Me._presentador = New PresentadorConsultarServicio(Me, Servicio)
        End Sub
        Private Sub _btnModificar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Modificar()
        End Sub

        Private Sub _btnRegresar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me._presentador.Regresar()
        End Sub

        Private Sub _btnEliminar_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_ConfirmacionEliminarServicio, "Eliminar Servicio", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
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

        Public Property Servicio As Object Implements Contratos.Servicios.IConsultarServicio.Servicio
            Get
                Return Me._gridDatos.DataContext
            End Get
            Set(ByVal value As Object)
                Me._gridDatos.DataContext = value
            End Set
        End Property
    End Class
End Namespace