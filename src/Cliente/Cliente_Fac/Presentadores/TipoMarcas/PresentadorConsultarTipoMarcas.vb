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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TipoMarcas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.TipoMarcas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.TipoMarcas
    Class PresentadorConsultarTipoMarcas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarTipoMarcas
        Private _TipoMarcaServicios As ITipoMarcaServicios
        Private _TipoMarcas As IList(Of TipoMarca)

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarTipoMarcas)
            Try
                Me._ventana = ventana
                Me._TipoMarcaServicios = DirectCast(Activator.GetObject(GetType(ITipoMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoMarcaServicios")), ITipoMarcaServicios)
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

            Me.Navegar(New ConsultarTipoMarcas())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarTipoMarcas, Recursos.Ids.fac_ConsultarTipoMarca)
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

                Me._TipoMarcas = Me._TipoMarcaServicios.ConsultarTodos()
                Me._ventana.Count = Me._TipoMarcas.Count
                Me._ventana.Resultados = Me._TipoMarcas
                Me._ventana.TipoMarcaFiltrar = New TipoMarca()
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

                Dim TipoMarca As TipoMarca = DirectCast(Me._ventana.TipoMarcaFiltrar, TipoMarca)
                'TipoMarca.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                'TipoMarca.Id = Nothing
                Dim TipoMarcasFiltrados As IEnumerable(Of TipoMarca) = Me._TipoMarcas


                If Not String.IsNullOrEmpty(TipoMarca.Id) Then
                    TipoMarcasFiltrados = From p In TipoMarcasFiltrados Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(TipoMarca.Id.ToLower())
                End If

                If Not String.IsNullOrEmpty(TipoMarca.Doc_Esp) Then
                    TipoMarcasFiltrados = From p In TipoMarcasFiltrados Where p.Doc_Esp IsNot Nothing AndAlso p.Doc_Esp.ToLower().Contains(TipoMarca.Doc_Esp.ToLower())
                End If

                If Not String.IsNullOrEmpty(TipoMarca.Doc_Ingl) Then
                    TipoMarcasFiltrados = From p In TipoMarcasFiltrados Where p.Doc_Ingl IsNot Nothing AndAlso p.Doc_Ingl.ToLower().Contains(TipoMarca.Doc_Ingl.ToLower())
                End If

                If Not String.IsNullOrEmpty(TipoMarca.Docs_Esp) Then
                    TipoMarcasFiltrados = From p In TipoMarcasFiltrados Where p.Docs_Esp IsNot Nothing AndAlso p.Docs_Esp.ToLower().Contains(TipoMarca.Docs_Esp.ToLower())
                End If

                If Not String.IsNullOrEmpty(TipoMarca.Docs_Ingl) Then
                    TipoMarcasFiltrados = From p In TipoMarcasFiltrados Where p.Docs_Ingl IsNot Nothing AndAlso p.Docs_Ingl.ToLower().Contains(TipoMarca.Docs_Ingl.ToLower())
                End If

                Me._ventana.Count = TipoMarcasFiltrados.ToList().Count
                If TipoMarcasFiltrados.ToList().Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If
                Me._ventana.Resultados = TipoMarcasFiltrados.ToList()
                'Me._ventana.Resultados = TipoMarcasFiltrados.ToList(IEnumerable(Of TipoMarca))

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
        ''' Método que invoca una nueva página "ConsultarTipoMarca" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarTipoMarca()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarTipoMarca(Me._ventana.TipoMarcaSeleccionado))
            'Me.Navegar(New ConsultarTipoMarca())
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