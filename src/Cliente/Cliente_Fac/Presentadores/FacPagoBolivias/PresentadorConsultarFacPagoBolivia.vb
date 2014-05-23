Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacPagoBolivias
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacPagoBolivias
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacGestiones
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Namespace Presentadores.FacPagoBolivias
    Class PresentadorConsultarFacPagoBolivia
        Inherits PresentadorBase

        Private _ventana As IConsultarFacPagoBolivia
        Private _FacPagoBoliviaServicios As IFacPagoBoliviaServicios
        'Private _AsociadoServicios As IAsociadoServicios
        Private _BancoGServicios As IBancoGServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _facbancosServicios As IFacBancoServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _FacPagoBolivia As FacPagoBolivia        
        Private _cartasServicios As ICartaServicios
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        ''' <param name="FacPagoBolivia">FacPagoBolivia a mostrar</param>
        Public Sub New(ByVal ventana As IConsultarFacPagoBolivia, ByVal FacPagoBolivia As Object)
            Try
                Me._ventana = ventana

                _FacPagoBolivia = DirectCast(FacPagoBolivia, FacPagoBolivia)

                Me._ventana.FacPagoBolivia = FacPagoBolivia
                Me._ventana.SetFormaPago = ""
                Me._ventana.SetFormaPago = BuscarFormaPago(FacPagoBolivia.PagoPag)

                Me._ventana.SetFormaPago = ""
                Me._ventana.SetFormaPago = BuscarFormaPago(FacPagoBolivia.PagoPag)

                Me._ventana.SetTipoPago = ""
                Me._ventana.SetTipoPago = BuscarTipoPago(FacPagoBolivia.PagoRec)


                'Me._ventana.Region = DirectCast(Me._ventana.FacPagoBolivia, FacPagoBolivia).Region
                Me._FacPagoBoliviaServicios = DirectCast(Activator.GetObject(GetType(IFacPagoBoliviaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacPagoBoliviaServicios")), IFacPagoBoliviaServicios)
                'Me._AsociadoServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._cartasServicios = DirectCast(Activator.GetObject(GetType(ICartaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("CartaServicios")), ICartaServicios)
                Me._BancoGServicios = DirectCast(Activator.GetObject(GetType(IBancoGServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("bancogServicios")), IBancoGServicios)
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

            Me.Navegar(New ConsultarFacPagoBolivia(_FacPagoBolivia))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacPagoBolivia, Recursos.Ids.fac_ConsultarFacPagoBolivia)

                Dim FacPagoBolivia As FacPagoBolivia = DirectCast(Me._ventana.FacPagoBolivia, FacPagoBolivia)

                Me._ventana.MontoBol = FacPagoBolivia.MontoBol
                Me._ventana.MontoRec = FacPagoBolivia.MontoRec

                Dim asociadoaux As New Asociado
                Dim asociado As List(Of Asociado)
                If FacPagoBolivia.Id IsNot Nothing Then
                    asociadoaux.Id = FacPagoBolivia.Id.Id
                    asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                    Me._ventana.Asociados = asociado
                    Me._ventana.Asociado = asociado(0)
                    Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
                End If

                'Dim Carta As List(Of Carta)
                If FacPagoBolivia.Carta IsNot Nothing Then
                    Dim cartaaux As New Carta
                    cartaaux.Id = FacPagoBolivia.Carta.Id
                    Dim carta As List(Of Carta) = Me._cartasServicios.ObtenerCartasFiltro(cartaaux)
                    Me._ventana.Cartas = carta
                    Me._ventana.Carta = carta(0)
                    Me._ventana.NombreCarta = carta(0).Id & " - " & carta(0).Medio & " - " & FormatDateTime(carta(0).Fecha, DateFormat.ShortDate)
                End If

                Dim bancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)
                Me._ventana.BancosRec = bancos
                'Me._ventana.Banco = FacCredito.Banco                
                Me._ventana.BancoRec = Me.BuscarFacBanco(bancos, FacPagoBolivia.BancoRec)
                'Me._ventana.Banco = Me.BuscarBancoG(bancos, FacPagoBolivia.Banco)

                Dim bancospag As IList(Of BancoG) = Me._BancoGServicios.ConsultarTodos()
                Me._ventana.Bancos = bancospag
                'Me._ventana.Banco = FacCredito.Banco
                Me._ventana.Banco = Me.BuscarBancoG(bancospag, FacPagoBolivia.BancoPag)

                If (FacPagoBolivia.FechaPago IsNot Nothing) And (FacPagoBolivia.FechaPago <> Date.MinValue) Then
                    Me._ventana.FechaPagoBolivia = FacPagoBolivia.FechaPago
                Else
                    Me._ventana.FechaPagoBolivia = Nothing
                End If

                'Me._ventana.FechaPagoBolivia = Date.Today

                Me._ventana.FocoPredeterminado()
                Me._ventana.HabilitarCampos = False
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

                    'Modifica los datos del FacPagoBolivia
                    Dim FacPagoBolivia As FacPagoBolivia = DirectCast(Me._ventana.FacPagoBolivia, FacPagoBolivia)
                    'FacPagoBolivia.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)

                    FacPagoBolivia.MontoRec = Me._ventana.MontoRec
                    FacPagoBolivia.MontoBol = Me._ventana.MontoBol

                    If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing And DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                        FacPagoBolivia.Id = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                    End If


                    'If Not Me._FacPagoBoliviaServicios.VerificarExistencia(FacPagoBolivia) Then
                    If DirectCast(Me._ventana.BancoRec, FacBanco) IsNot Nothing Then
                        FacPagoBolivia.BancoRec = If(Not DirectCast(Me._ventana.BancoRec, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.BancoRec, FacBanco), Nothing)
                    End If
                    FacPagoBolivia.PagoRec = _ventana.TipoPago
                    If DirectCast(Me._ventana.Banco, BancoG) IsNot Nothing Then
                        FacPagoBolivia.BancoPag = If(Not DirectCast(Me._ventana.Banco, BancoG).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, BancoG), Nothing)
                    End If
                    FacPagoBolivia.PagoPag = _ventana.GetFormaPago
                    If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                        If Me._ventana.Carta.id <> Integer.MinValue Then
                            FacPagoBolivia.Carta = DirectCast(Me._ventana.Carta, Carta)
                        Else
                            'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                            'Exit Sub
                        End If
                    Else
                        'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                        'Exit Sub
                    End If


                    If Me._FacPagoBoliviaServicios.InsertarOModificar(FacPagoBolivia, UsuarioLogeado.Hash) Then
                        '_paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacPagoBoliviaModificado
                        'Me.Navegar(_paginaPrincipal)
                        MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacPagoBoliviaModificado, "Modificado")
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

        Public Sub MontoRec_LostFocus()
            Me._ventana.MontoRec = Me._ventana.MontoRec
            Me._ventana.MontoBol = Me._ventana.MontoRec
        End Sub

        Public Sub Ver_Carta()

            If DirectCast(Me._ventana.Carta, Carta) IsNot Nothing Then
                If Me._ventana.Carta.id <> Integer.MinValue Then
                    Dim ag As New Mostrar_Detalle_Carta(Me._ventana.Carta)
                    'ag.Owner = Me
                    ag.ShowDialog()
                Else
                    MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If
            Else
                MessageBox.Show("Debe especificar una Carta ", "Error", MessageBoxButton.OK)
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If
        End Sub

        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._FacPagoBoliviaServicios.Eliminar(DirectCast(Me._ventana.FacPagoBolivia, FacPagoBolivia), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_FacPagoBoliviaEliminado
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
                'Me._ventana.Personas = asociado.Contactos
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
    End Class
End Namespace
