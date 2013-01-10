Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Imports Trascend.Bolet.Cliente.FacReportes
Imports System.Windows
Imports System.Windows.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DataTable = System.Data.DataTable
Imports System.Data
Namespace Presentadores.FacReportes
    Class PresentadorFacturaAnuladaRpt
        Inherits PresentadorBase

        Private _ventana As IFacturaAnuladaRpt
        Private _FacFacturaAnulada As FacFacturaAnulada
        Private _FacFactura As FacFactura
        Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        ' Dim _FacFacturaDetalle As List(Of FacFactuDetalle)
        'Private _FacFacturaServicios As IFacFacturaServicios
        Private _facoperacionanuladaServicios As IFacOperacionAnuladaServicios
        Private _PaisServicios As IPaisServicios
        'Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        'Private _asociadosServicios As IAsociadoServicios
        'Private _InteresadosServicios As IInteresadoServicios
        'Private _contactoServicios As IContactoServicios
        ''Private _facoperacionesServicios As IFacOperacionServicios
        'Private _idiomasServicios As IIdiomaServicios
        'Private _monedasServicios As IMonedaServicios
        'Private _asociados As IList(Of Asociado)
        'Private _Interesados As IList(Of Interesado)
        'Private _asociadosimp As IList(Of Asociado)
        'Private _Marcas As IList(Of Marca)
        'Private _detalleenvios As IList(Of FacDetalleEnvio)
        'Private _Cartas As IList(Of Carta)
        ''Private _FacOperaciones As IList(Of FacOperacion)
        'Private _tasasServicios As ITasaServicios
        ''Private _FacCreditoServicios As IFacCreditoServicios
        'Private _FacContadorProServicios As IFacContadorProServicios
        'Private _bancosServicios As IFacBancoServicios
        'Private _detalleenviosServicios As IFacDetalleEnvioServicios
        ' Private _etiquetaServicios As IEtiquetaServicios
        'Private _cartasServicios As ICartaServicios
        'Private _paisesServicios As IPaisServicios
        'Private _desgloseserviciosServicios As IFacDesgloseServicioServicios
        'Private _DepartamentoserviciosServicios As IFacDepartamentoServicioServicios
        'Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        'Private _FacFactuDetaProformasServicios As IFacFactuDetaProformaServicios
        'Private _TarifaServiciosServicios As ITarifaServicioServicios
        'Private _DocumentosMarcasServicios As IDocumentosMarcaServicios
        'Private _DocumentosPatentesServicios As IDocumentosPatenteServicios
        'Private _DocumentosTraduccionesServicios As IDocumentosTraduccionServicios
        'Private _FacRecursosServicios As IFacRecursoServicios
        'Private _MaterialesServicios As IMaterialServicios
        'Private _MarcasServicios As IMarcaServicios
        'Private _PatentesServicios As IPatenteServicios
        'Private _FacAnualidadesServicios As IFacAnualidadServicios
        'Private _TipoMarcasServicios As ITipoMarcaServicios
        'Private _TipoPatentesServicios As ITipoPatenteServicios
        'Private _TipoClasesServicios As ITipoClaseServicios
        'Private _FacOperacionDetalleTmsServicios As IFacOperacionDetalleTmServicios
        'Private _FacImpuestosServicios As IFacImpuestoServicios
        'Private _FacOperacionServicios As IFacOperacionServicios
        'Private _FacOperacionProformasServicios As IFacOperacionProformaServicios
        'Private _FacOperacionDetaServicios As IFacOperacionDetalleServicios
        'Private _FacOperacionDetaProformasServicios As IFacOperacionDetaProformaServicios
        'Private _FacOperacionDetaTmServicios As IFacOperacionDetalleTmServicios
        'Private _FacOperacionDetaTmProformasServicios As IFacOperacionDetaTmProformaServicios
        'Private _FacContadorServicios As IContadorFacServicios
        'Private _FacInternacionalesServicios As IFacInternacionalServicios
        'FacOperacionDetaProforma
        ''Private _FacFormaServicios As IFacFormaServicios
        'Dim xoperacion As String
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacFacturaProforma">FacFacturaProforma a mostrar</param>
        Public Sub New(ByVal ventana As IFacturaAnuladaRpt, ByVal FacFactura As Object)
            Try
                Me._ventana = ventana
                _FacFactura = FacFactura

                'Me._ventana.FacFacturaProforma = New FacFacturaProforma()
                'Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._facoperacionanuladaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionAnuladaServicios")), IFacOperacionAnuladaServicios)
                Me._FacFacturaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaServicios")), IFacFacturaAnuladaServicios)
                Me._PaisServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                'Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                'Me._InteresadosServicios = DirectCast(Activator.GetObject(GetType(IInteresadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("InteresadoServicios")), IInteresadoServicios)
                ''Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                'Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                'Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                'Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                ''Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                'Me._FacContadorProServicios = DirectCast(Activator.GetObject(GetType(IFacContadorProServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacContadorProServicios")), IFacContadorProServicios)
                'Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                'Me._detalleenviosServicios = DirectCast(Activator.GetObject(GetType(IFacDetalleEnvioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DetalleEnvioServicios")), IFacDetalleEnvioServicios)
                'Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                'Me._paisesServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                'Me._desgloseserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseservicioServicios")), IFacDesgloseServicioServicios)
                'Me._DepartamentoserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDepartamentoServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicioServicios")), IFacDepartamentoServicioServicios)
                'Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                'Me._etiquetaServicios = DirectCast(Activator.GetObject(GetType(IEtiquetaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("EtiquetaServicios")), IEtiquetaServicios)
                'Me._FacFactuDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                'Me._TarifaServiciosServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicioServicios")), ITarifaServicioServicios)

                'Me._DocumentosMarcasServicios = DirectCast(Activator.GetObject(GetType(IDocumentosMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosMarcaServicios")), IDocumentosMarcaServicios)
                'Me._DocumentosPatentesServicios = DirectCast(Activator.GetObject(GetType(IDocumentosPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosPatenteServicios")), IDocumentosPatenteServicios)
                'Me._DocumentosTraduccionesServicios = DirectCast(Activator.GetObject(GetType(IDocumentosTraduccionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosTraduccionServicios")), IDocumentosTraduccionServicios)
                'Me._FacRecursosServicios = DirectCast(Activator.GetObject(GetType(IFacRecursoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacRecursoServicios")), IFacRecursoServicios)
                'Me._MaterialesServicios = DirectCast(Activator.GetObject(GetType(IMaterialServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MaterialServicios")), IMaterialServicios)
                ''Me._FacFormaServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)
                'Me._MarcasServicios = DirectCast(Activator.GetObject(GetType(IMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MarcaServicios")), IMarcaServicios)
                'Me._PatentesServicios = DirectCast(Activator.GetObject(GetType(IPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PatenteServicios")), IPatenteServicios)
                'Me._FacAnualidadesServicios = DirectCast(Activator.GetObject(GetType(IFacAnualidadServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacAnualidadServicios")), IFacAnualidadServicios)
                'Me._TipoMarcasServicios = DirectCast(Activator.GetObject(GetType(ITipoMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoMarcaServicios")), ITipoMarcaServicios)
                'Me._TipoPatentesServicios = DirectCast(Activator.GetObject(GetType(ITipoPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoPatenteServicios")), ITipoPatenteServicios)
                'Me._TipoClasesServicios = DirectCast(Activator.GetObject(GetType(ITipoClaseServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoClaseServicios")), ITipoClaseServicios)
                'Me._FacOperacionDetalleTmsServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleTmServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleTmServicios")), IFacOperacionDetalleTmServicios)
                'Me._FacImpuestosServicios = DirectCast(Activator.GetObject(GetType(IFacImpuestoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacImpuestoServicios")), IFacImpuestoServicios)
                'Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                'Me._FacOperacionProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                'Me._FacOperacionDetaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleServicios")), IFacOperacionDetalleServicios)
                'Me._FacOperacionDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaProformaServicios")), IFacOperacionDetaProformaServicios)
                'Me._FacOperacionDetaTmServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleTmServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleTmServicios")), IFacOperacionDetalleTmServicios)
                'Me._FacOperacionDetaTmProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaTmProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaTmProformaServicios")), IFacOperacionDetaTmProformaServicios)
                'Me._FacContadorServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                'Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)

                'eliminar_operacion_detalle_tm_usuario() ' para eliminar los operacion tmp de operacion_detalle_tm

                'Dim proforma As FacFacturaProforma = FacFacturaOProforma
                'If proforma.Status = 1 Then
                '    pasar_profora_a_factura(FacFacturaOProforma)
                'Else
                '    'Me._ventana.FacFactura = FacFacturaOProforma
                'End If

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacturaAnuladaRpt(_FacFactura))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
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
                'If (existe_tasa_dia(Date.Now, "US") = True) Then
                Me.ActualizarTituloVentanaPrincipal("Reporte Factura", "Reporte")

                _FacFacturaAnulada = consultar_factura_anulada(_FacFactura.Id)



                Me._ventana.FocoPredeterminado()

                'procedimiento para crear el reporte
                Reporte()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
                'Else
                'Me.Navegar(Recursos.MensajesConElUsuario.fac_error_tasa_dia, True)
                'End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Function consultar_factura_anulada(ByVal id As Integer) As FacFacturaAnulada
            Dim FacFacturaAuxiliar As New FacFacturaAnulada()
            Dim FacFacturas As List(Of FacFacturaAnulada)
            FacFacturaAuxiliar.Id = id
            FacFacturas = Me._FacFacturaAnuladaServicios.ObtenerFacFacturaAnuladasFiltro(FacFacturaAuxiliar)
            If FacFacturas IsNot Nothing Then
                If FacFacturas.Count > 0 Then
                    Return (FacFacturas(0))
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function

        'Public Sub imprimir_factura()
        '    _FacFactura.Status = 1
        '    Reporte()
        'End Sub

        'Public Sub re_imprimir_s_f()
        '    'w_lista = "cfactura=%%cfactura.fac_facturastipo=3imp=%%dst.trailermip=3bst=1ifpago=%%ifpago.fac_facturas"
        '    'Activate("BT_FACT00_N_BF_P".EXEC(W_LISTA))
        '    _FacFactura.Status = 2
        '    Reporte()
        'End Sub

        'Public Sub imprimir_statement()
        '    _FacFactura.Status = 3
        '    Reporte()
        'End Sub

        Public Sub Reporte()
            Mouse.OverrideCursor = Cursors.Wait
            Try

                Dim reporte As New ReportDocument()
                Dim estructuraDeDatosEnc As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)()

                'reporte.Load();                
                Dim datosEnc As New DataTable("DataTable1")               
                datosEnc = datosenc_colum()

                'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                'estructuraDeDatos.Add(estructura)

                ObtenerEstructura(estructuraDeDatosEnc)


                'estructuraDeDatosEnc = ObtenerEstructuraEnc()

                'estructuraDeDatosDeta = ObtenerEstructuraDeta()


                datosEnc = ArmarReporteEnc(datosEnc, estructuraDeDatosEnc)

                'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
                Dim ds As New DataSet()
                ds.Tables.Add(datosEnc)                
                reporte.Load(GetRutaReporte())
                reporte.SetDataSource(ds)
                'reporte.SetDataSource(datosDeta)
                Me._ventana.CrystalViewer = reporte
                'reporte.PrintToPrinter(1, False, 1, 0)                

                Mouse.OverrideCursor = Nothing

                '#End Region

            Catch ex As ApplicationException
                'logger.Error(ex.Message)
                'Me.Navegar(ex.Message, True)
                Mouse.OverrideCursor = Nothing
            Catch ex As Exception
                Mouse.OverrideCursor = Nothing
                '' Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)

            End Try
        End Sub

        Private Function GetRutaReporte() As String
            Dim retorno As String
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFacturaAnulada.rpt"
            'retorno = "C:\DG_2012_09_11\DG\src\Cliente\Cliente_Fac\Reportes\Carta1CR.rpt"
            Return retorno

        End Function

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("TituloNotaAnulacion")
            datosEnc2.Columns.Add("TituloNumeroControl")
            datosEnc2.Columns.Add("TituloNumeroFactura")
            datosEnc2.Columns.Add("FechaFac")
            datosEnc2.Columns.Add("NumeroAnulacion")
            datosEnc2.Columns.Add("NumeroControl")
            datosEnc2.Columns.Add("NumeroFacturaStatement")
            datosEnc2.Columns.Add("Motivo")
            datosEnc2.Columns.Add("Detalle")
            datosEnc2.Columns.Add("TituloMonto")
            datosEnc2.Columns.Add("Monto")
            datosEnc2.Columns.Add("Rif")
            datosEnc2.Columns.Add("Cliente")
            datosEnc2.Columns.Add("Fecha")
            datosEnc2.Columns.Add("TituloFactura")
            datosEnc2.Columns.Add("RifCliente")
            datosEnc2.Columns.Add("NitCliente")
            datosEnc2.Columns.Add("Moneda")
            datosEnc2.Columns.Add("Cliente2")
            datosEnc2.Columns.Add("Pais")
            Return datosEnc2
        End Function

        Public Sub lp_fecha_esc_n(ByVal dfecha As Date, ByRef cfecha As String)
            Dim w_dia, w_mes, w_ano As Integer
            w_mes = dfecha.Month
            w_dia = dfecha.Day
            w_ano = dfecha.Year
            cfecha = fecha(w_mes, w_dia, w_ano)
        End Sub

        Public Sub lp_fecha_esc_n_bf(ByVal dfecha As Date, ByRef cfecha As String)
            Dim w_dia, w_mes, w_ano As Integer
            w_mes = dfecha.Month
            w_dia = dfecha.Day
            w_ano = dfecha.Year
            If w_dia < 10 Then
                w_dia = "0" & w_dia
            End If
            If w_mes < 10 Then
                cfecha = "Caracas " & w_dia & "/0" & w_mes & "/" & w_ano
            Else
                cfecha = "Caracas " & w_dia & "/" & w_mes & "/" & w_ano
            End If
        End Sub


        Public Function fecha(ByVal mes As Integer, ByVal dia As Integer, ByVal anio As Integer) As String
            Dim retorna As String = ""
            Select Case mes
                Case 1
                    retorna = "Caracas, Enero " & dia & ", " & anio
                Case 2
                    retorna = "Caracas, Febrero " & dia & ", " & anio
                Case 3
                    retorna = "Caracas, Marzo " & dia & ", " & anio
                Case 4
                    retorna = "Caracas, Abril " & dia & ", " & anio
                Case 5
                    retorna = "Caracas, Mayo " & dia & ", " & anio
                Case 6
                    retorna = "Caracas, Junio " & dia & ", " & anio
                Case 7
                    retorna = "Caracas, Julio " & dia & ", " & anio
                Case 8
                    retorna = "Caracas, Agosto " & dia & ", " & anio
                Case 9
                    retorna = "Caracas, Septiembre " & dia & ", " & anio
                Case 10
                    retorna = "Caracas, Octubre " & dia & ", " & anio
                Case 11
                    retorna = "Caracas, Noviembre " & dia & ", " & anio
                Case 12
                    retorna = "Caracas, Diciembre " & dia & ", " & anio
            End Select

            Return (retorna)
        End Function


        Public Function inicializar_enc() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            structura.TituloNotaAnulacion = ""
            structura.TituloNumeroControl = ""
            structura.TituloNumeroFactura = ""
            structura.FechaFac = ""
            structura.NumeroAnulacion = ""
            structura.NumeroControl = ""
            structura.NumeroFacturaStatement = ""
            structura.Motivo = ""
            structura.Detalle = ""
            structura.TituloMonto = ""
            structura.Monto = ""
            structura.Rif = ""
            structura.Cliente = ""
            structura.Fecha = ""
            structura.TituloFactura = ""
            structura.RifCliente = ""
            structura.NitCliente = ""
            structura.Cliente2 = ""
            structura.Pais = ""
            Return (structura)
        End Function


        Public Sub lp_compl(ByVal cCadena_i As String, ByRef csalida As String)
            Dim nmed, ncero, nini As Integer
            nmed = cCadena_i.Length

            If nmed < 7 Then
                csalida = cCadena_i
                nini = 1
                ncero = 7 - nmed
                While (nini <= ncero)
                    csalida = "0" & cCadena_i
                    nini = nini + 1
                End While
                csalida = "No.  " & csalida
            Else
                csalida = "No.  " & cCadena_i
            End If
        End Sub

        Public Function consultar_operaciones_anuladas(ByVal id As Integer) As FacOperacionAnulada
            Dim FacoperacionanuladaAuxiliar As New FacOperacionAnulada
            Dim Facoperacionanuladas As List(Of FacOperacionAnulada)
            FacoperacionanuladaAuxiliar.CodigoOperacion = id
            FacoperacionanuladaAuxiliar.Id = "ND"

            Facoperacionanuladas = Me._facoperacionanuladaServicios.ObtenerFacOperacionAnuladasFiltro(FacoperacionanuladaAuxiliar)
            If Facoperacionanuladas IsNot Nothing Then
                If Facoperacionanuladas.Count > 0 Then
                    Return (Facoperacionanuladas(0))
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function


        Public Function fac_repfac_anul_fisica() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            Dim FC As String = ""
            structura = inicializar_enc()
            structura.Rif = "RIF: J-00044377-7 / NIT: 0036695722"
            Try
                lp_fecha_esc_n(_FacFacturaAnulada.FechaAnulacion, FC)
                structura.Fecha = FC
                structura.Cliente = "NO APLICA."
                If _FacFacturaAnulada.Secanula IsNot Nothing Then
                    lp_compl(_FacFacturaAnulada.Secanula, structura.NumeroAnulacion)
                End If
                structura.TituloFactura = "NUMERO DE FACTURA"
                If _FacFacturaAnulada.Seniat IsNot Nothing Then
                    lp_compl(_FacFacturaAnulada.Seniat, structura.NumeroFacturaStatement)
                End If
                structura.NumeroControl = "No.  " & _FacFacturaAnulada.Control
                structura.Motivo = "Factura Anulada por error en la Impresión."
                structura.TituloNotaAnulacion = "NOTA DE ANULACION"
                structura.TituloNumeroControl = "NUMERO DE CONTROL"
                structura.TituloNumeroFactura = "NUMERO DE FACTURA"

                structura.TituloMonto = "MONTO"
                structura.Monto = "0.000"

            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Public Function fac_repfac_anul(ByVal fechas As DateTime) As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            Dim FC As String = ""
            structura = inicializar_enc()
            structura.Rif = "RIF: J-00044377-7 / NIT: 0036695722"
            Try
                lp_fecha_esc_n(_FacFacturaAnulada.FechaAnulacion, FC)
                structura.Fecha = FC
                If _FacFacturaAnulada.Iintere = True Then
                    structura.Cliente2 = _FacFacturaAnulada.Asociado.Nombre
                End If
                structura.Cliente = _FacFacturaAnulada.Asociado.Nombre & " " & _FacFacturaAnulada.Asociado.Domicilio
                If _FacFacturaAnulada.Asociado.Rif IsNot Nothing And _FacFacturaAnulada.Asociado.Rif <> "" Then
                    structura.RifCliente = "RIF: " & _FacFacturaAnulada.Asociado.Rif
                End If
                If _FacFacturaAnulada.Asociado.Nit IsNot Nothing And _FacFacturaAnulada.Asociado.Nit <> "" Then
                    structura.RifCliente = "NIT: " & _FacFacturaAnulada.Asociado.Nit
                End If
                If _FacFacturaAnulada.Asociado IsNot Nothing Then
                    If _FacFacturaAnulada.Asociado.Pais IsNot Nothing Then
                        Dim paises As IList(Of Pais) = Me._PaisServicios.ConsultarTodos()
                        structura.Pais = BuscarPais(paises, _FacFacturaAnulada.Asociado.Pais).NombreIngles
                    End If
                End If
                If _FacFacturaAnulada.Xter = 1 Then
                    If _FacFacturaAnulada.Secanula IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Secanula, structura.NumeroAnulacion)
                    End If
                    structura.TituloFactura = "NUMERO DE FACTURA"
                    structura.FechaFac = _FacFacturaAnulada.FechaSeniat
                    If _FacFacturaAnulada.Seniat IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Seniat, structura.NumeroFacturaStatement)
                    End If
                    structura.NumeroControl = "No.  " & _FacFacturaAnulada.Control
                    If _FacFacturaAnulada.Motivo IsNot Nothing Then
                        structura.Motivo = _FacFacturaAnulada.Motivo.Detalle
                    End If
                    structura.Detalle = _FacFacturaAnulada.Detalle
                    structura.TituloNotaAnulacion = "NOTA DE ANULACION"
                    structura.TituloNumeroControl = "NUMERO DE CONTROL"
                    structura.TituloNumeroFactura = "NUMERO DE FACTURA"
                End If
                If _FacFacturaAnulada.Xter = 2 Then
                    If _FacFacturaAnulada.Secanula2 IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Secanula2, structura.NumeroAnulacion)
                    End If
                    structura.TituloFactura = "STATEMENT"
                    structura.FechaFac = _FacFacturaAnulada.FechaFactura
                    If _FacFacturaAnulada.Id IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Id, structura.NumeroFacturaStatement)
                    End If
                    structura.NumeroControl = "No.  " & _FacFacturaAnulada.Control2
                    structura.Motivo = _FacFacturaAnulada.Motivo2.Detalle
                    structura.Detalle = _FacFacturaAnulada.Detalle2
                    structura.TituloNotaAnulacion = "NOTA DE ANULACION"
                    structura.TituloNumeroControl = "NUMERO DE CONTROL"
                    structura.TituloNumeroFactura = "NUMERO DE STATEMENT"
                End If

                If _FacFacturaAnulada.Xter = 3 Then
                    If _FacFacturaAnulada.Secanula2 IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Secanula2, structura.NumeroAnulacion)
                    End If
                    structura.TituloFactura = "STATEMENT"
                    structura.FechaFac = _FacFacturaAnulada.FechaFactura
                    If _FacFacturaAnulada.Id IsNot Nothing Then
                        lp_compl(_FacFacturaAnulada.Id, structura.NumeroFacturaStatement)
                    End If
                    structura.NumeroControl = "No.  " & _FacFacturaAnulada.Control
                    structura.Motivo = _FacFacturaAnulada.Motivo.Detalle
                    structura.Detalle = _FacFacturaAnulada.Detalle
                    structura.TituloNotaAnulacion = "NOTA DE ANULACION"
                    structura.TituloNumeroControl = "NUMERO DE CONTROL"
                    structura.TituloNumeroFactura = "NUMERO DE STATEMENT"
                End If
                If fechas <= CDate("31-12-2007") Then
                    If _FacFacturaAnulada.Moneda.Id = "US" Then
                        structura.TituloMonto = "MONTO " & _FacFacturaAnulada.Moneda.Id
                    Else
                        structura.TituloMonto = "MONTO " & _FacFacturaAnulada.Moneda.Id
                    End If
                    Dim facoperacionanulada As FacOperacionAnulada = consultar_operaciones_anuladas(_FacFacturaAnulada.Id)
                    If facoperacionanulada IsNot Nothing Then
                        structura.Monto = facoperacionanulada.Monto
                    End If
                Else
                    Dim facoperacionanulada As FacOperacionAnulada = consultar_operaciones_anuladas(_FacFacturaAnulada.Id)
                    If facoperacionanulada IsNot Nothing Then
                        If _FacFacturaAnulada.Xter = 1 Then
                            structura.TituloMonto = "MONTO BsF"
                            structura.Monto = facoperacionanulada.SaldoBf
                        End If
                        If _FacFacturaAnulada.Xter = 2 Then
                            structura.TituloMonto = "MONTO US"
                            structura.Monto = facoperacionanulada.Saldo
                        End If
                        If _FacFacturaAnulada.Xter = 3 Then
                            structura.TituloMonto = "MONTO US"
                            structura.Monto = facoperacionanulada.Saldo
                        End If
                    End If
                End If

            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFActuraEnc))
            enc = ObtenerEstructuraEnc()
        End Sub

        Private Function ObtenerEstructuraEnc() As List(Of StructReporteFActuraEnc)
            Dim retorno As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)
            Dim structura As New StructReporteFActuraEnc()
            Try
                'structura = fac_repfac_anul_fisica()
                'retorno.Add(structura)
                Select Case _FacFacturaAnulada.Terrero.ToString
                    Case "1"
                        _FacFacturaAnulada.Xter = 1                        
                        structura = fac_repfac_anul(_FacFacturaAnulada.FechaSeniat)                        
                        retorno.Add(structura)
                    Case "2"
                        _FacFacturaAnulada.Xter = 1                        
                        structura = fac_repfac_anul(_FacFacturaAnulada.FechaSeniat)
                        retorno.Add(structura)

                        _FacFacturaAnulada.Xter = 2
                        structura = fac_repfac_anul(_FacFacturaAnulada.FechaFactura)
                        retorno.Add(structura)
                    Case "3"
                        _FacFacturaAnulada.Xter = 3
                        structura = fac_repfac_anul(_FacFacturaAnulada.FechaFactura)
                        retorno.Add(structura)
                End Select
 
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return retorno
        End Function
        
        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("TituloNotaAnulacion") = structura.TituloNotaAnulacion
                    filaDatos("TituloNumeroControl") = structura.TituloNumeroControl
                    filaDatos("TituloNumeroFactura") = structura.TituloNumeroFactura
                    filaDatos("FechaFac") = structura.FechaFac
                    filaDatos("NumeroAnulacion") = structura.NumeroAnulacion
                    filaDatos("NumeroControl") = structura.NumeroControl
                    filaDatos("NumeroFacturaStatement") = structura.NumeroFacturaStatement
                    filaDatos("Motivo") = structura.Motivo
                    filaDatos("Detalle") = structura.Detalle
                    filaDatos("TituloMonto") = structura.TituloMonto
                    filaDatos("Monto") = poner_decimal(structura.Monto)
                    filaDatos("Rif") = structura.Rif
                    filaDatos("Cliente") = structura.Cliente
                    filaDatos("Fecha") = structura.Fecha
                    filaDatos("TituloFactura") = structura.TituloFactura
                    filaDatos("RifCliente") = structura.RifCliente
                    filaDatos("NitCliente") = structura.NitCliente
                    filaDatos("Cliente2") = structura.Cliente2
                    filaDatos("Pais") = structura.Pais
                    datos.Rows.Add(filaDatos)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function
      
        Structure StructReporteFActuraEnc
            Private _TituloNotaAnulacion As String
            Private _TituloNumeroControl As String
            Private _TituloNumeroFactura As String
            Private _FechaFac As String
            Private _NumeroAnulacion As String
            Private _NumeroControl As String
            Private _NumeroFacturaStatement As String
            Private _Motivo As String
            Private _Rif As String
            Private _Detalle As String
            Private _TituloMonto As String
            Private _Monto As String
            Private _Cliente As String
            Private _Fecha As String
            Private _TituloFactura As String
            Private _RifCliente As String
            Private _NitCliente As String
            Private _Cliente2 As String
            Private _Pais As String

            Public Property TituloNotaAnulacion() As String
                Get
                    Return Me._TituloNotaAnulacion
                End Get
                Set(ByVal value As String)
                    Me._TituloNotaAnulacion = value
                End Set
            End Property

            Public Property TituloNumeroControl() As String
                Get
                    Return Me._TituloNumeroControl
                End Get
                Set(ByVal value As String)
                    Me._TituloNumeroControl = value
                End Set
            End Property

            Public Property TituloNumeroFactura() As String
                Get
                    Return Me._TituloNumeroFactura
                End Get
                Set(ByVal value As String)
                    Me._TituloNumeroFactura = value
                End Set
            End Property

            Public Property FechaFac() As String
                Get
                    Return Me._FechaFac
                End Get
                Set(ByVal value As String)
                    Me._FechaFac = value
                End Set
            End Property

            Public Property NumeroAnulacion() As String
                Get
                    Return Me._NumeroAnulacion
                End Get
                Set(ByVal value As String)
                    Me._NumeroAnulacion = value
                End Set
            End Property

            Public Property NumeroControl() As String
                Get
                    Return Me._NumeroControl
                End Get
                Set(ByVal value As String)
                    Me._NumeroControl = value
                End Set
            End Property

            Public Property NumeroFacturaStatement() As String
                Get
                    Return Me._NumeroFacturaStatement
                End Get
                Set(ByVal value As String)
                    Me._NumeroFacturaStatement = value
                End Set
            End Property

            Public Property Motivo() As String
                Get
                    Return Me._Motivo
                End Get
                Set(ByVal value As String)
                    Me._Motivo = value
                End Set
            End Property

            Public Property Detalle() As String
                Get
                    Return Me._Detalle
                End Get
                Set(ByVal value As String)
                    Me._Detalle = value
                End Set
            End Property

            Public Property TituloMonto() As String
                Get
                    Return Me._TituloMonto
                End Get
                Set(ByVal value As String)
                    Me._TituloMonto = value
                End Set
            End Property

            Public Property Monto() As String
                Get
                    Return Me._Monto
                End Get
                Set(ByVal value As String)
                    Me._Monto = value
                End Set
            End Property

            Public Property Rif() As String
                Get
                    Return Me._Rif
                End Get
                Set(ByVal value As String)
                    Me._Rif = value
                End Set
            End Property


            Public Property Cliente() As String
                Get
                    Return Me._Cliente
                End Get
                Set(ByVal value As String)
                    Me._Cliente = value
                End Set
            End Property
            Public Property Fecha() As String
                Get
                    Return Me._Fecha
                End Get
                Set(ByVal value As String)
                    Me._Fecha = value
                End Set
            End Property

            Public Property TituloFactura() As String
                Get
                    Return Me._TituloFactura
                End Get
                Set(ByVal value As String)
                    Me._TituloFactura = value
                End Set
            End Property

            Public Property RifCliente() As String
                Get
                    Return Me._RifCliente
                End Get
                Set(ByVal value As String)
                    Me._RifCliente = value
                End Set
            End Property

            Public Property NitCliente() As String
                Get
                    Return Me._NitCliente
                End Get
                Set(ByVal value As String)
                    Me._NitCliente = value
                End Set
            End Property

            Public Property Cliente2() As String
                Get
                    Return Me._Cliente2
                End Get
                Set(ByVal value As String)
                    Me._Cliente2 = value
                End Set
            End Property

            Public Property Pais() As String
                Get
                    Return Me._Pais
                End Get
                Set(ByVal value As String)
                    Me._Pais = value
                End Set
            End Property

        End Structure

    End Class
End Namespace
