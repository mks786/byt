Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaAnuladas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaAnuladas
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.FacFacturaAnuladas
    Class PresentadorAgregarFacFacturaAnulada
        Inherits PresentadorBase
        Private _ventana As IAgregarFacFacturaAnulada
        Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        Private _FacAnuladaFisicaServicios As IFacAnuladaFisicaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios
        'Private _contactoServicios As IContactoServicios        
        Private _facoperacionesServicios As IFacOperacionServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios
        Private _asociados As IList(Of Asociado)
        Private _FacOperaciones As IList(Of FacOperacion)
        Private _tasasServicios As ITasaServicios
        Private _FacCreditoServicios As IFacCreditoServicios
        Private _contadorfacServicios As IContadorFacServicios
        Private _bancosServicios As IFacBancoServicios
        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacCobroFacturaServicios As IFacCobroFacturaServicios
        Private _FacInternacionalesServicios As IFacInternacionalServicios
        Private _FacInternacionalAnuladasServicios As IFacInternacionalAnuladaServicios
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private _FacContadorServicios As IContadorFacServicios
        Private _facoperacionanuladaServicios As IFacOperacionAnuladaServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        Private _FacFactuDetaAnuladaServicios As IFacFactuDetaAnuladaServicios
        Private _facoperaciondetalleServicios As IFacOperacionDetalleServicios
        Private _FacOperacionDetaProformasServicios As IFacOperacionDetaProformaServicios
        Private _FacMotivosservicios As IMotivoServicios
        ' Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        Dim xoperacion As String
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarFacFacturaAnulada)
            Try
                Me._ventana = ventana
                'Me._ventana.FacFacturaAnulada = New FacFacturaAnulada()
                Me._FacFacturaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaServicios")), IFacFacturaAnuladaServicios)
                Me._FacAnuladaFisicaServicios = DirectCast(Activator.GetObject(GetType(IFacAnuladaFisicaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacAnuladaFisicaServicios")), IFacAnuladaFisicaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacCobroFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacCobroFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCobroFacturaServicios")), IFacCobroFacturaServicios)
                Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)
                Me._FacInternacionalAnuladasServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalAnuladaServicios")), IFacInternacionalAnuladaServicios)
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._FacContadorServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                Me._facoperacionanuladaServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionAnuladaServicios")), IFacOperacionAnuladaServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                Me._FacFactuDetaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaAnuladaServicios")), IFacFactuDetaAnuladaServicios)
                Me._facoperaciondetalleServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetalleServicios")), IFacOperacionDetalleServicios)
                Me._FacOperacionDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaProformaServicios")), IFacOperacionDetaProformaServicios)
                Me._FacMotivosservicios = DirectCast(Activator.GetObject(GetType(IMotivoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MotivoServicios")), IMotivoServicios)
                ' Me._FacFacturaAnuladaFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaFacturaServicios")), IFacFacturaAnuladaFacturaServicios)

                Dim FacFacturaAnulada As New FacFacturaAnulada()
                Me._ventana.FacFacturaAnulada = FacFacturaAnulada

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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarFacFacturaAnulada, Recursos.Ids.AgregarUsuario)
                Me._ventana.MensajeErrorCobro = ""

                Dim FacFacturaAnulada As New FacFacturaAnulada()
                'FacFacturaAnulada.FechaCobro = Date.Now
                Me._ventana.FacFacturaAnulada = FacFacturaAnulada

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                Dim motivos As IList(Of FacMotivo) = Me._FacMotivosservicios.ConsultarTodos()
                Dim primermotivos As New FacMotivo()
                primermotivos.Id = Integer.MinValue
                motivos.Insert(0, primermotivos)
                Me._ventana.Motivos = motivos
                Me._ventana.Motivos2 = motivos

                Me._ventana.FocoPredeterminado()
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

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New AgregarFacFacturaAnulada())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                'variables
                'Dim lista_d, w_lis_dat As String
                Dim nActual, w_fac, w_fac_o As Integer
                'Dim w_old_fecha As DateTime
                'Dim w_old_mpago As String
                'Dim w_old_desc_pago As String
                'Dim w_old_cbanco As FacBanco
                Dim w_asocia As Asociado
                'variables

                If Me._ventana.BDesg = False Then
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Debe Seleccionar el Chek de anulación", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

                If Me._ventana.Factura = "0" Then

                    If MessageBoxResult.Yes = MessageBox.Show("Anulacion de Factura Fisica ?", "Anulacion", MessageBoxButton.YesNo, MessageBoxImage.Question) Then

                        If Me._ventana.Control = "" Or Me._ventana.Control = Nothing Then
                            Mouse.OverrideCursor = Nothing
                            Me._ventana.MensajeErrorCobro = "Debe Cargar el Control "
                            Exit Sub
                        End If

                        Dim contador As Integer = numero_contador("FAC_FACTURAS_ANUL")
                        Me._ventana.Secuencia = contador
                        nActual = contador

                        '                                clear/e "FAC_ANUFISICA"
                        '                    creocc("FAC_ANUFISICA", -1)
                        '            FANULA.FAC_ANUFISICA   = $date         
                        '                    SECANULA.FAC_ANUFISICA = nActual
                        '                    Control.FAC_ANUFISICA = xcontrol.dumm
                        ';
                        '                    Call gp_graba("FAC_ANUFISICA", "S")
                        ';
                        '            message/nobeep "Anulación Satisfactoria"
                        ';
                        '            message/error "Anulación Realizada"
                        ';
                        ';           Impresion
                        ';
                        '                    Activate("fac_repfac_anfi".exec(nActual, "X"))

                        'agregar factura fisica
                        Dim facturaanuladafisica As New FacFacturaAnulada
                        agregar_anulacion_fisica(nActual, facturaanuladafisica)

                        MessageBox.Show("Anulación Satisfactoria", "Anulacion", MessageBoxButton.OK)

                        Mouse.OverrideCursor = Nothing

                        IrConsultarFacFacturaAnuladaReporte(Nothing, facturaanuladafisica)

                    End If
                    'Exit sub 'Verificar es return(0)
                Else

                    Dim facoperacionaux As New FacOperacion
                    Dim facoperaciones As List(Of FacOperacion)

                    Dim factura As FacFactura = consultar_factura(Me._ventana.Factura)
                    If factura IsNot Nothing Then
                        If factura.Proforma IsNot Nothing Then
                            factura.Proforma = buscar_facfacturaproforma(factura.Proforma.Id)
                        End If
                    End If
                    Dim facinternacional As FacInternacional = Nothing

                    facoperacionaux.Id = "ND"
                    facoperacionaux.CodigoOperacion = Me._ventana.Factura
                    facoperaciones = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(facoperacionaux)
                    If facoperaciones.Count > 0 Then
                        If facoperaciones(0).Saldo = 0 Then
                            Mouse.OverrideCursor = Nothing
                            MessageBox.Show("La factura No. " & Me._ventana.Factura & " Ya fue Cobrada no se Puede Anular", "Error", MessageBoxButton.OK)
                            Me._ventana.MensajeErrorCobro = "La factura No. " & Me._ventana.Factura & " Ya fue Cobrada no se Puede Anular"
                            Exit Sub
                        End If
                    End If

                    If factura.Proforma IsNot Nothing Then
                        If factura.Proforma.Local = "I" Then 'para proformas internacionales

                            facinternacional = buscar_facinternacional(factura.Proforma.Id)
                            If facinternacional IsNot Nothing Then


                                'para transferir proforma 
                                If Me._ventana.cpro = "" Or Me._ventana.cpro = Nothing Then
                                    Mouse.OverrideCursor = Nothing
                                    MessageBox.Show("Debe seleccionar una proforma a transferir", "Error", MessageBoxButton.OK)
                                    Me._ventana.MensajeErrorCobro = "Debe seleccionar una proforma a transferir"
                                    Exit Sub
                                End If
                                Dim facproformastran = buscar_facfacturaproforma(Me._ventana.cpro)
                                'fin verificar la proforma a transferir
                                If facproformastran Is Nothing Then
                                    Mouse.OverrideCursor = Nothing
                                    MessageBox.Show("No existe Proforma No. " & (Me._ventana.cpro), "Error", MessageBoxButton.OK)
                                    Me._ventana.MensajeErrorCobro = "No existe Proforma No. " & (Me._ventana.cpro)
                                    Exit Sub
                                Else
                                    If facproformastran.Local <> "I" Then
                                        Mouse.OverrideCursor = Nothing
                                        MessageBox.Show("Proforma no es internacional", "Error", MessageBoxButton.OK)
                                        Me._ventana.MensajeErrorCobro = "Proforma no es internacional"
                                        Exit Sub
                                    Else
                                        w_asocia = facproformastran.Asociado
                                    End If
                                End If

                                'este es nuevo lo agregue yo carlos 
                                Dim facinternacional_proforma_nueva As FacInternacional = Nothing
                                facinternacional_proforma_nueva = buscar_facinternacional(Me._ventana.cpro)
                                If facinternacional_proforma_nueva Is Nothing Then
                                    If MessageBoxResult.Yes = MessageBox.Show("La Proforma Nueva no tiene Cxp Inernacional Desea continuar?", "Anulacion Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question) Then

                                    Else
                                        Mouse.OverrideCursor = Nothing
                                        Exit Sub
                                    End If
                                End If
                                'If facinternacional_proforma_nueva IsNot Nothing Then
                                '    Mouse.OverrideCursor = Nothing
                                '    MessageBox.Show("Proforma a transferir ya esta registrada en una Cxp internacional", "Error", MessageBoxButton.OK)
                                '    Me._ventana.MensajeErrorCobro = "Proforma a transferir ya esta registrada en una Cxp internacional"
                                '    Exit Sub
                                'End If

                                'If facinternacional.FechaPago IsNot Nothing Then
                                '    w_old_fecha = facinternacional.FechaPago 'fpago.lv_fac_cxp_int
                                'End If
                                'w_old_mpago = facinternacional.TipoPago 'mpago.lv_fac_cxp_int
                                'w_old_desc_pago = facinternacional.DescripcionPago 'desc_pago.lv_fac_cxp_int
                                'If facinternacional.Banco IsNot Nothing Then
                                '    w_old_cbanco = facinternacional.Banco  'cbanco.lv_fac_cxp_int
                                'End If

                                'pasar de internacional a internacional anulada
                                'fin pasar de internacional a internacional anulada
                                Dim facinternacionalAnulada As New FacInternacionalAnulada
                                facinternacionalAnulada.Id = facinternacional.Id
                                facinternacionalAnulada.Asociado = facinternacional.Asociado
                                facinternacionalAnulada.Asociado_o = facinternacional.Asociado_o
                                facinternacionalAnulada.Numerofactura = facinternacional.Numerofactura
                                facinternacionalAnulada.Monto = facinternacional.Monto
                                facinternacionalAnulada.Fecha = facinternacional.Fecha
                                facinternacionalAnulada.Pais = facinternacional.Pais
                                facinternacionalAnulada.Detalle = facinternacional.Detalle
                                facinternacionalAnulada.FechaPago = facinternacional.FechaPago
                                facinternacionalAnulada.TipoPago = facinternacional.TipoPago
                                facinternacionalAnulada.DescripcionPago = facinternacional.DescripcionPago
                                facinternacionalAnulada.Banco = facinternacional.Banco
                                facinternacionalAnulada.Factura = facinternacional.Factura
                                facinternacionalAnulada.FechaAnulacion = facinternacional.FechaRecepcion

                                If _FacInternacionalAnuladasServicios.InsertarOModificar(facinternacionalAnulada, UsuarioLogeado.Hash) = True Then  'agregar a internacional anulada

                                    ' si la proforma nueva tiene cxp internacional 
                                    If facinternacional_proforma_nueva IsNot Nothing Then
                                        If facinternacional.FechaPago IsNot Nothing Then
                                            facinternacional_proforma_nueva.FechaPago = facinternacional.FechaPago
                                            'If facinternacional.TipoPago IsNot Nothing Then
                                            facinternacional_proforma_nueva.TipoPago = facinternacional.TipoPago
                                            'End If
                                            If facinternacional.Asociado IsNot Nothing Then
                                                If facinternacional.Asociado.Id > Integer.MinValue Then
                                                    facinternacional_proforma_nueva.Asociado = facinternacional.Asociado
                                                End If
                                            End If
                                            If facinternacional.DescripcionPago IsNot Nothing And facinternacional.DescripcionPago <> "" Then
                                                facinternacional_proforma_nueva.DescripcionPago = facinternacional.DescripcionPago
                                            End If
                                            If facinternacional.Banco IsNot Nothing Then
                                                If facinternacional.Banco.Id > Integer.MinValue Then
                                                    facinternacional_proforma_nueva.Asociado = facinternacional.Asociado
                                                End If
                                            End If
                                        End If
                                    End If

                                    _FacInternacionalesServicios.Eliminar(facinternacional, UsuarioLogeado.Hash) 'elimina de internacional¡

                                    'esto lo agregue yo para guardar la cxp internacional con la proforma nueva antes de eliminar
                                    'facinternacional_proforma_nueva = facinternacional
                                    'facinternacional_proforma_nueva.Id = Me._ventana.cpro
                                    'If _FacInternacionalesServicios.InsertarOModificar(facinternacional_proforma_nueva, UsuarioLogeado.Hash) = True Then
                                    '    'hasta aqui esto lo agregue yo para guardar la cxp internacional con la proforma nueva antes de eliminar

                                    '    _FacInternacionalesServicios.Eliminar(facinternacional, UsuarioLogeado.Hash) 'elimina de internacional¡
                                    '    Dim facinternacionaltra As FacInternacional = buscar_facinternacional(Me._ventana.cpro)
                                    'End If


                                    'If facinternacionaltra IsNot Nothing Then
                                    '    Dim internacionaltra As New FacInternacional
                                    '    internacionaltra.FechaPago = w_old_fecha
                                    '    internacionaltra.TipoPago = w_old_mpago
                                    '    internacionaltra.DescripcionPago = w_old_desc_pago
                                    '    internacionaltra.Banco = w_old_cbanco
                                    '    internacionaltra.Asociado = w_asocia
                                    '    _FacInternacionalesServicios.InsertarOModificar(internacionaltra, UsuarioLogeado.Hash) 'elimina de internacional
                                    'End If
                                End If
                            Else
                                Mouse.OverrideCursor = Nothing
                                MessageBox.Show("No existe en CXP Internacional No " & factura.Proforma.Id, "Error", MessageBoxButton.OK)
                                Me._ventana.MensajeErrorCobro = "No existe en CXP Internacional No " & factura.Proforma.Id
                                Exit Sub
                            End If
                        End If 'fin para proformas internacionales 
                    End If 'Proceso 1/4 Culminado


                    If factura.Terrero <> "3" Then
                        If Me._ventana.Control = "" Or Me._ventana.Control = Nothing Then
                            Mouse.OverrideCursor = Nothing
                            Me._ventana.MensajeErrorCobro = "Debe Cargar el Control "
                            Exit Sub
                        End If
                    End If

                    If factura.Proforma IsNot Nothing Then
                        w_fac = factura.Proforma.Id
                    End If
                    w_fac_o = factura.Id

                    Dim facturaanuladabuscar As FacFacturaAnulada = consultar_factura_anulada(w_fac_o)
                    If facturaanuladabuscar IsNot Nothing Then
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Ya esta Anulada esta factura", "Error", MessageBoxButton.OK)
                        Me._ventana.MensajeErrorCobro = "Ya esta Anulada esta factura"
                        Exit Sub
                    End If

                    Dim facoperacionanulada As FacOperacionAnulada = consultar_operaciones_anuladas(w_fac_o)
                    If facoperacionanulada IsNot Nothing Then
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Ya esta Anulada esta factura", "Error", MessageBoxButton.OK)
                        Me._ventana.MensajeErrorCobro = "Ya esta Anulada esta factura"
                        Exit Sub
                    End If

                    If Me._ventana.Motivo Is Nothing Or Me._ventana.Motivo.id = Integer.MinValue Then
                        Mouse.OverrideCursor = Nothing
                        Me._ventana.MensajeErrorCobro = "Debe Cargar el Motivo"
                        Exit Sub
                    End If

                    Me._ventana.Secuencia = 0
                    Me._ventana.Secuencia2 = 0

                    If factura.Terrero = "3" Then
                        Dim contador As Integer = numero_contador("FAC_STATEMENT_ANUL")
                        Me._ventana.Secuencia2 = contador
                    End If

                    If factura.Terrero = "1" Then
                        Dim contador As Integer = numero_contador("FAC_FACTURAS_ANUL")
                        Me._ventana.Secuencia = contador
                    End If

                    If factura.Terrero = "2" Then
                        Dim contador As Integer = numero_contador("FAC_FACTURAS_ANUL")
                        Me._ventana.Secuencia = contador

                        contador = numero_contador("FAC_STATEMENT_ANUL")
                        Me._ventana.Secuencia2 = contador
                    End If

                    'crear factura anulada
                    Dim facturaanulada As FacFacturaAnulada = Crear_FacAnulada(factura)

                    'crear detalle factura anulada
                    Dim detalleanulada As IList(Of FacFactuDetaAnulada) = Crear_FacAnuladaDetalle(factura, facturaanulada)

                    'crear operacion anulada
                    Dim operacionanulada As List(Of FacOperacionAnulada) = Crea_OperacionAnulada(factura)


                    ''actualiza factura a estatus de anulada
                    factura.Anulada = "SI"
                    'factura.Asociado = BuscarAsociado_cero
                    factura.Impuesto = 0
                    factura.Descuento = 0
                    'factura.AsociadoImp = Nothing
                    factura.InteresadoImp = Nothing
                    factura.Caso = "."
                    _FacFacturaServicios.InsertarOModificar(factura, UsuarioLogeado.Hash)

                    ''eliminar facoperacion
                    Eliminar_Operacion(factura)

                    ''eliminar facdetalle
                    If detalleanulada Is Nothing Then
                        If detalleanulada.Count > 0 Then
                            Eliminar_FacDetalle(factura)
                        End If
                    End If

                    ''eliminar operacion detalle
                    Eliminar_Operacion_Detalle(factura)

                    ''anular proforma
                    If factura.Proforma IsNot Nothing Then
                        factura.Proforma.Anulada = "SI"
                        factura.Proforma.XCausaRec = Me._ventana.Detalle
                        _FacFacturaProformaServicios.InsertarOModificar(factura.Proforma, UsuarioLogeado.Hash)
                        ''eliminar operacion detalle proforma
                        Eliminar_Operacion_Detalle_Proforma(factura.Proforma)
                    End If

                    MessageBox.Show("Anulación Satisfactoria", "Anulacion", MessageBoxButton.OK)

                    'faltan los reportes y la tabla factuar fisica
                    Mouse.OverrideCursor = Nothing
                    IrConsultarFacFacturaAnuladaReporte(factura, Nothing)
                    'If facturaanulada.Terrero = "1" Then

                    'End If

                    'If facturaanulada.Terrero = "2" Then

                    'End If

                    'If facturaanulada.Terrero = "2" Then

                    'End If
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
            Mouse.OverrideCursor = Nothing
        End Sub

        Public Sub irproforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            Dim facproformastran = buscar_facfacturaproforma(Me._ventana.cpro)
            'fin verificar la proforma a transferir
            If facproformastran Is Nothing Then
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("No existe Proforma No. " & (Me._ventana.cpro), "Error", MessageBoxButton.OK)
                Me._ventana.MensajeErrorCobro = "No existe Proforma No. " & (Me._ventana.cpro)
                Exit Sub
            End If
            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas.ConsultarFacFacturaProforma(facproformastran))

            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Function existe_detalle_anulada(ByVal facfacturaanul As FacFacturaAnulada) As Boolean
            Dim existe As Boolean = False
            Dim detalleaux As New FacFactuDetaAnulada()
            detalleaux.Factura = facfacturaanul
            Dim facfacturaanuldetalle As List(Of FacFactuDetaAnulada)
            facfacturaanuldetalle = _FacFactuDetaAnuladaServicios.ObtenerFacFactuDetaAnuladasFiltro(detalleaux)
            If facfacturaanuldetalle IsNot Nothing Then
                If facfacturaanuldetalle.Count > 0 Then
                    existe = True
                End If
            End If
            Return existe
        End Function

        Public Sub IrConsultarFacFacturaAnuladaReporte(ByVal factura As FacFactura, ByVal facfacturaanulada As FacFacturaAnulada)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New FacturaAnuladaRpt(factura, facfacturaanulada))
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub agregar_anulacion_fisica(ByVal nActual As Integer, ByRef facfacturaanulada As FacFacturaAnulada)
            Dim facanuladafisica As New FacAnuladaFisica
            facanuladafisica.Id = nActual
            facanuladafisica.FechaAnulacion = FormatDateTime(Date.Now, DateFormat.ShortDate)
            facanuladafisica.Control = Me._ventana.Control
            _FacAnuladaFisicaServicios.InsertarOModificar(facanuladafisica, UsuarioLogeado.Hash)

            facfacturaanulada.Id = 0
            facfacturaanulada.Secanula = nActual
            facfacturaanulada.FechaAnulacion = FormatDateTime(Date.Now, DateFormat.ShortDate)
            facfacturaanulada.Control = Me._ventana.Control
            facfacturaanulada.Xter = 10
        End Sub

        Public Function Crear_FacAnulada(ByVal factura As FacFactura) As FacFacturaAnulada           
                Dim facfacturaanulada As New FacFacturaAnulada
            Try
                facfacturaanulada.Id = factura.Id
                facfacturaanulada.FechaFactura = factura.FechaFactura
                facfacturaanulada.Asociado = factura.Asociado
                facfacturaanulada.Idioma = factura.Idioma
                facfacturaanulada.Moneda = factura.Moneda
                facfacturaanulada.Caso = factura.Caso
                facfacturaanulada.Inicial = factura.Inicial
                facfacturaanulada.Impuesto = factura.Impuesto
                facfacturaanulada.Descuento = factura.Descuento
                facfacturaanulada.AsociadoImp = factura.AsociadoImp
                facfacturaanulada.InteresadoImp = factura.InteresadoImp
                facfacturaanulada.Terrero = factura.Terrero
                facfacturaanulada.Email = factura.Email
                facfacturaanulada.Seniat = factura.Seniat
                facfacturaanulada.FechaSeniat = factura.FechaSeniat
                facfacturaanulada.PSeniat = factura.PSeniat
                facfacturaanulada.IP = factura.IP
                facfacturaanulada.XAsociado = factura.XAsociado
                facfacturaanulada.Rif = factura.Rif
                facfacturaanulada.XNit = factura.XNit
                'facfacturaanulada. = factura.DetalleEnvio
                'facfacturaanulada.IMulmon = factura.IMulmon
                'facfacturaanulada.MonedaImp = factura.MonedaImp
                'facfacturaanulada.TasaCambio = factura.TasaCambio
                'facfacturaanulada.Codeti = factura.Codeti
                'facfacturaanulada.NumeroControl = factura.NumeroControl
                'facfacturaanulada.Local = factura.Local
                ' facfacturaanulada.Proforma = factura.Proforma
                ' facfacturaanulada.Ourref = factura.Ourref
                'facfacturaanulada.Instruc = factura.Instruc
                'facfacturaanulada.Carta = factura.Carta
                'facfacturaanulada.CodigoDepartamento = factura.CodigoDepartamento
                'facfacturaanulada.CodGuia = factura.CodGuia
                'facfacturaanulada.CodigoSocio = factura.CodigoSocio
                'facfacturaanulada.MSocio = factura.MSocio
                'facfacturaanulada.MCia = factura.MCia
                'facfacturaanulada.CondFac = factura.CondFac
                facfacturaanulada.MSubtimpo = factura.MSubtimpo
                facfacturaanulada.MDescuento = factura.MDescuento
                facfacturaanulada.MTbimp = factura.MTbimp
                facfacturaanulada.Mtbexc = factura.Mtbexc
                facfacturaanulada.MSubtotal = factura.MSubtotal
                facfacturaanulada.Mtimp = factura.Mtimp
                facfacturaanulada.Mttotal = factura.Mttotal
                facfacturaanulada.MSubtimpoBf = factura.MSubtimpoBf
                facfacturaanulada.MDescuentoBf = factura.MDescuentoBf
                facfacturaanulada.MTbimpBf = factura.MTbimpBf
                facfacturaanulada.MTbexcBf = factura.MTbexcBf
                facfacturaanulada.MSubtotalBf = factura.MSubtotalBf
                facfacturaanulada.MTimpBf = factura.MTimpBf
                facfacturaanulada.MTtotalBf = factura.MTtotalBf
                facfacturaanulada.XAsociado_O = factura.XAsociado_O
                'facfacturaanulada.Status = factura.Status
                If Me._ventana.Motivo IsNot Nothing Then
                    If DirectCast(Me._ventana.Motivo, FacMotivo).Id <> Integer.MinValue Then
                        facfacturaanulada.Motivo = Me._ventana.Motivo
                    End If
                End If
                If IsNumeric(Me._ventana.Control) Then
                    facfacturaanulada.Control = Me._ventana.Control
                End If
                facfacturaanulada.Detalle = Me._ventana.Detalle
                facfacturaanulada.Anulada = "SI"

                If factura.Terrero = "1" Then
                    If IsNumeric(Me._ventana.Secuencia) Then
                        facfacturaanulada.Secanula = Me._ventana.Secuencia
                    End If
                End If
                If factura.Terrero = "2" Then
                    If IsNumeric(Me._ventana.Secuencia) Then
                        facfacturaanulada.Secanula = Me._ventana.Secuencia
                    End If
                    If IsNumeric(Me._ventana.Secuencia2) Then
                        facfacturaanulada.Secanula2 = Me._ventana.Secuencia2
                    End If
                    If Me._ventana.Motivo2 IsNot Nothing Then
                        If DirectCast(Me._ventana.Motivo2, FacMotivo).Id <> Integer.MinValue Then
                            facfacturaanulada.Motivo2 = Me._ventana.Motivo2
                        End If
                    End If
                    facfacturaanulada.Detalle2 = Me._ventana.Detalle2
                End If
                If factura.Terrero = "3" Then
                    If IsNumeric(Me._ventana.Secuencia2) Then
                        facfacturaanulada.Secanula2 = Me._ventana.Secuencia2
                    End If
                End If
                facfacturaanulada.FechaAnulacion = FormatDateTime(Date.Now, DateFormat.ShortDate)

                Dim exitoso As Boolean = _FacFacturaAnuladaServicios.InsertarOModificar(facfacturaanulada, UsuarioLogeado.Hash)

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

            Return (facfacturaanulada)
        End Function

        Public Function Crear_FacAnuladaDetalle(ByVal factura As FacFactura, ByVal facfacturaanulada As FacFacturaAnulada) As IList(Of FacFactuDetaAnulada)

            ''--> para pasar de detalleproforma a detalle
            Dim detallaux As New FacFactuDetalle
            Dim detallaanulada As New List(Of FacFactuDetaAnulada)
            Dim detalles As List(Of FacFactuDetalle)
            Dim creo_detalle As Boolean
            Try
                detallaux.Factura = factura
                detalles = Me._FacFactuDetaServicios.ObtenerFacFactuDetallesFiltro(detallaux)

                For i As Integer = 0 To detalles.Count - 1
                    detallaanulada.Add(New FacFactuDetaAnulada)
                    detallaanulada(i).Id = detalles(i).Id
                    detallaanulada(i).Factura = facfacturaanulada
                    detallaanulada(i).BDetalle = detalles(i).BDetalle
                    detallaanulada(i).XDetalle = detalles(i).XDetalle
                    detallaanulada(i).CServicio = detalles(i).CServicio
                    detallaanulada(i).Pendiente = detalles(i).Pendiente
                    detallaanulada(i).Servicio = detalles(i).Servicio
                    detallaanulada(i).NCantidad = detalles(i).NCantidad
                    detallaanulada(i).Pu = detalles(i).Pu
                    detallaanulada(i).Descuento = detalles(i).Descuento
                    detallaanulada(i).Bsel = detalles(i).Bsel
                    'detallaanulada(i).TipoServicio = detalles(i).TipoServicio
                    'detallaanulada(i).Codigo = detalles(i).Codigo
                    'detallaanulada(i).Iimp = detalles(i).Iimp
                    detallaanulada(i).XDetalleEs = detalles(i).XDetalleEs
                    detallaanulada(i).BDetalleEs = detalles(i).BDetalleEs
                    'detallaanulada(i).Tasa = detalles(i).Tasa
                    detallaanulada(i).Impuesto = detalles(i).Impuesto
                    detallaanulada(i).MImpuesto = detalles(i).MImpuesto
                    detallaanulada(i).MDescuento = detalles(i).MDescuento
                    detallaanulada(i).BDetalleBf = detalles(i).BDetalleBf
                    detallaanulada(i).PuBf = detalles(i).PuBf
                    detallaanulada(i).MImpuestoBf = detalles(i).MImpuestoBf
                    detallaanulada(i).MDescuentoBf = detalles(i).MDescuentoBf
                    detallaanulada(i).Desglose = detalles(i).Desglose
                    'facturadetalles(i).Factura = Nothing

                    If _FacFactuDetaAnuladaServicios.InsertarOModificar(detallaanulada(i), UsuarioLogeado.Hash) = True Then 'si se agrego el detalle de anulada con exito eliminar de detalle factura
                        Me._FacFactuDetaServicios.Eliminar(detalles(i), UsuarioLogeado.Hash)
                    End If
                Next

                creo_detalle = existe_detalle_anulada(facfacturaanulada)

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

            If creo_detalle = True Then
                Return (detallaanulada)
            Else
                Return Nothing
            End If
        End Function

        Public Function Crea_OperacionAnulada(ByVal factura As FacFactura) As List(Of FacOperacionAnulada)

            '--> para pasar de operacionproforma a operacion
            Dim operacionaux As New FacOperacion
            operacionaux.CodigoOperacion = factura.Id
            Dim operaciones As List(Of FacOperacion) = _facoperacionesServicios.ObtenerFacOperacionesFiltro(operacionaux)
            Dim operacionAnulada As New List(Of FacOperacionAnulada)
            For i As Integer = 0 To operaciones.Count - 1
                operacionAnulada.Add(New FacOperacionAnulada)
                'operacionAnulada(i) = DirectCast(operaciones(i), FacOperacion)
                operacionAnulada(i).Id = operaciones(i).Id
                operacionAnulada(i).Asociado = operaciones(i).Asociado
                operacionAnulada(i).CodigoOperacion = operaciones(i).CodigoOperacion
                operacionAnulada(i).FechaOperacion = operaciones(i).FechaOperacion
                operacionAnulada(i).OperacionImp = operaciones(i).OperacionImp
                operacionAnulada(i).Moneda = operaciones(i).Moneda
                operacionAnulada(i).Idioma = operaciones(i).Idioma
                operacionAnulada(i).Monto = operaciones(i).Monto
                operacionAnulada(i).MontoBf = operaciones(i).MontoBf
                operacionAnulada(i).Saldo = operaciones(i).Saldo
                operacionAnulada(i).XOperacion = operaciones(i).XOperacion
                operacionAnulada(i).SaldoBf = operaciones(i).SaldoBf
                '                operacionAnulada(i).CodigoOperacion = Nothing

                _facoperacionanuladaServicios.InsertarOModificar(operacionAnulada(i), UsuarioLogeado.Hash)
            Next
            Return (operacionAnulada)
        End Function

        Public Function Eliminar_Operacion(ByVal factura As FacFactura) As Boolean
            Dim operacionaux As New FacOperacion
            operacionaux.CodigoOperacion = factura.Id
            Dim operaciones As List(Of FacOperacion) = _facoperacionesServicios.ObtenerFacOperacionesFiltro(operacionaux)            
            For i As Integer = 0 To operaciones.Count - 1
                _facoperacionesServicios.Eliminar(operaciones(i), UsuarioLogeado.Hash)
            Next
            Return (True)
        End Function

        Public Function Eliminar_Operacion_Detalle(ByVal factura As FacFactura) As Boolean
            Dim operacionaux As New FacOperacionDetalle
            operacionaux.Factura = factura
            Dim operaciones As List(Of FacOperacionDetalle) = _facoperaciondetalleServicios.ObtenerFacOperacionDetallesFiltro(operacionaux)
            For i As Integer = 0 To operaciones.Count - 1
                _facoperaciondetalleServicios.Eliminar(operaciones(i), UsuarioLogeado.Hash)
            Next
            Return (True)
        End Function

        Public Function Eliminar_Operacion_Detalle_Proforma(ByVal factura As FacFacturaProforma) As Boolean
            Dim operacionaux As New FacOperacionDetaProforma
            operacionaux.Factura = factura
            Dim operaciones As List(Of FacOperacionDetaProforma) = _FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(operacionaux)
            For i As Integer = 0 To operaciones.Count - 1
                _FacOperacionDetaProformasServicios.Eliminar(operaciones(i), UsuarioLogeado.Hash)
            Next
            Return (True)
        End Function

        Public Function Eliminar_FacDetalle(ByVal factura As FacFactura) As Boolean

            Dim detallaux As New FacFactuDetalle
            Dim detalles As List(Of FacFactuDetalle)
            detallaux.Factura = factura
            detalles = Me._FacFactuDetaServicios.ObtenerFacFactuDetallesFiltro(detallaux)
            If detalles.Count > 0 Then
                For i As Integer = 0 To detalles.Count - 1
                    Me._FacFactuDetaServicios.Eliminar(detalles(i), UsuarioLogeado.Hash)
                Next
            End If
            Return (True)
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

        Public Function buscar_facfacturaproforma(ByVal id As Integer) As FacFacturaProforma
            Dim facfacturaproformaaux As New FacFacturaProforma
            facfacturaproformaaux.Id = id
            Dim facfacturaproformas As List(Of FacFacturaProforma) = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(facfacturaproformaaux)
            If facfacturaproformas IsNot Nothing Then
                If facfacturaproformas.Count > 0 Then
                    Return (facfacturaproformas(0))
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function

        Public Function consultar_factura(ByVal id As Integer) As FacFactura
            Dim FacFacturaAuxiliar As New FacFactura()
            Dim FacFacturas As List(Of FacFactura)
            FacFacturaAuxiliar.Id = id

            FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)
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

        Public Sub agregar_asociado_factura(ByVal factura As FacFactura)
            Dim asociadoaux As New Asociado
            Dim asociado As List(Of Asociado)
            If factura.Asociado IsNot Nothing Then
                asociadoaux.Id = factura.Asociado.Id
                asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                Me._ventana.Asociados = asociado
                Me._ventana.Asociado = asociado(0)
                Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
            End If
        End Sub

        Public Sub BuscarFactura()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
                Mouse.OverrideCursor = Cursors.Wait
                'Dim filtroValido As Boolean = False
                'Dim filtroValido As Integer = 0
                'Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                'dos filtros sean utilizados
                If Me._ventana.Factura <> "0" Then
                    Dim FacFacturaAuxiliar As New FacFactura()
                    Dim FacCobroFacturaAuxiliar As New FacCobroFactura
                    Dim FacCobroFacturas As List(Of FacCobroFactura)
                    'Dim FacFacturas As FacFactura = DirectCast(_ventana.FacFacturaFiltrar, FacFactura)

                    If Not Me._ventana.Factura.Equals("") Then
                        FacFacturaAuxiliar.Id = Integer.Parse(Me._ventana.Factura)
                    End If

                    'If Not Me._ventana.CreditoSent.Equals("") Then
                    '    FacFacturaAuxiliar.CreditoSent = Integer.Parse(Me._ventana.CreditoSent)
                    'End If

                    If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                        FacFacturaAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                    End If

                    'If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                    '    'FacFacturaAuxiliar.Banco = DirectCast(Me._ventana.Banco, FacBanco)
                    'End If

                    'If (Me._ventana.Idioma IsNot Nothing) AndAlso (DirectCast(Me._ventana.Idioma, Idioma).Id <> "") Then
                    '    FacFacturaAuxiliar.Idioma = DirectCast(Me._ventana.Idioma, Idioma)
                    'End If

                    'If (Me._ventana.Moneda IsNot Nothing) AndAlso (DirectCast(Me._ventana.Moneda, Moneda).Id <> "") Then
                    '    FacFacturaAuxiliar.Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                    'End If

                    'If Not Me._ventana.FechaFactura.Equals("") Then
                    '    Dim FechaFacFactura As DateTime = DateTime.Parse(Me._ventana.FechaFactura)
                    '    FacFacturaAuxiliar.FechaFactura = FechaFacFactura
                    'End If

                    'If (filtroValido = True) Then
                    'FacFacturaAuxiliar.Inicial = UsuarioLogeado.Iniciales
                    Dim FacFacturas As IList(Of FacFactura)
                    FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)
                    If FacFacturas.Count > 0 Then
                        If FacFacturas(0).Anulada <> "SI" Then

                            If FacFacturas(0).Terrero = "2" Then
                                Me._ventana.ActivaDesactiva = False
                            Else
                                Me._ventana.ActivaDesactiva = True
                            End If
                            FacCobroFacturaAuxiliar.Factura = FacFacturas(0).Id
                            FacCobroFacturas = _FacCobroFacturaServicios.ObtenerFacCobroFacturasFiltro(FacCobroFacturaAuxiliar)
                            If FacCobroFacturas.Count > 0 Then
                                Mouse.OverrideCursor = Nothing
                                MessageBox.Show("La factura No. " & Me._ventana.Factura & " no puede ser anulada porque se ha comenzado a pagar", "Error", MessageBoxButton.OK)
                                Me._ventana.MensajeErrorCobro = "La factura No. " & Me._ventana.Factura & " no puede ser anulada porque se ha comenzado a pagar"
                                Exit Sub
                            End If
                            ' if ($empty(fac_cobroxfactura))
                            'field_video $fieldname,"col=0"
                            '$prompt = IANULACION.FAC_FACTURAS
                            ' Else
                            ' message("La factura %%CFACTURA no puede ser anulada porque se ha comenzado a pagar")
                            ' Return (-1)
                            'End If

                        Else
                            Mouse.OverrideCursor = Nothing
                            MessageBox.Show("La factura No. " & Me._ventana.Factura & "  ya ha sido anulada", "Error", MessageBoxButton.OK)
                            Me._ventana.MensajeErrorCobro = "La factura No. " & Me._ventana.Factura & "  ya ha sido anulada"
                            Exit Sub
                        End If

                        Me._ventana.SetLocalidad = Me.BuscarLocalidad(FacFacturas(0).Local)
                        agregar_asociado_factura(FacFacturas(0))
                    Else
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("La factura # " & Me._ventana.Factura & " no existe", "Error", MessageBoxButton.OK)
                        Me._ventana.MensajeErrorCobro = "La factura # " & Me._ventana.Factura & " no existe"
                        Exit Sub
                    End If
                End If
                'Me._ventana.Resultados = Nothing
                'Me._ventana.Resultados = FacFacturas
                'sumar(FacFacturas)
                'Else
                '    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto)
                'End If
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
            Mouse.OverrideCursor = Nothing
        End Sub


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

        Public Function BuscarAsociado_cero() As Asociado            
            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing

            asociadoaux.Id = 0
            asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)

            Return (asociados(0))
        End Function

        Public Sub CambiarAsociado()
            Mouse.OverrideCursor = Cursors.Wait
            Try
                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    If Me._ventana.Asociado.id <> Integer.MinValue Then
                        ' Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                    Else
                        Me._ventana.NombreAsociado = Nothing
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothingo
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Sub VerFacturas()
            Dim FacOperacionAuxiliar As New FacOperacion()
            If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                FacOperacionAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            End If
            FacOperacionAuxiliar.Id = "ND"
            FacOperacionAuxiliar.Saldo = -1
            'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
            Me._FacOperaciones = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar)
            ' Me._ventana.ResultadosFactura2 = Me._FacOperaciones
            Me._ventana.MensajeErrorCobro = ""
        End Sub

    End Class
End Namespace
