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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionAnuladas
    Class PresentadorConsultarDepositoFacOperacionAnuladas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionAnuladas
        Private _FacOperacionAnuladaServicios As IFacOperacionAnuladaServicios
        Private _FacOperacionAnuladas As IList(Of FacOperacionAnulada)
        Dim FacOperacionAnuladaselect As IList(Of FacOperacionAnuladaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionAnuladas)
            Try
                Me._ventana = ventana
                Me._FacOperacionAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionAnuladaServicios")), IFacOperacionAnuladaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionAnuladas, Recursos.Ids.fac_ConsultarFacOperacionAnulada)
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


                Dim FacOperacionAnuladaAuxiliar As New FacOperacionAnulada()
                FacOperacionAnuladaAuxiliar.Deposito = "1"
                'Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ConsultarTodos()
                Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ObtenerFacOperacionAnuladasFiltro(FacOperacionAnuladaAuxiliar)
                FacOperacionAnuladaselect = convertir_FacOperacionAnuladaSelec(Me._FacOperacionAnuladas)
                Me._ventana.Resultados = FacOperacionAnuladaselect
                Dim chequevacio As New FacOperacionAnulada
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionAnuladaFiltrar = chequevacio


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

                Dim v_FacOperacionAnulada As List(Of FacOperacionAnuladaSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionAnuladaSelec))
                Dim v_FacOperacionAnulada2 As FacOperacionAnulada = DirectCast(_ventana.FacOperacionAnuladaFiltrar, FacOperacionAnulada)
                v_FacOperacionAnulada2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionAnulada2 As List(Of FacOperacionAnulada) = DirectCast(_ventana.Resultados, List(Of FacOperacionAnulada))

                For i As Integer = 0 To v_FacOperacionAnulada.Count - 1
                    If (v_FacOperacionAnulada(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionAnulada(i).Deposito = "2"
                            v_FacOperacionAnulada(i).FechaDeposito = v_FacOperacionAnulada2.FechaDeposito
                            v_FacOperacionAnulada(i).NDeposito = v_FacOperacionAnulada2.NDeposito
                            v_FacOperacionAnulada(i).Banco = v_FacOperacionAnulada2.Banco
                            Dim a As New FacOperacionAnulada
                            a = convertir_FacOperacionAnulada(v_FacOperacionAnulada(i))
                            Dim exitoso As Boolean = _FacOperacionAnuladaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionAnuladaAuxiliar As New FacOperacionAnulada()
                FacOperacionAnuladaAuxiliar.Deposito = "1"
                'Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ConsultarTodos()
                Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ObtenerFacOperacionAnuladasFiltro(FacOperacionAnuladaAuxiliar)
                FacOperacionAnuladaselect = convertir_FacOperacionAnuladaSelec(Me._FacOperacionAnuladas)
                Me._ventana.Resultados = FacOperacionAnuladaselect

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
        ''' Método que invoca una nueva página "ConsultarFacOperacionAnulada" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionAnulada()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionAnulada(Me._ventana.FacOperacionAnuladaSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionAnulada())
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


                'Dim FacOperacionAnuladaAuxiliar As New FacOperacionAnulada()
                'FacOperacionAnuladaAuxiliar.Deposito = "1"

                ''Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ConsultarTodos()
                'Me._FacOperacionAnuladas = Me._FacOperacionAnuladaServicios.ObtenerFacOperacionAnuladasFiltro(FacOperacionAnuladaAuxiliar)

                'FacOperacionAnuladaselect = convertir_FacOperacionAnuladaSelec(Me._FacOperacionAnuladas)

                For i As Integer = 0 To FacOperacionAnuladaselect.Count - 1
                    FacOperacionAnuladaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionAnuladaselect

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


        'para covertir una clase FacOperacionAnulada a FacOperacionAnuladaSelec
        Public Function convertir_FacOperacionAnuladaSelec(ByVal v_FacOperacionAnulada As IList(Of FacOperacionAnulada)) As IList(Of FacOperacionAnuladaSelec)
            Dim FacOperacionAnuladaSelec As New List(Of FacOperacionAnuladaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionAnulada.Count
            For i As Integer = 0 To v_FacOperacionAnulada.Count - 1
                FacOperacionAnuladaSelec.Add(New FacOperacionAnuladaSelec)
                FacOperacionAnuladaSelec.Item(i).Seleccion = False
                FacOperacionAnuladaSelec.Item(i).Banco = v_FacOperacionAnulada.Item(i).Banco
                FacOperacionAnuladaSelec.Item(i).BancoG = v_FacOperacionAnulada.Item(i).BancoG
                FacOperacionAnuladaSelec.Item(i).Id = v_FacOperacionAnulada.Item(i).Id
                FacOperacionAnuladaSelec.Item(i).NDeposito = v_FacOperacionAnulada.Item(i).NDeposito
                FacOperacionAnuladaSelec.Item(i).Deposito = v_FacOperacionAnulada.Item(i).Deposito
                FacOperacionAnuladaSelec.Item(i).NCheque = v_FacOperacionAnulada.Item(i).NCheque
                FacOperacionAnuladaSelec.Item(i).Monto = v_FacOperacionAnulada.Item(i).Monto
                monto = v_FacOperacionAnulada.Item(i).Monto + monto
                FacOperacionAnuladaSelec.Item(i).Fecha = v_FacOperacionAnulada.Item(i).Fecha
                FacOperacionAnuladaSelec.Item(i).FechaReg = v_FacOperacionAnulada.Item(i).FechaReg
                FacOperacionAnuladaSelec.Item(i).FechaDeposito = v_FacOperacionAnulada.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionAnuladaSelec
        End Function

        'para covertir una clase FacOperacionAnuladaSelec a FacOperacionAnulada
        Public Function convertir_FacOperacionAnulada(ByVal v_FacOperacionAnuladaSelec2 As FacOperacionAnuladaSelec) As FacOperacionAnulada
            Dim FacOperacionAnulada2 As New FacOperacionAnulada
            FacOperacionAnulada2.Banco = v_FacOperacionAnuladaSelec2.Banco
            FacOperacionAnulada2.BancoG = v_FacOperacionAnuladaSelec2.BancoG
            FacOperacionAnulada2.Id = v_FacOperacionAnuladaSelec2.Id
            FacOperacionAnulada2.NDeposito = v_FacOperacionAnuladaSelec2.NDeposito
            FacOperacionAnulada2.Deposito = v_FacOperacionAnuladaSelec2.Deposito
            FacOperacionAnulada2.NCheque = v_FacOperacionAnuladaSelec2.NCheque
            FacOperacionAnulada2.Monto = v_FacOperacionAnuladaSelec2.Monto
            FacOperacionAnulada2.Fecha = v_FacOperacionAnuladaSelec2.Fecha
            FacOperacionAnulada2.FechaReg = v_FacOperacionAnuladaSelec2.FechaReg
            FacOperacionAnulada2.FechaDeposito = v_FacOperacionAnuladaSelec2.FechaDeposito

            Return FacOperacionAnulada2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionAnuladaSelec
        Inherits FacOperacionAnulada

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