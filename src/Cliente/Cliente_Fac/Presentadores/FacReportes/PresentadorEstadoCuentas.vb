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
    Class PresentadorEstadoCuentas
        Inherits PresentadorBase

        Private _ventana As IEstadoCuentas                
        Dim _FacFacturaDetalle As List(Of FacFactuDetalle)

        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacOperacionServicios As IFacOperacionServicios
        Private _etiquetaServicios As IEtiquetaServicios
        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        Private _PaisServicios As IPaisServicios

        '
        Private _Tipo As String
        Private _Asociado As Asociado

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios

        Public Sub New(ByVal ventana As IEstadoCuentas, ByVal Tipo As String, ByVal Asociado As Asociado)
            Try
                Me._ventana = ventana

                _Tipo = Tipo
                If Tipo = "2" Then ' esto quiere decir que lo llamo desde las Pantallas de Asociados_Marcas_Patentes
                    _Asociado = Asociado

                End If
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                Me._etiquetaServicios = DirectCast(Activator.GetObject(GetType(IEtiquetaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("EtiquetaServicios")), IEtiquetaServicios)
                Me._PaisServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.EstadoCuentas(_Tipo, _Asociado))
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
                Me.ActualizarTituloVentanaPrincipal("Reporte Factura Digital", "Reporte")


                Me._ventana.FocoPredeterminado()

                If _Tipo = "2" Then  ' esto quiere decir que lo llamo desde las Pantallas de Asociados_Marcas_Patentes
                    Dim asociadoaux As New Asociado
                    Dim asociado As List(Of Asociado)
                    If _Asociado IsNot Nothing Then
                        asociadoaux.Id = _Asociado.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados = asociado
                        Me._ventana.Asociado = asociado(0)
                        Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre

                        Me._ventana.Asociados2 = asociado
                        Me._ventana.Asociado2 = asociado(0)
                        Me._ventana.NombreAsociado2 = asociado(0).Id & " - " & asociado(0).Nombre
                    End If
                End If

                Me._ventana.Fecha1 = CDate("01-01-1900")
                Me._ventana.Fecha2 = FormatDateTime(Date.Now, DateFormat.ShortDate)

                Dim paises As IList(Of Pais) = Me._PaisServicios.ConsultarTodos()
                Dim primerpais As New Pais()
                primerpais.Id = Integer.MinValue
                paises.Insert(0, primerpais)
                Me._ventana.Paises = paises

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


        '''''''''''''''''''''''''reportes''''''''''''''''''
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
                Dim ds As New DataSet()
                ds.Tables.Add(datosEnc)
                ds.Tables.Add(datosDeta)
                Dim reporte As New ReportDocument()
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
        Private Function GetRutaReporte() As String
            Dim retorno As String
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RpFacEstadoCuenta.rpt"
            'retorno = "C:\DG_2012_09_11\DG\src\Cliente\Cliente_Fac\Reportes\Carta1CR.rpt"
            Return retorno

        End Function


        Public Function etiquetas_texto(ByVal codigo As String) As String()
            Dim valor(2) As String

            Dim etiquetas As List(Of Etiqueta) = _etiquetaServicios.ConsultarTodos()
            Dim EtiquetaFiltrados As IEnumerable(Of Etiqueta) = etiquetas
            EtiquetaFiltrados = From e In EtiquetaFiltrados Where e.Id IsNot Nothing AndAlso e.Id.ToLower().Contains(codigo.ToLower())
            If EtiquetaFiltrados.Count > 0 Then
                valor(0) = EtiquetaFiltrados(0).Descripcion1
                valor(1) = EtiquetaFiltrados(0).Descripcion2
            Else
                valor(0) = ""
                valor(1) = ""
            End If
            Return (valor)

        End Function

        Public Sub lp_fecha_esc_n(ByVal dfecha As Date, ByRef cfecha As String, ByVal facoperacion As FacOperacion)
            Dim w_dia, w_mes, w_ano As Integer
            w_mes = Date.Now.Month
            w_dia = Date.Now.Day
            w_ano = Date.Now.Year
            cfecha = fecha(w_mes, w_dia, w_ano, facoperacion)
        End Sub

        Public Function fecha(ByVal mes As Integer, ByVal dia As Integer, ByVal anio As Integer, ByVal facoperacion As FacOperacion) As String
            Dim retorna As String = ""
            Select Case mes
                Case 1
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Enero " & dia & ", " & anio
                    Else
                        retorna = " January " & dia & ", " & anio
                    End If
                Case 2
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Febrero " & dia & ", " & anio
                    Else
                        retorna = " February " & dia & ", " & anio
                    End If
                Case 3
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Marzo " & dia & ", " & anio
                    Else
                        retorna = " March " & dia & ", " & anio
                    End If
                Case 4
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Abril " & dia & ", " & anio
                    Else
                        retorna = " April " & dia & ", " & anio
                    End If
                Case 5
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Mayo " & dia & ", " & anio
                    Else
                        retorna = " May " & dia & ", " & anio
                    End If
                Case 6
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Junio " & dia & ", " & anio
                    Else
                        retorna = " June " & dia & ", " & anio
                    End If
                Case 7
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Julio " & dia & ", " & anio
                    Else
                        retorna = " July " & dia & ", " & anio
                    End If
                Case 8
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Agosto " & dia & ", " & anio
                    Else
                        retorna = " August " & dia & ", " & anio
                    End If
                Case 9
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Septiembre " & dia & ", " & anio
                    Else
                        retorna = " September " & dia & ", " & anio
                    End If
                Case 10
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Octubre " & dia & ", " & anio
                    Else
                        retorna = " October " & dia & ", " & anio
                    End If
                Case 11
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Noviembre " & dia & ", " & anio
                    Else
                        retorna = " November " & dia & ", " & anio
                    End If
                Case 12
                    If facoperacion.Idioma.Id = "ES" Then
                        retorna = " Diciembre " & dia & ", " & anio
                    Else
                        retorna = " December " & dia & ", " & anio
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

        Public Function Buscar_Operacion() As List(Of FacOperacion)
            Try
                Dim operacionaux As New FacOperacion
                Dim valor As Boolean = False
                'para generar el FacOperacion
                Dim FacOperacionaux As New FacOperacion
                operacionaux.ValorQuery = ""
                If Me._ventana.Fecha1 IsNot Nothing And Me._ventana.Fecha1.ToString <> "" And Me._ventana.Fecha2 IsNot Nothing And Me._ventana.Fecha2.ToString <> "" Then
                    operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion between '" & Me._ventana.Fecha1 & "' and '" & Me._ventana.Fecha2 & "'"
                    valor = True
                Else
                    If Me._ventana.Fecha1 IsNot Nothing And Me._ventana.Fecha1.ToString <> "" Then
                        operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                        operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion ='" & Me._ventana.Fecha1 & "'"
                        valor = True
                    End If
                End If

                If Me._ventana.Asociado IsNot Nothing And Me._ventana.Asociado2 IsNot Nothing Then
                    If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue And DirectCast(Me._ventana.Asociado2, Asociado).Id > Integer.MinValue Then
                        Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                        Dim asociado2 As Asociado = DirectCast(Me._ventana.Asociado2, Asociado)

                        If valor = False Then
                            operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                            operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
                        Else
                            operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
                        End If
                        operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id>= " & asociado.Id & " and Asociado.Id<= " & asociado2.Id
                        valor = True
                    Else
                        If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                            Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            If valor = False Then
                                operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                                operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
                            Else
                                operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
                            End If
                            operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id= " & asociado.Id
                            valor = True
                        End If
                    End If
                Else
                    If Me._ventana.Asociado IsNot Nothing Then
                        If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                            Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                            If valor = False Then
                                operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                                operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
                            Else
                                operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
                            End If
                            operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id= " & asociado.Id
                            valor = True
                        End If
                    End If
                End If

                If valor = True Then
                    operacionaux.ValorQuery = operacionaux.ValorQuery & " order by Asociado.Id, o.FechaOperacion, o.CodigoOperacion desc "
                    operacionaux.Seleccion = True
                End If

                Dim operaciones As List(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
                If operaciones.Count >= 0 Then
                    Return (operaciones)
                Else
                    Return (Nothing)
                End If
            Catch ex As Exception
                'logger.Error(ex.Message)
                Return (Nothing)
            End Try
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

        Public Function ObtenerEstructuraEnc(ByVal operacion As FacOperacion) As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            Dim w_s As String = ""
            structura = inicializar_enc()
            Try

                If operacion.Asociado IsNot Nothing Then
                    If operacion.Asociado IsNot Nothing Then
                        structura.CodigoCliente1 = operacion.Asociado.Id
                        structura.Cliente1 = operacion.Asociado.Nombre
                        structura.Domicilio1 = operacion.Asociado.Domicilio
                        structura.Cliente1 = structura.Cliente1 & ControlChars.NewLine & structura.Domicilio1
                        If operacion.Asociado.Pais IsNot Nothing Then
                            structura.Pais1 = BuscarPais(Me._ventana.Paises, operacion.Asociado.Pais).NombreIngles
                            structura.Cliente1 = structura.Cliente1 & ControlChars.NewLine & structura.Pais1
                        End If
                        structura.Mail1 = operacion.Asociado.Email
                        structura.Cliente1 = structura.Cliente1 & ControlChars.NewLine & structura.Mail1
                    End If
                End If
                If Me._ventana.TipoMoneda = "Moneda Original" Then
                    structura.Moneda = operacion.Moneda.Id
                Else
                    structura.Moneda = "BsF"
                End If

                Dim fc As String = ""
                'If Me._ventana.Fecha1 IsNot Nothing And Me._ventana.Fecha1.ToString <> "" Then
                lp_fecha_esc_n(Date.Now, fc, operacion)
                'End If
                structura.Titulo1 = "STATEMENT OF ACCOUNT TO " & fc
                structura.Titulo2 = "ESTADO DE CUENTA HASTA " & fc

                'preguntar
                'linea = "Select replace(xdomicilio,chr(10),chr(32)) from fac_asociados where casociado = %%casociado.fac_operaciones"
                'Sql(linea, "ORA")

                If operacion.Asociado IsNot Nothing Then
                    If operacion.Asociado IsNot Nothing Then
                        structura.CodigoCliente2 = operacion.Asociado.Id
                        structura.Cliente2 = operacion.Asociado.Nombre
                        structura.Domicilio2 = operacion.Asociado.Domicilio
                        structura.Mail2 = operacion.Asociado.Email
                        If operacion.Asociado.Pais IsNot Nothing Then
                            structura.Pais2 = BuscarPais(Me._ventana.Paises, operacion.Asociado.Pais).NombreIngles
                        End If
                        'structura.Pais1 = operacion.Asociado.Pais.NombreIngles
                    End If
                End If
                'structura.Domicilio2 = operacion.Asociado.Domicilio

                'structura.Cliente2 = operacion.Asociado.Nombre
                'structura.CodigoCliente2 = operacion.Asociado.Id
                'structura.Mail2 = operacion.Asociado.Email
                ''structura.Pais2 = operacion.Asociado.Pais.NombreIngles

            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Private Sub ObtenerEstructura(ByRef enc As IList(Of StructReporteFActuraEnc), ByRef det As List(Of StructReporteFActuraDeta))

            Dim retorno As IList(Of StructReporteFActuraEnc) = New List(Of StructReporteFActuraEnc)
            Dim structura As New StructReporteFActuraEnc()
            Dim j As Integer = 0
            Dim montototal As Double = 0
            Dim crea As Boolean = False
            Try
                Dim facOperacion As List(Of FacOperacion) = Buscar_Operacion()
                For i As Integer = 0 To facOperacion.Count - 1
                    If i = 0 Then
                        'llamar encabezado
                        structura = ObtenerEstructuraEnc(facOperacion(i))
                        j = j + 1
                        structura.Id = j
                        crea = True
                    Else
                        If facOperacion(i).Asociado.Id <> facOperacion(i - 1).Asociado.Id Then
                            'llamar encabezado                           
                                structura.Mttotal = montototal
                                retorno.Add(structura)                            

                            structura = ObtenerEstructuraEnc(facOperacion(i))
                            j = j + 1
                            structura.Id = j
                            montototal = 0
                            crea = False
                            'retorno.Add(structura)
                            'ObtenerEstructuraDeta(j, det)
                            'Else
                            '    If facOperacion(i).Asociado.Id <> facOperacion(i - 1).Asociado.Id Then
                            '        crea = False
                            '    End If
                        End If
                    End If
                    ObtenerEstructuraDeta(j, det, facOperacion(i), montototal)
                    If i = facOperacion.Count - 1 Then
                        'llamar detalle
                    Else
                        If facOperacion(i).Asociado.Id <> facOperacion(i + 1).Asociado.Id Then
                            'llamar detalle
                        End If
                    End If
                Next
                If j = 1 And retorno.Count = 0 Then
                    structura.Mttotal = SetFormatoDouble2(montototal)
                    retorno.Add(structura)
                End If
                enc = retorno
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
        End Sub

        Private Sub ObtenerEstructuraDeta(ByVal id As String, ByVal detalle As List(Of StructReporteFActuraDeta), ByVal operacion As FacOperacion, ByRef montototal As Double)
            Dim retorno As IList(Of StructReporteFActuraDeta) = New List(Of StructReporteFActuraDeta)
            retorno = detalle
            Dim monto As Double = 0
            Dim structura As New StructReporteFActuraDeta()
            Try
                structura.Fecha = operacion.FechaOperacion
                If operacion.Id = "ND" Then
                    lp_compl(operacion.FechaOperacion, operacion.CodigoOperacion, structura.Nota)
                Else
                    structura.Nota = operacion.CodigoOperacion
                End If
                If Me._ventana.TipoMoneda = "Moneda Original" Then
                    structura.Moneda = operacion.Moneda.Id
                Else
                    structura.Moneda = "BsF"
                End If
                structura.Desc = operacion.XOperacion
                If operacion.Id = "ND" Then
                    monto = operacion.Monto
                End If

                If operacion.Id = "NC" Then
                    monto = operacion.Monto * -1
                End If

                If operacion.Id = "NP" Then
                    monto = operacion.Monto * -1
                End If
                structura.MMonto = SetFormatoDouble2(monto)
                montototal = montototal + monto
                If montototal < 1 And montototal > -1 Then
                    montototal = 0
                End If
                structura.Id = id
                retorno.Add(structura)
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try

            detalle = retorno
        End Sub

        Public Function inicializar_enc() As StructReporteFActuraEnc
            Dim structura As New StructReporteFActuraEnc()
            structura.Id = ""
            structura.Titulo1 = ""
            structura.Titulo2 = ""
            structura.CodigoCliente1 = ""
            structura.CodigoCliente2 = ""
            structura.Cliente1 = ""
            structura.Cliente2 = ""
            structura.Domicilio1 = ""
            structura.Domicilio2 = ""
            structura.Pais1 = ""
            structura.Pais2 = ""
            structura.Mail1 = ""
            structura.Mail2 = ""
            structura.Moneda = ""
            structura.Mttotal = ""
            Return (structura)
        End Function

        Public Function datosenc_colum() As DataTable
            Dim datosEnc2 As New DataTable("DataTable1")
            datosEnc2.Columns.Add("Id")
            datosEnc2.Columns.Add("Titulo1")
            datosEnc2.Columns.Add("Titulo2")
            datosEnc2.Columns.Add("CodigoCliente1")
            datosEnc2.Columns.Add("CodigoCliente2")
            datosEnc2.Columns.Add("Cliente1")
            datosEnc2.Columns.Add("Cliente2")
            datosEnc2.Columns.Add("Domicilio1")
            datosEnc2.Columns.Add("Domicilio2")
            datosEnc2.Columns.Add("Pais1")
            datosEnc2.Columns.Add("Pais2")
            datosEnc2.Columns.Add("Mail1")
            datosEnc2.Columns.Add("Mail2")
            datosEnc2.Columns.Add("Moneda")
            datosEnc2.Columns.Add("Mttotal")
            Return datosEnc2
        End Function

        Public Function datosdeta_colum() As DataTable
            Dim datosdeta2 As New DataTable("DataTable2")
            datosdeta2.Columns.Add("Id")
            datosdeta2.Columns.Add("Fecha")
            datosdeta2.Columns.Add("Nota")
            datosdeta2.Columns.Add("Desc")
            datosdeta2.Columns.Add("Moneda")
            datosdeta2.Columns.Add("MMonto")
            Return datosdeta2
        End Function

        Private Function ArmarReporteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraEnc In estructurasDeDatos
                    Dim filaDatos As DataRow = datos.NewRow()
                    filaDatos("Id") = structura.Id
                    filaDatos("Titulo1") = structura.Titulo1
                    filaDatos("Titulo2") = structura.Titulo2
                    filaDatos("CodigoCliente1") = structura.CodigoCliente1
                    filaDatos("CodigoCliente2") = structura.CodigoCliente2
                    filaDatos("Cliente1") = structura.Cliente1
                    filaDatos("Cliente2") = structura.Cliente2
                    filaDatos("Domicilio1") = structura.Domicilio1
                    filaDatos("Domicilio2") = structura.Domicilio2
                    filaDatos("Pais1") = structura.Pais1
                    filaDatos("Pais2") = structura.Pais2
                    filaDatos("Mail1") = structura.Mail1
                    filaDatos("Mail2") = structura.Mail2
                    filaDatos("Moneda") = structura.Moneda
                    filaDatos("Mttotal") = poner_decimal(structura.Mttotal)
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
                    filaDatos("Nota") = structura.Nota
                    filaDatos("Desc") = structura.Desc
                    filaDatos("Moneda") = structura.Moneda
                    filaDatos("MMonto") = poner_decimal(structura.MMonto)
                    datos.Rows.Add(filaDatos)
                Next
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return datos
        End Function

        Structure StructReporteFActuraEnc
            Private _Id As String
            Private _Titulo1 As String
            Private _Titulo2 As String
            Private _CodigoCliente1 As String
            Private _CodigoCliente2 As String
            Private _Cliente1 As String
            Private _Cliente2 As String
            Private _Domicilio1 As String
            Private _Domicilio2 As String
            Private _Pais1 As String
            Private _Pais2 As String
            Private _Mail1 As String
            Private _Mail2 As String
            Private _Moneda As String
            Private _Mttotal As String

            Public Property Id() As String
                Get
                    Return Me._Id
                End Get
                Set(ByVal value As String)
                    Me._Id = value
                End Set
            End Property

            Public Property Titulo1() As String
                Get
                    Return Me._Titulo1
                End Get
                Set(ByVal value As String)
                    Me._Titulo1 = value
                End Set
            End Property

            Public Property Titulo2() As String
                Get
                    Return Me._Titulo2
                End Get
                Set(ByVal value As String)
                    Me._Titulo2 = value
                End Set
            End Property

            Public Property CodigoCliente1() As String
                Get
                    Return Me._CodigoCliente1
                End Get
                Set(ByVal value As String)
                    Me._CodigoCliente1 = value
                End Set
            End Property


            Public Property CodigoCliente2() As String
                Get
                    Return Me._CodigoCliente2
                End Get
                Set(ByVal value As String)
                    Me._CodigoCliente2 = value
                End Set
            End Property

            Public Property Cliente1() As String
                Get
                    Return Me._Cliente1
                End Get
                Set(ByVal value As String)
                    Me._Cliente1 = value
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

            Public Property Domicilio1() As String
                Get
                    Return Me._Domicilio1
                End Get
                Set(ByVal value As String)
                    Me._Domicilio1 = value
                End Set
            End Property

            Public Property Domicilio2() As String
                Get
                    Return Me._Domicilio2
                End Get
                Set(ByVal value As String)
                    Me._Domicilio2 = value
                End Set
            End Property

            Public Property Pais1() As String
                Get
                    Return Me._Pais1
                End Get
                Set(ByVal value As String)
                    Me._Pais1 = value
                End Set
            End Property

            Public Property Pais2() As String
                Get
                    Return Me._Pais2
                End Get
                Set(ByVal value As String)
                    Me._Pais2 = value
                End Set
            End Property

            Public Property Mail1() As String
                Get
                    Return Me._Mail1
                End Get
                Set(ByVal value As String)
                    Me._Mail1 = value
                End Set
            End Property


            Public Property Mail2() As String
                Get
                    Return Me._Mail2
                End Get
                Set(ByVal value As String)
                    Me._Mail2 = value
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
        End Structure

        Structure StructReporteFActuraDeta
            Private _Id As String
            Private _Fecha As String
            Private _Nota As String
            Private _Desc As String
            Private _Moneda As String
            Private _MMonto As String

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

            Public Property Nota() As String
                Get
                    Return Me._Nota
                End Get
                Set(ByVal value As String)
                    Me._Nota = value
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
