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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCreditos
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacCreditos
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacCreditos
    Class PresentadorConsultarDepositoFacCreditos
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacCreditos
        Private _FacCreditoServicios As IFacCreditoServicios
        Private _FacCreditos As IList(Of FacCredito)
        Dim FacCreditoselect As IList(Of FacCreditoSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacCreditos)
            Try
                Me._ventana = ventana
                Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacCreditos, Recursos.Ids.fac_ConsultarFacCredito)
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


                Dim FacCreditoAuxiliar As New FacCredito()
                FacCreditoAuxiliar.Deposito = "1"
                'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
                Me._FacCreditos = Me._FacCreditoServicios.ObtenerFacCreditosFiltro(FacCreditoAuxiliar)
                FacCreditoselect = convertir_FacCreditoSelec(Me._FacCreditos)
                Me._ventana.Resultados = FacCreditoselect
                Dim chequevacio As New FacCredito
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacCreditoFiltrar = chequevacio


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

                Dim v_FacCredito As List(Of FacCreditoSelec) = DirectCast(_ventana.Resultados, List(Of FacCreditoSelec))
                Dim v_FacCredito2 As FacCredito = DirectCast(_ventana.FacCreditoFiltrar, FacCredito)
                v_FacCredito2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacCredito2 As List(Of FacCredito) = DirectCast(_ventana.Resultados, List(Of FacCredito))

                For i As Integer = 0 To v_FacCredito.Count - 1
                    If (v_FacCredito(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacCredito(i).Deposito = "2"
                            v_FacCredito(i).FechaDeposito = v_FacCredito2.FechaDeposito
                            v_FacCredito(i).NDeposito = v_FacCredito2.NDeposito
                            v_FacCredito(i).Banco = v_FacCredito2.Banco
                            Dim a As New FacCredito
                            a = convertir_FacCredito(v_FacCredito(i))
                            Dim exitoso As Boolean = _FacCreditoServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacCreditoAuxiliar As New FacCredito()
                FacCreditoAuxiliar.Deposito = "1"
                'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
                Me._FacCreditos = Me._FacCreditoServicios.ObtenerFacCreditosFiltro(FacCreditoAuxiliar)
                FacCreditoselect = convertir_FacCreditoSelec(Me._FacCreditos)
                Me._ventana.Resultados = FacCreditoselect

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
        ''' Método que invoca una nueva página "ConsultarFacCredito" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacCredito()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacCredito(Me._ventana.FacCreditoSeleccionado))
            'Me.Navegar(New ConsultarFacCredito())
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


                'Dim FacCreditoAuxiliar As New FacCredito()
                'FacCreditoAuxiliar.Deposito = "1"

                ''Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
                'Me._FacCreditos = Me._FacCreditoServicios.ObtenerFacCreditosFiltro(FacCreditoAuxiliar)

                'FacCreditoselect = convertir_FacCreditoSelec(Me._FacCreditos)

                For i As Integer = 0 To FacCreditoselect.Count - 1
                    FacCreditoselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacCreditoselect

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


        'para covertir una clase FacCredito a FacCreditoSelec
        Public Function convertir_FacCreditoSelec(ByVal v_FacCredito As IList(Of FacCredito)) As IList(Of FacCreditoSelec)
            Dim FacCreditoSelec As New List(Of FacCreditoSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacCredito.Count
            For i As Integer = 0 To v_FacCredito.Count - 1
                FacCreditoSelec.Add(New FacCreditoSelec)
                FacCreditoSelec.Item(i).Seleccion = False
                FacCreditoSelec.Item(i).Banco = v_FacCredito.Item(i).Banco
                FacCreditoSelec.Item(i).BancoG = v_FacCredito.Item(i).BancoG
                FacCreditoSelec.Item(i).Id = v_FacCredito.Item(i).Id
                FacCreditoSelec.Item(i).NDeposito = v_FacCredito.Item(i).NDeposito
                FacCreditoSelec.Item(i).Deposito = v_FacCredito.Item(i).Deposito
                FacCreditoSelec.Item(i).NCheque = v_FacCredito.Item(i).NCheque
                FacCreditoSelec.Item(i).Monto = v_FacCredito.Item(i).Monto
                monto = v_FacCredito.Item(i).Monto + monto
                FacCreditoSelec.Item(i).Fecha = v_FacCredito.Item(i).Fecha
                FacCreditoSelec.Item(i).FechaReg = v_FacCredito.Item(i).FechaReg
                FacCreditoSelec.Item(i).FechaDeposito = v_FacCredito.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacCreditoSelec
        End Function

        'para covertir una clase FacCreditoSelec a FacCredito
        Public Function convertir_FacCredito(ByVal v_FacCreditoSelec2 As FacCreditoSelec) As FacCredito
            Dim FacCredito2 As New FacCredito
            FacCredito2.Banco = v_FacCreditoSelec2.Banco
            FacCredito2.BancoG = v_FacCreditoSelec2.BancoG
            FacCredito2.Id = v_FacCreditoSelec2.Id
            FacCredito2.NDeposito = v_FacCreditoSelec2.NDeposito
            FacCredito2.Deposito = v_FacCreditoSelec2.Deposito
            FacCredito2.NCheque = v_FacCreditoSelec2.NCheque
            FacCredito2.Monto = v_FacCreditoSelec2.Monto
            FacCredito2.Fecha = v_FacCreditoSelec2.Fecha
            FacCredito2.FechaReg = v_FacCreditoSelec2.FechaReg
            FacCredito2.FechaDeposito = v_FacCreditoSelec2.FechaDeposito

            Return FacCredito2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacCreditoSelec
        Inherits FacCredito

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