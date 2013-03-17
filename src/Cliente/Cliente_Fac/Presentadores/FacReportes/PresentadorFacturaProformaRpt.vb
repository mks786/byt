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

Imports Trascend.Bolet.Cliente.FacReportes
Imports System.Windows
Imports System.Windows.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DataTable = System.Data.DataTable
Imports System.Data
Namespace Presentadores.FacReportes
    Class PresentadorFacturaProformaRpt
        Inherits PresentadorBase

        Private _ventana As IFacturaProformaRpt
        Private _FacFacturaProforma As FacFacturaProforma
        Dim _FacFacturaProformaDetalle As List(Of FacFactuDetaProforma)
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios        
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _etiquetaServicios As IEtiquetaServicios

        Private _FacFactuDetaServicios As IFacFactuDetaProformaServicios

        'Dim xoperacion As String
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacFacturaProformaProforma">FacFacturaProformaProforma a mostrar</param>
        Public Sub New(ByVal ventana As IFacturaProformaRpt, ByVal FacFacturaProforma As Object)
            Try
                Me._ventana = ventana
                _FacFacturaProforma = FacFacturaProforma

                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)

                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetaProformaServicios")), IFacFactuDetaProformaServicios)
                Me._etiquetaServicios = DirectCast(Activator.GetObject(GetType(IEtiquetaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("EtiquetaServicios")), IEtiquetaServicios)


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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacturaProformaRpt(_FacFacturaProforma))
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
                Me.ActualizarTituloVentanaPrincipal("Reporte FacturaProforma", "Reporte")

                'Dim FacFacturaProforma As FacFacturaProforma = DirectCast(Me._ventana.FacFacturaProforma, FacFacturaProforma)

                Dim detalleaux As New FacFactuDetaProforma()
                detalleaux.Factura = _FacFacturaProforma
                _FacFacturaProformaDetalle = Me._FacFactuDetaServicios.ObtenerFacFactuDetaProformasFiltro(detalleaux)

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

        Public Sub Reporte()
            Mouse.OverrideCursor = Cursors.Wait
            Try

                Dim reporte As New ReportDocument()
                Dim estructuraDeDatosEnc As IList(Of StructReporteFacturaProformaEnc) = New List(Of StructReporteFacturaProformaEnc)()

                Dim estructuraDeDatosDeta As IList(Of StructReporteFacturaProformaDeta) = New List(Of StructReporteFacturaProformaDeta)()
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
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFacturaProforma.rpt"
            'retorno = "C:\DG_2012_09_11\DG\src\Cliente\Cliente_Fac\Reportes\Carta1CR.rpt"
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
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Enero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, January " & dia & ", " & anio
                    End If
                Case 2
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Febrero " & dia & ", " & anio
                    Else
                        retorna = "Caracas, February " & dia & ", " & anio
                    End If
                Case 3
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Marzo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, March " & dia & ", " & anio
                    End If
                Case 4
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Abril " & dia & ", " & anio
                    Else
                        retorna = "Caracas, April " & dia & ", " & anio
                    End If
                Case 5
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Mayo " & dia & ", " & anio
                    Else
                        retorna = "Caracas, May " & dia & ", " & anio
                    End If
                Case 6
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Junio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, June " & dia & ", " & anio
                    End If
                Case 7
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Julio " & dia & ", " & anio
                    Else
                        retorna = "Caracas, July " & dia & ", " & anio
                    End If
                Case 8
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Agosto " & dia & ", " & anio
                    Else
                        retorna = "Caracas, August " & dia & ", " & anio
                    End If
                Case 9
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Septiembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, September " & dia & ", " & anio
                    End If
                Case 10
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Octubre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, October " & dia & ", " & anio
                    End If
                Case 11
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
                        retorna = "Caracas, Noviembre " & dia & ", " & anio
                    Else
                        retorna = "Caracas, November " & dia & ", " & anio
                    End If
                Case 12
                    If _FacFacturaProforma.Idioma.Id = "ES" Then
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

        Public Function BT_FACT00_N() As StructReporteFacturaProformaEnc
            Dim structura As New StructReporteFacturaProformaEnc()
            Dim w_s As String = ""
            structura = inicializar_enc()
            Try
                'w_lista = "cFacturaProforma=%%cFacturaProforma.fac_FacturaProformastipo=%%xterrero.fac_FacturaProformasimp=%%dst.trailermip=2bst=0ifpago=%%ifpago.fac_FacturaProformas"
                If _FacFacturaProforma.Caso IsNot Nothing Then
                    structura.Caso = _FacFacturaProforma.Caso
                Else
                    structura.Caso = ""
                End If
                'If _FacFacturaProforma.Status = 1 Then 'if ($ifp$ = 1)
                '    structura.TituloPago = "Condicion de Pago : Contado"
                'Else
                '    structura.TituloPago = "Condicion de Pago : Contado"
                'End If

                If _FacFacturaProforma.AsociadoImp IsNot Nothing Then
                    'If _FacFacturaProforma.Asociado.BIsf = True Then
                    '    structura.Invoice = "I N V O I C E   N°"
                    'End If
                    'structura.Cliente = _FacFacturaProforma.Asociado.Nombre
                    'structura. = _FacFacturaProforma.Asociado.Domicilio
                    'structura.Rif = _FacFacturaProforma.Asociado.Rif
                    'structura.Nit = _FacFacturaProforma.Asociado.Nit
                    '---  '_FacFacturaProforma.Asociado.Pais.NombreEspanol 
                Else
                    If _FacFacturaProforma.InteresadoImp IsNot Nothing Then

                    End If
                    'If _FacFacturaProforma.Asociado.BIsf = True Then
                    '    structura.Invoice = "I N V O I C E   N°"
                    'End If
                    'structura.Cliente = _FacFacturaProforma.InteresadoImp.Nombre
                    'structura. = _FacFacturaProforma.InteresadoImp.Domicilio
                    'structura.Rif = _FacFacturaProforma.InteresadoImp.Rif
                    'structura.Nit = _FacFacturaProforma.InteresadoImp.Nit
                    '---  '_FacFacturaProforma.InteresadoImp.Pais.NombreEspanol 
                End If

                If _FacFacturaProforma.XAsociado IsNot Nothing Then
                    structura.Cliente = _FacFacturaProforma.XAsociado
                Else
                    structura.Cliente = ""
                End If

                If _FacFacturaProforma.Rif IsNot Nothing Then
                    structura.Rif = _FacFacturaProforma.Rif
                Else
                    structura.Rif = ""
                End If
                If _FacFacturaProforma.XNit IsNot Nothing Then
                    structura.Nit = _FacFacturaProforma.XNit
                Else
                    structura.Nit = ""
                End If

                If structura.Cliente <> "" Then

                    If _FacFacturaProforma.P_mip IsNot Nothing Then
                        Select Case _FacFacturaProforma.P_mip
                            Case 1 ' FacturaProforma 
                                'call lp_compl(fseniat.fac_FacturaProformas,cseniat.fac_FacturaProformas,w_s) 
                                'xFacturaProforma.encabezado   = w_s                            
                                structura.Invoice = ""
                                structura.XFacturaProforma = "PROFORMA  N° " & _FacFacturaProforma.Id
                                If _FacFacturaProforma.FechaSeniat IsNot Nothing Then
                                    lp_compl(_FacFacturaProforma.FechaSeniat, _FacFacturaProforma.Seniat, w_s)
                                    'If _FacFacturaProforma.Status <> 2 Then                                    
                                    'Else
                                    '    structura.XFacturaProforma = w_s
                                    'End If

                                    lp_fecha_esc_n(_FacFacturaProforma.FechaSeniat, structura.Fecha)
                                Else
                                    structura.Fecha = ""
                                End If
                            Case 2 'Caso Est
                                'Call lp_compl(fFacturaProforma.fac_FacturaProformas, cFacturaProforma.fac_FacturaProformas, w_s)
                                'xinvoice.encabezado = w_s
                                structura.Invoice = ""
                                structura.XFacturaProforma = "PROFORMA  N° " & _FacFacturaProforma.Id
                                If _FacFacturaProforma.FechaFactura IsNot Nothing Then
                                    lp_compl(_FacFacturaProforma.FechaFactura, _FacFacturaProforma.Id, w_s)
                                    'If _FacFacturaProforma.Status <> 2 Then                                    
                                    'Else
                                    '    structura.Invoice = w_s
                                    'End If
                                    'structura.XFacturaProforma = ""

                                    lp_fecha_esc_n(_FacFacturaProforma.FechaFactura, structura.Fecha)
                                Else
                                    structura.Fecha = ""
                                End If
                            Case 3 'Caso FacturaProforma
                                'call lp_compl(fseniat.fac_FacturaProformas,cseniat.fac_FacturaProformas,w_s) 
                                'xFacturaProforma.encabezado   = w_s                            
                                structura.Invoice = ""
                                structura.XFacturaProforma = "PROFORMA  N° " & _FacFacturaProforma.Id
                                If _FacFacturaProforma.FechaSeniat IsNot Nothing Then
                                    lp_compl(_FacFacturaProforma.FechaSeniat, _FacFacturaProforma.Seniat, w_s)
                                    'If _FacFacturaProforma.Status <> 2 Then                                    
                                    'Else
                                    '    structura.XFacturaProforma = w_s
                                    'End If
                                    ' structura.Invoice = ""
                                    lp_fecha_esc_n(_FacFacturaProforma.FechaSeniat, structura.Fecha)
                                Else
                                    structura.Fecha = ""
                                End If
                                If _FacFacturaProforma.FechaFactura IsNot Nothing Then
                                    lp_compl(_FacFacturaProforma.FechaFactura, _FacFacturaProforma.Id, w_s)
                                End If
                            Case 4 ' Est
                                'Call lp_compl(fFacturaProforma.fac_FacturaProformas, cFacturaProforma.fac_FacturaProformas, w_s)
                                'xinvoice.encabezado = w_s
                                If _FacFacturaProforma.FechaFactura IsNot Nothing Then
                                    lp_compl(_FacFacturaProforma.FechaFactura, _FacFacturaProforma.Id, w_s)
                                    If _FacFacturaProforma.Status <> 2 Then
                                        structura.Invoice = "PROFORMA  N° " & w_s
                                    Else
                                        structura.Invoice = w_s
                                    End If
                                    structura.XFacturaProforma = ""

                                    lp_fecha_esc_n(_FacFacturaProforma.FechaFactura, structura.Fecha)
                                Else
                                    structura.Fecha = ""
                                End If
                        End Select
                        'skip(10)
                        'print_break("ENCABEZADO")
                    End If
                End If

                '''NOTA :Agregar campo a a la structura verificar donde se imprime
                'If _FacFacturaProforma.NumeroControl IsNot Nothing Then
                '    structura.Control = _FacFacturaProforma.NumeroControl
                'Else
                '    structura.Control = ""
                'End If
                If _FacFacturaProforma.Codeti <> "" And _FacFacturaProforma.Codeti IsNot Nothing Then
                    Dim textos(2) As String
                    Select Case _FacFacturaProforma.P_mip
                        Case 1 ' FacturaProforma 
                            textos = etiquetas_texto(_FacFacturaProforma.Codeti)
                        Case 2 'Caso Est
                            textos = etiquetas_texto(_FacFacturaProforma.Codeti)
                        Case 3 'Caso FacturaProforma
                            textos = etiquetas_texto(_FacFacturaProforma.Codeti)
                        Case 4 ' Est
                            textos = etiquetas_texto(_FacFacturaProforma.Codeti)
                    End Select
                    structura.Texto1 = textos(0)
                    structura.Texto2 = textos(1)
                End If
                '''NOTA :Agregar campo a a la structura verificar donde se imprime
                'If _FacFacturaProforma.Idioma.Id = "ES" Then
                '    structura.Xour = "Nuestra Referencia"
                '    structura.Xourref = _FacFacturaProforma.Ourref
                'Else
                If _FacFacturaProforma.Idioma.Id = "ES" Then
                    structura.Xour = "Nuestra Referencia"
                    structura.xins = "Instrucciones"
                Else
                    structura.Xour = "Our Reference"
                    '
                    structura.xins = "Instruction"
                End If
                structura.Xourref = _FacFacturaProforma.Ourref
                'agregar
                structura.xinstruc = _FacFacturaProforma.Instruc

                'End If

                structura.Msubtimpo = SetFormatoDouble2(_FacFacturaProforma.MSubtimpo)
                structura.Mdescuento = SetFormatoDouble2(_FacFacturaProforma.MDescuento)
                structura.Mtbimp = SetFormatoDouble2(_FacFacturaProforma.MTbimp)
                structura.Mtbexc = SetFormatoDouble2(_FacFacturaProforma.Mtbexc)
                structura.Msubtotal = SetFormatoDouble2(_FacFacturaProforma.MSubtotal)
                structura.Mttotal = SetFormatoDouble2(_FacFacturaProforma.Mttotal)
                structura.Mtimp = SetFormatoDouble2(_FacFacturaProforma.Mtimp)

                Select Case _FacFacturaProforma.P_mip
                    Case 1 ' FacturaProforma 
                        structura.Seniat = w_s
                    Case 2 'Caso Est
                        structura.Seniat = w_s
                    Case 3 'Caso FacturaProforma
                        structura.Seniat = w_s
                    Case 4 ' Est
                        structura.Seniat = w_s
                End Select

                If _FacFacturaProforma.Status = 1 Then
                    If _FacFacturaProforma.Moneda.Id <> "BF" Then
                        structura.Moneda = _FacFacturaProforma.Moneda.Id
                    Else
                        structura.Moneda = "BSF"
                        structura.Moneda2 = ""
                        structura.Mensajebsf = ""
                    End If
                Else
                    structura.Moneda2 = "BSF"
                    structura.Mensajebsf = "Currency: Moneda:"

                End If
                'If _FacFacturaProforma.Bst = 1 Then
                '    structura.Piva = _FacFacturaProforma.PSeniat
                'Else
                structura.Piva = SetFormatoDouble2(_FacFacturaProforma.Impuesto)
                'End If
                structura.TituloNa = "Iva Tax "
                structura.TituloCantidad = "Cant Quanti"
                structura.TituloPub = "Precio/Unt Price/Unt"
                structura.TituloNDesc = "Descuento Discount"
                structura.TituloMMonto = "Monto Amount"


            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFacturaProformaEnc), ByRef det As List(Of StructReporteFacturaProformaDeta))
            enc = ObtenerEstructuraEnc()
            Select Case _FacFacturaProforma.Status
                Case 1
                    ObtenerEstructuraDeta("N", "1", det)
                Case 2
                    ObtenerEstructuraDeta("NBF", "1", det)
            End Select
            'If (_FacFacturaProforma.Status <> 1 And _FacFacturaProforma.Status <> 2 And _FacFacturaProforma.Status <> 3 And _FacFacturaProforma.Status <> 4) Then
            '    Select Case _FacFacturaProforma.Terrero.ToString
            '        Case "1"
            '            ObtenerEstructuraDeta("NBF", "1", det)
            '        Case "2"
            '            ObtenerEstructuraDeta("N", "1", det)
            '            ObtenerEstructuraDeta("NBF", "2", det)
            '        Case "3"
            '            ObtenerEstructuraDeta("N", "1", det)
            '    End Select
            'Else
            '    If _FacFacturaProforma.Status = 1 Then
            '        Select Case _FacFacturaProforma.Terrero.ToString
            '            Case "1"
            '                ObtenerEstructuraDeta("NBF", "1", det)
            '            Case "2"
            '                ObtenerEstructuraDeta("NBF", "1", det)
            '            Case "3"
            '                ObtenerEstructuraDeta("N", "1", det)
            '        End Select
            '    Else
            '        If _FacFacturaProforma.Status = 2 Then
            '            Select Case _FacFacturaProforma.Terrero.ToString
            '                Case "1"
            '                    ObtenerEstructuraDeta("NBF", "1", det)
            '                Case "2"
            '                    ObtenerEstructuraDeta("N", "1", det)
            '            End Select

            '        Else
            '            If _FacFacturaProforma.Status = 3 Then
            '                ObtenerEstructuraDeta("N", "1", det)
            '            Else
            '                If _FacFacturaProforma.Status = 4 Then
            '                    ObtenerEstructuraDeta("NBF", "1", det)
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
        End Sub

        Private Function ObtenerEstructuraEnc() As List(Of StructReporteFacturaProformaEnc)
            Dim retorno As IList(Of StructReporteFacturaProformaEnc) = New List(Of StructReporteFacturaProformaEnc)
            Dim structura As New StructReporteFacturaProformaEnc()
            Try
                _FacFacturaProforma.P_mip = 3                            
                structura = BT_FACT00_N()
                structura.Id = "1"
                retorno.Add(structura)

                'If (_FacFacturaProforma.Status <> 1 And _FacFacturaProforma.Status <> 2 And _FacFacturaProforma.Status <> 3 And _FacFacturaProforma.Status <> 4) Then
                '    Select Case _FacFacturaProforma.Terrero.ToString
                '        Case "1"
                '            _FacFacturaProforma.P_mip = 1
                '            _FacFacturaProforma.Bst = 0
                '            structura = BT_FACT00_N_BF()
                '            structura.Id = "1"
                '            retorno.Add(structura)
                '        Case "2"
                '            _FacFacturaProforma.P_mip = 2
                '            _FacFacturaProforma.Bst = 0
                '            structura = BT_FACT00_N()
                '            structura.Id = "1"
                '            retorno.Add(structura)

                '            _FacFacturaProforma.P_mip = 3
                '            _FacFacturaProforma.Bst = 0
                '            structura = BT_FACT00_N_BF()
                '            structura.Id = "2"
                '            retorno.Add(structura)
                '        Case "3"
                '            _FacFacturaProforma.P_mip = 1
                '            _FacFacturaProforma.Bst = 0
                '            structura = BT_FACT00_N()
                '            structura.Id = "1"
                '            retorno.Add(structura)
                '    End Select
                'Else

                '    If _FacFacturaProforma.Status = 1 Then
                '        Select Case _FacFacturaProforma.Terrero.ToString
                '            Case "1"
                '                _FacFacturaProforma.P_mip = 1
                '                _FacFacturaProforma.Bst = 0
                '                structura = BT_FACT00_N_BF()
                '                structura.Id = "1"
                '                retorno.Add(structura)
                '            Case "2"
                '                _FacFacturaProforma.P_mip = 3
                '                _FacFacturaProforma.Bst = 0
                '                structura = BT_FACT00_N_BF()
                '                structura.Id = "1"
                '                retorno.Add(structura)
                '                'Case "3"
                '                '    _FacFacturaProforma.P_mip = 1
                '                '    _FacFacturaProforma.Bst = 0
                '                '    structura = BT_FACT00_N()
                '                '    structura.Id = "1"
                '                '    retorno.Add(structura)
                '        End Select
                '    Else
                '        If _FacFacturaProforma.Status = 2 Then
                '            Select Case _FacFacturaProforma.Terrero.ToString
                '                Case "1"
                '                    _FacFacturaProforma.P_mip = 3
                '                    _FacFacturaProforma.Bst = 1
                '                    _FacFacturaProforma.Terrero = "3"
                '                    structura = BT_FACT00_N_BF() 'deveria ser el BT_FACT00_N_BF_p                        
                '                    structura.Id = "1"
                '                    retorno.Add(structura)

                '                    '_FacFacturaProforma.P_mip = 1
                '                    '_FacFacturaProforma.Bst = 0
                '                    'structura = BT_FACT00_N_BF()
                '                    'structura.Id = "1"
                '                    'retorno.Add(structura)
                '                Case "2"
                '                    _FacFacturaProforma.P_mip = 3
                '                    _FacFacturaProforma.Bst = 0
                '                    structura = BT_FACT00_N()
                '                    structura.Id = "1"
                '                    retorno.Add(structura)
                '                    'Case "3"
                '                    '    _FacFacturaProforma.P_mip = 1
                '                    '    _FacFacturaProforma.Bst = 0
                '                    '    structura = BT_FACT00_N()
                '                    '    structura.Id = "1"
                '                    '    retorno.Add(structura)
                '            End Select
                '        Else
                '            If _FacFacturaProforma.Status = 3 Then
                '                Select Case _FacFacturaProforma.Terrero.ToString
                '                    Case "2"
                '                        _FacFacturaProforma.P_mip = 2
                '                        _FacFacturaProforma.Bst = 0
                '                        structura = BT_FACT00_N()
                '                        structura.Id = "1"
                '                        retorno.Add(structura)
                '                    Case "3"
                '                        _FacFacturaProforma.P_mip = 4
                '                        _FacFacturaProforma.Bst = 0
                '                        structura = BT_FACT00_N()
                '                        structura.Id = "1"
                '                        retorno.Add(structura)
                '                End Select
                '            Else
                '                If _FacFacturaProforma.Status = 4 Then
                '                    _FacFacturaProforma.P_mip = 3
                '                    _FacFacturaProforma.Bst = 1
                '                    _FacFacturaProforma.Terrero = "3"
                '                    structura = BT_FACT00_N_BF() 'deveria ser el BT_FACT00_N_BF_p                        
                '                    structura.Id = "1"
                '                    retorno.Add(structura)
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return retorno
        End Function

        Private Sub ObtenerEstructuraDeta(ByVal tipo As String, ByVal id As String, ByVal detalle As List(Of StructReporteFacturaProformaDeta))
            Dim retorno As IList(Of StructReporteFacturaProformaDeta) = New List(Of StructReporteFacturaProformaDeta)
            retorno = detalle
            Dim structura As New StructReporteFacturaProformaDeta()
            Try

                For i As Integer = 0 To _FacFacturaProformaDetalle.Count - 1
                    structura.Cantidad = _FacFacturaProformaDetalle(i).NCantidad
                    structura.Id = id
                    If tipo = "N" Then
                        structura.Servicio = _FacFacturaProformaDetalle(i).XDetalle
                        structura.Npub = SetFormatoDouble2(_FacFacturaProformaDetalle(i).Pu)
                        structura.Ndesc = SetFormatoDouble2(_FacFacturaProformaDetalle(i).MDescuento)
                        structura.MMonto = SetFormatoDouble2(_FacFacturaProformaDetalle(i).BDetalle)
                    Else
                        structura.Servicio = _FacFacturaProformaDetalle(i).XDetalleEs
                        structura.MMonto = SetFormatoDouble2(_FacFacturaProformaDetalle(i).BDetalleBf)
                        structura.Ndesc = SetFormatoDouble2(_FacFacturaProformaDetalle(i).MDescuento)

                        If _FacFacturaProformaDetalle(i).NCantidad <> 0 Then
                            Dim w_cuadre As Double
                            w_cuadre = _FacFacturaProformaDetalle(i).PuBf / _FacFacturaProformaDetalle(i).NCantidad
                            If (w_cuadre <> _FacFacturaProformaDetalle(i).PuBf) Then
                                structura.Npub = SetFormatoDouble2(w_cuadre)
                            Else
                                structura.Npub = SetFormatoDouble2(_FacFacturaProformaDetalle(i).PuBf)
                            End If
                        Else
                            structura.Npub = SetFormatoDouble2(_FacFacturaProformaDetalle(i).PuBf)
                        End If

                    End If

                    If _FacFacturaProformaDetalle(i).Impuesto.ToString = "T" Then
                        structura.Na = "X"
                    Else
                        structura.Na = ""
                    End If
                    retorno.Add(structura)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try

            detalle = retorno
        End Sub

        Public Function inicializar_enc() As StructReporteFacturaProformaEnc
            Dim structura As New StructReporteFacturaProformaEnc()
            structura.Id = ""
            structura.Fecha = ""
            structura.Cliente = ""
            structura.Invoice = ""
            structura.Rif = ""
            structura.Nit = ""
            structura.Caso = ""
            structura.TituloPago = ""
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
            structura.TituloNa = ""
            structura.TituloCantidad = ""
            structura.TituloPub = ""
            structura.TituloNDesc = ""
            structura.TituloMMonto = ""
            structura.XFacturaProforma = ""
            structura.Control = ""
            structura.Seniat = ""
            structura.Xour = ""
            structura.Xourref = ""
            structura.xins = ""
            structura.xinstruc = ""
            structura.Moneda2 = ""
            structura.Mensajebsf = ""
            Return (structura)
        End Function

        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFacturaProformaEnc)) As DataTable
            Try
                For Each structura As StructReporteFacturaProformaEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Id") = structura.Id
                    filaDatos("Fecha") = structura.Fecha
                    filaDatos("Cliente") = structura.Cliente
                    filaDatos("Invoice") = structura.Invoice
                    filaDatos("Rif") = structura.Rif
                    filaDatos("Nit") = structura.Nit
                    filaDatos("Caso") = structura.Caso
                    filaDatos("TituloPago") = structura.TituloPago
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
                    filaDatos("TituloNa") = structura.TituloNa
                    filaDatos("TituloCantidad") = structura.TituloCantidad
                    filaDatos("TituloPub") = structura.TituloPub
                    filaDatos("TituloNDesc") = structura.TituloNDesc
                    filaDatos("TituloMMonto") = structura.TituloMMonto
                    filaDatos("Mttotal") = poner_decimal(structura.Mttotal)
                    filaDatos("XFacturaProforma") = structura.XFacturaProforma
                    filaDatos("Control") = structura.Control
                    filaDatos("Xour") = structura.Xour
                    filaDatos("Xourref") = structura.Xourref
                    filaDatos("Seniat") = structura.Seniat
                    filaDatos("xins") = structura.xins
                    filaDatos("xinstruc") = structura.xinstruc
                    filaDatos("Moneda2") = structura.Moneda2
                    filaDatos("Mensajebsf") = structura.Mensajebsf
                    datos.Rows.Add(filaDatos)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Private Function ArmarReporteDeta(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFacturaProformaDeta)) As DataTable
            Try
                For Each structura As StructReporteFacturaProformaDeta In estructurasDeDatos
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

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("Id")
            datosEnc2.Columns.Add("Cliente")
            datosEnc2.Columns.Add("Invoice")
            datosEnc2.Columns.Add("Fecha")
            datosEnc2.Columns.Add("Rif")
            datosEnc2.Columns.Add("Nit")
            datosEnc2.Columns.Add("Caso")
            datosEnc2.Columns.Add("TituloPago")
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
            datosEnc2.Columns.Add("TituloNa")
            datosEnc2.Columns.Add("TituloCantidad")
            datosEnc2.Columns.Add("TituloPub")
            datosEnc2.Columns.Add("TituloNDesc")
            datosEnc2.Columns.Add("TituloMMonto")
            datosEnc2.Columns.Add("XFacturaProforma")
            datosEnc2.Columns.Add("Control")
            datosEnc2.Columns.Add("Xour")
            datosEnc2.Columns.Add("Xourref")
            datosEnc2.Columns.Add("Seniat")
            datosEnc2.Columns.Add("xins")
            datosEnc2.Columns.Add("xinstruc")
            datosEnc2.Columns.Add("Moneda2")
            datosEnc2.Columns.Add("Mensajebsf")
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

        Structure StructReporteFacturaProformaEnc
            Private _Id As String
            Private _Cliente As String
            Private _Fecha As String
            Private _Invoice As String
            Private _Rif As String
            Private _Nit As String
            Private _Caso As String
            Private _TituloPago As String
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
            Private _TituloNa As String
            Private _TituloCantidad As String
            Private _TituloPub As String
            Private _TituloNDesc As String
            Private _TituloMMonto As String
            Private _XFacturaProforma As String
            Private _Control As String
            Private _Xour As String
            Private _Xourref As String
            Private _Seniat As String
            Private _xins As String
            Private _xinstruc As String

            Private _Moneda2 As String
            Private _Mensajebsf As String

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

            Public Property TituloPago() As String
                Get
                    Return Me._TituloPago
                End Get
                Set(ByVal value As String)
                    Me._TituloPago = value
                End Set
            End Property

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
            Public Property TituloNa() As String
                Get
                    Return Me._TituloNa
                End Get
                Set(ByVal value As String)
                    Me._TituloNa = value
                End Set
            End Property
            Public Property TituloCantidad() As String
                Get
                    Return Me._TituloCantidad
                End Get
                Set(ByVal value As String)
                    Me._TituloCantidad = value
                End Set
            End Property
            Public Property TituloPub() As String
                Get
                    Return Me._TituloPub
                End Get
                Set(ByVal value As String)
                    Me._TituloPub = value
                End Set
            End Property
            Public Property TituloNDesc() As String
                Get
                    Return Me._TituloNDesc
                End Get
                Set(ByVal value As String)
                    Me._TituloNDesc = value
                End Set
            End Property

            Public Property TituloMMonto() As String
                Get
                    Return Me._TituloMMonto
                End Get
                Set(ByVal value As String)
                    Me._TituloMMonto = value
                End Set
            End Property

            Public Property XFacturaProforma() As String
                Get
                    Return Me._XFacturaProforma
                End Get
                Set(ByVal value As String)
                    Me._XFacturaProforma = value
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

            Public Property xins() As String
                Get
                    Return Me._xins
                End Get
                Set(ByVal value As String)
                    Me._xins = value
                End Set
            End Property

            Public Property xinstruc() As String
                Get
                    Return Me._xinstruc
                End Get
                Set(ByVal value As String)
                    Me._xinstruc = value
                End Set
            End Property

            Public Property Moneda2() As String
                Get
                    Return Me._Moneda2
                End Get
                Set(ByVal value As String)
                    Me._Moneda2 = value
                End Set
            End Property

            Public Property Mensajebsf() As String
                Get
                    Return Me._Mensajebsf
                End Get
                Set(ByVal value As String)
                    Me._Mensajebsf = value
                End Set
            End Property
        End Structure

        Structure StructReporteFacturaProformaDeta
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
