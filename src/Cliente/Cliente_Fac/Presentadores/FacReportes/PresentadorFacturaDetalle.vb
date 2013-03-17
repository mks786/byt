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
Imports System.IO
Imports Microsoft.Win32
Namespace Presentadores.FacReportes
    Class PresentadorFacturaDetalle
        Inherits PresentadorBase

        Private _ventana As IFacturaDetalle
        ' Private _FacFactura As FacFactura       
        Private _FacFacturaServicios As IFacFacturaServicios
        'Private _FacFacturaAnuladaServicios As IFacFacturaAnuladaServicios
        ' Private _tasasServicios As ITasaServicios
        Private _FacFactuDetaServicios As IFacFactuDetalleServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New(ByVal ventana As IFacturaDetalle)
            Try
                Me._ventana = ventana

                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                ' Me._FacFacturaAnuladaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaAnuladaServicios")), IFacFacturaAnuladaServicios)
                'Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._FacFactuDetaServicios = DirectCast(Activator.GetObject(GetType(IFacFactuDetalleServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFactuDetalleServicios")), IFacFactuDetalleServicios)
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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacturaDetalle())
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
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemFacturaDetalle, Recursos.Ids.fac_menuItemFacturaDetalle)
        End Sub

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

        Public Function consultar_factura_detalle(ByVal factura As FacFactura) As List(Of FacFactuDetalle)

            Dim FacFacturadetalleAuxiliar As New FacFactuDetalle()
            Dim FacFacturasdetalle As List(Of FacFactuDetalle)
            FacFacturasdetalle = Nothing
            Try                
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

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        Public Sub generar_txt()
            Mouse.OverrideCursor = Cursors.Wait
            Dim txt_dir As String = ConfigurationManager.AppSettings("facturadet")
            Dim factura As List(Of FacFactura) = consultar_factura()

            If factura IsNot Nothing Then
                If File.Exists(txt_dir) Then
                    File.Delete(txt_dir)
                End If
                Console.WriteLine("{0} already exists.", txt_dir)
                Using txt_Writer = New System.IO.StreamWriter(txt_dir)
                    Dim linea As String = ""
                    For i As Integer = 0 To factura.Count - 1
                        Dim detalle As List(Of FacFactuDetalle) = consultar_factura_detalle(factura(i))
                        If detalle IsNot Nothing Then
                            For j As Integer = 0 To detalle.Count - 1
                                If detalle(j).XDetalleEs <> "" Then
                                    linea = ""
                                    linea = factura(i).Seniat & vbTab
                                    linea = linea & detalle(j).XDetalleEs & vbTab
                                    linea = linea & detalle(j).NCantidad & vbTab
                                    linea = linea & SetFormatoDouble2(detalle(j).PuBf) & vbTab
                                    linea = linea & SetFormatoDouble2(detalle(j).Descuento) & vbTab
                                    If detalle(j).Impuesto.ToString = "T" Or detalle(j).Impuesto.ToString = "1" Then
                                        linea = linea & "0"
                                    Else
                                        linea = linea & "10"
                                    End If
                                End If
                                txt_Writer.WriteLine(linea)
                            Next
                        Else
                            linea = ""
                            linea = factura(i).Seniat & vbTab
                            linea = linea & "*******Anulada********" & vbTab
                            linea = linea & "0" & vbTab
                            linea = linea & "0" & vbTab
                            linea = linea & "0" & vbTab
                            linea = linea & "0"
                            txt_Writer.WriteLine(linea)
                        End If
                    Next
                    txt_Writer.Close() 'cierra txt
                    System.GC.Collect()
                    Mouse.OverrideCursor = Nothing
                End Using
                Process.Start(txt_dir)
                MessageBox.Show("Registro Creado con exito en la ruta " & txt_dir, "TXT", MessageBoxButton.OK)
            Else
                MessageBox.Show("No existe informacion para los parametros seleccionados", "TXT", MessageBoxButton.OK)
            End If
        End Sub


        'Public Sub lp_ini(ByVal detalle As FacFactuDetalle, ByRef p_xservicio As String, ByRef p_ncanti As Integer, ByRef p_npu As Double)
        '    If detalle.Factura.Asociado.Idioma.Id = "IN" And detalle.XDetalleEs = "" Then

        '    End If
        'End Sub


        'Public Sub lp_conv_detalle(ByVal p_cadena As String, ByRef p_salida As String, , ByRef p_hp As Integer, ByVal p_serv As Integer,p_fac As Integer, p_det As integer)
        '    Dim w_cadse, w_res As String

        'End Sub

        'Public Sub lp_busqueda(ByVal p_cd As String, ByVal p_bu As String, ByRef p_ex As Integer)

        'End Sub


    End Class
End Namespace
