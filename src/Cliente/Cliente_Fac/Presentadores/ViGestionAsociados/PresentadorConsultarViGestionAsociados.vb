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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ViGestionAsociados
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.ViGestionAsociados
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.ViGestionAsociados
    Class PresentadorConsultarViGestionAsociados
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarViGestionAsociados
        Private _ViGestionAsociadoServicios As IViGestionAsociadoServicios
        Private _ViGestionAsociados As IList(Of ViGestionAsociado)
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarViGestionAsociados)
            Try
                Me._ventana = ventana
                Me._ViGestionAsociadoServicios = DirectCast(Activator.GetObject(GetType(IViGestionAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ViGestionAsociadoServicios")), IViGestionAsociadoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
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

            Me.Navegar(New ConsultarViGestionAsociados())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarViGestionAsociados, Recursos.Ids.fac_ConsultarViGestionAsociado)
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

                Me._ViGestionAsociados = Me._ViGestionAsociadoServicios.ConsultarTodos()
                Me._ventana.Count = Me._ViGestionAsociados.Count
                Me._ventana.Resultados = Me._ViGestionAsociados
                Me._ventana.ViGestionAsociadoFiltrar = New ViGestionAsociado

                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraAsociado As New Asociado()
                'primeraAsociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraAsociado)
                'Me._ventana.Asociados = asociados
                Me._ventana.NCantidad = Nothing
                Me._ventana.NSaldo = Nothing
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

                Dim ViGestionAsociado As ViGestionAsociado = DirectCast(Me._ventana.ViGestionAsociadoFiltrar, ViGestionAsociado)
                'ViGestionAsociado.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                'ViGestionAsociado.Id = Nothing
                Dim ViGestionAsociadosFiltrados As IEnumerable(Of ViGestionAsociado) = Me._ViGestionAsociados

                If Me._ventana.Asociado IsNot Nothing AndAlso Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals(Integer.MinValue) Then
                    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Id.Id = Integer.Parse(DirectCast(Me._ventana.Asociado, Asociado).Id)
                    'ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Id.Id.ToString().ToLower().Contains(asociado.Id.ToString().ToLower())
                End If

                'If Not String.IsNullOrEmpty(ViGestionAsociado.Id) Then
                '    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Id = Integer.Parse(ViGestionAsociado.Id)
                '    'Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(ViGestionAsociado.Id.ToLower())
                'End If

                If Not String.IsNullOrEmpty(Me._ventana.NCantidad) Then                    
                    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Cantidad = Integer.Parse((Me._ventana.NCantidad))
                    'Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(ViGestionAsociado.Id.ToLower())
                End If

                If Not String.IsNullOrEmpty(Me._ventana.FechaUltima) Then
                    Dim FechaUltimaAux As DateTime = DateTime.Parse(Me._ventana.FechaUltima)
                    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.FechaUltima.Equals(FechaUltimaAux)
                End If

                If Not String.IsNullOrEmpty(ViGestionAsociado.Moneda) Then
                    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Moneda IsNot Nothing AndAlso p.Moneda.ToLower().Contains(ViGestionAsociado.Moneda.ToLower())
                End If

                If Not String.IsNullOrEmpty(Me._ventana.NSaldo) Then
                    ViGestionAsociadosFiltrados = From p In ViGestionAsociadosFiltrados Where p.Saldo = Integer.Parse((Me._ventana.NSaldo))
                    'Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(ViGestionAsociado.Id.ToLower())
                End If

                Me._ventana.Count = ViGestionAsociadosFiltrados.ToList().Count

                If ViGestionAsociadosFiltrados.ToList().Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If

                Me._ventana.Resultados = ViGestionAsociadosFiltrados.ToList()
                'Me._ventana.Resultados = ViGestionAsociadosFiltrados.ToList(IEnumerable(Of ViGestionAsociado))

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
        ''' Método que invoca una nueva página "ConsultarViGestionAsociado" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacGestionesAsociado()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            Dim asociado As Asociado = DirectCast(Me._ventana.ViGestionAsociadoSeleccionado, ViGestionAsociado).Id
            Me.Navegar(New ConsultarFacGestionesAsociado(asociado))
            'Me.Navegar(New ConsultarViGestionAsociado())
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
                        ' Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                    Else
                        Me._ventana.NombreAsociado = Nothing
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

    End Class
End Namespace