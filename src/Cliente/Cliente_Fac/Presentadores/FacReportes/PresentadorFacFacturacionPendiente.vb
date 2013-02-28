Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
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
    Class PresentadorFacFacturacionPendiente
        Inherits PresentadorBase

        Private _ventana As IFacFacturacionPendiente
        'Private _FacFactura As FacFactura
        Private _FacFacturaPendiente As List(Of FacFacturaPendiente)
        'Dim _FacFacturaDetalle As List(Of FacFactuDetalle)
        Private _asociadosServicios As IAsociadoServicios

        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacFacturaPendienteServicios As IFacFacturaPendienteServicios
        Private _etiquetaServicios As IEtiquetaServicios
        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        Private _FacOperacionServicios As IFacOperacionServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New(ByVal ventana As IFacFacturacionPendiente)
            Try
                Me._ventana = ventana
                Me._FacFacturaPendienteServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaPendienteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaPendienteServicios")), IFacFacturaPendienteServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                Me._etiquetaServicios = DirectCast(Activator.GetObject(GetType(IEtiquetaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("EtiquetaServicios")), IEtiquetaServicios)
                Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacFacturacionPendiente())
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
                ActualizarTitulo()


                Me._ventana.FocoPredeterminado()

                Me._ventana.Fecha1 = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Me._ventana.MayorMenor = "Ma"

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

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemFacFacturacionPendiente, Recursos.Ids.fac_menuItemFacFacturacionPendiente)
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

        Public Sub CambiarAsociado()
            Try
                ' Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    If Me._ventana.Asociado.id <> Integer.MinValue Then
                        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                        Me._ventana.Asociados2 = Me._ventana.Asociados
                        Me._ventana.Asociado2 = Me._ventana.Asociado
                        Me._ventana.NombreAsociado2 = Me._ventana.NombreAsociado
                    End If
                End If
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub BuscarAsociadofin()
            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing
            Dim i As Boolean = False
            'asociadoaux = Nothing
            'Dim asociadosFiltrados As IEnumerable(Of Asociado) = Me._asociados
            Mouse.OverrideCursor = Cursors.Wait

            If Not String.IsNullOrEmpty(Me._ventana.idAsociadoFiltrar2) And Me._ventana.idAsociadoFiltrar2 <> "0" Then
                asociadoaux.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar2)
                '
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreAsociadoFiltrar2) Then
                asociadoaux.Nombre = Me._ventana.NombreAsociadoFiltrar2
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreAsociadoFiltrar.ToLower())
                i = True
            End If

            'Me._asociados = Me._asociadosServicios.ConsultarAsociadoConTodo(asociadoaux)
            If i = True Then
                asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Else
                Me._ventana.Asociados2 = Nothing
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")                
                Exit Sub
            End If

            Dim primerasociado As New Asociado()
            primerasociado.Id = Integer.MinValue
            asociados.Insert(0, primerasociado)

            Mouse.OverrideCursor = Nothing

            Me._ventana.Asociados2 = asociados

            'If asociadosFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Asociados = asociadosFiltrados
            'Else
            '    Me._ventana.Asociados = Me._asociados
            'End If
        End Sub

        Public Sub CambiarAsociadofin()
            Try
                'Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado2, Asociado))
                'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
                If DirectCast(Me._ventana.Asociado2, Asociado) IsNot Nothing Then
                    If Me._ventana.Asociado2.id <> Integer.MinValue Then
                        Me._ventana.NombreAsociado2 = DirectCast(Me._ventana.Asociado2, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado2, Asociado).Nombre
                    End If
                End If
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub Reporte()
            Mouse.OverrideCursor = Cursors.Wait
            Try

                'para generar el factura pendiente 
                Dim FacFacturaPendienteaux As New FacFacturaPendiente

                'asociado
                If Me._ventana.Asociado IsNot Nothing And Me._ventana.Asociado2 IsNot Nothing Then
                    If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue And DirectCast(Me._ventana.Asociado2, Asociado).Id > Integer.MinValue Then
                        Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                        Dim asociado2 As Asociado = DirectCast(Me._ventana.Asociado2, Asociado)
                        FacFacturaPendienteaux.ValorQuery = " asociado.Id>= " & asociado.Id & " and asociado.Id<= " & asociado2.Id
                    Else
                        If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                            Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            FacFacturaPendienteaux.ValorQuery = " asociado.Id>= " & asociado.Id
                        Else
                            MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                        End If

                    End If
                Else
                    If Me._ventana.Asociado IsNot Nothing Then
                        If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                            Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            FacFacturaPendienteaux.ValorQuery = " asociado.Id>= " & asociado.Id
                        Else
                            MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                        End If
                    Else
                        MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                    End If
                    End If
                'fin asociado

                If Me._ventana.Fecha1 <> "" Then
                    FacFacturaPendienteaux.ValorQuery = FacFacturaPendienteaux.ValorQuery & " and fp.FechaOperacion<= '" & Me._ventana.Fecha1 & "' "
                End If

                If Me._ventana.MayorMenor = "ME" Then
                    FacFacturaPendienteaux.ValorQuery = FacFacturaPendienteaux.ValorQuery & " and fp.FechaFactura< '01/01/2008'"
                Else
                    FacFacturaPendienteaux.ValorQuery = FacFacturaPendienteaux.ValorQuery & " and fp.FechaFactura>= '01/01/2008'"
                End If
                FacFacturaPendienteaux.ValorQuery = FacFacturaPendienteaux.ValorQuery & "  order by pais.Id,fp.FechaFactura"
                _FacFacturaPendiente = _FacFacturaPendienteServicios.ObtenerFacFacturaPendientesFiltro(FacFacturaPendienteaux)
                '_FacFacturaPendiente = _FacFacturaPendienteServicios.ConsultarTodos
                'fin para generar el factura totz



                Dim reporte As New ReportDocument()
                Dim estructuraDeDatosEnc As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)()

                Dim estructuraDeDatosDeta As IList(Of StructReporteFActuraDeta) = New List(Of StructReporteFActuraDeta)()
                'reporte.Load();                
                Dim datosEnc As New DataTable("DataTable1")
                Dim datosDeta As New DataTable("DataTable2")
                datosEnc = datosenc_colum()

                datosDeta = datosdeta_colum()
                'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                'estructuraDeDatos.Add(estructura)

                ObtenerEstructura(estructuraDeDatosEnc, estructuraDeDatosDeta)
                'estructuraDeDatosEnc = ObtenerEstructuraEnc()

                'estructuraDeDatosDeta = ObtenerEstructuraDeta()


                datosEnc = ArmarReporteEnc(datosEnc, estructuraDeDatosEnc)

                datosDeta = ArmarReporteDeta(datosDeta, estructuraDeDatosDeta)
                'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
                Dim ds As New DataSet()
                ds.Tables.Add(datosEnc)
                ds.Tables.Add(datosDeta)
                reporte.Load(GetRutaReporte())
                reporte.SetDataSource(ds)
                'reporte.SetDataSource(datosDeta)
                Mouse.OverrideCursor = Nothing
                IrConsultarReporte(reporte)

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

        Public Sub IrConsultarReporte(ByVal reporte As ReportDocument)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New ReportesRpt(reporte))
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Private Function GetRutaReporte() As String
            Dim retorno As String
            If Me._ventana.MayorMenor = "ME" Then
                retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFacturacionPendienteMenor.rpt"
            Else
                retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFacturaDigital.rpt"
            End If

            'retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFactura.rpt"            
            Return retorno

        End Function

        Public Function etiquetas_texto(ByVal codigo As String) As String()
            Dim valor(2) As String

            Dim etiquetas As List(Of Etiqueta) = _etiquetaServicios.ConsultarTodos()
            Dim EtiquetaFiltrados As IEnumerable(Of Etiqueta) = etiquetas
            EtiquetaFiltrados = From e In EtiquetaFiltrados Where e.Id IsNot Nothing AndAlso e.Id.ToLower().Contains(codigo.ToLower())
            valor(0) = EtiquetaFiltrados(0).Descripcion1
            valor(1) = EtiquetaFiltrados(0).Descripcion2
            Return (valor)

        End Function

        Public Sub lp_fecha_esc_n(ByVal dfecha As Date, ByRef cfecha As String, ByVal idioma As String)
            Dim w_dia, w_mes, w_ano As Integer
            w_mes = dfecha.Month
            w_dia = dfecha.Day
            w_ano = dfecha.Year
            cfecha = fecha(w_mes, w_dia, w_ano, idioma)
        End Sub

        'Public Sub lp_fecha_esc_n_bf(ByVal dfecha As Date, ByRef cfecha As String)
        '    Dim w_dia, w_mes, w_ano As Integer
        '    w_mes = dfecha.Month
        '    w_dia = dfecha.Day
        '    w_ano = dfecha.Year
        '    cfecha = "Caracas 0" & w_dia & "/0" & w_mes & "/" & w_ano
        'End Sub

        Public Function fecha(ByVal mes As Integer, ByVal dia As Integer, ByVal anio As Integer, ByVal idioma As String) As String
            Dim retorna As String = ""
            Select Case mes
                Case 1
                    If idioma = "ES" Then
                        retorna = "Caracas, Enero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, January " & dia & ", " & anio
                    End If
                Case 2
                    If idioma = "ES" Then
                        retorna = "Caracas, Febrero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, February " & dia & ", " & anio
                    End If
                Case 3
                    If idioma = "ES" Then
                        retorna = "Caracas, Marzo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, March " & dia & ", " & anio
                    End If
                Case 4
                    If idioma = "ES" Then
                        retorna = "Caracas, Abril " & dia & ", " & anio
                    Else
                        retorna = "Caracas, April " & dia & ", " & anio
                    End If
                Case 5
                    If idioma = "ES" Then
                        retorna = "Caracas, Mayo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, May " & dia & ", " & anio
                    End If
                Case 6
                    If idioma = "ES" Then
                        retorna = "Caracas, Junio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, June " & dia & ", " & anio
                    End If
                Case 7
                    If idioma = "ES" Then
                        retorna = "Caracas, Julio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, July " & dia & ", " & anio
                    End If
                Case 8
                    If idioma = "ES" Then
                        retorna = "Caracas, Agosto " & dia & ", " & anio
                    Else
                        retorna = "Caracas, August " & dia & ", " & anio
                    End If
                Case 9
                    If idioma = "ES" Then
                        retorna = "Caracas, Septiembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, September " & dia & ", " & anio
                    End If
                Case 10
                    If idioma = "ES" Then
                        retorna = "Caracas, Octubre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, October " & dia & ", " & anio
                    End If
                Case 11
                    If idioma = "ES" Then
                        retorna = "Caracas, Noviembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, November " & dia & ", " & anio
                    End If
                Case 12
                    If idioma = "ES" Then
                        retorna = "Caracas, Diciembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, December " & dia & ", " & anio
                    End If

            End Select

            Return (retorna)
        End Function

        Public Sub lp_compl(ByVal dfecha As DateTime, ByVal nfac As Integer, ByRef csalida As String)
            Dim w_par, w_camp, w_cero As String
            Dim w_fal As Integer
            w_cero = "0"
            w_par = dfecha.Year
            w_par = w_par.Substring(2, 2)
            w_camp = nfac
            If w_camp.Length < 7 Then
                w_fal = 7 - w_camp.Length
                While (w_fal > 0)
                    w_camp = w_cero & w_camp
                    w_fal = w_fal - 1
                End While
            End If
            csalida = w_par & "-" & w_camp
        End Sub

        Public Function Buscar_Operacion(ByVal cfactura As Integer) As Boolean
            Dim operacionaux As New FacOperacion
            operacionaux.CodigoOperacion = cfactura
            operacionaux.Id = "ND"
            Dim operaciones As List(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
            If operaciones.Count >= 0 Then
                Return (True)
            Else
                Return (False)
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


        Public Function consultar_facturadetalle(ByVal idfactura As Integer) As List(Of FacFactuDetalle)
            Dim FacFacturadetalleAuxiliar As New FacFactuDetalle()
            Dim FacFacturasdetalle As List(Of FacFactuDetalle)
            Dim factura As New FacFactura
            factura.Id = idfactura
            FacFacturadetalleAuxiliar.Factura = factura

            FacFacturasdetalle = Me._FacFactuDetaServicios.ObtenerFacFactuDetallesFiltro(FacFacturadetalleAuxiliar)
            If FacFacturasdetalle IsNot Nothing Then
                If FacFacturasdetalle.Count > 0 Then
                    Return (FacFacturasdetalle)
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function

        Public Function ObtenerEstructuraEnc(ByVal total_z As FacFacturaPendiente) As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            Dim w_s As String = ""
            structura = inicializar_enc()
            Try


                If total_z.Caso IsNot Nothing Then
                    structura.Caso = total_z.Caso
                Else
                    structura.Caso = ""
                End If

                If total_z.XAsociado IsNot Nothing Then
                    structura.Cliente = total_z.XAsociado
                Else
                    structura.Cliente = ""
                End If

                If total_z.Rif IsNot Nothing Then
                    structura.Rif = total_z.Rif
                Else
                    structura.Rif = ""
                End If
                If total_z.XNit IsNot Nothing Then
                    structura.Nit = total_z.XNit
                Else
                    structura.Nit = ""
                End If

                If structura.Cliente <> "" Then
                    Select Case total_z.Terrero
                        Case "1" '  
                            If total_z.FechaSeniat IsNot Nothing Then
                                lp_compl(total_z.FechaSeniat, total_z.Seniat, w_s)
                                structura.Invoice = "STATEMENT  N°" & w_s
                            Else
                                structura.Invoice = ""
                            End If
                            If total_z.Asociado.BPendienteStatement = True Then
                                If total_z.Asociado.TipoPersona.ToString = "T" Then
                                    structura.Tipo = "FACTURA  N°"
                                Else
                                    structura.Tipo = "STATEMENT  N°"
                                End If
                            Else
                                structura.Tipo = "FACTURA  N°"
                            End If
                            structura.Seniat = w_s
                            If total_z.FechaSeniat IsNot Nothing Then
                                lp_compl(total_z.FechaSeniat, total_z.Seniat, w_s)
                                structura.Fecha = w_s
                            Else
                                structura.Fecha = ""
                            End If
                        Case "2"
                            structura.Invoice = ""
                            If total_z.FechaSeniat IsNot Nothing Then
                                lp_compl(total_z.FechaSeniat, total_z.Seniat, w_s)
                                structura.Invoice = "STATEMENT  N°" & w_s
                                structura.Xfactura = ""
                            End If
                            If total_z.FechaFactura IsNot Nothing Then
                                lp_compl(total_z.FechaFactura, total_z.Id, w_s)
                                structura.Seniat = w_s
                                lp_fecha_esc_n(total_z.FechaFactura, structura.Fecha, total_z.Idioma.Id)
                            Else
                                structura.Fecha = ""
                            End If

                            If total_z.Asociado.BPendienteStatement = True Then
                                If total_z.FechaFactura IsNot Nothing Then
                                    lp_compl(total_z.FechaFactura, total_z.Id, w_s)
                                    structura.Invoice = "STATEMENT  N°" & w_s
                                    structura.Seniat = w_s
                                    structura.Xfactura = ""
                                    lp_fecha_esc_n(total_z.FechaFactura, structura.Fecha, total_z.Idioma.Id)
                                Else
                                    structura.Fecha = ""
                                End If
                                structura.Tipo = "STATEMENT  N°"
                            Else
                                If total_z.FechaSeniat IsNot Nothing Then
                                    lp_compl(total_z.FechaSeniat, total_z.Seniat, w_s)
                                    structura.Invoice = "STATEMENT  N°" & w_s
                                    structura.Seniat = w_s
                                    structura.Xfactura = ""
                                    lp_fecha_esc_n(total_z.FechaSeniat, structura.Fecha, total_z.Idioma.Id)
                                Else
                                    structura.Fecha = ""
                                End If

                                structura.Tipo = "FACTURA  N°"
                            End If
                        Case "3"
                            If total_z.FechaFactura IsNot Nothing Then
                                lp_compl(total_z.FechaFactura, total_z.Id, w_s)
                                structura.Xfactura = ""
                                structura.Invoice = "STATEMENT  N°" & w_s
                                lp_fecha_esc_n(total_z.FechaFactura, structura.Fecha, total_z.Idioma.Id)
                            Else
                                structura.Fecha = ""
                            End If
                            If total_z.Asociado.BPendienteStatement = True Then
                                structura.Tipo = "STATEMENT  N°"
                            Else
                                If total_z.Asociado.TipoPersona.ToString = "T" Then
                                    structura.Tipo = "STATEMENT  N°"
                                Else
                                    structura.Tipo = "FACTURA  N°"
                                End If
                            End If
                    End Select

                    If total_z.Codeti <> "" And total_z.Codeti IsNot Nothing Then
                        Dim textos(2) As String
                        textos = etiquetas_texto(total_z.Codeti)
                        structura.Texto1 = textos(0)
                        structura.Texto2 = textos(1)
                    End If

                    If Buscar_Operacion(total_z.Id) = True Then
                        Select Case total_z.P_mip
                            Case 1 ' Factura 
                                structura.Seniat = w_s
                            Case 2 'Caso Est
                                structura.Seniat = w_s
                            Case 3 'Caso Factura
                                structura.Seniat = w_s
                            Case 4 ' Est
                                structura.Seniat = w_s
                        End Select
                    End If
                    Dim factura As FacFactura = consultar_factura(total_z.Id)
                    If factura IsNot Nothing Then
                        If Me._ventana.TipoMoneda = "Moneda Original" Then
                            structura.Msubtimpo = factura.MSubtimpo
                            structura.Mdescuento = factura.MDescuento
                            structura.Mtbimp = factura.MTbimp
                            structura.Mtbexc = factura.Mtbexc
                            structura.Msubtotal = factura.MSubtotal
                            structura.Mttotal = factura.Mttotal
                            structura.Mtimp = factura.Mtimp
                        End If
                        If Me._ventana.TipoMoneda = "Bolivar Fuerte" Then
                            structura.Msubtimpo = factura.MSubtimpoBf
                            structura.Mdescuento = factura.MDescuentoBf
                            structura.Mtbimp = factura.MTbimpBf
                            structura.Mtbexc = factura.MTbexcBf
                            structura.Msubtotal = factura.MSubtotalBf
                            structura.Mttotal = factura.MTtotalBf
                            structura.Mtimp = factura.MTimpBf
                        End If
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFActuraEnc), ByRef det As List(Of StructReporteFActuraDeta))

            Dim retorno As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)
            Dim structura As New StructReporteFActuraEnc()
            Dim j As Integer = 0
            Try
                For i As Integer = 0 To _FacFacturaPendiente.Count - 1
                    If i = 0 Then
                        'llamar encabezado
                        structura = ObtenerEstructuraEnc(_FacFacturaPendiente(i))
                        j = j + 1
                        structura.Id = j
                        retorno.Add(structura)
                        ObtenerEstructuraDeta(j, det, _FacFacturaPendiente(i).Id)
                    Else
                        If _FacFacturaPendiente(i).Id <> _FacFacturaPendiente(i - 1).Id Then
                            'llamar encabezado
                            structura = ObtenerEstructuraEnc(_FacFacturaPendiente(i))
                            j = j + 1
                            structura.Id = j
                            retorno.Add(structura)
                            ObtenerEstructuraDeta(j, det, _FacFacturaPendiente(i).Id)
                        End If
                    End If

                    If i = _FacFacturaPendiente.Count - 1 Then
                        'llamar detalle
                    Else
                        If _FacFacturaPendiente(i).Id <> _FacFacturaPendiente(i + 1).Id Then
                            'llamar detalle
                        End If
                    End If
                Next
                enc = retorno
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
        End Sub

        Private Sub ObtenerEstructuraDeta(ByVal id As String, ByVal detalle As List(Of StructReporteFActuraDeta), ByVal idfactura As Integer)
            Dim retorno As IList(Of StructReporteFActuraDeta) = New List(Of StructReporteFActuraDeta)
            retorno = detalle
            Dim _FacFacturaDetalle As List(Of FacFactuDetalle) = consultar_facturadetalle(idfactura)

            Dim structura As New StructReporteFActuraDeta()
            structura = inicializar_deta()
            Try

                For i As Integer = 0 To _FacFacturaDetalle.Count - 1
                    structura.Servicio = _FacFacturaDetalle(i).XDetalle
                    structura.Cantidad = _FacFacturaDetalle(i).NCantidad
                    structura.Id = id
                    If Me._ventana.TipoMoneda = "Moneda Original" Then
                        structura.Npub = _FacFacturaDetalle(i).Pu
                        structura.Ndesc = _FacFacturaDetalle(i).MDescuento
                        structura.MMonto = _FacFacturaDetalle(i).BDetalle
                    End If
                    If Me._ventana.TipoMoneda = "Bolivar Fuerte" Then
                        If _FacFacturaDetalle(i).NCantidad <> 0 Then

                        End If
                        structura.MMonto = _FacFacturaDetalle(i).BDetalleBf
                        structura.Ndesc = _FacFacturaDetalle(i).MDescuento
                        If _FacFacturaDetalle(i).NCantidad <> 0 Then
                            Dim w_cuadre As Double
                            w_cuadre = _FacFacturaDetalle(i).PuBf / _FacFacturaDetalle(i).NCantidad
                            If (w_cuadre <> _FacFacturaDetalle(i).PuBf) Then
                                structura.Npub = w_cuadre
                            Else
                                structura.Npub = _FacFacturaDetalle(i).PuBf
                            End If
                        Else
                            structura.Npub = _FacFacturaDetalle(i).PuBf
                        End If
                    End If

                    'If (w_monto = 0) Then
                    '    Text.detalle = "Services: Servicios:"
                    'Else
                    '    Text.detalle = ""
                    'End If
                    'w_monto = w_monto + mmonto.detalle

                    retorno.Add(structura)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try

            detalle = retorno
        End Sub


        Public Function inicializar_enc() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            structura.Id = ""
            structura.Fecha = ""
            structura.Cliente = ""
            structura.Invoice = ""
            structura.Rif = ""
            structura.Nit = ""
            structura.Caso = ""
            structura.TipoPago = ""
            structura.Texto1 = ""
            structura.Texto2 = ""
            structura.Msubtimpo = ""
            structura.Mdescuento = ""
            structura.Mtbimp = ""
            structura.Mtbexc = ""
            structura.Msubtotal = ""
            structura.Mttotal = ""
            structura.Mtimp = ""
            structura.Moneda = ""
            structura.Piva = ""
            structura.Xfactura = ""
            structura.Control = ""
            structura.Seniat = ""
            structura.Tipo = ""
            structura.Xour = ""
            structura.Xourref = ""
            Return (structura)
        End Function

        Public Function inicializar_deta() As StructReporteFActuraDeta
            Dim structura As New StructReporteFActuraDeta()
            structura.Id = ""
            structura.Servicio = ""
            structura.Na = ""
            structura.Npub = ""
            structura.Cantidad = ""
            structura.MMonto = ""
            structura.Ndesc = ""
            Return (structura)
        End Function

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("Id")
            datosEnc2.Columns.Add("Cliente")
            datosEnc2.Columns.Add("Invoice")
            datosEnc2.Columns.Add("Fecha")
            datosEnc2.Columns.Add("Rif")
            datosEnc2.Columns.Add("Nit")
            datosEnc2.Columns.Add("Caso")
            datosEnc2.Columns.Add("TipoPago")
            datosEnc2.Columns.Add("Texto1")
            datosEnc2.Columns.Add("Texto2")
            datosEnc2.Columns.Add("Msubtimpo")
            datosEnc2.Columns.Add("Mdescuento")
            datosEnc2.Columns.Add("Mtbimp")
            datosEnc2.Columns.Add("Mtbexc")
            datosEnc2.Columns.Add("Msubtotal")
            datosEnc2.Columns.Add("Mtimp")
            datosEnc2.Columns.Add("Moneda")
            datosEnc2.Columns.Add("Mttotal")
            datosEnc2.Columns.Add("Piva")
            datosEnc2.Columns.Add("Xfactura")
            datosEnc2.Columns.Add("Control")
            datosEnc2.Columns.Add("Xour")
            datosEnc2.Columns.Add("Xourref")
            datosEnc2.Columns.Add("Seniat")
            datosEnc2.Columns.Add("Tipo")
            Return datosEnc2
        End Function

        Public Function datosdeta_colum() As DataTable
            Dim datosdeta2 As New DataTable("DataTable2")
            datosdeta2.Columns.Add("Id")
            datosdeta2.Columns.Add("Servicio")
            datosdeta2.Columns.Add("Na")
            datosdeta2.Columns.Add("Cantidad")
            datosdeta2.Columns.Add("Npub")
            datosdeta2.Columns.Add("Ndesc")
            datosdeta2.Columns.Add("MMonto")
            Return datosdeta2
        End Function

        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Id") = structura.Id
                    filaDatos("Fecha") = structura.Fecha
                    filaDatos("Cliente") = structura.Cliente
                    filaDatos("Invoice") = structura.Invoice
                    filaDatos("Rif") = structura.Rif
                    filaDatos("Nit") = structura.Nit
                    filaDatos("Caso") = structura.Caso
                    filaDatos("TipoPago") = structura.TipoPago
                    filaDatos("Texto1") = structura.Texto1
                    filaDatos("Texto2") = structura.Texto2
                    filaDatos("Msubtimpo") = poner_decimal(structura.Msubtimpo)
                    filaDatos("Mdescuento") = poner_decimal(structura.Mdescuento)
                    filaDatos("Mtbimp") = poner_decimal(structura.Mtbimp)
                    filaDatos("Mtbexc") = poner_decimal(structura.Mtbexc)
                    filaDatos("Msubtotal") = poner_decimal(structura.Msubtotal)
                    filaDatos("Mtimp") = poner_decimal(structura.Mtimp)
                    filaDatos("Moneda") = structura.Moneda
                    filaDatos("Piva") = poner_decimal(structura.Piva)
                    filaDatos("Mttotal") = poner_decimal(structura.Mttotal)
                    filaDatos("Xfactura") = structura.Xfactura
                    filaDatos("Control") = structura.Control
                    filaDatos("Xour") = structura.Xour
                    filaDatos("Xourref") = structura.Xourref
                    filaDatos("Seniat") = structura.Seniat
                    filaDatos("Tipo") = structura.Tipo
                    datos.Rows.Add(filaDatos)

                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Private Function ArmarReporteDeta(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraDeta)) As DataTable
            Try
                For Each structura As StructReporteFActuraDeta In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Id") = structura.Id
                    filaDatos("Servicio") = structura.Servicio
                    filaDatos("Na") = structura.Na
                    filaDatos("Npub") = poner_decimal(structura.Npub)
                    filaDatos("Cantidad") = structura.Cantidad
                    filaDatos("MMonto") = poner_decimal(structura.MMonto)
                    filaDatos("Ndesc") = poner_decimal(structura.Ndesc)
                    datos.Rows.Add(filaDatos)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Structure StructReporteFActuraEnc
            Private _Id As String
            Private _Cliente As String
            Private _Fecha As String
            Private _Invoice As String
            Private _Rif As String
            Private _Nit As String
            Private _Caso As String
            Private _TipoPago As String
            Private _Texto1 As String
            Private _Texto2 As String
            Private _Msubtimpo As String
            Private _Mdescuento As String
            Private _Mtbimp As String
            Private _Mtbexc As String
            Private _Msubtotal As String
            Private _Mtimp As String
            Private _Moneda As String
            Private _Mttotal As String
            Private _Piva As String
            Private _Xfactura As String
            Private _Control As String
            Private _Xour As String
            Private _Xourref As String
            Private _Seniat As String
            Private _Tipo As String

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
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

            Public Property Invoice() As String
                Get
                    Return Me._Invoice
                End Get
                Set(ByVal value As String)
                    Me._Invoice = value
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


            Public Property Rif() As String
                Get
                    Return Me._Rif
                End Get
                Set(ByVal value As String)
                    Me._Rif = value
                End Set
            End Property

            Public Property Nit() As String
                Get
                    Return Me._Nit
                End Get
                Set(ByVal value As String)
                    Me._Nit = value
                End Set
            End Property

            Public Property Caso() As String
                Get
                    Return Me._Caso
                End Get
                Set(ByVal value As String)
                    Me._Caso = value
                End Set
            End Property

            'Public Property TituloPago() As String
            '    Get
            '        Return Me._TituloPago
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloPago = value
            '    End Set
            'End Property

            Public Property TipoPago() As String
                Get
                    Return Me._TipoPago
                End Get
                Set(ByVal value As String)
                    Me._TipoPago = value
                End Set
            End Property

            Public Property Texto1() As String
                Get
                    Return Me._Texto1
                End Get
                Set(ByVal value As String)
                    Me._Texto1 = value
                End Set
            End Property

            Public Property Texto2() As String
                Get
                    Return Me._Texto2
                End Get
                Set(ByVal value As String)
                    Me._Texto2 = value
                End Set
            End Property

            Public Property Msubtimpo() As String
                Get
                    Return Me._Msubtimpo
                End Get
                Set(ByVal value As String)
                    Me._Msubtimpo = value
                End Set
            End Property


            Public Property Mdescuento() As String
                Get
                    Return Me._Mdescuento
                End Get
                Set(ByVal value As String)
                    Me._Mdescuento = value
                End Set
            End Property
            Public Property Mtbimp() As String
                Get
                    Return Me._Mtbimp
                End Get
                Set(ByVal value As String)
                    Me._Mtbimp = value
                End Set
            End Property
            Public Property Mtbexc() As String
                Get
                    Return Me._Mtbexc
                End Get
                Set(ByVal value As String)
                    Me._Mtbexc = value
                End Set
            End Property
            Public Property Msubtotal() As String
                Get
                    Return Me._Msubtotal
                End Get
                Set(ByVal value As String)
                    Me._Msubtotal = value
                End Set
            End Property
            Public Property Mtimp() As String
                Get
                    Return Me._Mtimp
                End Get
                Set(ByVal value As String)
                    Me._Mtimp = value
                End Set
            End Property
            Public Property Moneda() As String
                Get
                    Return Me._Moneda
                End Get
                Set(ByVal value As String)
                    Me._Moneda = value
                End Set
            End Property
            Public Property Mttotal() As String
                Get
                    Return Me._Mttotal
                End Get
                Set(ByVal value As String)
                    Me._Mttotal = value
                End Set
            End Property
            Public Property Piva() As String
                Get
                    Return Me._Piva
                End Get
                Set(ByVal value As String)
                    Me._Piva = value
                End Set
            End Property

            'Public Property TituloNa() As String
            '    Get
            '        Return Me._TituloNa
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloNa = value
            '    End Set
            'End Property
            'Public Property TituloCantidad() As String
            '    Get
            '        Return Me._TituloCantidad
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloCantidad = value
            '    End Set
            'End Property
            'Public Property TituloPub() As String
            '    Get
            '        Return Me._TituloPub
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloPub = value
            '    End Set
            'End Property
            'Public Property TituloNDesc() As String
            '    Get
            '        Return Me._TituloNDesc
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloNDesc = value
            '    End Set
            'End Property

            'Public Property TituloMMonto() As String
            '    Get
            '        Return Me._TituloMMonto
            '    End Get
            '    Set(ByVal value As String)
            '        Me._TituloMMonto = value
            '    End Set
            'End Property

            Public Property Xfactura() As String
                Get
                    Return Me._Xfactura
                End Get
                Set(ByVal value As String)
                    Me._Xfactura = value
                End Set
            End Property

            Public Property Control() As String
                Get
                    Return Me._Control
                End Get
                Set(ByVal value As String)
                    Me._Control = value
                End Set
            End Property
            Public Property Xour() As String
                Get
                    Return Me._Xour
                End Get
                Set(ByVal value As String)
                    Me._Xour = value
                End Set
            End Property
            Public Property Xourref() As String
                Get
                    Return Me._Xourref
                End Get
                Set(ByVal value As String)
                    Me._Xourref = value
                End Set
            End Property
            Public Property Seniat() As String
                Get
                    Return Me._Seniat
                End Get
                Set(ByVal value As String)
                    Me._Seniat = value
                End Set
            End Property

            Public Property Tipo As String
                Get
                    Return Me._Tipo
                End Get
                Set(ByVal value As String)
                    Me._Tipo = value
                End Set
            End Property


        End Structure

        Structure StructReporteFActuraDeta
            Private _Id As String
            Private _Servicio As String
            Private _Na As String
            Private _Cantidad As String
            Private _Npub As String
            Private _Ndesc As String
            Private _MMonto As String

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
                End Set
            End Property

            Public Property Servicio() As String
                Get
                    Return Me._Servicio
                End Get
                Set(ByVal value As String)
                    Me._Servicio = value
                End Set
            End Property

            Public Property Na() As String
                Get
                    Return Me._Na
                End Get
                Set(ByVal value As String)
                    Me._Na = value
                End Set
            End Property

            Public Property Cantidad() As String
                Get
                    Return Me._Cantidad
                End Get
                Set(ByVal value As String)
                    Me._Cantidad = value
                End Set
            End Property


            Public Property Npub() As String
                Get
                    Return Me._Npub
                End Get
                Set(ByVal value As String)
                    Me._Npub = value
                End Set
            End Property

            Public Property Ndesc() As String
                Get
                    Return Me._Ndesc
                End Get
                Set(ByVal value As String)
                    Me._Ndesc = value
                End Set
            End Property

            Public Property MMonto() As String
                Get
                    Return Me._MMonto
                End Get
                Set(ByVal value As String)
                    Me._MMonto = value
                End Set
            End Property

        End Structure

    End Class
End Namespace
