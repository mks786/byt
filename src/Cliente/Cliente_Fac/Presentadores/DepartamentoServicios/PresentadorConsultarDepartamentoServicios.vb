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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.DepartamentoServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.DepartamentoServicios
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios

Namespace Presentadores.DepartamentoServicios
    Class PresentadorConsultarDepartamentoServicios
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepartamentoServicios
        Private _DepartamentoServicioServicios As IFacDepartamentoServicioServicios
        Private _DepartamentoServicios As IList(Of FacDepartamentoServicio)

        Private _ids_departamentoServicios As IDepartamentoServicios
        Private _servicioServicios As IFacServicioServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepartamentoServicios)
            Try
                Me._ventana = ventana
                Me._DepartamentoServicioServicios = DirectCast(Activator.GetObject(GetType(IFacDepartamentoServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicioServicios")), IFacDepartamentoServicioServicios)
                Me._ids_departamentoServicios = DirectCast(Activator.GetObject(GetType(IDepartamentoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("DepartamentoServicios")), IDepartamentoServicios)
                Me._servicioServicios = DirectCast(Activator.GetObject(GetType(IFacServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacServicioServicios")), IFacServicioServicios)

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarDepartamentoServicios, Recursos.Ids.fac_ConsultarDepartamentoServicio)
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

                Dim departamentoServicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicioFiltrar, FacDepartamentoServicio)

                Me._DepartamentoServicios = Me._DepartamentoServicioServicios.ConsultarTodos()
                Me._ventana.Count = Me._DepartamentoServicios.Count
                Me._ventana.Resultados = Me._DepartamentoServicios
                Me._ventana.DepartamentoServicioFiltrar = New FacDepartamentoServicio()

                Dim ids As IList(Of Departamento) = Me._ids_departamentoServicios.ConsultarTodos()
                Me._ventana.GetSetIds = ids
                Me._ventana.GetSetId = Me.BuscarDepartamento(ids, departamentoServicio.Id)

                Dim servicios As IList(Of FacServicio) = Me._servicioServicios.ConsultarTodos()
                Me._ventana.Servicios = servicios
                Me._ventana.Servicio = Me.BuscarServicio(servicios, departamentoServicio.Servicio)


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

            Me.Navegar(New ConsultarDepartamentoServicios())
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

                Dim DepartamentoServicio As FacDepartamentoServicio = DirectCast(Me._ventana.DepartamentoServicioFiltrar, FacDepartamentoServicio)
                'DepartamentoServicio.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                'DepartamentoServicio.Id = Nothing
                Dim DepartamentoServiciosFiltrados As IEnumerable(Of FacDepartamentoServicio) = Me._DepartamentoServicios


                If Me._ventana.Id IsNot Nothing AndAlso Not DirectCast(Me._ventana.GetSetId, Departamento).Id.Equals("NGN") Then
                    DepartamentoServiciosFiltrados = From e In DepartamentoServiciosFiltrados Where e.Id IsNot Nothing AndAlso e.Id.Id.Contains(DirectCast(Me._ventana.GetSetId, Departamento).Id)
                End If

                If Me._ventana.Servicio IsNot Nothing AndAlso Not DirectCast(Me._ventana.Servicio, FacServicio).Id.Equals("NGN") Then
                    DepartamentoServiciosFiltrados = From e In DepartamentoServiciosFiltrados Where e.Servicio IsNot Nothing AndAlso e.Servicio.Id.Contains(DirectCast(Me._ventana.Servicio, FacServicio).Id)
                End If

                Me._ventana.Count = DepartamentoServiciosFiltrados.ToList().Count
                Me._ventana.Resultados = DepartamentoServiciosFiltrados.ToList()
                'Me._ventana.Resultados = DepartamentoServiciosFiltrados.ToList(IEnumerable(Of DepartamentoServicio))

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
        ''' Método que invoca una nueva página "ConsultarDepartamentoServicio" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarDepartamentoServicio()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarDepartamentoServicio(Me._ventana.DepartamentoServicioSeleccionado))
            '        Me.Navegar(New ConsultarDepartamentoServicio())
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