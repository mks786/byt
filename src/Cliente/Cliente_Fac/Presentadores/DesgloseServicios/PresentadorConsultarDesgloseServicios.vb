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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DesgloseServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.DesgloseServicios
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios

Namespace Presentadores.DesgloseServicios
    Class PresentadorConsultarDesgloseServicios
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDesgloseServicios
        Private _DesgloseServicioServicios As IFacDesgloseServicioServicios
        Private _DesgloseServicios As IList(Of FacDesgloseServicio)
        Private _servicioServicios As IFacServicioServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDesgloseServicios)
            Try
                Me._ventana = ventana
                Me._DesgloseServicioServicios = DirectCast(Activator.GetObject(GetType(IFacDesgloseServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseServicioServicios")), IFacDesgloseServicioServicios)
                'Me._ids_DesgloseServicios = DirectCast(Activator.GetObject(GetType(IDesgloseServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DesgloseServicios")), IDesgloseServicios)
                Me._servicioServicios = DirectCast(Activator.GetObject(GetType(IFacServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacServicioServicios")), IFacServicioServicios)

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarDesgloseServicios, Recursos.Ids.fac_ConsultarDesgloseServicio)
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

                Me._DesgloseServicios = Me._DesgloseServicioServicios.ConsultarTodos()
                Me._ventana.Count = Me._DesgloseServicios.Count
                Me._ventana.Resultados = Me._DesgloseServicios
                Me._ventana.DesgloseServicioFiltrar = New FacDesgloseServicio()

                Dim Servicios As IList(Of FacServicio) = Me._servicioServicios.ConsultarTodos()
                Dim primerServicio As New FacServicio()
                primerServicio.Id = "NGN"
                Servicios.Insert(0, primerServicio)
                Me._ventana.Servicios = Servicios                

                'Dim DesgloseServicio As DesgloseServicio = DirectCast(Me._ventana.Resultados, DesgloseServicio)

                'Me._ventana.Servicio = Me.BuscarServicio(Servicios, DesgloseServicio.Servicio)

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

            Me.Navegar(New ConsultarDesgloseServicios())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
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

                Dim DesgloseServicio As FacDesgloseServicio = DirectCast(Me._ventana.DesgloseServicioFiltrar, FacDesgloseServicio)
                'DesgloseServicio.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                'DesgloseServicio.Id = Nothing
                Dim DesgloseServiciosFiltrados As IEnumerable(Of FacDesgloseServicio) = Me._DesgloseServicios

                If Not String.IsNullOrEmpty(Me._ventana.Id.ToString()) AndAlso Not Me._ventana.Id.Equals(" "c) Then
                    DesgloseServiciosFiltrados = From a In DesgloseServiciosFiltrados Where a.Id.ToString().ToLower().Contains(Me._ventana.Id.ToString().ToLower())
                End If

                If Me._ventana.Servicio IsNot Nothing AndAlso Not DirectCast(Me._ventana.Servicio, FacServicio).Id.Equals("NGN") Then
                    DesgloseServiciosFiltrados = From a In DesgloseServiciosFiltrados Where a.Servicio IsNot Nothing AndAlso a.Servicio.Id.Contains(DirectCast(Me._ventana.Servicio, FacServicio).Id)
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Pporc) Then
                    DesgloseServiciosFiltrados = From a In DesgloseServiciosFiltrados Where a.Pporc = Integer.Parse(Me._ventana.Pporc)
                End If

                Me._ventana.Count = DesgloseServiciosFiltrados.ToList().Count
                Me._ventana.Resultados = DesgloseServiciosFiltrados.ToList()
                'Me._ventana.Resultados = DesgloseServiciosFiltrados.ToList(IEnumerable(Of DesgloseServicio))

                '#Region "trace"
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
        ''' Método que invoca una nueva página "ConsultarDesgloseServicio" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarDesgloseServicio()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarDesgloseServicio(Me._ventana.DesgloseServicioSeleccionado))
            '        Me.Navegar(New ConsultarDesgloseServicio())
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
    End Class
End Namespace