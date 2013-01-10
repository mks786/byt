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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFacturaProformas
    Class PresentadorConsultarDepositoFacFacturaProformas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacFacturaProformas
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private _FacFacturaProformas As IList(Of FacFacturaProforma)
        Dim FacFacturaProformaselect As IList(Of FacFacturaProformaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacFacturaProformas)
            Try
                Me._ventana = ventana
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturaProformas, Recursos.Ids.fac_ConsultarFacFacturaProforma)
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


                Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                FacFacturaProformaAuxiliar.Deposito = "1"
                'Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ConsultarTodos()
                Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)
                FacFacturaProformaselect = convertir_FacFacturaProformaSelec(Me._FacFacturaProformas)
                Me._ventana.Resultados = FacFacturaProformaselect
                Dim chequevacio As New FacFacturaProforma
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacFacturaProformaFiltrar = chequevacio


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

                Dim v_FacFacturaProforma As List(Of FacFacturaProformaSelec) = DirectCast(_ventana.Resultados, List(Of FacFacturaProformaSelec))
                Dim v_FacFacturaProforma2 As FacFacturaProforma = DirectCast(_ventana.FacFacturaProformaFiltrar, FacFacturaProforma)
                v_FacFacturaProforma2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacFacturaProforma2 As List(Of FacFacturaProforma) = DirectCast(_ventana.Resultados, List(Of FacFacturaProforma))

                For i As Integer = 0 To v_FacFacturaProforma.Count - 1
                    If (v_FacFacturaProforma(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacFacturaProforma(i).Deposito = "2"
                            v_FacFacturaProforma(i).FechaDeposito = v_FacFacturaProforma2.FechaDeposito
                            v_FacFacturaProforma(i).NDeposito = v_FacFacturaProforma2.NDeposito
                            v_FacFacturaProforma(i).Banco = v_FacFacturaProforma2.Banco
                            Dim a As New FacFacturaProforma
                            a = convertir_FacFacturaProforma(v_FacFacturaProforma(i))
                            Dim exitoso As Boolean = _FacFacturaProformaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                FacFacturaProformaAuxiliar.Deposito = "1"
                'Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ConsultarTodos()
                Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)
                FacFacturaProformaselect = convertir_FacFacturaProformaSelec(Me._FacFacturaProformas)
                Me._ventana.Resultados = FacFacturaProformaselect

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
        ''' Método que invoca una nueva página "ConsultarFacFacturaProforma" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFacturaProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacFacturaProforma(Me._ventana.FacFacturaProformaSeleccionado))
            'Me.Navegar(New ConsultarFacFacturaProforma())
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


                'Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                'FacFacturaProformaAuxiliar.Deposito = "1"

                ''Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ConsultarTodos()
                'Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)

                'FacFacturaProformaselect = convertir_FacFacturaProformaSelec(Me._FacFacturaProformas)

                For i As Integer = 0 To FacFacturaProformaselect.Count - 1
                    FacFacturaProformaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacFacturaProformaselect

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


        'para covertir una clase FacFacturaProforma a FacFacturaProformaSelec
        Public Function convertir_FacFacturaProformaSelec(ByVal v_FacFacturaProforma As IList(Of FacFacturaProforma)) As IList(Of FacFacturaProformaSelec)
            Dim FacFacturaProformaSelec As New List(Of FacFacturaProformaSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacFacturaProforma.Count
            For i As Integer = 0 To v_FacFacturaProforma.Count - 1
                FacFacturaProformaSelec.Add(New FacFacturaProformaSelec)
                FacFacturaProformaSelec.Item(i).Seleccion = False
                FacFacturaProformaSelec.Item(i).Banco = v_FacFacturaProforma.Item(i).Banco
                FacFacturaProformaSelec.Item(i).BancoG = v_FacFacturaProforma.Item(i).BancoG
                FacFacturaProformaSelec.Item(i).Id = v_FacFacturaProforma.Item(i).Id
                FacFacturaProformaSelec.Item(i).NDeposito = v_FacFacturaProforma.Item(i).NDeposito
                FacFacturaProformaSelec.Item(i).Deposito = v_FacFacturaProforma.Item(i).Deposito
                FacFacturaProformaSelec.Item(i).NCheque = v_FacFacturaProforma.Item(i).NCheque
                FacFacturaProformaSelec.Item(i).Monto = v_FacFacturaProforma.Item(i).Monto
                monto = v_FacFacturaProforma.Item(i).Monto + monto
                FacFacturaProformaSelec.Item(i).Fecha = v_FacFacturaProforma.Item(i).Fecha
                FacFacturaProformaSelec.Item(i).FechaReg = v_FacFacturaProforma.Item(i).FechaReg
                FacFacturaProformaSelec.Item(i).FechaDeposito = v_FacFacturaProforma.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacFacturaProformaSelec
        End Function

        'para covertir una clase FacFacturaProformaSelec a FacFacturaProforma
        Public Function convertir_FacFacturaProforma(ByVal v_FacFacturaProformaSelec2 As FacFacturaProformaSelec) As FacFacturaProforma
            Dim FacFacturaProforma2 As New FacFacturaProforma
            FacFacturaProforma2.Banco = v_FacFacturaProformaSelec2.Banco
            FacFacturaProforma2.BancoG = v_FacFacturaProformaSelec2.BancoG
            FacFacturaProforma2.Id = v_FacFacturaProformaSelec2.Id
            FacFacturaProforma2.NDeposito = v_FacFacturaProformaSelec2.NDeposito
            FacFacturaProforma2.Deposito = v_FacFacturaProformaSelec2.Deposito
            FacFacturaProforma2.NCheque = v_FacFacturaProformaSelec2.NCheque
            FacFacturaProforma2.Monto = v_FacFacturaProformaSelec2.Monto
            FacFacturaProforma2.Fecha = v_FacFacturaProformaSelec2.Fecha
            FacFacturaProforma2.FechaReg = v_FacFacturaProformaSelec2.FechaReg
            FacFacturaProforma2.FechaDeposito = v_FacFacturaProformaSelec2.FechaDeposito

            Return FacFacturaProforma2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacFacturaProformaSelec
        Inherits FacFacturaProforma

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