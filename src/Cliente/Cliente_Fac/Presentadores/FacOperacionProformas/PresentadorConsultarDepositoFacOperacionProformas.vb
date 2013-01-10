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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacOperacionProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacOperacionProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacOperacionProformas
    Class PresentadorConsultarDepositoFacOperacionProformas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacOperacionProformas
        Private _FacOperacionProformaServicios As IFacOperacionProformaServicios
        Private _FacOperacionProformas As IList(Of FacOperacionProforma)
        Dim FacOperacionProformaselect As IList(Of FacOperacionProformaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacOperacionProformas)
            Try
                Me._ventana = ventana
                Me._FacOperacionProformaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacOperacionProformas, Recursos.Ids.fac_ConsultarFacOperacionProforma)
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


                Dim FacOperacionProformaAuxiliar As New FacOperacionProforma()
                FacOperacionProformaAuxiliar.Deposito = "1"
                'Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ConsultarTodos()
                Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ObtenerFacOperacionProformasFiltro(FacOperacionProformaAuxiliar)
                FacOperacionProformaselect = convertir_FacOperacionProformaSelec(Me._FacOperacionProformas)
                Me._ventana.Resultados = FacOperacionProformaselect
                Dim chequevacio As New FacOperacionProforma
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacOperacionProformaFiltrar = chequevacio


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

                Dim v_FacOperacionProforma As List(Of FacOperacionProformaSelec) = DirectCast(_ventana.Resultados, List(Of FacOperacionProformaSelec))
                Dim v_FacOperacionProforma2 As FacOperacionProforma = DirectCast(_ventana.FacOperacionProformaFiltrar, FacOperacionProforma)
                v_FacOperacionProforma2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacOperacionProforma2 As List(Of FacOperacionProforma) = DirectCast(_ventana.Resultados, List(Of FacOperacionProforma))

                For i As Integer = 0 To v_FacOperacionProforma.Count - 1
                    If (v_FacOperacionProforma(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacOperacionProforma(i).Deposito = "2"
                            v_FacOperacionProforma(i).FechaDeposito = v_FacOperacionProforma2.FechaDeposito
                            v_FacOperacionProforma(i).NDeposito = v_FacOperacionProforma2.NDeposito
                            v_FacOperacionProforma(i).Banco = v_FacOperacionProforma2.Banco
                            Dim a As New FacOperacionProforma
                            a = convertir_FacOperacionProforma(v_FacOperacionProforma(i))
                            Dim exitoso As Boolean = _FacOperacionProformaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacOperacionProformaAuxiliar As New FacOperacionProforma()
                FacOperacionProformaAuxiliar.Deposito = "1"
                'Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ConsultarTodos()
                Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ObtenerFacOperacionProformasFiltro(FacOperacionProformaAuxiliar)
                FacOperacionProformaselect = convertir_FacOperacionProformaSelec(Me._FacOperacionProformas)
                Me._ventana.Resultados = FacOperacionProformaselect

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
        ''' Método que invoca una nueva página "ConsultarFacOperacionProforma" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacOperacionProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacOperacionProforma(Me._ventana.FacOperacionProformaSeleccionado))
            'Me.Navegar(New ConsultarFacOperacionProforma())
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


                'Dim FacOperacionProformaAuxiliar As New FacOperacionProforma()
                'FacOperacionProformaAuxiliar.Deposito = "1"

                ''Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ConsultarTodos()
                'Me._FacOperacionProformas = Me._FacOperacionProformaServicios.ObtenerFacOperacionProformasFiltro(FacOperacionProformaAuxiliar)

                'FacOperacionProformaselect = convertir_FacOperacionProformaSelec(Me._FacOperacionProformas)

                For i As Integer = 0 To FacOperacionProformaselect.Count - 1
                    FacOperacionProformaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacOperacionProformaselect

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


        'para covertir una clase FacOperacionProforma a FacOperacionProformaSelec
        Public Function convertir_FacOperacionProformaSelec(ByVal v_FacOperacionProforma As IList(Of FacOperacionProforma)) As IList(Of FacOperacionProformaSelec)
            Dim FacOperacionProformaSelec As New List(Of FacOperacionProformaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacOperacionProforma.Count
            For i As Integer = 0 To v_FacOperacionProforma.Count - 1
                FacOperacionProformaSelec.Add(New FacOperacionProformaSelec)
                FacOperacionProformaSelec.Item(i).Seleccion = False
                FacOperacionProformaSelec.Item(i).Banco = v_FacOperacionProforma.Item(i).Banco
                FacOperacionProformaSelec.Item(i).BancoG = v_FacOperacionProforma.Item(i).BancoG
                FacOperacionProformaSelec.Item(i).Id = v_FacOperacionProforma.Item(i).Id
                FacOperacionProformaSelec.Item(i).NDeposito = v_FacOperacionProforma.Item(i).NDeposito
                FacOperacionProformaSelec.Item(i).Deposito = v_FacOperacionProforma.Item(i).Deposito
                FacOperacionProformaSelec.Item(i).NCheque = v_FacOperacionProforma.Item(i).NCheque
                FacOperacionProformaSelec.Item(i).Monto = v_FacOperacionProforma.Item(i).Monto
                monto = v_FacOperacionProforma.Item(i).Monto + monto
                FacOperacionProformaSelec.Item(i).Fecha = v_FacOperacionProforma.Item(i).Fecha
                FacOperacionProformaSelec.Item(i).FechaReg = v_FacOperacionProforma.Item(i).FechaReg
                FacOperacionProformaSelec.Item(i).FechaDeposito = v_FacOperacionProforma.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacOperacionProformaSelec
        End Function

        'para covertir una clase FacOperacionProformaSelec a FacOperacionProforma
        Public Function convertir_FacOperacionProforma(ByVal v_FacOperacionProformaSelec2 As FacOperacionProformaSelec) As FacOperacionProforma
            Dim FacOperacionProforma2 As New FacOperacionProforma
            FacOperacionProforma2.Banco = v_FacOperacionProformaSelec2.Banco
            FacOperacionProforma2.BancoG = v_FacOperacionProformaSelec2.BancoG
            FacOperacionProforma2.Id = v_FacOperacionProformaSelec2.Id
            FacOperacionProforma2.NDeposito = v_FacOperacionProformaSelec2.NDeposito
            FacOperacionProforma2.Deposito = v_FacOperacionProformaSelec2.Deposito
            FacOperacionProforma2.NCheque = v_FacOperacionProformaSelec2.NCheque
            FacOperacionProforma2.Monto = v_FacOperacionProformaSelec2.Monto
            FacOperacionProforma2.Fecha = v_FacOperacionProformaSelec2.Fecha
            FacOperacionProforma2.FechaReg = v_FacOperacionProformaSelec2.FechaReg
            FacOperacionProforma2.FechaDeposito = v_FacOperacionProformaSelec2.FechaDeposito

            Return FacOperacionProforma2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacOperacionProformaSelec
        Inherits FacOperacionProforma

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