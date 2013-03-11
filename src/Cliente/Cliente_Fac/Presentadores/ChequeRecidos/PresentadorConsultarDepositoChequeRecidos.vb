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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ChequeRecidos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.ChequeRecidos
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.ChequeRecidos
    Class PresentadorConsultarDepositoChequeRecidos
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoChequeRecidos
        Private _ChequeRecidoServicios As IChequeRecidoServicios
        Private _ChequeRecidos As IList(Of ChequeRecido)
        Dim ChequeRecidoselect As IList(Of ChequeRecidoSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios        
        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoChequeRecidos)
            Try
                Me._ventana = ventana
                Me._ChequeRecidoServicios = DirectCast(Activator.GetObject(GetType(IChequeRecidoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ChequeRecidoServicios")), IChequeRecidoServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)                
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarDepositoChequeRecido, Recursos.Ids.fac_ConsultarDepositoChequeRecidos)
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

                Dim ChequeRecidoAuxiliar As New ChequeRecido()                
                ChequeRecidoAuxiliar.Deposito = "1"
                'Me._ChequeRecidos = Me._ChequeRecidoServicios.ConsultarTodos()
                Me._ChequeRecidos = Me._ChequeRecidoServicios.ObtenerChequeRecidosFiltro(ChequeRecidoAuxiliar)
                ChequeRecidoselect = convertir_ChequeRecidoSelec(Me._ChequeRecidos)

                Me._ventana.Count = ChequeRecidoselect.Count
                Me._ventana.Resultados = ChequeRecidoselect
                Dim chequevacio As New ChequeRecido
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.ChequeRecidoFiltrar = chequevacio


                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._ventana.Asociados = asociados

                '                Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)()
                Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)
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

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarDepositoChequeRecidos())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
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

                Dim v_ChequeRecido As List(Of ChequeRecidoSelec) = DirectCast(_ventana.Resultados, List(Of ChequeRecidoSelec))
                Dim v_ChequeRecido2 As ChequeRecido = DirectCast(_ventana.ChequeRecidoFiltrar, ChequeRecido)
                v_ChequeRecido2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_ChequeRecido2 As List(Of ChequeRecido) = DirectCast(_ventana.Resultados, List(Of ChequeRecido))

                For i As Integer = 0 To v_ChequeRecido.Count - 1
                    If (v_ChequeRecido(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_ChequeRecido(i).Deposito = "2"
                            v_ChequeRecido(i).FechaDeposito = v_ChequeRecido2.FechaDeposito
                            v_ChequeRecido(i).NDeposito = v_ChequeRecido2.NDeposito
                            v_ChequeRecido(i).Banco = v_ChequeRecido2.Banco
                            Dim a As New ChequeRecido
                            a = convertir_ChequeRecido(v_ChequeRecido(i))
                            Dim exitoso As Boolean = _ChequeRecidoServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim ChequeRecidoAuxiliar As New ChequeRecido()
                ChequeRecidoAuxiliar.Deposito = "1"
                'Me._ChequeRecidos = Me._ChequeRecidoServicios.ConsultarTodos()
                Me._ChequeRecidos = Me._ChequeRecidoServicios.ObtenerChequeRecidosFiltro(ChequeRecidoAuxiliar)
                ChequeRecidoselect = convertir_ChequeRecidoSelec(Me._ChequeRecidos)
                Me._ventana.Count = ChequeRecidoselect.Count
                If ChequeRecidoselect.Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If
                Me._ventana.Resultados = ChequeRecidoselect

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
        ''' Método que invoca una nueva página "ConsultarChequeRecido" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarChequeRecido()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarChequeRecido(Me._ventana.ChequeRecidoSeleccionado))
            'Me.Navegar(New ConsultarChequeRecido())
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
            If Me._ventana.CurAdorner IsNot Nothing Then
                If Me._ventana.CurSortCol.Equals(column) AndAlso Me._ventana.CurAdorner.Direction = newDir Then
                    newDir = ListSortDirection.Descending
                End If
            End If

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


                'Dim ChequeRecidoAuxiliar As New ChequeRecido()
                'ChequeRecidoAuxiliar.Deposito = "1"

                ''Me._ChequeRecidos = Me._ChequeRecidoServicios.ConsultarTodos()
                'Me._ChequeRecidos = Me._ChequeRecidoServicios.ObtenerChequeRecidosFiltro(ChequeRecidoAuxiliar)

                'ChequeRecidoselect = convertir_ChequeRecidoSelec(Me._ChequeRecidos)

                For i As Integer = 0 To ChequeRecidoselect.Count - 1
                    ChequeRecidoselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = ChequeRecidoselect

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


        'para covertir una clase ChequeRecido a ChequeRecidoSelec
        Public Function convertir_ChequeRecidoSelec(ByVal v_ChequeRecido As IList(Of ChequeRecido)) As IList(Of ChequeRecidoSelec)
            Dim ChequeRecidoSelec As New List(Of ChequeRecidoSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_ChequeRecido.Count
            For i As Integer = 0 To v_ChequeRecido.Count - 1
                ChequeRecidoSelec.Add(New ChequeRecidoSelec)
                ChequeRecidoSelec.Item(i).Seleccion = False
                ChequeRecidoSelec.Item(i).Banco = v_ChequeRecido.Item(i).Banco
                ChequeRecidoSelec.Item(i).BancoG = v_ChequeRecido.Item(i).BancoG
                ChequeRecidoSelec.Item(i).Id = v_ChequeRecido.Item(i).Id
                ChequeRecidoSelec.Item(i).NDeposito = v_ChequeRecido.Item(i).NDeposito
                ChequeRecidoSelec.Item(i).Deposito = v_ChequeRecido.Item(i).Deposito
                ChequeRecidoSelec.Item(i).NCheque = v_ChequeRecido.Item(i).NCheque
                ChequeRecidoSelec.Item(i).Monto = v_ChequeRecido.Item(i).Monto
                monto = v_ChequeRecido.Item(i).Monto + monto
                ChequeRecidoSelec.Item(i).Fecha = v_ChequeRecido.Item(i).Fecha
                ChequeRecidoSelec.Item(i).FechaReg = v_ChequeRecido.Item(i).FechaReg
                ChequeRecidoSelec.Item(i).FechaDeposito = v_ChequeRecido.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return ChequeRecidoSelec
        End Function

        'para covertir una clase ChequeRecidoSelec a ChequeRecido
        Public Function convertir_ChequeRecido(ByVal v_ChequeRecidoSelec2 As ChequeRecidoSelec) As ChequeRecido
            Dim ChequeRecido2 As New ChequeRecido
            ChequeRecido2.Banco = v_ChequeRecidoSelec2.Banco
            ChequeRecido2.BancoG = v_ChequeRecidoSelec2.BancoG
            ChequeRecido2.Id = v_ChequeRecidoSelec2.Id
            ChequeRecido2.NDeposito = v_ChequeRecidoSelec2.NDeposito
            ChequeRecido2.Deposito = v_ChequeRecidoSelec2.Deposito
            ChequeRecido2.NCheque = v_ChequeRecidoSelec2.NCheque
            ChequeRecido2.Monto = v_ChequeRecidoSelec2.Monto
            ChequeRecido2.Fecha = v_ChequeRecidoSelec2.Fecha
            ChequeRecido2.FechaReg = v_ChequeRecidoSelec2.FechaReg
            ChequeRecido2.FechaDeposito = v_ChequeRecidoSelec2.FechaDeposito

            Return ChequeRecido2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class ChequeRecidoSelec
        Inherits ChequeRecido

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