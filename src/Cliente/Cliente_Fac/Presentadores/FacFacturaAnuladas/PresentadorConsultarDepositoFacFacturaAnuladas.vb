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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFacturaAnuladas
    Class PresentadorConsultarDepositoFacFacturaAnuladas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacFacturaAnuladas
        Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        Private _FacFacturaAnuladas As IList(Of FacFacturaAnulada)
        Dim FacFacturaAnuladaselect As IList(Of FacFacturaAnuladaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacFacturaAnuladas)
            Try
                Me._ventana = ventana
                Me._FacFacturaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaServicios")), IFacFacturaAnuladaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturaAnuladas, Recursos.Ids.fac_ConsultarFacFacturaAnulada)
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


                Dim FacFacturaAnuladaAuxiliar As New FacFacturaAnulada()
                FacFacturaAnuladaAuxiliar.Deposito = "1"
                'Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ConsultarTodos()
                Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ObtenerFacFacturaAnuladasFiltro(FacFacturaAnuladaAuxiliar)
                FacFacturaAnuladaselect = convertir_FacFacturaAnuladaSelec(Me._FacFacturaAnuladas)
                Me._ventana.Resultados = FacFacturaAnuladaselect
                Dim chequevacio As New FacFacturaAnulada
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacFacturaAnuladaFiltrar = chequevacio


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

                Dim v_FacFacturaAnulada As List(Of FacFacturaAnuladaSelec) = DirectCast(_ventana.Resultados, List(Of FacFacturaAnuladaSelec))
                Dim v_FacFacturaAnulada2 As FacFacturaAnulada = DirectCast(_ventana.FacFacturaAnuladaFiltrar, FacFacturaAnulada)
                v_FacFacturaAnulada2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacFacturaAnulada2 As List(Of FacFacturaAnulada) = DirectCast(_ventana.Resultados, List(Of FacFacturaAnulada))

                For i As Integer = 0 To v_FacFacturaAnulada.Count - 1
                    If (v_FacFacturaAnulada(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacFacturaAnulada(i).Deposito = "2"
                            v_FacFacturaAnulada(i).FechaDeposito = v_FacFacturaAnulada2.FechaDeposito
                            v_FacFacturaAnulada(i).NDeposito = v_FacFacturaAnulada2.NDeposito
                            v_FacFacturaAnulada(i).Banco = v_FacFacturaAnulada2.Banco
                            Dim a As New FacFacturaAnulada
                            a = convertir_FacFacturaAnulada(v_FacFacturaAnulada(i))
                            Dim exitoso As Boolean = _FacFacturaAnuladaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacFacturaAnuladaAuxiliar As New FacFacturaAnulada()
                FacFacturaAnuladaAuxiliar.Deposito = "1"
                'Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ConsultarTodos()
                Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ObtenerFacFacturaAnuladasFiltro(FacFacturaAnuladaAuxiliar)
                FacFacturaAnuladaselect = convertir_FacFacturaAnuladaSelec(Me._FacFacturaAnuladas)
                Me._ventana.Resultados = FacFacturaAnuladaselect

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
        ''' Método que invoca una nueva página "ConsultarFacFacturaAnulada" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFacturaAnulada()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacFacturaAnulada(Me._ventana.FacFacturaAnuladaSeleccionado))
            'Me.Navegar(New ConsultarFacFacturaAnulada())
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


                'Dim FacFacturaAnuladaAuxiliar As New FacFacturaAnulada()
                'FacFacturaAnuladaAuxiliar.Deposito = "1"

                ''Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ConsultarTodos()
                'Me._FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ObtenerFacFacturaAnuladasFiltro(FacFacturaAnuladaAuxiliar)

                'FacFacturaAnuladaselect = convertir_FacFacturaAnuladaSelec(Me._FacFacturaAnuladas)

                For i As Integer = 0 To FacFacturaAnuladaselect.Count - 1
                    FacFacturaAnuladaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacFacturaAnuladaselect

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


        'para covertir una clase FacFacturaAnulada a FacFacturaAnuladaSelec
        Public Function convertir_FacFacturaAnuladaSelec(ByVal v_FacFacturaAnulada As IList(Of FacFacturaAnulada)) As IList(Of FacFacturaAnuladaSelec)
            Dim FacFacturaAnuladaSelec As New List(Of FacFacturaAnuladaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacFacturaAnulada.Count
            For i As Integer = 0 To v_FacFacturaAnulada.Count - 1
                FacFacturaAnuladaSelec.Add(New FacFacturaAnuladaSelec)
                FacFacturaAnuladaSelec.Item(i).Seleccion = False
                FacFacturaAnuladaSelec.Item(i).Banco = v_FacFacturaAnulada.Item(i).Banco
                FacFacturaAnuladaSelec.Item(i).BancoG = v_FacFacturaAnulada.Item(i).BancoG
                FacFacturaAnuladaSelec.Item(i).Id = v_FacFacturaAnulada.Item(i).Id
                FacFacturaAnuladaSelec.Item(i).NDeposito = v_FacFacturaAnulada.Item(i).NDeposito
                FacFacturaAnuladaSelec.Item(i).Deposito = v_FacFacturaAnulada.Item(i).Deposito
                FacFacturaAnuladaSelec.Item(i).NCheque = v_FacFacturaAnulada.Item(i).NCheque
                FacFacturaAnuladaSelec.Item(i).Monto = v_FacFacturaAnulada.Item(i).Monto
                monto = v_FacFacturaAnulada.Item(i).Monto + monto
                FacFacturaAnuladaSelec.Item(i).Fecha = v_FacFacturaAnulada.Item(i).Fecha
                FacFacturaAnuladaSelec.Item(i).FechaReg = v_FacFacturaAnulada.Item(i).FechaReg
                FacFacturaAnuladaSelec.Item(i).FechaDeposito = v_FacFacturaAnulada.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacFacturaAnuladaSelec
        End Function

        'para covertir una clase FacFacturaAnuladaSelec a FacFacturaAnulada
        Public Function convertir_FacFacturaAnulada(ByVal v_FacFacturaAnuladaSelec2 As FacFacturaAnuladaSelec) As FacFacturaAnulada
            Dim FacFacturaAnulada2 As New FacFacturaAnulada
            FacFacturaAnulada2.Banco = v_FacFacturaAnuladaSelec2.Banco
            FacFacturaAnulada2.BancoG = v_FacFacturaAnuladaSelec2.BancoG
            FacFacturaAnulada2.Id = v_FacFacturaAnuladaSelec2.Id
            FacFacturaAnulada2.NDeposito = v_FacFacturaAnuladaSelec2.NDeposito
            FacFacturaAnulada2.Deposito = v_FacFacturaAnuladaSelec2.Deposito
            FacFacturaAnulada2.NCheque = v_FacFacturaAnuladaSelec2.NCheque
            FacFacturaAnulada2.Monto = v_FacFacturaAnuladaSelec2.Monto
            FacFacturaAnulada2.Fecha = v_FacFacturaAnuladaSelec2.Fecha
            FacFacturaAnulada2.FechaReg = v_FacFacturaAnuladaSelec2.FechaReg
            FacFacturaAnulada2.FechaDeposito = v_FacFacturaAnuladaSelec2.FechaDeposito

            Return FacFacturaAnulada2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacFacturaAnuladaSelec
        Inherits FacFacturaAnulada

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