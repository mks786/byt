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
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFacturaProformas
    Class PresentadorConsultarFacFacturaProformasAutorizacion
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarFacFacturaProformasAutorizacion
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        'Private _FacFacturaProformas As IList(Of FacFacturaProforma)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios
        Private _FacFactuDetaProformasServicios As IFacFactuDetaProformaServicios
        Private _FacOperacionProformasServicios As IFacOperacionProformaServicios
        Private _FacOperacionDetaProformasServicios As IFacOperacionDetaProformaServicios
        Private _tasasServicios As ITasaServicios
        Private _FacInternacionalesServicios As IFacInternacionalServicios


        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarFacFacturaProformasAutorizacion)
            Try
                Me._ventana = ventana
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._FacFactuDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                Me._FacOperacionProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                Me._FacOperacionDetaProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionDetaProformaServicios")), IFacOperacionDetaProformaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturaProformasAutorizacion, Recursos.Ids.fac_ConsultarFacFacturaProforma)
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

                'Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ConsultarTodos()
                'Consultar()

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                'Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ConsultarTodos()
                'Dim primerafacbanco As New FacBanco()
                'primerafacbanco.Id = Integer.MinValue
                'facbancos.Insert(0, primerafacbanco)
                'Me._ventana.Bancos = facbancos

                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim primeridioma As New Idioma
                'primeridioma.Id = ""
                'idiomas.Insert(0, primeridioma)
                'Me._ventana.Idiomas = idiomas

                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Dim primeramoneda As New Moneda
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


        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacFacturaProformasAutorizacion())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub
        Public Sub cargar()
            Dim FacFacturaProformaAuxiliar As New FacFacturaProforma
            'FacFacturaProformaAuxiliar.Inicial = UsuarioLogeado.Iniciales
            FacFacturaProformaAuxiliar.CodigoDepartamento = UsuarioLogeado.Departamento.Id
            FacFacturaProformaAuxiliar.Status = 1 ' esto es para el campo auto porque es in

            Dim FacFacturaProformas As IList(Of FacFacturaProforma)
            FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)

            Me._ventana.Resultados = Nothing
            Me._ventana.Resultados = FacFacturaProformas
            Me._ventana.FacFacturaProformaFiltrar = New FacFacturaProforma
            sumar(FacFacturaProformas)
        End Sub


        Public Sub seleccionar(ByVal value As Boolean)

            Try
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If

                Dim proformas As List(Of FacFacturaProforma) = Me._ventana.Resultados
                For i As Integer = 1 To proformas.Count - 1
                    proformas(i).Seleccion = value
                Next

                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = proformas

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

        Public Sub sumar(ByVal proforma As IList(Of FacFacturaProforma))
            Dim cbp, cbr, cdp, cdr As Integer
            cbp = 0
            'cba = 0
            'cbf = 0
            cbr = 0
            cdp = 0
            'cda = 0
            ' cdf = 0
            cdr = 0
            For i As Integer = 0 To proforma.Count - 1
                If proforma(i).Moneda.Id = "BF" Then
                    If proforma(i).Auto = "0" Then
                        cbp = cbp + 1
                    End If
                    'If proforma(i).Auto = "1" Then
                    '    cba = cba + 1
                    'End If
                    'If proforma(i).Auto = "2" Then
                    '    cbf = cbf + 1
                    'End If
                    If proforma(i).Auto = "3" Then
                        cbr = cbr + 1
                    End If
                Else
                    'If proforma(i).Moneda.Id = "US" Then
                    If proforma(i).Auto = "0" Then
                        cdp = cdp + 1
                    End If
                    'If proforma(i).Auto = "1" Then
                    '    cda = cda + 1
                    'End If
                    'If proforma(i).Auto = "2" Then
                    '    cdf = cdf + 1
                    'End If
                    If proforma(i).Auto = "3" Then
                        cdr = cdr + 1
                    End If
                    'End If
                End If
            Next
            Me._ventana.Cbp = cbp
            'Me._ventana.Cba = cba
            'Me._ventana.Cbf = cbf
            Me._ventana.Cbr = cbr
            Me._ventana.Cdp = cdp
            'Me._ventana.Cda = cda
            'Me._ventana.Cdf = cdf
            Me._ventana.Cdr = cdr
        End Sub

        Public Sub autorizacion()
            'If UsuarioLogeado.BAutorizar = True Then
            Mouse.OverrideCursor = Cursors.Wait
            Dim w_ret, w_pas As Integer
            Dim proforma As List(Of FacFacturaProforma) = Me._ventana.Resultados
            For i As Integer = 0 To proforma.Count - 1
                If proforma(i).Seleccion = True Then
                    w_pas = 0
                    lp_verifica(proforma(i), w_ret)
                    If (w_ret = 1) Then
                        MessageBox.Show("Proforma No " & proforma(i).Id & ", posee un error de integrida, Notificar a sistemas, se suspende esta autorizacion")
                        w_pas = 1
                    End If
                    If (w_ret = 2) Then
                        MessageBox.Show("Proforma No " & proforma(i).Id & ", posee un error de integrida con el calculo de Bolivar Fuerte, Notificar a sistemas, se suspende esta autorizacion")
                        w_pas = 1
                    End If
                    If proforma(i).Local = "I" And w_pas = 0 Then  '; Para los casos internacionales deben existir cuentas por pagar
                        '; Para los casos internacionales deben existir cuentas por pagar
                        Dim facinternacionalaux As New FacInternacional
                        facinternacionalaux.Id = proforma(i).Id
                        Dim facinternacionales As List(Of FacInternacional) = Me._FacInternacionalesServicios.ObtenerFacInternacionalesFiltro(facinternacionalaux)
                        If facinternacionales.Count <= 0 Then
                            MessageBox.Show("Proforma No " & proforma(i).Id & " No Posee Registro de Cuentas x Pagar, se suspende esta autorizacion")
                            w_pas = 1
                        End If

                    End If
                    If w_pas = 0 Then
                        proforma(i).Auto = "1"
                        proforma(i).FechaEcuota = FormatDateTime(Date.Now, DateFormat.ShortDate)
                        Dim exitoso As Boolean = _FacFacturaProformaServicios.InsertarOModificar(proforma(i), UsuarioLogeado.Hash)
                    End If
                End If
            Next
            MessageBox.Show("Autorizacion Satisfactoria")
            Consultar()
            Mouse.OverrideCursor = Nothing
            'Else
            'MessageBox.Show("No posee Privilegios para Autorizar")
            'End If
        End Sub
        Public Sub lp_verifica(ByVal proforma As FacFacturaProforma, ByRef r_val As Integer)
            Dim w_calculo_1, w_calculo_2, w_resta As Integer
            r_val = 0

            Dim proformadetalleaux As New FacFactuDetaProforma
            proformadetalleaux.Factura = proforma
            Dim proformadetalle As List(Of FacFactuDetaProforma) = Me._FacFactuDetaProformasServicios.ObtenerFacFactuDetaProformasFiltro(proformadetalleaux)

            If proformadetalle.Count <= 0 Then
                r_val = 1
                Exit Sub
            End If

            Dim operacionproformaaux As New FacOperacionProforma
            operacionproformaaux.CodigoOperacion = proforma.Id
            Dim operacionproforma As List(Of FacOperacionProforma) = Me._FacOperacionProformasServicios.ObtenerFacOperacionProformasFiltro(operacionproformaaux)
            If operacionproforma.Count <= 0 Then
                r_val = 1
                Exit Sub
            End If

            Dim operacionproformadetalleaux As New FacOperacionDetaProforma
            Dim operacionproformadetalle As List(Of FacOperacionDetaProforma)
            For i As Integer = 0 To proformadetalle.Count - 1
                If proformadetalle(i).Servicio.Itipo = "M" Or proformadetalle(i).Servicio.Itipo = "P" Then
                    'Me._FacOperacionDetaProformasServicios
                    operacionproformadetalleaux.Factura = proforma
                    operacionproformadetalleaux.Detalle = proformadetalle(i).Id
                    operacionproformadetalle = Me._FacOperacionDetaProformasServicios.ObtenerFacOperacionDetaProformasFiltro(operacionproformadetalleaux)
                    If operacionproformadetalle.Count <= 0 Then
                        r_val = 1
                        Exit Sub
                    Else
                        If proformadetalle.Count <> operacionproformadetalle.Count Then ' verificar si el detalle e con el count o con la i
                            r_val = 1
                            Exit Sub
                        End If
                    End If
                End If
            Next

            Dim tasa As New Tasa()
            tasa.Id = proforma.FechaFactura
            tasa.Moneda = proforma.Moneda.Id
            Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            If tasas.Count > 0 Then

                'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
                w_calculo_1 = proforma.MTbimp * tasas(0).Tasabf
                'w_calculo_1 = w_calculo_1[round,2]
                w_calculo_1 = FormatNumber(w_calculo_1, 2)
                'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
                w_calculo_2 = FormatNumber(proforma.MTbimpBf, 2)
                w_resta = w_calculo_1 - w_calculo_2
                If w_resta > 0.05 Then
                    r_val = 2
                    Exit Sub
                End If

                'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
                w_calculo_1 = proforma.Mtbexc * tasas(0).Tasabf
                'w_calculo_1 = w_calculo_1[round,2]
                w_calculo_1 = FormatNumber(w_calculo_1, 2)
                'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
                w_calculo_2 = FormatNumber(proforma.MTbexcBf, 2)
                w_resta = w_calculo_1 - w_calculo_2
                If w_resta > 0.05 Then
                    r_val = 2
                    Exit Sub
                End If

                For i As Integer = 0 To proformadetalle.Count - 1
                    'w_calculo_1 = (mtbexc.fac_facturas_pro * btasa.fac_tasas)
                    w_calculo_2 = proformadetalle(i).BDetalle * tasas(0).Tasabf
                    'w_calculo_1 = w_calculo_1[round,2]
                    w_calculo_2 = FormatNumber(w_calculo_2, 2)
                    'w_calculo_2 = mtbexc_bf.fac_facturas_pro[round,2]
                    w_calculo_1 = FormatNumber(proformadetalle(i).BDetalleBf, 2)
                    w_resta = w_calculo_1 - w_calculo_2
                    If w_resta > 0.05 Then
                        r_val = 2
                        Exit Sub
                    End If
                Next
            End If
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

                'Dim filtroValido As Boolean = False
                'Dim filtroValido As Integer = 0
                'Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                'dos filtros sean utilizados
                Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                'Dim FacFacturaProformas As FacFacturaProforma = DirectCast(_ventana.FacFacturaProformaFiltrar, FacFacturaProforma)

                If Not Me._ventana.Id.Equals("") Then
                    FacFacturaProformaAuxiliar.Id = Integer.Parse(Me._ventana.Id)
                End If

                'If Not Me._ventana.CreditoSent.Equals("") Then
                '    FacFacturaProformaAuxiliar.CreditoSent = Integer.Parse(Me._ventana.CreditoSent)
                'End If

                If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                    FacFacturaProformaAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                End If

                'If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                '    'FacFacturaProformaAuxiliar.Banco = DirectCast(Me._ventana.Banco, FacBanco)
                'End If

                'If (Me._ventana.Idioma IsNot Nothing) AndAlso (DirectCast(Me._ventana.Idioma, Idioma).Id <> "") Then
                '    FacFacturaProformaAuxiliar.Idioma = DirectCast(Me._ventana.Idioma, Idioma)
                'End If

                'If (Me._ventana.Moneda IsNot Nothing) AndAlso (DirectCast(Me._ventana.Moneda, Moneda).Id <> "") Then
                '    FacFacturaProformaAuxiliar.Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                'End If

                If Not Me._ventana.FechaFactura.Equals("") Then
                    Dim FechaFacFacturaProforma As DateTime = DateTime.Parse(Me._ventana.FechaFactura)
                    FacFacturaProformaAuxiliar.FechaFactura = FechaFacFacturaProforma
                End If

                FacFacturaProformaAuxiliar.CodigoDepartamento = UsuarioLogeado.Departamento.Id
                FacFacturaProformaAuxiliar.Status = 1 ' esto es para el campo auto porque es in
                'If (filtroValido = True) Then
                'FacFacturaProformaAuxiliar.Inicial = UsuarioLogeado.Iniciales
                Dim FacFacturaProformas As IList(Of FacFacturaProforma)
                FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)
                Me._ventana.Resultados = Nothing
                Me._ventana.Count = FacFacturaProformas.Count
                Me._ventana.Resultados = FacFacturaProformas
                sumar(FacFacturaProformas)
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
            Me._ventana.FacFacturaProformaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New ConsultarFacFacturaProforma(Me._ventana.FacFacturaProformaSeleccionado))
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

        Public Sub CambiarAsociado()
            Try
                'Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
                Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub
    End Class
End Namespace