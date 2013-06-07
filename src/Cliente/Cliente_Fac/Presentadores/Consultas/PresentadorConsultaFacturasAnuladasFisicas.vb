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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.Consultas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Consultas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacReportes
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.Consultas
    Class PresentadorConsultaFacturasAnuladasFisicas
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultaFacturasAnuladasFisicas
        Private _FacAnuladaFisicaServicios As IFacAnuladaFisicaServicios
        Private _FacFacturaServicios As IFacFacturaServicios
        'Private _FacAnuladaFisicas As IList(Of FacAnuladaFisica)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultaFacturasAnuladasFisicas)
            Try
                Me._ventana = ventana
                Me._FacAnuladaFisicaServicios = DirectCast(Activator.GetObject(GetType(IFacAnuladaFisicaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacAnuladaFisicaServicios")), IFacAnuladaFisicaServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            'Me.ActualizarTituloVentanaPrincipal("Consulta de Facturas Anuladas", Recursos.Ids.fac_menuItemConsultaFacturasAnuladasFisicas)
            Me.ActualizarTituloVentanaPrincipal("Consulta de Facturas Anuladas", "FAC_ANUL_FISI")
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
                'Me._ventana.FacAnuladaFisicaFiltrar = New FacAnuladaFisica
                'sumar(FacAnuladaFisicas)
                Consultar()


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


                Dim FacAnuladaFisicaAuxiliar As New FacAnuladaFisica



                'If (filtroValido = True) Then
                'FacAnuladaFisicaAuxiliar.Inicial = UsuarioLogeado.Iniciales
                'FacAnuladaFisicaAuxiliar.Anulada = "SI"
                Dim FacAnuladaFisicas As IList(Of FacAnuladaFisica)
                FacAnuladaFisicas = Me._FacAnuladaFisicaServicios.ObtenerFacAnuladaFisicasFiltro(FacAnuladaFisicaAuxiliar)
                'FacAnuladaFisicas = Me._FacAnuladaFisicaServicios.ConsultarTodos
                Me._ventana.Resultados = Nothing
                Me._ventana.Count = FacAnuladaFisicas.Count
                If FacAnuladaFisicas.Count <= 0 Then
                    MessageBox.Show("Mensaje: No se encontraron registros")
                End If
                Me._ventana.Resultados = FacAnuladaFisicas

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
        ''' Método que invoca una nueva página "ConsultarFacAnuladaFisica" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacAnuladaFisica()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacAnuladaFisicaSeleccionado.Accion = 2 'no modificar
            Dim facanuladaFisica As FacAnuladaFisica = DirectCast(Me._ventana.FacAnuladaFisicaSeleccionado, FacAnuladaFisica)
            Dim FacFacturaAnulada As New FacFacturaAnulada
            FacFacturaAnulada.Id = 0
            FacFacturaAnulada.Secanula = facanuladaFisica.Id
            FacFacturaAnulada.FechaAnulacion = facanuladaFisica.FechaAnulacion
            FacFacturaAnulada.Control = facanuladaFisica.Control
            FacFacturaAnulada.Xter = 10

            Me.Navegar(New FacturaAnuladaRpt(Nothing, FacFacturaAnulada))

            'Me.Navegar(New ConsultarFacAnuladaFisica())
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