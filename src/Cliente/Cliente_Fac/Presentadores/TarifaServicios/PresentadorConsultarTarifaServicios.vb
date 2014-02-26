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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.TarifaServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.TarifaServicios
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios

Namespace Presentadores.TarifaServicios
    Class PresentadorConsultarTarifaServicios
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarTarifaServicios
        Private _TarifaServicioServicios As ITarifaServicioServicios
        Private _TarifaServicios As IList(Of TarifaServicio)
        Private _tarifas_tarifaServicios As ITarifaServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarTarifaServicios)
            Try
                Me._ventana = ventana
                Me._TarifaServicioServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicioServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicioServicios")), ITarifaServicioServicios)
                'Me._ids_TarifaServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicios")), ITarifaServicios)
                Me._tarifas_tarifaServicios = DirectCast(Activator.GetObject(GetType(ITarifaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TarifaServicios")), ITarifaServicios)

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

            Me.Navegar(New ConsultarTarifaServicios())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarTarifaServicios, Recursos.Ids.fac_ConsultarTarifaServicio)
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

                'Dim TarifaServicio As TarifaServicio = DirectCast(Me._ventana.TarifaServicioFiltrar, TarifaServicio)

                Me._TarifaServicios = Me._TarifaServicioServicios.ConsultarTodos()
                Me._ventana.Count = Me._TarifaServicios.Count
                Me._ventana.Resultados = Me._TarifaServicios
                Me._ventana.TarifaServicioFiltrar = New TarifaServicio()

                Dim tarifas As IList(Of Tarifa) = Me._tarifas_tarifaServicios.ConsultarTodos()
                Dim primeraTarifa As New Tarifa()
                primeraTarifa.Id = "NGN"
                tarifas.Insert(0, primeraTarifa)
                Me._ventana.Tarifas = tarifas
                'Me._ventana.Tarifa = Me.BuscarTarifa2(tarifas, TarifaServicio.Tarifa)

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

                Dim TarifaServicio As TarifaServicio = DirectCast(Me._ventana.TarifaServicioFiltrar, TarifaServicio)               

                Dim TarifaServiciosFiltrados As IEnumerable(Of TarifaServicio) = Me._TarifaServicios


                'If Not String.IsNullOrEmpty(TarifaServicio.Tarifa) Then
                '    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(TarifaServicio.Id.ToLower())
                'End If

                If Not String.IsNullOrEmpty(TarifaServicio.Id) Then
                    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(TarifaServicio.Id.ToLower())
                End If


                If Not String.IsNullOrEmpty(Me._ventana.Tasa) Then
                    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Id = Integer.Parse(Me._ventana.Tasa)
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Mont_Us) Then
                    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Mont_Us = Integer.Parse(Me._ventana.Mont_Us)
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Mont_Bs) Then
                    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Mont_Bs = Integer.Parse(Me._ventana.Mont_Bs)
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Mont_Bf) Then
                    TarifaServiciosFiltrados = From p In TarifaServiciosFiltrados Where p.Mont_Bf = Integer.Parse(Me._ventana.Mont_Bf)
                End If

                If Me._ventana.Tarifa IsNot Nothing AndAlso Not DirectCast(Me._ventana.Tarifa, Tarifa).Id.Equals("NGN") Then
                    TarifaServiciosFiltrados = From e In TarifaServiciosFiltrados Where e.Tarifa IsNot Nothing AndAlso e.Tarifa.Id.Contains(DirectCast(Me._ventana.Tarifa, Tarifa).Id)
                End If


                Me._ventana.Count = TarifaServiciosFiltrados.ToList().Count
                If TarifaServiciosFiltrados.ToList().Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If
                Me._ventana.Resultados = TarifaServiciosFiltrados.ToList()
                'Me._ventana.Resultados = TarifaServiciosFiltrados.ToList(IEnumerable(Of TarifaServicio))

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
        ''' Método que invoca una nueva página "ConsultarTarifaServicio" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarTarifaServicio()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Dim tarifaServicioSeleccionado As TarifaServicio = DirectCast(Me._ventana.TarifaServicioSeleccionado(0), TarifaServicio)

            'Me.Navegar(New ConsultarTarifaServicio(Me._ventana.TarifaServicioSeleccionado)) -- CODIGO ORIGINAL COMENTADO NO BORRAR

            Me.Navegar(New ConsultarTarifaServicio(tarifaServicioSeleccionado))


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

        Public Sub CalcularTarifasDeServicio(ByVal cantidadRegistros As Integer)

            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim listaTarifaServicios As List(Of TarifaServicio) = New List(Of TarifaServicio)()

                For index = 0 To cantidadRegistros - 1
                    Dim TarifaServicio As TarifaServicio = DirectCast(Me._ventana.TarifaServicioSeleccionado(index), TarifaServicio)
                    listaTarifaServicios.Add(TarifaServicio)
                Next


                If listaTarifaServicios.Count > 0 Then
                    Dim ventanaRecalcular As RecalcularTarifaServicios = New RecalcularTarifaServicios(listaTarifaServicios)
                    ventanaRecalcular.ShowDialog()
                End If



                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try

        End Sub

        

    End Class
End Namespace