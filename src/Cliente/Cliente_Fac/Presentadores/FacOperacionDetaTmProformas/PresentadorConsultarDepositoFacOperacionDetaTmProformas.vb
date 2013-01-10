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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionDetaTmProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionDetaTmProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionDetaTmProformas
    Class PresentadorConsultarDepositoFacOperacionDetaTmProformas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionDetaTmProformas
        Private _FacOperacionDetaTmProformaServicios As IFacOperacionDetaTmProformaServicios
        Private _FacOperacionDetaTmProformas As IList(Of FacOperacionDetaTmProforma)
        Dim FacOperacionDetaTmProformaselect As IList(Of FacOperacionDetaTmProformaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionDetaTmProformas)
            Try
                Me._ventana = ventana
                Me._FacOperacionDetaTmProformaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaTmProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaTmProformaServicios")), IFacOperacionDetaTmProformaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionDetaTmProformas, Recursos.Ids.fac_ConsultarFacOperacionDetaTmProforma)
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


                Dim FacOperacionDetaTmProformaAuxiliar As New FacOperacionDetaTmProforma()
                FacOperacionDetaTmProformaAuxiliar.Deposito = "1"
                'Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ConsultarTodos()
                Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ObtenerFacOperacionDetaTmProformasFiltro(FacOperacionDetaTmProformaAuxiliar)
                FacOperacionDetaTmProformaselect = convertir_FacOperacionDetaTmProformaSelec(Me._FacOperacionDetaTmProformas)
                Me._ventana.Resultados = FacOperacionDetaTmProformaselect
                Dim chequevacio As New FacOperacionDetaTmProforma
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionDetaTmProformaFiltrar = chequevacio


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

                Dim v_FacOperacionDetaTmProforma As List(Of FacOperacionDetaTmProformaSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetaTmProformaSelec))
                Dim v_FacOperacionDetaTmProforma2 As FacOperacionDetaTmProforma = DirectCast(_ventana.FacOperacionDetaTmProformaFiltrar, FacOperacionDetaTmProforma)
                v_FacOperacionDetaTmProforma2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionDetaTmProforma2 As List(Of FacOperacionDetaTmProforma) = DirectCast(_ventana.Resultados, List(Of FacOperacionDetaTmProforma))

                For i As Integer = 0 To v_FacOperacionDetaTmProforma.Count - 1
                    If (v_FacOperacionDetaTmProforma(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionDetaTmProforma(i).Deposito = "2"
                            v_FacOperacionDetaTmProforma(i).FechaDeposito = v_FacOperacionDetaTmProforma2.FechaDeposito
                            v_FacOperacionDetaTmProforma(i).NDeposito = v_FacOperacionDetaTmProforma2.NDeposito
                            v_FacOperacionDetaTmProforma(i).Banco = v_FacOperacionDetaTmProforma2.Banco
                            Dim a As New FacOperacionDetaTmProforma
                            a = convertir_FacOperacionDetaTmProforma(v_FacOperacionDetaTmProforma(i))
                            Dim exitoso As Boolean = _FacOperacionDetaTmProformaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionDetaTmProformaAuxiliar As New FacOperacionDetaTmProforma()
                FacOperacionDetaTmProformaAuxiliar.Deposito = "1"
                'Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ConsultarTodos()
                Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ObtenerFacOperacionDetaTmProformasFiltro(FacOperacionDetaTmProformaAuxiliar)
                FacOperacionDetaTmProformaselect = convertir_FacOperacionDetaTmProformaSelec(Me._FacOperacionDetaTmProformas)
                Me._ventana.Resultados = FacOperacionDetaTmProformaselect

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
        ''' Método que invoca una nueva página "ConsultarFacOperacionDetaTmProforma" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionDetaTmProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionDetaTmProforma(Me._ventana.FacOperacionDetaTmProformaSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionDetaTmProforma())
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


                'Dim FacOperacionDetaTmProformaAuxiliar As New FacOperacionDetaTmProforma()
                'FacOperacionDetaTmProformaAuxiliar.Deposito = "1"

                ''Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ConsultarTodos()
                'Me._FacOperacionDetaTmProformas = Me._FacOperacionDetaTmProformaServicios.ObtenerFacOperacionDetaTmProformasFiltro(FacOperacionDetaTmProformaAuxiliar)

                'FacOperacionDetaTmProformaselect = convertir_FacOperacionDetaTmProformaSelec(Me._FacOperacionDetaTmProformas)

                For i As Integer = 0 To FacOperacionDetaTmProformaselect.Count - 1
                    FacOperacionDetaTmProformaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionDetaTmProformaselect

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


        'para covertir una clase FacOperacionDetaTmProforma a FacOperacionDetaTmProformaSelec
        Public Function convertir_FacOperacionDetaTmProformaSelec(ByVal v_FacOperacionDetaTmProforma As IList(Of FacOperacionDetaTmProforma)) As IList(Of FacOperacionDetaTmProformaSelec)
            Dim FacOperacionDetaTmProformaSelec As New List(Of FacOperacionDetaTmProformaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionDetaTmProforma.Count
            For i As Integer = 0 To v_FacOperacionDetaTmProforma.Count - 1
                FacOperacionDetaTmProformaSelec.Add(New FacOperacionDetaTmProformaSelec)
                FacOperacionDetaTmProformaSelec.Item(i).Seleccion = False
                FacOperacionDetaTmProformaSelec.Item(i).Banco = v_FacOperacionDetaTmProforma.Item(i).Banco
                FacOperacionDetaTmProformaSelec.Item(i).BancoG = v_FacOperacionDetaTmProforma.Item(i).BancoG
                FacOperacionDetaTmProformaSelec.Item(i).Id = v_FacOperacionDetaTmProforma.Item(i).Id
                FacOperacionDetaTmProformaSelec.Item(i).NDeposito = v_FacOperacionDetaTmProforma.Item(i).NDeposito
                FacOperacionDetaTmProformaSelec.Item(i).Deposito = v_FacOperacionDetaTmProforma.Item(i).Deposito
                FacOperacionDetaTmProformaSelec.Item(i).NCheque = v_FacOperacionDetaTmProforma.Item(i).NCheque
                FacOperacionDetaTmProformaSelec.Item(i).Monto = v_FacOperacionDetaTmProforma.Item(i).Monto
                monto = v_FacOperacionDetaTmProforma.Item(i).Monto + monto
                FacOperacionDetaTmProformaSelec.Item(i).Fecha = v_FacOperacionDetaTmProforma.Item(i).Fecha
                FacOperacionDetaTmProformaSelec.Item(i).FechaReg = v_FacOperacionDetaTmProforma.Item(i).FechaReg
                FacOperacionDetaTmProformaSelec.Item(i).FechaDeposito = v_FacOperacionDetaTmProforma.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionDetaTmProformaSelec
        End Function

        'para covertir una clase FacOperacionDetaTmProformaSelec a FacOperacionDetaTmProforma
        Public Function convertir_FacOperacionDetaTmProforma(ByVal v_FacOperacionDetaTmProformaSelec2 As FacOperacionDetaTmProformaSelec) As FacOperacionDetaTmProforma
            Dim FacOperacionDetaTmProforma2 As New FacOperacionDetaTmProforma
            FacOperacionDetaTmProforma2.Banco = v_FacOperacionDetaTmProformaSelec2.Banco
            FacOperacionDetaTmProforma2.BancoG = v_FacOperacionDetaTmProformaSelec2.BancoG
            FacOperacionDetaTmProforma2.Id = v_FacOperacionDetaTmProformaSelec2.Id
            FacOperacionDetaTmProforma2.NDeposito = v_FacOperacionDetaTmProformaSelec2.NDeposito
            FacOperacionDetaTmProforma2.Deposito = v_FacOperacionDetaTmProformaSelec2.Deposito
            FacOperacionDetaTmProforma2.NCheque = v_FacOperacionDetaTmProformaSelec2.NCheque
            FacOperacionDetaTmProforma2.Monto = v_FacOperacionDetaTmProformaSelec2.Monto
            FacOperacionDetaTmProforma2.Fecha = v_FacOperacionDetaTmProformaSelec2.Fecha
            FacOperacionDetaTmProforma2.FechaReg = v_FacOperacionDetaTmProformaSelec2.FechaReg
            FacOperacionDetaTmProforma2.FechaDeposito = v_FacOperacionDetaTmProformaSelec2.FechaDeposito

            Return FacOperacionDetaTmProforma2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionDetaTmProformaSelec
        Inherits FacOperacionDetaTmProforma

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