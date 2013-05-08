Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones
Namespace Presentadores.FacFacturas
    Class PresentadorConsultarFacFactura
        Inherits PresentadorBase

        Private _ventana As IConsultarFacFactura
        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios
        Private _InteresadosServicios As IInteresadoServicios
        'Private _contactoServicios As IContactoServicios
        ''Private _facoperacionesServicios As IFacOperacionServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios
        Private _asociados As IList(Of Asociado)
        Private _Interesados As IList(Of Interesado)
        Private _asociadosimp As IList(Of Asociado)
        Private _Marcas As IList(Of Marca)
        Private _detalleenvios As IList(Of FacDetalleEnvio)
        Private _Cartas As IList(Of Carta)
        ''Private _FacOperaciones As IList(Of FacOperacion)
        Private _tasasServicios As ITasaServicios
        ''Private _FacCreditoServicios As IFacCreditoServicios
        Private _FacContadorProServicios As IFacContadorProServicios
        Private _bancosServicios As IFacBancoServicios
        Private _guiasServicios As IGuiaServicios
        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _cartasServicios As ICartaServicios
        Private _paisesServicios As IPaisServicios
        Private _desgloseserviciosServicios As IFacDesgloseServicioServicios
        Private _DepartamentoserviciosServicios As IFacDepartamentoServicioServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        Private _FacFactuDetaProformasServicios As IFacFactuDetaProformaServicios
        Private _TarifaServiciosServicios As ITarifaServicioServicios
        Private _DocumentosMarcasServicios As IDocumentosMarcaServicios
        Private _DocumentosPatentesServicios As IDocumentosPatenteServicios
        Private _DocumentosTraduccionesServicios As IDocumentosTraduccionServicios
        Private _FacRecursosServicios As IFacRecursoServicios
        Private _MaterialesServicios As IMaterialServicios
        Private _MarcasServicios As IMarcaServicios
        Private _PatentesServicios As IPatenteServicios
        Private _FacAnualidadesServicios As IFacAnualidadServicios
        Private _TipoMarcasServicios As ITipoMarcaServicios
        Private _TipoPatentesServicios As ITipoPatenteServicios
        Private _TipoClasesServicios As ITipoClaseServicios
        Private _FacOperacionDetalleTmsServicios As IFacOperacionDetalleTmServicios
        Private _FacImpuestosServicios As IFacImpuestoServicios
        Private _FacOperacionServicios As IFacOperacionServicios
        Private _FacOperacionProformasServicios As IFacOperacionProformaServicios
        Private _FacOperacionDetaServicios As IFacOperacionDetalleServicios
        Private _FacOperacionDetaProformasServicios As IFacOperacionDetaProformaServicios
        Private _FacOperacionDetaTmServicios As IFacOperacionDetalleTmServicios
        Private _FacOperacionDetaTmProformasServicios As IFacOperacionDetaTmProformaServicios
        Private _FacContadorServicios As IContadorFacServicios
        Private _FacInternacionalesServicios As IFacInternacionalServicios
        Private _DepartamentoServicios As IDepartamentoServicios
        Private _facfactura As FacFactura
        'FacOperacionDetaProforma
        ''Private _FacFormaServicios As IFacFormaServicios
        Dim xoperacion As String
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacFacturaProforma">FacFacturaProforma a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarFacFactura, ByVal FacFactura As Object)
            Try
                Me._ventana = ventana
                Me._ventana.FacFactura = FacFactura
                _facfactura = FacFactura

                'Me._ventana.FacFacturaProforma = New FacFacturaProforma()
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._InteresadosServicios = DirectCast(Activator.GetObject(GetType(IInteresadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("InteresadoServicios")), IInteresadoServicios)
                ''Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                ''Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                Me._FacContadorProServicios = DirectCast(Activator.GetObject(GetType(IFacContadorProServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacContadorProServicios")), IFacContadorProServicios)
                Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._guiasServicios = DirectCast(Activator.GetObject(GetType(IGuiaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("guiaServicios")), IGuiaServicios)
                Me._detalleenviosServicios = DirectCast(Activator.GetObject(GetType(IFacDetalleEnvioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DetalleEnvioServicios")), IFacDetalleEnvioServicios)
                Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                Me._paisesServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                Me._desgloseserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseservicioServicios")), IFacDesgloseServicioServicios)
                Me._DepartamentoserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDepartamentoServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicioServicios")), IFacDepartamentoServicioServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                Me._FacFactuDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                Me._TarifaServiciosServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicioServicios")), ITarifaServicioServicios)

                Me._DocumentosMarcasServicios = DirectCast(Activator.GetObject(GetType(IDocumentosMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosMarcaServicios")), IDocumentosMarcaServicios)
                Me._DocumentosPatentesServicios = DirectCast(Activator.GetObject(GetType(IDocumentosPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosPatenteServicios")), IDocumentosPatenteServicios)
                Me._DocumentosTraduccionesServicios = DirectCast(Activator.GetObject(GetType(IDocumentosTraduccionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DocumentosTraduccionServicios")), IDocumentosTraduccionServicios)
                Me._FacRecursosServicios = DirectCast(Activator.GetObject(GetType(IFacRecursoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacRecursoServicios")), IFacRecursoServicios)
                Me._MaterialesServicios = DirectCast(Activator.GetObject(GetType(IMaterialServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MaterialServicios")), IMaterialServicios)
                ''Me._FacFormaServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)
                Me._MarcasServicios = DirectCast(Activator.GetObject(GetType(IMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MarcaServicios")), IMarcaServicios)
                Me._PatentesServicios = DirectCast(Activator.GetObject(GetType(IPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PatenteServicios")), IPatenteServicios)
                Me._FacAnualidadesServicios = DirectCast(Activator.GetObject(GetType(IFacAnualidadServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacAnualidadServicios")), IFacAnualidadServicios)
                Me._TipoMarcasServicios = DirectCast(Activator.GetObject(GetType(ITipoMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoMarcaServicios")), ITipoMarcaServicios)
                Me._TipoPatentesServicios = DirectCast(Activator.GetObject(GetType(ITipoPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoPatenteServicios")), ITipoPatenteServicios)
                Me._TipoClasesServicios = DirectCast(Activator.GetObject(GetType(ITipoClaseServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoClaseServicios")), ITipoClaseServicios)
                Me._FacOperacionDetalleTmsServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleTmServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleTmServicios")), IFacOperacionDetalleTmServicios)
                Me._FacImpuestosServicios = DirectCast(Activator.GetObject(GetType(IFacImpuestoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacImpuestoServicios")), IFacImpuestoServicios)
                Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._FacOperacionProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                Me._FacOperacionDetaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleServicios")), IFacOperacionDetalleServicios)
                Me._FacOperacionDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaProformaServicios")), IFacOperacionDetaProformaServicios)
                Me._FacOperacionDetaTmServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleTmServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleTmServicios")), IFacOperacionDetalleTmServicios)
                Me._FacOperacionDetaTmProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaTmProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaTmProformaServicios")), IFacOperacionDetaTmProformaServicios)
                Me._FacContadorServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)
                Me._DepartamentoServicios = DirectCast(Activator.GetObject(GetType(IDepartamentoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicios")), IDepartamentoServicios)

                eliminar_operacion_detalle_tm_usuario() ' para eliminar los operacion tmp de operacion_detalle_tm

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
                If (existe_tasa_dia(Date.Now, "US") = True) Then
                    Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFactura, Recursos.Ids.fac_ConsultarFacFactura)

                    Dim FacFactura As FacFactura = DirectCast(Me._ventana.FacFactura, FacFactura)

                    'Me._detalleenvios = Me._detalleenviosServicios.ConsultarTodos()
                    'Me._ventana.DetalleEnvios = Me._detalleenvios
                    'Dim primeradetalleenvio As New FacDetalleEnvio()
                    'primeradetalleenvio.Id = "FED"
                    'primeradetalleenvio.Descripcion = "FEDEX"

                    'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                    'Me._ventana.Asociados = Me._asociados

                    SaldoPendiente(FacFactura.Id)
                    If FacFactura.CodigoDepartamento IsNot Nothing And FacFactura.CodigoDepartamento <> "" Then
                        Dim departamentos As List(Of Departamento)
                        departamentos = Me._DepartamentoServicios.ConsultarTodos
                        Dim departamentos2 As IEnumerable(Of Departamento) = departamentos
                        departamentos2 = From d In departamentos2 Where d.Id IsNot Nothing AndAlso d.Id.ToLower().Contains(FacFactura.CodigoDepartamento.ToLower())
                        If departamentos2.ToList IsNot Nothing Then
                            If departamentos2.ToList.Count > 0 Then
                                Me._ventana.Departamento = departamentos2.ToList(0).Descripcion
                            End If
                        End If
                    End If

                    Dim asociadoaux As New Asociado
                    Dim asociado As List(Of Asociado)
                    If FacFactura.Asociado IsNot Nothing Then
                        asociadoaux.Id = FacFactura.Asociado.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados = asociado
                        Me._ventana.Asociado = asociado(0)
                        Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre

                        If (asociado(0).Tarifa IsNot Nothing) AndAlso (asociado(0).Tarifa.Id <> "") Then
                            Me._ventana.Tarifa = asociado(0).Tarifa.Id
                        End If
                    End If

                    If FacFactura.AsociadoImp IsNot Nothing Then
                        asociadoaux.Id = FacFactura.AsociadoImp.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.AsociadosImp = asociado
                        Me._ventana.AsociadoImp = asociado(0)
                        Me._ventana.NombreAsociadoImp = asociado(0).Id & " - " & asociado(0).Nombre
                    End If

                    If FacFactura.Carta IsNot Nothing Then
                        Dim cartaaux As New Carta
                        Dim carta As List(Of Carta)
                        cartaaux.Id = FacFactura.Carta.Id
                        carta = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
                        Me._ventana.Cartas = carta
                        Me._ventana.Carta = carta(0)
                        Me._ventana.NombreCarta = carta(0).Id & " - " & carta(0).Medio
                    End If

                    If FacFactura.InteresadoImp IsNot Nothing Then
                        Dim Interesadoaux As New Interesado
                        Dim interesado As List(Of Interesado)
                        Interesadoaux.Id = FacFactura.InteresadoImp.Id
                        interesado = Me._InteresadosServicios.ObtenerInteresadosFiltro(Interesadoaux)
                        Me._ventana.Interesados = interesado
                        Me._ventana.Interesado = interesado(0)
                        Me._ventana.NombreInteresado = interesado(0).Id & " - " & interesado(0).Nombre
                    End If

                    Me._detalleenvios = Me._detalleenviosServicios.ConsultarTodos()
                    Me._ventana.DetalleEnvios = Me._detalleenvios
                    Dim primeradetalleenvio As New FacDetalleEnvio
                    primeradetalleenvio = Me.BuscarFacDetalleEnvio(Me._detalleenvios, FacFactura.DetalleEnvio)
                    Me._ventana.DetalleEnvio = primeradetalleenvio

                    Dim detalleaux As New FacFactuDetalle()
                    detalleaux.Factura = FacFactura
                    Me._ventana.ResultadosFacFactuDeta = Me._FacFactuDetaServicios.ObtenerFacFactuDetallesFiltro(detalleaux)

                    Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                    Me._ventana.Idiomas = idiomas
                    Me._ventana.Idioma = Me.BuscarIdioma(idiomas, FacFactura.Idioma)

                    Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                    Me._ventana.Monedas = monedas
                    Me._ventana.Moneda = Me.BuscarMoneda(monedas, FacFactura.Moneda)


                    Dim guias As List(Of Guia) = Me._guiasServicios.ConsultarTodos()
                    'Me._ventana.Guias = guias
                    'If FacFactura.CodGuia <> "" And FacFactura.CodGuia IsNot Nothing Then
                    '    Dim guiaaux As New Guia
                    '    guiaaux.Id = FacFactura.CodGuia
                    '    Me._ventana.Guia = Me.BuscarGuia(guias, guiaaux)
                    'Else
                    Dim primeraguias As New Guia
                    primeraguias.Id = ""
                    primeraguias.Des_guia = ""
                    guias.Insert(0, primeraguias)
                    'End If
                    Me._ventana.Guias = guias
                    If FacFactura.CodGuia <> "" And FacFactura.CodGuia IsNot Nothing Then
                        Dim guiaaux As New Guia
                        guiaaux.Id = FacFactura.CodGuia
                        Me._ventana.Guia = Me.BuscarGuia(guias, guiaaux)
                    End If


                    Me._ventana.SetLocalidad = Me.BuscarLocalidad(FacFactura.Local)

                    Me._ventana.SetXterrero = BuscarTerrero(FacFactura.Terrero)

                    ' estaas son  campos double
                    Me._ventana.Desc = FacFactura.Descuento
                    Me._ventana.MSubtimpo = FacFactura.MSubtimpo
                    Me._ventana.MDescuento = FacFactura.MDescuento
                    Me._ventana.MTbimp = FacFactura.MTbimp
                    Me._ventana.Mtbexc = FacFactura.Mtbexc
                    Me._ventana.Msubtotal = FacFactura.MSubtotal
                    Me._ventana.Mtimp = FacFactura.Mtimp
                    Me._ventana.Mttotal = FacFactura.Mttotal
                    Me._ventana.MSubtimpoBf = FacFactura.MSubtimpoBf
                    Me._ventana.MDescuentoBf = FacFactura.MDescuentoBf
                    Me._ventana.MTbimpBf = FacFactura.MTbimpBf
                    Me._ventana.MtbexcBf = FacFactura.MTbexcBf
                    Me._ventana.MsubtotalBf = FacFactura.MSubtotalBf
                    Me._ventana.MtimpBf = FacFactura.MTimpBf
                    Me._ventana.MttotalBf = FacFactura.MTtotalBf
                    'Me._ventana.NCantidad = FacFactura.Cantidad
                    Me._ventana.PDescuento = FacFactura.Descuento
                    'FacFactura.Descuento = Me._ventana.PDescuento
                    'Me._ventana.Pu = FacFactura.Pu
                    'Me._ventana.BDetalle = FacFactura.BDetalle
                    Me._ventana.Impuesto = FacFactura.Impuesto
                    Me._ventana.Seleccion = True
                    Dim facimpuestos As List(Of FacImpuesto) = Me._FacImpuestosServicios.ObtenerFacImpuestosFiltro(Nothing)
                    If facimpuestos IsNot Nothing Then
                        Me._ventana.Impuesto = facimpuestos(0).Impuesto
                    Else
                        'Me._ventana.Impuesto = Nothing
                    End If
                    'Me._ventana.PDescuento = 0

                    'esto es para determinar si la pantalla esta en formato de modificar o solo lectura

                    'Me._ventana.AccionRealizar = FacFactura.Accion

                    Me._ventana.FocoPredeterminado()

                    '#Region "trace"
                    If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                        logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                        '#End Region
                    End If
                Else
                    Me.Navegar(Recursos.MensajesConElUsuario.fac_error_tasa_dia, True)
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try

            'If DirectCast(_ventana.FacFactura, FacFactura).Status = -1 Then
            '    Imprimir(-1)
            'End If
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacFactura(_facfactura))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub Ver_Carta()

            If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                If Me._ventana.Carta.id <> Integer.MinValue Then
                    Dim ag As New Mostrar_Detalle_Carta(Me._ventana.Carta)
                    'ag.Owner = Me
                    ag.ShowDialog()
                Else
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If
            Else
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                Exit Sub
            End If
        End Sub

        Public Function BuscarTerrero(ByVal value As String) As String            
            Dim valor As String = ""
            If value = "1" Then
                valor = "F"
            Else
                If value = "2" Then
                    valor = "S-F"
                Else
                    If value = "3" Then
                        valor = "S "
                    End If
                End If
            End If            
            Return (valor)
        End Function

        Public Sub SaldoPendiente(ByVal id As Integer)
            Dim operacionaux As New FacOperacion
            operacionaux.CodigoOperacion = id
            operacionaux.Id = "ND"
            Dim operaciones As List(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
            If operaciones IsNot Nothing Then
                If operaciones.Count > 0 Then
                    Me._ventana.SaldoPendiente = operaciones(0).Saldo
                End If
            End If
        End Sub

        Public Sub buscar_departamento_servicio_esp(ByVal iddetalle As Integer)
            Dim FacFactuDeta As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDeta IsNot Nothing Then
                Dim i As Integer = 0
                While (i < (FacFactuDeta.Count))
                    If (FacFactuDeta.Item(i).Id = iddetalle) Then
                        'MessageBox.Show(FacFactuDetaProformas.Item(i).XDetalleEs, "", MessageBoxButton.OK)

                        Dim ag As New Mostrar_Detalle(FacFactuDeta.Item(i), Me, 1)
                        'ag.Owner = Me
                        ag.ShowDialog()

                        i = FacFactuDeta.Count
                    End If
                    i = i + 1
                End While
            End If
        End Sub

        Public Sub buscar_departamento_servicio_cambiar_español_ingles(ByVal detalle As FacFactuDetalle)
            Dim FacFactuDetalles As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDetalles IsNot Nothing Then
                Dim i As Integer = 0
                While (i < (FacFactuDetalles.Count))
                    If (FacFactuDetalles.Item(i).Id = detalle.Id) Then
                        FacFactuDetalles.Item(i) = detalle
                        Dim guardar As Boolean
                        guardar = _FacFactuDetaServicios.InsertarOModificar(FacFactuDetalles.Item(i), UsuarioLogeado.Hash)

                        i = FacFactuDetalles.Count

                        'FacFactuDetalles.Item(i) = detalle
                        'guarda los cambios en el detalle
                    End If
                    i = i + 1
                End While
            End If
            Me._ventana.ResultadosFacFactuDeta = Nothing
            Me._ventana.ResultadosFacFactuDeta = FacFactuDetalles
        End Sub

        'Public Sub pasar_profora_a_factura(ByVal proforma As FacFacturaProforma)
        '    Mouse.OverrideCursor = Cursors.Wait
        '    Try
        '        '#Region "trace"
        '        If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
        '            logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '        End If
        '        Dim factura As New FacFactura
        '        Dim facturadetalles As New List(Of FacFactuDetalle)
        '        Dim facturaoperaciones As New List(Of FacOperacion)
        '        Dim facturaoperaciondetalle As New List(Of FacOperacionDetalle)
        '        Dim facturaoperaciondetalletm As New List(Of FacOperacionDetalleTm)

        '        ''-->guardar de proforma a factura
        '        'factura.Id = 'Nothing  antes de guardar llamar al buscar el contador de facturas para el numero factura
        '        factura.Anulada = proforma.Anulada
        '        factura.FechaFactura = FormatDateTime(Date.Now, DateFormat.ShortDate)
        '        factura.Asociado = proforma.Asociado
        '        factura.Idioma = proforma.Idioma
        '        factura.Moneda = proforma.Moneda
        '        factura.Caso = proforma.Caso
        '        factura.Inicial = proforma.Inicial
        '        factura.Impuesto = proforma.Impuesto
        '        factura.Descuento = proforma.Descuento
        '        factura.AsociadoImp = proforma.AsociadoImp
        '        factura.InteresadoImp = proforma.InteresadoImp
        '        factura.Terrero = proforma.Terrero
        '        Me._ventana.Xterrero = factura.Terrero
        '        factura.Email = proforma.Email
        '        factura.Seniat = proforma.Seniat
        '        factura.FechaSeniat = proforma.FechaSeniat
        '        factura.PSeniat = proforma.PSeniat
        '        factura.IP = proforma.IP
        '        factura.XAsociado = proforma.XAsociado
        '        factura.Rif = proforma.Rif
        '        factura.XNit = proforma.XNit
        '        factura.DetalleEnvio = proforma.DetalleEnvio
        '        factura.IMulmon = proforma.IMulmon
        '        factura.MonedaImp = proforma.MonedaImp
        '        factura.TasaCambio = proforma.TasaCambio
        '        factura.Codeti = proforma.Codeti
        '        factura.NumeroControl = proforma.NumeroControl()
        '        factura.Local = proforma.Local
        '        factura.Proforma = proforma
        '        factura.Ourref = proforma.Ourref
        '        factura.Instruc = proforma.Instruc
        '        factura.Carta = proforma.Carta
        '        factura.CodigoDepartamento = proforma.CodigoDepartamento
        '        factura.CodGuia = Nothing 'NOTA BUSCAR La Guia proforma.CodGuia
        '        factura.CodigoSocio = proforma.CodigoSocio
        '        factura.MSocio = proforma.MSocio
        '        factura.MCia = proforma.MCia
        '        factura.CondFac = proforma.CondFac
        '        factura.MSubtimpo = proforma.MSubtimpo
        '        factura.MDescuento = proforma.MDescuento
        '        factura.MTbimp = proforma.MTbimp
        '        factura.Mtbexc = proforma.Mtbexc
        '        factura.MSubtotal = proforma.MSubtotal
        '        factura.Mtimp = proforma.Mtimp
        '        factura.Mttotal = proforma.Mttotal
        '        factura.MSubtimpoBf = proforma.MSubtimpoBf
        '        factura.MDescuentoBf = proforma.MDescuentoBf
        '        factura.MTbimpBf = proforma.MTbimpBf
        '        factura.MTbexcBf = proforma.MTbexcBf
        '        factura.MSubtotalBf = proforma.MSubtotalBf
        '        factura.MTimpBf = proforma.MTimpBf
        '        factura.MTtotalBf = proforma.MTtotalBf
        '        factura.XAsociado_O = proforma.XAsociado_O
        '        Me._ventana.FacFactura = factura
        '        If factura.Moneda.Id = "BF" Then
        '            Me._ventana.Xterrero = "1"
        '        End If
        '        factura.Status = 1
        '        ''--> fin guardar de proforma a factura

        '        ''--> para pasar de detalleproforma a detalle
        '        Dim detalleproformaaux As New FacFactuDetaProforma()
        '        Dim detalleproformas As List(Of FacFactuDetaProforma)
        '        detalleproformaaux.Factura = proforma
        '        detalleproformas = Me._FacFactuDetaProformasServicios.ObtenerFacFactuDetaProformasFiltro(detalleproformaaux)
        '        For i As Integer = 0 To detalleproformas.Count - 1
        '            facturadetalles.Add(New FacFactuDetalle)
        '            facturadetalles(i).Id = detalleproformas(i).Id
        '            facturadetalles(i).Factura = Nothing
        '            facturadetalles(i).BDetalle = detalleproformas(i).BDetalle
        '            facturadetalles(i).XDetalle = detalleproformas(i).XDetalle
        '            facturadetalles(i).CServicio = detalleproformas(i).CServicio
        '            facturadetalles(i).Pendiente = detalleproformas(i).Pendiente
        '            facturadetalles(i).Servicio = detalleproformas(i).Servicio
        '            facturadetalles(i).NCantidad = detalleproformas(i).NCantidad
        '            facturadetalles(i).Pu = detalleproformas(i).Pu
        '            facturadetalles(i).Descuento = detalleproformas(i).Descuento
        '            facturadetalles(i).Bsel = detalleproformas(i).Bsel
        '            facturadetalles(i).TipoServicio = detalleproformas(i).TipoServicio
        '            facturadetalles(i).Codigo = detalleproformas(i).Codigo
        '            facturadetalles(i).Iimp = detalleproformas(i).Iimp
        '            facturadetalles(i).XDetalleEs = detalleproformas(i).XDetalleEs
        '            'facturadetalles(i).BDetalleEs = detalleproformas(i).BDetalleEs
        '            facturadetalles(i).Tasa = detalleproformas(i).Tasa
        '            facturadetalles(i).Impuesto = detalleproformas(i).Impuesto
        '            facturadetalles(i).MImpuesto = detalleproformas(i).MImpuesto
        '            facturadetalles(i).MDescuento = detalleproformas(i).MDescuento
        '            facturadetalles(i).BDetalleBf = detalleproformas(i).BDetalleBf
        '            facturadetalles(i).PuBf = detalleproformas(i).PuBf
        '            facturadetalles(i).MImpuestoBf = detalleproformas(i).MImpuestoBf
        '            facturadetalles(i).MDescuentoBf = detalleproformas(i).MDescuentoBf
        '            facturadetalles(i).Desglose = detalleproformas(i).Desglose
        '            'facturadetalles(i).Factura = Nothing
        '        Next
        '        Me._ventana.ResultadosFacFactuDeta = facturadetalles
        '        ''-->fin para pasar de detalleproforma a detalle

        '        ''--> para pasar de operacionproforma a operacion
        '        Dim operacionproformaaux As New FacOperacionProforma
        '        operacionproformaaux.CodigoOperacion = proforma.Id
        '        Dim operacionproformas As List(Of FacOperacionProforma) = _FacOperacionProformasServicios.ObtenerFacOperacionProformasFiltro(operacionproformaaux)
        '        For i As Integer = 0 To operacionproformas.Count - 1
        '            facturaoperaciones.Add(New FacOperacion)
        '            'facturaoperaciones(i) = DirectCast(operacionproformas(i), FacOperacion)
        '            facturaoperaciones(i).Id = operacionproformas(i).Id
        '            facturaoperaciones(i).Asociado = operacionproformas(i).Asociado
        '            facturaoperaciones(i).CodigoOperacion = operacionproformas(i).CodigoOperacion
        '            facturaoperaciones(i).FechaOperacion = operacionproformas(i).FechaOperacion
        '            facturaoperaciones(i).OperacionImp = operacionproformas(i).FechaOperacionImp
        '            facturaoperaciones(i).Moneda = operacionproformas(i).Moneda
        '            facturaoperaciones(i).Idioma = operacionproformas(i).Idioma
        '            facturaoperaciones(i).Monto = operacionproformas(i).Monto
        '            facturaoperaciones(i).MontoBf = operacionproformas(i).MontoBf
        '            facturaoperaciones(i).Saldo = operacionproformas(i).Saldo
        '            facturaoperaciones(i).XOperacion = operacionproformas(i).XOperacion
        '            facturaoperaciones(i).SaldoBf = operacionproformas(i).SaldoBf
        '            facturaoperaciones(i).CodigoOperacion = Nothing
        '        Next
        '        ''--> fin para pasar de operacionproforma a operacion


        '        ''--> para pasar de operaciondetalleproforma a operaciondetalle
        '        Dim operaciondetalleproformaaux As New FacOperacionDetaProforma
        '        operaciondetalleproformaaux.Factura = proforma
        '        Dim operaciondetalleproformas As List(Of FacOperacionDetaProforma) = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(operaciondetalleproformaaux)
        '        For i As Integer = 0 To operaciondetalleproformas.Count - 1
        '            facturaoperaciondetalle.Add(New FacOperacionDetalle)
        '            'facturaoperaciondetalle(i) = DirectCast(operaciondetalleproformas(i), FacOperacionDetalle)
        '            facturaoperaciondetalle(i).Codigo = operaciondetalleproformas(i).Codigo
        '            facturaoperaciondetalle(i).Detalle = operaciondetalleproformas(i).Detalle
        '            facturaoperaciondetalle(i).Factura = Nothing
        '            facturaoperaciondetalle(i).Id = operaciondetalleproformas(i).Id
        '            facturaoperaciondetalle(i).Servicio = operaciondetalleproformas(i).Servicio
        '            'facturaoperaciondetalle(i).Factura = Nothing
        '        Next
        '        ''--> fin para pasar de operaciondetalleproforma a operaciondetalle

        '        ''--> para pasar de operaciondetalletmproforma a operaciondetalletm
        '        Dim operaciondetalletmproformaaux As New FacOperacionDetaTmProforma
        '        operaciondetalletmproformaaux.Factura = proforma
        '        Dim operaciondetalletmproformas As List(Of FacOperacionDetaTmProforma) = _FacOperacionDetaTmProformasServicios.ObtenerFacOperacionDetaTmProformasFiltro(operaciondetalletmproformaaux)
        '        For i As Integer = 0 To operaciondetalletmproformas.Count - 1
        '            facturaoperaciondetalletm.Add(New FacOperacionDetalleTm)
        '            'facturaoperaciondetalletm(i) = DirectCast(operaciondetalletmproformas(i), FacOperacionDetalleTm)
        '            facturaoperaciondetalletm(i).Id = operaciondetalletmproformas(i).Id
        '            facturaoperaciondetalletm(i).Detalle = operaciondetalletmproformas(i).Detalle
        '            facturaoperaciondetalletm(i).Servicio = operaciondetalletmproformas(i).Servicio
        '            facturaoperaciondetalletm(i).Codigo = operaciondetalletmproformas(i).Codigo
        '            facturaoperaciondetalletm(i).Usuario = UsuarioLogeado
        '            'facturaoperaciondetalletm(i).Factura = Nothing
        '        Next
        '        ''--> fin para pasar de operaciondetalletmproforma a operaciondetalletm
        '    Catch ex As Exception
        '        logger.[Error](ex.Message)
        '        Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
        '    Finally
        '        Mouse.OverrideCursor = Nothing
        '    End Try
        'End Sub


        ''' <summary>
        ''' Método que dependiendo del estado de la página, habilita los campos o 
        ''' modifica los datos del usuario
        ''' </summary>
        Public Sub Modificar()
            Mouse.OverrideCursor = Cursors.Wait
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Habilitar campos
                If Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar Then
                    Me._ventana.HabilitarCampos = True
                    Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar
                Else

                    Dim FacFactura As FacFactura = DirectCast(_ventana.FacFactura, FacFactura)

                    'FacFactura.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                    If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                        If Me._ventana.Asociado.id <> Integer.MinValue Then
                            FacFactura.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            If DirectCast(Me._ventana.Interesado, Interesado) Is Nothing Then
                                If DirectCast(Me._ventana.AsociadoImp, Asociado) Is Nothing Then
                                    FacFactura.AsociadoImp = DirectCast(Me._ventana.Asociado, Asociado)
                                Else
                                    If Me._ventana.AsociadoImp.id = Integer.MinValue Then
                                        FacFactura.AsociadoImp = DirectCast(Me._ventana.Asociado, Asociado)
                                    End If
                                End If
                            Else
                                FacFactura.AsociadoImp = Nothing
                            End If
                        End If
                    Else

                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If
                    If DirectCast(Me._ventana.AsociadoImp, Asociado) IsNot Nothing Then
                        If Me._ventana.AsociadoImp.id <> Integer.MinValue Then
                            FacFactura.AsociadoImp = DirectCast(Me._ventana.AsociadoImp, Asociado)
                        End If
                    Else

                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If

                    'FacFactura.AsociadoImp = If(Not DirectCast(Me._ventana.AsociadoImp, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.AsociadoImp, Asociado), Nothing)
                    If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                        If Me._ventana.Carta.id <> Integer.MinValue Then
                            FacFactura.Carta = DirectCast(Me._ventana.Carta, Carta)
                        Else
                            Mouse.OverrideCursor = Nothing
                            Me._ventana.MensajeError = "Debe especificar Carta Orden"
                            Exit Sub
                        End If
                    Else
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Debe especificar Carta Orden"
                        Exit Sub
                    End If

                    If DirectCast(Me._ventana.Interesado, Interesado) IsNot Nothing Then
                        If Me._ventana.Interesado.id <> Integer.MinValue Then
                            FacFactura.InteresadoImp = DirectCast(Me._ventana.Interesado, Interesado)
                        Else
                            FacFactura.InteresadoImp = Nothing
                        End If
                    Else
                        FacFactura.InteresadoImp = Nothing
                    End If

                    'FacFactura.Carta = If(Not DirectCast(Me._ventana.Carta, Carta).Id.Equals("NGN"), DirectCast(Me._ventana.Carta, Carta), Nothing)
                    'FacFactura.InteresadoImp = If(Not DirectCast(Me._ventana.intersado, Interesado).Id.Equals("NGN"), DirectCast(Me._ventana.Interesado, Interesado), Nothing)
                    If DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio) IsNot Nothing Then
                        FacFactura.DetalleEnvio = DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio)
                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If
                    'FacFactura.DetalleEnvio = If(Not DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio).Id.Equals("NGN"), DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio), Nothing)
                    FacFactura.Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    FacFactura.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)
                    FacFactura.MonedaImp = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    'FacFactura.IdiomaImp = If(Not DirectCast(Me._ventana.Idiomaimp, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)

                    If DirectCast(Me._ventana.Guia, Guia).Id <> "" Then
                        FacFactura.CodGuia = DirectCast(Me._ventana.Guia, Guia).Id
                    End If


                    If FacFactura.Caso = "" Or FacFactura.Caso = Nothing Then
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Debe especificar Caso/Referencia"
                        Exit Sub
                    End If

                    If Me._ventana.GetXterrero = "" Then
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Tipo no ha sido seleccionado"
                        Exit Sub
                    End If

                    If Me._ventana.Impuesto = "" Or Me._ventana.Impuesto = Nothing Or Not IsNumeric(Me._ventana.Impuesto) Then
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Debe especificar Impuesto"
                        Exit Sub
                    End If
                    Dim xasociado As String = Me._ventana.XAsociado.ToString
                    FacFactura.XAsociado = xasociado

                    If FacFactura.FechaFactura Is Nothing Then
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Debe especificar Fecha Factura"
                        Exit Sub
                    Else
                        FacFactura.FechaFactura = FormatDateTime(FacFactura.FechaFactura, DateFormat.ShortDate)
                    End If

                    'If FacFactura.FechaSeniat IsNot Nothing Then
                    '    FacFactura.FechaSeniat = FormatDateTime(FacFactura.FechaSeniat, DateFormat.ShortDate)
                    'End If

                    'FacFactura.Auto = "0"
                    FacFactura.Terrero = Me._ventana.GetXterrero
                    FacFactura.Anulada = "NO"
                    FacFactura.Local = Me._ventana.Localidad
                    'FacFactura.BIMulmon = Me._ventana.BIMulmon

                    'FacFactura.Descuento = Me._ventana.Desc
                    FacFactura.MSubtimpo = Me._ventana.MSubtimpo
                    FacFactura.MDescuento = Me._ventana.MDescuento
                    FacFactura.MTbimp = Me._ventana.MTbimp
                    FacFactura.Mtbexc = Me._ventana.Mtbexc
                    FacFactura.MSubtotal = Me._ventana.Msubtotal
                    FacFactura.Mtimp = Me._ventana.Mtimp
                    FacFactura.Mttotal = Me._ventana.Mttotal
                    FacFactura.MSubtimpoBf = Me._ventana.MSubtimpoBf
                    FacFactura.MDescuentoBf = Me._ventana.MDescuentoBf
                    FacFactura.MTbimpBf = Me._ventana.MTbimpBf
                    FacFactura.MTbexcBf = Me._ventana.MtbexcBf
                    FacFactura.MSubtotalBf = Me._ventana.MsubtotalBf
                    FacFactura.MTimpBf = Me._ventana.MtimpBf
                    FacFactura.MTtotalBf = Me._ventana.MttotalBf
                    If Me._ventana.Impuesto <> "" And Me._ventana.Impuesto <> Nothing And IsNumeric(Me._ventana.Impuesto) Then
                        FacFactura.Impuesto = Me._ventana.Impuesto
                    Else
                        FacFactura.Impuesto = 0
                    End If
                    FacFactura.Descuento = Me._ventana.PDescuento
                    FacFactura.CodigoDepartamento = UsuarioLogeado.Departamento.Id
                    If FacFactura.Inicial = "" Or FacFactura.Inicial = Nothing Then
                        FacFactura.Inicial = UsuarioLogeado.Iniciales
                    End If
                    If FacFactura.CodigoDepartamento = "" Or FacFactura.CodigoDepartamento = Nothing Then
                        FacFactura.CodigoDepartamento = UsuarioLogeado.Departamento.Id
                    End If

                    'para internacional
                    If FacFactura.Local = "I" Then
                        internacional(FacFactura.Proforma.Id, FacFactura.Id)
                    End If
                    'fin para internacional

                    'If FacFactura.Status = 1 Then
                    '    '-para los contadores
                    '    If Me._ventana.Xterrero = "1" Or Me._ventana.Xterrero = "2" Then
                    '        Dim valor_contador As Integer
                    '        valor_contador = numero_contador("FAC_FACTURAS_CSENIAT")
                    '        If valor_contador > -1 Then
                    '            FacFactura.Seniat = valor_contador
                    '        End If

                    '        valor_contador = numero_contador("FAC_CONTROL")
                    '        If valor_contador > -1 Then
                    '            FacFactura.NumeroControl = valor_contador
                    '        End If
                    '        FacFactura.Id = FacFactura.Seniat
                    '        FacFactura.PSeniat = FacFactura.Impuesto
                    '        FacFactura.FechaSeniat = FacFactura.FechaFactura
                    '    End If
                    '    If Me._ventana.Xterrero = "2" Or Me._ventana.Xterrero = "3" Then
                    '        Dim valor_contador As Integer
                    '        valor_contador = numero_contador("FAC_FACTURAS_CSTATEMENT")
                    '        If valor_contador > -1 Then
                    '            FacFactura.Id = valor_contador
                    '        End If
                    '    End If
                    '    '- fin para los contadores
                    '    FacFactura.Status = 2
                    'End If

                    If Me._FacFacturaServicios.InsertarOModificar(FacFactura, UsuarioLogeado.Hash) Then

                        'cambiar el estatus de la proforma
                        'FacFactura.Proforma.Auto = "2"
                        'Dim exitoso As Boolean = _FacFacturaProformaServicios.InsertarOModificar(FacFactura.Proforma, UsuarioLogeado.Hash)

                        actualizar_detalle_(FacFactura.Id)
                        operacion_detalle_(FacFactura.Id)
                        operacion_detalle_tm_(FacFactura.Id)

                        Dim operacionaux As New FacOperacion
                        operacionaux.Id = "ND"
                        operacionaux.CodigoOperacion = FacFactura.Id
                        Dim operacion As IList(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
                        If operacion.Count > 0 Then
                            'operacion.Id = "ND"
                            'operacion.CodigoOperacion = FacFactura.Id
                            'operacion.FechaOperacion = FacFactura.FechaFactura
                            operacion(0).Asociado = FacFactura.Asociado
                            operacion(0).Idioma = FacFactura.Idioma
                            operacion(0).Moneda = FacFactura.Moneda
                            operacion(0).Monto = FacFactura.Mttotal
                            operacion(0).Saldo = FacFactura.Mttotal
                            operacion(0).MontoBf = FacFactura.MTtotalBf
                            operacion(0).SaldoBf = FacFactura.MTtotalBf
                            'bsaldo.fac_operaciones_pro = bmonto.fac_operaciones_pro 'NOTA falto este campo
                            operacion(0).XOperacion = constr_xobs(FacFactura.Caso)

                            ' NOTA FALTA 
                            'xoperacion.fac_operaciones_pro = $$xobservacion
                            '              Call CONV_DATE()
                            'foperacion_imp.fac_operaciones_pro = $FECHA$
                            _FacOperacionServicios.InsertarOModificar(operacion(0), UsuarioLogeado.Hash)
                        Else
                            agregar_operacion(FacFactura)
                        End If

                        '
                        '    Proceso de Recalculo por cambio de tasa o cambio de impuesto 
                        '
                        If FacFactura.FechaFactura >= CDate("01-01-2008") Then
                            Recalculo_x_imp_nueva()
                        Else
                            Recalculo_x_imp_nueva()
                        End If

                        'verificar integridad
                        Dim w_ret As Integer = 0
                        lp_verifica(FacFactura, w_ret)
                        If (w_ret <> 0) Then
                            Mouse.OverrideCursor = Nothing
                            MessageBox.Show("Factura No " & FacFactura.Id & " posee un error de integridad, Error " & w_ret & " Notificar a sistemas")
                            'MessageBox.Show("Factura No %%cfactura.fac_facturas posee un error de integridad, Error %%w_ret  Notificar a sistemas")
                            Exit Sub
                        End If
                        If FacFactura.Terrero = "1" Then
                            '  w_lista = "cfactura=%%cfactura.fac_facturastipo=%%xterrero.fac_facturasimp=%%dst.trailermip=1bst=0ifpago=%%ifpago.fac_facturas"
                            ';
                            '  Activate("BT_FACT00_N_BF".EXEC(W_LISTA))
                        End If
                        If FacFactura.Terrero = "2" Then
                            '  w_lista = "cfactura=%%cfactura.fac_facturastipo=%%xterrero.fac_facturasimp=%%dst.trailermip=2bst=0ifpago=%%ifpago.fac_facturas"
                            ';
                            '  Activate("BT_FACT00_N".EXEC(W_LISTA))
                            ';
                            '  w_lista = "cfactura=%%cfactura.fac_facturastipo=%%xterrero.fac_facturasimp=%%dst.trailermip=3bst=0ifpago=%%ifpago.fac_facturas"
                            ';
                            '  Activate("BT_FACT00_N_BF".EXEC(W_LISTA))
                        End If

                        If FacFactura.Terrero = "2" Then
                            '  w_lista = "cfactura=%%cfactura.fac_facturastipo=%%xterrero.fac_facturasimp=%%dst.trailermip=4bst=0ifpago=%%ifpago.fac_facturas"
                            ';
                            '  Activate("BT_FACT00_N".EXEC(W_LISTA))
                        End If

                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFacturaModificado
                        MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacFacturaModificado)
                        Me._ventana.FacFactura = Nothing
                        Me._ventana.FacFactura = FacFactura
                        '    Me.Navegar(_paginaPrincipal)
                        'End If
                    End If
                End If

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

        Public Sub procesar_statement()
            Dim FacFactura As FacFactura = DirectCast(_ventana.FacFactura, FacFactura)
            If FacFactura.Terrero.ToString = "1" Or FacFactura.Terrero.ToString = "2" Then
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Se trata de una F o de una S-F", "Error", MessageBoxButton.OK)
                Exit Sub
            End If
            ' If (bsaldo > 0) Then
            '     message/error "El statement aún no ha sido cobrado"
            '     Return -1
            ' End If

            If FacFactura.FechaSeniat IsNot Nothing Or FacFactura.PSeniat <> 0 Or FacFactura.Seniat IsNot Nothing Then
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("El statement ya fue procesado con Número " & FacFactura.Seniat & " en fecha " & FacFactura.FechaSeniat & " con impuesto " & FacFactura.PSeniat, "Error", MessageBoxButton.OK)
                Exit Sub
            End If

            Dim valor_contador As Integer
            valor_contador = numero_contador("FAC_FACTURAS_CSENIAT")
            If valor_contador > -1 Then
                FacFactura.Seniat = valor_contador
            End If

            valor_contador = numero_contador("FAC_CONTROL")
            If valor_contador > -1 Then
                FacFactura.NumeroControl = valor_contador
            End If

            Dim facimpuestos As List(Of FacImpuesto) = Me._FacImpuestosServicios.ObtenerFacImpuestosFiltro(Nothing)
            FacFactura.PSeniat = facimpuestos(0).Impuesto
            FacFactura.FechaSeniat = FormatDateTime(Date.Now, DateFormat.ShortDate)
            If Me._FacFacturaServicios.InsertarOModificar(FacFactura, UsuarioLogeado.Hash) = True Then
                MessageBox.Show("Proceso Actualizado con exito", "Actualizacion", MessageBoxButton.OK)
            End If
            If FacFactura.FechaFactura >= CDate("01-01-2008") Then
                Recalculo_x_imp_nueva()
            Else
                Recalculo_x_imp_vieja()
            End If
            Imprimir(4)
        End Sub


        Public Sub Imprimir(ByVal tipo As Integer)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Dim FacFactura As FacFactura = DirectCast(Me._ventana.FacFactura, FacFactura)
            FacFactura.Status = tipo

            If tipo = 1 Then
                If _ventana.GetXterrero = "3" Then
                    MessageBox.Show("Se trata de un Statement NO de una Factura", "ERROR", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If

                If FacFactura.Seniat Is Nothing Then
                    If MessageBoxResult.Yes = MessageBox.Show("Posible Error: Se trata de un statement NO procesado ¿desea imprimir la factura de todas maneras?", "PREGUNTA", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                    Else
                        Mouse.OverrideCursor = Nothing
                        Exit Sub
                    End If
                End If            
            End If
            If tipo = 3 Then
                If _ventana.GetXterrero = "1" Then
                    MessageBox.Show("Se trata de una factura NO de un statement", "ERROR", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
            End If

            Me.Navegar(New FacturaRpt((FacFactura)))
            'Me.Navegar(New FacturaRpt2())
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ImprimirCopia(ByVal tipo As Integer)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Dim FacFactura As FacFactura = DirectCast(Me._ventana.FacFactura, FacFactura)
            If tipo = 10 Then
                tipo = 1                
            End If
            FacFactura.Status = tipo

            If tipo = 1 Or tipo = 4 Then
                If _ventana.GetXterrero = "3" Then
                    MessageBox.Show("Se trata de un Statement NO de una Factura", "ERROR", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
                If FacFactura.Seniat Is Nothing Then
                    If MessageBoxResult.Yes = MessageBox.Show("Posible Error: Se trata de un statement NO procesado ¿desea imprimir la factura de todas maneras?", "PREGUNTA", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                    Else
                        Mouse.OverrideCursor = Nothing
                        Exit Sub
                    End If
                End If
            End If

            If tipo = 3 Then
                If _ventana.GetXterrero = "1" Then
                    MessageBox.Show("Se trata de una factura NO de un statement", "ERROR", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
            End If

            Me.Navegar(New FacturaCopiaRpt((FacFactura)))
            'Me.Navegar(New FacturaRpt2())
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ImprimirAnulada()
            Dim FacFactura As FacFactura = DirectCast(Me._ventana.FacFactura, FacFactura)
            FacFactura.Status = 4
            IrConsultarFacFacturaAnuladaReporte(FacFactura)
        End Sub

        Public Sub IrConsultarFacFacturaAnuladaReporte(ByVal factura As FacFactura)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New FacturaCopiaAnuladaRpt(factura))
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub agregar_operacion(ByVal facfactura As FacFactura)
            Dim operacion As New FacOperacion
            operacion.Id = "ND"
            operacion.CodigoOperacion = facfactura.Id
            operacion.FechaOperacion = facfactura.FechaFactura
            operacion.Asociado = facfactura.Asociado
            operacion.Idioma = facfactura.Idioma
            operacion.Moneda = facfactura.Moneda
            operacion.Monto = facfactura.Mttotal
            operacion.Saldo = facfactura.Mttotal
            operacion.MontoBf = facfactura.MTtotalBf
            operacion.SaldoBf = facfactura.MTtotalBf
            'bsaldo.fac_operaciones_pro = bmonto.fac_operaciones_pro 'NOTA falto este campo
            operacion.XOperacion = constr_xobs(facfactura.Caso)

            ' NOTA FALTA 
            'xoperacion.fac_operaciones_pro = $$xobservacion
            '              Call CONV_DATE()
            'foperacion_imp.fac_operaciones_pro = $FECHA$
            _FacOperacionServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash)
        End Sub

        Public Sub internacional(ByVal idproforma As Integer, ByVal idfactura As Integer)
            Dim facinternacionalaux As New FacInternacional
            facinternacionalaux.Id = idproforma
            Dim facinternacionales As List(Of FacInternacional) = Me._FacInternacionalesServicios.ObtenerFacInternacionalesFiltro(facinternacionalaux)
            If facinternacionales.Count = 1 Then
                facinternacionales(0).Factura = idfactura
                Me._FacInternacionalesServicios.InsertarOModificar(facinternacionales(0), UsuarioLogeado.Hash)
                'MessageBox.Show("Proforma No " & Id & " No Posee Registro de Cuentas x Pagar, se suspende esta autorizacion")
                'w_pas = 1
            End If
        End Sub

        Public Function numero_contador(ByVal nombre_contador As String) As Integer
            'para el contador
            Dim contador As New ContadorFac
            contador.Id = nombre_contador
            contador = _FacContadorServicios.ConsultarPorId(contador)

            Dim valor_contador = contador.ProximoValor
            contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
            Dim exitocontador As Boolean = _FacContadorServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
            If exitocontador = True Then
                Return (valor_contador)
            Else
                Return -1
            End If
        End Function

        Public Sub lp_verifica(ByVal proforma As FacFactura, ByRef r_val As Integer)
            'Dim w_calculo_1, w_calculo_2, w_resta As Integer
            r_val = 0

            Dim proformadetalleaux As New FacFactuDetalle
            proformadetalleaux.Factura = proforma
            Dim proformadetalle As List(Of FacFactuDetalle) = Me._FacFactuDetaServicios.ObtenerFacFactuDetallesFiltro(proformadetalleaux)

            If proformadetalle.Count <= 0 Then
                r_val = 1
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If

            Dim operacionproformaaux As New FacOperacion
            operacionproformaaux.CodigoOperacion = proforma.Id
            Dim operacionproforma As List(Of FacOperacion) = Me._FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionproformaaux)
            If operacionproforma.Count <= 0 Then
                r_val = 1
                Mouse.OverrideCursor = Nothing
                Exit Sub

            End If

            Dim operacionproformadetalleaux As New FacOperacionDetalle
            Dim operacionproformadetalle As List(Of FacOperacionDetalle)
            For i As Integer = 0 To proformadetalle.Count - 1
                If proformadetalle(i).Servicio.Itipo = "M" Or proformadetalle(i).Servicio.Itipo = "P" Then
                    'Me._FacOperacionDetaProformasServicios
                    operacionproformadetalleaux.Factura = proforma
                    operacionproformadetalleaux.Detalle = proformadetalle(i).Id
                    operacionproformadetalle = Me._FacOperacionDetaServicios.ObtenerFacOperacionDetallesFiltro(operacionproformadetalleaux)
                    If operacionproformadetalle.Count <= 0 Then
                        r_val = 1
                        Mouse.OverrideCursor = Nothing
                        Exit Sub
                    Else
                        If proformadetalle.Count <> operacionproformadetalle.Count Then ' verificar si el detalle e con el count o con la i
                            r_val = 1
                            Exit Sub
                        End If
                    End If
                End If
            Next

            'Dim tasa As New Tasa()
            'tasa.Id = proforma.FechaFactura
            'tasa.Moneda = proforma.Moneda.Id
            'Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            'If tasas.Count > 0 Then

            '    'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
            '    w_calculo_1 = proforma.MTbimp * tasas(0).Tasabf
            '    'w_calculo_1 = w_calculo_1[round,2]
            '    w_calculo_1 = w_calculo_1
            '    'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
            '    w_calculo_2 = proforma.MTbimpBf
            '    w_resta = w_calculo_1 - w_calculo_2
            '    If w_resta > 0.05 Then
            '        r_val = 2
            '        Exit Sub
            '    End If

            '    'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
            '    w_calculo_1 = proforma.Mtbexc * tasas(0).Tasabf
            '    'w_calculo_1 = w_calculo_1[round,2]
            '    w_calculo_1 = w_calculo_1
            '    'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
            '    w_calculo_2 = proforma.MTbexcBf
            '    w_resta = w_calculo_1 - w_calculo_2
            '    If w_resta > 0.05 Then
            '        r_val = 2
            '        Exit Sub
            '    End If

            '    For i As Integer = 0 To proformadetalle.Count - 1
            '        'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
            '        w_calculo_2 = proformadetalle(i).BDetalle * tasas(0).Tasabf
            '        'w_calculo_1 = w_calculo_1[round,2]
            '        w_calculo_2 = w_calculo_2
            '        'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
            '        w_calculo_1 = proformadetalle(i).BDetalleBf
            '        w_resta = w_calculo_1 - w_calculo_2
            '        If w_resta > 0.05 Then
            '            r_val = 2
            '            Exit Sub
            '        End If
            '    Next
            'End If
        End Sub


        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._FacFacturaServicios.Eliminar(DirectCast(Me._ventana.FacFactura, FacFactura), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFacturaEliminado
                    Me.Navegar(_paginaPrincipal)
                End If

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
            End Try
        End Sub

        Public Function constr_xobs(ByVal caso As String) As String
            Dim observacion As String = Nothing

            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            For i As Integer = 0 To FacFactuDetas.Count - 1
                If observacion = Nothing Then
                    observacion = FacFactuDetas(i).XDetalle
                Else
                    observacion = observacion & "; " & FacFactuDetas(i).XDetalle
                End If
            Next
            observacion = observacion & "; " & caso
            Return observacion
        End Function

        Public Sub BuscarAsociado2()
            Mouse.OverrideCursor = Cursors.Wait
            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing
            Dim i As Boolean = False
            'asociadoaux = Nothing
            'Dim asociadosFiltrados As IEnumerable(Of Asociado) = Me._asociados
            Mouse.OverrideCursor = Cursors.Wait

            If Not String.IsNullOrEmpty(Me._ventana.idAsociadoFiltrar) And Me._ventana.idAsociadoFiltrar <> "0" Then
                asociadoaux.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
                '
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreAsociadoFiltrar) Then
                asociadoaux.Nombre = UCase(Me._ventana.NombreAsociadoFiltrar)
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreAsociadoFiltrar.ToLower())
                i = True
            End If
            If i = True Then
                asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Else
                Me._ventana.Asociados = Nothing
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
                Exit Sub
            End If

            Dim primerasociado As New Asociado()
            primerasociado.Id = Integer.MinValue
            asociados.Insert(0, primerasociado)

            Me._ventana.Asociados = asociados

            Mouse.OverrideCursor = Nothing

            'If asociadosFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Asociados = asociadosFiltrados
            'Else
            '    Me._ventana.Asociados = Me._asociados
            'End If
            Mouse.OverrideCursor = Nothing
        End Sub

        Public Sub BuscarInteresado2()
            Dim Interesadoaux As New Interesado
            Dim Interesados As List(Of Interesado) = Nothing
            Dim i As Boolean = False
            'Dim InteresadosFiltrados As IEnumerable(Of Interesado) = Me._Interesados

            If Not String.IsNullOrEmpty(Me._ventana.idInteresadoFiltrar) Then
                Interesadoaux.Id = Integer.Parse(Me._ventana.idInteresadoFiltrar)
                '    InteresadosFiltrados = From p In InteresadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idInteresadoFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreInteresadoFiltrar) Then
                Interesadoaux.Nombre = UCase(Me._ventana.NombreInteresadoFiltrar)
                '    InteresadosFiltrados = From p In InteresadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreInteresadoFiltrar.ToLower())
                i = True
            End If


            'Me._Interesados = Me._InteresadosServicios.ConsultarInteresadoConTodo(Interesadoaux)
            If i = True Then
                Interesados = Me._InteresadosServicios.ObtenerInteresadosFiltro(Interesadoaux)
            Else
                Me._ventana.Interesados = Nothing
                MessageBox.Show("Error: No Existe Interesado Relacionado a la Búsqueda")
            End If
            Dim primerinteresado As New Interesado()
            primerinteresado.Id = Integer.MinValue
            Interesados.Insert(0, primerinteresado)

            Me._ventana.Interesados = Interesados

            'If InteresadosFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Interesados = InteresadosFiltrados
            'Else
            '    Me._ventana.Interesados = Me._Interesados
            'End If
        End Sub

        Public Sub BuscarAsociadoImp()

            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing
            Dim i As Boolean = False
            'Dim asociadosFiltrados As IEnumerable(Of Asociado) = Me._asociados

            If Not String.IsNullOrEmpty(Me._ventana.idAsociadoFiltrarImp) And Me._ventana.idAsociadoFiltrarImp <> "0" Then
                asociadoaux.Id = Integer.Parse(Me._ventana.idAsociadoFiltrarImp)
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreAsociadoFiltrarImp) Then
                asociadoaux.Nombre = UCase(Me._ventana.NombreAsociadoFiltrarImp)
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreAsociadoFiltrar.ToLower())
                i = True
            End If


            'Me._asociados = Me._asociadosServicios.ConsultarAsociadoConTodo(asociadoaux)
            If asociadoaux IsNot Nothing Then
                asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Else
                Me._ventana.AsociadosImp = Nothing
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
                Exit Sub
            End If

            Dim primerasociado As New Asociado()
            primerasociado.Id = Integer.MinValue
            asociados.Insert(0, primerasociado)

            Me._ventana.AsociadosImp = asociados
        End Sub

        Public Sub BuscarCarta()
            Dim cartas As List(Of Carta) = Nothing
            If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                Dim cartaaux As New Carta

                'Dim CartasFiltrados As IEnumerable(Of Carta) = Me._Cartas

                If Not String.IsNullOrEmpty(Me._ventana.idCartaFiltrar) Then
                    cartaaux.Id = Integer.Parse(Me._ventana.idCartaFiltrar)
                    'CartasFiltrados = From p In CartasFiltrados Where p.Id = Integer.Parse(Me._ventana.idCartaFiltrar)
                End If

                'If Not String.IsNullOrEmpty(Me._ventana.NombreCartaFiltrar) Then
                '    cartaaux.Medio = UCase(Me._ventana.NombreCartaFiltrar)
                '    'CartasFiltrados = From p In CartasFiltrados Where p.Medio IsNot Nothing AndAlso p.Medio.ToLower().Contains(Me._ventana.NombreCartaFiltrar.ToLower())
                'End If

                If Not String.IsNullOrEmpty(Me._ventana.FechaCartaFiltrar) Then
                    cartaaux.Fecha = Me._ventana.FechaCartaFiltrar
                End If

                cartaaux.Asociado = DirectCast(Me._ventana.Asociado, Asociado)

                cartas = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
            Else
                Me._ventana.Cartas = Nothing
                MessageBox.Show("Error: No Existe Carta Relacionado a la Búsqueda")
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If

            Dim primercarta As New Carta()
            primercarta.Id = Integer.MinValue
            cartas.Insert(0, primercarta)

            Me._ventana.Cartas = cartas
            'If CartasFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Cartas = CartasFiltrados
            'Else
            '    Me._ventana.Cartas = Me._Cartas
            'End If
        End Sub

        Public Sub BuscarMarca()
            Dim Marcas As IList(Of Marca)
            Dim marca As New Marca
            If Me._ventana.idMarcaFiltrar <> "" Then
                If (IsNumeric(Me._ventana.idMarcaFiltrar)) Then
                    marca.Id = Me._ventana.idMarcaFiltrar
                End If
            End If
            marca.Descripcion = UCase(Me._ventana.NombreMarcaFiltrar)
            '876228
            'If marca IsNot Nothing Then
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marca)
            Me._ventana.ResultadosMarca = Marcas
            'Else
            '    Me._ventana.ResultadosMarca = Nothing
            'End If
            Me._ventana.MensajeError = ""

        End Sub

        Public Function BuscarMarca_2(ByVal id As String) As Marca
            Dim Marcas As List(Of Marca)
            Dim marcaaux As New Marca
            If id <> "" Then
                If (IsNumeric(id)) Then
                    marcaaux.Id = id
                End If
            End If
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marcaaux)
            Return Marcas(0)
        End Function

        Public Function BuscarPatente_2(ByVal id As String) As Patente
            Dim Patentes As List(Of Patente)
            Dim Patenteaux As New Patente
            If id <> "" Then
                If (IsNumeric(id)) Then
                    Patenteaux.Id = id
                End If
            End If
            Patentes = Me._PatentesServicios.ObtenerPatentesFiltro(Patenteaux)
            Return Patentes(0)
        End Function

        Public Function encontrarmarca(ByVal idmarca As String) As IList(Of Marca)
            Dim Marcas As IList(Of Marca)
            Dim marca As New Marca
            If idmarca <> "" Then
                If IsNumeric(idmarca) Then
                    marca.Id = idmarca
                End If
            End If
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marca)
            Return Marcas
        End Function

        Public Sub BuscarPatente()
            Dim Patentes As IList(Of Patente)
            Dim Patente As New Patente
            If Me._ventana.idPatenteFiltrar <> "" Then
                If (IsNumeric(Me._ventana.idPatenteFiltrar)) Then
                    Patente.Id = Me._ventana.idPatenteFiltrar
                End If
            End If
            Patente.Descripcion = UCase(Me._ventana.NombrePatenteFiltrar)
            '876228

            Patentes = Me._PatentesServicios.ObtenerPatentesFiltro(Patente)

            Me._ventana.ResultadosPatente = Patentes
            Me._ventana.MensajeError = ""
        End Sub

        Public Function encontrarPatente(ByVal idPatente As String) As IList(Of Patente)
            Dim Patentes As IList(Of Patente)
            Dim Patente As New Patente
            If idPatente <> "" Then
                If IsNumeric(idPatente) Then
                    Patente.Id = idPatente
                End If
            End If
            Patentes = Me._PatentesServicios.ObtenerPatentesFiltro(Patente)
            Return Patentes
        End Function

        Public Sub VerDesgloseServicios()
            'If DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio) IsNot Nothing) Then '
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            If departamento_servicio IsNot Nothing Then
                Dim _FacDesgloseServicios As IList(Of FacDesgloseServicio)
                Dim DesgloseServiciosaux As New FacDesgloseServicio

                DesgloseServiciosaux.Servicio = departamento_servicio.Servicio
                _FacDesgloseServicios = Me._desgloseserviciosServicios.ObtenerFacDesgloseServiciosFiltro(DesgloseServiciosaux)

                'Dim DesgloseServiciosFiltrados As IEnumerable(Of FacDesgloseServicio) = _FacDesgloseServicios

                'If departamento_servicio.Servicio IsNot Nothing AndAlso Not DirectCast(departamento_servicio.Servicio, FacServicio).Id.Equals("NGN") Then
                '    DesgloseServiciosFiltrados = From a In DesgloseServiciosFiltrados Where a.Servicio IsNot Nothing AndAlso a.Servicio.Id.Contains(DirectCast(departamento_servicio.Servicio, FacServicio).Id)
                'End If


                Me._ventana.ResultadosDesgloseServicio2 = _FacDesgloseServicios
                Me._ventana.MensajeError = ""
                'End If
            Else
                Me._ventana.ResultadosDesgloseServicio2 = Nothing
            End If
        End Sub

        Public Sub VerDepartamentoServicios()
            If Me._ventana.Moneda IsNot Nothing Then
                Dim _FacDepartamentoServicios As IList(Of FacDepartamentoServicio)
                Dim departamentoservicioauxiliar As New FacDepartamentoServicio
                departamentoservicioauxiliar.Id = UsuarioLogeado.Departamento
                Dim servicioaux As New FacServicio
                'servicioaux = Nothing

                If Me._ventana.ServicioId <> "" Then
                    servicioaux.Id = UCase(Me._ventana.ServicioId)
                End If
                If Me._ventana.ServicioCod_Cont <> "" Then
                    servicioaux.Cod_Cont = UCase(Me._ventana.ServicioCod_Cont)
                End If
                If Me._ventana.ServicioXreferencia <> "" Then
                    servicioaux.Xreferencia = UCase(Me._ventana.ServicioXreferencia)
                End If
                If Not Me._ventana.Tipo.Equals(" "c) Then
                    servicioaux.Itipo = Me._ventana.Tipo
                Else
                    servicioaux.Itipo = " "
                End If
                If Not Me._ventana.Localidad2.Equals(" "c) Then
                    servicioaux.Local = Me._ventana.Localidad2
                Else
                    servicioaux.Local = " "
                End If

                departamentoservicioauxiliar.Servicio = servicioaux
                _FacDepartamentoServicios = Me._DepartamentoserviciosServicios.ObtenerFacDepartamentoServiciosFiltro(departamentoservicioauxiliar)

                Me._ventana.ResultadosDepartamentoServicio2 = _FacDepartamentoServicios
                Me._ventana.MensajeError = ""
            Else
                Me._ventana.VerTipo = "13"
                MessageBox.Show("Seleccione Asociado")
            End If
        End Sub

        Public Sub VerTipoMarcaPatente()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)

            ' aqui dependeiendo de la condicion tengo que regresar a la pantalla para que puedan seleccionar la marca o patente si aplica
            If (departamento_servicio.Servicio.BItidoc = True) Then
                If (departamento_servicio.Servicio.Itipo = "M") Then
                    Me._ventana.VerTipo = "1" ' para marca
                Else
                    If departamento_servicio.Servicio.Itipo = "P" Then
                        Me._ventana.VerTipo = "2" ' para patente
                    End If
                End If
            Else
                If (departamento_servicio.Servicio.Itipo = "C") Then
                    Me._ventana.VerTipo = "3" ' Cantidad                    
                Else
                    VerTipoTraduccion()
                End If
            End If

        End Sub

        Public Sub VerTipoTraduccion()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            If (departamento_servicio.Servicio.BItraduc = True) Then
                Me._ventana.VerTipo = "4" ' Traduccion  
            Else
                VerTipoRecurso()
            End If
        End Sub

        Public Sub VerTipoRecurso()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            If (departamento_servicio.Servicio.BRecursos = True) Then
                Me._ventana.VerTipo = "5" ' Recurso
            Else
                VerTipoMaterial()
            End If
        End Sub

        Public Sub VerTipoMaterial()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            If (departamento_servicio.Servicio.BMaterial = True) Then
                Me._ventana.VerTipo = "6" ' Material
            Else
                VerTipoMultiplesMarcas()
            End If
        End Sub

        Public Sub VerTipoMultiplesMarcas()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            'Dim FacFactuDeta As FacFactuDeta = DirectCast(Me._ventana.FacFactuDeta_2Seleccionado, FacFactuDeta)
            If (departamento_servicio.Servicio.Itipo = "M") Then
                If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                    Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                    If imult.Trim = "T" Or imult = "1" Then
                        Me._ventana.VerTipo = "7" ' Multiples Marcas
                    Else
                        Me._ventana.VerTipo = "13"
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Este Servicios no Acepta Multiregistro"
                        Exit Sub
                    End If
                Else
                    Me._ventana.VerTipo = "8" ' Marcas
                End If

            Else
                'Me._ventana.VerTipo = "9" ' Multiples Patentes
                VerTipoMultiplesPatentes()
            End If
        End Sub

        Public Sub VerTipoMultiplesPatentes()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            'Dim FacFactuDeta As FacFactuDeta = DirectCast(Me._ventana.FacFactuDeta_2Seleccionado, FacFactuDeta)
            If (departamento_servicio.Servicio.Itipo = "P") Then
                If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                    Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                    If imult.Trim = "T" Or imult = "1" Then
                        Me._ventana.VerTipo = "9" ' Multiples Patentes
                    Else
                        Me._ventana.VerTipo = "13"
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeError = "Este Servicios no Acepta Multiregistro"
                        Exit Sub
                    End If
                Else
                    Me._ventana.VerTipo = "10" ' Patentes
                End If

            Else
                VerTipoAnualidad()
            End If
        End Sub

        Public Sub VerTipoAnualidad()
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            If (departamento_servicio.Servicio.BAnual = True) Then
                Me._ventana.VerTipo = "11" ' Anualidad  
            Else
                VerTipoDesgloseServicio()
            End If
        End Sub

        Public Sub VerTipoDesgloseServicio()

            If (Me._ventana.Desglose = True) Then
                Me._ventana.VerTipo = "12" ' degloseServicio
            Else
                AgregarDetalle()
            End If
        End Sub

        Public Sub VerDocumentoMarcas()
            Dim _DocumentosMarcas As IList(Of DocumentosMarca)
            _DocumentosMarcas = Me._DocumentosMarcasServicios.ConsultarTodos

            Me._ventana.ResultadosDocumentosMarca = _DocumentosMarcas
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub VerDocumentoPatentes()
            Dim _DocumentosPatentes As IList(Of DocumentosPatente)
            _DocumentosPatentes = Me._DocumentosPatentesServicios.ConsultarTodos

            Me._ventana.ResultadosDocumentosPatente = _DocumentosPatentes
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub VerDocumentoTraducciones()
            Dim _DocumentosTraducciones As IList(Of DocumentosTraduccion)
            _DocumentosTraducciones = Me._DocumentosTraduccionesServicios.ConsultarTodos

            Me._ventana.ResultadosDocumentosTraduccion = _DocumentosTraducciones
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub VerFacRecursos()
            Dim _FacRecursos As IList(Of FacRecurso)
            _FacRecursos = Me._FacRecursosServicios.ConsultarTodos

            Me._ventana.ResultadosRecurso = _FacRecursos
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub VerMateriales()
            Dim Materiales As IList(Of Material)
            Materiales = Me._MaterialesServicios.ConsultarTodos

            Me._ventana.ResultadosMaterial = Materiales
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub VerAnualidades()
            Dim Anualidades As IList(Of FacAnualidad)
            Anualidades = Me._FacAnualidadesServicios.ConsultarTodos

            Me._ventana.ResultadosAnualidad = Anualidades
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub AgregarDetalle()
            Mouse.OverrideCursor = Cursors.Wait
            'para el contador
            Dim contador As New FacContadorPro
            contador.Id = "FAC_DETALLES_PRO"
            contador = _FacContadorProServicios.ConsultarPorId(contador)

            Dim valor_contador = contador.ProximoValor
            contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
            Dim exitocontador As Boolean = _FacContadorProServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)

            If exitocontador = True Then
                'agrega los valores a FacFactuDeta
                InsertarDetalle(valor_contador)
            End If
            'fin contador  
            Mouse.OverrideCursor = Nothing
        End Sub

        Public Sub SeleccionarDetalle()
            If DirectCast(Me._ventana.FacFactuDeta_Seleccionado, FacFactuDetalle) IsNot Nothing Then
                Dim detalle_ As FacFactuDetalle = DirectCast(Me._ventana.FacFactuDeta_Seleccionado, FacFactuDetalle)
                'Dim desglose_servicio As FacDesgloseServicio = DirectCast(Me._ventana.DesgloseServicio_Seleccionado, FacDesgloseServicio)
                'Dim servicio As New FacServicio
                If detalle_ IsNot Nothing Then

                    'Dim _FacDepartamentoServicios As IList(Of FacDepartamentoServicio)
                    'Dim departamentoservicioauxiliar As New FacDepartamentoServicio
                    'departamentoservicioauxiliar.Id = UsuarioLogeado.Departamento
                    'servicio.Id = detalle_.Servicio.Id
                    'departamentoservicioauxiliar.Servicio = servicio
                    '_FacDepartamentoServicios = Me._DepartamentoserviciosServicios.ObtenerFacDepartamentoServiciosFiltro(departamentoservicioauxiliar)
                    'If _FacDepartamentoServicios(0).Servicio.Imodpr = "N" Then
                    Me._ventana.Activar_Desactivar = True
                    If detalle_.Servicio.Imodpr = "F" Or detalle_.Servicio.Imodpr = Nothing Then
                        Me._ventana.Desactivar_Precio = False
                        'Else
                        '    Me._ventana.Desactivar_Precio = True
                    End If
                    If detalle_.Servicio.Aimpuesto = "F" Or detalle_.Servicio.Aimpuesto = Nothing Then
                        detalle_.Descuento = 0
                        Me._ventana.Desactivar_Descuento = False
                    End If

                    ' esto tengo que verificarlo con el desglose servicio
                    'If desglose_servicio.Id = "G" Then
                    '    detalle_.Impuesto = "F"
                    '    detalle_.Descuento = 0
                    '    Me._ventana.Desactivar_Descuento = False
                    'End If

                    If detalle_.NCantidad.ToString <> "" And detalle_.NCantidad.ToString <> Nothing Then
                        Me._ventana.NCantidad = detalle_.NCantidad
                    Else
                        Me._ventana.NCantidad = 0
                    End If
                    If detalle_.Pu.ToString <> "" And detalle_.Pu.ToString <> Nothing Then
                        Me._ventana.Pu = detalle_.Pu
                    Else
                        Me._ventana.Pu = 0
                    End If
                    If detalle_.Descuento.ToString <> "" And detalle_.Descuento.ToString <> Nothing Then
                        Me._ventana.Descuento = detalle_.Descuento
                    Else
                        Me._ventana.Descuento = 0
                    End If
                    If detalle_.BDetalle.ToString <> "" And detalle_.BDetalle.ToString <> Nothing Then
                        Me._ventana.BDetalle = detalle_.BDetalle
                    Else
                        Me._ventana.BDetalle = 0
                    End If
                End If
            End If
        End Sub

        Public Sub focus()
            If DirectCast(Me._ventana.FacFactuDeta_Seleccionado, FacFactuDetalle) IsNot Nothing Then
                Dim detalle_ As FacFactuDetalle = DirectCast(Me._ventana.FacFactuDeta_Seleccionado, FacFactuDetalle)

                If Not IsNumeric(Me._ventana.Pu) Then
                    Me._ventana.Pu = 0
                End If
                If Not IsNumeric(Me._ventana.NCantidad) Then
                    Me._ventana.NCantidad = 0
                End If
                If Not IsNumeric(Me._ventana.Descuento) Then
                    Me._ventana.Descuento = 0
                End If
                If Not IsNumeric(Me._ventana.BDetalle) Then
                    Me._ventana.BDetalle = 0
                End If

                Me._ventana.BDetalle = Me._ventana.Pu * Me._ventana.NCantidad
                detalle_.BDetalle = Me._ventana.BDetalle
                detalle_.Pu = Me._ventana.Pu
                detalle_.Descuento = Me._ventana.Descuento
                Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                Dim idmoneda As String
                If moneda IsNot Nothing Then
                    idmoneda = moneda.Id
                Else
                    idmoneda = ""
                End If
                If Me._ventana.Pu <> 0 Then
                    If moneda IsNot Nothing Then
                        If moneda.Id = "BF" Then
                            detalle_.Pu = Me._ventana.Pu
                            detalle_.PuBf = Me._ventana.Pu
                        Else
                            If moneda.Id = "US" Then
                                Dim tasa As Tasa = buscar_tasa(Date.Now, "")
                                If tasa IsNot Nothing Then
                                    detalle_.PuBf = Me._ventana.Pu * tasa.Tasabf
                                    detalle_.PuBf = detalle_.PuBf
                                End If
                            End If

                        End If
                    End If
                End If
                detalle_.BDetalleBf = detalle_.PuBf * Me._ventana.NCantidad
                detalle_.NCantidad = Me._ventana.NCantidad
                'Dim guardar As Boolean = _FacFactuDetasServicios.InsertarOModificar(detalle_, UsuarioLogeado.Hash)
                If moneda IsNot Nothing Then
                    recalcular(moneda.Id)
                Else

                    recalcular("")
                End If
            End If
        End Sub

        Public Sub focus2()
            Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
            If moneda IsNot Nothing Then
                recalcular(moneda.Id)
            Else
                recalcular("")
            End If
        End Sub

        Public Sub ModificarGridFacFactuDeta(ByVal detalle_ As FacFactuDetalle)
            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))

            Dim i As Integer = 0
            Dim ver As Boolean = True
            While ((i <= FacFactuDetas.Count - 1) And (ver = True))

                If (FacFactuDetas(i).Id = detalle_.Id) Then
                    FacFactuDetas(i).Pu = detalle_.Pu
                    FacFactuDetas(i).PuBf = detalle_.PuBf
                    FacFactuDetas(i).NCantidad = detalle_.NCantidad
                    FacFactuDetas(i).BDetalle = detalle_.BDetalle
                    FacFactuDetas(i).BDetalleBf = detalle_.BDetalleBf
                    FacFactuDetas(i).Descuento = detalle_.Descuento
                    ver = False
                End If
                i = i + 1
            End While
            'For i As Integer = 0 To FacFactuDetas.Count - 1
            '    If (FacFactuDetas.Item(i).Id = detalle_.Id) Then
            '        FacFactuDetas.Item(i).Pu = detalle_.Pu
            '        FacFactuDetas.Item(i).PuBf = detalle_.PuBf
            '        FacFactuDetas.Item(i).NCantidad = detalle_.NCantidad
            '        FacFactuDetas.Item(i).BDetalle = detalle_.BDetalle
            '        FacFactuDetas.Item(i).BDetalleBf = detalle_.BDetalleBf
            '        FacFactuDetas.Item(i).Descuento = detalle_.Descuento
            '    End If
            'Next

            Me._ventana.ResultadosFacFactuDeta = Nothing
            Me._ventana.ResultadosFacFactuDeta = FacFactuDetas
            Me._ventana.MensajeError = ""
        End Sub


        Public Sub ElimDepartamentoServicios()
            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDetas IsNot Nothing Then
                Dim FacFactuDetasaux As New List(Of FacFactuDetalle)
                Dim j As Integer = 0
                For i As Integer = 0 To FacFactuDetas.Count - 1
                    If (FacFactuDetas.Item(i).Seleccion = False) Then

                        FacFactuDetasaux.Add(New FacFactuDetalle)
                        FacFactuDetasaux(j) = FacFactuDetas.Item(i)
                        j = j + 1

                    Else
                        'aqui va lo de eliminar los operacion detale prof
                    End If
                Next

                Me._ventana.ResultadosFacFactuDeta = Nothing
                Me._ventana.ResultadosFacFactuDeta = FacFactuDetasaux

                Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                If moneda IsNot Nothing Then
                    recalcular(moneda.Id)
                Else
                    recalcular("")
                End If
                Me._ventana.MensajeError = ""
            End If
        End Sub

        Public Sub recalcular(ByVal moneda As String)
            Me._ventana.MSubtimpo = 0
            Me._ventana.MDescuento = 0
            Me._ventana.MTbimp = 0
            Me._ventana.Mtbexc = 0
            Me._ventana.Msubtotal = 0
            Me._ventana.Mtimp = 0
            Me._ventana.Mttotal = 0
            Me._ventana.MSubtimpoBf = 0
            Me._ventana.MDescuentoBf = 0
            Me._ventana.MTbimpBf = 0
            Me._ventana.MtbexcBf = 0
            Me._ventana.MsubtotalBf = 0
            Me._ventana.MtimpBf = 0
            Me._ventana.MttotalBf = 0

            Dim w_monto, w_monto_bf As Integer


            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDetas IsNot Nothing Then
                Dim tasa As Tasa = buscar_tasa(CDate(Me._ventana.FechaFactura), moneda)
                'Dim guardar As Boolean
                For i As Integer = 0 To FacFactuDetas.Count - 1
                    If moneda = "US" Then
                        If tasa IsNot Nothing Then
                            FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle * tasa.Tasabf
                            FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu * tasa.Tasabf
                        End If
                    Else
                        FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle
                        FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu
                    End If

                    'guarda los cambios en el detalle
                    'guardar = _FacFactuDetasServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)

                    w_monto = FacFactuDetas(i).BDetalle
                    w_monto_bf = FacFactuDetas(i).BDetalleBf

                    If FacFactuDetas(i).Impuesto = "T" Or FacFactuDetas(i).Impuesto = "1" Then
                        Me._ventana.MSubtimpo = Me._ventana.MSubtimpo + w_monto
                        Me._ventana.MSubtimpoBf = Me._ventana.MSubtimpoBf + w_monto_bf
                    Else
                        Me._ventana.Mtbexc = Me._ventana.Mtbexc + w_monto
                        Me._ventana.MtbexcBf = Me._ventana.MtbexcBf + w_monto_bf
                    End If
                    Me._ventana.MDescuento = Me._ventana.MDescuento + ((FacFactuDetas(i).Pu * FacFactuDetas(i).NCantidad) * FacFactuDetas(i).Descuento) / 100
                    Me._ventana.MDescuento = Me._ventana.MDescuento

                    Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf + ((FacFactuDetas(i).PuBf * FacFactuDetas(i).NCantidad) * FacFactuDetas(i).Descuento) / 100
                    Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf
                Next

                Me._ventana.MTbimp = Me._ventana.MSubtimpo - Me._ventana.MDescuento
                Me._ventana.MTbimpBf = Me._ventana.MSubtimpoBf - Me._ventana.MDescuentoBf

                Me._ventana.Msubtotal = Me._ventana.MTbimp + Me._ventana.Mtbexc
                Me._ventana.MsubtotalBf = Me._ventana.MTbimpBf + Me._ventana.MtbexcBf
                If Me._ventana.Impuesto = "" Or Me._ventana.Impuesto = Nothing Or Not IsNumeric(Me._ventana.Impuesto) Then
                    Me._ventana.Mtimp = Me._ventana.MTbimp * (0 / 100)
                    Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (0 / 100)
                Else
                    Me._ventana.Mtimp = Me._ventana.MTbimp * (Me._ventana.Impuesto / 100)
                    Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (Me._ventana.Impuesto / 100)
                End If
                Me._ventana.Mttotal = Me._ventana.Msubtotal + Me._ventana.Mtimp
                Me._ventana.MttotalBf = Me._ventana.MsubtotalBf + Me._ventana.MtimpBf

                Me._ventana.Mtimp = Me._ventana.Mtimp
                Me._ventana.MtimpBf = Me._ventana.MtimpBf

                Me._ventana.Mttotal = Me._ventana.Mttotal
                Me._ventana.MttotalBf = Me._ventana.MttotalBf

                Me._ventana.ResultadosFacFactuDeta = Nothing
                Me._ventana.ResultadosFacFactuDeta = FacFactuDetas
                Me._ventana.Activar_Desactivar = False
                Me._ventana.NCantidad = 0
                Me._ventana.Pu = 0
                Me._ventana.BDetalle = 0
                Me._ventana.Descuento = 0
            End If
        End Sub

        Public Sub Recalculo_x_imp_vieja()
            Me._ventana.MSubtimpo = 0
            Me._ventana.MDescuento = 0
            Me._ventana.MTbimp = 0
            Me._ventana.Mtbexc = 0
            Me._ventana.Msubtotal = 0
            Me._ventana.Mtimp = 0
            Me._ventana.Mttotal = 0
            Me._ventana.MSubtimpoBf = 0
            Me._ventana.MDescuentoBf = 0
            Me._ventana.MTbimpBf = 0
            Me._ventana.MtbexcBf = 0
            Me._ventana.MsubtotalBf = 0
            Me._ventana.MtimpBf = 0
            Me._ventana.MttotalBf = 0

            Dim w_monto, w_monto_bf As Integer
            Dim factura As FacFactura = Me._ventana.FacFactura
            Dim fecha As DateTime
            Dim w_impos As Integer = 0
            Dim w_impu As Double? = Nothing
            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDetas IsNot Nothing Then
                'Dim guardar As Boolean
                For i As Integer = 0 To FacFactuDetas.Count - 1

                    If factura.Moneda.Id = "US" Then

                        If factura.FechaSeniat IsNot Nothing Then
                            fecha = FormatDateTime(factura.FechaSeniat, DateFormat.ShortDate)
                        Else
                            fecha = FormatDateTime(factura.FechaFactura, DateFormat.ShortDate)
                        End If
                        Dim tasa As Tasa = buscar_tasa(fecha, "US")
                        If tasa IsNot Nothing Then
                            FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle * tasa.Tasabf
                            FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu * tasa.Tasabf
                        End If
                    Else
                        FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle
                        FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu
                    End If

                    'guarda los cambios en el detalle
                    'guardar = _FacFactuDetasServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)
                    Me._FacFactuDetaServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)

                    w_monto = FacFactuDetas(i).BDetalle
                    w_monto_bf = FacFactuDetas(i).BDetalleBf

                    If FacFactuDetas(i).Impuesto = "T" Or FacFactuDetas(i).Impuesto = "1" Then
                        Me._ventana.MSubtimpo = Me._ventana.MSubtimpo + w_monto
                        Me._ventana.MSubtimpoBf = Me._ventana.MSubtimpoBf + w_monto_bf
                    Else
                        Me._ventana.Mtbexc = Me._ventana.Mtbexc + w_monto
                        Me._ventana.MtbexcBf = Me._ventana.MtbexcBf + w_monto_bf
                    End If
                Next
                If factura.Descuento <> 0 Then

                    Me._ventana.MDescuento = (factura.MSubtimpo * factura.Descuento) / 100
                    Me._ventana.MDescuento = Me._ventana.MDescuento

                    Me._ventana.MDescuentoBf = (factura.MSubtimpoBf * factura.Descuento) / 100
                    Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf

                    Me._ventana.MTbimp = Me._ventana.MSubtimpo - factura.Descuento
                    Me._ventana.MTbimpBf = Me._ventana.MSubtimpoBf - Me._ventana.MDescuentoBf

                    Me._ventana.Msubtotal = Me._ventana.MTbimp + Me._ventana.Mtbexc
                    Me._ventana.MsubtotalBf = Me._ventana.MTbimpBf + Me._ventana.MtbexcBf

                    Me._ventana.Mtimp = Me._ventana.MTbimp * (factura.Impuesto / 100)
                    'Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (Me._ventana.Impuesto / 100)
                    If factura.PSeniat <> 0 Then
                        w_impu = factura.PSeniat
                    Else
                        w_impu = factura.Impuesto
                    End If
                    Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (w_impu / 100)

                    Me._ventana.Mttotal = Me._ventana.Msubtotal + Me._ventana.Mtimp
                    Me._ventana.MttotalBf = Me._ventana.MsubtotalBf + Me._ventana.MtimpBf

                    Me._ventana.Mtimp = Me._ventana.Mtimp
                    Me._ventana.MtimpBf = Me._ventana.MtimpBf

                    Me._ventana.Mttotal = Me._ventana.Mttotal
                    Me._ventana.MttotalBf = Me._ventana.MttotalBf

                    'Me._ventana.ResultadosFacFactuDeta = Nothing
                    'Me._ventana.ResultadosFacFactuDeta = FacFactuDetas
                    'Me._ventana.Activar_Desactivar = False
                    'Me._ventana.NCantidad = 0
                    'Me._ventana.Pu = 0
                    'Me._ventana.BDetalle = 0
                    'Me._ventana.Descuento = 0
                Else
                    'Me._ventana.MDescuento = (factura.MSubtimpo * factura.Descuento) / 100
                    'Me._ventana.MDescuento = Me._ventana.MDescuento

                    'Me._ventana.MDescuentoBf = (factura.MSubtimpoBf * factura.Descuento) / 100
                    'Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf

                    Me._ventana.MTbimp = Me._ventana.MSubtimpo - factura.Descuento
                    Me._ventana.MTbimpBf = Me._ventana.MSubtimpoBf - Me._ventana.MDescuentoBf

                    Me._ventana.Msubtotal = Me._ventana.MTbimp + Me._ventana.Mtbexc
                    Me._ventana.MsubtotalBf = Me._ventana.MTbimpBf + Me._ventana.MtbexcBf

                    Me._ventana.Mtimp = Me._ventana.MTbimp * (factura.Impuesto / 100)
                    'Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (Me._ventana.Impuesto / 100)
                    If factura.PSeniat <> 0 Then
                        w_impu = factura.PSeniat
                    Else
                        w_impu = factura.Impuesto
                    End If
                    Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (w_impu / 100)

                    Me._ventana.Mttotal = Me._ventana.Msubtotal + Me._ventana.Mtimp
                    Me._ventana.MttotalBf = Me._ventana.MsubtotalBf + Me._ventana.MtimpBf

                    Me._ventana.Mtimp = Me._ventana.Mtimp
                    Me._ventana.MtimpBf = Me._ventana.MtimpBf

                    Me._ventana.Mttotal = Me._ventana.Mttotal
                    Me._ventana.MttotalBf = Me._ventana.MttotalBf
                End If 'descuento

                Me._ventana.ResultadosFacFactuDeta = Nothing
                Me._ventana.ResultadosFacFactuDeta = FacFactuDetas

                factura.MSubtimpo = Me._ventana.MSubtimpo
                factura.MDescuento = Me._ventana.MDescuento
                factura.MTbimp = Me._ventana.MTbimp
                factura.Mtbexc = Me._ventana.Mtbexc
                factura.MSubtotal = Me._ventana.Msubtotal
                factura.Mtimp = Me._ventana.Mtimp
                factura.Mttotal = Me._ventana.Mttotal
                factura.MSubtimpoBf = Me._ventana.MSubtimpoBf
                factura.MDescuentoBf = Me._ventana.MDescuentoBf
                factura.MTbimpBf = Me._ventana.MTbimpBf
                factura.MTbexcBf = Me._ventana.MtbexcBf
                factura.MSubtotalBf = Me._ventana.MsubtotalBf
                factura.MTimpBf = Me._ventana.MtimpBf
                factura.MTtotalBf = Me._ventana.MttotalBf
                Me._FacFacturaServicios.InsertarOModificar(factura, UsuarioLogeado.Hash)
            End If
        End Sub

        Public Sub Recalculo_x_imp_nueva()
            Me._ventana.MSubtimpo = 0
            Me._ventana.MDescuento = 0
            Me._ventana.MTbimp = 0
            Me._ventana.Mtbexc = 0
            Me._ventana.Msubtotal = 0
            Me._ventana.Mtimp = 0
            Me._ventana.Mttotal = 0
            Me._ventana.MSubtimpoBf = 0
            Me._ventana.MDescuentoBf = 0
            Me._ventana.MTbimpBf = 0
            Me._ventana.MtbexcBf = 0
            Me._ventana.MsubtotalBf = 0
            Me._ventana.MtimpBf = 0
            Me._ventana.MttotalBf = 0

            Dim w_monto, w_monto_bf As Integer
            Dim factura As FacFactura = Me._ventana.FacFactura
            Dim fecha As DateTime
            Dim w_impos As Integer = 0
            Dim w_impu As Double? = Nothing
            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            If FacFactuDetas IsNot Nothing Then
                'Dim guardar As Boolean
                For i As Integer = 0 To FacFactuDetas.Count - 1

                    If factura.Moneda.Id = "US" Then

                        If factura.FechaSeniat IsNot Nothing Then
                            fecha = FormatDateTime(factura.FechaSeniat, DateFormat.ShortDate)
                        Else
                            fecha = FormatDateTime(factura.FechaFactura, DateFormat.ShortDate)
                        End If
                        Dim tasa As Tasa = buscar_tasa(fecha, "US")
                        If tasa IsNot Nothing Then
                            FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle * tasa.Tasabf
                            FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu * tasa.Tasabf
                        End If
                    Else
                        FacFactuDetas(i).BDetalleBf = FacFactuDetas(i).BDetalle
                        FacFactuDetas(i).PuBf = FacFactuDetas(i).Pu
                    End If

                    'guarda los cambios en el detalle
                    'guardar = _FacFactuDetasServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)
                    Me._FacFactuDetaServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)

                    w_monto = FacFactuDetas(i).BDetalle
                    w_monto_bf = FacFactuDetas(i).BDetalleBf

                    If FacFactuDetas(i).Impuesto = "T" Or FacFactuDetas(i).Impuesto = "1" Then
                        Me._ventana.MSubtimpo = Me._ventana.MSubtimpo + w_monto
                        Me._ventana.MSubtimpoBf = Me._ventana.MSubtimpoBf + w_monto_bf
                    Else
                        Me._ventana.Mtbexc = Me._ventana.Mtbexc + w_monto
                        Me._ventana.MtbexcBf = Me._ventana.MtbexcBf + w_monto_bf
                    End If
                    Me._ventana.MDescuento = Me._ventana.MDescuento + ((FacFactuDetas(i).Pu * FacFactuDetas(i).NCantidad) * FacFactuDetas(i).Descuento) / 100
                    Me._ventana.MDescuento = Me._ventana.MDescuento

                    Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf + ((FacFactuDetas(i).PuBf * FacFactuDetas(i).NCantidad) * FacFactuDetas(i).Descuento) / 100
                    Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf
                Next
                'If factura.Descuento <> 0 Then

                'Me._ventana.MDescuento = (factura.MSubtimpo * factura.Descuento) / 100
                'Me._ventana.MDescuento = Me._ventana.MDescuento

                'Me._ventana.MDescuentoBf = (factura.MSubtimpoBf * factura.Descuento) / 100
                'Me._ventana.MDescuentoBf = Me._ventana.MDescuentoBf

                Me._ventana.MTbimp = Me._ventana.MSubtimpo - factura.Descuento
                Me._ventana.MTbimpBf = Me._ventana.MSubtimpoBf - Me._ventana.MDescuentoBf

                Me._ventana.Msubtotal = Me._ventana.MTbimp + Me._ventana.Mtbexc
                Me._ventana.MsubtotalBf = Me._ventana.MTbimpBf + Me._ventana.MtbexcBf

                Me._ventana.Mtimp = Me._ventana.MTbimp * (factura.Impuesto / 100)
                'Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (Me._ventana.Impuesto / 100)
                If factura.PSeniat <> 0 Then
                    w_impu = factura.PSeniat
                Else
                    w_impu = factura.Impuesto
                End If
                Me._ventana.MtimpBf = Me._ventana.MTbimpBf * (w_impu / 100)

                Me._ventana.Mttotal = Me._ventana.Msubtotal + Me._ventana.Mtimp
                Me._ventana.MttotalBf = Me._ventana.MsubtotalBf + Me._ventana.MtimpBf

                Me._ventana.Mtimp = Me._ventana.Mtimp
                Me._ventana.MtimpBf = Me._ventana.MtimpBf

                Me._ventana.Mttotal = Me._ventana.Mttotal
                Me._ventana.MttotalBf = Me._ventana.MttotalBf


                Me._ventana.ResultadosFacFactuDeta = Nothing
                Me._ventana.ResultadosFacFactuDeta = FacFactuDetas

                factura.MSubtimpo = Me._ventana.MSubtimpo
                factura.MDescuento = Me._ventana.MDescuento
                factura.MTbimp = Me._ventana.MTbimp
                factura.Mtbexc = Me._ventana.Mtbexc
                factura.MSubtotal = Me._ventana.Msubtotal
                factura.Mtimp = Me._ventana.Mtimp
                factura.Mttotal = Me._ventana.Mttotal
                factura.MSubtimpoBf = Me._ventana.MSubtimpoBf
                factura.MDescuentoBf = Me._ventana.MDescuentoBf
                factura.MTbimpBf = Me._ventana.MTbimpBf
                factura.MTbexcBf = Me._ventana.MtbexcBf
                factura.MSubtotalBf = Me._ventana.MsubtotalBf
                factura.MTimpBf = Me._ventana.MtimpBf
                factura.MTtotalBf = Me._ventana.MttotalBf
                Me._FacFacturaServicios.InsertarOModificar(factura, UsuarioLogeado.Hash)
            End If
        End Sub

        Public Function buscar_tasa(ByVal fecha As DateTime, ByVal moneda As String) As Tasa
            Dim tasa As New Tasa()
            tasa.Id = fecha
            If moneda <> "" Then
                tasa.Moneda = moneda
            End If
            Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            If tasas.Count > 0 Then
                Return tasas(0)
            Else
                Return Nothing
            End If
        End Function

        'guara los detalles con el numero de la factura
        Public Sub actualizar_detalle_(ByVal idfactura As Integer)
            Dim FacFactuDeta As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            Dim FacFactura As New FacFactura
            FacFactura.Id = idfactura
            Dim guardar As Boolean
            For i As Integer = 0 To FacFactuDeta.Count - 1
                FacFactuDeta(i).Factura = FacFactura
                'guarda los cambios en el detalle
                guardar = _FacFactuDetaServicios.InsertarOModificar(FacFactuDeta(i), UsuarioLogeado.Hash)
            Next
        End Sub

        Public Sub InsertarDetalle(ByVal contador As Integer)
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            Dim facfactudeta As New FacFactuDetalle
            Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
            Dim idioma As Idioma = DirectCast(Me._ventana.Idioma, Idioma)
            Dim w_paso As Integer = 1
            Dim cstr1 As String = Nothing

            Try

                facfactudeta.Id = contador
                facfactudeta.Servicio = departamento_servicio.Servicio

                facfactudeta.TipoServicio = departamento_servicio.Servicio.Itipo
                facfactudeta.Impuesto = departamento_servicio.Servicio.Aimpuesto
                facfactudeta.Desglose = departamento_servicio.Servicio.Desg

                If departamento_servicio.Servicio.BImodpr = False Then ' verificar este campo en el detalle facfactudeta.imodpr
                    'facfactudeta.imodpr = "N"
                Else
                    'facfactudeta.imodpr = "S"
                End If

                'para determinar la tarifa y realizar calculos basado en la misma
                Dim tarifaservicioaux As New TarifaServicio
                Dim tarifa2 As New Tarifa2
                tarifa2.Id = DirectCast(Me._ventana.Tarifa, String)
                tarifaservicioaux.Id = departamento_servicio.Servicio.Id
                tarifaservicioaux.Tarifa = tarifa2
                Dim tarifaservicios As IList(Of TarifaServicio)
                tarifaservicios = Me._TarifaServiciosServicios.ObtenerTarifaServiciosFiltro(tarifaservicioaux)

                If tarifaservicios.Count > 0 Then
                    If (moneda.Id = "BS") Then
                        Me._ventana.MensajeError = "La moneda debe ser BSF o $"
                        facfactudeta.BDetalle = tarifaservicios(0).Mont_Bs
                        facfactudeta.Pu = tarifaservicios(0).Mont_Bs
                    End If
                    If (moneda.Id = "US") Then
                        facfactudeta.BDetalle = tarifaservicios(0).Mont_Us
                        facfactudeta.Pu = tarifaservicios(0).Mont_Us
                    End If

                    Dim tasa As New Tasa()
                    tasa.Id = Me._ventana.FechaFactura
                    tasa.Moneda = "US"
                    Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
                    Dim btasa As Double
                    If tasas.Count > 0 Then
                        btasa = tasas(0).Tasabf
                    Else
                        btasa = 0
                    End If
                    If (moneda.Id = "BF") Then
                        facfactudeta.BDetalle = tarifaservicios(0).Mont_Us * btasa
                        facfactudeta.Pu = tarifaservicios(0).Mont_Us * btasa
                    End If
                    facfactudeta.BDetalleBf = tarifaservicios(0).Mont_Us * btasa
                    facfactudeta.PuBf = tarifaservicios(0).Mont_Us * btasa
                End If
                'fin para determinar la tarifa y realizar calculos basado en la misma
                Dim monto_bf As Double = 0
                Dim monto_bs As Double = 0
                Dim monto_us As Double = 0
                Dim tipodoc_bf As Boolean = False
                Dim tipodoc_bs As Boolean = False
                Dim tipodoc_us As Boolean = False


                Dim ctdoc As String = Nothing 'documento marca o patente
                Dim lista_en(19) As String
                lista_en = asignar_vacio()
                'variables de los codigos que van en la variable lista_en
                Dim v_pagina As Integer? = Nothing 'cantidad
                Dim cttra As Integer? = Nothing 'documento traduccion
                Dim w_recurso As Integer? = Nothing 'fac recurso
                Dim w_material As Integer? = Nothing 'material
                Dim lista_marca_patente(4) As String
                Dim multiples_marca_patente As Boolean = False
                Dim marca_patente As Boolean = False
                Dim v_anualidad As Integer? = Nothing 'Anualidad
                Dim lista_ad As String = Nothing 'lista_add
                Dim v_ninter As String = Nothing ' codigo marca o patente
                Dim v_citipop As String = Nothing 'tipo cuando sea patente
                Dim v_citipom As String = Nothing 'tipo cuando sea marca
                Dim vmarca As Boolean = False
                Dim vpatente As Boolean = False

                ' si tipo de documento es true entro y evaluo
                If departamento_servicio.Servicio.BItidoc = True Then
                    If departamento_servicio.Servicio.Itipo = "M" Then ' si es tipo es marca busco documento marca
                        Dim documento_marca As DocumentosMarca = DirectCast(Me._ventana.DocumentoMarca_Seleccionado, DocumentosMarca)
                        If (documento_marca.Mont_Bf <> Nothing) Then
                            monto_bf = documento_marca.Mont_Bf
                            tipodoc_bf = True
                        End If
                        If (documento_marca.Mont_Bs <> Nothing) Then
                            monto_bs = documento_marca.Mont_Bs
                            tipodoc_bs = True
                        End If
                        If (documento_marca.Mont_Us <> Nothing) Then
                            monto_us = documento_marca.Mont_Us
                            tipodoc_us = True
                        End If
                        ctdoc = documento_marca.Id
                    Else
                        If departamento_servicio.Servicio.Itipo = "P" Then ' si es tipo es Patente busco documento Patente
                            Dim documento_Patente As DocumentosPatente = DirectCast(Me._ventana.DocumentoPatente_Seleccionado, DocumentosPatente)
                            If (documento_Patente.Mont_Bf <> Nothing) Then
                                monto_bf = documento_Patente.Mont_Bf
                                tipodoc_bf = True
                            End If
                            If (documento_Patente.Mont_Bs <> Nothing) Then
                                monto_bs = documento_Patente.Mont_Bs
                                tipodoc_bs = True
                            End If
                            If (documento_Patente.Mont_Us <> Nothing) Then
                                monto_us = documento_Patente.Mont_Us
                                tipodoc_us = True
                            End If
                            ctdoc = documento_Patente.Id
                        End If
                    End If

                    If (tipodoc_bf = True) Then
                        facfactudeta.BDetalleBf = monto_bf
                        facfactudeta.PuBf = monto_bf

                        If (moneda.Id = "BF") Then
                            facfactudeta.BDetalle = monto_bf
                            facfactudeta.Pu = monto_bf
                        End If
                    End If

                    If (moneda.Id = "BS") Then
                        If (tipodoc_bs = True) Then
                            facfactudeta.BDetalle = monto_bs
                            facfactudeta.Pu = monto_bs
                        End If
                    End If

                    If (moneda.Id = "US") Then
                        If (tipodoc_us = True) Then
                            facfactudeta.BDetalle = monto_us
                            facfactudeta.Pu = monto_us
                        End If
                    End If
                    ' fin si tipo de documento es true entro y evaluo
                Else
                    ' para las cantidades
                    If IsNumeric(Me._ventana.Cantidad) Then
                        v_pagina = Me._ventana.Cantidad
                    Else
                        'v_pagina = 0
                    End If

                    'lista_en = "Pagina=" & v_pagina & ";" 'aqui tengo que verificar como trabajar esta variable mas adelante                
                    If v_pagina IsNot Nothing Then
                        lista_en(11) = v_pagina
                    End If
                End If

                If (departamento_servicio.Servicio.BItraduc = True) Then
                    Dim documento_traduccion As DocumentosTraduccion = DirectCast(Me._ventana.DocumentoTraduccion_Seleccionado, DocumentosTraduccion)
                    cttra = documento_traduccion.Id
                    'If lista_en = Nothing Then
                    '    lista_en = "codser=" & departamento_servicio.Servicio.Id & ";codtra=" & cttra & ";"
                    lista_en(0) = departamento_servicio.Servicio.Id
                    If cttra IsNot Nothing Then
                        lista_en(7) = cttra
                    End If

                    'Else
                    '    lista_en = lista_en & "codser=" & departamento_servicio.Servicio.Id & ";codtra=" & cttra & ";"
                    'End If

                End If

                If (departamento_servicio.Servicio.BRecursos = True) Then
                    Dim facrecurso As FacRecurso = DirectCast(Me._ventana.Recurso_Seleccionado, FacRecurso)
                    w_recurso = facrecurso.Id
                    'If lista_en = Nothing Then
                    '    lista_en = "codser=" & departamento_servicio.Servicio.Id & ";recurso=" & w_recurso & ";"
                    lista_en(0) = departamento_servicio.Servicio.Id
                    If w_recurso IsNot Nothing Then
                        lista_en(17) = w_recurso
                    End If
                    'Else

                    '    lista_en = lista_en & "codser=" & departamento_servicio.Servicio.Id & ";recurso=" & w_recurso & ";"
                    'End If
                End If

                If (departamento_servicio.Servicio.BMaterial = True) Then
                    Dim material As Material = DirectCast(Me._ventana.Material_Seleccionado, Material)
                    w_material = material.Id
                    'If lista_en = Nothing Then
                    '    lista_en = "codser=" & departamento_servicio.Servicio.Id & ";material=" & w_material & ";"
                    lista_en(0) = departamento_servicio.Servicio.Id
                    If w_material IsNot Nothing Then
                        lista_en(18) = w_material
                    End If
                    'Else
                    '    lista_en = lista_en & "codser=" & departamento_servicio.Servicio.Id & ";material=" & w_material & ";"
                    'End If

                End If


                If (departamento_servicio.Servicio.BAnual = True) Then 'Anualidad
                    Dim FacAnualidad As FacAnualidad = DirectCast(Me._ventana.Anualidad_Seleccionado, FacAnualidad)
                    v_anualidad = FacAnualidad.Id
                End If

                'para las multiples marcas O marca -- multiples patentes O patente

                If (departamento_servicio.Servicio.Itipo = "M") Or (departamento_servicio.Servicio.Itipo = "P") Then
                    If (departamento_servicio.Servicio.Itipo = "M") Then
                        If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                            Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                            If imult.Trim = "T" Or imult = "1" Then
                                lista_marca_patente = generar_multiples_marcas()
                                multiples_marca_patente = True
                                vmarca = True
                            Else
                                w_paso = 0
                                Me._ventana.VerTipo = "13"
                                Mouse.OverrideCursor = Nothing
                                Me._ventana.MensajeError = "Este Servicios no Acepta Multiregistro"
                                Exit Sub
                            End If
                        Else
                            lista_marca_patente = generar_marca()
                            marca_patente = True
                            v_ninter = lista_marca_patente(1)
                            v_citipom = lista_marca_patente(3)
                            '"clasi=%%v_citipom"
                            lista_ad = "clasi=" & lista_marca_patente(3)
                            lista_en(10) = lista_marca_patente(3).ToString
                        End If

                    Else

                        If (departamento_servicio.Servicio.Itipo = "P") Then
                            If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                                Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                                If imult.Trim = "T" Or imult = "1" Then
                                    lista_marca_patente = generar_multiples_Patentes()
                                    multiples_marca_patente = True
                                    vpatente = True
                                Else
                                    w_paso = 0
                                    Me._ventana.VerTipo = "13"
                                    Mouse.OverrideCursor = Nothing
                                    Me._ventana.MensajeError = "Este Servicios no Acepta Multiregistro"
                                    Exit Sub
                                End If
                            Else
                                lista_marca_patente = generar_Patente()
                                marca_patente = True
                                v_ninter = lista_marca_patente(1)
                                v_citipop = lista_marca_patente(3)
                                lista_ad = ""
                                lista_en(10) = ""
                            End If
                        End If

                    End If

                    If multiples_marca_patente = True Then
                        Dim cantidad As Integer = lista_marca_patente(0)
                        facfactudeta.BDetalle = facfactudeta.BDetalle * cantidad
                        facfactudeta.NCantidad = cantidad
                        facfactudeta.Pu = facfactudeta.BDetalle / cantidad

                        facfactudeta.BDetalleBf = facfactudeta.BDetalleBf * cantidad
                        facfactudeta.PuBf = facfactudeta.BDetalleBf / cantidad
                    End If

                    If marca_patente = True Or multiples_marca_patente = True Then

                        If Me._ventana.Ourref = "" Or Me._ventana.Ourref = Nothing Then
                            Me._ventana.Ourref = lista_marca_patente(1)
                        Else
                            Me._ventana.Ourref = Me._ventana.Ourref & "," & lista_marca_patente(1)
                        End If

                        If Me._ventana.Caso = "" Or Me._ventana.Caso = Nothing Then
                            Me._ventana.Caso = lista_marca_patente(2)
                        Else
                            Me._ventana.Caso = Me._ventana.Caso & "/" & lista_marca_patente(2)
                        End If

                        ''falta agregar lo de tipo servicio aqui el codigo 
                        '            clear/e "FAC_TIPOSERV"
                        'cfactura.fac_tiposerv/init = cfactura.fac_facturas_pro
                        'itipo.fac_tiposerv/init    = v_itipo
                        'cinterno.fac_tiposerv/init = v_ninter
                        'retrieve/e "FAC_TIPOSERV"
                        'if ($status >= 0)
                        '    setocc("FAC_TIPOSERV", -1)
                        '   erase/e "FAC_TIPOSERV"
                        '    commit()
                        'End If

                        'creocc("FAC_TIPOSERV", -1)
                        'cfactura.fac_tiposerv/init = cfactura.fac_facturas_pro
                        'itipo.fac_tiposerv/init    = v_itipo
                        'cinterno.fac_tiposerv/init = $$LST_SELECCION
                        'Call gp_graba("FAC_TIPOSERV", "S")


                        ';   Asignado variables a la entidad fac_detalles_pro para uso posterior
                        ';
                        '            codigo.fac_detalles_pro = v_ninter
                        '            ioperacion.fac_detalles_pro = v_itipo
                        ';
                        '            lista_en = "codser=%%codser.fac_detalles_protipo=%%v_itipocodigomp=%%v_nintercidioma=%%cidioma.fac_facturas_prolocal=%%v_localitidoc=%%v_itidoccodtdoc=%%ctdoccodtra=%%cttratipomarc=%%v_citipomtipopate=%%v_citipop%%lista_adpagina=%%v_paginaanual=%%v_anualmulti_mar=%%$$lst_marca2multi_pate=%%$$lst_patente2bsel=%%bsel.fac_detalles_proanualidad=%%v_Anualidadrecurso=%%w_recursomaterial=%%w_material"
                        ';
                        facfactudeta.Codigo = v_ninter
                        'facfactudeta.ioperacion = v_itipo
                        'este campo se saca de el servicio del detalle itipo


                        'lista_en = "codser=" & departamento_servicio.Servicio.Id & ";" & "tipo=" & departamento_servicio.Servicio.Itipo & ";" & "codigomp=" & v_ninter
                        'lista_en = lista_en & ";" & "cidioma=" & idioma.Id & ";" & "local=" & departamento_servicio.Servicio.Local & ";" & "itidoc=" & departamento_servicio.Servicio.Itidoc
                        'If lista_ad <> "" Then
                        '    lista_en = lista_en & ";" & "codtdoc=" & ctdoc & ";" & "codtra=" & cttra & ";" & "tipomarc=" & v_citipom & ";" & "tipopate=" & v_citipop & ";" & lista_ad
                        'Else
                        '    lista_en = lista_en & ";" & "codtdoc=" & ctdoc & ";" & "codtra=" & cttra & ";" & "tipomarc=" & v_citipom & ";" & "tipopate=" & v_citipop
                        'End If
                        'lista_en = lista_en & ";" & "pagina=" & v_pagina & ";" & "anual=" & departamento_servicio.Servicio.BAnual
                        'If vmarca = True Then
                        '    lista_en = lista_en & ";" & "multi_mar=" & lista_marca_patente(1)
                        'Else
                        '    If vpatente = True Then
                        '        lista_en = lista_en & ";" & "multi_pate=" & lista_marca_patente(1)
                        '    End If
                        'End If
                        'lista_en = lista_en & ";" & "bsel=" & Me._ventana.Seleccion & ";" & "anualidad=" & v_anualidad & ";" & "recurso=" & w_recurso & ";" & "material=" & w_material

                        'lista_en("codser") = departamento_servicio.Servicio.Id.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Id <> "" Then
                            lista_en(0) = departamento_servicio.Servicio.Id.ToString
                        Else
                            lista_en(0) = ""
                        End If
                        'lista_en("tipo") = departamento_servicio.Servicio.Itipo.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Itipo.ToString <> "" Then
                            lista_en(1) = departamento_servicio.Servicio.Itipo.ToString
                        Else
                            lista_en(1) = ""
                        End If
                        ' lista_en("codigomp") = v_ninter.ToString
                        If v_ninter <> "" And v_ninter <> Nothing Then
                            lista_en(2) = v_ninter.ToString
                        Else
                            lista_en(2) = ""
                        End If
                        'lista_en("cidioma") = idioma.Id.ToString
                        If idioma IsNot Nothing Then
                            lista_en(3) = idioma.Id.ToString
                        Else
                            lista_en(3) = ""
                        End If

                        'lista_en("local") = departamento_servicio.Servicio.Local.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Local.ToString <> "" Then
                            lista_en(4) = departamento_servicio.Servicio.Local.ToString
                        Else
                            lista_en(4) = ""
                        End If
                        'lista_en("itidoc") = departamento_servicio.Servicio.Itidoc.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Itidoc.ToString <> "" Then
                            lista_en(5) = departamento_servicio.Servicio.Itidoc.ToString
                        Else
                            lista_en(5) = ""
                        End If
                        'lista_en("codtdoc") = ctdoc.ToString
                        If ctdoc <> "" And ctdoc <> Nothing Then
                            lista_en(6) = ctdoc.ToString
                        Else
                            lista_en(6) = ""
                        End If
                        'lista_en("codtra") = cttra.ToString
                        If cttra IsNot Nothing Then
                            lista_en(7) = cttra.ToString
                        Else
                            lista_en(7) = ""
                        End If
                        'lista_en("tipomarc") = v_citipom.ToString
                        If v_citipom <> "" And v_citipom <> Nothing Then
                            lista_en(8) = v_citipom.ToString
                        Else
                            lista_en(8) = ""
                        End If
                        'lista_en("tipopate") = v_citipop.ToString
                        If v_citipop <> "" And v_citipop <> Nothing Then
                            lista_en(9) = v_citipop.ToString
                        Else
                            lista_en(9) = ""
                        End If
                        'lista_en("clasi") = lista_marca_patente(3).ToString
                        If lista_ad <> "" Then
                            lista_en(10) = lista_marca_patente(3).ToString
                        Else
                            lista_en(10) = ""
                        End If
                        'lista_en("pagina") = v_pagina.ToString
                        If v_pagina IsNot Nothing Then
                            lista_en(11) = v_pagina.ToString
                        Else
                            lista_en(11) = ""
                        End If
                        'lista_en("anual") = departamento_servicio.Servicio.BAnual.ToString
                        If departamento_servicio.Servicio IsNot Nothing Then
                            If departamento_servicio.Servicio.BAnual = True Then
                                lista_en(12) = departamento_servicio.Servicio.BAnual.ToString
                            Else
                                lista_en(12) = ""
                            End If

                        Else
                            lista_en(12) = ""
                        End If
                        If vmarca = True Or vpatente = True Then
                            If vmarca = True Then
                                '    lista_en = lista_en & ";" & "multi_mar=" & lista_marca_patente(1)
                                'lista_en("multi_mar") = lista_marca_patente(1)
                                lista_en(13) = lista_marca_patente(1)

                                'lista_en("multi_pate") = ""
                                lista_en(14) = ""
                            Else

                                If vpatente = True Then
                                    '        lista_en = lista_en & ";" & "multi_pate=" & lista_marca_patente(1)
                                    'lista_en("multi_pate") = lista_marca_patente(1)
                                    lista_en(14) = lista_marca_patente(1)

                                    'lista_en("multi_mar") = ""
                                    lista_en(13) = ""
                                End If
                            End If
                        Else
                            'lista_en("multi_pate") = ""
                            lista_en(14) = ""

                            'lista_en("multi_mar") = ""
                            lista_en(13) = ""
                        End If
                        'lista_en("bsel") = Me._ventana.Seleccion
                        lista_en(15) = Me._ventana.Seleccion

                        'lista_en("anualidad") = v_anualidad
                        If v_anualidad IsNot Nothing Then
                            lista_en(16) = v_anualidad
                        End If
                        'lista_en("recurso") = w_recurso
                        If w_recurso IsNot Nothing Then
                            lista_en(17) = w_recurso
                        End If
                        'lista_en("material") = w_material.ToString
                        If w_material IsNot Nothing Then
                            lista_en(18) = w_material
                        End If
                    End If
                    'fin para las multiples marcas p multiples patentes o marca o patente
                Else
                    If (departamento_servicio.Servicio.Itipo = "E") Or (departamento_servicio.Servicio.Itipo = "C") Then
                        facfactudeta.NCantidad = 1

                        'lista_en = "codser=" & departamento_servicio.Servicio.Id & ";" & "tipo=" & departamento_servicio.Servicio.Itipo & ";" & "codigomp=" & v_ninter
                        'lista_en = lista_en & ";" & "cidioma=" & idioma.Id & ";" & "local=" & departamento_servicio.Servicio.Local & ";" & "itidoc=" & departamento_servicio.Servicio.Itidoc
                        'If lista_ad <> "" Then
                        '    lista_en = lista_en & ";" & "codtdoc=" & ctdoc & ";" & "codtra=" & cttra & ";" & "tipomarc=" & v_citipom & ";" & "tipopate=" & v_citipop & ";" & lista_ad
                        'Else
                        '    lista_en = lista_en & ";" & "codtdoc=" & ctdoc & ";" & "codtra=" & cttra & ";" & "tipomarc=" & v_citipom & ";" & "tipopate=" & v_citipop
                        'End If
                        'lista_en = lista_en & ";" & "pagina=" & v_pagina & ";" & "anual=" & departamento_servicio.Servicio.BAnual
                        'If vmarca = True Then
                        '    lista_en = lista_en & ";" & "multi_mar=" & lista_marca_patente(1)
                        'Else
                        '    If vpatente = True Then
                        '        lista_en = lista_en & ";" & "multi_pate=" & lista_marca_patente(1)
                        '    End If
                        'End If
                        'lista_en = lista_en & ";" & "bsel=" & Me._ventana.Seleccion & ";" & "anualidad=" & v_anualidad & ";" & "recurso=" & w_recurso & ";" & "material=" & w_material

                        'lista_en("codser") = departamento_servicio.Servicio.Id.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Id <> "" Then
                            lista_en(0) = departamento_servicio.Servicio.Id.ToString
                        Else
                            lista_en(0) = ""
                        End If
                        'lista_en("tipo") = departamento_servicio.Servicio.Itipo.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Itipo.ToString <> "" Then
                            lista_en(1) = departamento_servicio.Servicio.Itipo.ToString
                        Else
                            lista_en(1) = ""
                        End If
                        ' lista_en("codigomp") = v_ninter.ToString
                        If v_ninter <> "" And v_ninter <> Nothing Then
                            lista_en(2) = v_ninter.ToString
                        Else
                            lista_en(2) = ""
                        End If
                        'lista_en("cidioma") = idioma.Id.ToString
                        If idioma IsNot Nothing Then
                            lista_en(3) = idioma.Id.ToString
                        Else
                            lista_en(3) = ""
                        End If

                        'lista_en("local") = departamento_servicio.Servicio.Local.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Local.ToString <> "" Then
                            lista_en(4) = departamento_servicio.Servicio.Local.ToString
                        Else
                            lista_en(4) = ""
                        End If
                        'lista_en("itidoc") = departamento_servicio.Servicio.Itidoc.ToString
                        If departamento_servicio.Servicio IsNot Nothing And departamento_servicio.Servicio.Itidoc.ToString <> "" Then
                            lista_en(5) = departamento_servicio.Servicio.Itidoc.ToString
                        Else
                            lista_en(5) = ""
                        End If
                        'lista_en("codtdoc") = ctdoc.ToString
                        If ctdoc <> "" And ctdoc <> Nothing Then
                            lista_en(6) = ctdoc.ToString
                        Else
                            lista_en(6) = ""
                        End If
                        'lista_en("codtra") = cttra.ToString
                        If cttra IsNot Nothing Then
                            lista_en(7) = cttra.ToString
                        Else
                            lista_en(7) = ""
                        End If
                        'lista_en("tipomarc") = v_citipom.ToString
                        If v_citipom <> "" And v_citipom <> Nothing Then
                            lista_en(8) = v_citipom.ToString
                        Else
                            lista_en(8) = ""
                        End If
                        'lista_en("tipopate") = v_citipop.ToString
                        If v_citipop <> "" And v_citipop <> Nothing Then
                            lista_en(9) = v_citipop.ToString
                        Else
                            lista_en(9) = ""
                        End If
                        'lista_en("clasi") = lista_marca_patente(3).ToString
                        If lista_ad <> "" Then
                            lista_en(10) = lista_marca_patente(3).ToString
                        Else
                            lista_en(10) = ""
                        End If
                        'lista_en("pagina") = v_pagina.ToString
                        If v_pagina IsNot Nothing Then
                            lista_en(11) = v_pagina.ToString
                        Else
                            lista_en(11) = ""
                        End If
                        'lista_en("anual") = departamento_servicio.Servicio.BAnual.ToString
                        If departamento_servicio.Servicio IsNot Nothing Then
                            If departamento_servicio.Servicio.BAnual = True Then
                                lista_en(12) = departamento_servicio.Servicio.BAnual.ToString
                            Else
                                lista_en(12) = ""
                            End If

                        Else
                            lista_en(12) = ""
                        End If

                        If vmarca = True Or vpatente = True Then
                            If vmarca = True Then
                                '    lista_en = lista_en & ";" & "multi_mar=" & lista_marca_patente(1)
                                'lista_en("multi_mar") = lista_marca_patente(1)
                                lista_en(13) = lista_marca_patente(1)

                                'lista_en("multi_pate") = ""
                                lista_en(14) = ""
                            Else

                                If vpatente = True Then
                                    '        lista_en = lista_en & ";" & "multi_pate=" & lista_marca_patente(1)
                                    'lista_en("multi_pate") = lista_marca_patente(1)
                                    lista_en(14) = lista_marca_patente(1)

                                    'lista_en("multi_mar") = ""
                                    lista_en(13) = ""
                                End If
                            End If
                        Else
                            'lista_en("multi_pate") = ""
                            lista_en(14) = ""

                            'lista_en("multi_mar") = ""
                            lista_en(13) = ""
                        End If
                        'lista_en("bsel") = Me._ventana.Seleccion
                        lista_en(15) = Me._ventana.Seleccion.ToString

                        'lista_en("anualidad") = v_anualidad
                        If v_anualidad IsNot Nothing Then
                            lista_en(16) = v_anualidad
                        End If
                        'lista_en("recurso") = w_recurso
                        If w_recurso IsNot Nothing Then
                            lista_en(17) = w_recurso
                        End If

                        'lista_en("material") = w_material.ToString
                        If w_material IsNot Nothing Then
                            lista_en(18) = w_material
                        End If
                    End If
                End If
                Dim ccampo As String = Nothing
                Dim ccampo_es As String = Nothing
                FAC_BSERV(lista_en, ccampo, ccampo_es)

                If ccampo <> "" And ccampo <> Nothing Then
                    facfactudeta.XDetalle = ccampo
                    facfactudeta.XDetalleEs = ccampo_es
                    If ((departamento_servicio.Servicio.Itipo = "C") And (v_pagina IsNot Nothing)) Then
                        facfactudeta.Pu = facfactudeta.BDetalle
                        facfactudeta.NCantidad = v_pagina
                        facfactudeta.BDetalle = facfactudeta.NCantidad * facfactudeta.Pu

                        facfactudeta.PuBf = facfactudeta.BDetalleBf
                        facfactudeta.NCantidad = v_pagina
                        facfactudeta.BDetalleBf = facfactudeta.NCantidad * facfactudeta.PuBf
                    End If
                End If 'If ccampo <> "" And ccampo <> Nothing Then

                If (w_paso = 0) Then
                    facfactudeta.Servicio = Nothing
                    facfactudeta.XDetalle = ""
                    facfactudeta.BDetalle = 0
                    facfactudeta.NCantidad = 0
                    facfactudeta.Pu = 0
                    facfactudeta.Descuento = 0
                    facfactudeta.Bsel = "True"
                    Me._ventana.Seleccion = True
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
                If facfactudeta.Impuesto = "T" Then
                    If IsNumeric(Me._ventana.Desc) = True Then
                        facfactudeta.Descuento = Me._ventana.Desc
                    End If
                End If
                If ((departamento_servicio.Servicio.Itipo = "M") Or (departamento_servicio.Servicio.Itipo = "P")) Then
                    cstr1 = lista_marca_patente(1)
                    If vmarca = True Then
                        Dim Marcasselec As New List(Of MarcaSelec)
                        ' si el grid de multiples marcas tiene ya porlomenos un registro
                        If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then
                            Marcasselec = DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec))
                            For i As Integer = 0 To Marcasselec.Count - 1
                                eliminar_operacion_detalle_tm(Marcasselec(i).Id, contador, departamento_servicio.Servicio)
                            Next
                        End If
                    End If

                    If vpatente = True Then
                        Dim patentesselec As New List(Of PatenteSelec)
                        ' si el grid de multiples patentes tiene ya porlomenos un registro
                        If (DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec)) IsNot Nothing) Then
                            patentesselec = DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec))
                            For i As Integer = 0 To patentesselec.Count - 1
                                eliminar_operacion_detalle_tm(patentesselec(i).Id, contador, departamento_servicio.Servicio)
                            Next
                        End If
                    End If
                End If 'If ((departamento_servicio.Servicio.Itipo = "M") Or (departamento_servicio.Servicio.Itipo = "P")) Then

                ' esta parte es la que sigue despues de llamar al lp_proceso en el detail del boton
                If departamento_servicio.Servicio.BImodpr = False Then ' verificar este campo en el detalle facfactudeta.imodpr
                    'facfactudeta.imodpr = "N"
                    Me._ventana.Activar_Desactivar = False
                Else
                    'facfactudeta.imodpr = "S"
                    Me._ventana.Activar_Desactivar = True
                End If

                If Me._ventana.Desglose = True Then
                    Dim desglose_servicio As FacDesgloseServicio = DirectCast(Me._ventana.DesgloseServicio_Seleccionado, FacDesgloseServicio)
                    If desglose_servicio IsNot Nothing Then
                        If desglose_servicio.Servicio IsNot Nothing Then
                            If facfactudeta.Pu.ToString <> "" And desglose_servicio.Pporc.ToString <> "" Then
                                facfactudeta.Pu = facfactudeta.Pu * desglose_servicio.Pporc / 100
                            Else
                                facfactudeta.Pu = 0
                                facfactudeta.PuBf = 0
                            End If
                            If facfactudeta.Descuento.ToString <> "" And facfactudeta.NCantidad.ToString <> "" Then
                                facfactudeta.BDetalle = (facfactudeta.Pu * facfactudeta.NCantidad) * (1 - (facfactudeta.Descuento / 100))
                                facfactudeta.BDetalleBf = (facfactudeta.PuBf * facfactudeta.NCantidad) * (1 - (facfactudeta.Descuento / 100))
                            Else
                                facfactudeta.BDetalle = 0
                                facfactudeta.BDetalleBf = 0
                                If facfactudeta.NCantidad.ToString = Nothing Or facfactudeta.NCantidad.ToString = "" Then
                                    facfactudeta.NCantidad = 0
                                End If
                                If facfactudeta.Descuento.ToString = Nothing Or facfactudeta.Descuento.ToString = "" Then
                                    facfactudeta.Descuento = 0
                                End If
                            End If
                            'falta crear esta tabla para buscar el valor
                            ' clear/e "fac_desg_cole"
                            'itipo.fac_desg_cole/init   = $$LST_SELECCION2
                            'idioma.fac_desg_cole/init   = cidioma.fac_idiomas
                            'retrieve/e "fac_desg_cole"
                            'if ($status >=0)
                            '                      xdetalle.fac_detalles_pro = "%%xcole.fac_desg_cole %%xdetalle.fac_detalles_pro"
                            '                  End If
                            '$status = 0
                        End If 'If desglose_servicio.Servicio IsNot Nothing Then

                        If facfactudeta.Impuesto = "F" Then
                            facfactudeta.Descuento = 0
                            Me._ventana.Desactivar_Descuento = False
                        End If

                        If desglose_servicio.Id = "G" Then
                            facfactudeta.Impuesto = "F"
                            facfactudeta.Descuento = 0
                            Me._ventana.Desactivar_Descuento = False
                        End If

                    End If
                End If 'If Me._ventana.Desglose = True Then

                facfactudeta.BBsel = Me._ventana.Seleccion
                facfactudeta.BDesglose = Me._ventana.Desglose
                'GUARDAR EL DETALLE DE LA 
                Dim factura_ As New FacFactura
                Dim guardar As Boolean = False
                factura_.Id = 0
                facfactudeta.Factura = factura_
                'guardar = _FacFactuDetasServicios.InsertarOModificar(facfactudeta, UsuarioLogeado.Hash)
                'If guardar = True Then
                agrega_detalle(facfactudeta)
                recalcular(moneda.Id)
                ' End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub agrega_detalle(ByVal detalle_ As FacFactuDetalle)
            Dim detallesaux As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            Dim detalles As New List(Of FacFactuDetalle)
            Dim i As Integer = 0
            Me._ventana.VerTipo = "13"
            If detallesaux IsNot Nothing Then
                detalles = detallesaux
                i = detallesaux.Count
            End If
            detalles.Add(New FacFactuDetalle)
            If detalle_.NCantidad Is Nothing Then
                detalle_.NCantidad = 1
            Else
                If detalle_.BBsel = True And detalle_.NCantidad = 0 Then
                    detalle_.NCantidad = 1
                End If
            End If
            detalles(i) = detalle_
            Me._ventana.ResultadosFacFactuDeta = Nothing
            Me._ventana.ResultadosFacFactuDeta = detalles
            Me._ventana.FacFactuDeta_Seleccionado = detalle_
        End Sub

        Public Sub Salir()
            Me._ventana.VerTipo = "13"
        End Sub



        Public Sub eliminar_operacion_detalle_tm(ByVal valor As String, ByVal contador As Integer, ByVal servicio As FacServicio)
            Dim FacOperacionDetalleTm As List(Of FacOperacionDetalleTm)
            Dim FacOperacionDetalleTmaux As New FacOperacionDetalleTm
            Dim elim As Boolean = False
            FacOperacionDetalleTmaux.Codigo = valor
            FacOperacionDetalleTmaux.Usuario = UsuarioLogeado
            FacOperacionDetalleTmaux.Detalle = contador
            FacOperacionDetalleTm = _FacOperacionDetalleTmsServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmaux)
            If FacOperacionDetalleTm IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDetalleTm.Count - 1
                    elim = _FacOperacionDetalleTmsServicios.Eliminar(FacOperacionDetalleTm(i), UsuarioLogeado.Hash)
                Next
            End If
            FacOperacionDetalleTmaux.Id = "ND"
            FacOperacionDetalleTmaux.Codigo = valor
            FacOperacionDetalleTmaux.Usuario = UsuarioLogeado
            FacOperacionDetalleTmaux.Detalle = contador
            FacOperacionDetalleTmaux.Servicio = servicio
            elim = _FacOperacionDetalleTmsServicios.InsertarOModificar(FacOperacionDetalleTmaux, UsuarioLogeado.Hash)
        End Sub

        Public Sub eliminar_operacion_detalle_tm_usuario()
            Dim FacOperacionDetalleTm As List(Of FacOperacionDetalleTm)
            Dim FacOperacionDetalleTmaux As New FacOperacionDetalleTm
            Dim elim As Boolean = False
            FacOperacionDetalleTmaux.Usuario = UsuarioLogeado
            FacOperacionDetalleTm = _FacOperacionDetalleTmsServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmaux)
            If FacOperacionDetalleTm IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDetalleTm.Count - 1
                    elim = _FacOperacionDetalleTmsServicios.Eliminar(FacOperacionDetalleTm(i), UsuarioLogeado.Hash)
                Next
            End If
        End Sub
        Public Sub operacion_detalle_(ByVal idfactura As String)
            elim_operacion_detalle_(idfactura)
            agregar_modificar_operacion_detalle_(idfactura)

            'Dim FacOperacionDeta As List(Of FacOperacionDeta)
            'Dim FacOperacionDetaaux As New FacOperacionDeta
            'Dim factura As New FacFactura
            'factura.Id = idfactura
            'Dim elim As Boolean = False

            'FacOperacionDetaaux.Factura = factura
            'FacOperacionDeta = _FacOperacionDetasServicios.ObtenerFacOperacionDetasFiltro(FacOperacionDetaaux)
            'If FacOperacionDeta IsNot Nothing Then
            '    For i As Integer = 0 To FacOperacionDeta.Count - 1
            '        elim = _FacOperacionDetasServicios.Eliminar(FacOperacionDeta(i), UsuarioLogeado.Hash)
            '    Next
            'End If
            'Dim FacFactuDetas As List(Of FacFactuDeta) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDeta))
            'Dim FacFactura As New FacFactura

            'Dim guardar As Boolean
            'For i As Integer = 0 To FacFactuDetas.Count - 1
            '    'if (itiposerv.fac_detalles_pro != "C" & itiposerv.fac_detalles_pro != "E") & (bsel.fac_detalles_pro = 1 | bsel.fac_detalles_pro = "T")
            '    If (FacFactuDetas(i).TipoServicio <> "C" And FacFactuDetas(i).TipoServicio <> "E") And (FacFactuDetas(i).BBsel = True) Then
            '        FacOperacionDetaaux.Factura = factura
            '        FacOperacionDeta = _FacOperacionDetasServicios.ObtenerFacOperacionDetasFiltro(FacOperacionDetaaux)
            '        If FacOperacionDeta Is Nothing Then
            '            FacOperacionDetaaux.Codigo = FacFactuDetas(i).Codigo
            '            FacOperacionDetaaux.Factura = FacFactuDetas(i).Factura
            '            FacOperacionDetaaux.Detalle = FacFactuDetas(i).Id
            '            FacOperacionDetaaux.Id = "ND"
            '            FacOperacionDetaaux.Servicio = FacFactuDetas(i).Servicio.Id                        
            '            guardar = _FacOperacionDetasServicios.InsertarOModificar(FacFactuDetas(i), UsuarioLogeado.Hash)
            '        End If
            '    End If
            'Next
        End Sub
        Public Sub elim_operacion_detalle_(ByVal idfactura As String)
            Dim FacOperacionDeta As List(Of FacOperacionDetalle)
            Dim FacOperacionDetaaux As New FacOperacionDetalle
            Dim factura As New FacFactura
            factura.Id = idfactura
            Dim elim As Boolean = False

            FacOperacionDetaaux.Factura = factura
            FacOperacionDeta = _FacOperacionDetaServicios.ObtenerFacOperacionDetallesFiltro(FacOperacionDetaaux)
            If FacOperacionDeta IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDeta.Count - 1
                    elim = _FacOperacionDetaServicios.Eliminar(FacOperacionDeta(i), UsuarioLogeado.Hash)
                Next
            End If
        End Sub

        Public Sub agregar_modificar_operacion_detalle_(ByVal idfactura As String)
            Dim FacOperacionDeta As List(Of FacOperacionDetalle)
            Dim FacOperacionDetaaux As New FacOperacionDetalle
            Dim factura As New FacFactura
            factura.Id = idfactura

            Dim FacFactuDetas As List(Of FacFactuDetalle) = DirectCast(Me._ventana.ResultadosFacFactuDeta, List(Of FacFactuDetalle))
            Dim FacFactura As New FacFactura

            Dim guardar As Boolean
            For i As Integer = 0 To FacFactuDetas.Count - 1
                'if (itiposerv.fac_detalles_pro != "C" & itiposerv.fac_detalles_pro != "E") & (bsel.fac_detalles_pro = 1 | bsel.fac_detalles_pro = "T")
                If (FacFactuDetas(i).TipoServicio <> "C" And FacFactuDetas(i).TipoServicio <> "E") And (FacFactuDetas(i).BBsel = True) Then
                    FacOperacionDetaaux.Factura = factura
                    FacOperacionDeta = _FacOperacionDetaServicios.ObtenerFacOperacionDetallesFiltro(FacOperacionDetaaux)
                    If FacOperacionDeta.Count <= 0 Then
                        Dim FacOperacionDetaaux2 As New FacOperacionDetalle
                        FacOperacionDetaaux2.Codigo = FacFactuDetas(i).Codigo
                        FacOperacionDetaaux2.Factura = FacFactuDetas(i).Factura
                        FacOperacionDetaaux2.Detalle = FacFactuDetas(i).Id
                        FacOperacionDetaaux2.Id = "ND"
                        FacOperacionDetaaux2.Servicio = FacFactuDetas(i).Servicio
                        guardar = _FacOperacionDetaServicios.InsertarOModificar(FacOperacionDetaaux2, UsuarioLogeado.Hash)
                    End If
                End If
            Next
        End Sub

        Public Sub operacion_detalle_tm_(ByVal idfactura As String)
            Dim FacOperacionDetaTm As List(Of FacOperacionDetalleTm)
            Dim FacOperacionDetaTmaux As New FacOperacionDetalleTm
            Dim factura As New FacFactura
            factura.Id = idfactura
            Dim elim As Boolean = False

            'FacOperacionDetaTmaux.Factura = factura
            FacOperacionDetaTmaux.Usuario = UsuarioLogeado
            FacOperacionDetaTm = _FacOperacionDetaTmServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetaTmaux)
            If FacOperacionDetaTm IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDetaTm.Count - 1
                    elim = _FacOperacionDetaTmServicios.Eliminar(FacOperacionDetaTm(i), UsuarioLogeado.Hash)
                Next
            End If

            Dim FacOperacionDetalleTm As List(Of FacOperacionDetalleTm)
            Dim FacOperacionDetalleTmaux As New FacOperacionDetalleTm
            FacOperacionDetalleTmaux.Usuario = UsuarioLogeado
            FacOperacionDetalleTm = _FacOperacionDetalleTmsServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmaux)
            If FacOperacionDetalleTm IsNot Nothing Then
                Dim guardar As Boolean
                For i As Integer = 0 To FacOperacionDetalleTm.Count - 1
                    Dim FacOperacionDeta As New FacOperacionDetalle
                    FacOperacionDeta.Codigo = FacOperacionDetalleTm(i).Codigo
                    FacOperacionDeta.Factura = factura
                    FacOperacionDeta.Detalle = FacOperacionDetalleTm(i).Detalle
                    FacOperacionDeta.Id = "ND"
                    FacOperacionDeta.Servicio = FacOperacionDetalleTm(i).Servicio
                    guardar = _FacOperacionDetaServicios.InsertarOModificar(FacOperacionDeta, UsuarioLogeado.Hash)

                    Dim FacOperacionDetaTmaux2 As New FacOperacionDetalleTm
                    FacOperacionDetaTmaux2.Id = FacOperacionDetalleTm(i).Id
                    FacOperacionDetaTmaux2.Codigo = FacOperacionDetalleTm(i).Codigo
                    FacOperacionDetaTmaux2.Detalle = FacOperacionDetalleTm(i).Detalle
                    'FacOperacionDetaTmaux2.Factura = factura
                    FacOperacionDetaTmaux2.Usuario = FacOperacionDetalleTm(i).Usuario
                    FacOperacionDetaTmaux2.Servicio = FacOperacionDetalleTm(i).Servicio
                    guardar = _FacOperacionDetaTmServicios.InsertarOModificar(FacOperacionDetaTmaux2, UsuarioLogeado.Hash)
                Next
            End If
        End Sub

        Public Function generar_multiples_marcas() As String()
            Dim lista(3) As String
            lista(0) = ""
            lista(1) = ""
            lista(2) = ""

            Dim Marcas As New List(Of MarcaSelec)
            ' si el grid de multiples marcas tiene ya porlomenos un registro
            If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then
                Marcas = DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec))

                For i As Integer = 0 To Marcas.Count - 1
                    If lista(1) = "" Then
                        lista(1) = Marcas(i).Id
                    Else
                        lista(1) = lista(1) & "," & Marcas(i).Id
                    End If

                    If lista(2) = "" Then
                        lista(2) = Marcas(i).PrimeraReferencia
                    Else
                        lista(2) = lista(2) & "," & Marcas(i).PrimeraReferencia
                    End If
                Next

                lista(0) = Marcas.Count
            End If

            Return lista
        End Function

        Public Function generar_marca() As String()
            Dim lista(4) As String
            lista(0) = ""
            lista(1) = ""
            lista(2) = ""
            lista(3) = ""

            Dim _Marca As Marca = DirectCast(Me._ventana.Marcas_Seleccionado, Marca)
            If _Marca IsNot Nothing Then
                lista(1) = _Marca.Id
                lista(2) = _Marca.PrimeraReferencia
                lista(0) = 1
                '"cmarca=%%CMARCA.MYP_MARCASitipo=%%itipo.MYP_MARCAS"
                '$$LST_SELECCION2 = "cmarca=%%CMARCA.MYP_MARCASitipo=%%itipo.MYP_MARCAS"
                lista(3) = _Marca.Tipo
            End If

            Return lista
        End Function


        Public Function generar_multiples_Patentes() As String()
            Dim lista(3) As String
            lista(0) = ""
            lista(1) = ""
            lista(2) = ""

            Dim Patentes As New List(Of PatenteSelec)
            ' si el grid de multiples Patentes tiene ya porlomenos un registro
            If (DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec)) IsNot Nothing) Then
                Patentes = DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec))

                For i As Integer = 0 To Patentes.Count - 1
                    If lista(1) = "" Then
                        lista(1) = Patentes(i).Id
                    Else
                        lista(1) = lista(1) & "," & Patentes(i).Id
                    End If

                    If lista(2) = "" Then
                        lista(2) = Patentes(i).PrimeraReferencia
                    Else
                        lista(2) = lista(2) & "," & Patentes(i).PrimeraReferencia
                    End If
                Next

                lista(0) = Patentes.Count
            End If

            Return lista
        End Function

        Public Function generar_Patente() As String()
            Dim lista(4) As String
            lista(0) = ""
            lista(1) = ""
            lista(2) = ""
            lista(3) = ""

            Dim _Patente As Patente = DirectCast(Me._ventana.Patentes_Seleccionado, Patente)
            If _Patente IsNot Nothing Then
                lista(1) = _Patente.Id
                lista(2) = _Patente.PrimeraReferencia
                lista(0) = 1
                '"cmarca=%%CMARCA.MYP_MARCASitipo=%%itipo.MYP_MARCAS"
                '$$LST_SELECCION2 = "cmarca=%%CMARCA.MYP_MARCASitipo=%%itipo.MYP_MARCAS"
                lista(3) = _Patente.Tipo
            End If

            Return lista
        End Function

        Public Sub FAC_BSERV(ByVal list_dat As String(), ByRef list_ret As String, ByRef list_ret_es As String)
            Dim v_cadena As String = Nothing
            Dim v_cadena_es As String = Nothing
            Dim v_cadtmp As String = Nothing
            Dim v_cadtmp_es As String = Nothing
            Dim v_envia As String = Nothing
            Dim v_bol_con As Boletin = Nothing
            Dim v_lista_mp, v_lista_mp_es, v_lista_ot(10), v_lista_ot_es(10) As String
            v_lista_mp = ""
            v_lista_mp_es = ""
            Dim v_lista_1(22) As String
            v_lista_1(0) = "" '("Marca")
            v_lista_1(1) = "" '("Inscripcion")
            v_lista_1(2) = "" '("Registro")
            v_lista_1(3) = "" '("Tipo_Clase")
            v_lista_1(4) = "" '("Bol_Con")
            v_lista_1(5) = "" '("Fec_Bol_Con")
            v_lista_1(6) = "" '("Interesado")
            v_lista_1(7) = "" '("Tdoc_Marca")
            v_lista_1(8) = "" '("Tipo_Marca")
            v_lista_1(9) = "" '("Clasen")
            v_lista_1(10) = "" '("Clasificacionn")
            v_lista_1(11) = "" '("Clasificacion")
            v_lista_1(12) = "" '("Clase")
            v_lista_1(13) = "" '("Patente")
            v_lista_1(14) = "" '("Tdoc_Patente")
            v_lista_1(15) = "" '("Tipo_Patente")
            v_lista_1(16) = "" '("Pais")
            v_lista_1(17) = "" '("Tdoc_Traducc")
            v_lista_1(18) = "" '("Anualidad")
            v_lista_1(19) = "" '("Recurso")
            v_lista_1(20) = "" '("Matarial")
            v_lista_1(21) = "" '("NPaginas"

            Dim v_lista_1_es(22) As String
            v_lista_1_es(0) = "" '("Marca")
            v_lista_1_es(1) = "" '("Inscripcion")
            v_lista_1_es(2) = "" '("Registro")
            v_lista_1_es(3) = "" '("Tipo_Clase")
            v_lista_1_es(4) = "" '("Bol_Con")
            v_lista_1_es(5) = "" '("Fec_Bol_Con")
            v_lista_1_es(6) = "" '("Interesado")
            v_lista_1_es(7) = "" '("Tdoc_Marca")
            v_lista_1_es(8) = "" '("Tipo_Marca")
            v_lista_1_es(9) = "" '("Clasen")
            v_lista_1_es(10) = "" '("Clasificacionn")
            v_lista_1_es(11) = "" '("Clasificacion")
            v_lista_1_es(12) = "" '("Clase")
            v_lista_1_es(13) = "" '("Patente")
            v_lista_1_es(14) = "" '("Tdoc_Patente")
            v_lista_1_es(15) = "" '("Tipo_Patente")
            v_lista_1_es(16) = "" '("Pais")
            v_lista_1_es(17) = "" '("Tdoc_Traducc")
            v_lista_1_es(18) = "" '("Anualidad")
            v_lista_1_es(19) = "" '("Recurso")
            v_lista_1_es(20) = "" '("Matarial")
            v_lista_1_es(21) = "" '("NPaginas"

            'getitem/id v_codser, list_dat, "codser"
            'getitem/id v_tipo, list_dat, "tipo"
            'getitem/id v_codmp, list_dat, "codigomp"
            'getitem/id v_idioma, list_dat, "cidioma"
            'getitem/id v_local, list_dat, "local"
            'getitem/id v_codtdoc, list_dat, "codtdoc"
            'getitem/id v_codtra, list_dat, "codtra"
            'getitem/id v_tipomarc, list_dat, "tipomarc"
            'getitem/id v_tipopate, list_dat, "tipopate"
            'getitem/id v_clasi, list_dat, "clasi"
            'getitem/id v_pagina, list_dat, "pagina"
            'getitem/id v_anual, list_dat, "anual"
            'getitem/id v_mult_marc, list_dat, "multi_mar"
            'getitem/id v_mult_pate, list_dat, "multi_pate"
            'getitem/id v_bsel, list_dat, "bsel"
            'getitem/id v_anualidad, list_dat, "anualidad"
            'getitem/id v_recurso, list_dat, "recurso"
            'getitem/id v_material, list_dat, "material"
            Dim v_codser As String = list_dat(0) '("codser")
            Dim v_tipo As String = list_dat(1) '("tipo")
            Dim v_codmp As String = list_dat(2) '("codigomp")
            Dim v_idioma As String = list_dat(3) '("cidioma")
            Dim v_local As String = list_dat(4) '("local")
            Dim v_codtdoc As String = list_dat(6) '("codtdoc")
            Dim v_codtra As String = list_dat(7) '("codtra")
            Dim v_tipomarc As String = list_dat(8) '("tipomarc")
            Dim v_tipopate As String = list_dat(9) '("tipopate")
            Dim v_clasi As String = list_dat(10) '("clasi")
            Dim v_pagina As String = list_dat(11) '("pagina")
            Dim v_anual As String = list_dat(12) '("anual")
            Dim v_mult_marc As String = list_dat(13) '("multi_mar")
            Dim v_mult_pate As String = list_dat(14) '("multi_pate")
            Dim v_bsel As String = list_dat(15) '("bsel")
            Dim v_anualidad As String = list_dat(16) '("anualidad")
            Dim v_recurso As String = list_dat(17) '("recurso")
            Dim v_material As String = list_dat(18) '("material")

            Dim v_codmult As String = Nothing
            Dim Marcas As IList(Of Marca)
            Dim Patentes As IList(Of Patente)
            Dim servicio As FacServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio).Servicio
            v_codmult = servicio.Codmult
            Try


                Dim imult As String = servicio.Imult.ToString
                If v_idioma = "ES" Then
                    If servicio.Imult <> "" And v_bsel = "False" Then
                        v_cadena = servicio.Detalles_Esp
                        v_cadena_es = servicio.Detalles_Esp
                    Else
                        v_cadena = servicio.Detalle_Esp
                        v_cadena_es = servicio.Detalle_Esp
                    End If
                Else
                    If servicio.Imult <> "" And v_bsel = "False" Then
                        v_cadena = servicio.Detalles_Ing
                        v_cadena_es = servicio.Detalles_Esp
                    Else
                        v_cadena = servicio.Detalle_Ing
                        v_cadena_es = servicio.Detalle_Esp
                    End If

                End If

                If v_local = "N" Then

                    If (v_tipo = "M") Then
                        If v_codmp <> "" Then
                            Marcas = encontrarmarca(v_codmp)
                            If Marcas(0).BoletinConcesion IsNot Nothing Then
                                v_bol_con = Marcas(0).BoletinConcesion
                            End If
                            v_lista_1(0) = Marcas(0).Descripcion '("Marca")
                            v_lista_1(1) = Marcas(0).CodigoInscripcion '("Inscripcion")
                            v_lista_1(2) = Marcas(0).CodigoRegistro '("Registro")
                            If Marcas(0).Internacional IsNot Nothing Then
                                v_lista_1(3) = Marcas(0).Internacional.Id '("Tipo_Clase")
                                v_lista_1_es(3) = Marcas(0).Internacional.Id '("Tipo_Clase")
                            End If
                            If Marcas(0).BoletinConcesion IsNot Nothing Then
                                v_lista_1(4) = Marcas(0).BoletinConcesion.Id '("Bol_Con")
                                v_lista_1(5) = Marcas(0).BoletinConcesion.FechaBoletin '("Fec_Bol_Con")
                                v_lista_1_es(4) = Marcas(0).BoletinConcesion.Id '("Bol_Con")
                                v_lista_1_es(5) = Marcas(0).BoletinConcesion.FechaBoletin '("Fec_Bol_Con")
                            End If
                            If Marcas(0).Interesado IsNot Nothing Then
                                v_lista_1(6) = Marcas(0).Interesado.Nombre '("Interesado")
                                v_lista_1_es(6) = Marcas(0).Interesado.Nombre '("Interesado")
                            End If
                            v_lista_1_es(0) = Marcas(0).Descripcion '("Marca")
                            v_lista_1_es(1) = Marcas(0).CodigoInscripcion '("Inscripcion")
                            v_lista_1_es(2) = Marcas(0).CodigoRegistro '("Registro")

                            ' para nacional
                            If Marcas(0).Nacional IsNot Nothing Then
                                Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")

                                v_lista_1(9) = Marcas(0).Nacional.Id '("Clasen")
                                v_lista_1_es(9) = Marcas(0).Nacional.Id '("Clasen")

                                If (v_idioma = "ES") Then
                                    v_lista_1(10) = tipoclase.Doc_Esp '("Clasificacionn")
                                    v_lista_1_es(10) = tipoclase.Doc_Esp '("Clasificacionn")
                                Else
                                    v_lista_1(10) = tipoclase.Doc_Ingl '("Clasificacionn")
                                    v_lista_1_es(10) = tipoclase.Doc_Esp '("Clasificacionn")
                                End If
                            End If

                            If (v_clasi = "LC" Or v_clasi = "NC") Then
                                Dim tipoclase As TipoClase = buscar_tipo_clase_id(v_clasi)
                                If (v_idioma = "ES") Then
                                    v_lista_1(11) = tipoclase.Doc_Esp '("Clasificacion")
                                    v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                Else
                                    v_lista_1(11) = tipoclase.Doc_Ingl '("Clasificacion")
                                    v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                End If
                            Else
                                ' para internacional
                                If Marcas(0).Internacional IsNot Nothing Then
                                    Dim tipoclase As TipoClase = buscar_tipo_clase_id("I")

                                    v_lista_1(12) = Marcas(0).Internacional.Id '("Clase")
                                    v_lista_1_es(12) = Marcas(0).Internacional.Id '("Clase")

                                    If (v_idioma = "ES") Then
                                        v_lista_1(11) = tipoclase.Doc_Esp '("Clasificacion")
                                        v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                    Else
                                        v_lista_1(11) = tipoclase.Doc_Ingl '("Clasificacion")
                                        v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                    End If

                                Else
                                    Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")

                                    v_lista_1(12) = Marcas(0).Nacional.Id '("Clase")
                                    v_lista_1_es(12) = Marcas(0).Nacional.Id '("Clase")

                                    If (v_idioma = "ES") Then
                                        v_lista_1(11) = tipoclase.Doc_Esp '("Clasificacion")
                                        v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                    Else
                                        v_lista_1(11) = tipoclase.Doc_Ingl '("Clasificacion")
                                        v_lista_1_es(11) = tipoclase.Doc_Esp '("Clasificacion")
                                    End If

                                End If
                            End If

                        End If

                        If v_codtdoc <> "" Then
                            Dim documento_marca As DocumentosMarca = DirectCast(Me._ventana.DocumentoMarca_Seleccionado, DocumentosMarca)
                            If (v_idioma = "ES") Then
                                v_lista_1(7) = documento_marca.Doc_Esp '("Tdoc_Marca")
                                v_lista_1_es(7) = documento_marca.Doc_Esp '("Tdoc_Marca")
                            Else
                                v_lista_1(7) = documento_marca.Doc_Ingl '("Tdoc_Marca")
                                v_lista_1_es(7) = documento_marca.Doc_Esp '("Tdoc_Marca")
                            End If
                        End If

                        If (v_tipomarc <> "") Then
                            Dim tipomarca As TipoMarca = buscar_tipo_marca_id(v_tipomarc)
                            If tipomarca IsNot Nothing Then
                                If (v_idioma = "ES") Then
                                    v_lista_1(8) = tipomarca.Doc_Esp '("Tipo_Marca")
                                    v_lista_1_es(8) = tipomarca.Doc_Esp '("Tipo_Marca")
                                Else
                                    v_lista_1(8) = tipomarca.Doc_Ingl '("Tipo_Marca")
                                    v_lista_1_es(8) = tipomarca.Doc_Esp '("Tipo_Marca")
                                End If
                            End If
                        End If
                    End If 'v_tipo = "M"

                    If (v_tipo = "P") Then
                        If v_codmp <> "" Then
                            v_tipopate = ""
                            Patentes = encontrarPatente(v_codmp)
                            v_tipopate = Patentes(0).Tipo
                            v_lista_1(13) = Patentes(0).Descripcion '("Patente")
                            v_lista_1_es(13) = Patentes(0).Descripcion '("Patente")
                            If Patentes(0).BoletinConcesion IsNot Nothing Then
                                v_bol_con = Patentes(0).BoletinConcesion
                            End If
                            v_lista_1(1) = Patentes(0).CodigoInscripcion '("Inscripcion")
                            v_lista_1(2) = Patentes(0).CodigoRegistro '("Registro")
                            'If Patentes(0).Internacional IsNot Nothing Then
                            'v_lista_1(3) = Patentes(0).Internacion '("Tipo_Clase")
                            'End If
                            If Patentes(0).BoletinConcesion IsNot Nothing Then
                                v_lista_1(4) = Patentes(0).BoletinConcesion.Id '("Bol_Con")
                                v_lista_1_es(4) = Patentes(0).BoletinConcesion.Id '("Bol_Con")
                                v_lista_1(5) = Patentes(0).BoletinConcesion.FechaBoletin '("Fec_Bol_Con")
                                v_lista_1_es(5) = Patentes(0).BoletinConcesion.FechaBoletin '("Fec_Bol_Con")
                            End If
                            If Patentes(0).Interesado IsNot Nothing Then
                                v_lista_1(6) = Patentes(0).Interesado.Nombre '("Interesado")
                                v_lista_1_es(6) = Patentes(0).Interesado.Nombre '("Interesado")
                            End If
                            v_lista_1_es(1) = Patentes(0).CodigoInscripcion '("Inscripcion")
                            v_lista_1_es(2) = Patentes(0).CodigoRegistro '("Registro")
                            'v_lista_1_es(3) = Patentes(0).Internacional.Id '("Tipo_Clase")                            

                        End If
                        If v_codtdoc <> "" Then
                            Dim documento_Patente As DocumentosPatente = DirectCast(Me._ventana.DocumentoPatente_Seleccionado, DocumentosPatente)
                            If (v_idioma = "ES") Then
                                v_lista_1(14) = documento_Patente.Doc_Esp '("Tdoc_Patente")
                                v_lista_1_es(14) = documento_Patente.Doc_Esp '("Tdoc_Patente")
                            Else
                                v_lista_1(14) = documento_Patente.Doc_Ingl '("Tdoc_Patente")
                                v_lista_1_es(14) = documento_Patente.Doc_Esp '("Tdoc_Patente")
                            End If
                        End If

                        If (v_tipopate <> "") Then
                            Dim tipoPatente As TipoPatente = buscar_tipo_patente_id(v_tipopate)
                            If tipoPatente IsNot Nothing Then
                                If (v_idioma = "ES") Then
                                    v_lista_1(15) = tipoPatente.Doc_Esp '("Tipo_Patente")
                                    v_lista_1_es(15) = tipoPatente.Doc_Esp '("Tipo_Patente")
                                Else
                                    v_lista_1(15) = tipoPatente.Doc_Ingl '("Tipo_Patente")
                                    v_lista_1_es(15) = tipoPatente.Doc_Esp '("Tipo_Patente")
                                End If
                            End If
                        End If
                    End If 'v_tipo = "P"

                Else 'v_local <> "N"

                    If (v_tipo = "M") Then
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        'if (v_codmp != "")
                        '   clear/e "mpe_mdonde"
                        '   cmarca.mpe_mdonde/init = v_codmp
                        '   retrieve/e "mpe_mdonde"
                        '   if ($status >= 0)
                        '               v_lista_1 = "Clase=%%XCLASE.mpe_mdondeInscripcion=%%cinscripcion.mpe_mdonde"
                        '               v_lista_1_es = "Clase=%%XCLASE.mpe_mdondeInscripcion=%%cinscripcion.mpe_mdonde"
                        '           End If
                        '   clear/e "myp_paises"
                        '   cpais.myp_paises/init = cpais.mpe_mdonde
                        '   retrieve/e "myp_paises"
                        '   if ($status >= 0)
                        '               v_lista_1 = "%%v_lista_1Pais=%%xpais.myp_paises"
                        '               v_lista_1_es = "%%v_lista_1Pais=%%xpais.myp_paises"
                        '           End If
                        '       End If
                    End If

                    If (v_tipo = "P") Then

                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        '''NOTAAAAA Falta realizar la entidad mpe_mdonde
                        'if (v_codmp != "")
                        'clear/e "mpe_pdonde"
                        'cpatente.mpe_pdonde/init = v_codmp
                        'retrieve/e "mpe_pdonde"
                        'if ($status >= 0)
                        '            v_lista_1 = "Inscripcion=%%cinscripcion.mpe_pdonde"
                        '            v_lista_1_es = "Inscripcion=%%cinscripcion.mpe_pdonde"
                        '        End If
                        'clear/e "myp_paises"
                        'cpais.myp_paises/init = cpais.mpe_pdonde
                        'retrieve/e "myp_paises"
                        'if ($status >= 0)
                        '            v_lista_1 = "%%v_lista_1Pais=%%xpais.myp_paises"
                        '            v_lista_1_es = "%%v_lista_1Pais=%%xpais.myp_paises"
                        '        End If
                        '    End If
                    End If
                End If 'v_local = "N"

                If (v_codtra <> "") Then
                    Dim documento_traduccion As DocumentosTraduccion = DirectCast(Me._ventana.DocumentoTraduccion_Seleccionado, DocumentosTraduccion)
                    If (v_idioma = "ES") Then
                        v_lista_1(17) = documento_traduccion.Doc_Esp '("Tdoc_Traducc")
                        v_lista_1_es(17) = documento_traduccion.Doc_Esp '("Tdoc_Traducc")
                    Else
                        v_lista_1_es(17) = documento_traduccion.Doc_Ingl '("Tdoc_Traducc")
                        v_lista_1_es(17) = documento_traduccion.Doc_Esp '("Tdoc_Traducc")
                    End If
                End If

                If (v_anual <> "") Then
                    Dim Anualidad As FacAnualidad = DirectCast(Me._ventana.Anualidad_Seleccionado, FacAnualidad)
                    If (v_idioma = "ES") Then
                        v_lista_1(18) = Anualidad.Doc_Esp '("Anualidad")
                        v_lista_1_es(18) = Anualidad.Doc_Esp '("Anualidad")
                    Else
                        v_lista_1_es(18) = Anualidad.Doc_Ingl '("Anualidad")
                        v_lista_1_es(18) = Anualidad.Doc_Esp '("Anualidad")
                    End If
                End If

                If (v_recurso <> "") Then
                    Dim Recurso As FacRecurso = DirectCast(Me._ventana.Recurso_Seleccionado, FacRecurso)
                    If (v_idioma = "ES") Then
                        v_lista_1(19) = Recurso.Doc_Esp '("Recurso")
                        v_lista_1_es(19) = Recurso.Doc_Esp '("Recurso")
                    Else
                        v_lista_1_es(19) = Recurso.Doc_Ingl '("Recurso")
                        v_lista_1_es(19) = Recurso.Doc_Esp '("Recurso")
                    End If
                End If

                If (v_material <> "") Then
                    Dim Material As Material = DirectCast(Me._ventana.Material_Seleccionado, Material)
                    If (v_idioma = "ES") Then
                        v_lista_1(20) = Material.Doc_Esp '("Material")
                        v_lista_1_es(20) = Material.Doc_Esp '("Material")
                    Else
                        v_lista_1_es(20) = Material.Doc_Ingl '("Material")
                        v_lista_1_es(20) = Material.Doc_Esp '("Material")
                    End If
                End If

                If (v_pagina <> "") Then
                    v_lista_1_es(21) = v_pagina '("NPaginas")
                    v_lista_1_es(21) = v_pagina '("NPaginas")
                End If

                ' Remplazar en la Cadena con valores de la Lista
                If ((v_mult_pate = "" And v_mult_marc = "") Or (v_mult_pate = Nothing And v_mult_marc = Nothing)) Then

                    v_cadtmp = v_cadena
                    v_cadtmp_es = v_cadena_es
                    v_envia = "|Marca|"
                    ''''''''''''''''''''''''''''''''(MARCA)
                    sustituye(v_cadtmp, v_envia, v_lista_1(0), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(0), v_cadtmp_es)

                    v_envia = "|Inscripcion|"
                    ''''''''''''''''''''''''''''''''(Inscripcion)
                    sustituye(v_cadtmp, v_envia, v_lista_1(1), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(1), v_cadtmp_es)

                    v_envia = "|Clase|"
                    ''''''''''''''''''''''''''''''''(Clase)
                    sustituye(v_cadtmp, v_envia, v_lista_1(12), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(12), v_cadtmp_es)

                    v_envia = "|Clasen|"
                    ''''''''''''''''''''''''''''''''(Clasen)
                    sustituye(v_cadtmp, v_envia, v_lista_1(9), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(9), v_cadtmp_es)

                    v_envia = "|Registro|"
                    ''''''''''''''''''''''''''''''''(Registro)
                    sustituye(v_cadtmp, v_envia, v_lista_1(2), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(2), v_cadtmp_es)

                    v_envia = "|Interesado|"
                    ''''''''''''''''''''''''''''''''(Interesado)
                    sustituye(v_cadtmp, v_envia, v_lista_1(6), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(6), v_cadtmp_es)

                    v_envia = "|Patente|"
                    ''''''''''''''''''''''''''''''''(Patente)
                    sustituye(v_cadtmp, v_envia, v_lista_1(13), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(13), v_cadtmp_es)

                    v_envia = "|Pais|"
                    ''''''''''''''''''''''''''''''''(Pais)
                    sustituye(v_cadtmp, v_envia, v_lista_1(16), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(16), v_cadtmp_es)

                    v_envia = "|NPaginas|"
                    ''''''''''''''''''''''''''''''''(NPaginas)
                    sustituye(v_cadtmp, v_envia, v_lista_1(21), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(21), v_cadtmp_es)

                    v_envia = "|FechaFax|"
                    ''''''''''este esta pendiente por verificar''''''''''''''''''''''(FechaFax)
                    'sustituye(v_cadtmp, v_envia, v_lista_1(9), v_cadtmp)
                    'sustituye(v_cadtmp_es, v_envia, v_lista_1_es(9), v_cadtmp_es)

                    v_envia = "|Clasificacion|"
                    ''''''''''''''''''''''''''''''''(Clasificacion)
                    sustituye(v_cadtmp, v_envia, v_lista_1(11), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(11), v_cadtmp_es)

                    v_envia = "|Clasificacionn|"
                    ''''''''''''''''''''''''''''''''(Clasificacionn)
                    sustituye(v_cadtmp, v_envia, v_lista_1(10), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(10), v_cadtmp_es)

                    v_envia = "|Tipo_Marca|"
                    ''''''''''''''''''''''''''''''''(Tipo_Marca)
                    sustituye(v_cadtmp, v_envia, v_lista_1(8), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(8), v_cadtmp_es)

                    v_envia = "|Tipo_Patente|"
                    ''''''''''''''''''''''''''''''''(Tipo_Patente)
                    sustituye(v_cadtmp, v_envia, v_lista_1(15), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(15), v_cadtmp_es)

                    v_envia = "|Tipo_Clase|"
                    ''''''''''''''''''''''''''''''''(Tipo_Clase)
                    sustituye(v_cadtmp, v_envia, v_lista_1(3), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(3), v_cadtmp_es)

                    v_envia = "|Tdoc_Marca|"
                    ''''''''''''''''''''''''''''''''(Tdoc_Marca)
                    sustituye(v_cadtmp, v_envia, v_lista_1(7), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(7), v_cadtmp_es)

                    v_envia = "|Tdoc_Patente|"
                    ''''''''''''''''''''''''''''''''(Tdoc_Patente)
                    sustituye(v_cadtmp, v_envia, v_lista_1(14), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(14), v_cadtmp_es)

                    v_envia = "|Tdoc_Traducc|"
                    ''''''''''''''''''''''''''''''''(Tdoc_Traducc)
                    sustituye(v_cadtmp, v_envia, v_lista_1(17), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(17), v_cadtmp_es)

                    v_envia = "|Bol_Con|"
                    ''''''''''''''''''''''''''''''''(Bol_Con)
                    sustituye(v_cadtmp, v_envia, v_lista_1(4), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(4), v_cadtmp_es)

                    v_envia = "|Fec_Bol_Con|"
                    ''''''''''''''''''''''''''''''''(Fec_Bol_Con)
                    sustituye(v_cadtmp, v_envia, v_lista_1(5), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(5), v_cadtmp_es)

                    v_envia = "|Anualidad|"
                    ''''''''''''''''''''''''''''''''(Anualidad)
                    sustituye(v_cadtmp, v_envia, v_lista_1(18), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(18), v_cadtmp_es)

                    v_envia = "|Recurso|"
                    ''''''''''''''''''''''''''''''''(Recurso)
                    sustituye(v_cadtmp, v_envia, v_lista_1(19), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(19), v_cadtmp_es)

                    v_envia = "|Material|"
                    ''''''''''''''''''''''''''''''''(Material)
                    sustituye(v_cadtmp, v_envia, v_lista_1(20), v_cadtmp)
                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(20), v_cadtmp_es)
                End If 'if (v_mult_pate = "" & v_mult_marc = "")

                'para multis marcas
                If v_mult_marc <> "" And v_mult_marc <> Nothing Then
                    Dim Marcasselec As New List(Of MarcaSelec)
                    ' si el grid de multiples marcas tiene ya porlomenos un registro
                    If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then
                        Marcasselec = DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec))
                        v_lista_mp = ""
                        v_lista_mp_es = ""
                        'v_lista_ot = ""
                        'v_lista_ot_es = ""
                        Dim marca As Marca
                        For i As Integer = 0 To Marcasselec.Count - 1
                            marca = BuscarMarca_2(Marcasselec(i).Id)
                            If i < Marcasselec.Count - 1 Then
                                If (v_codmult = "M1") Then

                                    If (v_lista_mp = "") Then
                                        v_lista_mp = marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                        '"%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        'v_lista_mp_es = "%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        v_lista_mp_es = marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                    Else
                                        v_lista_mp = v_lista_mp & ";" & marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                        'v_lista_mp = "%%v_lista_mp;%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        'v_lista_mp_es = "%%v_lista_mp_es;%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        v_lista_mp_es = v_lista_mp_es & ";" & marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                    End If

                                    'nacional <>""
                                    If (marca.Nacional IsNot Nothing) AndAlso (Not marca.Nacional.Id.Equals("")) Then
                                        Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")

                                        v_lista_ot(0) = marca.Nacional.Id  'Clasen
                                        v_lista_ot_es(0) = marca.Nacional.Id  'Clasen
                                        If (v_idioma = "ES") Then
                                            v_lista_ot(1) = tipoclase.Doc_Esp 'Clasificacionn
                                            v_lista_ot_es(1) = tipoclase.Doc_Esp 'Clasificacionn
                                        Else
                                            v_lista_ot(1) = tipoclase.Doc_Ingl 'Clasificacionn
                                            v_lista_ot_es(1) = tipoclase.Doc_Esp 'Clasificacionn
                                        End If
                                    End If  'nacional <>""

                                    If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                        Dim tipoclase As TipoClase = buscar_tipo_clase_id(marca.Tipo)

                                        If (v_idioma = "ES") Then
                                            v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                            '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                            '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                            v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                        Else
                                            v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                            '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                            '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                            v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                        End If

                                    Else 'If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                        If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                            Dim tipoclase As TipoClase = buscar_tipo_clase_id("I")
                                            If (v_idioma = "ES") Then
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            Else
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            End If
                                        Else ' 'If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                            Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")
                                            If (v_idioma = "ES") Then
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            Else
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            End If
                                            v_lista_mp = v_lista_mp & " " & marca.Nacional.Id
                                            v_lista_mp_es = v_lista_mp_es & " " & marca.Nacional.Id
                                        End If 'If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                    End If 'If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                End If ' If (v_codmult = "M1") Then

                                If (v_codmult = "M2") Then
                                    If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then
                                        If v_lista_mp = Nothing Or v_lista_mp = "" Then
                                            v_lista_mp = marca.CodigoRegistro
                                            v_lista_mp_es = marca.CodigoRegistro
                                        Else
                                            v_lista_mp = v_lista_mp & ", " & marca.CodigoRegistro
                                            v_lista_mp_es = v_lista_mp_es & ", " & marca.CodigoRegistro
                                        End If
                                    Else ' If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then
                                        If v_lista_mp = Nothing Or v_lista_mp = "" Then
                                            v_lista_mp = marca.CodigoInscripcion
                                            v_lista_mp_es = marca.CodigoInscripcion
                                        Else
                                            v_lista_mp = v_lista_mp & ", " & marca.CodigoInscripcion
                                            v_lista_mp_es = v_lista_mp_es & ", " & marca.CodigoInscripcion
                                        End If
                                    End If ' If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then
                                End If 'If (v_codmult = "M2") Then
                            Else ' 'If i < Marcasselec.Count - 1 Then
                                If (v_codmult = "M1") Then

                                    If (v_lista_mp = "") Then
                                        v_lista_mp = marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                        '"%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        'v_lista_mp_es = "%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        v_lista_mp_es = marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                    Else
                                        v_lista_mp = v_lista_mp & ";" & marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                        'v_lista_mp = "%%v_lista_mp;%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        'v_lista_mp_es = "%%v_lista_mp_es;%%xmarca.myp_marcas, Serial No %%cinscripcion.myp_marcas"
                                        v_lista_mp_es = v_lista_mp_es & ";" & marca.Descripcion & ", Serial No " & marca.CodigoInscripcion
                                    End If

                                    Dim documento_marca As DocumentosMarca = DirectCast(Me._ventana.DocumentoMarca_Seleccionado, DocumentosMarca)
                                    If documento_marca IsNot Nothing Then
                                        If (v_idioma = "ES") Then
                                            v_lista_1(7) = documento_marca.Doc_Esp 'Tdoc_Marca
                                            v_lista_1_es(7) = documento_marca.Doc_Esp 'Tdoc_Marca
                                        Else
                                            v_lista_1(7) = documento_marca.Doc_Ingl 'Tdoc_Marca
                                            v_lista_1_es(7) = documento_marca.Doc_Esp 'Tdoc_Marca
                                        End If
                                    End If

                                    v_cadtmp = v_cadena
                                    v_cadtmp_es = v_cadena_es
                                    v_envia = "|Tdoc_Marca|"
                                    ''''''''''''''''''''''''''''''''(Tdoc_Marca)
                                    sustituye(v_cadtmp, v_envia, v_lista_1(7), v_cadtmp)
                                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(7), v_cadtmp_es)

                                    Dim tipomarca As TipoMarca = buscar_tipo_marca_id(marca.Tipo)
                                    If (v_idioma = "ES") Then
                                        v_lista_mp = v_lista_mp & " " & tipomarca.Doc_Esp
                                        '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                        '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                        v_lista_mp_es = v_lista_mp & " " & tipomarca.Doc_Esp
                                    Else
                                        v_lista_mp = v_lista_mp & " " & tipomarca.Doc_Ingl
                                        '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                        '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                        v_lista_mp_es = v_lista_mp & " " & tipomarca.Doc_Esp
                                    End If
                                    v_cadena = v_cadtmp
                                    v_cadena_es = v_cadtmp_es

                                    v_cadtmp = v_cadena
                                    v_cadtmp_es = v_cadena_es
                                    v_envia = "|Tipo_Marca|"
                                    ''''''''''''''''''''''''''''''''(Tipo_Marca)
                                    sustituye(v_cadtmp, v_envia, v_lista_1(8), v_cadtmp)
                                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(8), v_cadtmp_es)

                                    ''nacional <>""
                                    If (marca.Nacional IsNot Nothing) AndAlso (Not marca.Nacional.Id.Equals("")) Then
                                        Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")

                                        v_lista_ot(0) = marca.Nacional.Id  'Clasen
                                        v_lista_ot_es(0) = marca.Nacional.Id  'Clasen
                                        If (v_idioma = "ES") Then
                                            v_lista_ot(1) = tipoclase.Doc_Esp 'Clasificacionn
                                            v_lista_ot_es(1) = tipoclase.Doc_Esp 'Clasificacionn
                                        Else
                                            v_lista_ot(1) = tipoclase.Doc_Ingl 'Clasificacionn
                                            v_lista_ot_es(1) = tipoclase.Doc_Esp 'Clasificacionn
                                        End If
                                    End If  'nacional <>""

                                    If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                        Dim tipoclase As TipoClase = buscar_tipo_clase_id(marca.Tipo)

                                        If (v_idioma = "ES") Then
                                            v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                            '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                            '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                            v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                        Else
                                            v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                            '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                            '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                            v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                        End If

                                    Else 'If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                        If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                            Dim tipoclase As TipoClase = buscar_tipo_clase_id("I")
                                            If (v_idioma = "ES") Then
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            Else
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            End If
                                        Else ' 'If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                            Dim tipoclase As TipoClase = buscar_tipo_clase_id("N")
                                            If (v_idioma = "ES") Then
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Esp
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            Else
                                                v_lista_mp = v_lista_mp & " " & tipoclase.Doc_Ingl
                                                '"%%v_lista_mp %%XDOC_ESPE.fac_tipo_clase"
                                                '"%%v_lista_mp_es %%XDOC_ESPE.fac_tipo_clase"
                                                v_lista_mp_es = v_lista_mp & " " & tipoclase.Doc_Esp
                                            End If
                                            v_lista_mp = v_lista_mp & " " & marca.Nacional.Id
                                            v_lista_mp_es = v_lista_mp_es & " " & marca.Nacional.Id
                                        End If 'If ((marca.Internacional IsNot Nothing) AndAlso (Not marca.Internacional.Id.Equals(""))) Then
                                    End If 'If marca.Tipo = "LC" Or marca.Tipo = "NC" Then
                                End If ' If (v_codmult = "M1") Then

                                If (v_codmult = "M2") Then
                                    If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then
                                        If v_lista_mp = Nothing Or v_lista_mp = "" Then
                                            v_lista_mp = marca.CodigoRegistro
                                            v_lista_mp_es = marca.CodigoRegistro
                                        Else
                                            v_lista_mp = v_lista_mp & ", " & marca.CodigoRegistro
                                            v_lista_mp_es = v_lista_mp_es & ", " & marca.CodigoRegistro
                                        End If
                                    Else ' If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then
                                        If v_lista_mp = Nothing Or v_lista_mp = "" Then
                                            v_lista_mp = marca.CodigoInscripcion
                                            v_lista_mp_es = marca.CodigoInscripcion
                                        Else
                                            v_lista_mp = v_lista_mp & ", " & marca.CodigoInscripcion
                                            v_lista_mp_es = v_lista_mp_es & ", " & marca.CodigoInscripcion
                                        End If
                                    End If ' If marca.CodigoRegistro <> Nothing And marca.CodigoRegistro <> "" Then

                                    Dim tipomarca As TipoMarca = buscar_tipo_marca_id(marca.Tipo)
                                    If (v_idioma = "ES") Then
                                        v_lista_1(8) = tipomarca.Doc_Esp 'Tipo_Marca
                                        v_lista_1_es(8) = tipomarca.Doc_Esp 'Tipo_Marca
                                    Else
                                        v_lista_1(8) = tipomarca.Doc_Ingl 'Tipo_Marca
                                        v_lista_1_es(8) = tipomarca.Doc_Esp 'Tipo_Marca
                                    End If

                                    v_cadtmp = v_cadena
                                    v_cadtmp_es = v_cadena_es
                                    v_envia = "|Tipo_Marca|"
                                    ''''''''''''''''''''''''''''''''(Tipo_Marca)
                                    sustituye(v_cadtmp, v_envia, v_lista_1(8), v_cadtmp)
                                    sustituye(v_cadtmp_es, v_envia, v_lista_1_es(8), v_cadtmp_es)

                                End If 'If (v_codmult = "M2") Then
                            End If 'If i < Marcasselec.Count - 1 Then
                        Next
                    End If 'If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then
                End If 'If v_mult_marc <> "" And v_mult_marc <> Nothing Then


                'para multis patentes
                If v_mult_pate <> "" And v_mult_pate <> Nothing Then
                    Dim patentesselec As New List(Of PatenteSelec)
                    ' si el grid de multiples patentes tiene ya porlomenos un registro
                    If (DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec)) IsNot Nothing) Then
                        patentesselec = DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec))
                        v_lista_mp = ""
                        v_lista_mp_es = ""
                        'v_lista_ot = ""
                        'v_lista_ot_es = ""
                        Dim patente As Patente
                        For i As Integer = 0 To patentesselec.Count - 1
                            patente = BuscarPatente_2(patentesselec(i).Id)
                            If i < patentesselec.Count - 1 Then
                                If (v_codmult = "P1") Then

                                    If patente.CodigoRegistro <> Nothing And patente.CodigoRegistro <> "" Then
                                        v_lista_mp = v_lista_mp & " " & patente.CodigoRegistro
                                        v_lista_mp_es = v_lista_mp_es & " " & patente.CodigoRegistro
                                    Else
                                        v_lista_mp = v_lista_mp & ", " & patente.CodigoInscripcion
                                        v_lista_mp_es = v_lista_mp_es & ", " & patente.CodigoInscripcion
                                    End If 'If patente.CodigoRegistro <> Nothing And patente.CodigoRegistro <> "" Then

                                End If ' If (v_codmult = "P1") Then

                                If (v_codmult = "P2") Then
                                    If (v_lista_mp = "") Then
                                        v_lista_mp = patente.CodigoInscripcion
                                        v_lista_mp_es = patente.CodigoInscripcion
                                    Else
                                        v_lista_mp = v_lista_mp & ";" & patente.CodigoInscripcion
                                        v_lista_mp_es = v_lista_mp_es & ";" & patente.CodigoInscripcion
                                    End If
                                End If 'If (v_codmult = "P2") Then
                            Else ' 'If i < patentesselec.Count - 1 Then
                                If (v_codmult = "P1") Then

                                    If patente.CodigoRegistro <> Nothing And patente.CodigoRegistro <> "" Then
                                        v_lista_mp = v_lista_mp & " " & patente.CodigoRegistro
                                        v_lista_mp_es = v_lista_mp_es & " " & patente.CodigoRegistro
                                    Else
                                        v_lista_mp = v_lista_mp & "," & patente.CodigoInscripcion
                                        v_lista_mp_es = v_lista_mp_es & "," & patente.CodigoInscripcion
                                    End If 'If patente.CodigoRegistro <> Nothing And patente.CodigoRegistro <> "" Then
                                End If ' If (v_codmult = "P1") Then

                                If (v_codmult = "P2") Then
                                    If v_lista_mp = Nothing Or v_lista_mp = "" Then
                                        v_lista_mp = patente.CodigoInscripcion
                                        v_lista_mp_es = patente.CodigoInscripcion
                                    Else
                                        v_lista_mp = v_lista_mp & "," & patente.CodigoInscripcion
                                        v_lista_mp_es = v_lista_mp_es & "," & patente.CodigoInscripcion
                                    End If

                                End If 'If (v_codmult = "P2") Then

                                Dim tipopatente As TipoPatente = buscar_tipo_patente_id(patente.Tipo)
                                If (v_idioma = "ES") Then
                                    v_lista_1(15) = tipopatente.Doc_Esp 'Tipo_Patente
                                    v_lista_1_es(15) = tipopatente.Doc_Esp 'Tipo_Patente
                                Else
                                    v_lista_1(15) = tipopatente.Doc_Ingl 'Tipo_Patente
                                    v_lista_1_es(15) = tipopatente.Doc_Esp 'Tipo_patente
                                End If

                                v_cadtmp = v_cadena
                                v_cadtmp_es = v_cadena_es
                                v_envia = "|Tipo_patente|"
                                ''''''''''''''''''''''''''''''''(Tipo_patente)
                                sustituye(v_cadtmp, v_envia, v_lista_1(15), v_cadtmp)
                                sustituye(v_cadtmp_es, v_envia, v_lista_1_es(15), v_cadtmp_es)

                            End If 'If i < patentesselec.Count - 1 Then
                        Next
                    End If 'If (DirectCast(Me._ventana.ResultadosMultiplespatentes, List(Of patenteSelec)) IsNot Nothing) Then
                End If 'If v_mult_pate <> "" And v_mult_pate <> Nothing Then

                list_ret = v_cadtmp & " " & v_lista_mp
                list_ret_es = v_cadtmp_es & " " & v_lista_mp_es
            Catch ex As Exception

            End Try
        End Sub

        Function buscar_tipo_clase_id(ByVal id As String) As TipoClase
            Dim tipoclaseaux As New TipoClase
            tipoclaseaux.Id = id
            Dim tipoclase As List(Of TipoClase) = _TipoClasesServicios.ObtenerTipoClasesFiltro(tipoclaseaux)
            Return tipoclase(0)
        End Function

        Function buscar_tipo_marca_id(ByVal id As String) As TipoMarca
            Dim tipomarcaaux As New TipoMarca
            tipomarcaaux.Id = id
            Dim tipomarca As List(Of TipoMarca) = _TipoMarcasServicios.ObtenerTipoMarcasFiltro(tipomarcaaux)
            If tipomarca.Count > 0 Then
                Return tipomarca(0)
            Else
                Return Nothing
            End If
        End Function

        Function buscar_tipo_patente_id(ByVal id As String) As TipoPatente
            Dim tipopatenteaux As New TipoPatente
            tipopatenteaux.Id = id
            Dim tipopatente As List(Of TipoPatente) = _TipoPatentesServicios.ObtenerTipoPatentesFiltro(tipopatenteaux)
            If tipopatente.Count > 0 Then
                Return tipopatente(0)
            Else
                Return Nothing
            End If
        End Function

        Public Sub sustituye(ByVal cCadena As String, ByVal cCadbu As String, ByVal cCadre As String, ByRef cCadr As String)
            '          Numeric(npos1, npos2, nlong, nlong1, nlong2, w_cont, npos3, nlongli)
            Dim npos1, npos2, nlong, nlong2, w_cont, npos3 As Integer

            'String  cCadena,cCadena1,cCadena2
            Dim cCadena1, cCadena2 As String

            npos1 = 0
            nlong2 = cCadbu.Length - 1
            w_cont = 0

            While (w_cont = 0)
                nlong = cCadena.Length
                'Dim a As String = (cCadena.Substring(npos1, nlong))
                npos2 = InStr(cCadena.Substring(npos1, nlong).ToUpper, cCadbu.ToUpper)
                If npos2 > 0 Then
                    npos3 = npos2 + nlong2
                    npos2 = npos2 - 1
                    cCadena1 = cCadena.Substring(npos1, npos2)
                    If npos3 <= nlong Then
                        cCadena2 = cCadena.Substring(npos3)
                    Else
                        cCadena2 = ""
                    End If
                    cCadena = cCadena1 & " " & cCadre & " " & cCadena2
                Else
                    w_cont = 1
                End If
            End While
            cCadr = cCadena
        End Sub


        Public Sub CambiarAsociado()
            Mouse.OverrideCursor = Cursors.Wait
            Try
                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    If Me._ventana.Asociado.id <> Integer.MinValue Then
                        Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                        'Me._ventana.IdAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id
                        Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                        Me._ventana.Idiomas = idiomas
                        Me._ventana.Idioma = Me.BuscarIdioma(idiomas, asociado.Idioma)

                        Dim Monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                        Me._ventana.Monedas = Monedas
                        Me._ventana.Moneda = Me.BuscarMoneda(Monedas, asociado.Moneda)
                        'busca el resto de los datos
                        BuscarCarta()

                        adivinar(asociado, DirectCast(Me._ventana.Moneda, Moneda))

                        If (Me._ventana.AsociadoImp Is Nothing) Then
                            Me._ventana.NombreAsociadoImp = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                            'Me._ventana.AsociadoImp = asociado
                        End If
                    Else
                        Me._ventana.NombreAsociado = Nothing
                    End If
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothingo
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Sub CambiarInteresado()
            Try
                If DirectCast(Me._ventana.Interesado, Interesado) IsNot Nothing Then
                    If Me._ventana.Interesado.id <> Integer.MinValue Then
                        Dim Interesado As Interesado = Me._InteresadosServicios.ConsultarInteresadoConTodo(DirectCast(Me._ventana.Interesado, Interesado))
                        Me._ventana.NombreInteresado = DirectCast(Me._ventana.Interesado, Interesado).Id & " - " & DirectCast(Me._ventana.Interesado, Interesado).Nombre
                        'Me._ventana.IdInteresado = DirectCast(Me._ventana.Interesado, Interesado).I
                        adivinar2(Interesado)
                        Me._ventana.AsociadoImp = Nothing
                        Me._ventana.AsociadosImp = Nothing
                        Me._ventana.NombreAsociadoImp = Nothing
                    Else
                        Me._ventana.NombreInteresado = Nothing
                    End If
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothingo
            End Try
        End Sub

        Public Sub adivinar2(ByVal interesado As Interesado)
            'If Interesado.BActivo = True Then
            'If Interesado.BAlerta = False Then
            'If (Interesado.Tarifa IsNot Nothing) AndAlso (Not Interesado.Tarifa.Id.Equals("")) Then
            'If (Interesado.Tarifa IsNot Nothing) AndAlso (Interesado.Tarifa.Id <> "") Then
            '    Me._ventana.Tarifa = Interesado.Tarifa.Id
            'End If
            'If (Interesado.Etiqueta IsNot Nothing) AndAlso (Interesado.Etiqueta.Id <> "") Then
            '    Me._ventana.Codeti = Interesado.Etiqueta.Id
            'End If
            'If (Interesado.Rif IsNot Nothing) AndAlso (Interesado.Rif <> "") Then
            Me._ventana.Rif = ""
            'End If
            'If (Interesado.Nit IsNot Nothing) AndAlso (Interesado.Nit <> "") Then
            Me._ventana.XNit = ""
            'End If

            Dim xasociado As String = ""
            If (interesado.Nombre IsNot Nothing) AndAlso (interesado.Nombre <> "") Then
                xasociado = interesado.Nombre & ControlChars.NewLine
            End If
            If (interesado.Domicilio IsNot Nothing) AndAlso (interesado.Domicilio <> "") Then
                xasociado = xasociado & interesado.Domicilio & ControlChars.NewLine
            End If
            If (interesado.Pais IsNot Nothing) Then
                Dim paises As IList(Of Pais) = Me._paisesServicios.ConsultarTodos()
                'Dim pais As Pais = Me.BuscarPais(Interesado.Pais)
                Dim pais As Pais = BuscarPais(paises, interesado.Pais)
                xasociado = xasociado & pais.NombreIngles
            End If

            Me._ventana.XAsociado = xasociado
            'If interesado.Descuento < 25 And moneda.Id = "BF" Then
            '    Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
            'End If
            'Else
            '    Dim mensaje As String = "ALERTA: " & Interesado.AlarmaDescripcion
            '    MessageBox.Show(mensaje)
            'End If
            'Else
            '    Me._ventana.Idiomas = Nothing
            '    Me._ventana.Idioma = Nothing

            '    Me._ventana.Monedas = Nothing
            '    Me._ventana.Moneda = Nothing
            '    Me._ventana.NombreInteresadoImp = Nothing
            '    Me._ventana.InteresadoImp = Nothing
            '    Me._ventana.InteresadosImp = Nothing
            '    Me._ventana.NombreInteresado = Nothing
            '    Me._ventana.Interesado = Nothing
            '    Me._ventana.Interesados = Nothing
            '    Me._ventana.NombreInteresadoImp = Nothing
            '    Me._ventana.Cartas = Nothing
            '    MessageBox.Show("Error: Interesado no Activo")
            '    Me._ventana.MensajeError = "Error: Interesado no Activo"
            'End If
        End Sub

        Public Sub CambiarAsociadoImp()
            Mouse.OverrideCursor = Cursors.Wait
            Try
                If DirectCast(Me._ventana.AsociadoImp, Asociado) IsNot Nothing Then
                    If Me._ventana.AsociadoImp.id <> Integer.MinValue Then
                        Dim asociadoimp As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.AsociadoImp, Asociado))
                        Me._ventana.NombreAsociadoImp = DirectCast(Me._ventana.AsociadoImp, Asociado).Id & " - " & DirectCast(Me._ventana.AsociadoImp, Asociado).Nombre

                        Dim MonedasImp As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                        'Me._ventana.MonedasImp = MonedasImp
                        Dim moneda As Moneda = Me.BuscarMoneda(MonedasImp, asociadoimp.Moneda)

                        adivinar(asociadoimp, moneda)
                        Me._ventana.Interesado = Nothing
                        Me._ventana.Interesados = Nothing
                        Me._ventana.NombreInteresado = Nothing
                    Else
                        Me._ventana.NombreAsociadoImp = Nothing
                    End If
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Sub adivinar(ByVal asociado As Asociado, ByVal moneda As Moneda)
            If asociado.BActivo = True Then
                If asociado.BAlerta = True Then
                    Dim mensaje As String = "ALERTA: " & asociado.AlarmaDescripcion
                    MessageBox.Show(mensaje)
                End If
                'If (asociado.Tarifa IsNot Nothing) AndAlso (Not asociado.Tarifa.Id.Equals("")) Then
                If (asociado.Tarifa IsNot Nothing) AndAlso (asociado.Tarifa.Id <> "") Then
                    Me._ventana.Tarifa = asociado.Tarifa.Id
                End If
                If (asociado.Etiqueta IsNot Nothing) AndAlso (asociado.Etiqueta.Id <> "") Then
                    Me._ventana.Codeti = asociado.Etiqueta.Id
                End If
                If (asociado.Rif IsNot Nothing) AndAlso (asociado.Rif <> "") Then
                    Me._ventana.Rif = asociado.Rif
                End If
                If (asociado.Nit IsNot Nothing) AndAlso (asociado.Nit <> "") Then
                    Me._ventana.XNit = asociado.Nit
                End If

                Dim xasociado As String = ""
                If (asociado.Nombre IsNot Nothing) AndAlso (asociado.Nombre <> "") Then
                    xasociado = asociado.Nombre & ControlChars.NewLine
                End If
                If (asociado.Domicilio IsNot Nothing) AndAlso (asociado.Domicilio <> "") Then
                    xasociado = xasociado & asociado.Domicilio & ControlChars.NewLine
                End If
                If (asociado.Pais IsNot Nothing) Then
                    Dim paises As IList(Of Pais) = Me._paisesServicios.ConsultarTodos()
                    'Dim pais As Pais = Me.BuscarPais(asociado.Pais)
                    Dim pais As Pais = BuscarPais(paises, asociado.Pais)
                    xasociado = xasociado & pais.NombreIngles
                End If

                Me._ventana.XAsociado = xasociado
                If asociado.Descuento < 25 And moneda.Id = "BF" Then
                    Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                End If
            Else
                Me._ventana.Idiomas = Nothing
                Me._ventana.Idioma = Nothing

                Me._ventana.Monedas = Nothing
                Me._ventana.Moneda = Nothing
                Me._ventana.NombreAsociadoImp = Nothing
                Me._ventana.AsociadoImp = Nothing
                Me._ventana.AsociadosImp = Nothing
                Me._ventana.NombreAsociado = Nothing
                Me._ventana.Asociado = Nothing
                Me._ventana.Asociados = Nothing
                Me._ventana.NombreAsociadoImp = Nothing
                Me._ventana.Cartas = Nothing
                MessageBox.Show("Error: Asociado no Activo")
                Me._ventana.MensajeError = "Error: Asociado no Activo"
            End If
        End Sub

        Public Sub CambiarCarta()
            Try
                If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                    If Me._ventana.Carta.id <> Integer.MinValue Then
                        'Dim carta As List(Of Carta) = Me._cartasServicios.ObtenerCartasFiltro(DirectCast(Me._ventana.Carta, Carta))
                        Me._ventana.NombreCarta = DirectCast(Me._ventana.Carta, Carta).Id & " - " & DirectCast(Me._ventana.Carta, Carta).Medio
                    End If
                Else
                    Me._ventana.NombreCarta = Nothing
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub CambiarMarca()
            Try
                If DirectCast(Me._ventana.Marcas_Seleccionado, Marca) IsNot Nothing Then
                    Dim _Marca As Marca = DirectCast(Me._ventana.Marcas_Seleccionado, Marca)
                    Dim Marcas As New List(Of MarcaSelec)

                    If (_Marca IsNot Nothing) Then
                        ' si el grid de multiples marcas tiene ya porlomenos un registro
                        If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then
                            Marcas = DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec))
                        End If
                        Marcas.Add(New MarcaSelec)
                        Dim i As Integer
                        i = Marcas.Count - 1

                        Marcas(i).Id = _Marca.Id
                        Marcas(i).PrimeraReferencia = _Marca.PrimeraReferencia
                        Marcas(i).Seleccion = False

                    End If
                    Me._ventana.ResultadosMultiplesMarcas = Nothing
                    Me._ventana.ResultadosMultiplesMarcas = Marcas
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub EliminarMultiplesMarca()
            Try
                Dim Marcas As List(Of MarcaSelec)
                Dim MarcasAux As New List(Of MarcaSelec)
                If (DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec)) IsNot Nothing) Then ' si tiene multiples marcas agregadas
                    Marcas = DirectCast(Me._ventana.ResultadosMultiplesMarcas, List(Of MarcaSelec))
                    Dim cont As Integer = 0
                    For i As Integer = 0 To Marcas.Count - 1
                        If Marcas(i).Seleccion = False Then
                            MarcasAux.Add(New MarcaSelec)
                            MarcasAux(cont) = Marcas(i)
                            cont = cont + 1
                        End If

                    Next
                End If
                Me._ventana.ResultadosMultiplesMarcas = Nothing
                Me._ventana.ResultadosMultiplesMarcas = MarcasAux

            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub


        Public Sub CambiarPatente()
            Try
                If DirectCast(Me._ventana.Patentes_Seleccionado, Patente) IsNot Nothing Then
                    Dim _Patente As Patente = DirectCast(Me._ventana.Patentes_Seleccionado, Patente)
                    Dim Patentes As New List(Of PatenteSelec)

                    If (_Patente IsNot Nothing) Then
                        ' si el grid de multiples Patentes tiene ya porlomenos un registro
                        If (DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec)) IsNot Nothing) Then
                            Patentes = DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec))
                        End If
                        Patentes.Add(New PatenteSelec)
                        Dim i As Integer
                        i = Patentes.Count - 1

                        Patentes(i).Id = _Patente.Id
                        Patentes(i).PrimeraReferencia = _Patente.PrimeraReferencia
                        Patentes(i).Seleccion = False

                    End If
                    Me._ventana.ResultadosMultiplesPatentes = Nothing
                    Me._ventana.ResultadosMultiplesPatentes = Patentes
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub EliminarMultiplesPatente()
            Try
                Dim Patentes As List(Of PatenteSelec)
                Dim PatentesAux As New List(Of PatenteSelec)
                If (DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec)) IsNot Nothing) Then ' si tiene multiples Patentes agregadas
                    Patentes = DirectCast(Me._ventana.ResultadosMultiplesPatentes, List(Of PatenteSelec))
                    Dim cont As Integer = 0
                    For i As Integer = 0 To Patentes.Count - 1
                        If Patentes(i).Seleccion = False Then
                            PatentesAux.Add(New PatenteSelec)
                            PatentesAux(cont) = Patentes(i)
                            cont = cont + 1
                        End If

                    Next
                End If
                Me._ventana.ResultadosMultiplesPatentes = Nothing
                Me._ventana.ResultadosMultiplesPatentes = PatentesAux

            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Function existe_tasa_dia(ByVal fecha As DateTime, ByVal moneda As String) As Boolean
            Dim existe As Boolean = True

            Dim tasa As New Tasa()
            tasa.Id = fecha
            tasa.Moneda = moneda
            Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            If tasas.Count <= 0 Then
                existe = False
            End If
            Return (existe)
        End Function


        Public Function asignar_vacio() As String()
            Dim lista_en(19) As String

            lista_en(0) = Nothing
            lista_en(1) = Nothing
            lista_en(2) = Nothing
            lista_en(3) = Nothing
            lista_en(4) = Nothing
            lista_en(5) = Nothing
            lista_en(6) = Nothing
            lista_en(7) = Nothing
            lista_en(8) = Nothing
            lista_en(9) = Nothing
            lista_en(10) = Nothing
            lista_en(11) = Nothing
            lista_en(12) = Nothing
            lista_en(13) = Nothing
            lista_en(14) = Nothing
            lista_en(15) = Nothing
            lista_en(16) = Nothing
            lista_en(17) = Nothing
            lista_en(18) = Nothing

            Return lista_en
        End Function



        'este es para agregar el check para seleccionar las que se van a eliminar
        Public Class MarcaSelec
            ''Inherits Marca

            Private p_seleccion As Boolean
            Private p_id As Integer
            Private p_primerareferencia As String

            Public Property Seleccion() As Boolean
                Get
                    Return Me.p_seleccion
                End Get
                Set(ByVal Value As Boolean)
                    Me.p_seleccion = Value
                End Set
            End Property

            Public Property Id() As Integer
                Get
                    Return Me.p_id
                End Get
                Set(ByVal Value As Integer)
                    Me.p_id = Value
                End Set
            End Property

            Public Property PrimeraReferencia() As String
                Get
                    Return Me.p_primerareferencia
                End Get
                Set(ByVal Value As String)
                    Me.p_primerareferencia = Value
                End Set
            End Property

        End Class

        'este es para agregar el check para seleccionar las que se van a eliminar
        Public Class PatenteSelec
            ''Inherits Patente

            Private p_seleccion As Boolean
            Private p_id As Integer
            Private p_primerareferencia As String

            Public Property Seleccion() As Boolean
                Get
                    Return Me.p_seleccion
                End Get
                Set(ByVal Value As Boolean)
                    Me.p_seleccion = Value
                End Set
            End Property

            Public Property Id() As Integer
                Get
                    Return Me.p_id
                End Get
                Set(ByVal Value As Integer)
                    Me.p_id = Value
                End Set
            End Property

            Public Property PrimeraReferencia() As String
                Get
                    Return Me.p_primerareferencia
                End Get
                Set(ByVal Value As String)
                    Me.p_primerareferencia = Value
                End Set
            End Property

        End Class

    End Class
End Namespace
