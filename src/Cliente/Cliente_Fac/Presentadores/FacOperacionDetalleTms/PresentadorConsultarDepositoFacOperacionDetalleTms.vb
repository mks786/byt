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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetalleTms
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionDetalleTms
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionDetalleTms
    Class PresentadorConsultarDepositoFacOperacionDetalleTms
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionDetalleTms
        Private _FacOperacionDetalleTmServicios As IFacOperacionDetalleTmServicios
        Private _FacOperacionDetalleTms As IList(Of FacOperacionDetalleTm)
        Dim FacOperacionDetalleTmselect As IList(Of FacOperacionDetalleTmSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionDetalleTms)
            Try
                Me._ventana = ventana
                Me._FacOperacionDetalleTmServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleTmServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleTmServicios")), IFacOperacionDetalleTmServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionDetalleTms, Recursos.Ids.fac_ConsultarFacOperacionDetalleTm)
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


                Dim FacOperacionDetalleTmAuxiliar As New FacOperacionDetalleTm()
                FacOperacionDetalleTmAuxiliar.Deposito = "1"
                'Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ConsultarTodos()
                Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmAuxiliar)
                FacOperacionDetalleTmselect = convertir_FacOperacionDetalleTmSelec(Me._FacOperacionDetalleTms)
                Me._ventana.Resultados = FacOperacionDetalleTmselect
                Dim chequevacio As New FacOperacionDetalleTm
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionDetalleTmFiltrar = chequevacio


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

                Dim v_FacOperacionDetalleTm As List(Of FacOperacionDetalleTmSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetalleTmSelec))
                Dim v_FacOperacionDetalleTm2 As FacOperacionDetalleTm = DirectCast(_ventana.FacOperacionDetalleTmFiltrar, FacOperacionDetalleTm)
                v_FacOperacionDetalleTm2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionDetalleTm2 As List(Of FacOperacionDetalleTm) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetalleTm))

                For i As Integer = 0 To v_FacOperacionDetalleTm.Count - 1
                    If (v_FacOperacionDetalleTm(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionDetalleTm(i).Deposito = "2"
                            v_FacOperacionDetalleTm(i).FechaDeposito = v_FacOperacionDetalleTm2.FechaDeposito
                            v_FacOperacionDetalleTm(i).NDeposito = v_FacOperacionDetalleTm2.NDeposito
                            v_FacOperacionDetalleTm(i).Banco = v_FacOperacionDetalleTm2.Banco
                            Dim a As New FacOperacionDetalleTm
                            a = convertir_FacOperacionDetalleTm(v_FacOperacionDetalleTm(i))
                            Dim exitoso As Boolean = _FacOperacionDetalleTmServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionDetalleTmAuxiliar As New FacOperacionDetalleTm()
                FacOperacionDetalleTmAuxiliar.Deposito = "1"
                'Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ConsultarTodos()
                Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmAuxiliar)
                FacOperacionDetalleTmselect = convertir_FacOperacionDetalleTmSelec(Me._FacOperacionDetalleTms)
                Me._ventana.Resultados = FacOperacionDetalleTmselect

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
        ''' Método que invoca una nueva página "ConsultarFacOperacionDetalleTm" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionDetalleTm()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionDetalleTm(Me._ventana.FacOperacionDetalleTmSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionDetalleTm())
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


                'Dim FacOperacionDetalleTmAuxiliar As New FacOperacionDetalleTm()
                'FacOperacionDetalleTmAuxiliar.Deposito = "1"

                ''Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ConsultarTodos()
                'Me._FacOperacionDetalleTms = Me._FacOperacionDetalleTmServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmAuxiliar)

                'FacOperacionDetalleTmselect = convertir_FacOperacionDetalleTmSelec(Me._FacOperacionDetalleTms)

                For i As Integer = 0 To FacOperacionDetalleTmselect.Count - 1
                    FacOperacionDetalleTmselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionDetalleTmselect

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


        'para covertir una clase FacOperacionDetalleTm a FacOperacionDetalleTmSelec
        Public Function convertir_FacOperacionDetalleTmSelec(ByVal v_FacOperacionDetalleTm As IList(Of FacOperacionDetalleTm)) As IList(Of FacOperacionDetalleTmSelec)
            Dim FacOperacionDetalleTmSelec As New List(Of FacOperacionDetalleTmSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionDetalleTm.Count
            For i As Integer = 0 To v_FacOperacionDetalleTm.Count - 1
                FacOperacionDetalleTmSelec.Add(New FacOperacionDetalleTmSelec)
                FacOperacionDetalleTmSelec.Item(i).Seleccion = False
                FacOperacionDetalleTmSelec.Item(i).Banco = v_FacOperacionDetalleTm.Item(i).Banco
                FacOperacionDetalleTmSelec.Item(i).BancoG = v_FacOperacionDetalleTm.Item(i).BancoG
                FacOperacionDetalleTmSelec.Item(i).Id = v_FacOperacionDetalleTm.Item(i).Id
                FacOperacionDetalleTmSelec.Item(i).NDeposito = v_FacOperacionDetalleTm.Item(i).NDeposito
                FacOperacionDetalleTmSelec.Item(i).Deposito = v_FacOperacionDetalleTm.Item(i).Deposito
                FacOperacionDetalleTmSelec.Item(i).NCheque = v_FacOperacionDetalleTm.Item(i).NCheque
                FacOperacionDetalleTmSelec.Item(i).Monto = v_FacOperacionDetalleTm.Item(i).Monto
                monto = v_FacOperacionDetalleTm.Item(i).Monto + monto
                FacOperacionDetalleTmSelec.Item(i).Fecha = v_FacOperacionDetalleTm.Item(i).Fecha
                FacOperacionDetalleTmSelec.Item(i).FechaReg = v_FacOperacionDetalleTm.Item(i).FechaReg
                FacOperacionDetalleTmSelec.Item(i).FechaDeposito = v_FacOperacionDetalleTm.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionDetalleTmSelec
        End Function

        'para covertir una clase FacOperacionDetalleTmSelec a FacOperacionDetalleTm
        Public Function convertir_FacOperacionDetalleTm(ByVal v_FacOperacionDetalleTmSelec2 As FacOperacionDetalleTmSelec) As FacOperacionDetalleTm
            Dim FacOperacionDetalleTm2 As New FacOperacionDetalleTm
            FacOperacionDetalleTm2.Banco = v_FacOperacionDetalleTmSelec2.Banco
            FacOperacionDetalleTm2.BancoG = v_FacOperacionDetalleTmSelec2.BancoG
            FacOperacionDetalleTm2.Id = v_FacOperacionDetalleTmSelec2.Id
            FacOperacionDetalleTm2.NDeposito = v_FacOperacionDetalleTmSelec2.NDeposito
            FacOperacionDetalleTm2.Deposito = v_FacOperacionDetalleTmSelec2.Deposito
            FacOperacionDetalleTm2.NCheque = v_FacOperacionDetalleTmSelec2.NCheque
            FacOperacionDetalleTm2.Monto = v_FacOperacionDetalleTmSelec2.Monto
            FacOperacionDetalleTm2.Fecha = v_FacOperacionDetalleTmSelec2.Fecha
            FacOperacionDetalleTm2.FechaReg = v_FacOperacionDetalleTmSelec2.FechaReg
            FacOperacionDetalleTm2.FechaDeposito = v_FacOperacionDetalleTmSelec2.FechaDeposito

            Return FacOperacionDetalleTm2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionDetalleTmSelec
        Inherits FacOperacionDetalleTm

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