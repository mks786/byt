Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes

Imports Trascend.Bolet.Cliente.FacReportes
Imports System.Windows
Imports System.Windows.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DataTable = System.Data.DataTable
Imports System.Data
Namespace Presentadores.FacReportes
    Class PresentadorVentasTotalesRpt
        Inherits PresentadorBase

        Private _ventana As IVentasTotalesRpt
        Private _FacFactura As FacFactura
        Dim _FacFacturaDetalle As List(Of FacFactuDetalle)
        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        Private _tasasServicios As ITasaServicios

        'Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacFacturaProforma">FacFacturaProforma a mostrar</param>
        Public Sub New(ByVal ventana As IVentasTotalesRpt)
            Try
                Me._ventana = ventana
                '_FacFactura = FacFactura

                'Me._ventana.FacFacturaProforma = New FacFacturaProforma()
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacFacturaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaServicios")), IFacFacturaAnuladaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)


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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.VentasTotalesRpt())
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
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemVentasTotalesRpt, Recursos.Ids.fac_menuItemVentasTotalesRpt)
        End Sub

        Public Sub Reporte()
            Mouse.OverrideCursor = Cursors.Wait
            Try

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
                Dim reporte As New ReportDocument()
                Dim ds As New DataSet()
                ds.Tables.Add(datosEnc)
                ds.Tables.Add(datosDeta)
                reporte.Load(GetRutaReporte())
                reporte.SetDataSource(ds)
                'reporte.SetDataSource(datosDeta)
                ' Me._ventana.CrystalViewer = reporte
                'reporte.PrintToPrinter(1, False, 1, 0)                

                'agregar cuando este el la maquina virtual
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
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "VentasTotalesRpt.rpt"
            'retorno = "C:\DG_2012_09_11\DG\src\Cliente\Cliente_Fac\Reportes\Carta1CR.rpt"
            Return retorno

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
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Enero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, January " & dia & ", " & anio
                    End If
                Case 2
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Febrero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, February " & dia & ", " & anio
                    End If
                Case 3
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Marzo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, March " & dia & ", " & anio
                    End If
                Case 4
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Abril " & dia & ", " & anio
                    Else
                        retorna = "Caracas, April " & dia & ", " & anio
                    End If
                Case 5
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Mayo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, May " & dia & ", " & anio
                    End If
                Case 6
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Junio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, June " & dia & ", " & anio
                    End If
                Case 7
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Julio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, July " & dia & ", " & anio
                    End If
                Case 8
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Agosto " & dia & ", " & anio
                    Else
                        retorna = "Caracas, August " & dia & ", " & anio
                    End If
                Case 9
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Septiembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, September " & dia & ", " & anio
                    End If
                Case 10
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Octubre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, October " & dia & ", " & anio
                    End If
                Case 11
                    If _FacFactura.Idioma.Id = "ES" Then
                        retorna = "Caracas, Noviembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, November " & dia & ", " & anio
                    End If
                Case 12
                    If _FacFactura.Idioma.Id = "ES" Then
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

        Public Function consultar_factura_anulada(ByVal id As Integer) As FacFacturaAnulada
            Dim FacFacturaAnuladaAuxiliar As New FacFacturaAnulada()
            Dim FacFacturaAnuladas As List(Of FacFacturaAnulada)
            FacFacturaAnuladaAuxiliar.Id = id

            FacFacturaAnuladas = Me._FacFacturaAnuladaServicios.ObtenerFacFacturaAnuladasFiltro(FacFacturaAnuladaAuxiliar)
            If FacFacturaAnuladas IsNot Nothing Then
                If FacFacturaAnuladas.Count > 0 Then
                    Return (FacFacturaAnuladas(0))
                Else
                    Return (Nothing)
                End If
            Else
                Return (Nothing)
            End If
        End Function

        Public Function consultar_factura() As List(Of FacFactura)

            Dim FacFacturaAuxiliar As New FacFactura()
            Dim FacFacturas As List(Of FacFactura)
            FacFacturas = Nothing
            Try
                Dim fecha As DateTime = Me._ventana.FechaInicio
                FacFacturaAuxiliar.FechaDesde = Me._ventana.FechaInicio
                FacFacturaAuxiliar.FechaHasta = Me._ventana.FechaFin
                FacFacturaAuxiliar.Status = 1

                FacFacturas = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)
                If FacFacturas IsNot Nothing Then
                    If FacFacturas.Count > 0 Then
                        Return (FacFacturas)
                    Else
                        Return (Nothing)
                    End If
                Else
                    Return (Nothing)
                End If

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

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

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFActuraEnc), ByRef det As List(Of StructReporteFActuraDeta))
            'enc = ObtenerEstructuraEnc()
            Dim structuradetalle As StructReporteFActuraDeta
            Dim structura As StructReporteFActuraEnc
            Dim retornadet As IList(Of StructReporteFActuraDeta) = New List(Of StructReporteFActuraDeta)
            Dim retorna As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)
            Dim factura As List(Of FacFactura)
            Dim mtasa_t As Double = 0
            Dim cantidad As Integer = 0
            Dim mmontoext_t, mmonto_t, mbase_t, mbase_timp, mimp_t, mventa_t, msubtimpo_bf_t, mdescuento_bf_t As Double
            mmontoext_t = 0
            mmonto_t = 0
            mbase_t = 0
            mbase_timp = 0
            mimp_t = 0
            mventa_t = 0
            msubtimpo_bf_t = 0
            mdescuento_bf_t = 0
            Try
                factura = consultar_factura()
                For i As Integer = 0 To factura.Count - 1
                    structuradetalle = inicializar_det()

                    If factura(i).FechaSeniat IsNot Nothing Then
                        structuradetalle.Fecha = factura(i).FechaSeniat
                        lp_compl(factura(i).FechaSeniat, factura(i).Seniat, structuradetalle.Xfactura)
                    End If
                    If factura(i).Asociado IsNot Nothing Then
                        If factura(i).Asociado.Nombre IsNot Nothing Then
                            structuradetalle.Desc = factura(i).Asociado.Nombre
                        End If
                        If factura(i).Asociado.Rif IsNot Nothing Then
                            structuradetalle.Rif = factura(i).Asociado.Rif
                        End If
                        If factura(i).Asociado.Pais IsNot Nothing Then
                            'structuradetalle.Desc = structuradetalle.Desc & " " & factura(i).Asociado.Pais.NombreIngles
                        End If
                    End If
                    If factura(i).NumeroControl IsNot Nothing Then
                        structuradetalle.Ncontrol = factura(i).NumeroControl
                    End If

                    If factura(i).Moneda IsNot Nothing Then
                        structuradetalle.Moneda = factura(i).Moneda.Id
                    End If

                    Dim estaanulada As Boolean = False

                    If factura(i).Anulada = "SI" Then
                        Dim anulada As FacFacturaAnulada = consultar_factura_anulada(factura(i).Id)
                        If anulada IsNot Nothing Then
                            If anulada.FechaAnulacion >= Me._ventana.FechaInicio And anulada.FechaAnulacion <= Me._ventana.FechaFin Then
                                structuradetalle.Mmontoext = "0"
                                structuradetalle.Mbase = "0"
                                structuradetalle.Mimp = "0"
                                structuradetalle.imp = "0"
                                structuradetalle.Mmonto = "0"
                                structuradetalle.Mventa = "0"
                                structuradetalle.Mtasa = "0"
                                structuradetalle.Mbase_imp = "0"
                                structuradetalle.Msubtimpo_bf = "0"
                                structuradetalle.Mmontoext = "0"
                                structuradetalle.Mdescuento_bf = "0"
                                structuradetalle.Desc = "*******Factura Anulada*******"
                                estaanulada = True
                            End If
                        End If
                    End If

                    If estaanulada = False Then
                        If factura(i).Moneda IsNot Nothing Then
                            If factura(i).Moneda.Id = "US" Then
                                If factura(i).FechaSeniat IsNot Nothing Then
                                    Dim tasa As Tasa = buscar_tasa(factura(i).FechaSeniat, "")
                                    structuradetalle.Mtasa = tasa.Tasabf
                                    mtasa_t = mtasa_t + tasa.Tasabf
                                End If
                                structuradetalle.Mmontoext = FormatNumber(factura(i).MSubtotal, 2)
                                mmontoext_t = mmontoext_t + factura(i).MSubtotal
                                cantidad = cantidad + 1
                                structuradetalle.Mmonto = FormatNumber(factura(i).MSubtotalBf, 2)
                                mmonto_t = mmonto_t + factura(i).MSubtotalBf
                                structuradetalle.Mbase = FormatNumber(factura(i).MTbexcBf, 2)
                                mbase_t = mbase_t + factura(i).MTbexcBf
                                structuradetalle.Mbase_imp = FormatNumber(factura(i).MTbimpBf, 2)
                                mbase_timp = mbase_timp + factura(i).MTbimpBf
                                structuradetalle.Mimp = FormatNumber(factura(i).MTimpBf, 2)
                                mimp_t = mimp_t + factura(i).MTimpBf
                                structuradetalle.Mventa = FormatNumber(factura(i).MTtotalBf, 2)
                                mventa_t = mventa_t + factura(i).MTtotalBf
                                structuradetalle.imp = factura(i).PSeniat
                            Else
                                structuradetalle.Mmontoext = ""
                                structuradetalle.Mtasa = ""
                                structuradetalle.Mmonto = FormatNumber(factura(i).MSubtotalBf, 2)
                                mmonto_t = mmonto_t + factura(i).MSubtotalBf
                                structuradetalle.Mbase = FormatNumber(factura(i).MTbexcBf, 2)
                                mbase_t = mbase_t + factura(i).MTbexcBf
                                structuradetalle.Mbase_imp = FormatNumber(factura(i).MTbimpBf, 2)
                                mbase_timp = mbase_timp + factura(i).MTbimpBf
                                structuradetalle.Mimp = FormatNumber(factura(i).MTimpBf, 2)
                                mimp_t = mimp_t + factura(i).MTimpBf
                                structuradetalle.Mventa = FormatNumber(factura(i).MTtotalBf, 2)
                                mventa_t = mventa_t + factura(i).MTtotalBf
                                structuradetalle.imp = factura(i).PSeniat
                            End If
                        End If
                        structuradetalle.Msubtimpo_bf = FormatNumber(factura(i).MSubtimpoBf, 2)
                        msubtimpo_bf_t = msubtimpo_bf_t + factura(i).MSubtimpoBf
                        structuradetalle.Mdescuento_bf = FormatNumber(factura(i).MDescuentoBf, 2)
                        mdescuento_bf_t = mdescuento_bf_t + factura(i).MDescuentoBf
                    End If

                    structuradetalle.Id = "1"
                    retornadet.Add(structuradetalle)
                Next
                structura = inicializar_enc()
                structura.Xtitu1 = "LIBRO DE VENTAS"
                If Me._ventana.FechaInicio <> "" And Me._ventana.FechaFin = "" Then
                    structura.Xtitu2 = Me._ventana.FechaInicio
                Else
                    If Me._ventana.FechaInicio <> "" And Me._ventana.FechaFin <> "" Then
                        structura.Xtitu2 = "Entre las fechas " & Me._ventana.FechaInicio & " y " & Me._ventana.FechaFin
                    End If
                End If
                structura.Promedio = FormatNumber(mtasa_t / cantidad, 2)
                structura.Mmontoext_t = FormatNumber(mmontoext_t, 2)
                structura.Mmonto_t = FormatNumber(mmonto_t, 2)
                structura.Mbase_t = FormatNumber(mbase_t, 2)
                structura.Mbase_imp_t = FormatNumber(mbase_timp, 2)
                structura.Mimp_t = FormatNumber(mimp_t, 2)
                structura.Mventa_t = FormatNumber(mventa_t, 2)
                structura.Msubtimpo_bf_t = FormatNumber(msubtimpo_bf_t, 2)
                structura.Mdescuento_bf_t = FormatNumber(mdescuento_bf_t, 2)
                structura.Id = "1"
                retorna.Add(structura)

                enc = retorna
                det = retornadet
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
        End Sub

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("Id")
            datosEnc2.Columns.Add("Mmontoext_t")
            datosEnc2.Columns.Add("Mmonto_t")
            datosEnc2.Columns.Add("Mbase_t")
            datosEnc2.Columns.Add("Mbase_imp_t")
            datosEnc2.Columns.Add("Mimp_t")
            datosEnc2.Columns.Add("Mventa_t")
            datosEnc2.Columns.Add("Promedio")
            datosEnc2.Columns.Add("Xtitu1")
            datosEnc2.Columns.Add("Xtitu2")
            datosEnc2.Columns.Add("Cpagina")
            datosEnc2.Columns.Add("Mdescuento_bf_t")
            datosEnc2.Columns.Add("Msubtimpo_bf_t")
            Return datosEnc2
        End Function

        Public Function datosdeta_colum() As DataTable
            Dim datosdeta2 As New DataTable("DataTable2")
            datosdeta2.Columns.Add("Id")
            datosdeta2.Columns.Add("Fecha")
            datosdeta2.Columns.Add("Xfactura")
            datosdeta2.Columns.Add("Ncontrol")
            datosdeta2.Columns.Add("Rif")
            datosdeta2.Columns.Add("Desc")
            datosdeta2.Columns.Add("Moneda")
            datosdeta2.Columns.Add("Mmontoext")
            datosdeta2.Columns.Add("Mtasa")
            datosdeta2.Columns.Add("Mmonto")
            datosdeta2.Columns.Add("imp")
            datosdeta2.Columns.Add("Mbase")
            datosdeta2.Columns.Add("Mbase_imp")
            datosdeta2.Columns.Add("Mimp")
            datosdeta2.Columns.Add("Mventa")
            datosdeta2.Columns.Add("Msubtimpo_bf")
            datosdeta2.Columns.Add("Mdescuento_bf")
            Return datosdeta2
        End Function

        Public Function inicializar_enc() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            structura.Id = ""
            structura.Mmontoext_t = ""
            structura.Mmonto_t = ""
            structura.Mbase_t = ""
            structura.Mbase_imp_t = ""
            structura.Mimp_t = ""
            structura.Mventa_t = ""
            structura.Promedio = ""
            structura.Xtitu1 = ""
            structura.Xtitu2 = ""
            structura.Cpagina = ""
            structura.Mdescuento_bf_t = ""
            structura.Msubtimpo_bf_t = ""
            Return (structura)
        End Function

        Public Function inicializar_det() As StructReporteFActuraDeta
            Dim structura As New StructReporteFActuraDeta()
            structura.Id = ""
            structura.Fecha = ""
            structura.Xfactura = ""
            structura.Ncontrol = ""
            structura.Rif = ""
            structura.Desc = ""
            structura.Moneda = ""
            structura.Mmontoext = ""
            structura.Mtasa = ""
            structura.Mmonto = ""
            structura.imp = ""
            structura.Mbase = ""
            structura.Mbase_imp = ""
            structura.Mimp = ""
            structura.Mventa = ""
            structura.Msubtimpo_bf = ""
            structura.Mdescuento_bf = ""
            Return (structura)
        End Function

        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Id") = structura.Id
                    filaDatos("Mmontoext_t") = poner_decimal(structura.Mmontoext_t)
                    filaDatos("Mmonto_t") = poner_decimal(structura.Mmonto_t)
                    filaDatos("Mbase_t") = poner_decimal(structura.Mbase_t)
                    filaDatos("Mbase_imp_t") = poner_decimal(structura.Mbase_imp_t)
                    filaDatos("Mimp_t") = poner_decimal(structura.Mimp_t)
                    filaDatos("Mventa_t") = structura.Mventa_t
                    filaDatos("Promedio") = structura.Promedio
                    filaDatos("Xtitu1") = structura.Xtitu1
                    filaDatos("Xtitu2") = structura.Xtitu2
                    filaDatos("Cpagina") = structura.Cpagina
                    filaDatos("Mdescuento_bf_t") = poner_decimal(structura.Mdescuento_bf_t)
                    filaDatos("Msubtimpo_bf_t") = poner_decimal(structura.Msubtimpo_bf_t)
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
                    filaDatos("Fecha") = structura.Fecha
                    filaDatos("Xfactura") = structura.Xfactura
                    filaDatos("Ncontrol") = structura.Ncontrol
                    filaDatos("Rif") = structura.Rif
                    filaDatos("Desc") = structura.Desc
                    filaDatos("Moneda") = structura.Moneda
                    filaDatos("Mmontoext") = poner_decimal(structura.Mmontoext)
                    filaDatos("Mtasa") = poner_decimal(structura.Mtasa)
                    filaDatos("Mmonto") = poner_decimal(structura.Mmonto)
                    filaDatos("imp") = poner_decimal(structura.imp)
                    filaDatos("Mbase") = poner_decimal(structura.Mbase)
                    filaDatos("Mbase_imp") = poner_decimal(structura.Mbase_imp)
                    filaDatos("Mimp") = poner_decimal(structura.Mimp)
                    filaDatos("Mventa") = poner_decimal(structura.Mventa)
                    filaDatos("Msubtimpo_bf") = poner_decimal(structura.Msubtimpo_bf)
                    filaDatos("Mdescuento_bf") = poner_decimal(structura.Mdescuento_bf)

                    datos.Rows.Add(filaDatos)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Structure StructReporteFActuraEnc
            Private _Id As String
            Private _Mmontoext_t As String
            Private _Mmonto_t As String
            Private _Mbase_t As String
            Private _Mbase_imp_t As String
            Private _Mimp_t As String
            Private _Mventa_t As String
            Private _Promedio As String
            Private _Xtitu1 As String
            Private _Xtitu2 As String
            Private _Cpagina As String
            Private _Msubtimpo_bf_t As String
            Private _Mdescuento_bf_t As String

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
                End Set
            End Property

            Public Property Mmontoext_t() As String
                Get
                    Return Me._Mmontoext_t
                End Get
                Set(ByVal value As String)
                    Me._Mmontoext_t = value
                End Set
            End Property

            Public Property Mmonto_t() As String
                Get
                    Return Me._Mmonto_t
                End Get
                Set(ByVal value As String)
                    Me._Mmonto_t = value
                End Set
            End Property

            Public Property Mbase_t() As String
                Get
                    Return Me._Mbase_t
                End Get
                Set(ByVal value As String)
                    Me._Mbase_t = value
                End Set
            End Property

            Public Property Mbase_imp_t() As String
                Get
                    Return Me._Mbase_imp_t
                End Get
                Set(ByVal value As String)
                    Me._Mbase_imp_t = value
                End Set
            End Property

            Public Property Mimp_t() As String
                Get
                    Return Me._Mimp_t
                End Get
                Set(ByVal value As String)
                    Me._Mimp_t = value
                End Set
            End Property

            Public Property Mventa_t() As String
                Get
                    Return Me._Mventa_t
                End Get
                Set(ByVal value As String)
                    Me._Mventa_t = value
                End Set
            End Property

            Public Property Promedio() As String
                Get
                    Return Me._Promedio
                End Get
                Set(ByVal value As String)
                    Me._Promedio = value
                End Set
            End Property

            Public Property Xtitu1() As String
                Get
                    Return Me._Xtitu1
                End Get
                Set(ByVal value As String)
                    Me._Xtitu1 = value
                End Set
            End Property

            Public Property Xtitu2() As String
                Get
                    Return Me._Xtitu2
                End Get
                Set(ByVal value As String)
                    Me._Xtitu2 = value
                End Set
            End Property

            Public Property Cpagina() As String
                Get
                    Return Me._Cpagina
                End Get
                Set(ByVal value As String)
                    Me._Cpagina = value
                End Set
            End Property

            Public Property Msubtimpo_bf_t() As String
                Get
                    Return Me._Msubtimpo_bf_t
                End Get
                Set(ByVal value As String)
                    Me._Msubtimpo_bf_t = value
                End Set
            End Property

            Public Property Mdescuento_bf_t() As String
                Get
                    Return Me._Mdescuento_bf_t
                End Get
                Set(ByVal value As String)
                    Me._Mdescuento_bf_t = value
                End Set
            End Property

        End Structure

        Structure StructReporteFActuraDeta
            Private _Id As String
            Private _Fecha As String
            Private _Xfactura As String
            Private _Ncontrol As String
            Private _Rif As String
            Private _Desc As String
            Private _Moneda As String
            Private _Mmontoext As String
            Private _Mtasa As String
            Private _Mmonto As String
            Private _imp As String
            Private _Mbase As String
            Private _Mbase_imp As String
            Private _Mimp As String
            Private _Mventa As String
            Private _Msubtimpo_bf As String
            Private _Mdescuento_bf As String

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
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

            Public Property Xfactura() As String
                Get
                    Return Me._Xfactura
                End Get
                Set(ByVal value As String)
                    Me._Xfactura = value
                End Set
            End Property

            Public Property Ncontrol() As String
                Get
                    Return Me._Ncontrol
                End Get
                Set(ByVal value As String)
                    Me._Ncontrol = value
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

            Public Property Desc() As String
                Get
                    Return Me._Desc
                End Get
                Set(ByVal value As String)
                    Me._Desc = value
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

            Public Property Mmontoext() As String
                Get
                    Return Me._Mmontoext
                End Get
                Set(ByVal value As String)
                    Me._Mmontoext = value
                End Set
            End Property

            Public Property Mtasa() As String
                Get
                    Return Me._Mtasa
                End Get
                Set(ByVal value As String)
                    Me._Mtasa = value
                End Set
            End Property

            Public Property Mmonto() As String
                Get
                    Return Me._Mmonto
                End Get
                Set(ByVal value As String)
                    Me._Mmonto = value
                End Set
            End Property

            Public Property imp() As String
                Get
                    Return Me._imp
                End Get
                Set(ByVal value As String)
                    Me._imp = value
                End Set
            End Property


            Public Property Mbase() As String
                Get
                    Return Me._Mbase
                End Get
                Set(ByVal value As String)
                    Me._Mbase = value
                End Set
            End Property

            Public Property Mbase_imp() As String
                Get
                    Return Me._Mbase_imp
                End Get
                Set(ByVal value As String)
                    Me._Mbase_imp = value
                End Set
            End Property

            Public Property Mimp() As String
                Get
                    Return Me._Mimp
                End Get
                Set(ByVal value As String)
                    Me._Mimp = value
                End Set
            End Property

            Public Property Mventa() As String
                Get
                    Return Me._Mventa
                End Get
                Set(ByVal value As String)
                    Me._Mventa = value
                End Set
            End Property

            Public Property Msubtimpo_bf() As String
                Get
                    Return Me._Msubtimpo_bf
                End Get
                Set(ByVal value As String)
                    Me._Msubtimpo_bf = value
                End Set
            End Property

            Public Property Mdescuento_bf() As String
                Get
                    Return Me._Mdescuento_bf
                End Get
                Set(ByVal value As String)
                    Me._Mdescuento_bf = value
                End Set
            End Property

        End Structure
    End Class
End Namespace
