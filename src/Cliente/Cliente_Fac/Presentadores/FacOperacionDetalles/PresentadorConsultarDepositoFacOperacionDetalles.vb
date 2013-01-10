Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Linq
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports NLog
Imports Trascend.Bolet.Cliente.Ayuda
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetalles
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionDetalles
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionDetalles
    Class PresentadorConsultarDepositoFacOperacionDetalles
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionDetalles
        Private _FacOperacionDetalleServicios As IFacOperacionDetalleServicios
        Private _FacOperacionDetalles As IList(Of FacOperacionDetalle)
        Dim FacOperacionDetalleselect As IList(Of FacOperacionDetalleSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionDetalles)
            Try
                Me._ventana = ventana
                Me._FacOperacionDetalleServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleServicios")), IFacOperacionDetalleServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionDetalles, Recursos.Ids.fac_ConsultarFacOperacionDetalle)
        End Sub

        ''' <summary>
        ''' Método que carga los datos iniciales a mostrar en la página
        ''' </summary>
        Public Sub CargarPagina()
            Mouse.OverrideCursor = Cursors.Wait

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                ActualizarTitulo()


                Dim FacOperacionDetalleAuxiliar As New FacOperacionDetalle()
                FacOperacionDetalleAuxiliar.Deposito = "1"
                'Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ConsultarTodos()
                Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ObtenerFacOperacionDetallesFiltro(FacOperacionDetalleAuxiliar)
                FacOperacionDetalleselect = convertir_FacOperacionDetalleSelec(Me._FacOperacionDetalles)
                Me._ventana.Resultados = FacOperacionDetalleselect
                Dim chequevacio As New FacOperacionDetalle
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionDetalleFiltrar = chequevacio


                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._ventana.Asociados = asociados

                Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ConsultarTodos()
                Dim primerafacbanco As New FacBanco()
                primerafacbanco.Id = Integer.MinValue
                facbancos.Insert(0, primerafacbanco)
                Me._ventana.Bancos = facbancos

                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Me.Navegar(ex.Message, True)
            Catch ex As RemotingException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, True)
            Catch ex As SocketException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, True)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub


        ''' <summary>
        ''' Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        ''' por pantalla
        ''' </summary>
        Public Sub AplicarDeposito()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim v_FacOperacionDetalle As List(Of FacOperacionDetalleSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetalleSelec))
                Dim v_FacOperacionDetalle2 As FacOperacionDetalle = DirectCast(_ventana.FacOperacionDetalleFiltrar, FacOperacionDetalle)
                v_FacOperacionDetalle2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionDetalle2 As List(Of FacOperacionDetalle) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetalle))

                For i As Integer = 0 To v_FacOperacionDetalle.Count - 1
                    If (v_FacOperacionDetalle(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionDetalle(i).Deposito = "2"
                            v_FacOperacionDetalle(i).FechaDeposito = v_FacOperacionDetalle2.FechaDeposito
                            v_FacOperacionDetalle(i).NDeposito = v_FacOperacionDetalle2.NDeposito
                            v_FacOperacionDetalle(i).Banco = v_FacOperacionDetalle2.Banco
                            Dim a As New FacOperacionDetalle
                            a = convertir_FacOperacionDetalle(v_FacOperacionDetalle(i))
                            Dim exitoso As Boolean = _FacOperacionDetalleServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionDetalleAuxiliar As New FacOperacionDetalle()
                FacOperacionDetalleAuxiliar.Deposito = "1"
                'Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ConsultarTodos()
                Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ObtenerFacOperacionDetallesFiltro(FacOperacionDetalleAuxiliar)
                FacOperacionDetalleselect = convertir_FacOperacionDetalleSelec(Me._FacOperacionDetalles)
                Me._ventana.Resultados = FacOperacionDetalleselect

                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        ''' <summary>
        ''' Método que invoca una nueva página "ConsultarFacOperacionDetalle" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionDetalle()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionDetalle(Me._ventana.FacOperacionDetalleSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionDetalle())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que ordena una columna
        ''' </summary>
        Public Sub OrdenarColumna(ByVal column As GridViewColumnHeader)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim field As [String] = TryCast(column.Tag, [String])

            If Me._ventana.CurSortCol IsNot Nothing Then
                AdornerLayer.GetAdornerLayer(Me._ventana.CurSortCol).Remove(Me._ventana.CurAdorner)
                Me._ventana.ListaResultados.Items.SortDescriptions.Clear()
            End If

            Dim newDir As ListSortDirection = ListSortDirection.Ascending
            'If Me._ventana.CurSortCol = column AndAlso Me._ventana.CurAdorner.Direction = newDir Then
            '    newDir = ListSortDirection.Descending
            'End If

            Me._ventana.CurSortCol = column
            Me._ventana.CurAdorner = New SortAdorner(Me._ventana.CurSortCol, newDir)
            AdornerLayer.GetAdornerLayer(Me._ventana.CurSortCol).Add(Me._ventana.CurAdorner)
            Me._ventana.ListaResultados.Items.SortDescriptions.Add(New SortDescription(field, newDir))

            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        'para tildar  o destildar 
        Public Sub seleccionar(ByVal value As Boolean)

            Try
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If


                'Dim FacOperacionDetalleAuxiliar As New FacOperacionDetalle()
                'FacOperacionDetalleAuxiliar.Deposito = "1"

                ''Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ConsultarTodos()
                'Me._FacOperacionDetalles = Me._FacOperacionDetalleServicios.ObtenerFacOperacionDetallesFiltro(FacOperacionDetalleAuxiliar)

                'FacOperacionDetalleselect = convertir_FacOperacionDetalleSelec(Me._FacOperacionDetalles)

                For i As Integer = 0 To FacOperacionDetalleselect.Count - 1
                    FacOperacionDetalleselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionDetalleselect

                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Me.Navegar(ex.Message, True)
            Catch ex As RemotingException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorRemoting, True)
            Catch ex As SocketException
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorConexionServidor, True)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub


        'para covertir una clase FacOperacionDetalle a FacOperacionDetalleSelec
        Public Function convertir_FacOperacionDetalleSelec(ByVal v_FacOperacionDetalle As IList(Of FacOperacionDetalle)) As IList(Of FacOperacionDetalleSelec)
            Dim FacOperacionDetalleSelec As New List(Of FacOperacionDetalleSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionDetalle.Count
            For i As Integer = 0 To v_FacOperacionDetalle.Count - 1
                FacOperacionDetalleSelec.Add(New FacOperacionDetalleSelec)
                FacOperacionDetalleSelec.Item(i).Seleccion = False
                FacOperacionDetalleSelec.Item(i).Banco = v_FacOperacionDetalle.Item(i).Banco
                FacOperacionDetalleSelec.Item(i).BancoG = v_FacOperacionDetalle.Item(i).BancoG
                FacOperacionDetalleSelec.Item(i).Id = v_FacOperacionDetalle.Item(i).Id
                FacOperacionDetalleSelec.Item(i).NDeposito = v_FacOperacionDetalle.Item(i).NDeposito
                FacOperacionDetalleSelec.Item(i).Deposito = v_FacOperacionDetalle.Item(i).Deposito
                FacOperacionDetalleSelec.Item(i).NCheque = v_FacOperacionDetalle.Item(i).NCheque
                FacOperacionDetalleSelec.Item(i).Monto = v_FacOperacionDetalle.Item(i).Monto
                monto = v_FacOperacionDetalle.Item(i).Monto + monto
                FacOperacionDetalleSelec.Item(i).Fecha = v_FacOperacionDetalle.Item(i).Fecha
                FacOperacionDetalleSelec.Item(i).FechaReg = v_FacOperacionDetalle.Item(i).FechaReg
                FacOperacionDetalleSelec.Item(i).FechaDeposito = v_FacOperacionDetalle.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionDetalleSelec
        End Function

        'para covertir una clase FacOperacionDetalleSelec a FacOperacionDetalle
        Public Function convertir_FacOperacionDetalle(ByVal v_FacOperacionDetalleSelec2 As FacOperacionDetalleSelec) As FacOperacionDetalle
            Dim FacOperacionDetalle2 As New FacOperacionDetalle
            FacOperacionDetalle2.Banco = v_FacOperacionDetalleSelec2.Banco
            FacOperacionDetalle2.BancoG = v_FacOperacionDetalleSelec2.BancoG
            FacOperacionDetalle2.Id = v_FacOperacionDetalleSelec2.Id
            FacOperacionDetalle2.NDeposito = v_FacOperacionDetalleSelec2.NDeposito
            FacOperacionDetalle2.Deposito = v_FacOperacionDetalleSelec2.Deposito
            FacOperacionDetalle2.NCheque = v_FacOperacionDetalleSelec2.NCheque
            FacOperacionDetalle2.Monto = v_FacOperacionDetalleSelec2.Monto
            FacOperacionDetalle2.Fecha = v_FacOperacionDetalleSelec2.Fecha
            FacOperacionDetalle2.FechaReg = v_FacOperacionDetalleSelec2.FechaReg
            FacOperacionDetalle2.FechaDeposito = v_FacOperacionDetalleSelec2.FechaDeposito

            Return FacOperacionDetalle2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionDetalleSelec
        Inherits FacOperacionDetalle

        Private p_seleccion As Boolean

        Public Property Seleccion() As Boolean
            Get
                Return Me.p_seleccion
            End Get
            Set(ByVal Value As Boolean)
                Me.p_seleccion = Value
            End Set
        End Property

    End Class

End Namespace