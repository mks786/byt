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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Tasas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Tasas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.Tasas
    Class PresentadorConsultarTasas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarTasas
        Private _TasaServicios As ITasaServicios
        Private _Tasas As IList(Of Tasa)

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarTasas)
            Try
                Me._ventana = ventana
                Me._TasaServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
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

            Me.Navegar(New ConsultarTasas())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarTasas, Recursos.Ids.fac_ConsultarTasa)
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

                Consultar()
                'Dim tasa As New Tasa()
                'tasa.moneda = Me._TasaServicios.ConsultarTodos(0).moneda

                'Dim tasasaux As New Tasa
                'tasasaux.Id = FormatDateTime(Date.Now, DateFormat.ShortDate)
                'Me._Tasas = Me._TasaServicios.ObtenerTasasFiltro(tasasaux)
                'Me._ventana.Count = Me._Tasas.Count
                'Me._ventana.Resultados = Me._Tasas


                Me._ventana.TasaFiltrar = New Tasa()
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
            Dim tasasaux As New Tasa
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim tasa As Tasa = DirectCast(Me._ventana.TasaFiltrar, Tasa)
                'Tasa.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                'tasa.Id = Nothing
                ' Dim tasasFiltrados As IEnumerable(Of Tasa) = Me._Tasas
                If Not String.IsNullOrEmpty(Me._ventana.Id) Then
                    'tasasFiltrados = From a In tasasFiltrados Where a.Id.ToString().Contains(Me._ventana.Id)
                    tasasaux.Id = CDate(Me._ventana.Id)
                End If

                If tasa IsNot Nothing Then
                    If Not String.IsNullOrEmpty(tasa.Moneda) Then
                        'tasasFiltrados = From p In tasasFiltrados Where p.Moneda IsNot Nothing AndAlso p.Moneda.ToLower().Contains(tasa.Moneda.ToLower())
                        tasasaux.Moneda = tasa.Moneda
                    End If

                    If Not String.IsNullOrEmpty(tasa.Tasabf) Then
                        'tasasFiltrados = From p In tasasFiltrados Where p.Tasabf = Double.Parse(tasa.Tasabf)
                        'ImpuestosFiltrados = From p In ImpuestosFiltrados Where p.Valor = Double.Parse(Impuesto.Valor)
                        tasasaux.Tasabf = tasa.Tasabf
                    End If

                    If Not String.IsNullOrEmpty(tasa.Tasabs) Then
                        'tasasFiltrados = From p In tasasFiltrados Where p.Tasabs = Double.Parse(tasa.Tasabs)
                        'ImpuestosFiltrados = From p In ImpuestosFiltrados Where p.Valor = Double.Parse(Impuesto.Valor)
                        tasasaux.Tasabs = tasa.Tasabs
                    End If
                End If
                'tasasaux.Id = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Me._Tasas = Me._TasaServicios.ObtenerTasasFiltro(tasasaux)

                Me._ventana.Count = Me._Tasas.Count
                If Me._Tasas.Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If
                Me._ventana.Resultados = Me._Tasas
                'Me._ventana.Resultados = tasasFiltrados.ToList(IEnumerable(Of Tasa))

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
        ''' Método que invoca una nueva página "ConsultarTasa" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarTasa()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarTasa(Me._ventana.TasaSeleccionado))
            'Me.Navegar(New ConsultarTasa())
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
