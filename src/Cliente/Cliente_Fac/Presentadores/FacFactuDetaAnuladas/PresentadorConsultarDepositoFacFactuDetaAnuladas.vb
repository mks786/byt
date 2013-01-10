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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFactuDetaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFactuDetaAnuladas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFactuDetaAnuladas
    Class PresentadorConsultarDepositoFacFactuDetaAnuladas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacFactuDetaAnuladas
        Private _FacFactuDetaAnuladaServicios As IFacFactuDetaAnuladaServicios
        Private _FacFactuDetaAnuladas As IList(Of FacFactuDetaAnulada)
        Dim FacFactuDetaAnuladaselect As IList(Of FacFactuDetaAnuladaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacFactuDetaAnuladas)
            Try
                Me._ventana = ventana
                Me._FacFactuDetaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaAnuladaServicios")), IFacFactuDetaAnuladaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFactuDetaAnuladas, Recursos.Ids.fac_ConsultarFacFactuDetaAnulada)
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


                Dim FacFactuDetaAnuladaAuxiliar As New FacFactuDetaAnulada()
                'FacFactuDetaAnuladaAuxiliar.Deposito = "1"
                'Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ConsultarTodos()
                Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ObtenerFacFactuDetaAnuladasFiltro(FacFactuDetaAnuladaAuxiliar)
                FacFactuDetaAnuladaselect = convertir_FacFactuDetaAnuladaSelec(Me._FacFactuDetaAnuladas)
                Me._ventana.Resultados = FacFactuDetaAnuladaselect
                Dim chequevacio As New FacFactuDetaAnulada
                'chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacFactuDetaAnuladaFiltrar = chequevacio


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

                'Dim v_FacFactuDetaAnulada As List(Of FacFactuDetaAnuladaSelec) = DirectCast(_ventana.Resultados, List(Of FacFactuDetaAnuladaSelec))
                'Dim v_FacFactuDetaAnulada2 As FacFactuDetaAnulada = DirectCast(_ventana.FacFactuDetaAnuladaFiltrar, FacFactuDetaAnulada)
                'v_FacFactuDetaAnulada2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                ''Dim v_FacFactuDetaAnulada2 As List(Of FacFactuDetaAnulada) = DirectCast(_ventana.Resultados, List(Of FacFactuDetaAnulada))

                'For i As Integer = 0 To v_FacFactuDetaAnulada.Count - 1
                '    If (v_FacFactuDetaAnulada(i).Seleccion = True) Then
                '        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                '            v_FacFactuDetaAnulada(i).Deposito = "2"
                '            v_FacFactuDetaAnulada(i).FechaDeposito = v_FacFactuDetaAnulada2.FechaDeposito
                '            v_FacFactuDetaAnulada(i).NDeposito = v_FacFactuDetaAnulada2.NDeposito
                '            v_FacFactuDetaAnulada(i).Banco = v_FacFactuDetaAnulada2.Banco
                '            Dim a As New FacFactuDetaAnulada
                '            a = convertir_FacFactuDetaAnulada(v_FacFactuDetaAnulada(i))
                '            Dim exitoso As Boolean = _FacFactuDetaAnuladaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                '        End If
                '    End If
                'Next

                'Dim FacFactuDetaAnuladaAuxiliar As New FacFactuDetaAnulada()
                'FacFactuDetaAnuladaAuxiliar.Deposito = "1"
                ''Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ConsultarTodos()
                'Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ObtenerFacFactuDetaAnuladasFiltro(FacFactuDetaAnuladaAuxiliar)
                'FacFactuDetaAnuladaselect = convertir_FacFactuDetaAnuladaSelec(Me._FacFactuDetaAnuladas)
                'Me._ventana.Resultados = FacFactuDetaAnuladaselect

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
        ''' Método que invoca una nueva página "ConsultarFacFactuDetaAnulada" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFactuDetaAnulada()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacFactuDetaAnulada(Me._ventana.FacFactuDetaAnuladaSeleccionado))
            'Me.Navegar(New ConsultarFacFactuDetaAnulada())
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


                'Dim FacFactuDetaAnuladaAuxiliar As New FacFactuDetaAnulada()
                'FacFactuDetaAnuladaAuxiliar.Deposito = "1"

                ''Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ConsultarTodos()
                'Me._FacFactuDetaAnuladas = Me._FacFactuDetaAnuladaServicios.ObtenerFacFactuDetaAnuladasFiltro(FacFactuDetaAnuladaAuxiliar)

                'FacFactuDetaAnuladaselect = convertir_FacFactuDetaAnuladaSelec(Me._FacFactuDetaAnuladas)

                For i As Integer = 0 To FacFactuDetaAnuladaselect.Count - 1
                    FacFactuDetaAnuladaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacFactuDetaAnuladaselect

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


        'para covertir una clase FacFactuDetaAnulada a FacFactuDetaAnuladaSelec
        Public Function convertir_FacFactuDetaAnuladaSelec(ByVal v_FacFactuDetaAnulada As IList(Of FacFactuDetaAnulada)) As IList(Of FacFactuDetaAnuladaSelec)
            Dim FacFactuDetaAnuladaSelec As New List(Of FacFactuDetaAnuladaSelec)
            Dim monto As Double = 0
            'Me._ventana.NReg = v_FacFactuDetaAnulada.Count
            'For i As Integer = 0 To v_FacFactuDetaAnulada.Count - 1
            '    FacFactuDetaAnuladaSelec.Add(New FacFactuDetaAnuladaSelec)
            '    FacFactuDetaAnuladaSelec.Item(i).Seleccion = False
            '    FacFactuDetaAnuladaSelec.Item(i).Banco = v_FacFactuDetaAnulada.Item(i).Banco
            '    FacFactuDetaAnuladaSelec.Item(i).BancoG = v_FacFactuDetaAnulada.Item(i).BancoG
            '    FacFactuDetaAnuladaSelec.Item(i).Id = v_FacFactuDetaAnulada.Item(i).Id
            '    FacFactuDetaAnuladaSelec.Item(i).NDeposito = v_FacFactuDetaAnulada.Item(i).NDeposito
            '    FacFactuDetaAnuladaSelec.Item(i).Deposito = v_FacFactuDetaAnulada.Item(i).Deposito
            '    FacFactuDetaAnuladaSelec.Item(i).NCheque = v_FacFactuDetaAnulada.Item(i).NCheque
            '    FacFactuDetaAnuladaSelec.Item(i).Monto = v_FacFactuDetaAnulada.Item(i).Monto
            '    monto = v_FacFactuDetaAnulada.Item(i).Monto + monto
            '    FacFactuDetaAnuladaSelec.Item(i).Fecha = v_FacFactuDetaAnulada.Item(i).Fecha
            '    FacFactuDetaAnuladaSelec.Item(i).FechaReg = v_FacFactuDetaAnulada.Item(i).FechaReg
            '    FacFactuDetaAnuladaSelec.Item(i).FechaDeposito = v_FacFactuDetaAnulada.Item(i).FechaDeposito
            'Next
            'Me._ventana.Tmonto = monto
            Return FacFactuDetaAnuladaSelec
        End Function

        'para covertir una clase FacFactuDetaAnuladaSelec a FacFactuDetaAnulada
        Public Function convertir_FacFactuDetaAnulada(ByVal v_FacFactuDetaAnuladaSelec2 As FacFactuDetaAnuladaSelec) As FacFactuDetaAnulada
            Dim FacFactuDetaAnulada2 As New FacFactuDetaAnulada
            'FacFactuDetaAnulada2.Banco = v_FacFactuDetaAnuladaSelec2.Banco
            'FacFactuDetaAnulada2.BancoG = v_FacFactuDetaAnuladaSelec2.BancoG
            'FacFactuDetaAnulada2.Id = v_FacFactuDetaAnuladaSelec2.Id
            'FacFactuDetaAnulada2.NDeposito = v_FacFactuDetaAnuladaSelec2.NDeposito
            'FacFactuDetaAnulada2.Deposito = v_FacFactuDetaAnuladaSelec2.Deposito
            'FacFactuDetaAnulada2.NCheque = v_FacFactuDetaAnuladaSelec2.NCheque
            'FacFactuDetaAnulada2.Monto = v_FacFactuDetaAnuladaSelec2.Monto
            'FacFactuDetaAnulada2.Fecha = v_FacFactuDetaAnuladaSelec2.Fecha
            'FacFactuDetaAnulada2.FechaReg = v_FacFactuDetaAnuladaSelec2.FechaReg
            'FacFactuDetaAnulada2.FechaDeposito = v_FacFactuDetaAnuladaSelec2.FechaDeposito

            Return FacFactuDetaAnulada2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacFactuDetaAnuladaSelec
        Inherits FacFactuDetaAnulada

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