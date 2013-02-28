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
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.ViGestionAsociados
    Class PresentadorConsultarFacGestionesAsociado
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarFacGestionesAsociado
        Private _FacGestioneservicios As IFacGestionServicios
        Private _asociadosServicios As IAsociadoServicios
        ' 'Private _contactoServicios As IContactoServicios        
        Private _facoperacionesServicios As IFacOperacionServicios
        ' Private _idiomasServicios As IIdiomaServicios
        ' Private _monedasServicios As IMonedaServicios
        Private _asociado As Asociado

        'Private _FacOperaciones As IList(Of FacOperacion)
        Private _tasasServicios As ITasaServicios        
        ' Private _FacCreditoServicios As IFacCreditoServicios
        'Private _contadorfacServicios As IContadorFacServicios
        ' Private _bancosServicios As IFacBancoServicios
        ' Private _FacFormaServicios As IFacFormaServicios        
        'Private _ListaDatosValoresServicios As IListaDatosValoresServicios
        Private _cartasServicios As ICartaServicios
        Private _MediosGestionServicios As IMediosGestionServicios
        Private _ConceptoGestionServicios As IConceptoGestionServicios
        Private _TipoClienteServicios As ITipoClienteServicios
        'Private _asociado2 As Asociado
        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarFacGestionesAsociado, ByVal Asociado As Object)
            Try
                Me._ventana = ventana
                _asociado = DirectCast(Asociado, Asociado)               
                'Me._ventana.FacGestion  = New FacGestion ()
                Me._FacGestioneservicios = DirectCast(Activator.GetObject(GetType(IFacGestionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacGestionServicios")), IFacGestionServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                ' Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                ' Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                ' Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                ' Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                '  Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                '  Me._FacFormaServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)               
                '  Me._ListaDatosValoresServicios = DirectCast(Activator.GetObject(GetType(IListaDatosValoresServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ListaDatosValoresServicios")), IListaDatosValoresServicios)
                Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                Me._MediosGestionServicios = DirectCast(Activator.GetObject(GetType(IMediosGestionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MediosGestionServicios")), IMediosGestionServicios)
                Me._ConceptoGestionServicios = DirectCast(Activator.GetObject(GetType(IConceptoGestionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ConceptoGestionServicios")), IConceptoGestionServicios)
                Me._TipoClienteServicios = DirectCast(Activator.GetObject(GetType(ITipoClienteServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TipoClienteServicios")), ITipoClienteServicios)

                '_asociado2 = Asociado
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

            Me.Navegar(New ConsultarFacGestionesAsociado(_asociado))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_menuItemFacGestionesAsociado, Recursos.Ids.fac_menuItemFacGestionesAsociado)
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

                'Dim FacGestionAuxiliar As New FacGestion()
                ActualizarTitulo()

                'Me._FacGestiones = Me._FacGestioneservicios.ConsultarTodos()
                'Me._FacGestiones = Me._FacGestioneservicios.ObtenerFacGestionesFiltro(FacGestion Auxiliar)

                'Me._ventana.Resultados = Nothing
                'Me._ventana.FacGestionFiltrar = New FacGestion

                'Dim Medios As IList(Of MediosGestion) = Me._MediosGestionServicios.ConsultarTodos()
                'Dim primerMediosGestion As New MediosGestion()
                'primerMediosGestion.Id = ""
                'Medios.Insert(0, primerMediosGestion)
                'Me._ventana.Medios = Medios

                'Dim Conceptos As IList(Of ConceptoGestion) = Me._ConceptoGestionServicios.ConsultarTodos()
                'Dim primerConceptoGestion As New ConceptoGestion
                'primerConceptoGestion.Id = ""
                'Conceptos.Insert(0, primerConceptoGestion)


                'Me._ventana.Conceptos = Conceptos

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                'Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ConsultarTodos()
                'Dim primerafacbanco As New FacBanco()
                'primerafacbanco.Id = Integer.MinValue
                'facbancos.Insert(0, primerafacbanco)
                'Me._ventana.Bancos = facbancos

                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim primeridioma As New Idioma()
                'primeridioma.Id = ""
                'idiomas.Insert(0, primeridioma)
                'Me._ventana.Idiomas = idiomas

                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Dim primeramoneda As New Moneda()
                'primeramoneda.Id = ""
                'monedas.Insert(0, primeramoneda)
                'Me._ventana.Monedas = monedas
                _asociado = consultarAsociado(_asociado.Id)
                Me._ventana.FocoPredeterminado()
                Me._ventana.Asociado = _asociado.Id & " - " & _asociado.Nombre

                Dim domicilio As String
                domicilio = Replace(_asociado.Domicilio, vbTab, " ")
                domicilio = Replace(domicilio, vbCrLf, " ")
                domicilio = Replace(domicilio, vbLf, " ")
                Me._ventana.AsociadoDomicilio = domicilio                

                Dim TipoClientes As IList(Of TipoCliente) = Me._TipoClienteServicios.ConsultarTodos()
                Dim tipocliente As TipoCliente
                tipocliente = Me.BuscarTipoCliente(TipoClientes, _asociado.TipoCliente)
                Me._ventana.AsociadoTipo = tipocliente.Id & " - " & tipocliente.Descripcion

                Consultar()
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


        Public Function consultarAsociado(ByVal id As Integer) As Asociado
            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing


            Mouse.OverrideCursor = Cursors.Wait


            asociadoaux.Id = id
            asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Mouse.OverrideCursor = Nothing

            If asociados IsNot Nothing Then
                If asociados.Count > 0 Then
                    Return (asociados(0))
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

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
                Mouse.OverrideCursor = Cursors.Wait
                'Dim filtroValido As Boolean = False
                'Dim filtroValido As Integer = 0
                'Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                'dos filtros sean utilizados
                Dim FacGestionAuxiliar As New FacGestion()
                ' Dim FacGestiones As FacGestion = DirectCast(_ventana.FacGestionFiltrar, FacGestion)

                'If Me._ventana.Id IsNot Nothing And Me._ventana.Id <> "" Then
                '    FacGestionAuxiliar.Id = Integer.Parse(Me._ventana.Id)
                'End If

                'If Not Me._ventana.CreditoSent.Equals("") Then
                '    FacGestion Auxiliar.CreditoSent = Integer.Parse(Me._ventana.CreditoSent)
                'End If

                'If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                FacGestionAuxiliar.Asociado = _asociado
                'End If

                'If (Me._ventana.Medio IsNot Nothing) AndAlso (DirectCast(Me._ventana.Medio, MediosGestion).Id <> "") Then
                '    FacGestionAuxiliar.Medio = DirectCast(Me._ventana.Medio, MediosGestion).Id
                'End If

                'If (Me._ventana.Concepto IsNot Nothing) AndAlso (DirectCast(Me._ventana.Concepto, ConceptoGestion).Id <> "") Then
                '    FacGestionAuxiliar.ConceptoGestion = DirectCast(Me._ventana.Concepto, ConceptoGestion).Id
                'End If

                'If (Me._ventana.Idioma IsNot Nothing) AndAlso (DirectCast(Me._ventana.Idioma, Idioma).Id <> "") Then
                '    FacGestionAuxiliar.Idioma = DirectCast(Me._ventana.Idioma, Idioma))
                'End If

                'If (Me._ventana.Moneda IsNot Nothing) AndAlso (DirectCast(Me._ventana.Moneda, Moneda).Id <> "") Then
                '    FacGestionAuxiliar.Moneda = DirectCast(Me._ventana.Moneda, Moneda))
                'End If

                'If Not Me._ventana.FechaGestion.Equals("") Then
                '    Dim FechaFacGestion As DateTime = DateTime.Parse(Me._ventana.FechaGestion)
                '    FacGestionAuxiliar.FechaGestion = FechaFacGestion
                'End If

                'If (filtroValido = True) Then
                Dim facgestiones As List(Of FacGestion) = Me._FacGestioneservicios.ObtenerFacGestionesFiltro(FacGestionAuxiliar)
                Me._ventana.Resultados = Nothing
                'Me._ventana.Count = facgestiones.Count
                Me._ventana.Resultados = facgestiones
                Mouse.OverrideCursor = Nothing
                'Else
                '    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto)
                'End If

                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                Mouse.OverrideCursor = Nothing
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub Ver_Carta(ByVal id As Integer?)
            If IsNumeric(id) And id <> 0 Then
                Dim carta As Carta = BuscarCartas(id)

                If carta IsNot Nothing Then
                    Dim ag As New Mostrar_Detalle_Carta(carta)
                    ag.ShowDialog()
                Else
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Debe especificar una gestion ", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

            Else
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Debe especificar una gestion ", "Error", MessageBoxButton.OK)
                Exit Sub
            End If
        End Sub

        Public Function BuscarCartas(ByVal id As Integer) As Carta
            Mouse.OverrideCursor = Cursors.Wait
            Dim cartas As List(Of Carta)
            Dim cartaaux As New Carta
            cartaaux.Id = id
            cartas = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
            Mouse.OverrideCursor = Nothing
            If cartas IsNot Nothing Then
                If cartas.Count > 0 Then
                    Return cartas(0)
                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Método que invoca una nueva página "ConsultarFacGestion " y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacGestion()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Me.Navegar(New ConsultarFacGestion(Me._ventana.FacGestionSeleccionado))
                'Me.Navegar(New ConsultarFacGestion ())
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

        ''' <summary>
        ''' Método que ordena una columna
        ''' </summary>
        'Public Sub OrdenarColumna2(ByVal column As GridViewColumnHeader)
        '    '#Region "trace"
        '    If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
        '        logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '    End If
        '    '#End Region

        '    Dim field As [String] = TryCast(column.Tag, [String])
        '    Try
        '        If Me._ventana.CurSortCol IsNot Nothing Then
        '            AdornerLayer.GetAdornerLayer(Me._ventana.CurSortCol).Remove(Me._ventana.CurAdorner)
        '            Me._ventana.ListaResultados.Items.SortDescriptions.Clear()
        '        End If

        '        Dim newDir As ListSortDirection = ListSortDirection.Ascending
        '        'If Me._ventana.CurSortCol = column AndAlso Me._ventana.CurAdorner.Direction = newDir Then
        '        '    newDir = ListSortDirection.Descending
        '        'End If

        '        Me._ventana.CurSortCol = column
        '        Me._ventana.CurAdorner = New SortAdorner(Me._ventana.CurSortCol, newDir)
        '        AdornerLayer.GetAdornerLayer(Me._ventana.CurSortCol).Add(Me._ventana.CurAdorner)
        '        Me._ventana.ListaResultados.Items.SortDescriptions.Add(New SortDescription(field, newDir))

        '        '#Region "trace"
        '        If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
        '            logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
        '        End If
        '        '#End Region
        '    Catch ex As Exception
        '        logger.[Error](ex.Message)
        '        Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
        '    End Try
        'End Sub

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

        'Public Sub BuscarAsociado2()
        '    Mouse.OverrideCursor = Cursors.Wait
        '    Dim asociadoaux As New Asociado
        '    Dim asociados As List(Of Asociado) = Nothing
        '    Dim i As Boolean = False
        '    'asociadoaux = Nothing
        '    'Dim asociadosFiltrados As IEnumerable(Of Asociado) = Me._asociados
        '    Mouse.OverrideCursor = Cursors.Wait

        '    If Not String.IsNullOrEmpty(Me._ventana.idAsociadoFiltrar) And Me._ventana.idAsociadoFiltrar <> "0" Then
        '        asociadoaux.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
        '        '
        '        '    asociadosFiltrados = From p In asociadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
        '        i = True
        '    End If

        '    If Not String.IsNullOrEmpty(Me._ventana.NombreAsociadoFiltrar) Then
        '        asociadoaux.Nombre = UCase(Me._ventana.NombreAsociadoFiltrar)
        '        '    asociadosFiltrados = From p In asociadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreAsociadoFiltrar.ToLower())
        '        i = True
        '    End If
        '    If i = True Then
        '        asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
        '    Else
        '        Me._ventana.Asociados = Nothing
        '        MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")
        'Exit Sub 
        '    End If

        '    Dim primerasociado As New Asociado()
        '    primerasociado.Id = Integer.MinValue
        '    asociados.Insert(0, primerasociado)

        '    Me._ventana.Asociados = asociados

        '    Mouse.OverrideCursor = Nothing

        '    'If asociadosFiltrados.ToList.Count <> 0 Then
        '    '    Me._ventana.Asociados = asociadosFiltrados
        '    'Else
        '    '    Me._ventana.Asociados = Me._asociados
        '    'End If
        '    Mouse.OverrideCursor = Nothing
        'End Sub

        'Public Sub CambiarAsociado()
        '    Try
        '        'Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
        '        'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
        '        Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
        '        'Me._ventana.Personas = asociado.Contactos
        '    Catch e As ApplicationException
        '        'Me._ventana.Personas = Nothing
        '    End Try
        'End Sub
    End Class
End Namespace