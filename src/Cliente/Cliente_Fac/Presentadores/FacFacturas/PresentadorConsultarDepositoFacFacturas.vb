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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFacturas
    Class PresentadorConsultarDepositoFacFacturas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacFacturas
        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacFacturas As IList(Of FacFactura)
        Dim FacFacturaselect As IList(Of FacFacturaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacFacturas)
            Try
                Me._ventana = ventana
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturas, Recursos.Ids.fac_ConsultarFacFactura)
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


                Dim FacFacturaAuxiliar As New FacFactura()
                FacFacturaAuxiliar.Deposito = "1"
                'Me._FacFacturas = Me._FacFacturaServicios.ConsultarTodos()
                Me._FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)
                FacFacturaselect = convertir_FacFacturaSelec(Me._FacFacturas)
                Me._ventana.Resultados = FacFacturaselect
                Dim chequevacio As New FacFactura
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacFacturaFiltrar = chequevacio


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

                Dim v_FacFactura As List(Of FacFacturaSelec) = DirectCast(_ventana.Resultados, List(Of FacFacturaSelec))
                Dim v_FacFactura2 As FacFactura = DirectCast(_ventana.FacFacturaFiltrar, FacFactura)
                v_FacFactura2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacFactura2 As List(Of FacFactura) = DirectCast(_ventana.Resultados, List(Of FacFactura))

                For i As Integer = 0 To v_FacFactura.Count - 1
                    If (v_FacFactura(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacFactura(i).Deposito = "2"
                            v_FacFactura(i).FechaDeposito = v_FacFactura2.FechaDeposito
                            v_FacFactura(i).NDeposito = v_FacFactura2.NDeposito
                            v_FacFactura(i).Banco = v_FacFactura2.Banco
                            Dim a As New FacFactura
                            a = convertir_FacFactura(v_FacFactura(i))
                            Dim exitoso As Boolean = _FacFacturaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacFacturaAuxiliar As New FacFactura()
                FacFacturaAuxiliar.Deposito = "1"
                'Me._FacFacturas = Me._FacFacturaServicios.ConsultarTodos()
                Me._FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)
                FacFacturaselect = convertir_FacFacturaSelec(Me._FacFacturas)
                Me._ventana.Resultados = FacFacturaselect

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
        ''' Método que invoca una nueva página "ConsultarFacFactura" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFactura()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacFactura(Me._ventana.FacFacturaSeleccionado))
            'Me.Navegar(New ConsultarFacFactura())
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


                'Dim FacFacturaAuxiliar As New FacFactura()
                'FacFacturaAuxiliar.Deposito = "1"

                ''Me._FacFacturas = Me._FacFacturaServicios.ConsultarTodos()
                'Me._FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)

                'FacFacturaselect = convertir_FacFacturaSelec(Me._FacFacturas)

                For i As Integer = 0 To FacFacturaselect.Count - 1
                    FacFacturaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacFacturaselect

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


        'para covertir una clase FacFactura a FacFacturaSelec
        Public Function convertir_FacFacturaSelec(ByVal v_FacFactura As IList(Of FacFactura)) As IList(Of FacFacturaSelec)
            Dim FacFacturaSelec As New List(Of FacFacturaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacFactura.Count
            For i As Integer = 0 To v_FacFactura.Count - 1
                FacFacturaSelec.Add(New FacFacturaSelec)
                FacFacturaSelec.Item(i).Seleccion = False
                FacFacturaSelec.Item(i).Banco = v_FacFactura.Item(i).Banco
                FacFacturaSelec.Item(i).BancoG = v_FacFactura.Item(i).BancoG
                FacFacturaSelec.Item(i).Id = v_FacFactura.Item(i).Id
                FacFacturaSelec.Item(i).NDeposito = v_FacFactura.Item(i).NDeposito
                FacFacturaSelec.Item(i).Deposito = v_FacFactura.Item(i).Deposito
                FacFacturaSelec.Item(i).NCheque = v_FacFactura.Item(i).NCheque
                FacFacturaSelec.Item(i).Monto = v_FacFactura.Item(i).Monto
                monto = v_FacFactura.Item(i).Monto + monto
                FacFacturaSelec.Item(i).Fecha = v_FacFactura.Item(i).Fecha
                FacFacturaSelec.Item(i).FechaReg = v_FacFactura.Item(i).FechaReg
                FacFacturaSelec.Item(i).FechaDeposito = v_FacFactura.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacFacturaSelec
        End Function

        'para covertir una clase FacFacturaSelec a FacFactura
        Public Function convertir_FacFactura(ByVal v_FacFacturaSelec2 As FacFacturaSelec) As FacFactura
            Dim FacFactura2 As New FacFactura
            FacFactura2.Banco = v_FacFacturaSelec2.Banco
            FacFactura2.BancoG = v_FacFacturaSelec2.BancoG
            FacFactura2.Id = v_FacFacturaSelec2.Id
            FacFactura2.NDeposito = v_FacFacturaSelec2.NDeposito
            FacFactura2.Deposito = v_FacFacturaSelec2.Deposito
            FacFactura2.NCheque = v_FacFacturaSelec2.NCheque
            FacFactura2.Monto = v_FacFacturaSelec2.Monto
            FacFactura2.Fecha = v_FacFacturaSelec2.Fecha
            FacFactura2.FechaReg = v_FacFacturaSelec2.FechaReg
            FacFactura2.FechaDeposito = v_FacFacturaSelec2.FechaDeposito

            Return FacFactura2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacFacturaSelec
        Inherits FacFactura

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