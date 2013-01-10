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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFactuDetaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFactuDetaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFactuDetaProformas
    Class PresentadorConsultarDepositoFacFactuDetaProformas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacFactuDetaProformas
        Private _FacFactuDetaProformaServicios As IFacFactuDetaProformaServicios
        Private _FacFactuDetaProformas As IList(Of FacFactuDetaProforma)
        Dim FacFactuDetaProformaselect As IList(Of FacFactuDetaProformaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacFactuDetaProformas)
            Try
                Me._ventana = ventana
                Me._FacFactuDetaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFactuDetaProformas, Recursos.Ids.fac_ConsultarFacFactuDetaProforma)
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


                Dim FacFactuDetaProformaAuxiliar As New FacFactuDetaProforma()
                FacFactuDetaProformaAuxiliar.Deposito = "1"
                'Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ConsultarTodos()
                Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ObtenerFacFactuDetaProformasFiltro(FacFactuDetaProformaAuxiliar)
                FacFactuDetaProformaselect = convertir_FacFactuDetaProformaSelec(Me._FacFactuDetaProformas)
                Me._ventana.Resultados = FacFactuDetaProformaselect
                Dim chequevacio As New FacFactuDetaProforma
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacFactuDetaProformaFiltrar = chequevacio


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

                Dim v_FacFactuDetaProforma As List(Of FacFactuDetaProformaSelec) = DirectCast(_ventana.Resultados, List(Of FacFactuDetaProformaSelec))
                Dim v_FacFactuDetaProforma2 As FacFactuDetaProforma = DirectCast(_ventana.FacFactuDetaProformaFiltrar, FacFactuDetaProforma)
                v_FacFactuDetaProforma2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacFactuDetaProforma2 As List(Of FacFactuDetaProforma) = DirectCast(_ventana.Resultados, List(Of FacFactuDetaProforma))

                For i As Integer = 0 To v_FacFactuDetaProforma.Count - 1
                    If (v_FacFactuDetaProforma(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacFactuDetaProforma(i).Deposito = "2"
                            v_FacFactuDetaProforma(i).FechaDeposito = v_FacFactuDetaProforma2.FechaDeposito
                            v_FacFactuDetaProforma(i).NDeposito = v_FacFactuDetaProforma2.NDeposito
                            v_FacFactuDetaProforma(i).Banco = v_FacFactuDetaProforma2.Banco
                            Dim a As New FacFactuDetaProforma
                            a = convertir_FacFactuDetaProforma(v_FacFactuDetaProforma(i))
                            Dim exitoso As Boolean = _FacFactuDetaProformaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacFactuDetaProformaAuxiliar As New FacFactuDetaProforma()
                FacFactuDetaProformaAuxiliar.Deposito = "1"
                'Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ConsultarTodos()
                Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ObtenerFacFactuDetaProformasFiltro(FacFactuDetaProformaAuxiliar)
                FacFactuDetaProformaselect = convertir_FacFactuDetaProformaSelec(Me._FacFactuDetaProformas)
                Me._ventana.Resultados = FacFactuDetaProformaselect

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
        ''' Método que invoca una nueva página "ConsultarFacFactuDetaProforma" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFactuDetaProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacFactuDetaProforma(Me._ventana.FacFactuDetaProformaSeleccionado))
            'Me.Navegar(New ConsultarFacFactuDetaProforma())
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


                'Dim FacFactuDetaProformaAuxiliar As New FacFactuDetaProforma()
                'FacFactuDetaProformaAuxiliar.Deposito = "1"

                ''Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ConsultarTodos()
                'Me._FacFactuDetaProformas = Me._FacFactuDetaProformaServicios.ObtenerFacFactuDetaProformasFiltro(FacFactuDetaProformaAuxiliar)

                'FacFactuDetaProformaselect = convertir_FacFactuDetaProformaSelec(Me._FacFactuDetaProformas)

                For i As Integer = 0 To FacFactuDetaProformaselect.Count - 1
                    FacFactuDetaProformaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacFactuDetaProformaselect

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


        'para covertir una clase FacFactuDetaProforma a FacFactuDetaProformaSelec
        Public Function convertir_FacFactuDetaProformaSelec(ByVal v_FacFactuDetaProforma As IList(Of FacFactuDetaProforma)) As IList(Of FacFactuDetaProformaSelec)
            Dim FacFactuDetaProformaSelec As New List(Of FacFactuDetaProformaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacFactuDetaProforma.Count
            For i As Integer = 0 To v_FacFactuDetaProforma.Count - 1
                FacFactuDetaProformaSelec.Add(New FacFactuDetaProformaSelec)
                FacFactuDetaProformaSelec.Item(i).Seleccion = False
                FacFactuDetaProformaSelec.Item(i).Banco = v_FacFactuDetaProforma.Item(i).Banco
                FacFactuDetaProformaSelec.Item(i).BancoG = v_FacFactuDetaProforma.Item(i).BancoG
                FacFactuDetaProformaSelec.Item(i).Id = v_FacFactuDetaProforma.Item(i).Id
                FacFactuDetaProformaSelec.Item(i).NDeposito = v_FacFactuDetaProforma.Item(i).NDeposito
                FacFactuDetaProformaSelec.Item(i).Deposito = v_FacFactuDetaProforma.Item(i).Deposito
                FacFactuDetaProformaSelec.Item(i).NCheque = v_FacFactuDetaProforma.Item(i).NCheque
                FacFactuDetaProformaSelec.Item(i).Monto = v_FacFactuDetaProforma.Item(i).Monto
                monto = v_FacFactuDetaProforma.Item(i).Monto + monto
                FacFactuDetaProformaSelec.Item(i).Fecha = v_FacFactuDetaProforma.Item(i).Fecha
                FacFactuDetaProformaSelec.Item(i).FechaReg = v_FacFactuDetaProforma.Item(i).FechaReg
                FacFactuDetaProformaSelec.Item(i).FechaDeposito = v_FacFactuDetaProforma.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacFactuDetaProformaSelec
        End Function

        'para covertir una clase FacFactuDetaProformaSelec a FacFactuDetaProforma
        Public Function convertir_FacFactuDetaProforma(ByVal v_FacFactuDetaProformaSelec2 As FacFactuDetaProformaSelec) As FacFactuDetaProforma
            Dim FacFactuDetaProforma2 As New FacFactuDetaProforma
            FacFactuDetaProforma2.Banco = v_FacFactuDetaProformaSelec2.Banco
            FacFactuDetaProforma2.BancoG = v_FacFactuDetaProformaSelec2.BancoG
            FacFactuDetaProforma2.Id = v_FacFactuDetaProformaSelec2.Id
            FacFactuDetaProforma2.NDeposito = v_FacFactuDetaProformaSelec2.NDeposito
            FacFactuDetaProforma2.Deposito = v_FacFactuDetaProformaSelec2.Deposito
            FacFactuDetaProforma2.NCheque = v_FacFactuDetaProformaSelec2.NCheque
            FacFactuDetaProforma2.Monto = v_FacFactuDetaProformaSelec2.Monto
            FacFactuDetaProforma2.Fecha = v_FacFactuDetaProformaSelec2.Fecha
            FacFactuDetaProforma2.FechaReg = v_FacFactuDetaProformaSelec2.FechaReg
            FacFactuDetaProforma2.FechaDeposito = v_FacFactuDetaProformaSelec2.FechaDeposito

            Return FacFactuDetaProforma2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacFactuDetaProformaSelec
        Inherits FacFactuDetaProforma

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