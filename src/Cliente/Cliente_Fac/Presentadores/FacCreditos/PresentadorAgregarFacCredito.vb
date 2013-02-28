Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCreditos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacCreditos
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacCreditos
    Class PresentadorAgregarFacCredito
        Inherits PresentadorBase
        Private _ventana As IAgregarFacCredito
        Private _FacCreditoServicios As IFacCreditoServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios
        'Private _contactoServicios As IContactoServicios
        Private _bancosServicios As IFacBancoServicios
        Private _facoperacionesServicios As IFacOperacionServicios       
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios
        Private _tasasServicios As ITasaServicios
        Private _asociados As IList(Of Asociado)
        Private _contadorfacServicios As IContadorFacServicios
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IAgregarFacCredito)
            Try
                Me._ventana = ventana
                'Me._ventana.FacCredito = New FacCredito()
                Me._FacCreditoServicios = DirectCast(Activator.GetObject(GetType(IFacCreditoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCreditoServicios")), IFacCreditoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._bancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._facoperacionesServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionServicios")), IFacOperacionServicios)                
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
                Me._tasasServicios = DirectCast(Activator.GetObject(GetType(ITasaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("TasaServicios")), ITasaServicios)
                Me._contadorfacServicios = DirectCast(Activator.GetObject(GetType(IContadorFacServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ContadorFacServicios")), IContadorFacServicios)
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

                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarFacCredito, Recursos.Ids.AgregarUsuario)

                Dim FacCredito As New FacCredito()
                FacCredito.FechaCredito = Date.Now
                Me._ventana.FacCredito = FacCredito

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                Dim bancos As IList(Of FacBanco) = Me._bancosServicios.ConsultarTodos()
                Dim primerabanco As New FacBanco()
                primerabanco.Id = Integer.MinValue
                bancos.Insert(0, primerabanco)
                Me._ventana.Bancos = bancos

                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim primeridioma As New Idioma()
                'primeridioma.Id = ""
                'idiomas.Insert(0, primeridioma)
                'Me._ventana.Idiomas = idiomas
                '''Me._ventana.Idioma = primeridioma

                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Dim primeramoneda As New Moneda()
                'primeramoneda.Id = ""
                'monedas.Insert(0, primeramoneda)
                'Me._ventana.Monedas = monedas

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
        ''' Método que realiza toda la lógica para agregar al Usuario dentro de la base de datos
        ''' </summary>
        Public Sub Aceptar()
            Try
                Dim FacCredito As FacCredito = DirectCast(_ventana.FacCredito, FacCredito)


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


                'FacCredito.BCredito = Me._ventana.BCredito
                FacCredito.BCreditoBf = Me._ventana.BCreditoBf
                'FacCredito. = Me._ventana.BSaldo

                'para el contador
                Dim contador As New ContadorFac
                contador.Id = "FAC_CREDITOS"
                contador = _contadorfacServicios.ConsultarPorId(contador)
                FacCredito.Id = contador.ProximoValor
                contador.ProximoValor = System.Math.Max(System.Threading.Interlocked.Increment(contador.ProximoValor), contador.ProximoValor - 1)
                Dim exitocontador As Boolean = _contadorfacServicios.InsertarOModificar(contador, UsuarioLogeado.Hash)
                'fin contador          

                FacCredito.Timestamp = FormatDateTime(Date.Now, DateFormat.ShortDate)
                FacCredito.FechaCredito = FormatDateTime(FacCredito.FechaCredito, DateFormat.ShortDate)
                FacCredito.Operacion = "CREATE"
                Dim exitoso As Boolean = _FacCreditoServicios.InsertarOModificar(FacCredito, UsuarioLogeado.Hash)

                If exitoso Then
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


                    Mouse.OverrideCursor = Nothing
                    'Me.Navegar(Recursos.MensajesConElUsuario.fac_FacCobroInsertado, False)
                    If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacCreditoInsertado & " Modificar Credito?", "Modificar Creditos", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                        FacCredito.Accion = 1
                        IrConsultarFaccredito(FacCredito)
                    Else
                        Limpiar()
                    End If

                    'Me.Navegar(Recursos.MensajesConElUsuario.fac_FacCreditoInsertado, False)
                End If
                'Else
                'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorFacCreditoRepetido)
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

        Public Sub IrConsultarFaccredito(ByVal faccredito As FacCredito)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacCredito(faccredito))
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

            Me.Navegar(New AgregarFacCredito())
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
                Me._ventana.BSaldo = FacCredito.BCredito
                Dim bcredito As Double = FacCredito.BCredito
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
Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre

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
    End Class
End Namespace
