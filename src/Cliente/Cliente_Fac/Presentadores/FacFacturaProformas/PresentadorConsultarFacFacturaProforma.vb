Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaProformas
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones
Namespace Presentadores.FacFacturaProformas
    Class PresentadorConsultarFacFacturaProforma
        Inherits PresentadorBase

        Private _ventana As IConsultarFacFacturaProforma
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
        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _cartasServicios As ICartaServicios
        Private _paisesServicios As IPaisServicios
        Private _desgloseserviciosServicios As IFacDesgloseServicioServicios
        Private _DepartamentoserviciosServicios As IFacDepartamentoServicioServicios
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
        Private _FacOperacionProformasServicios As IFacOperacionProformaServicios
        Private _FacOperacionDetaProformasServicios As IFacOperacionDetaProformaServicios
        Private _FacOperacionDetaTmProformasServicios As IFacOperacionDetaTmProformaServicios
        Private _FacDesgloseColesServicios As IFacDesgloseColeServicios
        Private _DepartamentoServicios As IDepartamentoServicios

        Private _facfacturaproforma As FacFacturaProforma
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
        Public Sub New(ByVal ventana As IConsultarFacFacturaProforma, ByVal FacFacturaProforma As Object)
            Try
                Me._ventana = ventana
                Me._ventana.FacFacturaProforma = FacFacturaProforma
                _facfacturaproforma = FacFacturaProforma

                'Me._ventana.FacFacturaProforma = New FacFacturaProforma()
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
                Me._detalleenviosServicios = DirectCast(Activator.GetObject(GetType(IFacDetalleEnvioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DetalleEnvioServicios")), IFacDetalleEnvioServicios)
                Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                Me._paisesServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                Me._desgloseserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseservicioServicios")), IFacDesgloseServicioServicios)
                Me._DepartamentoserviciosServicios = DirectCast(Activator.GetObject(GetType(IFacDepartamentoServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicioServicios")), IFacDepartamentoServicioServicios)
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
                Me._FacOperacionProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                Me._FacOperacionDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaProformaServicios")), IFacOperacionDetaProformaServicios)
                Me._FacOperacionDetaTmProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaTmProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaTmProformaServicios")), IFacOperacionDetaTmProformaServicios)
                Me._FacDesgloseColesServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseColeServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacDesgloseColeServicios")), IFacDesgloseColeServicios)
                Me._DepartamentoServicios = DirectCast(Activator.GetObject(GetType(IDepartamentoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicios")), IDepartamentoServicios)

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
                    Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturaProforma, Recursos.Ids.fac_ConsultarFacFacturaProforma)

                    Dim FacFacturaProforma As FacFacturaProforma = DirectCast(Me._ventana.FacFacturaProforma, FacFacturaProforma)

                    'Me._detalleenvios = Me._detalleenviosServicios.ConsultarTodos()
                    'Me._ventana.DetalleEnvios = Me._detalleenvios
                    'Dim primeradetalleenvio As New FacDetalleEnvio()
                    'primeradetalleenvio.Id = "FED"
                    'primeradetalleenvio.Descripcion = "FEDEX"

                    'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                    'Me._ventana.Asociados = Me._asociados
                    Dim departamentos As List(Of Departamento)
                    departamentos = Me._DepartamentoServicios.ConsultarTodos
                    Dim departamentos2 As IEnumerable(Of Departamento) = departamentos             
                    departamentos2 = From d In departamentos2 Where d.Id IsNot Nothing AndAlso d.Id.ToLower().Contains(FacFacturaProforma.CodigoDepartamento.ToLower())
                    Me._ventana.Departamento = departamentos2.ToList(0).Descripcion

                    Dim asociadoaux As New Asociado
                    Dim asociado As List(Of Asociado)
                    If FacFacturaProforma.Asociado IsNot Nothing Then
                        asociadoaux.Id = FacFacturaProforma.Asociado.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados = asociado
                        Me._ventana.Asociado = asociado(0)
                        Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre

                        If (asociado(0).Tarifa IsNot Nothing) AndAlso (asociado(0).Tarifa.Id <> "") Then
                            Me._ventana.Tarifa = asociado(0).Tarifa.Id
                        End If
                    End If

                    SaldoPendiente(FacFacturaProforma.Id)

                    Me._ventana.BAuto = FacFacturaProforma.BAuto

                    If FacFacturaProforma.Auto = "0" Or FacFacturaProforma.Auto = "3" Then
                        If UsuarioLogeado.Rol.Id = "ADMINISTRADOR" Then
                            Me._ventana.Permisos = 1 'tiene permiso
                        Else
                            If UsuarioLogeado.Iniciales = FacFacturaProforma.Inicial Then
                                Me._ventana.Permisos = 2
                            Else
                                Me._ventana.Permisos = 0
                            End If
                        End If
                    Else
                        Me._ventana.Permisos = 0
                    End If

                    If FacFacturaProforma.AsociadoImp IsNot Nothing Then
                        asociadoaux.Id = FacFacturaProforma.AsociadoImp.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.AsociadosImp = asociado
                        Me._ventana.AsociadoImp = asociado(0)
                        Me._ventana.NombreAsociadoImp = asociado(0).Id & " - " & asociado(0).Nombre
                    End If

                    If FacFacturaProforma.Carta IsNot Nothing Then
                        Dim cartaaux As New Carta
                        Dim carta As List(Of Carta)
                        cartaaux.Id = FacFacturaProforma.Carta.Id
                        carta = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
                        Me._ventana.Cartas = carta
                        Me._ventana.Carta = carta(0)
                        Me._ventana.NombreCarta = carta(0).Id & " - " & carta(0).Medio
                    End If

                    If FacFacturaProforma.InteresadoImp IsNot Nothing Then
                        Dim Interesadoaux As New Interesado
                        Dim interesado As List(Of Interesado)
                        Interesadoaux.Id = FacFacturaProforma.InteresadoImp.Id
                        interesado = Me._InteresadosServicios.ObtenerInteresadosFiltro(Interesadoaux)
                        Me._ventana.Interesados = interesado
                        Me._ventana.Interesado = interesado(0)
                        Me._ventana.NombreInteresado = interesado(0).Id & " - " & interesado(0).Nombre
                    End If

                    Me._detalleenvios = Me._detalleenviosServicios.ConsultarTodos()
                    Me._ventana.DetalleEnvios = Me._detalleenvios
                    Dim primeradetalleenvio As New FacDetalleEnvio
                    primeradetalleenvio = Me.BuscarFacDetalleEnvio(Me._detalleenvios, FacFacturaProforma.DetalleEnvio)
                    Me._ventana.DetalleEnvio = primeradetalleenvio

                    Dim detalleproformaaux As New FacFactuDetaProforma()
                    detalleproformaaux.Factura = FacFacturaProforma
                    Me._ventana.ResultadosFacFactuDetaProforma = Me._FacFactuDetaProformasServicios.ObtenerFacFactuDetaProformasFiltro(detalleproformaaux)

                    eliminar_operacion_detalle_tm_usuario() ' para eliminar los operacion tmp de operacion_detalle_tm


                    Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                    Me._ventana.Idiomas = idiomas
                    Me._ventana.Idioma = Me.BuscarIdioma(idiomas, FacFacturaProforma.Idioma)

                    Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                    Me._ventana.Monedas = monedas
                    Me._ventana.Moneda = Me.BuscarMoneda(monedas, FacFacturaProforma.Moneda)

                    Me._ventana.SetLocalidad = Me.BuscarLocalidad(FacFacturaProforma.Local)

                    ' estaas son  campos double
                    Me._ventana.Desc = FacFacturaProforma.Descuento
                    Me._ventana.MSubtimpo = FacFacturaProforma.MSubtimpo
                    Me._ventana.MDescuento = FacFacturaProforma.MDescuento
                    Me._ventana.MTbimp = FacFacturaProforma.MTbimp
                    Me._ventana.Mtbexc = FacFacturaProforma.Mtbexc
                    Me._ventana.Msubtotal = FacFacturaProforma.MSubtotal
                    Me._ventana.Mtimp = FacFacturaProforma.Mtimp
                    Me._ventana.Mttotal = FacFacturaProforma.Mttotal
                    Me._ventana.MSubtimpoBf = FacFacturaProforma.MSubtimpoBf
                    Me._ventana.MDescuentoBf = FacFacturaProforma.MDescuentoBf
                    Me._ventana.MTbimpBf = FacFacturaProforma.MTbimpBf
                    Me._ventana.MtbexcBf = FacFacturaProforma.MTbexcBf
                    Me._ventana.MsubtotalBf = FacFacturaProforma.MSubtotalBf
                    Me._ventana.MtimpBf = FacFacturaProforma.MTimpBf
                    Me._ventana.MttotalBf = FacFacturaProforma.MTtotalBf
                    'Me._ventana.NCantidad = FacFacturaProforma.Cantidad
                    Me._ventana.PDescuento = FacFacturaProforma.Descuento
                    'FacFacturaProforma.Descuento = Me._ventana.PDescuento
                    'Me._ventana.Pu = FacFacturaProforma.Pu
                    'Me._ventana.BDetalle = FacFacturaProforma.BDetalle
                    Me._ventana.Impuesto = FacFacturaProforma.Impuesto
                    Me._ventana.Seleccion = True
                    Dim facimpuestos As List(Of FacImpuesto) = Me._FacImpuestosServicios.ObtenerFacImpuestosFiltro(Nothing)
                    If facimpuestos IsNot Nothing Then
                        Me._ventana.Impuesto = facimpuestos(0).Impuesto
                    Else
                        'Me._ventana.Impuesto = Nothing
                    End If
                    'Me._ventana.PDescuento = 0

                    'esto es para determinar si la pantalla esta en formato de modificar o solo lectura
                    'Me._ventana.AccionRealizar = FacFacturaProforma.Accion

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
        End Sub

        ''' <summary>
        ''' Método que dependiendo del estado de la página, habilita los campos o 
        ''' modifica los datos del usuario
        ''' </summary>

        Public Sub buscar_departamento_servicio_esp(ByVal iddetalle As Integer)
            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            If FacFactuDetaProformas IsNot Nothing Then
                Dim i As Integer = 0
                While (i < (FacFactuDetaProformas.Count))
                    If (FacFactuDetaProformas.Item(i).Id = iddetalle) Then
                        'MessageBox.Show(FacFactuDetaProformas.Item(i).XDetalleEs, "", MessageBoxButton.OK)

                        Dim ag As New Mostrar_Detalle(FacFactuDetaProformas.Item(i), Me, 2)
                        'ag.Owner = Me
                        ag.ShowDialog()

                        i = FacFactuDetaProformas.Count
                    End If
                    i = i + 1
                End While
            End If
        End Sub

        Public Sub Ver_Carta()

            If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                If Me._ventana.Carta.id <> Integer.MinValue Then
                    Dim ag As New Mostrar_Detalle_Carta(Me._ventana.Carta)
                    'ag.Owner = Me
                    ag.ShowDialog()
                Else
                    MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If
            Else
                MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                Exit Sub
            End If
        End Sub


        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacFacturaProforma(_facfacturaproforma))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub SaldoPendiente(ByVal id As Integer)
            Dim operacionaux As New FacOperacionProforma
            operacionaux.CodigoOperacion = id
            operacionaux.Id = "ND"
            Dim operaciones As List(Of FacOperacionProforma) = _FacOperacionProformasServicios.ObtenerFacOperacionProformasFiltro(operacionaux)
            If operaciones IsNot Nothing Then
                If operaciones.Count > 0 Then
                    Me._ventana.SaldoPendiente = operaciones(0).Saldo
                End If
            End If
        End Sub

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

                    Dim FacFacturaProforma As FacFacturaProforma = DirectCast(_ventana.FacFacturaProforma, FacFacturaProforma)

                    'FacFacturaProforma.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                    If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                        If Me._ventana.Asociado.id <> Integer.MinValue Then
                            FacFacturaProforma.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            If DirectCast(Me._ventana.Interesado, Interesado) Is Nothing Then
                                If DirectCast(Me._ventana.AsociadoImp, Asociado) Is Nothing Then
                                    FacFacturaProforma.AsociadoImp = DirectCast(Me._ventana.Asociado, Asociado)
                                Else
                                    If Me._ventana.AsociadoImp.id = Integer.MinValue Then
                                        FacFacturaProforma.AsociadoImp = DirectCast(Me._ventana.Asociado, Asociado)
                                    End If
                                End If
                            Else
                                FacFacturaProforma.AsociadoImp = Nothing
                            End If
                        End If
                    Else

                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If
                    If DirectCast(Me._ventana.AsociadoImp, Asociado) IsNot Nothing Then
                        If Me._ventana.AsociadoImp.id <> Integer.MinValue Then
                            FacFacturaProforma.AsociadoImp = DirectCast(Me._ventana.AsociadoImp, Asociado)
                        End If
                    Else

                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If

                    'FacFacturaProforma.AsociadoImp = If(Not DirectCast(Me._ventana.AsociadoImp, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.AsociadoImp, Asociado), Nothing)
                    If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                        If Me._ventana.Carta.id <> Integer.MinValue Then
                            FacFacturaProforma.Carta = DirectCast(Me._ventana.Carta, Carta)
                        Else
                            Me._ventana.MensajeError = "Debe especificar Carta Orden"
                            Exit Sub
                        End If
                    Else
                        Me._ventana.MensajeError = "Debe especificar Carta Orden"
                        Exit Sub
                    End If

                    If DirectCast(Me._ventana.Interesado, Interesado) IsNot Nothing Then
                        If Me._ventana.Interesado.id <> Integer.MinValue Then
                            FacFacturaProforma.InteresadoImp = DirectCast(Me._ventana.Interesado, Interesado)
                        Else
                            FacFacturaProforma.InteresadoImp = Nothing
                        End If
                    Else
                        FacFacturaProforma.InteresadoImp = Nothing
                    End If

                    'FacFacturaProforma.Carta = If(Not DirectCast(Me._ventana.Carta, Carta).Id.Equals("NGN"), DirectCast(Me._ventana.Carta, Carta), Nothing)
                    'FacFacturaProforma.InteresadoImp = If(Not DirectCast(Me._ventana.intersado, Interesado).Id.Equals("NGN"), DirectCast(Me._ventana.Interesado, Interesado), Nothing)
                    If DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio) IsNot Nothing Then
                        FacFacturaProforma.DetalleEnvio = DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio)
                        'Me._ventana.MensajeError = "Se aplicara un descuento de 25%"
                        'Exit Sub
                    End If
                    'FacFacturaProforma.DetalleEnvio = If(Not DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio).Id.Equals("NGN"), DirectCast(Me._ventana.DetalleEnvio, FacDetalleEnvio), Nothing)
                    FacFacturaProforma.Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    FacFacturaProforma.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)
                    FacFacturaProforma.MonedaImp = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    'FacFacturaProforma.IdiomaImp = If(Not DirectCast(Me._ventana.Idiomaimp, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)

                    If FacFacturaProforma.Caso = "" Or FacFacturaProforma.Caso = Nothing Then
                        Me._ventana.MensajeError = "Debe especificar Caso/Referencia"
                        Exit Sub
                    End If

                    If Me._ventana.Impuesto = "" Or Me._ventana.Impuesto = Nothing Or Not IsNumeric(Me._ventana.Impuesto) Then
                        Me._ventana.MensajeError = "Debe especificar Impuesto"
                        Exit Sub
                    End If
                    Dim xasociado As String = Me._ventana.XAsociado.ToString
                    FacFacturaProforma.XAsociado = xasociado

                    If FacFacturaProforma.FechaFactura Is Nothing Then
                        Me._ventana.MensajeError = "Debe especificar Fecha Factura"
                        Exit Sub
                    Else
                        FacFacturaProforma.FechaFactura = FormatDateTime(FacFacturaProforma.FechaFactura, DateFormat.ShortDate)
                    End If

                    'If FacFacturaProforma.FechaSeniat IsNot Nothing Then
                    '    FacFacturaProforma.FechaSeniat = FormatDateTime(FacFacturaProforma.FechaSeniat, DateFormat.ShortDate)
                    'End If

                    FacFacturaProforma.Auto = "0"
                    FacFacturaProforma.Terrero = "3"
                    FacFacturaProforma.Anulada = "NO"

                    FacFacturaProforma.Local = Me._ventana.Localidad
                    FacFacturaProforma.BIMulmon = Me._ventana.BIMulmon

                    'FacFacturaProforma.Descuento = Me._ventana.Desc
                    FacFacturaProforma.MSubtimpo = Me._ventana.MSubtimpo
                    FacFacturaProforma.MDescuento = Me._ventana.MDescuento
                    FacFacturaProforma.MTbimp = Me._ventana.MTbimp
                    FacFacturaProforma.Mtbexc = Me._ventana.Mtbexc
                    FacFacturaProforma.MSubtotal = Me._ventana.Msubtotal
                    FacFacturaProforma.Mtimp = Me._ventana.Mtimp
                    FacFacturaProforma.Mttotal = Me._ventana.Mttotal
                    FacFacturaProforma.MSubtimpoBf = Me._ventana.MSubtimpoBf
                    FacFacturaProforma.MDescuentoBf = Me._ventana.MDescuentoBf
                    FacFacturaProforma.MTbimpBf = Me._ventana.MTbimpBf
                    FacFacturaProforma.MTbexcBf = Me._ventana.MtbexcBf
                    FacFacturaProforma.MSubtotalBf = Me._ventana.MsubtotalBf
                    FacFacturaProforma.MTimpBf = Me._ventana.MtimpBf
                    FacFacturaProforma.MTtotalBf = Me._ventana.MttotalBf
                    If Me._ventana.Impuesto <> "" And Me._ventana.Impuesto <> Nothing And IsNumeric(Me._ventana.Impuesto) Then
                        FacFacturaProforma.Impuesto = Me._ventana.Impuesto
                    Else
                        FacFacturaProforma.Impuesto = 0
                    End If
                    FacFacturaProforma.Descuento = Me._ventana.PDescuento
                    FacFacturaProforma.CodigoDepartamento = UsuarioLogeado.Departamento.Id
                    If FacFacturaProforma.Inicial = "" Or FacFacturaProforma.Inicial = Nothing Then
                        FacFacturaProforma.Inicial = UsuarioLogeado.Iniciales
                    End If
                    If FacFacturaProforma.CodigoDepartamento = "" Or FacFacturaProforma.CodigoDepartamento = Nothing Then
                        FacFacturaProforma.CodigoDepartamento = UsuarioLogeado.Departamento.Id
                    End If


                    If Me._FacFacturaProformaServicios.InsertarOModificar(FacFacturaProforma, UsuarioLogeado.Hash) Then
                        actualizar_detalle_proforma(FacFacturaProforma.Id)
                        operacion_detalle_proforma(FacFacturaProforma.Id)
                        operacion_detalle_tm_proforma(FacFacturaProforma.Id)

                        Dim operacionaux As New FacOperacionProforma
                        operacionaux.Id = "ND"
                        operacionaux.CodigoOperacion = FacFacturaProforma.Id
                        Dim operacion As FacOperacionProforma = _FacOperacionProformasServicios.ObtenerFacOperacionProformasFiltro(operacionaux)(0)
                        'operacion.Id = "ND"
                        'operacion.CodigoOperacion = FacFacturaProforma.Id
                        'operacion.FechaOperacion = FacFacturaProforma.FechaFactura
                        operacion.Asociado = FacFacturaProforma.Asociado
                        operacion.Idioma = FacFacturaProforma.Idioma
                        operacion.Moneda = FacFacturaProforma.Moneda
                        operacion.Monto = FacFacturaProforma.Mttotal
                        operacion.Saldo = FacFacturaProforma.Mttotal
                        operacion.MontoBf = FacFacturaProforma.MTtotalBf
                        operacion.SaldoBf = FacFacturaProforma.MTtotalBf
                        'bsaldo.fac_operaciones_pro = bmonto.fac_operaciones_pro 'NOTA falto este campo
                        operacion.XOperacion = constr_xobs(FacFacturaProforma.Caso)

                        ' NOTA FALTA 
                        'xoperacion.fac_operaciones_pro = $$xobservacion
                        '              Call CONV_DATE()
                        'foperacion_imp.fac_operaciones_pro = $FECHA$
                        _FacOperacionProformasServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash)

                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFacturaProformaModificado
                        If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacFacturaProformaModificado & " Desea ir a la Pantalla Principal?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                            Me.Navegar(_paginaPrincipal)
                        End If
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

        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._FacFacturaProformaServicios.Eliminar(DirectCast(Me._ventana.FacFacturaProforma, FacFacturaProforma), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacFacturaProformaEliminado
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

            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            For i As Integer = 0 To FacFactuDetaProformas.Count - 1
                If observacion = Nothing Then
                    observacion = FacFactuDetaProformas(i).XDetalle
                Else
                    observacion = observacion & "; " & FacFactuDetaProformas(i).XDetalle
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
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
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
            Mouse.OverrideCursor = Cursors.Wait
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
            Mouse.OverrideCursor = Nothing

            'If InteresadosFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Interesados = InteresadosFiltrados
            'Else
            '    Me._ventana.Interesados = Me._Interesados
            'End If
        End Sub

        Public Sub BuscarAsociadoImp()
            Mouse.OverrideCursor = Cursors.Wait
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
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
            End If

            Dim primerasociado As New Asociado()
            primerasociado.Id = Integer.MinValue
            asociados.Insert(0, primerasociado)

            Me._ventana.AsociadosImp = asociados
            Mouse.OverrideCursor = Nothing
        End Sub

        Public Sub BuscarCarta()
            Mouse.OverrideCursor = Cursors.Wait
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
            End If

            Dim primercarta As New Carta()
            primercarta.Id = Integer.MinValue
            cartas.Insert(0, primercarta)

            Me._ventana.Cartas = cartas
            Mouse.OverrideCursor = Nothing
            'If CartasFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Cartas = CartasFiltrados
            'Else
            '    Me._ventana.Cartas = Me._Cartas
            'End If
        End Sub

        Public Sub BuscarMarca()
            'Mouse.OverrideCursor = Cursors.Wait
            Dim Marcas As IList(Of Marca)
            Dim marca As New Marca
            If Me._ventana.idMarcaFiltrar <> "" Then
                If (IsNumeric(Me._ventana.idMarcaFiltrar)) Then
                    marca.Id = Me._ventana.idMarcaFiltrar
                End If
                Me._ventana.idMarcaFiltrar = ""
            End If
            marca.Descripcion = UCase(Me._ventana.NombreMarcaFiltrar)
            Me._ventana.NombreMarcaFiltrar = ""

            marca.CodigoInscripcion = UCase(Me._ventana.CodigoInscripcion)
            Me._ventana.CodigoInscripcion = ""

            marca.CodigoRegistro = UCase(Me._ventana.Registro)
            Me._ventana.Registro = ""


            '876228
            'If marca IsNot Nothing Then
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marca)
            Me._ventana.ResultadosMarca = Marcas
            'Else
            '    Me._ventana.ResultadosMarca = Nothing
            'End If
            Me._ventana.MensajeError = ""
            'Mouse.OverrideCursor = Nothing
        End Sub

        Public Function BuscarMarca_2(ByVal id As String) As Marca
            'Mouse.OverrideCursor = Cursors.Wait
            Dim Marcas As List(Of Marca)
            Dim marcaaux As New Marca
            If id <> "" Then
                If (IsNumeric(id)) Then
                    marcaaux.Id = id
                End If
            End If
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marcaaux)
            Return Marcas(0)
            'Mouse.OverrideCursor = Nothing
        End Function

        Public Function BuscarPatente_2(ByVal id As String) As Patente
            'Mouse.OverrideCursor = Cursors.Wait
            Dim Patentes As List(Of Patente)
            Dim Patenteaux As New Patente
            If id <> "" Then
                If (IsNumeric(id)) Then
                    Patenteaux.Id = id
                End If
            End If
            Patentes = Me._PatentesServicios.ObtenerPatentesFiltro(Patenteaux)
            Return Patentes(0)
            'Mouse.OverrideCursor = Nothing
        End Function

        Public Function encontrarmarca(ByVal idmarca As String) As IList(Of Marca)
            'Mouse.OverrideCursor = Cursors.Wait
            Dim Marcas As IList(Of Marca)
            Dim marca As New Marca
            If idmarca <> "" Then
                If IsNumeric(idmarca) Then
                    marca.Id = idmarca
                End If
            End If
            Marcas = Me._MarcasServicios.ObtenerMarcasFiltro(marca)
            Return Marcas
            'Mouse.OverrideCursor = Nothing
        End Function

        Public Sub BuscarPatente()
            Dim Patentes As IList(Of Patente)
            Dim Patente As New Patente
            If Me._ventana.idPatenteFiltrar <> "" Then
                If (IsNumeric(Me._ventana.idPatenteFiltrar)) Then
                    Patente.Id = Me._ventana.idPatenteFiltrar
                End If
                Me._ventana.idPatenteFiltrar = ""
            End If
            Patente.Descripcion = UCase(Me._ventana.NombrePatenteFiltrar)
            Patente.Descripcion = ""
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
                    Me._ventana.ServicioId = ""
                End If
                If Me._ventana.ServicioCod_Cont <> "" Then
                    servicioaux.Cod_Cont = UCase(Me._ventana.ServicioCod_Cont)
                    Me._ventana.ServicioCod_Cont = ""
                End If
                If Me._ventana.ServicioXreferencia <> "" Then
                    servicioaux.Xreferencia = UCase(Me._ventana.ServicioXreferencia)
                    Me._ventana.ServicioXreferencia = ""
                End If
                If Not Me._ventana.Tipo.Equals(" "c) Then
                    servicioaux.Itipo = Me._ventana.Tipo
                    Me._ventana.Tipo = ""
                Else
                    servicioaux.Itipo = " "
                End If
                If Not Me._ventana.Localidad2.Equals(" "c) Then
                    servicioaux.Local = Me._ventana.Localidad2
                    Me._ventana.Localidad2 = ""
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

            Me._ventana.DescripcionServicio = "Servicio: " & departamento_servicio.Servicio.Id & " - " & departamento_servicio.Servicio.Xreferencia

            'esto es para que haga el desglose obligatorio
            If departamento_servicio.Servicio.Desg.ToString = "T" Or departamento_servicio.Servicio.Desg.ToString = "1" Then
                MessageBox.Show("Desglose Obligatorio", "Mesaje", MessageBoxButton.OK)
                Me._ventana.Desglose = True
            End If
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
            'Dim FacFactuDetaProforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma_2Seleccionado, FacFactuDetaProforma)
            If (departamento_servicio.Servicio.Itipo = "M") Then
                If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                    Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                    If imult.Trim = "T" Or imult = "1" Then
                        Me._ventana.VerTipo = "7" ' Multiples Marcas
                    Else
                        Me._ventana.VerTipo = "13"
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
            'Dim FacFactuDetaProforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma_2Seleccionado, FacFactuDetaProforma)
            If (departamento_servicio.Servicio.Itipo = "P") Then
                If (Me._ventana.Seleccion = False) Then 'esta si va falsa
                    Dim imult As String = departamento_servicio.Servicio.Imult.ToString.Trim
                    If imult.Trim = "T" Or imult = "1" Then
                        Me._ventana.VerTipo = "9" ' Multiples Patentes
                    Else
                        Me._ventana.VerTipo = "13"
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
                AgregarDetalleProforma()
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

        Public Sub AgregarDetalleProforma()
            Mouse.OverrideCursor = Cursors.Wait
            'para el contador
            Dim contador As New FacContadorPro
            contador.Id = "FAC_DETALLES_PRO"
            contador = _FacContadorProServicios.ConsultarPorId(contador)

            Dim valor_contador = contador.ProximoValor
            contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
            Dim exitocontador As Boolean = _FacContadorProServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)

            If exitocontador = True Then
                'agrega los valores a FacFactuDetaProforma
                InsertarDetalleProforma(valor_contador)
            End If
            'fin contador  
            Mouse.OverrideCursor = Nothing
        End Sub

        Public Sub SeleccionarDetalle()
            If DirectCast(Me._ventana.FacFactuDetaProforma_Seleccionado, FacFactuDetaProforma) IsNot Nothing Then
                Dim detalle_proforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma_Seleccionado, FacFactuDetaProforma)
                'Dim desglose_servicio As FacDesgloseServicio = DirectCast(Me._ventana.DesgloseServicio_Seleccionado, FacDesgloseServicio)
                'Dim servicio As New FacServicio
                If detalle_proforma IsNot Nothing Then

                    'Dim _FacDepartamentoServicios As IList(Of FacDepartamentoServicio)
                    'Dim departamentoservicioauxiliar As New FacDepartamentoServicio
                    'departamentoservicioauxiliar.Id = UsuarioLogeado.Departamento
                    'servicio.Id = detalle_proforma.Servicio.Id
                    'departamentoservicioauxiliar.Servicio = servicio
                    '_FacDepartamentoServicios = Me._DepartamentoserviciosServicios.ObtenerFacDepartamentoServiciosFiltro(departamentoservicioauxiliar)
                    'If _FacDepartamentoServicios(0).Servicio.Imodpr = "N" Then
                    Me._ventana.Activar_Desactivar = True
                    If detalle_proforma.Servicio.Imodpr = "F" Or detalle_proforma.Servicio.Imodpr = Nothing Then
                        Me._ventana.Desactivar_Precio = False
                        'Else
                        '    Me._ventana.Desactivar_Precio = True
                    End If

                    'esto sustituye el memsaje de abajo                    
                    Me._ventana.Desactivar_Descuento = detalle_proforma.Desactivar_Desglose


                    ' esto tengo que verificarlo con el desglose servicio
                    'If desglose_servicio.Id = "G" Then
                    '    detalle_proforma.Impuesto = "F"
                    '    detalle_proforma.Descuento = 0
                    '    Me._ventana.Desactivar_Descuento = False
                    'End If

                    If detalle_proforma.Servicio.BAimpuesto = False Then
                        detalle_proforma.Descuento = 0
                        Me._ventana.Desactivar_Descuento = True
                    End If


                    If detalle_proforma.NCantidad.ToString <> "" And detalle_proforma.NCantidad.ToString <> Nothing Then
                        Me._ventana.NCantidad = detalle_proforma.NCantidad
                    Else
                        Me._ventana.NCantidad = 0
                    End If
                    If detalle_proforma.Pu.ToString <> "" And detalle_proforma.Pu.ToString <> Nothing Then
                        Me._ventana.Pu = detalle_proforma.Pu
                    Else
                        Me._ventana.Pu = 0
                    End If
                    If detalle_proforma.Descuento.ToString <> "" And detalle_proforma.Descuento.ToString <> Nothing Then
                        Me._ventana.Descuento = detalle_proforma.Descuento
                    Else
                        Me._ventana.Descuento = 0
                    End If
                    If detalle_proforma.BDetalle.ToString <> "" And detalle_proforma.BDetalle.ToString <> Nothing Then
                        Me._ventana.BDetalle = detalle_proforma.BDetalle
                    Else
                        Me._ventana.BDetalle = 0
                    End If
                End If
            End If
        End Sub

        Public Sub focus()
            If DirectCast(Me._ventana.FacFactuDetaProforma_Seleccionado, FacFactuDetaProforma) IsNot Nothing Then
                Dim detalle_proforma As FacFactuDetaProforma = DirectCast(Me._ventana.FacFactuDetaProforma_Seleccionado, FacFactuDetaProforma)

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

                Me._ventana.BDetalle = (Me._ventana.Pu * Me._ventana.NCantidad)
                detalle_proforma.BDetalle = Me._ventana.BDetalle
                detalle_proforma.Pu = Me._ventana.Pu
                detalle_proforma.Descuento = Me._ventana.Descuento
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
                            detalle_proforma.Pu = Me._ventana.Pu
                            detalle_proforma.PuBf = Me._ventana.Pu
                        Else
                            If moneda.Id = "US" Then
                                Dim tasa As Tasa = buscar_tasa(Date.Now, "")
                                If tasa IsNot Nothing Then
                                    detalle_proforma.PuBf = (Me._ventana.Pu * tasa.Tasabf)
                                    detalle_proforma.PuBf = detalle_proforma.PuBf
                                End If
                            End If

                        End If
                    End If
                End If
                detalle_proforma.BDetalleBf = (detalle_proforma.PuBf * Me._ventana.NCantidad)
                detalle_proforma.NCantidad = Me._ventana.NCantidad
                'Dim guardar As Boolean = _FacFactuDetaProformasServicios.InsertarOModificar(detalle_proforma, UsuarioLogeado.Hash)
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

        Public Sub ModificarGridFacFactuDetaProforma(ByVal detalle_proforma As FacFactuDetaProforma)
            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))

            Dim i As Integer = 0
            Dim ver As Boolean = True
            While ((i <= FacFactuDetaProformas.Count - 1) And (ver = True))

                If (FacFactuDetaProformas(i).Id = detalle_proforma.Id) Then
                    FacFactuDetaProformas(i).Pu = detalle_proforma.Pu
                    FacFactuDetaProformas(i).PuBf = detalle_proforma.PuBf
                    FacFactuDetaProformas(i).NCantidad = detalle_proforma.NCantidad
                    FacFactuDetaProformas(i).BDetalle = detalle_proforma.BDetalle
                    FacFactuDetaProformas(i).BDetalleBf = detalle_proforma.BDetalleBf
                    FacFactuDetaProformas(i).Descuento = detalle_proforma.Descuento
                    ver = False
                End If
                i = i + 1
            End While
            'For i As Integer = 0 To FacFactuDetaProformas.Count - 1
            '    If (FacFactuDetaProformas.Item(i).Id = detalle_proforma.Id) Then
            '        FacFactuDetaProformas.Item(i).Pu = detalle_proforma.Pu
            '        FacFactuDetaProformas.Item(i).PuBf = detalle_proforma.PuBf
            '        FacFactuDetaProformas.Item(i).NCantidad = detalle_proforma.NCantidad
            '        FacFactuDetaProformas.Item(i).BDetalle = detalle_proforma.BDetalle
            '        FacFactuDetaProformas.Item(i).BDetalleBf = detalle_proforma.BDetalleBf
            '        FacFactuDetaProformas.Item(i).Descuento = detalle_proforma.Descuento
            '    End If
            'Next

            Me._ventana.ResultadosFacFactuDetaProforma = Nothing
            Me._ventana.ResultadosFacFactuDetaProforma = FacFactuDetaProformas
            Me._ventana.MensajeError = ""
        End Sub

        Public Sub ElimDepartamentoServicios()
            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            If FacFactuDetaProformas IsNot Nothing Then
                Dim FacFactuDetaProformasaux As New List(Of FacFactuDetaProforma)
                Dim j As Integer = 0
                For i As Integer = 0 To FacFactuDetaProformas.Count - 1
                    If (FacFactuDetaProformas.Item(i).Seleccion = False) Then

                        FacFactuDetaProformasaux.Add(New FacFactuDetaProforma)
                        FacFactuDetaProformasaux(j) = FacFactuDetaProformas.Item(i)
                        j = j + 1

                    Else
                        'aqui va lo de eliminar los operacion detale prof
                    End If
                Next

                Me._ventana.ResultadosFacFactuDetaProforma = Nothing
                Me._ventana.ResultadosFacFactuDetaProforma = FacFactuDetaProformasaux

                Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                If moneda IsNot Nothing Then
                    recalcular(moneda.Id)
                Else
                    recalcular("")
                End If
                Me._ventana.MensajeError = ""
            End If
        End Sub

        Public Sub buscar_departamento_servicio_cambiar_español_ingles(ByVal detalle As FacFactuDetaProforma)
            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            If FacFactuDetaProformas IsNot Nothing Then
                Dim i As Integer = 0
                While (i < (FacFactuDetaProformas.Count))
                    If (FacFactuDetaProformas.Item(i).Id = detalle.Id) Then

                        FacFactuDetaProformas.Item(i) = detalle

                        i = FacFactuDetaProformas.Count
                    End If
                    i = i + 1
                End While
            End If
            Me._ventana.ResultadosFacFactuDetaProforma = Nothing
            Me._ventana.ResultadosFacFactuDetaProforma = FacFactuDetaProformas
        End Sub

        Public Sub recalcular(ByVal moneda As String)
            Dim MSubtimpo, MDescuento, MTbimp, Mtbexc, Msubtotal, Mtimp, Mttotal, MSubtimpoBf, MDescuentoBf, MTbimpBf, MtbexcBf, MsubtotalBf, MtimpBf, MttotalBf As Double

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

            'trabajo con variables y despues se la asigno a los txt
            MSubtimpo = 0
            MDescuento = 0
            MTbimp = 0
            Mtbexc = 0
            Msubtotal = 0
            Mtimp = 0
            Mttotal = 0
            MSubtimpoBf = 0
            MDescuentoBf = 0
            MTbimpBf = 0
            MtbexcBf = 0
            MsubtotalBf = 0
            MtimpBf = 0
            MttotalBf = 0

            Dim w_monto, w_monto_bf As Double


            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            If FacFactuDetaProformas IsNot Nothing Then
                Dim tasa As Tasa = buscar_tasa(CDate(Me._ventana.FechaFactura), moneda)
                'Dim guardar As Boolean
                For i As Integer = 0 To FacFactuDetaProformas.Count - 1
                    If moneda = "US" Then
                        If tasa IsNot Nothing Then
                            FacFactuDetaProformas(i).BDetalleBf = FormatNumber((FacFactuDetaProformas(i).BDetalle * tasa.Tasabf), 2)
                            FacFactuDetaProformas(i).PuBf = FormatNumber((FacFactuDetaProformas(i).Pu * tasa.Tasabf), 2)
                        End If
                    Else
                        FacFactuDetaProformas(i).BDetalleBf = FormatNumber((FacFactuDetaProformas(i).BDetalle), 2)
                        FacFactuDetaProformas(i).PuBf = FormatNumber(FacFactuDetaProformas(i).Pu, 2)
                    End If

                    FacFactuDetaProformas(i).Pu = FormatNumber(FacFactuDetaProformas(i).Pu, 2)
                    FacFactuDetaProformas(i).BDetalle = FormatNumber(FacFactuDetaProformas(i).BDetalle, 2)

                    'guarda los cambios en el detalle
                    'guardar = _FacFactuDetaProformasServicios.InsertarOModificar(FacFactuDetaProformas(i), UsuarioLogeado.Hash)

                    w_monto = FacFactuDetaProformas(i).BDetalle
                    w_monto_bf = FacFactuDetaProformas(i).BDetalleBf

                    If FacFactuDetaProformas(i).Impuesto = "T" Or FacFactuDetaProformas(i).Impuesto = "1" Then
                        MSubtimpo = MSubtimpo + w_monto
                        MSubtimpoBf = MSubtimpoBf + w_monto_bf
                    Else
                        Mtbexc = (Mtbexc + w_monto)
                        MtbexcBf = (MtbexcBf + w_monto_bf)
                    End If
                    MDescuento = MDescuento + ((FacFactuDetaProformas(i).Pu * FacFactuDetaProformas(i).NCantidad) * FacFactuDetaProformas(i).Descuento) / 100
                    MDescuento = MDescuento

                    MDescuentoBf = MDescuentoBf + ((FacFactuDetaProformas(i).PuBf * FacFactuDetaProformas(i).NCantidad) * FacFactuDetaProformas(i).Descuento) / 100
                    MDescuentoBf = MDescuentoBf
                Next

                MTbimp = MSubtimpo - MDescuento
                MTbimpBf = MSubtimpoBf - MDescuentoBf

                Msubtotal = MTbimp + Mtbexc
                MsubtotalBf = MTbimpBf + MtbexcBf
                If Me._ventana.Impuesto = "" Or Me._ventana.Impuesto = Nothing Or Not IsNumeric(Me._ventana.Impuesto) Then
                    Mtimp = MTbimp * (0 / 100)
                    MtimpBf = MTbimpBf * (0 / 100)
                Else
                    Mtimp = MTbimp * (Me._ventana.Impuesto / 100)
                    MtimpBf = MTbimpBf * (Me._ventana.Impuesto / 100)
                End If
                Mttotal = Msubtotal + Mtimp
                MttotalBf = MsubtotalBf + MtimpBf

                Mtimp = Mtimp
                MtimpBf = MtimpBf

                Mttotal = Mttotal
                MttotalBf = MttotalBf



                Me._ventana.MSubtimpo = MSubtimpo
                Me._ventana.MDescuento = MDescuento
                Me._ventana.MTbimp = MTbimp
                Me._ventana.Mtbexc = Mtbexc
                Me._ventana.Msubtotal = Msubtotal
                Me._ventana.Mtimp = Mtimp
                Me._ventana.Mttotal = Mttotal
                Me._ventana.MSubtimpoBf = MSubtimpoBf
                Me._ventana.MDescuentoBf = MDescuentoBf
                Me._ventana.MTbimpBf = MTbimpBf
                Me._ventana.MtbexcBf = MtbexcBf
                Me._ventana.MsubtotalBf = MsubtotalBf
                Me._ventana.MtimpBf = MtimpBf
                Me._ventana.MttotalBf = MttotalBf


                Me._ventana.ResultadosFacFactuDetaProforma = Nothing
                Me._ventana.ResultadosFacFactuDetaProforma = FacFactuDetaProformas
                Me._ventana.Activar_Desactivar = False
                Me._ventana.NCantidad = 0
                Me._ventana.Pu = 0
                Me._ventana.BDetalle = 0
                Me._ventana.Descuento = 0
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
        Public Sub actualizar_detalle_proforma(ByVal idfactura As Integer)
            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            Dim facfacturaproforma As New FacFacturaProforma
            facfacturaproforma.Id = idfactura
            Dim guardar As Boolean
            For i As Integer = 0 To FacFactuDetaProformas.Count - 1
                FacFactuDetaProformas(i).Factura = facfacturaproforma
                'guarda los cambios en el detalle
                guardar = _FacFactuDetaProformasServicios.InsertarOModificar(FacFactuDetaProformas(i), UsuarioLogeado.Hash)
            Next
        End Sub

        Public Sub InsertarDetalleProforma(ByVal contador As Integer)
            Dim departamento_servicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicio_2Seleccionado, FacDepartamentoServicio)
            Dim facfactudetaproforma As New FacFactuDetaProforma
            Dim moneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
            Dim idioma As Idioma = DirectCast(Me._ventana.Idioma, Idioma)
            Dim w_paso As Integer = 1
            Dim cstr1 As String = Nothing

            Try

                facfactudetaproforma.Id = contador
                facfactudetaproforma.Servicio = departamento_servicio.Servicio

                facfactudetaproforma.TipoServicio = departamento_servicio.Servicio.Itipo
                facfactudetaproforma.Impuesto = departamento_servicio.Servicio.Aimpuesto
                facfactudetaproforma.Desglose = departamento_servicio.Servicio.Desg

                If departamento_servicio.Servicio.BImodpr = False Then ' verificar este campo en el detalle facfactudetaproforma.imodpr
                    'facfactudetaproforma.imodpr = "N"
                Else
                    'facfactudetaproforma.imodpr = "S"
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
                        facfactudetaproforma.BDetalle = tarifaservicios(0).Mont_Bs
                        facfactudetaproforma.Pu = tarifaservicios(0).Mont_Bs
                    End If
                    If (moneda.Id = "US") Then
                        facfactudetaproforma.BDetalle = tarifaservicios(0).Mont_Us
                        facfactudetaproforma.Pu = tarifaservicios(0).Mont_Us
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
                        facfactudetaproforma.BDetalle = (tarifaservicios(0).Mont_Us * btasa)
                        facfactudetaproforma.Pu = (tarifaservicios(0).Mont_Us * btasa)
                    End If
                    facfactudetaproforma.BDetalleBf = (tarifaservicios(0).Mont_Us * btasa)
                    facfactudetaproforma.PuBf = (tarifaservicios(0).Mont_Us * btasa)
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
                Dim cttra As String = Nothing 'documento traduccion
                Dim w_recurso As String = Nothing 'fac recurso
                Dim w_material As String = Nothing 'material
                Dim lista_marca_patente(4) As String
                Dim multiples_marca_patente As Boolean = False
                Dim marca_patente As Boolean = False
                Dim v_anualidad As String = Nothing 'Anualidad
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
                        facfactudetaproforma.BDetalleBf = monto_bf
                        facfactudetaproforma.PuBf = monto_bf

                        If (moneda.Id = "BF") Then
                            facfactudetaproforma.BDetalle = monto_bf
                            facfactudetaproforma.Pu = monto_bf
                        End If
                    End If

                    If (moneda.Id = "BS") Then
                        If (tipodoc_bs = True) Then
                            facfactudetaproforma.BDetalle = monto_bs
                            facfactudetaproforma.Pu = monto_bs
                        End If
                    End If

                    If (moneda.Id = "US") Then
                        If (tipodoc_us = True) Then
                            facfactudetaproforma.BDetalle = monto_us
                            facfactudetaproforma.Pu = monto_us
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
                        facfactudetaproforma.BDetalle = (facfactudetaproforma.BDetalle * cantidad)
                        facfactudetaproforma.NCantidad = cantidad
                        facfactudetaproforma.Pu = (facfactudetaproforma.BDetalle / cantidad)

                        facfactudetaproforma.BDetalleBf = (facfactudetaproforma.BDetalleBf * cantidad)
                        facfactudetaproforma.PuBf = (facfactudetaproforma.BDetalleBf / cantidad)
                    End If

                    If marca_patente = True Or multiples_marca_patente = True Then

                        If Me._ventana.Ourref = "" Or Me._ventana.Ourref = Nothing Then
                            Me._ventana.Ourref = lista_marca_patente(1)
                        Else
                            Me._ventana.Ourref = Me._ventana.Ourref & " , " & lista_marca_patente(1)
                        End If

                        If Me._ventana.Caso = "" Or Me._ventana.Caso = Nothing Then
                            Me._ventana.Caso = lista_marca_patente(2)
                        Else
                            Me._ventana.Caso = Me._ventana.Caso & " / " & lista_marca_patente(2)
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
                        facfactudetaproforma.Codigo = v_ninter
                        'facfactudetaproforma.ioperacion = v_itipo
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
                        facfactudetaproforma.NCantidad = 1

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
                    facfactudetaproforma.XDetalle = ccampo
                    facfactudetaproforma.XDetalleEs = ccampo_es
                    If ((departamento_servicio.Servicio.Itipo = "C") And (v_pagina IsNot Nothing)) Then
                        facfactudetaproforma.Pu = facfactudetaproforma.BDetalle
                        facfactudetaproforma.NCantidad = v_pagina
                        facfactudetaproforma.BDetalle = (facfactudetaproforma.NCantidad * facfactudetaproforma.Pu)

                        facfactudetaproforma.PuBf = facfactudetaproforma.BDetalleBf
                        facfactudetaproforma.NCantidad = v_pagina
                        facfactudetaproforma.BDetalleBf = (facfactudetaproforma.NCantidad * facfactudetaproforma.PuBf)
                    End If
                End If 'If ccampo <> "" And ccampo <> Nothing Then

                If (w_paso = 0) Then
                    facfactudetaproforma.Servicio = Nothing
                    facfactudetaproforma.XDetalle = ""
                    facfactudetaproforma.BDetalle = 0
                    facfactudetaproforma.NCantidad = 0
                    facfactudetaproforma.Pu = 0
                    facfactudetaproforma.Descuento = 0
                    facfactudetaproforma.Bsel = "True"
                    Me._ventana.Seleccion = True
                    Exit Sub
                End If
                If facfactudetaproforma.Impuesto = "T" Then
                    If IsNumeric(Me._ventana.Desc) = True Then
                        facfactudetaproforma.Descuento = Me._ventana.Desc
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
                If departamento_servicio.Servicio.BImodpr = False Then ' verificar este campo en el detalle facfactudetaproforma.imodpr
                    'facfactudetaproforma.imodpr = "N"
                    Me._ventana.Activar_Desactivar = False
                Else
                    'facfactudetaproforma.imodpr = "S"
                    Me._ventana.Activar_Desactivar = True
                End If

                facfactudetaproforma.Desactivar_Desglose = True 'para activarlo por defecto

                If Me._ventana.Desglose = True Then
                    Dim desglose_servicio As FacDesgloseServicio = DirectCast(Me._ventana.DesgloseServicio_Seleccionado, FacDesgloseServicio)
                    If desglose_servicio IsNot Nothing Then
                        If desglose_servicio.Id = "G" Then
                            facfactudetaproforma.Desactivar_Desglose = False
                        End If
                        If desglose_servicio.Servicio IsNot Nothing Then
                            If facfactudetaproforma.Pu.ToString <> "" And desglose_servicio.Pporc.ToString <> "" Then
                                facfactudetaproforma.Pu = (facfactudetaproforma.Pu * desglose_servicio.Pporc / 100)
                            Else
                                facfactudetaproforma.Pu = 0
                                facfactudetaproforma.PuBf = 0
                            End If
                            If facfactudetaproforma.Descuento.ToString <> "" And facfactudetaproforma.NCantidad.ToString <> "" Then
                                facfactudetaproforma.BDetalle = (facfactudetaproforma.Pu * facfactudetaproforma.NCantidad) * (1 - (facfactudetaproforma.Descuento / 100))
                                facfactudetaproforma.BDetalle = facfactudetaproforma.BDetalle
                                facfactudetaproforma.BDetalleBf = (facfactudetaproforma.PuBf * facfactudetaproforma.NCantidad) * (1 - (facfactudetaproforma.Descuento / 100))
                                facfactudetaproforma.BDetalleBf = facfactudetaproforma.BDetalleBf
                            Else
                                facfactudetaproforma.BDetalle = 0
                                facfactudetaproforma.BDetalleBf = 0
                                If facfactudetaproforma.NCantidad.ToString = Nothing Or facfactudetaproforma.NCantidad.ToString = "" Then
                                    facfactudetaproforma.NCantidad = 0
                                End If
                                If facfactudetaproforma.Descuento.ToString = Nothing Or facfactudetaproforma.Descuento.ToString = "" Then
                                    facfactudetaproforma.Descuento = 0
                                End If
                            End If
                            Dim desglosecoleaux As New FacDesgloseCole
                            desglosecoleaux.Id = desglose_servicio.Id
                            desglosecoleaux.Idioma = DirectCast(Me._ventana.Idioma, Idioma)
                            Dim desglosecoles As List(Of FacDesgloseCole) = _FacDesgloseColesServicios.ObtenerFacDesgloseColesFiltro(desglosecoleaux)
                            If desglosecoles IsNot Nothing Then
                                If desglosecoles.Count > 0 Then
                                    If DirectCast(Me._ventana.Idioma, Idioma).Id = "ES" Then
                                        facfactudetaproforma.XDetalle = desglosecoles(0).Detalle & " " & facfactudetaproforma.XDetalle
                                        facfactudetaproforma.XDetalleEs = desglosecoles(0).Detalle & " " & facfactudetaproforma.XDetalleEs
                                    Else
                                        facfactudetaproforma.XDetalle = desglosecoles(0).Detalle & " " & facfactudetaproforma.XDetalle
                                        desglosecoleaux.Id = desglose_servicio.Id
                                        Dim idiomacole As New Idioma
                                        idiomacole.Id = "ES"
                                        desglosecoleaux.Idioma = idiomacole
                                        desglosecoles = _FacDesgloseColesServicios.ObtenerFacDesgloseColesFiltro(desglosecoleaux)
                                        If desglosecoles IsNot Nothing Then
                                            If desglosecoles.Count > 0 Then
                                                facfactudetaproforma.XDetalleEs = desglosecoles(0).Detalle & " " & facfactudetaproforma.XDetalleEs
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    End If 'If desglose_servicio.Servicio IsNot Nothing Then

                    If facfactudetaproforma.Impuesto = "F" Then
                        facfactudetaproforma.Descuento = 0
                        Me._ventana.Desactivar_Descuento = True
                    End If

                    If desglose_servicio.Id = "G" Then
                        facfactudetaproforma.Impuesto = "F"
                        facfactudetaproforma.Descuento = 0
                        Me._ventana.Desactivar_Descuento = True
                    End If

                    Me._ventana.Desactivar_Descuento = facfactudetaproforma.Desactivar_Desglose
                End If 'If Me._ventana.Desglose = True Then

                facfactudetaproforma.BBsel = Me._ventana.Seleccion
                facfactudetaproforma.BDesglose = Me._ventana.Desglose
                'GUARDAR EL DETALLE DE LA PROFORMA
                Dim factura_proforma As New FacFacturaProforma
                Dim guardar As Boolean = False
                factura_proforma.Id = 0
                facfactudetaproforma.Factura = factura_proforma
                'guardar = _FacFactuDetaProformasServicios.InsertarOModificar(facfactudetaproforma, UsuarioLogeado.Hash)
                'If guardar = True Then
                agrega_detalle(facfactudetaproforma)
                recalcular(moneda.Id)
                ' End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub agrega_detalle(ByVal detalle_proforma As FacFactuDetaProforma)
            Dim detallesaux As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            Dim detalles As New List(Of FacFactuDetaProforma)
            Dim i As Integer = 0
            Me._ventana.VerTipo = "13"
            If detallesaux IsNot Nothing Then
                detalles = detallesaux
                i = detallesaux.Count
            End If

            detalles.Add(New FacFactuDetaProforma)
            If detalle_proforma.NCantidad Is Nothing Then
                detalle_proforma.NCantidad = 1
            Else
                If detalle_proforma.BBsel = True And detalle_proforma.NCantidad = 0 Then
                    detalle_proforma.NCantidad = 1
                End If
            End If

            If detalle_proforma.Servicio.BAimpuesto <> False Then
                Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                detalle_proforma.Descuento = asociado.Descuento
                If asociado.Descuento < 25 And DirectCast(Me._ventana.Moneda, Moneda).Id = "BF" Then
                    detalle_proforma.Descuento = 25
                End If
            Else
                detalle_proforma.Descuento = 0
            End If

            detalle_proforma.BDetalle = (detalle_proforma.Pu * detalle_proforma.NCantidad) * (1 - (detalle_proforma.Descuento / 100))
            detalle_proforma.BDetalle = detalle_proforma.BDetalle
            detalle_proforma.BDetalleBf = (detalle_proforma.PuBf * detalle_proforma.NCantidad) * (1 - (detalle_proforma.Descuento / 100))
            detalle_proforma.BDetalleBf = detalle_proforma.BDetalleBf

            detalles(i) = detalle_proforma
            Me._ventana.ResultadosFacFactuDetaProforma = Nothing
            Me._ventana.ResultadosFacFactuDetaProforma = detalles
            Me._ventana.FacFactuDetaProforma_Seleccionado = detalle_proforma
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

        Public Sub operacion_detalle_proforma(ByVal idfactura As String)
            elim_operacion_detalle_proforma(idfactura)
            agregar_modificar_operacion_detalle_proforma(idfactura)

            'Dim FacOperacionDetaproforma As List(Of FacOperacionDetaProforma)
            'Dim FacOperacionDetaproformaaux As New FacOperacionDetaProforma
            'Dim facturaproforma As New FacFacturaProforma
            'facturaproforma.Id = idfactura
            'Dim elim As Boolean = False

            'FacOperacionDetaproformaaux.Factura = facturaproforma
            'FacOperacionDetaproforma = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(FacOperacionDetaproformaaux)
            'If FacOperacionDetaproforma IsNot Nothing Then
            '    For i As Integer = 0 To FacOperacionDetaproforma.Count - 1
            '        elim = _FacOperacionDetaProformasServicios.Eliminar(FacOperacionDetaproforma(i), UsuarioLogeado.Hash)
            '    Next
            'End If
            'Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            'Dim facfacturaproforma As New FacFacturaProforma

            'Dim guardar As Boolean
            'For i As Integer = 0 To FacFactuDetaProformas.Count - 1
            '    'if (itiposerv.fac_detalles_pro != "C" & itiposerv.fac_detalles_pro != "E") & (bsel.fac_detalles_pro = 1 | bsel.fac_detalles_pro = "T")
            '    If (FacFactuDetaProformas(i).TipoServicio <> "C" And FacFactuDetaProformas(i).TipoServicio <> "E") And (FacFactuDetaProformas(i).BBsel = True) Then
            '        FacOperacionDetaproformaaux.Factura = facturaproforma
            '        FacOperacionDetaproforma = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(FacOperacionDetaproformaaux)
            '        If FacOperacionDetaproforma Is Nothing Then
            '            FacOperacionDetaproformaaux.Codigo = FacFactuDetaProformas(i).Codigo
            '            FacOperacionDetaproformaaux.Factura = FacFactuDetaProformas(i).Factura
            '            FacOperacionDetaproformaaux.Detalle = FacFactuDetaProformas(i).Id
            '            FacOperacionDetaproformaaux.Id = "ND"
            '            FacOperacionDetaproformaaux.Servicio = FacFactuDetaProformas(i).Servicio.Id                        
            '            guardar = _FacOperacionDetaProformasServicios.InsertarOModificar(FacFactuDetaProformas(i), UsuarioLogeado.Hash)
            '        End If
            '    End If
            'Next
        End Sub

        Public Sub elim_operacion_detalle_proforma(ByVal idfactura As String)
            Dim FacOperacionDetaproforma As List(Of FacOperacionDetaProforma)
            Dim FacOperacionDetaproformaaux As New FacOperacionDetaProforma
            Dim facturaproforma As New FacFacturaProforma
            facturaproforma.Id = idfactura
            Dim elim As Boolean = False

            FacOperacionDetaproformaaux.Factura = facturaproforma
            FacOperacionDetaproforma = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(FacOperacionDetaproformaaux)
            If FacOperacionDetaproforma IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDetaproforma.Count - 1
                    elim = _FacOperacionDetaProformasServicios.Eliminar(FacOperacionDetaproforma(i), UsuarioLogeado.Hash)
                Next
            End If
        End Sub

        Public Sub agregar_modificar_operacion_detalle_proforma(ByVal idfactura As String)
            Dim FacOperacionDetaproforma As List(Of FacOperacionDetaProforma)
            Dim FacOperacionDetaproformaaux As New FacOperacionDetaProforma
            Dim facturaproforma As New FacFacturaProforma
            facturaproforma.Id = idfactura

            Dim FacFactuDetaProformas As List(Of FacFactuDetaProforma) = DirectCast(Me._ventana.ResultadosFacFactuDetaProforma, List(Of FacFactuDetaProforma))
            Dim facfacturaproforma As New FacFacturaProforma

            Dim guardar As Boolean
            For i As Integer = 0 To FacFactuDetaProformas.Count - 1
                'if (itiposerv.fac_detalles_pro != "C" & itiposerv.fac_detalles_pro != "E") & (bsel.fac_detalles_pro = 1 | bsel.fac_detalles_pro = "T")
                If (FacFactuDetaProformas(i).TipoServicio <> "C" And FacFactuDetaProformas(i).TipoServicio <> "E") And (FacFactuDetaProformas(i).BBsel = True) Then
                    FacOperacionDetaproformaaux.Factura = facturaproforma
                    FacOperacionDetaproforma = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(FacOperacionDetaproformaaux)
                    If FacOperacionDetaproforma Is Nothing Or FacOperacionDetaproforma.Count <= 0 Then
                        Dim FacOperacionDetaproformaaux2 As New FacOperacionDetaProforma
                        FacOperacionDetaproformaaux2.Codigo = FacFactuDetaProformas(i).Codigo
                        FacOperacionDetaproformaaux2.Factura = FacFactuDetaProformas(i).Factura
                        FacOperacionDetaproformaaux2.Detalle = FacFactuDetaProformas(i).Id
                        FacOperacionDetaproformaaux2.Id = "ND"
                        FacOperacionDetaproformaaux2.Servicio = FacFactuDetaProformas(i).Servicio
                        guardar = _FacOperacionDetaProformasServicios.InsertarOModificar(FacOperacionDetaproformaaux2, UsuarioLogeado.Hash)
                    End If
                End If
            Next
        End Sub

        Public Sub operacion_detalle_tm_proforma(ByVal idfactura As String)
            Dim FacOperacionDetaTmproforma As List(Of FacOperacionDetaTmProforma)
            Dim FacOperacionDetaTmproformaaux As New FacOperacionDetaTmProforma
            Dim facturaproforma As New FacFacturaProforma
            facturaproforma.Id = idfactura
            Dim elim As Boolean = False

            FacOperacionDetaTmproformaaux.Factura = facturaproforma
            FacOperacionDetaTmproformaaux.Usuario = UsuarioLogeado
            FacOperacionDetaTmproforma = _FacOperacionDetaTmProformasServicios.ObtenerFacOperacionDetaTmProformasFiltro(FacOperacionDetaTmproformaaux)
            If FacOperacionDetaTmproforma IsNot Nothing Then
                For i As Integer = 0 To FacOperacionDetaTmproforma.Count - 1
                    elim = _FacOperacionDetaTmProformasServicios.Eliminar(FacOperacionDetaTmproforma(i), UsuarioLogeado.Hash)
                Next
            End If

            Dim FacOperacionDetalleTm As List(Of FacOperacionDetalleTm)
            Dim FacOperacionDetalleTmaux As New FacOperacionDetalleTm
            FacOperacionDetalleTmaux.Usuario = UsuarioLogeado
            FacOperacionDetalleTm = _FacOperacionDetalleTmsServicios.ObtenerFacOperacionDetalleTmsFiltro(FacOperacionDetalleTmaux)
            If FacOperacionDetalleTm IsNot Nothing Then
                Dim guardar As Boolean
                For i As Integer = 0 To FacOperacionDetalleTm.Count - 1
                    Dim FacOperacionDetaproforma As New FacOperacionDetaProforma
                    FacOperacionDetaproforma.Codigo = FacOperacionDetalleTm(i).Codigo
                    FacOperacionDetaproforma.Factura = facturaproforma
                    FacOperacionDetaproforma.Detalle = FacOperacionDetalleTm(i).Detalle
                    FacOperacionDetaproforma.Id = "ND"
                    FacOperacionDetaproforma.Servicio = FacOperacionDetalleTm(i).Servicio
                    guardar = _FacOperacionDetaProformasServicios.InsertarOModificar(FacOperacionDetaproforma, UsuarioLogeado.Hash)

                    Dim FacOperacionDetaTmproformaaux2 As New FacOperacionDetaTmProforma
                    FacOperacionDetaTmproformaaux2.Id = FacOperacionDetalleTm(i).Id
                    FacOperacionDetaTmproformaaux2.Codigo = FacOperacionDetalleTm(i).Codigo
                    FacOperacionDetaTmproformaaux2.Detalle = FacOperacionDetalleTm(i).Detalle
                    FacOperacionDetaTmproformaaux2.Factura = facturaproforma
                    FacOperacionDetaTmproformaaux2.Usuario = FacOperacionDetalleTm(i).Usuario
                    FacOperacionDetaTmproformaaux2.Servicio = FacOperacionDetalleTm(i).Servicio
                    guardar = _FacOperacionDetaTmProformasServicios.InsertarOModificar(FacOperacionDetaTmproformaaux2, UsuarioLogeado.Hash)
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
                        v_lista_1(17) = documento_traduccion.Doc_Ingl '("Tdoc_Traducc")
                        v_lista_1_es(17) = documento_traduccion.Doc_Esp '("Tdoc_Traducc")
                    End If
                End If

                If (v_anual <> "") Then
                    Dim Anualidad As FacAnualidad = DirectCast(Me._ventana.Anualidad_Seleccionado, FacAnualidad)
                    If (v_idioma = "ES") Then
                        v_lista_1(18) = Anualidad.Doc_Esp '("Anualidad")
                        v_lista_1_es(18) = Anualidad.Doc_Esp '("Anualidad")
                    Else
                        v_lista_1(18) = Anualidad.Doc_Ingl '("Anualidad")
                        v_lista_1_es(18) = Anualidad.Doc_Esp '("Anualidad")
                    End If
                End If

                If (v_recurso <> "") Then
                    Dim Recurso As FacRecurso = DirectCast(Me._ventana.Recurso_Seleccionado, FacRecurso)
                    If (v_idioma = "ES") Then
                        v_lista_1(19) = Recurso.Doc_Esp '("Recurso")
                        v_lista_1_es(19) = Recurso.Doc_Esp '("Recurso")
                    Else
                        v_lista_1(19) = Recurso.Doc_Ingl '("Recurso")
                        v_lista_1_es(19) = Recurso.Doc_Esp '("Recurso")
                    End If
                End If

                If (v_material <> "") Then
                    Dim Material As Material = DirectCast(Me._ventana.Material_Seleccionado, Material)
                    If (v_idioma = "ES") Then
                        v_lista_1(20) = Material.Doc_Esp '("Material")
                        v_lista_1_es(20) = Material.Doc_Esp '("Material")
                    Else
                        v_lista_1(20) = Material.Doc_Ingl '("Material")
                        v_lista_1_es(20) = Material.Doc_Esp '("Material")
                    End If
                End If

                If (v_pagina <> "") Then
                    v_lista_1(21) = v_pagina '("NPaginas")
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
                        ' esto es para que borre la direccion del interesado
                        Me._ventana.XAsociado = ""
                        Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                        'adivinar(asociado, DirectCast(Me._ventana.Moneda, Moneda))
                        xasociado(asociado)
                        Me._ventana.NombreInteresado = Nothing
                    End If
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothingo
            End Try
        End Sub

        Public Sub xasociado(ByVal asociado As Asociado)
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
                If (asociado.Tarifa IsNot Nothing) Then
                    If (asociado.Tarifa.Id <> "") Then
                        Me._ventana.Tarifa = asociado.Tarifa.Id
                    End If
                End If
                If (asociado.Etiqueta IsNot Nothing) Then
                    If (asociado.Etiqueta.Id <> "") Then
                        Me._ventana.Codeti = asociado.Etiqueta.Id
                    End If
                End If
                If (asociado.Rif IsNot Nothing) And (asociado.Rif <> "") Then
                    Me._ventana.Rif = asociado.Rif
                End If
                If (asociado.Nit IsNot Nothing) And (asociado.Nit <> "") Then
                    Me._ventana.XNit = asociado.Nit
                End If

                'Dim xasociado As String = ""
                'If (asociado.Nombre IsNot Nothing) AndAlso (asociado.Nombre <> "") Then
                '    xasociado = asociado.Nombre & ControlChars.NewLine
                'End If
                'If (asociado.Domicilio IsNot Nothing) AndAlso (asociado.Domicilio <> "") Then
                '    xasociado = xasociado & asociado.Domicilio & ControlChars.NewLine
                'End If
                'If (asociado.Pais IsNot Nothing) Then
                '    Dim paises As IList(Of Pais) = Me._paisesServicios.ConsultarTodos()
                '    'Dim pais As Pais = Me.BuscarPais(asociado.Pais)
                '    Dim pais As Pais = BuscarPais(paises, asociado.Pais)
                '    xasociado = xasociado & pais.NombreIngles
                'End If

                'Me._ventana.XAsociado = xasociado
                xasociado(asociado)
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
