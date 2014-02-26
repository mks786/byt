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
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacInternacionales
Imports System.Data



Namespace Presentadores.FacAsociadoMarcaPatentes
    Class PresentadorConsultarFacVistaFacturacionCxpInternas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarFacVistaFacturacionCxpInternas
        Private _FacVistaFacturacionCxpInternaservicios As IFacVistaFacturacionCxpInternaServicios
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private _FacInternacionalesServicios As IFacInternacionalServicios
        Private _listaFacturasPorPagar As List(Of FacVistaFacturacionCxpInterna) = New List(Of FacVistaFacturacionCxpInterna)()

        Private _asociado As Asociado
        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarFacVistaFacturacionCxpInternas, ByVal Asociado As Asociado)
            Try
                Me._ventana = ventana
                'Me._ventana.FacGestion  = New FacGestion ()
                _asociado = Asociado
                Me._FacVistaFacturacionCxpInternaservicios = DirectCast(Activator.GetObject(GetType(IFacVistaFacturacionCxpInternaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacVistaFacturacionCxpInternaServicios")), IFacVistaFacturacionCxpInternaServicios)
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)
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

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacVistaFacturacionCxpInternas, Recursos.Ids.fac_ConsultarFacGestion)
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

                'Me._FacVistaFacturacionCxpInternas = Me._FacVistaFacturacionCxpInternaservicios.ConsultarTodos()
                'Me._FacVistaFacturacionCxpInternas = Me._FacVistaFacturacionCxpInternaservicios.ObtenerFacVistaFacturacionCxpInternasFiltro(FacGestion Auxiliar)

                'Me._ventana.Resultados = Nothing
                'Me._ventana.FacGestionFiltrar = New FacVistaFacturacionCxpInterna

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

                Dim FacVistaFacturacionCxpInternaAuxiliar As New FacVistaFacturacionCxpInterna()

                'FacVistaFacturacionCxpInternaAuxiliar.Asociado = _asociado
                FacVistaFacturacionCxpInternaAuxiliar.Asociado_o = _asociado


                'FacVistaFacturacionCxpInternaAuxiliar.Cobrada = "NO"
                FacVistaFacturacionCxpInternaAuxiliar.Pagada = "NO"

                'If (filtroValido = True) Then
                Dim FacVistaFacturacionCxpInternas As List(Of FacVistaFacturacionCxpInterna) = Me._FacVistaFacturacionCxpInternaservicios.ObtenerFacVistaFacturacionCxpInternasFiltro(FacVistaFacturacionCxpInternaAuxiliar)
                Me._ventana.Resultados = Nothing
                Me._ventana.Count = FacVistaFacturacionCxpInternas.Count
                Me._listaFacturasPorPagar = FacVistaFacturacionCxpInternas
                Me._ventana.Resultados = FacVistaFacturacionCxpInternas
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

        Public Sub IrConsultarPagoInternacional()

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim facVistaFacturacionCxpInterno As FacVistaFacturacionCxpInterna =
                    DirectCast(Me._ventana.FacVistaFacCxpInternaSeleccionado, FacVistaFacturacionCxpInterna)
                Dim cproforma As Integer
                If facVistaFacturacionCxpInterno.Id IsNot Nothing Then
                    cproforma = facVistaFacturacionCxpInterno.Id.Value
                    Dim proforma As FacFacturaProforma
                    proforma = ConsultarProforma(cproforma)
                    If proforma IsNot Nothing Then
                        Dim internacional As FacInternacional
                        internacional = buscar_facinternacional(proforma.Id)
                        If internacional IsNot Nothing Then
                            'aqui va la pantalla de facinternacionales Pago
                            Me.Navegar(New FacInternacionalPago(proforma))
                        Else
                            MessageBox.Show("Debe realizar registro del pago", "", MessageBoxButton.OK)
                            Mouse.OverrideCursor = Nothing
                            Exit Sub
                        End If
                    Else

                    End If
                End If



                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

            Catch ex As Exception
                Mouse.OverrideCursor = Nothing
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)

            End Try

        End Sub

        Public Function ConsultarProforma(ByVal cproforma As Integer) As FacFacturaProforma
            Dim proforma As FacFacturaProforma = Nothing
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                FacFacturaProformaAuxiliar.Id = cproforma

                Dim FacFacturaProformas As IList(Of FacFacturaProforma)
                FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)

                If FacFacturaProformas IsNot Nothing Then
                    If FacFacturaProformas.Count > 0 Then
                        proforma = FacFacturaProformas(0)
                    Else
                        proforma = Nothing
                    End If
                Else
                    proforma = Nothing
                End If

                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try

            Return proforma
        End Function

        Public Function buscar_facinternacional(ByVal proforma As Integer) As FacInternacional
            Dim facinternacionalaux As New FacInternacional
            facinternacionalaux.Id = proforma
            Dim facinternacionales As List(Of FacInternacional) = Me._FacInternacionalesServicios.ObtenerFacInternacionalesFiltro(facinternacionalaux)
            If facinternacionales IsNot Nothing Then
                If facinternacionales.Count > 0 Then
                    Return (facinternacionales(0))
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function

        ''' Metodo para exportar un conjunto de Facturas a Excel
        Public Sub ExportarFacturasExcel()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim datos As DataTable = New DataTable()
                '''Dim listaFacturas As List(Of FacVistaFacturacionCxpInterna) = DirectCast(Me._ventana.Resultados, List(Of FacVistaFacturacionCxpInterna))
                datos = CrearColumnasDataTable()
                datos = LlenarDataTable(datos, Me._listaFacturasPorPagar)

                If datos.Rows.Count > 0 Then
                    Me._ventana.ExportarListView(datos)
                Else
                    Me._ventana.Mensaje("No hay registros para exportar", 0)
                End If



                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try
        End Sub

        Private Function CrearColumnasDataTable() As DataTable

            Dim datos As DataTable = New DataTable()

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                datos.Columns.Add("No Proforma", GetType(System.Int32))
                datos.Columns.Add("Cod Asociado", GetType(System.Int32))
                datos.Columns.Add("Asociado", GetType(System.String))
                datos.Columns.Add("No Factura", GetType(System.String))
                datos.Columns.Add("Monto", GetType(System.Double))
                datos.Columns.Add("Fecha Factura", GetType(System.DateTime))
                datos.Columns.Add("Pais", GetType(System.String))
                datos.Columns.Add("Detalle", GetType(System.String))
                datos.Columns.Add("Fecha Recepcion", GetType(System.DateTime))
                datos.Columns.Add("Dias Vencida", GetType(System.Int32))


                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try

            Return datos

        End Function

        Private Function LlenarDataTable(datos As DataTable, listaFacturas As List(Of FacVistaFacturacionCxpInterna)) As DataTable
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                For Each factura As FacVistaFacturacionCxpInterna In listaFacturas
                    Dim filaNueva As DataRow = datos.NewRow()
                    filaNueva("No Proforma") = factura.Id
                    filaNueva("Cod Asociado") = factura.Asociado_o.Id
                    filaNueva("Asociado") = factura.Asociado_o.Nombre
                    filaNueva("No Factura") = factura.NumeroFactura
                    filaNueva("Monto") = factura.Monto
                    filaNueva("Fecha Factura") = factura.FechaFactura
                    filaNueva("Pais") = factura.Pais
                    filaNueva("Detalle") = factura.Detalle
                    If (factura.FechaRecepcion IsNot Nothing) Then
                        filaNueva("Fecha Recepcion") = factura.FechaRecepcion
                    Else
                        filaNueva("Fecha Recepcion") = System.DBNull.Value
                    End If
                    filaNueva("Dias Vencida") = factura.DiasVencida
                    datos.Rows.Add(filaNueva)
                Next


                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try

            Return datos
        End Function

        Public Function ObtenerTituloReporte() As String

            Dim tituloReporte As String = String.Empty

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                tituloReporte = "Reporte de Facturas Por Pagar Asociado: " + _asociado.Nombre

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try

            Return tituloReporte

        End Function


    End Class
End Namespace