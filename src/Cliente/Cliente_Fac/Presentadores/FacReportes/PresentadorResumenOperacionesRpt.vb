Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
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
    Class PresentadorResumenOperacionesRpt
        Inherits PresentadorBase

        Private _ventana As IResumenOperacionesRpt

        Private _FacOperacionServicios As IFacOperacionServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _MonedaServicios As IMonedaServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()        

        Public Sub New(ByVal ventana As IResumenOperacionesRpt)
            Try
                Me._ventana = ventana
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)                
                Me._MonedaServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.ResumenOperacionesRpt())
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

                Dim iniciomes As String = "01/" & Date.Now.Month & "/" & Date.Now.Year
                Me._ventana.FechaInicio = FormatDateTime(CDate(iniciomes), DateFormat.ShortDate)
                Me._ventana.FechaFin = FormatDateTime(Date.Now, DateFormat.ShortDate)

                Me._ventana.FocoPredeterminado()

                'Me._ventana.TipoMonedas = _MonedaServicios.ConsultarTodos

                Dim monedas As IList(Of Moneda) = _MonedaServicios.ConsultarTodos
                Me._ventana.Monedas = monedas
                Me._ventana.Moneda = monedas(2)

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
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemResumenOperacionesRpt, Recursos.Ids.fac_menuItemResumenOperacionesRpt)
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

        '''''''''''''''''''''''''reportes''''''''''''''''''
        Public Sub Reporte()
            Mouse.OverrideCursor = Cursors.Wait
            Try
                Dim estructuraDeDatosEnc As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)()

                Dim estructuraDeDatosDeta As IList(Of StructReporteFActuraDet) = New List(Of StructReporteFActuraDet)()
                'reporte.Load();                
                Dim datosEnc As New DataTable("DataTable1")
                Dim datosDeta As New DataTable("DataTable2")
                datosEnc = datosenc_colum()
                datosDeta = datosdet_colum()
                'datosDeta = datosdeta_colum()
                'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                'estructuraDeDatos.Add(estructura)

                ObtenerEstructura(estructuraDeDatosEnc, estructuraDeDatosDeta)
                'estructuraDeDatosEnc = ObtenerEstructuraEnc()

                'estructuraDeDatosDeta = ObtenerEstructuraDeta()


                datosEnc = ArmarReporteEnc(datosEnc, estructuraDeDatosEnc)
                datosDeta = ArmarReporteDet(datosDeta, estructuraDeDatosDeta)

                Dim reporte As New ReportDocument()
                'datosDeta = ArmarReporteDeta(datosDeta, estructuraDeDatosDeta)
                'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
                Dim ds As New DataSet()
                ds.Tables.Add(datosEnc)
                ds.Tables.Add(datosDeta)
                reporte.Load(GetRutaReporte())
                reporte.SetDataSource(ds)
                'reporte.SetDataSource(datosDeta)

                'agregar cuando este el la maquina virtual
                Mouse.OverrideCursor = Nothing
                IrConsultarReporte(reporte)

                'Me._ventana.CrystalViewer = reporte

                'reporte.PrintToPrinter(1, False, 1, 0)                

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

        Public Function busca_asociado_reporte() As List(Of Asociado)
            Dim asociadoaux As New Asociado
            'para generar el Facasociado
            Dim Facasociadoaux As New Asociado
            asociadoaux.ValorQuery = ""
            If Me._ventana.Asociado IsNot Nothing Then
                If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)                    
                    asociadoaux.ValorQuery = asociadoaux.ValorQuery & " a.Id>= " & asociado.Id & " and  a.Id<= " & asociado.Id & " order by a.Nombre"
                End If
            End If
            'asociadoaux.EdoCuenta = "T"
            Dim asociados As List(Of Asociado)
            If asociadoaux.ValorQuery <> "" Then
                asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Else
                asociados = Me._asociadosServicios.ConsultarTodos()
            End If
            If asociados.Count > 0 Then
                Return (asociados)
            Else
                Return (Nothing)
            End If
        End Function

        Private Function GetRutaReporte() As String
            Dim retorno As String
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "ResumenOperacionesRpt.rpt"
            'retorno = "C:\DG_2012_09_11\DG\src\Cliente\Cliente_Fac\Reportes\Carta1CR.rpt"
            Return retorno

        End Function

        Public Function Buscar_Operacion() As List(Of FacOperacion)
            Dim operacionaux As New FacOperacion
            Dim valor As Boolean = False
            'para generar el FacOperacion
            Dim FacOperacionaux As New FacOperacion
            operacionaux.ValorQuery = ""
            If Me._ventana.FechaInicio IsNot Nothing And Me._ventana.FechaInicio.ToString <> "" And Me._ventana.FechaFin IsNot Nothing And Me._ventana.FechaFin.ToString <> "" Then
                operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion between '" & Me._ventana.FechaInicio & "' and '" & Me._ventana.FechaFin & "'"
                valor = True
            Else
                If Me._ventana.FechaInicio IsNot Nothing And Me._ventana.FechaInicio.ToString <> "" Then
                    operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion between '" & Me._ventana.FechaInicio & "' and '" & Me._ventana.FechaInicio & "'"
                    valor = True
                End If
            End If

            If Me._ventana.Asociado IsNot Nothing Then
                If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                    If valor = False Then
                        operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                        operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
                    Else
                        operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
                    End If
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id= " & Asociado.Id
                    valor = True
                End If
            End If

            If Me._ventana.Moneda IsNot Nothing Then
                Dim TipoMoneda As Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                If valor = False Then
                    operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
                Else
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
                End If
                operacionaux.ValorQuery = operacionaux.ValorQuery & " Moneda.Id= '" & TipoMoneda.Id & "'"
                valor = True
            End If

            If valor = True Then
                operacionaux.ValorQuery = operacionaux.ValorQuery & " order by Asociado.Nombre, o.FechaOperacion "
                operacionaux.Seleccion = True
            End If

            Dim operaciones As List(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
            If operaciones.Count >= 0 Then
                Return (operaciones)
            Else
                Return (Nothing)
            End If
        End Function

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFActuraEnc), ByRef det As IList(Of StructReporteFActuraDet))

            Dim retorno As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)
            Dim retornodet As IList(Of StructReporteFActuraDet) = New List(Of StructReporteFActuraDet)
            Dim structura As New StructReporteFActuraEnc()
            Dim structuradet As New StructReporteFActuraDet()

            Dim acmonto_pend, acmonto_cob, acmonto_cred, acmonto_pend_t, acmonto_cob_t, acmonto_cred_t As Double
            acmonto_pend = 0
            acmonto_cob = 0
            acmonto_cred = 0
            acmonto_pend_t = 0
            acmonto_cob_t = 0
            acmonto_cred_t = 0

            Dim crea As Boolean = False
            Try
                Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                'Dim asociados As List(Of Asociado) = busca_asociado_reporte()
                'If asociados IsNot Nothing Then
                'For j As Integer = 0 To asociados.Count - 1
                acmonto_pend = 0
                acmonto_cob = 0
                acmonto_cred = 0
                Dim facOperacion As List(Of FacOperacion) = Buscar_Operacion()
                If facOperacion IsNot Nothing Then
                    If facOperacion.Count > 0 Then
                        For i As Integer = 0 To facOperacion.Count - 1
                            If i = 0 Then

                            Else
                                If facOperacion(i).Asociado.Id <> facOperacion(i - 1).Asociado.Id Then
                                    structuradet.Ccliente = facOperacion(i - 1).Asociado.Id
                                    structuradet.Xcliente = facOperacion(i - 1).Asociado.Nombre
                                    structuradet.Mcob = FormatNumber(acmonto_cob, 2)
                                    structuradet.Mfac = FormatNumber(acmonto_pend, 2)
                                    structuradet.Mcr = FormatNumber(acmonto_cred, 2)
                                    acmonto_cob_t = acmonto_cob_t + acmonto_cob
                                    acmonto_pend_t = acmonto_pend_t + acmonto_pend
                                    acmonto_cred_t = acmonto_cred_t + acmonto_cred
                                    structuradet.Id = "1"
                                    retornodet.Add(structuradet)

                                    acmonto_pend = 0
                                    acmonto_cob = 0
                                    acmonto_cred = 0

                                End If
                            End If

                            If facOperacion(i).Moneda.Id = "US" Then
                                If facOperacion(i).Id = "ND" Then
                                    acmonto_pend = acmonto_pend + facOperacion(i).Monto
                                Else
                                    If facOperacion(i).Id = "NP" Then
                                        acmonto_cob = acmonto_cob + facOperacion(i).Monto
                                    Else
                                        If facOperacion(i).Id = "NC" Then
                                            acmonto_cred = acmonto_cred + facOperacion(i).Monto
                                        End If
                                    End If
                                End If
                            Else
                                If facOperacion(i).Id = "ND" Then
                                    acmonto_pend = acmonto_pend + facOperacion(i).MontoBf
                                Else
                                    If facOperacion(i).Id = "NP" Then
                                        acmonto_cob = acmonto_cob + facOperacion(i).MontoBf
                                    Else
                                        If facOperacion(i).Id = "NC" Then
                                            acmonto_cred = acmonto_cred + facOperacion(i).MontoBf
                                        End If
                                    End If
                                End If
                            End If


                            If i = facOperacion.Count - 1 Then

                                structuradet.Ccliente = facOperacion(i).Asociado.Id
                                structuradet.Xcliente = facOperacion(i).Asociado.Nombre
                                structuradet.Mcob = FormatNumber(acmonto_cob, 2)
                                structuradet.Mfac = FormatNumber(acmonto_pend, 2)
                                structuradet.Mcr = FormatNumber(acmonto_cred, 2)
                                acmonto_cob_t = acmonto_cob_t + acmonto_cob
                                acmonto_pend_t = acmonto_pend_t + acmonto_pend
                                acmonto_cred_t = acmonto_cred_t + acmonto_cred
                                structuradet.Id = "1"
                                retornodet.Add(structuradet)

                                acmonto_pend = 0
                                acmonto_cob = 0
                                acmonto_cred = 0

                            End If

                        Next

                    End If
                End If
                ' Next
                structura.Mcob = FormatNumber(acmonto_cob_t, 2)
                structura.Mfac = FormatNumber(acmonto_pend_t, 2)
                structura.Mcr = FormatNumber(acmonto_cred_t, 2)
                structura.Mcob_t = FormatNumber(acmonto_cob_t, 2)
                structura.Mfac_t = FormatNumber(acmonto_pend_t, 2)
                structura.Mcr_t = FormatNumber(acmonto_cred_t, 2)
                'If asociado IsNot Nothing Then
                '    If asociado.Id > Integer.MinValue Then
                '        structura.Ccliente = asociado.Id
                '        structura.Xcliente = asociado.Nombre
                '    End If
                'End If
                structura.Id = "1"
                structura.Xtitu1 = "RESUMEN DE OPERACIONES"
                structura.Xtitu2 = "DEL " & Me._ventana.FechaInicio & " AL " & Me._ventana.FechaFin
                structura.Xtitu2 = Replace(structura.Xtitu2, "/", "-")
                structura.Xtitu3 = "Moneda " & Me._ventana.Moneda.id

                retorno.Add(structura)
                enc = retorno
                det = retornodet
                'End If
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
        End Sub

        Public Function inicializar_enc() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            structura.Xtitu1 = ""
            structura.Xtitu2 = ""
            structura.Xtitu3 = ""
            structura.Cpagina = ""
            structura.Ccliente = ""
            structura.Xcliente = ""
            structura.Mfac = ""
            structura.Mcob = ""
            structura.Mcr = ""
            structura.Mfac_t = ""
            structura.Mcob_t = ""
            structura.Mcr_t = ""
            structura.Id = ""
            Return (structura)
        End Function

        Public Function inicializar_det() As StructReporteFActuraDet
            Dim structura As New StructReporteFActuraDet()
            structura.Ccliente = ""
            structura.Xcliente = ""
            structura.Mfac = ""
            structura.Mcob = ""
            structura.Mcr = ""
            structura.Id = ""
            Return (structura)
        End Function

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("Xtitu1")
            datosEnc2.Columns.Add("Xtitu2")
            datosEnc2.Columns.Add("Xtitu3")
            datosEnc2.Columns.Add("Cpagina")
            datosEnc2.Columns.Add("Ccliente")
            datosEnc2.Columns.Add("Xcliente")
            datosEnc2.Columns.Add("Mfac")
            datosEnc2.Columns.Add("Mcob")
            datosEnc2.Columns.Add("Mcr")
            datosEnc2.Columns.Add("Mfac_t")
            datosEnc2.Columns.Add("Mcob_t")
            datosEnc2.Columns.Add("Mcr_t")
            datosEnc2.Columns.Add("Id")
            Return datosEnc2
        End Function

        Public Function datosdet_colum() As DataTable
            Dim datosDet2 As New DataTable("DataTable2")
            datosDet2.Columns.Add("Ccliente")
            datosDet2.Columns.Add("Xcliente")
            datosDet2.Columns.Add("Mfac")
            datosDet2.Columns.Add("Mcob")
            datosDet2.Columns.Add("Mcr")
            datosDet2.Columns.Add("Id")
            Return datosDet2
        End Function

        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Xtitu1") = structura.Xtitu1
                    filaDatos("Xtitu2") = structura.Xtitu2
                    filaDatos("Xtitu3") = structura.Xtitu3
                    filaDatos("Cpagina") = structura.Cpagina
                    filaDatos("Ccliente") = structura.Ccliente
                    filaDatos("Xcliente") = structura.Xcliente
                    filaDatos("Mfac") = poner_decimal(structura.Mfac)
                    filaDatos("Mcob") = poner_decimal(structura.Mcob)
                    filaDatos("Mcr") = poner_decimal(structura.Mcr)
                    filaDatos("Mfac_t") = poner_decimal(structura.Mfac_t)
                    filaDatos("Mcob_t") = poner_decimal(structura.Mcob_t)
                    filaDatos("Mcr_t") = poner_decimal(structura.Mcr_t)
                    filaDatos("Id") = structura.Id
                    datos.Rows.Add(filaDatos)

                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Private Function ArmarReporteDet(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraDet)) As DataTable
            Try
                For Each structura As StructReporteFActuraDet In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Ccliente") = structura.Ccliente
                    filaDatos("Xcliente") = structura.Xcliente
                    filaDatos("Mfac") = poner_decimal(structura.Mfac)
                    filaDatos("Mcob") = poner_decimal(structura.Mcob)
                    filaDatos("Mcr") = poner_decimal(structura.Mcr)
                    filaDatos("Id") = structura.Id
                    datos.Rows.Add(filaDatos)

                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Structure StructReporteFActuraEnc            
            Private _Xtitu1 As String
            Private _Xtitu2 As String
            Private _Xtitu3 As String
            Private _Cpagina As String
            Private _Ccliente As String
            Private _Xcliente As String
            Private _Mfac As String
            Private _Mcob As String
            Private _Mcr As String
            Private _Mfac_t As String
            Private _Mcob_t As String
            Private _Mcr_t As String
            Private _Id As String

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

            Public Property Xtitu3() As String
                Get
                    Return Me._Xtitu3
                End Get
                Set(ByVal value As String)
                    Me._Xtitu3 = value
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

            Public Property Ccliente() As String
                Get
                    Return Me._Ccliente
                End Get
                Set(ByVal value As String)
                    Me._Ccliente = value
                End Set
            End Property

            Public Property Xcliente() As String
                Get
                    Return Me._Xcliente
                End Get
                Set(ByVal value As String)
                    Me._Xcliente = value
                End Set
            End Property

            Public Property Mfac() As String
                Get
                    Return Me._Mfac
                End Get
                Set(ByVal value As String)
                    Me._Mfac = value
                End Set
            End Property

            Public Property Mcob() As String
                Get
                    Return Me._Mcob
                End Get
                Set(ByVal value As String)
                    Me._Mcob = value
                End Set
            End Property

            Public Property Mcr() As String
                Get
                    Return Me._Mcr
                End Get
                Set(ByVal value As String)
                    Me._Mcr = value
                End Set
            End Property

            Public Property Mfac_t() As String
                Get
                    Return Me._Mfac_t
                End Get
                Set(ByVal value As String)
                    Me._Mfac_t = value
                End Set
            End Property

            Public Property Mcob_t() As String
                Get
                    Return Me._Mcob_t
                End Get
                Set(ByVal value As String)
                    Me._Mcob_t = value
                End Set
            End Property

            Public Property Mcr_t() As String
                Get
                    Return Me._Mcr_t
                End Get
                Set(ByVal value As String)
                    Me._Mcr_t = value
                End Set
            End Property

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
                End Set
            End Property
        End Structure

        Structure StructReporteFActuraDet

            Private _Ccliente As String
            Private _Xcliente As String
            Private _Mfac As String
            Private _Mcob As String
            Private _Mcr As String
            Private _Id As String

            Public Property Ccliente() As String
                Get
                    Return Me._Ccliente
                End Get
                Set(ByVal value As String)
                    Me._Ccliente = value
                End Set
            End Property

            Public Property Xcliente() As String
                Get
                    Return Me._Xcliente
                End Get
                Set(ByVal value As String)
                    Me._Xcliente = value
                End Set
            End Property

            Public Property Mfac() As String
                Get
                    Return Me._Mfac
                End Get
                Set(ByVal value As String)
                    Me._Mfac = value
                End Set
            End Property

            Public Property Mcob() As String
                Get
                    Return Me._Mcob
                End Get
                Set(ByVal value As String)
                    Me._Mcob = value
                End Set
            End Property

            Public Property Mcr() As String
                Get
                    Return Me._Mcr
                End Get
                Set(ByVal value As String)
                    Me._Mcr = value
                End Set
            End Property

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
                End Set
            End Property
        End Structure

    End Class
End Namespace
