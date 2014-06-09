Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCobros
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacCobros

Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
Imports Trascend.Bolet.Cliente.Ventanas.Asociados



Namespace Presentadores.FacCobros
    Class PresentadorAgregarFacCobro
        Inherits PresentadorBase
        Private _ventana As IAgregarFacCobro        
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _FacCobroServicios As IFacCobroServicios
        Private _asociadosServicios As IAsociadoServicios
        'Private _contactoServicios As IContactoServicios        
        Private _facoperacionesServicios As IFacOperacionServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios        
        Private _asociados As IList(Of Asociado)
        Private _FacOperaciones As IList(Of FacOperacion)
        Private _tasasServicios As ITasaServicios
        Private _FacCreditoServicios As IFacCreditoServicios
        Private _contadorfacServicios As IContadorFacServicios
        Private _bancosServicios As IFacBancoServicios
        Private _FacFormaServicios As IFacFormaServicios
        Private _FacCobroFacturaServicios As IFacCobroFacturaServicios
        Private _ListaDatosValoresServicios As IListaDatosValoresServicios
        Private _FacFacturaServicios As IFacFacturaServicios

        Dim xoperacion As String
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarFacCobro)
            Try
                Me._ventana = ventana
                'Me._ventana.FacCobro = New FacCobro()
                Me._FacCobroServicios = DirectCast(Activator.GetObject(GetType(IFacCobroServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCobroServicios")), IFacCobroServicios)               
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)               
                Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
                Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._FacFormaServicios = DirectCast(Activator.GetObject(GetType(IFacFormaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFormaServicios")), IFacFormaServicios)
                Me._FacCobroFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacCobroFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCobroFacturaServicios")), IFacCobroFacturaServicios)
                Me._ListaDatosValoresServicios = DirectCast(Activator.GetObject(GetType(IListaDatosValoresServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ListaDatosValoresServicios")), IListaDatosValoresServicios)
                Me._FacFacturaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaServicios")), IFacFacturaServicios)

                Dim FacCobro As New FacCobro()
                FacCobro.Timestamp = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Me._ventana.FacCobro = FacCobro

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
                'If (existe_tasa_dia(Date.Now, "US") = True) Then
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarFacCobro, Recursos.Ids.AgregarUsuario)
                Me._ventana.MensajeErrorCobro = ""

                Dim FacCobro As New FacCobro()
                FacCobro.FechaCobro = Date.Now
                ' FacCobro.EstadoCuenta = 0
                Me._ventana.FacCobro = FacCobro

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                'Dim bancos As IList(Of FacBanco) = Me._bancosServicios.ObtenerFacBancosFiltro(Nothing)()
                Dim bancos As IList(Of FacBanco) = Me._bancosServicios.ObtenerFacBancosFiltro(Nothing)
                Dim primerabanco As New FacBanco()
                primerabanco.Id = Integer.MinValue
                bancos.Insert(0, primerabanco)
                Me._ventana.Bancos = bancos

                Dim valoraxu As New ListaDatosValores
                valoraxu.Id = "XTIPOPAGO"
                Dim valores As IList(Of ListaDatosValores) = Me._ListaDatosValoresServicios.ConsultarListaDatosValoresPorParametro(valoraxu)
                Dim primervalor As New ListaDatosValores()
                primervalor.Id = ""
                primervalor.Descripcion = " "
                valores.Insert(0, primervalor)
                Me._ventana.Valores = valores

                Me._ventana.FocoPredeterminado()
                'Else
                'Me.Navegar(Recursos.MensajesConElUsuario.fac_error_tasa_dia, True)
                'End If
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
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                Dim FacCobro As FacCobro = DirectCast(_ventana.FacCobro, FacCobro)

                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing And DirectCast(Me._ventana.Asociado, Asociado).Id > Integer.MinValue Then
                    FacCobro.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)
                Else
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

                If DirectCast(Me._ventana.ResultadosForma, List(Of FacForma)) Is Nothing Then
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Error en los montos", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

                If DirectCast(Me._ventana.ResultadosFacturaCobro, List(Of FacCobroFactura)) Is Nothing Then
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Error en los montos", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

                FacCobro.FechaCobro = FormatDateTime(FacCobro.FechaCobro, DateFormat.ShortDate)
                If FacCobro.FechaB IsNot Nothing Then
                    FacCobro.FechaB = FormatDateTime(FacCobro.FechaB, DateFormat.ShortDate)
                End If

                'If Not Me._FacCreditoServicios.VerificarExistencia(FacCredito) Then   
                If DirectCast(Me._ventana.Moneda, Moneda) IsNot Nothing And DirectCast(Me._ventana.Moneda, Moneda).Id <> "" Then
                    FacCobro.Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                End If
                If DirectCast(Me._ventana.Idioma, Idioma) IsNot Nothing And DirectCast(Me._ventana.Idioma, Idioma).Id <> "" Then
                    FacCobro.Idioma = If(Not DirectCast(Me._ventana.Idioma, Idioma).Id.Equals("NGN"), DirectCast(Me._ventana.Idioma, Idioma), Nothing)
                End If
                If DirectCast(Me._ventana.Banco, FacBanco) IsNot Nothing And DirectCast(Me._ventana.Banco, FacBanco).Id > Integer.MinValue Then
                    FacCobro.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                Else
                    Mouse.OverrideCursor = Nothing
                    MessageBox.Show("Banco requerido", "Error", MessageBoxButton.OK)
                    Exit Sub
                End If

                FacCobro.Timestamp = FormatDateTime(Date.Now, DateFormat.ShortDate)

                Dim valido As Boolean = True
                If (FacCobro.Moneda.Id = "US") Then
                    'If (Me._ventana.SumaBono <> Me._ventana.SumaBforma) Then
                    '    Me._ventana.MensajeErrorCobro = "Error en los montos"
                    '    valido = False
                    'End If
                    If Me._ventana.SumaBono <> Me._ventana.SumaBforma Then
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Error en los montos", "Error", MessageBoxButton.OK)
                        Exit Sub
                    End If
                Else
                    'If (Me._ventana.SumaBonoBf <> Me._ventana.SumaBformaBf) Then
                    '    Me._ventana.MensajeErrorCobro = "Error en los montos"
                    '    valido = False
                    'End If
                    If Me._ventana.SumaBonoBf <> Me._ventana.SumaBformaBf Then
                        Mouse.OverrideCursor = Nothing
                        MessageBox.Show("Error en los montos", "Error", MessageBoxButton.OK)
                        Exit Sub
                    End If
                End If

                If (valido = True) Then

                    'para el contador
                    Dim contador As New ContadorFac
                    contador.Id = "FAC_COBROS"
                    contador = _contadorfacServicios.ConsultarPorId(contador)
                    FacCobro.Id = contador.ProximoValor
                    contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
                    Dim exitocontador As Boolean = _contadorfacServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
                    'fin contador                   

                    Dim FacFormas As List(Of FacForma) = DirectCast(Me._ventana.ResultadosForma, List(Of FacForma))
                    ModificarFacOperacionesForma(FacFormas)

                    Dim FacCobroFactura As List(Of FacCobroFactura) = DirectCast(Me._ventana.ResultadosFacturaCobro, List(Of FacCobroFactura))
                    ModificarFacOperacionesCobroxFactura(FacCobroFactura)
                    FacCobro.Inicial = UsuarioLogeado.Iniciales
                    Dim exitoso As Boolean = _FacCobroServicios.InsertarOModificar(FacCobro, UsuarioLogeado.Hash)

                    If exitoso Then
                        AgregarFacFormaCobro(FacFormas, FacCobro)
                        AgregarFacCobroFacturaCobro(FacCobroFactura, FacCobro)

                        Dim operacion As New FacOperacion
                        operacion.Id = "NP"
                        operacion.CodigoOperacion = FacCobro.Id
                        operacion.FechaOperacion = FacCobro.FechaCobro
                        operacion.Asociado = FacCobro.Asociado
                        operacion.Idioma = FacCobro.Idioma
                        operacion.Moneda = FacCobro.Moneda
                        operacion.Monto = Me._ventana.SumaBono
                        operacion.Saldo = Me._ventana.SumaBono
                        operacion.MontoBf = Me._ventana.SumaBonoBf
                        operacion.SaldoBf = Me._ventana.SumaBonoBf
                        operacion.XOperacion = xoperacion
                        _facoperacionesServicios.InsertarOModificar(operacion, UsuarioLogeado.Hash)

                        Mouse.OverrideCursor = Nothing
                        'Me.Navegar(Recursos.MensajesConElUsuario.fac_FacCobroInsertado, False)
                        If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacCobroInsertado & " Modificar Cobros?", "Modificar Cobros", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                            FacCobro.Accion = 1
                            IrConsultarFacCobro(FacCobro)
                        Else
                            Limpiar()
                        End If
                    End If
                    'Else
                    'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorFacCobroRepetido)
                    ' End If
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

        Function existe_tasa_dia(ByVal fecha As DateTime, ByVal moneda As String) As Boolean
            Dim existe As Boolean = True

            Dim tasa As New Tasa()
            tasa.Id = fecha
            tasa.Moneda = moneda
            Dim tasas As IList(Of Tasa) = _tasasServicios.ObtenerTasasFiltro(tasa)
            If tasas.Count <= 0 Then
                existe = False
            End If
            Return (existe)
        End Function

        Public Sub IrConsultarFacCobro(ByVal faccobro As FacCobro)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacCobro(faccobro))
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New AgregarFacCobro())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub VerFacturas()
            Dim facturas As List(Of FacOperacion) = DirectCast(Me._ventana.ResultadosFactura2, List(Of FacOperacion))
            If facturas Is Nothing Then
                verfacturas_buscar()
            Else
                If facturas.Count > 0 Then
                    If facturas(0).Asociado.Id <> DirectCast(Me._ventana.Asociado, Asociado).Id Then
                        verfacturas_buscar()
                    End If
                End If
            End If
        End Sub

        Public Sub BuscarFactura(ByVal Cfactura As Integer)
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
                Dim FacFacturaAuxiliar As New FacFactura()
                FacFacturaAuxiliar.Id = Cfactura


                Dim FacFactura As FacFactura
                FacFactura = Me._FacFacturaServicios.ObtenerFacFacturasFiltro(FacFacturaAuxiliar)(0)
                If FacFactura IsNot Nothing Then
                    IrConsultarFacFactura(FacFactura)
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


        Public Sub IrConsultarFacFactura(ByVal Factura As FacFactura)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            'Me._ventana.FacFacturaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New ConsultarFacFactura(Factura))
            'Me.Navegar(New ConsultarFacFactura())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        'Public Sub verfacturas_buscar()
        '    Dim FacOperacionAuxiliar As New FacOperacion()
        '    If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
        '        FacOperacionAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
        '    End If
        '    FacOperacionAuxiliar.Id = "ND"
        '    FacOperacionAuxiliar.Saldo = -1
        '    'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
        '    Me._FacOperaciones = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar)
        '    Me._ventana.ResultadosFactura2 = Me._FacOperaciones
        '    Me._ventana.MensajeErrorCobro = ""
        'End Sub

        Public Sub verfacturas_buscar()
            Dim valor As Boolean = False
            Dim nc As String = "ND"
            Dim FacOperacionAuxiliar As New FacOperacion()
            'If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
            '    FacOperacionAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            'End If
            'FacOperacionAuxiliar.Id = "ND"
            'FacOperacionAuxiliar.Saldo = -1
            'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()

            If Me._ventana.Asociado IsNot Nothing Then
                Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                If valor = False Then
                    FacOperacionAuxiliar.ValorQuery = "Select o from FacOperacion o left join fetch o.Asociado as Asociado left join fetch o.Moneda as Moneda left join fetch o.Idioma as Idioma"
                    FacOperacionAuxiliar.ValorQuery = FacOperacionAuxiliar.ValorQuery & " where "
                Else
                    FacOperacionAuxiliar.ValorQuery = FacOperacionAuxiliar.ValorQuery & " and "
                End If
                FacOperacionAuxiliar.ValorQuery = FacOperacionAuxiliar.ValorQuery & " Asociado.Id= " & asociado.Id
                valor = True
            Else
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                Exit Sub
            End If

            If valor = True Then
                FacOperacionAuxiliar.ValorQuery = FacOperacionAuxiliar.ValorQuery & "and o.Saldo > 0 and o.Id='" & nc & "'  order by o.FechaOperacion, o.CodigoOperacion "
                FacOperacionAuxiliar.Seleccion = True
            End If
            Me._FacOperaciones = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar)
            Me._ventana.ResultadosFactura2 = Me._FacOperaciones
            'Me._ventana.MensajeErrorCobro = ""
        End Sub

        Public Sub MostrarForma()
            Dim FacForma As FacForma = DirectCast(Me._ventana.FacFormaSeleccionada, FacForma)
            Me._ventana.Xforma = FacForma.XForma
            If FacForma.Credito IsNot Nothing Then
                Me._ventana.Credito = FacForma.Credito.Id
            End If
            Me._ventana.Bforma = FacForma.BForma
            Me._ventana.BformaBf = FacForma.BFormaBf
        End Sub

        Public Sub ModificarForma()
            Dim FacFormas As List(Of FacForma) = DirectCast(Me._ventana.ResultadosForma, List(Of FacForma))
            'Dim id As Integer = Me._ventana.Credito
            Dim forma As FacForma = Me._ventana.FacFormaSeleccionada
            Dim sumabforma As Double = 0
            Dim sumabformabf As Double = 0
            For i As Integer = 0 To FacFormas.Count - 1
                If (FacFormas.Item(i).Id = forma.Id) Then
                    FacFormas.Item(i).XForma = Me._ventana.Xforma
                    FacFormas.Item(i).BForma = FormatNumber(Me._ventana.Bforma, 2)
                    'FacFormas.Item(i).BFormaBf = Me._ventana.BformaBf
                    FacFormas.Item(i).BFormaBf = FormatNumber(FacFormas.Item(i).BForma * FacFormas.Item(i).Tasa, 2)
                End If
                sumabforma = sumabforma + FacFormas.Item(i).BForma
                sumabformabf = sumabformabf + FacFormas.Item(i).BFormaBf
            Next
            Me._ventana.SumaBforma = sumabforma
            Me._ventana.SumaBformaBf = sumabformabf
            Me._ventana.ResultadosForma = Nothing
            Me._ventana.ResultadosForma = FacFormas
        End Sub

        Public Sub sumarForma()
            Dim FacFormas As List(Of FacForma) = DirectCast(Me._ventana.ResultadosForma, List(Of FacForma))
            Dim sumabforma As Double = 0
            Dim sumabformabf As Double = 0
            For i As Integer = 0 To FacFormas.Count - 1
                sumabforma = sumabforma + FacFormas.Item(i).BForma
                sumabformabf = sumabformabf + FacFormas.Item(i).BFormaBf
            Next
            Me._ventana.SumaBforma = sumabforma
            Me._ventana.SumaBformaBf = sumabformabf
        End Sub

        Public Sub sumarcobrofactura()
            Dim Faccobrofactura As List(Of FacCobroFactura) = DirectCast(Me._ventana.ResultadosFacturaCobro, List(Of FacCobroFactura))
            Dim Moneda As Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
            Dim sumabono As Double = 0
            Dim sumabonobf As Double = 0
            Dim j As Integer = 0
            xoperacion = ""
            For i As Integer = 0 To Faccobrofactura.Count - 1
                If (Moneda.Id = "US") Then
                    sumabono = sumabono + Faccobrofactura.Item(j).Bono
                Else
                    If (Moneda.Id = "BS") Then
                        sumabono = sumabono + Faccobrofactura.Item(j).Bono / 2.15
                    Else
                        If (Moneda.Id = "BF") Then
                            sumabono = sumabono + Faccobrofactura.Item(j).BonoBf
                        End If
                    End If

                End If
                sumabonobf = sumabonobf + Faccobrofactura.Item(j).BonoBf

                'If (xoperacion = "") Then
                '    xoperacion = Faccobrofactura.Item(j).Factura
                'Else
                '    xoperacion = xoperacion & "," & Faccobrofactura.Item(j).Factura
                'End If

                j = j + 1
            Next
            Me._ventana.SumaBono = sumabono
            Me._ventana.SumaBonoBf = sumabonobf
        End Sub

        Public Sub ModificarFacOperacionesForma(ByVal FacFormas As List(Of FacForma))

            For i As Integer = 0 To FacFormas.Count - 1
                If FacFormas.Item(i).Credito IsNot Nothing Then
                    Dim FacOperacionAuxiliar As New FacOperacion()
                    FacOperacionAuxiliar.Id = "NC"
                    FacOperacionAuxiliar.CodigoOperacion = FacFormas.Item(i).Credito.Id
                    Dim FacOperacion As New FacOperacion
                    FacOperacion = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar).Item(0)
                    FacOperacion.Saldo = 0
                    FacOperacion.SaldoBf = 0
                    Dim exitoso As Boolean = Me._facoperacionesServicios.InsertarOModificar(FacOperacion, UsuarioLogeado.Hash)
                End If
            Next
        End Sub

        Public Sub AgregarFacFormaCobro(ByVal FacFormas As List(Of FacForma), ByVal Cobro As FacCobro)
            For i As Integer = 0 To FacFormas.Count - 1
                FacFormas.Item(i).Cobro = Cobro
                Dim exitoso As Boolean = Me._FacFormaServicios.InsertarOModificar(FacFormas.Item(i), UsuarioLogeado.Hash)
            Next
        End Sub

        Public Sub ModificarFacOperacionesCobroxFactura(ByVal CobroxFacturas As List(Of FacCobroFactura))

            For i As Integer = 0 To CobroxFacturas.Count - 1
                Dim FacOperacionAuxiliar As New FacOperacion()
                FacOperacionAuxiliar.Id = "ND"
                FacOperacionAuxiliar.CodigoOperacion = CobroxFacturas.Item(i).Factura
                Dim FacOperacion As New FacOperacion
                FacOperacion = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar).Item(0)
                FacOperacion.Saldo = 0
                FacOperacion.SaldoBf = 0
                Dim exitoso As Boolean = Me._facoperacionesServicios.InsertarOModificar(FacOperacion, UsuarioLogeado.Hash)
            Next
        End Sub

        Public Sub AgregarFacCobroFacturaCobro(ByVal FacCobroFacturas As List(Of FacCobroFactura), ByVal Cobro As FacCobro)
            For i As Integer = 0 To FacCobroFacturas.Count - 1
                FacCobroFacturas.Item(i).Id = Cobro
                Dim exitoso As Boolean = Me._FacCobroFacturaServicios.InsertarOModificar(FacCobroFacturas.Item(i), UsuarioLogeado.Hash)
            Next
        End Sub

        Public Sub VerCreditos()
            Dim FacOperacionAuxiliar As New FacOperacion()
            If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                FacOperacionAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
            End If
            FacOperacionAuxiliar.Id = "NC"
            FacOperacionAuxiliar.Saldo = -1
            'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()
            Me._FacOperaciones = Me._facoperacionesServicios.ObtenerFacOperacionesFiltro(FacOperacionAuxiliar)
            Me._ventana.ResultadosFactura = Me._FacOperaciones
            Me._ventana.MensajeErrorCobro = ""
        End Sub

        Public Sub AgregarFacturas2()
            Dim FacOperacion As List(Of FacOperacion) = DirectCast(_ventana.ResultadosFactura2, List(Of FacOperacion))
            Dim FacCobroFactura As New List(Of FacCobroFactura)
            Dim Moneda As Moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
            Dim sumabono As Double = 0
            Dim sumabonobf As Double = 0
            Dim j As Integer = 0
            xoperacion = ""
            For i As Integer = 0 To FacOperacion.Count - 1
                If (FacOperacion.Item(i).Seleccion = True) Then
                    FacCobroFactura.Add(New FacCobroFactura)
                    Dim cobro As New FacCobro
                    cobro.Id = 0
                    FacCobroFactura.Item(j).Id = cobro
                    FacCobroFactura.Item(j).Factura = FacOperacion.Item(i).CodigoOperacion
                    FacCobroFactura.Item(j).Bono = FormatNumber(FacOperacion.Item(i).Saldo, 2)
                    FacCobroFactura.Item(j).BonoBf = FormatNumber(FacOperacion.Item(i).SaldoBf, 2)
                    If (Moneda.Id = "US") Then
                        sumabono = sumabono + FacCobroFactura.Item(j).Bono
                    Else
                        If (Moneda.Id = "BS") Then
                            sumabono = sumabono + FacCobroFactura.Item(j).Bono / 2.15
                        Else
                            If (Moneda.Id = "BF") Then
                                sumabono = sumabono + FacCobroFactura.Item(j).BonoBf
                            End If
                        End If

                    End If
                    sumabonobf = sumabonobf + FacCobroFactura.Item(j).BonoBf

                    If (xoperacion = "") Then
                        xoperacion = FacCobroFactura.Item(j).Factura
                    Else
                        xoperacion = xoperacion & "," & FacCobroFactura.Item(j).Factura
                    End If

                    j = j + 1
                End If
            Next
            Me._ventana.ResultadosFacturaCobro = FacCobroFactura
            Me._ventana.SumaBono = sumabono
            Me._ventana.SumaBonoBf = sumabonobf
        End Sub

        Public Sub AgregarForma()
            Dim FacCobro As FacCobro = DirectCast(_ventana.FacCobro, FacCobro)
            Dim moneda As Moneda
            If AdvertenciaTasa() = False Then
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If

            moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)

            Dim FacOperacion As List(Of FacOperacion) = DirectCast(_ventana.ResultadosFactura, List(Of FacOperacion))
            Dim FacForma As New List(Of FacForma)
            Dim sumabforma As Double = 0
            Dim sumabformabf As Double = 0
            Dim j As Integer = 0
            For i As Integer = 0 To FacOperacion.Count - 1
                If (FacOperacion.Item(i).Seleccion = True) Then
                    FacForma.Add(New FacForma)
                    Dim cobro As New FacCobro
                    cobro.Id = 0
                    FacForma.Item(j).Cobro = cobro
                    FacForma.Item(j).Id = j

                    Dim FacCreditoAuxiliar As New FacCredito()
                    FacCreditoAuxiliar.Id = FacOperacion.Item(i).CodigoOperacion
                    'Me._FacCreditos = Me._FacCreditoServicios.ConsultarTodos()  
                    Dim credito As FacCredito = Me._FacCreditoServicios.ObtenerFacCreditosFiltro(FacCreditoAuxiliar).Item(0)
                    FacForma.Item(j).Credito = Me._FacCreditoServicios.ObtenerFacCreditosFiltro(FacCreditoAuxiliar).Item(0)
                    If (FacOperacion.Item(i).Moneda.Id = "US") Then
                        FacForma.Item(j).BForma = FormatNumber(FacOperacion.Item(i).Saldo, 2)

                        Dim tasas As Tasa = buscar_tasa(FacCobro.FechaCobro, moneda.Id)
                        If tasas IsNot Nothing Then
                            FacForma.Item(j).Tasa = FormatNumber(tasas.Tasabf, 2)
                            FacForma.Item(j).BFormaBf = FormatNumber(FacForma.Item(j).BForma * tasas.Tasabf, 2)
                        Else
                            FacForma.Item(j).Tasa = FormatNumber(1, 2)
                            FacForma.Item(j).BFormaBf = FormatNumber(FacForma.Item(j).BForma, 2)
                        End If
                    Else
                        If (FacOperacion.Item(i).Moneda.Id = "BF") Then
                            FacForma.Item(j).BForma = FormatNumber(FacOperacion.Item(i).SaldoBf, 2)
                            FacForma.Item(j).BFormaBf = FormatNumber(FacOperacion.Item(i).SaldoBf, 2)
                        Else
                            If (FacOperacion.Item(i).Moneda.Id = "BS") Then
                                FacForma.Item(j).BForma = FormatNumber(FacOperacion.Item(i).SaldoBf, 2)
                                FacForma.Item(j).BFormaBf = FormatNumber(FacOperacion.Item(i).SaldoBf, 2)
                            End If
                        End If
                    End If
                    FacForma.Item(j).XForma = "Credit"
                    FacForma.Item(j).TipoPago = "Credit"
                    sumabforma = sumabforma + FacForma.Item(j).BForma
                    sumabformabf = sumabformabf + FacForma.Item(j).BFormaBf
                    j = j + 1
                End If
            Next

            'esto es con el fin de poder agregar las formas que no sean creditos
            Dim forma As List(Of FacForma) = DirectCast(_ventana.ResultadosForma, List(Of FacForma))
            If forma IsNot Nothing Then
                For i As Integer = 0 To forma.Count - 1
                    If forma(i).TipoPago <> "Credit" Then
                        FacForma.Add(New FacForma)
                        FacForma.Item(j) = forma(i)
                        sumabforma = sumabforma + FacForma.Item(j).BForma
                        sumabformabf = sumabformabf + FacForma.Item(j).BFormaBf
                        j = j + 1
                    End If
                Next
            End If

            Me._ventana.ResultadosForma = FacForma
            Me._ventana.SumaBforma = sumabforma
            Me._ventana.SumaBformaBf = sumabformabf
        End Sub

        Public Sub eliminar_pago(ByVal id As Integer)
            Dim FacForma As New List(Of FacForma)
            Dim sumabforma As Double = 0
            Dim sumabformabf As Double = 0
            Dim j As Integer = 0

            Dim forma As List(Of FacForma) = DirectCast(_ventana.ResultadosForma, List(Of FacForma))
            If forma IsNot Nothing Then
                For i As Integer = 0 To forma.Count - 1
                    If forma(i).Id <> id Then
                        FacForma.Add(New FacForma)
                        FacForma.Item(j) = forma(i)
                        sumabforma = sumabforma + FacForma.Item(j).BForma
                        sumabformabf = sumabformabf + FacForma.Item(j).BFormaBf
                        j = j + 1
                    End If
                Next
            End If

            Me._ventana.ResultadosForma = Nothing
            Me._ventana.ResultadosForma = FacForma
            Me._ventana.SumaBforma = sumabforma
            Me._ventana.SumaBformaBf = sumabformabf
        End Sub

        Public Function AdvertenciaTasa() As Boolean
            Dim FacCobro As FacCobro = DirectCast(_ventana.FacCobro, FacCobro)
            Dim moneda As Moneda
            Dim respuesta As Boolean = True
            If DirectCast(Me._ventana.Moneda, Moneda) IsNot Nothing Then
                moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
            Else
                MessageBox.Show("Error Elija Moneda", "Error", MessageBoxButton.OK)
                Return (False)
            End If
            If moneda.Id = "US" Then
                If existe_tasa_dia(FacCobro.FechaCobro, moneda.Id) = False Then
                    If MessageBoxResult.Yes = MessageBox.Show("Advertencia Tasa para la fecha de cobro no existe, Desae Continuer de todos Modos?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                        respuesta = True
                    Else
                        Me._ventana.BformaMan = Nothing
                        Dim primervalor As New ListaDatosValores()
                        primervalor.Id = ""
                        primervalor.Descripcion = " "
                        Me._ventana.Valor = primervalor
                        respuesta = False
                    End If
                End If
            Else
                respuesta = True
            End If
            Return (respuesta)
        End Function

        Public Sub AgregarFormaManual()
            Dim FacCobro As FacCobro = DirectCast(_ventana.FacCobro, FacCobro)
            Dim moneda As Moneda
            If AdvertenciaTasa() = False Then
                Mouse.OverrideCursor = Nothing
                Exit Sub
            End If

            moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
            'Dim FacFormas As List(Of FacForma) = DirectCast(_ventana.ResultadosForma, List(Of FacForma))
            Dim FacFormas As New List(Of FacForma)
            Dim j As Integer = 0
            Dim FacFormasresul As List(Of FacForma) = DirectCast(Me._ventana.ResultadosForma, List(Of FacForma))
            If FacFormasresul IsNot Nothing Then
                For i As Integer = 0 To FacFormasresul.Count - 1
                    FacFormas.Add(New FacForma)
                    FacFormas(j) = FacFormasresul(i)
                    j = j + 1
                Next
            End If

            Dim FacForma As New FacForma
            Dim sumabforma As Double = 0
            Dim sumabformabf As Double = 0
            Mouse.OverrideCursor = Cursors.Wait
            Dim cobro As New FacCobro
            Try
                If IsNumeric(Me._ventana.BformaMan) Then
                    cobro.Id = 0
                    FacForma.Cobro = cobro
                    If FacFormas IsNot Nothing Then
                        If FacFormas.Count > 0 Then
                            FacForma.Id = FacFormas.Count + 1
                            j = FacFormas.Count
                        Else
                            FacForma.Id = 1
                            j = 0
                            Me._ventana.SumaBforma = 0
                            Me._ventana.SumaBformaBf = 0
                        End If
                    Else
                        FacForma.Id = 1
                        j = 0
                        Me._ventana.SumaBforma = 0
                        Me._ventana.SumaBformaBf = 0
                    End If
                    FacForma.Credito = Nothing
                    FacForma.BForma = FormatNumber(Me._ventana.BformaMan, 2)

                    'Dim moneda As Moneda = Nothing
                    'If DirectCast(Me._ventana.Moneda, Moneda) IsNot Nothing Then
                    '    moneda = If(Not DirectCast(Me._ventana.Moneda, Moneda).Id.Equals("NGN"), DirectCast(Me._ventana.Moneda, Moneda), Nothing)
                    'Else
                    '    MessageBox.Show("Error Elija Moneda", "Error", MessageBoxButton.OK)
                    '    Exit Sub
                    'End If
                    'Dim FacCobro As FacCobro = DirectCast(_ventana.FacCobro, FacCobro)
                    Dim tasas As Tasa = buscar_tasa(FacCobro.FechaCobro, moneda.Id)
                    If tasas IsNot Nothing Then
                        FacForma.Tasa = FormatNumber(tasas.Tasabf, 2)
                        FacForma.BFormaBf = FormatNumber(FacForma.BForma * tasas.Tasabf, 2)
                    Else
                        FacForma.Tasa = FormatNumber(1, 2)
                        FacForma.BFormaBf = FormatNumber(FacForma.BForma, 2)
                    End If


                    'If IsNumeric(Me._ventana.SumaBforma) Then
                    Me._ventana.SumaBforma = Me._ventana.SumaBforma + FacForma.BForma
                    'Else
                    '    Me._ventana.SumaBforma = FacForma.BForma
                    'End If
                    'If IsNumeric(Me._ventana.SumaBformaBf) Then
                    Me._ventana.SumaBformaBf = Me._ventana.SumaBformaBf + FacForma.BFormaBf
                    'Else
                    '    Me._ventana.SumaBformaBf = FacForma.BFormaBf
                    'End If

                    FacForma.XForma = Me._ventana.XformaMan
                    Me._ventana.XformaMan = ""
                    FacForma.TipoPago = DirectCast(Me._ventana.Valor, ListaDatosValores).Descripcion

                    Dim primervalor As New ListaDatosValores()
                    primervalor.Id = ""
                    primervalor.Descripcion = " "
                    Me._ventana.Valor = primervalor

                    'If j > 0 Then
                    FacFormas.Add(New FacForma)
                    'End If
                    FacFormas(j) = FacForma
                    Me._ventana.ResultadosForma = Nothing
                    Me._ventana.ResultadosForma = FacFormas
                    Me._ventana.BformaMan = Nothing
                    'sumarForma()
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

                Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                Me._ventana.Idiomas = idiomas
                Me._ventana.Idioma = Me.BuscarIdioma(idiomas, DirectCast(Me._ventana.Asociado, Asociado).Idioma)

                Dim Monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                Me._ventana.Monedas = Monedas
                Me._ventana.Moneda = Me.BuscarMoneda(Monedas, DirectCast(Me._ventana.Asociado, Asociado).Moneda)

            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub

        Public Sub ConsultarBanco()
            Dim cbanco As String = Me._ventana.Cbanco
            If IsNumeric(cbanco) Then
                'Dim bancos As IList(Of FacBanco) = Me._bancosServicios.ObtenerFacBancosFiltro(Nothing)()
                'Me._ventana.Bancos = bancos
                'Me._ventana.Banco = FacCredito.Banco
                Dim banco As New FacBanco
                banco.Id = cbanco
                Me._ventana.Banco = Me.BuscarFacBanco(DirectCast(Me._ventana.Bancos, List(Of FacBanco)), banco)
            End If
        End Sub

        ''' METODO QUE CONSULTA UN ASOCIADO
        Public Sub ConsultarAsociado()

            Dim strArray() As String
            Dim idAsociado As String
            Dim asociado As Asociado = New Asociado()
            Dim asociados As IList(Of Asociado)

            Try
                If Me._ventana.NombreAsociado <> "" Then
                    strArray = Me._ventana.NombreAsociado.Split("-")
                    idAsociado = strArray(0).Trim()
                    asociado.Id = Integer.Parse(idAsociado)
                    asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociado)
                    If asociados.Count > 0 Then
                        asociado = asociados(0)
                        Me.Navegar(New ConsultarAsociado(asociado, Me._ventana, False))
                    Else
                        Me._ventana.Mensaje("El Asociado no existe")
                    End If
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado + ": " + ex.Message, True)
            End Try

        End Sub

    End Class
End Namespace
