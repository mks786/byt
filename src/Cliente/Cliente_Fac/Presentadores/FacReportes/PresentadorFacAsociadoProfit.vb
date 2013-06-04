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
    Class PresentadorFacAsociadoProfit
        Inherits PresentadorBase

        Private _ventana As IFacAsociadoProfit
        'Private _Asociado As Asociado
        'Dim _AsociadoDetalle As List(Of FacFactuDetalle)
        Private _AsociadoServicios As IAsociadoServicios
        'Private _AsociadoAnuladaServicios As IAsociadoAnuladaServicios
        ' Private _tasasServicios As ITasaServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New(ByVal ventana As IFacAsociadoProfit)
            Try
                Me._ventana = ventana

                Me._AsociadoServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                'Me._AsociadoAnuladaServicios = DirectCast(Activator.GetObject(GetType(IAsociadoAnuladaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoAnuladaServicios")), IAsociadoAnuladaServicios)
                ' Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)

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

            Me.Navegar(New Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes.FacAsociadoProfit())
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
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemFacAsociadoProfit, Recursos.Ids.fac_menuItemFacAsociadoProfit)
        End Sub

        Public Function consultar_Asociado() As List(Of Asociado)

            Dim AsociadoAuxiliar As New Asociado()
            Dim Asociados As List(Of Asociado)
            Asociados = Nothing
            Try
                ' Dim fecha As DateTime = Me._ventana.FechaInicio
                ' AsociadoAuxiliar.FechaDesde = Me._ventana.FechaInicio
                ' AsociadoAuxiliar.FechaHasta = Me._ventana.FechaFin
                'AsociadoAuxiliar.Status = 1

                ' Asociados = Me._AsociadoServicios.ObtenerAsociadosFiltro(AsociadoAuxiliar)
                Asociados = Me._AsociadoServicios.ConsultarTodos
                If Asociados IsNot Nothing Then
                    If Asociados.Count > 0 Then
                        Return (Asociados)
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
            Dim txt_dir As String = ConfigurationManager.AppSettings("AsociadoProfit")
            Dim asociados As List(Of Asociado) = consultar_Asociado()

            If asociados IsNot Nothing Then
                If File.Exists(txt_dir) Then
                    File.Delete(txt_dir)
                End If
                Console.WriteLine("{0} already exists.", txt_dir)
                Using txt_Writer = New System.IO.StreamWriter(txt_dir)
                    Dim linea As String = ""
                    For i As Integer = 0 To asociados.Count - 1
                        linea = ""
                        linea = asociados(i).Id & vbTab
                        linea = linea & asociados(i).Nombre & vbTab
                        linea = linea & asociados(i).Rif & vbTab
                        linea = linea & asociados(i).Nit & vbTab
                        Dim domicilio As String = asociados(i).Domicilio
                        domicilio = Replace(domicilio, vbCrLf, " ")
                        domicilio = Replace(domicilio, vbTab, " ")
                        domicilio = Replace(domicilio, vbLf, " ")
                        domicilio = Replace(domicilio, vbCr, " ")
                        domicilio = Replace(domicilio, vbNewLine, " ")
                        domicilio = Replace(domicilio, vbNullChar, " ")
                        domicilio = Replace(domicilio, vbNullString, " ")
                        domicilio = Replace(domicilio, vbObjectError, " ")
                        domicilio = Replace(domicilio, vbTab, " ")
                        domicilio = Replace(domicilio, vbBack, " ")
                        domicilio = Replace(domicilio, vbFormFeed, " ")
                        domicilio = Replace(domicilio, vbVerticalTab, " ")
                        linea = linea & domicilio & vbTab
                        'linea = linea & asociados(i).DiaCredito & vbTab
                        linea = linea & "C0" & vbTab
                        linea = linea & asociados(i).Telefono1 & vbTab
                        linea = linea & asociados(i).Fax1
                        txt_Writer.WriteLine(linea)
                    Next
                    txt_Writer.Close() 'cierra txt
                    System.GC.Collect()
                    Mouse.OverrideCursor = Nothing
                End Using
                Process.Start(txt_dir)
                MessageBox.Show("Registro Creado con exito en la ruta " & txt_dir, "TXT", MessageBoxButton.OK)
            End If
        End Sub




    End Class
End Namespace
