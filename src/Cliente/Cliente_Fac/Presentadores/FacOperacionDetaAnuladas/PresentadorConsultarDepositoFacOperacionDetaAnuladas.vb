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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionDetaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionDetaAnuladas
    Class PresentadorConsultarDepositoFacOperacionDetaAnuladas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionDetaAnuladas
        Private _FacOperacionDetaAnuladaServicios As IFacOperacionDetaAnuladaServicios
        Private _FacOperacionDetaAnuladas As IList(Of FacOperacionDetaAnulada)
        Dim FacOperacionDetaAnuladaselect As IList(Of FacOperacionDetaAnuladaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionDetaAnuladas)
            Try
                Me._ventana = ventana
                Me._FacOperacionDetaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaAnuladaServicios")), IFacOperacionDetaAnuladaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionDetaAnuladas, Recursos.Ids.fac_ConsultarFacOperacionDetaAnulada)
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


                Dim FacOperacionDetaAnuladaAuxiliar As New FacOperacionDetaAnulada()
                FacOperacionDetaAnuladaAuxiliar.Deposito = "1"
                'Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ConsultarTodos()
                Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ObtenerFacOperacionDetaAnuladasFiltro(FacOperacionDetaAnuladaAuxiliar)
                FacOperacionDetaAnuladaselect = convertir_FacOperacionDetaAnuladaSelec(Me._FacOperacionDetaAnuladas)
                Me._ventana.Resultados = FacOperacionDetaAnuladaselect
                Dim chequevacio As New FacOperacionDetaAnulada
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionDetaAnuladaFiltrar = chequevacio


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

                Dim v_FacOperacionDetaAnulada As List(Of FacOperacionDetaAnuladaSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetaAnuladaSelec))
                Dim v_FacOperacionDetaAnulada2 As FacOperacionDetaAnulada = DirectCast(_ventana.FacOperacionDetaAnuladaFiltrar, FacOperacionDetaAnulada)
                v_FacOperacionDetaAnulada2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionDetaAnulada2 As List(Of FacOperacionDetaAnulada) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetaAnulada))

                For i As Integer = 0 To v_FacOperacionDetaAnulada.Count - 1
                    If (v_FacOperacionDetaAnulada(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionDetaAnulada(i).Deposito = "2"
                            v_FacOperacionDetaAnulada(i).FechaDeposito = v_FacOperacionDetaAnulada2.FechaDeposito
                            v_FacOperacionDetaAnulada(i).NDeposito = v_FacOperacionDetaAnulada2.NDeposito
                            v_FacOperacionDetaAnulada(i).Banco = v_FacOperacionDetaAnulada2.Banco
                            Dim a As New FacOperacionDetaAnulada
                            a = convertir_FacOperacionDetaAnulada(v_FacOperacionDetaAnulada(i))
                            Dim exitoso As Boolean = _FacOperacionDetaAnuladaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionDetaAnuladaAuxiliar As New FacOperacionDetaAnulada()
                FacOperacionDetaAnuladaAuxiliar.Deposito = "1"
                'Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ConsultarTodos()
                Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ObtenerFacOperacionDetaAnuladasFiltro(FacOperacionDetaAnuladaAuxiliar)
                FacOperacionDetaAnuladaselect = convertir_FacOperacionDetaAnuladaSelec(Me._FacOperacionDetaAnuladas)
                Me._ventana.Resultados = FacOperacionDetaAnuladaselect

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
        ''' Método que invoca una nueva página "ConsultarFacOperacionDetaAnulada" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionDetaAnulada()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionDetaAnulada(Me._ventana.FacOperacionDetaAnuladaSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionDetaAnulada())
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


                'Dim FacOperacionDetaAnuladaAuxiliar As New FacOperacionDetaAnulada()
                'FacOperacionDetaAnuladaAuxiliar.Deposito = "1"

                ''Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ConsultarTodos()
                'Me._FacOperacionDetaAnuladas = Me._FacOperacionDetaAnuladaServicios.ObtenerFacOperacionDetaAnuladasFiltro(FacOperacionDetaAnuladaAuxiliar)

                'FacOperacionDetaAnuladaselect = convertir_FacOperacionDetaAnuladaSelec(Me._FacOperacionDetaAnuladas)

                For i As Integer = 0 To FacOperacionDetaAnuladaselect.Count - 1
                    FacOperacionDetaAnuladaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionDetaAnuladaselect

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


        'para covertir una clase FacOperacionDetaAnulada a FacOperacionDetaAnuladaSelec
        Public Function convertir_FacOperacionDetaAnuladaSelec(ByVal v_FacOperacionDetaAnulada As IList(Of FacOperacionDetaAnulada)) As IList(Of FacOperacionDetaAnuladaSelec)
            Dim FacOperacionDetaAnuladaSelec As New List(Of FacOperacionDetaAnuladaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionDetaAnulada.Count
            For i As Integer = 0 To v_FacOperacionDetaAnulada.Count - 1
                FacOperacionDetaAnuladaSelec.Add(New FacOperacionDetaAnuladaSelec)
                FacOperacionDetaAnuladaSelec.Item(i).Seleccion = False
                FacOperacionDetaAnuladaSelec.Item(i).Banco = v_FacOperacionDetaAnulada.Item(i).Banco
                FacOperacionDetaAnuladaSelec.Item(i).BancoG = v_FacOperacionDetaAnulada.Item(i).BancoG
                FacOperacionDetaAnuladaSelec.Item(i).Id = v_FacOperacionDetaAnulada.Item(i).Id
                FacOperacionDetaAnuladaSelec.Item(i).NDeposito = v_FacOperacionDetaAnulada.Item(i).NDeposito
                FacOperacionDetaAnuladaSelec.Item(i).Deposito = v_FacOperacionDetaAnulada.Item(i).Deposito
                FacOperacionDetaAnuladaSelec.Item(i).NCheque = v_FacOperacionDetaAnulada.Item(i).NCheque
                FacOperacionDetaAnuladaSelec.Item(i).Monto = v_FacOperacionDetaAnulada.Item(i).Monto
                monto = v_FacOperacionDetaAnulada.Item(i).Monto + monto
                FacOperacionDetaAnuladaSelec.Item(i).Fecha = v_FacOperacionDetaAnulada.Item(i).Fecha
                FacOperacionDetaAnuladaSelec.Item(i).FechaReg = v_FacOperacionDetaAnulada.Item(i).FechaReg
                FacOperacionDetaAnuladaSelec.Item(i).FechaDeposito = v_FacOperacionDetaAnulada.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionDetaAnuladaSelec
        End Function

        'para covertir una clase FacOperacionDetaAnuladaSelec a FacOperacionDetaAnulada
        Public Function convertir_FacOperacionDetaAnulada(ByVal v_FacOperacionDetaAnuladaSelec2 As FacOperacionDetaAnuladaSelec) As FacOperacionDetaAnulada
            Dim FacOperacionDetaAnulada2 As New FacOperacionDetaAnulada
            FacOperacionDetaAnulada2.Banco = v_FacOperacionDetaAnuladaSelec2.Banco
            FacOperacionDetaAnulada2.BancoG = v_FacOperacionDetaAnuladaSelec2.BancoG
            FacOperacionDetaAnulada2.Id = v_FacOperacionDetaAnuladaSelec2.Id
            FacOperacionDetaAnulada2.NDeposito = v_FacOperacionDetaAnuladaSelec2.NDeposito
            FacOperacionDetaAnulada2.Deposito = v_FacOperacionDetaAnuladaSelec2.Deposito
            FacOperacionDetaAnulada2.NCheque = v_FacOperacionDetaAnuladaSelec2.NCheque
            FacOperacionDetaAnulada2.Monto = v_FacOperacionDetaAnuladaSelec2.Monto
            FacOperacionDetaAnulada2.Fecha = v_FacOperacionDetaAnuladaSelec2.Fecha
            FacOperacionDetaAnulada2.FechaReg = v_FacOperacionDetaAnuladaSelec2.FechaReg
            FacOperacionDetaAnulada2.FechaDeposito = v_FacOperacionDetaAnuladaSelec2.FechaDeposito

            Return FacOperacionDetaAnulada2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionDetaAnuladaSelec
        Inherits FacOperacionDetaAnulada

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