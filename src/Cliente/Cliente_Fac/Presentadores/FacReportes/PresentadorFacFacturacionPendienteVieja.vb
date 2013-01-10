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
    Class PresentadorFacFacturacionPendienteVieja
        Inherits PresentadorBase

        Private _ventana As IFacFacturacionPendienteVieja

        Dim _FacFacturaDetalle As List(Of FacFactuDetalle)
        Private _FacFacturaPendiente As List(Of FacFacturaPendiente)

        Private _FacFacturaServicios As IFacFacturaServicios
        Private _FacFacturaPendienteServicios As IFacFacturaPendienteServicios

        Private _FacOperacionServicios As IFacOperacionServicios
        Private _FacOperacionpaisServicios As IFacOperacionPaisServicios
        Private _etiquetaServicios As IEtiquetaServicios
        Private _detalleenviosServicios As IFacDetalleEnvioServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios
        Private _FacCobroFacturas As IFacCobroFacturaServicios
        Private _PaisServicios As IPaisServicios
        Private _usuarioServicios As IUsuarioServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios

        Public Sub New(ByVal ventana As IFacFacturacionPendienteVieja)
            Try
                Me._ventana = ventana
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._FacOperacionServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._FacOperacionpaisServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionPaisServicios")), IFacOperacionPaisServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._FacFacturaPendienteServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaPendienteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaPendienteServicios")), IFacFacturaPendienteServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
                Me._etiquetaServicios = DirectCast(Activator.GetObject(GetType(IEtiquetaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("EtiquetaServicios")), IEtiquetaServicios)
                Me._FacCobroFacturas = DirectCast(Activator.GetObject(GetType(IFacCobroFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCobroFacturaServicios")), IFacCobroFacturaServicios)
                Me._PaisServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                Me._usuarioServicios = DirectCast(Activator.GetObject(GetType(IUsuarioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("usuarioServicios")), IUsuarioServicios)

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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacFacturacionPendienteVieja())
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

                Me._ventana.FechaCorte = FormatDateTime(Date.Now, DateFormat.ShortDate)               

                Me._ventana.FocoPredeterminado()



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
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemFacFacturacionPendienteVieja, Recursos.Ids.fac_menuItemFacFacturacionPendienteVieja)
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
                ' Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    If Me._ventana.Asociado.id <> Integer.MinValue Then
                        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
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
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
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

                _FacFacturaPendiente = Buscar_FacturaPendiente()


                Dim estructuraDeDatosEnc As IList(Of StructReporteFActuraPendienteEnc) = New List(Of StructReporteFActuraPendienteEnc)()

                Dim estructuraDeDatosDeta As IList(Of StructReporteFActuraPendienteDeta) = New List(Of StructReporteFActuraPendienteDeta)()
                'reporte.Load();                
                Dim datosEnc As New DataTable("DataTable1")
                Dim datosDeta As New DataTable("DataTable2")
                datosEnc = datosPendienteenc_colum()

                datosDeta = datosPendientedeta_colum()
                'Dim estructura As StructReporteCarta1 = ObtenerEstructuraCarta1()
                'estructuraDeDatos.Add(estructura)

                ObtenerEstructuraPendiente(estructuraDeDatosEnc, estructuraDeDatosDeta)
                If estructuraDeDatosEnc.Count > 0 Then
                    Dim reportePendiente As New ReportDocument()
                    'estructuraDeDatosEnc = ObtenerEstructuraEnc()

                    'estructuraDeDatosDeta = ObtenerEstructuraDeta()


                    datosEnc = ArmarReportePendienteEnc(datosEnc, estructuraDeDatosEnc)

                    datosDeta = ArmarReportePendienteDeta(datosDeta, estructuraDeDatosDeta)
                    'reporte.PrintOptions.PrinterName = ConfigurationManager.AppSettings["ImpresoraReportes"];
                    Dim ds As New DataSet()
                    ds.Tables.Add(datosEnc)
                    ds.Tables.Add(datosDeta)
                    reportePendiente.Load(GetRutaReportePendiente())
                    reportePendiente.SetDataSource(ds)

                    IrConsultarReporte(reportePendiente)
                End If
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
            w_mes = Date.Now.Month
            w_dia = Date.Now.Day
            w_ano = Date.Now.Year
            cfecha = fecha(w_mes, w_dia, w_ano, idioma)
        End Sub

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

        Public Function Buscar_Operacion(ByVal nc As String, ByVal idasociado As Integer) As List(Of FacOperacion)
            Dim operacionaux As New FacOperacion
            Dim valor As Boolean = False
            'para generar el FacOperacion
            Dim FacOperacionaux As New FacOperacion
            operacionaux.ValorQuery = ""
            'If Me._ventana.Fecha1 IsNot Nothing And Me._ventana.Fecha1.ToString <> "" And Me._ventana.Fecha2 IsNot Nothing And Me._ventana.Fecha2.ToString <> "" Then
            '    operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
            '    operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion between '" & Me._ventana.Fecha1 & "' and '" & Me._ventana.Fecha1 & "'"
            '    valor = True
            'Else
            If Me._ventana.FechaCorte IsNot Nothing And Me._ventana.FechaCorte.ToString <> "" Then
                operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                operacionaux.ValorQuery = operacionaux.ValorQuery & " where o.FechaOperacion <='" & Me._ventana.FechaCorte & "'"
                valor = True
            End If
            'End If

            'If Me._ventana.Asociado IsNot Nothing And Me._ventana.Asociado2 IsNot Nothing Then
            '    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            '    Dim asociado2 As Asociado = DirectCast(Me._ventana.Asociado2, Asociado)

            '    If valor = False Then
            '        operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
            '        operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
            '    Else
            '        operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
            '    End If
            '    operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id>= " & asociado.Id & " and Asociado.Id<= " & asociado2.Id
            '    valor = True
            'Else
            '    If Me._ventana.Asociado IsNot Nothing Then
            '        Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            If valor = False Then
                operacionaux.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                operacionaux.ValorQuery = operacionaux.ValorQuery & " where "
            Else
                operacionaux.ValorQuery = operacionaux.ValorQuery & " and "
            End If
            operacionaux.ValorQuery = operacionaux.ValorQuery & " Asociado.Id= " & idasociado
            valor = True
            '    End If
            'End If

            If valor = True Then
                operacionaux.ValorQuery = operacionaux.ValorQuery & "and o.Saldo > 0 and o.Id='" & nc & "'  order by Asociado.Id, o.FechaOperacion "
                operacionaux.Seleccion = True
            End If

            Dim operaciones As List(Of FacOperacion) = _FacOperacionServicios.ObtenerFacOperacionesFiltro(operacionaux)
            If operaciones.Count >= 0 Then
                Return (operaciones)
            Else
                Return (Nothing)
            End If
        End Function

        Public Function consultar_cobro_factura(ByVal factura As Integer) As Boolean
            Dim faccobrofacturaAuxiliar As New FacCobroFactura()
            Dim FaccobroFacturas As List(Of FacCobroFactura)
            faccobrofacturaAuxiliar.Factura = factura

            FaccobroFacturas = Me._FacCobroFacturas.ObtenerFacCobroFacturasFiltro(faccobrofacturaAuxiliar)
            If FaccobroFacturas IsNot Nothing Then
                If FaccobroFacturas.Count > 0 Then
                    Return (True)
                Else
                    Return (False)
                End If
            Else
                Return (False)
            End If
        End Function

        Public Function sumar_abono(ByVal idasociado As Integer) As Double
            Dim monto As Double = 0
            Dim facOperacion As List(Of FacOperacion) = Buscar_Operacion("NC", idasociado)
            For i As Integer = 0 To facOperacion.Count - 1
                monto = monto + (facOperacion(i).Saldo * -1)
            Next
            Return (monto)
        End Function


        '''''''''''''''reportes  facturas pendientes
        Private Function GetRutaReportePendiente() As String
            Dim retorno As String
            retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "FacturasPendientesAsociados.rpt"
            'retorno = Environment.CurrentDirectory & ConfigurationManager.AppSettings("rutaFacreportes") & "RptFactura.rpt"    

            Return retorno

        End Function

        Public Function etiquetas_textoPendiente(ByVal codigo As String) As String()
            Dim valor(2) As String

            Dim etiquetas As List(Of Etiqueta) = _etiquetaServicios.ConsultarTodos()
            Dim EtiquetaFiltrados As IEnumerable(Of Etiqueta) = etiquetas
            EtiquetaFiltrados = From e In EtiquetaFiltrados Where e.Id IsNot Nothing AndAlso e.Id.ToLower().Contains(codigo.ToLower())
            valor(0) = EtiquetaFiltrados(0).Descripcion1
            valor(1) = EtiquetaFiltrados(0).Descripcion2
            Return (valor)

        End Function

        Public Sub lp_fecha_esc_nPendiente(ByVal dfecha As Date, ByRef cfecha As String, ByVal idioma As String)
            Dim w_dia, w_mes, w_ano As Integer
            w_mes = dfecha.Month
            w_dia = dfecha.Day
            w_ano = dfecha.Year
            cfecha = fecha(w_mes, w_dia, w_ano, idioma)
        End Sub

        Public Function fechaPendiente(ByVal mes As Integer, ByVal dia As Integer, ByVal anio As Integer, ByVal idioma As String) As String
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

        Public Sub lp_complPendiente(ByVal dfecha As DateTime, ByVal nfac As Integer, ByRef csalida As String)
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

        Public Function encontrar_OperacionPendiente(ByVal cfactura As Integer) As Boolean
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

        Public Function Buscar_FacturaPendiente() As List(Of FacFacturaPendiente)
            Dim facturapendienteaux As New FacFacturaPendiente
            Dim valor As Boolean = False
            'para generar el FacFacturaPendiente
            Dim FacFacturaPendienteaux As New FacFacturaPendiente
            facturapendienteaux.ValorQuery = ""
            'If Me._ventana.Fecha1 IsNot Nothing And Me._ventana.Fecha1.ToString <> "" And Me._ventana.Fecha2 IsNot Nothing And Me._ventana.Fecha2.ToString <> "" Then
            '    facturapendienteaux.ValorQuery = "Select o from FacFacturaPendiente o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
            '    facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " where o.Fechafacturapendiente between '" & Me._ventana.Fecha1 & "' and '" & Me._ventana.Fecha1 & "'"
            '    valor = True
            'Else
            If Me._ventana.FechaCorte IsNot Nothing And Me._ventana.FechaCorte.ToString <> "" Then
                'facturapendienteaux.ValorQuery = "Select o from FacFacturaPendiente o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " fp.FechaOperacion <= '" & Me._ventana.FechaCorte & "'"
                valor = True
            End If
            'End If

            If Me._ventana.Asociado IsNot Nothing And Me._ventana.Asociado2 IsNot Nothing Then
                If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue And DirectCast(Me._ventana.Asociado2, Asociado).Id > Integer.MinValue Then
                    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                    Dim asociado2 As Asociado = DirectCast(Me._ventana.Asociado2, Asociado)
                    If valor = True Then
                        facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " and "
                    End If
                    facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " asociado.Id>= " & asociado.Id & " and asociado.Id<= " & asociado2.Id
                    valor = True
                Else
                    If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                        Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                        If valor = True Then
                            facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " and "
                        End If
                        facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " asociado.Id= " & asociado.Id
                        valor = True
                    End If
                End If
            Else

                If Me._ventana.Asociado IsNot Nothing Then
                    If DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                        Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                        If valor = True Then
                            facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " and "
                        End If
                        facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & " asociado.Id= " & asociado.Id
                        valor = True
                    End If
                End If                
                End If


                If valor = True Then
                    facturapendienteaux.ValorQuery = facturapendienteaux.ValorQuery & "  order by pais.Id,fp.FechaFactura"
                    'facturapendienteaux.Seleccion = True
                End If

                Dim facturapendientees As List(Of FacFacturaPendiente) = _FacFacturaPendienteServicios.ObtenerFacFacturaPendientesFiltro(facturapendienteaux)
                If facturapendientees.Count >= 0 Then
                    Return (facturapendientees)
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

        Public Function ObtenerEstructuraPendienteEnc(ByVal fac_pendiente As FacFacturaPendiente, ByVal monto As Double) As StructReporteFActuraPendienteEnc
            Dim structura As New StructReporteFActuraPendienteEnc()
            Dim w_s As String = ""
            structura = inicializar_Pendienteenc()
            Try

                If fac_pendiente.Caso IsNot Nothing Then
                    structura.Caso = fac_pendiente.Caso
                Else
                    structura.Caso = ""
                End If

                If fac_pendiente.XAsociado IsNot Nothing Then
                    structura.Cliente = fac_pendiente.XAsociado
                Else
                    structura.Cliente = ""
                End If                
                If fac_pendiente.Rif IsNot Nothing Then
                    structura.Rif = fac_pendiente.Rif
                Else
                    structura.Rif = ""
                End If
                If fac_pendiente.XNit IsNot Nothing Then
                    structura.Nit = fac_pendiente.XNit
                Else
                    structura.Nit = ""
                End If

                If structura.Cliente <> "" Then
                    Select Case fac_pendiente.Terrero
                        Case "1" '  
                            If fac_pendiente.FechaSeniat IsNot Nothing Then
                                lp_compl(fac_pendiente.FechaSeniat, fac_pendiente.Seniat, w_s)
                                structura.Invoice = w_s
                            Else
                                structura.Invoice = ""
                            End If
                            If fac_pendiente.Asociado.BPendienteStatement = True Then
                                If fac_pendiente.Asociado.TipoPersona.ToString = "T" Then
                                    structura.Tipo = "FACTURA  N°"
                                Else
                                    structura.Tipo = "STATEMENT  N°"
                                End If
                            Else
                                structura.Tipo = "FACTURA  N°"
                            End If
                            structura.Seniat = w_s
                            If fac_pendiente.FechaSeniat IsNot Nothing Then
                                lp_compl(fac_pendiente.FechaSeniat, fac_pendiente.Seniat, w_s)
                                structura.Fecha = w_s
                            Else
                                structura.Fecha = ""
                            End If
                        Case "2"
                            structura.Invoice = ""
                            If fac_pendiente.FechaSeniat IsNot Nothing Then
                                lp_compl(fac_pendiente.FechaSeniat, fac_pendiente.Seniat, w_s)
                                structura.Invoice = w_s
                                structura.Xfactura = ""
                            End If
                            If fac_pendiente.FechaFactura IsNot Nothing Then
                                lp_compl(fac_pendiente.FechaFactura, fac_pendiente.Id, w_s)
                                structura.Seniat = w_s
                                lp_fecha_esc_n(fac_pendiente.FechaFactura, structura.Fecha, fac_pendiente.Idioma.Id)
                            Else
                                structura.Fecha = ""
                            End If

                            If fac_pendiente.Asociado.BPendienteStatement = True Then
                                If fac_pendiente.FechaFactura IsNot Nothing Then
                                    lp_compl(fac_pendiente.FechaFactura, fac_pendiente.Id, w_s)
                                    structura.Invoice = w_s
                                    structura.Seniat = w_s
                                    structura.Xfactura = ""
                                    lp_fecha_esc_n(fac_pendiente.FechaFactura, structura.Fecha, fac_pendiente.Idioma.Id)
                                Else
                                    structura.Fecha = ""
                                End If
                                structura.Tipo = "STATEMENT  N°"
                            Else
                                If fac_pendiente.FechaSeniat IsNot Nothing Then
                                    lp_compl(fac_pendiente.FechaSeniat, fac_pendiente.Seniat, w_s)
                                    structura.Invoice = w_s
                                    structura.Seniat = w_s
                                    structura.Xfactura = ""
                                    lp_fecha_esc_n(fac_pendiente.FechaSeniat, structura.Fecha, fac_pendiente.Idioma.Id)
                                Else
                                    structura.Fecha = ""
                                End If

                                structura.Tipo = "INVOICE  N°"
                            End If
                        Case "3"
                            If fac_pendiente.FechaFactura IsNot Nothing Then
                                lp_compl(fac_pendiente.FechaFactura, fac_pendiente.Id, w_s)
                                structura.Xfactura = ""
                                structura.Invoice = w_s
                                structura.Seniat = w_s
                                lp_fecha_esc_n(fac_pendiente.FechaFactura, structura.Fecha, fac_pendiente.Idioma.Id)
                            Else
                                structura.Fecha = ""
                            End If
                            If fac_pendiente.Asociado.BPendienteStatement = True Then
                                structura.Tipo = "STATEMENT  N°"
                            Else
                                If fac_pendiente.Asociado.TipoPersona.ToString = "T" Then
                                    structura.Tipo = "STATEMENT  N°"
                                Else
                                    structura.Tipo = "FACTURA  N°"
                                End If
                            End If
                    End Select
                    structura.Moneda = fac_pendiente.Moneda.Id
                    structura.Mbruto = monto
                    Dim descuento As Double
                    descuento = (monto * fac_pendiente.Descuento) / 100
                    structura.Mdescuento = descuento
                    '((w_monto -mdescuento.Trailer) * pimpuesto.fac_pend) / 100
                    structura.Miva = ((monto - descuento) * fac_pendiente.Impuesto) / 100

                    If encontrar_OperacionPendiente(fac_pendiente.Id) = True Then
                        Select Case fac_pendiente.P_mip
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

                End If
            Catch ex As Exception
                'logger.Error(ex.Message)

            End Try
            Return (structura)
        End Function

        Private Sub ObtenerEstructuraPendiente(ByRef enc As IList(Of StructReporteFActuraPendienteEnc), ByRef det As List(Of StructReporteFActuraPendienteDeta))

            Dim retorno As IList(Of StructReporteFActuraPendienteEnc) = New List(Of StructReporteFActuraPendienteEnc)
            Dim structura As New StructReporteFActuraPendienteEnc()
            Dim j As Integer = 0
            Dim monto As Double = 0
            Try
                For i As Integer = 0 To _FacFacturaPendiente.Count - 1
                    If i = 0 Then
                        'llamar encabezado
                        j = j + 1
                        ObtenerEstructuraPendienteDeta(j, det, _FacFacturaPendiente(i).Id, monto)

                        structura = ObtenerEstructuraPendienteEnc(_FacFacturaPendiente(i), monto)
                        structura.Id = j
                        retorno.Add(structura)
                    Else
                        If _FacFacturaPendiente(i).Id <> _FacFacturaPendiente(i - 1).Id Then
                            'llamar encabezado
                            j = j + 1

                            ObtenerEstructuraPendienteDeta(j, det, _FacFacturaPendiente(i).Id, monto)

                            structura = ObtenerEstructuraPendienteEnc(_FacFacturaPendiente(i), monto)
                            structura.Id = j
                            retorno.Add(structura)
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

        Private Sub ObtenerEstructuraPendienteDeta(ByVal id As String, ByVal detalle As List(Of StructReporteFActuraPendienteDeta), ByVal idfactura As Integer, ByRef monto As Double)
            Dim retorno As IList(Of StructReporteFActuraPendienteDeta) = New List(Of StructReporteFActuraPendienteDeta)
            retorno = detalle
            Dim _FacFacturaDetalle As List(Of FacFactuDetalle) = consultar_facturadetalle(idfactura)
            monto = 0
            Dim structura As New StructReporteFActuraPendienteDeta()
            structura = inicializar_Pendientedeta()
            Try

                For i As Integer = 0 To _FacFacturaDetalle.Count - 1
                    structura.Servicio = _FacFacturaDetalle(i).XDetalle
                    structura.Id = id
                    'If Me._ventana.TipoMoneda = "Moneda Original" Then
                    '    structura.Npub = _FacFacturaDetalle(i).Pu
                    '    structura.Ndesc = _FacFacturaDetalle(i).MDescuento
                    structura.MMonto = _FacFacturaDetalle(i).BDetalle
                    monto = monto + _FacFacturaDetalle(i).BDetalle
                    'End If
                    'If Me._ventana.TipoMoneda = "Bolivar Fuerte" Then
                    '    If _FacFacturaDetalle(i).NCantidad <> 0 Then

                    '    End If
                    '    structura.MMonto = _FacFacturaDetalle(i).BDetalleBf
                    '    structura.Ndesc = _FacFacturaDetalle(i).MDescuento
                    '    If _FacFacturaDetalle(i).NCantidad <> 0 Then
                    '        Dim w_cuadre As Double
                    '        w_cuadre = _FacFacturaDetalle(i).PuBf / _FacFacturaDetalle(i).NCantidad
                    '        If (w_cuadre <> _FacFacturaDetalle(i).PuBf) Then
                    '            structura.Npub = w_cuadre
                    '        Else
                    '            structura.Npub = _FacFacturaDetalle(i).PuBf
                    '        End If
                    '    Else
                    '        structura.Npub = _FacFacturaDetalle(i).PuBf
                    '    End If
                    'End If

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

        Public Function inicializar_Pendienteenc() As StructReporteFActuraPendienteEnc
            Dim structura As New StructReporteFActuraPendienteEnc()
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
            structura.Mbruto = ""
            structura.Mdescuento = ""
            structura.Miva = ""
            structura.Mtotal = ""
            structura.Moneda = ""
            structura.Xfactura = ""
            structura.Control = ""
            structura.Seniat = ""
            structura.Tipo = ""
            structura.Xour = ""
            structura.Xourref = ""
            Return (structura)
        End Function

        Public Function inicializar_Pendientedeta() As StructReporteFActuraPendienteDeta
            Dim structura As New StructReporteFActuraPendienteDeta()
            structura.Id = ""
            structura.Servicio = ""
            structura.Na = ""
            structura.Npub = ""
            structura.Cantidad = ""
            structura.MMonto = ""
            structura.Ndesc = ""
            Return (structura)
        End Function

        Public Function datosPendienteenc_colum() As DataTable
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
            datosEnc2.Columns.Add("Mbruto")
            datosEnc2.Columns.Add("Mdescuento")
            datosEnc2.Columns.Add("Miva")
            datosEnc2.Columns.Add("Mtotal")
            datosEnc2.Columns.Add("Moneda")
            datosEnc2.Columns.Add("Xfactura")
            datosEnc2.Columns.Add("Control")
            datosEnc2.Columns.Add("Xour")
            datosEnc2.Columns.Add("Xourref")
            datosEnc2.Columns.Add("Seniat")
            datosEnc2.Columns.Add("Tipo")
            Return datosEnc2
        End Function

        Public Function datosPendientedeta_colum() As DataTable
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

        Private Function ArmarReportePendienteEnc(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraPendienteEnc)) As DataTable
            Try
                For Each structura As StructReporteFActuraPendienteEnc In estructurasDeDatos
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
                    filaDatos("Mbruto") = poner_decimal(structura.Mbruto)
                    filaDatos("Mdescuento") = poner_decimal(structura.Mdescuento)
                    filaDatos("Miva") = poner_decimal(structura.Miva)
                    filaDatos("Moneda") = structura.Moneda
                    filaDatos("Mtotal") = poner_decimal(structura.Mtotal)
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

        Private Function ArmarReportePendienteDeta(ByVal datos As DataTable, ByVal estructurasDeDatos As IList(Of StructReporteFActuraPendienteDeta)) As DataTable
            Try
                For Each structura As StructReporteFActuraPendienteDeta In estructurasDeDatos
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

        Structure StructReporteFActuraPendienteEnc
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
            Private _Mbruto As String
            Private _Mdescuento As String
            Private _Miva As String
            Private _Mtotal As String
            Private _Moneda As String
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

            Public Property Mbruto() As String
                Get
                    Return Me._Mbruto
                End Get
                Set(ByVal value As String)
                    Me._Mbruto = value
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
            Public Property Miva() As String
                Get
                    Return Me._Miva
                End Get
                Set(ByVal value As String)
                    Me._Miva = value
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
            Public Property Mtotal() As String
                Get
                    Return Me._Mtotal
                End Get
                Set(ByVal value As String)
                    Me._Mtotal = value
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

        Structure StructReporteFActuraPendienteDeta
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
        '''''''''''''''Fin reportes  facturas pendientes
    End Class
End Namespace
