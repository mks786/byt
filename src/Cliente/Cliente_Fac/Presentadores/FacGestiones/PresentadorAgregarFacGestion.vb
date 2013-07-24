Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacGestiones
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.FacGestiones
    Class PresentadorAgregarFacGestion
        Inherits PresentadorBase
        Private _ventana As IAgregarFacGestion
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _FacGestioneservicios As IFacGestionServicios
        Private _asociadosServicios As IAsociadoServicios
        ' 'Private _contactoServicios As IContactoServicios        
        Private _facoperacionesServicios As IFacOperacionServicios
        ' Private _idiomasServicios As IIdiomaServicios
        ' Private _monedasServicios As IMonedaServicios
        Private _asociados As IList(Of Asociado)
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
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarFacGestion)
            Try
                Me._ventana = ventana
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

                Dim FacGestion As New FacGestion()
                FacGestion.Inicial = UsuarioLogeado.Iniciales
                FacGestion.FechaIngreso = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Me._ventana.FacGestion = FacGestion

            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        ''' <summary>
        ''' Método que carga los datos iniciales a mostrar en la página
        ''' </summary>
        Public Sub CargarPagina()
            Mouse.OverrideCursor = Cursors.Wait

            Try
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarFacGestion, Recursos.Ids.AgregarUsuario)
                Me._ventana.MensajeErrorFacGestion = ""

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                Dim Medios As IList(Of MediosGestion) = Me._MediosGestionServicios.ConsultarTodos()
                Dim primerMediosGestion As New MediosGestion()
                primerMediosGestion.Id = ""
                Medios.Insert(0, primerMediosGestion)
                Me._ventana.Medios = Medios

                Dim Conceptos As IList(Of ConceptoGestion) = Me._ConceptoGestionServicios.ConsultarTodos()
                Dim primerConceptoGestion As New ConceptoGestion
                primerConceptoGestion.Id = ""
                Conceptos.Insert(0, primerConceptoGestion)


                Me._ventana.Conceptos = Conceptos

                Me._ventana.ConceptoRespuestas = Conceptos

                'Dim valoraxu As New ListaDatosValores
                'valoraxu.Id = "XTIPOPAGO"
                'Dim valores As IList(Of ListaDatosValores) = Me._ListaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(valoraxu)
                'Me._ventana.Valores = valores


                Me._ventana.FocoPredeterminado()
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

            Me.Navegar(New AgregarFacGestion())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                Dim FacGestion As FacGestion = DirectCast(_ventana.FacGestion, FacGestion)

                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    FacGestion.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                    FacGestion.TipoAsociado = DirectCast(Me._ventana.Asociado, Asociado).TipoCliente
                Else
                    MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If

                If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                    If Me._ventana.Carta.id <> Integer.MinValue Then
                        FacGestion.CodigoResp = DirectCast(Me._ventana.Carta, Carta).Id
                    Else
                        'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                        'Exit Sub
                    End If
                Else                    
                    'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                    'Exit Sub
                End If

                If DirectCast(Me._ventana.Carta_2, Carta) IsNot Nothing Then
                    If Me._ventana.Carta_2.id <> Integer.MinValue Then
                        FacGestion.Respuesta = DirectCast(Me._ventana.Carta_2, Carta).Id
                    Else
                        'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                        'Exit Sub
                    End If
                Else
                    'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                    'Exit Sub
                End If

                'FacGestion.FechaCobro = FormatDateTime(FacGestion.FechaCobro, DateFormat.ShortDate)
                'If FacGestion.FechaB IsNot Nothing Then
                '    FacGestion.FechaB = FormatDateTime(FacGestion.FechaB, DateFormat.ShortDate)
                'End If

                'If Not Me._FacCreditoServicios.VerificarExistencia(FacCredito) Then   
                If DirectCast(Me._ventana.Medio, MediosGestion) IsNot Nothing Then
                    FacGestion.Medio = DirectCast(Me._ventana.Medio, MediosGestion).Id
                End If
                If DirectCast(Me._ventana.Concepto, ConceptoGestion) IsNot Nothing Then
                    FacGestion.ConceptoGestion = DirectCast(Me._ventana.Concepto, ConceptoGestion).Id
                End If
                If DirectCast(Me._ventana.ConceptoRespuesta, ConceptoGestion) IsNot Nothing Then
                    FacGestion.ConceptoGestion2 = DirectCast(Me._ventana.ConceptoRespuesta, ConceptoGestion).Id
                End If

                If FacGestion.FechaGestion Is Nothing Then
                    MessageBox.Show("Ingrese Fecha Gestion", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
                'If DirectCast(Me._ventana.Idioma, Idioma) IsNot Nothing Then
                '    FacGestion.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)
                'End If
                'If DirectCast(Me._ventana.Banco, FacBanco) IsNot Nothing Then
                '    FacGestion.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'End If

                'para el contador
                'Dim contador As New ContadorFac
                'contador.Id = "FAC_COBROS"
                'contador = _contadorfacServicios.ConsultarPorId(contador)
                'FacGestion.Id = contador.ProximoValor
                'contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
                'Dim exitocontador As Boolean = _contadorfacServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
                    'fin contador                   
                FacGestion.Operacion = "CREATE"
                FacGestion.Inicial = UsuarioLogeado.Iniciales
                    Dim exitoso As Boolean = _FacGestioneservicios.InsertarOModificar(FacGestion, UsuarioLogeado.Hash)

                    If exitoso Then
                    Mouse.OverrideCursor = Nothing
                        'Me.Navegar(Recursos.MensajesConElUsuario.fac_FacGestion Insertado, False)
                    If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacGestionInsertado & " Modificar Gestion?", "Modificar Gestion", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                        IrConsultarFacGestion(FacGestion)
                    Else
                        Me.Navegar(_paginaPrincipal)
                    End If
                    End If
                    'Else
                    'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorFacGestion Repetido)
                    ' End If
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
            End Try
        End Sub

        Public Sub Ver_Carta_Gestion()

            If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                If Me._ventana.Carta.id <> Integer.MinValue Then
                    Dim ag As New Mostrar_Detalle_Carta(Me._ventana.Carta)
                    'ag.Owner = Me
                    ag.ShowDialog()
                Else
                    MessageBox.Show("Debe especificar una gestion ", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
            Else
                MessageBox.Show("Debe especificar una gestion ", "Error", MessageBoxButton.OK)
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If
        End Sub

        Public Sub Ver_Carta_Respuesta()
            If DirectCast(Me._ventana.Carta_2, Carta) IsNot Nothing Then
                If Me._ventana.Carta_2.id <> Integer.MinValue Then
                    Dim ag As New Mostrar_Detalle_Carta(Me._ventana.Carta_2)
                    'ag.Owner = Me
                    ag.ShowDialog()
                Else
                    MessageBox.Show("Debe especificar una Respuesta ", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
            Else
                MessageBox.Show("Debe especificar una Respuesta ", "Error", MessageBoxButton.OK)
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If
        End Sub

        Public Sub IrConsultarFacGestion(ByVal FacGestion As FacGestion)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacGestion(FacGestion))
            'Me.Navegar(New ConsultarFacGestion ())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Function buscar_tasa(ByVal fecha As DateTime, ByVal moneda As String) As Tasa
            Dim tasa As New Tasa()
            tasa.Id = fecha
            tasa.Moneda = moneda
            Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            If tasas.Count > 0 Then
                Return (tasas(0))
            Else
                Return (Nothing)
            End If

        End Function

        Public Sub MaxGestion()
            Dim gestionaux As New FacGestion
            gestionaux.Asociado = Me._ventana.Asociado
            gestionaux.Status = 1
            Dim gestiones As IList(Of FacGestion) = _FacGestioneservicios.ObtenerFacGestionesFiltro(gestionaux)
            If gestionaux IsNot Nothing Then
                If gestiones.Count < 1 Then
                    Me._ventana.Id = 1
                Else
                    If gestiones.Count >= 1 Then
                        Me._ventana.Id = gestiones(0).Id + 1
                    End If
                End If
            Else
                Me._ventana.Id = 1
            End If
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
                If asociados Is Nothing Then
                    Me._ventana.Asociados = Nothing
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")                    
                    Exit Sub
                Else
                    If asociados.Count < 1 Then
                        Me._ventana.Asociados = Nothing
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")                        
                        Exit Sub
                    End If
                End If
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
                '----forma vieja
                'Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
                'Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Nombre

                '--forma nueva
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

                MaxGestion()

                Dim TipoClientes As IList(Of TipoCliente) = Me._TipoClienteServicios.ConsultarTodos()
                Me._ventana.TipoClientes = TipoClientes
                Me._ventana.TipoCliente = Me.BuscarTipoCliente(TipoClientes, DirectCast(Me._ventana.Asociado, Asociado).TipoCliente)

                'Dim Monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Me._ventana.Monedas = Monedas
                'Me._ventana.Moneda = Me.BuscarMoneda(Monedas, DirectCast(Me._ventana.Asociado, Asociado).Moneda)

            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub BuscarCarta()
            Mouse.OverrideCursor = Cursors.Wait
            Dim i As Boolean = False
            Dim cartas As List(Of Carta) = Nothing
            'If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
            Dim cartaaux As New Carta

            'Dim CartasFiltrados As IEnumerable(Of Carta) = Me._Cartas

            If Not String.IsNullOrEmpty(Me._ventana.idCartaFiltrar) Then
                cartaaux.Id = Integer.Parse(Me._ventana.idCartaFiltrar)
                'CartasFiltrados = From p In CartasFiltrados Where p.Id = Integer.Parse(Me._ventana.idCartaFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreCartaFiltrar) Then
                cartaaux.Medio = UCase(Me._ventana.NombreCartaFiltrar)
                'CartasFiltrados = From p In CartasFiltrados Where p.Medio IsNot Nothing AndAlso p.Medio.ToLower().Contains(Me._ventana.NombreCartaFiltrar.ToLower())
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.FechaCartaFiltrar) Then
                cartaaux.Fecha = Me._ventana.FechaCartaFiltrar
            End If

            'cartaaux.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            If i = True Then
                cartas = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
                'Else
                If cartas Is Nothing Then
                    Me._ventana.Cartas = Nothing
                    MessageBox.Show("Error: No Existe Carta Relacionado a la Búsqueda")
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                Else
                    If cartas.Count < 1 Then
                        Me._ventana.Cartas = Nothing
                        MessageBox.Show("Error: No Existe Carta Relacionado a la Búsqueda")
                        Mouse.OverrideCursor = Nothing
                        Exit Sub
                    End If
                End If
            Else
                Me._ventana.Cartas = Nothing
                MessageBox.Show("Error: No Existe Carta Relacionado a la Búsqueda")
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If
            'End If

            Dim primercarta As New Carta()
            primercarta.Id = Integer.MinValue
            cartas.Insert(0, primercarta)

            Me._ventana.Cartas = cartas
            Mouse.OverrideCursor = Nothing
            'If CartasFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Cartas = CartasFiltrados
            'Else
            '    Me._ventana.Cartas = Me._Cartas
            'End If
        End Sub

        Public Sub CambiarCarta()
            Try
                If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                    If Me._ventana.Carta.id <> Integer.MinValue Then
                        'Dim carta As List(Of Carta) = Me._cartasServicios.ObtenerCartasFiltro(DirectCast(Me._ventana.Carta, Carta))
                        Me._ventana.NombreCarta = DirectCast(Me._ventana.Carta, Carta).Id & " - " & DirectCast(Me._ventana.Carta, Carta).Medio & " - " & FormatDateTime(DirectCast(Me._ventana.Carta, Carta).Fecha, DateFormat.ShortDate)
                    End If
                Else
                    Me._ventana.NombreCarta = Nothing
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub BuscarCarta_2()
            Mouse.OverrideCursor = Cursors.Wait
            Dim i As Boolean = False
            Dim Cartas_2 As List(Of Carta) = Nothing
            'If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
            Dim Carta_2aux As New Carta

            'Dim Carta_2sFiltrados As IEnumerable(Of Carta_2) = Me._Carta_2s

            If Not String.IsNullOrEmpty(Me._ventana.idCarta_2Filtrar) Then
                Carta_2aux.Id = Integer.Parse(Me._ventana.idCarta_2Filtrar)
                'Carta_2sFiltrados = From p In Carta_2sFiltrados Where p.Id = Integer.Parse(Me._ventana.idCarta_2Filtrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreCarta_2Filtrar) Then
                Carta_2aux.Medio = UCase(Me._ventana.NombreCarta_2Filtrar)
                'Carta_2sFiltrados = From p In Carta_2sFiltrados Where p.Medio IsNot Nothing AndAlso p.Medio.ToLower().Contains(Me._ventana.NombreCarta_2Filtrar.ToLower())
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.FechaCarta_2Filtrar) Then
                Carta_2aux.Fecha = Me._ventana.FechaCarta_2Filtrar
            End If

            'Carta_2aux.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            If i = True Then
                Cartas_2 = Me._cartasServicios.ObtenerCartasFiltro(Carta_2aux)
                'Else
                If Cartas_2 Is Nothing Then
                    Me._ventana.Cartas_2 = Nothing
                    MessageBox.Show("Error: No Existe Carta_2 Relacionado a la Búsqueda")
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                Else
                    If Cartas_2.Count < 1 Then
                        Me._ventana.Cartas_2 = Nothing
                        MessageBox.Show("Error: No Existe Carta_2 Relacionado a la Búsqueda")
                        Mouse.OverrideCursor = Nothing
                        Exit Sub
                    End If
                End If
            Else
                Me._ventana.Cartas_2 = Nothing
                MessageBox.Show("Error: No Existe Carta_2 Relacionado a la Búsqueda")
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If
            'End If

            Dim primerCarta_2 As New Carta()
            primerCarta_2.Id = Integer.MinValue
            Cartas_2.Insert(0, primerCarta_2)

            Me._ventana.Cartas_2 = Cartas_2
            Mouse.OverrideCursor = Nothing
            'If Carta_2sFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Carta_2s = Carta_2sFiltrados
            'Else
            '    Me._ventana.Carta_2s = Me._Carta_2s
            'End If
        End Sub

        Public Sub CambiarCarta_2()
            Try
                If DirectCast(Me._ventana.Carta_2, Carta) IsNot Nothing Then
                    If Me._ventana.Carta_2.id <> Integer.MinValue Then
                        'Dim Carta_2 As List(Of Carta) = Me._cartasServicios.ObtenerCartasFiltro(DirectCast(Me._ventana.Carta_2, Carta))
                        Dim carta As Carta = DirectCast(Me._ventana.Carta_2, Carta)
                        Me._ventana.NombreCarta_2 = carta.Id
                        If carta.Medio IsNot Nothing Then
                            Me._ventana.NombreCarta_2 = Me._ventana.NombreCarta_2 & " - " & carta.Medio
                        End If
                        If (Not carta.Fecha.Equals(DateTime.MinValue)) Then
                            Me._ventana.NombreCarta_2 = Me._ventana.NombreCarta_2 & " - " & FormatDateTime(carta.Fecha, DateFormat.ShortDate)
                        End If
                    End If
                Else
                    Me._ventana.NombreCarta_2 = Nothing
                End If
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

    End Class
End Namespace
