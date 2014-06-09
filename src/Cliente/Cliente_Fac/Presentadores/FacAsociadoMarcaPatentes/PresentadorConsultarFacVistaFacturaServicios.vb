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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacAsociadoMarcaPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacAsociadoMarcaPatentes
    Class PresentadorConsultarFacVistaFacturaServicios
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarFacVistaFacturaServicios
        Private _FacVistaFacturaServicioservicios As IFacVistaFacturaServicioServicios
        Private _FacFacturaServicios As IFacFacturaServicios
        Private _id As String
        Private _tipo As String
        Private _cFactura As String
        Private _cAlterno As String
        Private _referencia As String
        Private _presentacion As Boolean


        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarFacVistaFacturaServicios, ByVal Id As String, ByVal tipo As String)
            Try
                Me._ventana = ventana
                _id = Id
                _tipo = tipo
                'Me._ventana.FacGestion  = New FacGestion ()                
                Me._FacVistaFacturaServicioservicios = DirectCast(Activator.GetObject(GetType(IFacVistaFacturaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacVistaFacturaServicioServicios")), IFacVistaFacturaServicioServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                'Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                ' Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                ' Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                'Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                ' Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                ' Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                '  Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                '  Me._FacFormaServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)               
                '  Me._ListaDatosValoresServicios = DirectCast(Activator.GetObject(GetType(IListaDatosValoresServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ListaDatosValoresServicios")), IListaDatosValoresServicios)
                'Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                ' Me._MediosGestionServicios = DirectCast(Activator.GetObject(GetType(IMediosGestionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MediosGestionServicios")), IMediosGestionServicios)
                'Me._ConceptoGestionServicios = DirectCast(Activator.GetObject(GetType(IConceptoGestionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ConceptoGestionServicios")), IConceptoGestionServicios)
                ' Me._TipoClienteServicios = DirectCast(Activator.GetObject(GetType(ITipoClienteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoClienteServicios")), ITipoClienteServicios)

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        ' Constructor para cuando la ventana se llama desde Control de Eventos de Presentacion
        Public Sub New(ByVal ventana As IConsultarFacVistaFacturaServicios, ByVal cFactura As String, ByVal cAlterno As String, ByVal referencia As String, ByVal presentacion As Boolean)
            Try
                Me._ventana = ventana
                _cFactura = cFactura
                _cAlterno = cAlterno
                _presentacion = presentacion

                Me._FacVistaFacturaServicioservicios = DirectCast(Activator.GetObject(GetType(IFacVistaFacturaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacVistaFacturaServicioServicios")), IFacVistaFacturaServicioServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub


        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacVistaFacturaServicio, Recursos.Ids.fac_ConsultarFacGestion)
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

                'Dim FacGestionAuxiliar As New FacGestion()
                ActualizarTitulo()
                Consultar()
                'Me._FacVistaFacturaServicios = Me._FacVistaFacturaServicioservicios.ConsultarTodos()
                'Me._FacVistaFacturaServicios = Me._FacVistaFacturaServicioservicios.ObtenerFacVistaFacturaServiciosFiltro(FacGestion Auxiliar)

                'Me._ventana.Resultados = Nothing
                'Me._ventana.FacGestionFiltrar = New FacVistaFacturaServicio

                'Dim Medios As IList(Of MediosGestion) = Me._MediosGestionServicios.ConsultarTodos()
                'Dim primerMediosGestion As New MediosGestion()
                'primerMediosGestion.Id = ""
                'Medios.Insert(0, primerMediosGestion)
                'Me._ventana.Medios = Medios

                'Dim Conceptos As IList(Of ConceptoGestion) = Me._ConceptoGestionServicios.ConsultarTodos()
                'Dim primerConceptoGestion As New ConceptoGestion
                'primerConceptoGestion.Id = ""
                'Conceptos.Insert(0, primerConceptoGestion)


                'Me._ventana.Conceptos = Conceptos

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                'Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)()
                'Dim primerafacbanco As New FacBanco()
                'primerafacbanco.Id = Integer.MinValue
                'facbancos.Insert(0, primerafacbanco)
                'Me._ventana.Bancos = facbancos

                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim primeridioma As New Idioma()
                'primeridioma.Id = ""
                'idiomas.Insert(0, primeridioma)
                'Me._ventana.Idiomas = idiomas

                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Dim primeramoneda As New Moneda()
                'primeramoneda.Id = ""
                'monedas.Insert(0, primeramoneda)
                'Me._ventana.Monedas = monedas

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
        Public Sub Consultar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
                Mouse.OverrideCursor = Cursors.Wait

                Dim FacVistaFacturaServicioAuxiliar As New FacVistaFacturaServicio()

                FacVistaFacturaServicioAuxiliar.Id = _id

                FacVistaFacturaServicioAuxiliar.Tipo = _tipo

                '''En caso de que venga desde el Control de las Presentaciones
                If _presentacion Then
                    If (_cFactura IsNot Nothing) Then
                        FacVistaFacturaServicioAuxiliar.Factura = Integer.Parse(_cFactura)
                    ElseIf (_cAlterno IsNot Nothing) Then
                        FacVistaFacturaServicioAuxiliar.CodigoAlterno = _cAlterno
                    ElseIf (_referencia IsNot Nothing) Then
                        FacVistaFacturaServicioAuxiliar.Ourref = _referencia
                    End If
                    FacVistaFacturaServicioAuxiliar.Id = Nothing
                    FacVistaFacturaServicioAuxiliar.Tipo = ""
                End If


                'If (filtroValido = True) Then
                Dim FacVistaFacturaServicios As List(Of FacVistaFacturaServicio) = Me._FacVistaFacturaServicioservicios.ObtenerFacVistaFacturaServiciosFiltro(FacVistaFacturaServicioAuxiliar)
                Me._ventana.Resultados = Nothing
                Me._ventana.Count = FacVistaFacturaServicios.Count
                Me._ventana.Resultados = FacVistaFacturaServicios
                Mouse.OverrideCursor = Nothing


                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                Mouse.OverrideCursor = Nothing
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub BuscarFactura(ByVal Cfactura As Integer)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
                Dim FacFacturaAuxiliar As New FacFactura()
                FacFacturaAuxiliar.Id = Cfactura


                Dim FacFactura As FacFactura
                FacFactura = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)(0)
                If FacFactura IsNot Nothing Then                    
                        IrConsultarFacFactura(FacFactura)                    
                End If
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub


        Public Sub IrConsultarFacFactura(ByVal Factura As FacFactura)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New ConsultarFacFactura(Factura))
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

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
    End Class
End Namespace