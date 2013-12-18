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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Trascend.Bolet.Cliente.Ventanas.Marcas
Imports Trascend.Bolet.Cliente.Ventanas.Patentes


Namespace Presentadores.FacFacturaProformas
    Class PresentadorListaMarcasPatentesFacFacturaProforma
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IListaMarcasPatentesFacFacturaProforma
        Private _marcaServicios As IMarcaServicios
        Private _patenteServicios As IPatenteServicios
        Private _listaDatosValoresServicios As IListaDatosValoresServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IListaMarcasPatentesFacFacturaProforma)
            Try
                Me._ventana = ventana
                Me._marcaServicios = DirectCast(Activator.GetObject(GetType(IMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MarcaServicios")), IMarcaServicios)
                Me._patenteServicios = DirectCast(Activator.GetObject(GetType(IPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PatenteServicios")), IPatenteServicios)
                Me._listaDatosValoresServicios = DirectCast(Activator.GetObject(GetType(IListaDatosValoresServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ListaDatosValoresServicios")), IListaDatosValoresServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub New(ByVal ventana As IListaMarcasPatentesFacFacturaProforma, ByVal ventanaPadre As Object)
            Try

                Me._ventana = ventana
                Me._ventanaPadre = ventanaPadre

                Me._ventana = ventana
                Me._marcaServicios = DirectCast(Activator.GetObject(GetType(IMarcaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MarcaServicios")), IMarcaServicios)
                Me._patenteServicios = DirectCast(Activator.GetObject(GetType(IPatenteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PatenteServicios")), IPatenteServicios)
                Me._listaDatosValoresServicios = DirectCast(Activator.GetObject(GetType(IListaDatosValoresServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ListaDatosValoresServicios")), IListaDatosValoresServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacFacturas, Recursos.Ids.fac_ConsultarFacFactura)
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
                Me._ventana.FocoPredeterminado()

                Dim listaElementos As List(Of ListaDatosValores)
                listaElementos =
                    Me._listaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(New ListaDatosValores(Recursos.Etiquetas.cbiMarcaPatenteFacFacturas))
                Me._ventana.Elementos = listaElementos


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

                Dim marcaAuxiliar As New Marca()
                Dim patenteAuxiliar As New Patente()
                Dim listaMarcas As List(Of Marca)
                Dim listaPatentes As List(Of Patente)

                If (Me._ventana.ElementoConsultado IsNot Nothing) Then
                    If (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("MN") Or
                        (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("MI") Then
                        marcaAuxiliar = ObtenerDatosParaMarca()
                        listaMarcas = Me._marcaServicios.ObtenerMarcasFiltro(marcaAuxiliar)
                        If listaMarcas.Count <> 0 Then
                            Me._ventana.GestionarVisibilidadListas(True)
                            Me._ventana.Marcas = listaMarcas
                        Else
                            Me._ventana.Mensaje("No hay resultados con el filtro aplicado. Verifique el filtro.", 0)
                        End If

                    ElseIf (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("PN") Or
                        (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("PI") Then
                        patenteAuxiliar = ObtenerDatosParaPatente()
                        listaPatentes = Me._patenteServicios.ObtenerPatentesFiltro(patenteAuxiliar)
                        If listaPatentes.Count <> 0 Then
                            Me._ventana.GestionarVisibilidadListas(False)
                            Me._ventana.Patentes = listaPatentes
                        Else
                            Me._ventana.Mensaje("No hay resultados con el filtro aplicado. Verifique el filtro.", 0)
                        End If
                    End If
                Else
                    Me._ventana.Mensaje("Debe seleccionar una opcion para consultar una Marca o una Patente", 0)
                End If



                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Private Function ObtenerDatosParaMarca() As Marca
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim marca As New Marca()

                If Not String.IsNullOrEmpty(Me._ventana.IdElemento) Then
                    marca.Id = Integer.Parse(Me._ventana.IdElemento)
                Else
                    marca.Id = Integer.MinValue
                End If

                If Not String.IsNullOrEmpty(Me._ventana.IdInternacionalElemento) Then
                    marca.CodigoMarcaInternacional = Integer.Parse(Me._ventana.IdInternacionalElemento)
                Else
                    marca.CodigoMarcaInternacional = 0
                End If

                If Me._ventana.ElementoConsultado IsNot Nothing And (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("MI") Then
                    marca.LocalidadMarca = "I"
                Else
                    marca.LocalidadMarca = "N"
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Registro) Then
                    marca.CodigoRegistro = Me._ventana.Registro
                Else
                    marca.CodigoRegistro = Nothing
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Solicitud) Then
                    marca.CodigoInscripcion = Me._ventana.Solicitud
                Else
                    marca.CodigoInscripcion = Nothing
                End If

                Return marca

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If

            Catch ex As Exception
                Throw
            End Try

        End Function

        Private Function ObtenerDatosParaPatente() As Patente
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim patente As New Patente()

                If Not String.IsNullOrEmpty(Me._ventana.IdElemento) Then
                    patente.Id = Integer.Parse(Me._ventana.IdElemento)
                Else
                    patente.Id = Integer.MinValue
                End If

                If Not String.IsNullOrEmpty(Me._ventana.IdInternacionalElemento) Then
                    patente.CodigoPatenteInternacional = Integer.Parse(Me._ventana.IdInternacionalElemento)
                Else
                    patente.CodigoPatenteInternacional = 0
                End If

                If Me._ventana.ElementoConsultado IsNot Nothing And (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("PI") Then
                    patente.LocalidadPatente = "I"
                Else
                    patente.LocalidadPatente = "N"
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Registro) Then
                    patente.CodigoRegistro = Me._ventana.Registro
                Else
                    patente.CodigoRegistro = Nothing
                End If

                If Not String.IsNullOrEmpty(Me._ventana.Solicitud) Then
                    patente.CodigoInscripcion = Me._ventana.Solicitud
                Else
                    patente.CodigoInscripcion = Nothing
                End If

                Return patente

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If

            Catch ex As Exception
                Throw
            End Try
        End Function

        Sub BusquedaMarcaOPatenteInternacional()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If (Me._ventana.ElementoConsultado IsNot Nothing) Then
                    If (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("MI") Or
                        (DirectCast(Me._ventana.ElementoConsultado, ListaDatosValores)).Valor.Equals("PI") Then
                        Me._ventana.HabilitarCampoInternacional(True)
                    Else
                        Me._ventana.HabilitarCampoInternacional(False)
                    End If
                    Me._ventana.IdElemento = String.Empty
                    Me._ventana.IdInternacionalElemento = String.Empty
                    Me._ventana.Solicitud = String.Empty
                    Me._ventana.Registro = String.Empty
                    Me._ventana.Marcas = Nothing
                    Me._ventana.Patentes = Nothing
                End If


                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)

            End Try

        End Sub

        Sub LimpiarTodo()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Me._ventana.IdElemento = String.Empty
                Me._ventana.IdInternacionalElemento = String.Empty
                Me._ventana.HabilitarCampoInternacional(False)
                Me._ventana.Solicitud = String.Empty
                Me._ventana.Registro = String.Empty
                Me._ventana.ElementoConsultado = Nothing
                Me._ventana.Marcas = Nothing
                Me._ventana.Patentes = Nothing

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

        Sub ConsultarMarcaSeleccionada()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._ventana.MarcaSeleccionada IsNot Nothing Then
                    Dim marcaSeleccionada As Marca
                    marcaSeleccionada = DirectCast(Me._ventana.MarcaSeleccionada, Marca)
                    Me.Navegar(New ConsultarMarca(marcaSeleccionada, Me._ventana))
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

        Sub ConsultarPatenteSeleccionada()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._ventana.PatenteSeleccionada IsNot Nothing Then
                    Dim patenteSeleccionada As Patente
                    patenteSeleccionada = DirectCast(Me._ventana.PatenteSeleccionada, Patente)
                    Me.Navegar(New GestionarPatente(patenteSeleccionada, Me._ventana))
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
