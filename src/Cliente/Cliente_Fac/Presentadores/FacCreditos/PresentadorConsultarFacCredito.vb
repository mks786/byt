Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCreditos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacCreditos
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Namespace Presentadores.FacCreditos
    Class PresentadorConsultarFacCredito
        Inherits PresentadorBase
        Private _ventana As IConsultarFacCredito
        Private _FacCreditoServicios As IFacCreditoServicios

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios
        'Private _contactoServicios As IContactoServicios
        Private _bancosServicios As IFacBancoServicios
        Private _formasServicios As IFacFormaServicios
        Private _facoperacionesServicios As IFacOperacionServicios       
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios
        Private _tasasServicios As ITasaServicios
        Private _asociados As IList(Of Asociado)
        Private _FacFormas As IList(Of FacForma)
        Private _faccredito As FacCredito
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacCredito">FacCredito a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarFacCredito, ByVal FacCredito As Object)
            Try
                Me._ventana = ventana
                Me._ventana.FacCredito = FacCredito
                _faccredito = FacCredito
                'Me._ventana.Region = DirectCast(Me._ventana.FacCredito, FacCredito).Region
                Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)                
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._formasServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)
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
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacCredito, Recursos.Ids.fac_ConsultarFacCredito)

                Dim FacCredito As FacCredito = DirectCast(Me._ventana.FacCredito, FacCredito)

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados
                'Me._ventana.Asociado = Me.BuscarAsociado(Me._asociados, FacCredito.Asociado)
                'Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Nombre

                Dim asociadoaux As New Asociado
                Dim asociado As List(Of Asociado)
                If FacCredito.Asociado IsNot Nothing Then
                    asociadoaux.Id = FacCredito.Asociado.Id
                    asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                    Me._ventana.Asociados = asociado
                    Me._ventana.Asociado = asociado(0)
                    Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
                End If

                Dim FacFormaAuxiliar As New FacForma()
                FacFormaAuxiliar.Credito = DirectCast(Me._ventana.FacCredito, FacCredito)
                'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
                Me._FacFormas = Me._formasServicios.ObtenerFacFormasFiltro(FacFormaAuxiliar)
                Me._ventana.Resultados = Me._FacFormas

                Dim bancos As IList(Of FacBanco) = Me._bancosServicios.ObtenerFacBancosFiltro(Nothing)
                Me._ventana.Bancos = bancos
                'Me._ventana.Banco = FacCredito.Banco
                Me._ventana.Banco = Me.BuscarFacBanco(bancos, FacCredito.Banco)

                Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                Me._ventana.Idiomas = idiomas
                Me._ventana.Idioma = Me.BuscarIdioma(idiomas, FacCredito.Idioma)

                Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                Me._ventana.Monedas = monedas
                Me._ventana.Moneda = Me.BuscarMoneda(monedas, FacCredito.Moneda)


                Me._ventana.BCredito = FacCredito.BCredito

                Me._ventana.BCreditoBf = FacCredito.BCreditoBf

                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Sub pararegresar()
            If _faccredito.Accion = 1 Then
                LimpiarAgregar()
            Else
                Regresar()
            End If
        End Sub

        Public Sub LimpiarAgregar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New AgregarFacCredito())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        ''' <summary>
        ''' Método que dependiendo del estado de la página, habilita los campos o 
        ''' modifica los datos del usuario
        ''' </summary>
        Public Sub Modificar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                'Habilitar campos
                If Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar Then
                    Me._ventana.HabilitarCampos = True
                    Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar
                Else

                    'Modifica los datos del FacCredito
                    Dim FacCredito As FacCredito = DirectCast(Me._ventana.FacCredito, FacCredito)

                    If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing And DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                        FacCredito.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                    Else
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                        Exit Sub
                    End If

                    'FacCredito.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)

                    'If Not Me._FacCreditoServicios.VerificarExistencia(FacCredito) Then
                    'FacCredito.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                    'FacCredito.Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    'FacCredito.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)


                    If DirectCast(Me._ventana.Moneda, Moneda) IsNot Nothing And DirectCast(Me._ventana.Moneda, Moneda).Id <> "" Then
                        FacCredito.Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    End If
                    If DirectCast(Me._ventana.Idioma, Idioma) IsNot Nothing And DirectCast(Me._ventana.Idioma, Idioma).Id <> "" Then
                        FacCredito.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)
                    End If
                    If DirectCast(Me._ventana.Banco, FacBanco) IsNot Nothing And DirectCast(Me._ventana.Banco, FacBanco).Id > Integer.MinValue Then
                        FacCredito.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                    Else
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Banco requerido", "Error", MessageBoxButton.OK)
                        Exit Sub
                    End If


                    FacCredito.Operacion = "UPDATE"
                    FacCredito.BCreditoBf = Me._ventana.BCreditoBf
                    FacCredito.BCredito = Me._ventana.BCredito

                    If Me._FacCreditoServicios.InsertarOModificar(FacCredito, UsuarioLogeado.Hash) Then


                        Dim operacion As New FacOperacion
                        operacion.Id = "NC"
                        operacion.CodigoOperacion = FacCredito.Id
                        operacion.FechaOperacion = FacCredito.FechaCredito
                        operacion.Asociado = FacCredito.Asociado
                        operacion.Idioma = FacCredito.Idioma
                        operacion.Moneda = FacCredito.Moneda
                        operacion.Monto = FacCredito.BCredito
                        operacion.Saldo = FacCredito.BCredito
                        operacion.MontoBf = FacCredito.BCreditoBf
                        operacion.SaldoBf = FacCredito.BCreditoBf
                        operacion.XOperacion = FacCredito.Concepto
                        _facoperacionesServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash)

                        'Dim operacion As New FacOperacion                        
                        'operacion.CodigoOperacion = FacCredito.Id
                        'operacion.XOperacion = FacCredito.Concepto
                        '_facoperacionesServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash)

                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacCreditoModificado
                        'Me.Navegar(_paginaPrincipal)
                        MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacCreditoModificado, "Modificado")
                    End If
                End If

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
            End Try
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacCredito(_faccredito))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub BuscarTasa()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If

                Dim moneda As Moneda
                moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                Dim FacCredito As FacCredito = DirectCast(_ventana.FacCredito, FacCredito)
                Dim tasa As New Tasa()
                tasa.Id = FacCredito.FechaCredito
                tasa.Moneda = moneda.Id

                Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
                Me._ventana.BSaldo = Me._ventana.BCredito
                Dim bcredito As Double = Me._ventana.BCredito
                If tasas.Count > 0 Then
                    Dim valortasa As Double = tasas(0).Tasabf
                    Dim BCreditoBf = bcredito * valortasa
                    BCreditoBf = BCreditoBf
                    Me._ventana.BCreditoBf = BCreditoBf
                Else
                    Me._ventana.BCreditoBf = bcredito
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
            End Try
        End Sub

        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._FacCreditoServicios.Eliminar(DirectCast(Me._ventana.FacCredito, FacCredito), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacCreditoEliminado
                    Me.Navegar(_paginaPrincipal)
                End If

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
            End Try
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
                'Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
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
                'Me._ventana.Idiomas = Nothing
                'Me._ventana.Monedas = Nothing
                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Me._ventana.Idiomas = idiomas
                'Me._ventana.Monedas = monedas
                'Me._ventana.Idiomas = asociado.Idioma
                'Me._ventana.Monedas = asociado.Moneda
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

    End Class
End Namespace
