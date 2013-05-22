Imports System
Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ChequeRecidos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.ChequeRecidos
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales


Namespace Presentadores.ChequeRecidos
    Class PresentadorConsultarChequeRecido
        Inherits PresentadorBase
        Private _ventana As IConsultarChequeRecido
        Private _ChequeRecidoServicios As IChequeRecidoServicios
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios
        Private _contactoServicios As IContactoServicios
        Private _bancogsServicios As IBancoGServicios
        Private _asociados As IList(Of Asociado)
        Private _chequerido As ChequeRecido
        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="ventana">Página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarChequeRecido, ChequeRecido As Object)
            Try
                Me._ventana = ventana
                'Me._ventana.ChequeRecido = New ChequeRecido()
                Me._ChequeRecidoServicios = DirectCast(Activator.GetObject(GetType(IChequeRecidoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ChequeRecidoServicios")), IChequeRecidoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._bancogsServicios = DirectCast(Activator.GetObject(GetType(IBancoGServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("bancogServicios")), IBancoGServicios)

                ' Dim ChequeRecido As New ChequeRecido()
                Me._ventana.ChequeRecido = ChequeRecido
                _chequerido = ChequeRecido

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
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleAgregarChequeRecido, Recursos.Ids.AgregarUsuario)

                Me._ventana.HabilitarCampos = False

                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados
                Dim ChequeRecido As ChequeRecido = Me._ventana.ChequeRecido

                'Dim bancogs As IList(Of BancoG) = Me._bancogsServicios.ConsultarTodos()
                Dim bancogs As IList(Of BancoG) = Me._bancogsServicios.ObtenerBancoGsFiltro(Nothing)
                Dim primerabancog As New BancoG()
                primerabancog.Id = Integer.MinValue
                bancogs.Insert(0, primerabancog)
                Me._ventana.BancoGs = bancogs
                Me._ventana.BancoG = BuscarBancoG(bancogs, ChequeRecido.BancoG)


                Dim asociadoaux As New Asociado
                Dim asociado As List(Of Asociado)
                If ChequeRecido.Id IsNot Nothing Then
                    asociadoaux.Id = ChequeRecido.Id.Id
                    asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                    Me._ventana.Asociados = asociado
                    Me._ventana.Asociado = asociado(0)
                    Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
                End If

                Me._ventana.Monto = ChequeRecido.Monto

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

            Me.Navegar(New ConsultarChequeRecido(_chequerido))
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
        Public Sub Modificar()
            Try

                'Habilitar campos
                If Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnModificar Then
                    Me._ventana.HabilitarCampos = True
                    Me._ventana.TextoBotonModificar = Recursos.Etiquetas.btnAceptar
                Else

                    Dim ChequeRecido As ChequeRecido = DirectCast(_ventana.ChequeRecido, ChequeRecido)

                    ChequeRecido.Id = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)

                    'If Not Me._ChequeRecidoServicios.VerificarExistencia(ChequeRecido) Then
                    ChequeRecido.BancoG = If(Not DirectCast(Me._ventana.BancoG, BancoG).Id.Equals("NGN"), DirectCast(Me._ventana.BancoG, BancoG), Nothing)

                    ChequeRecido.Monto = Me._ventana.Monto
                    'Dim primerafacbanco As New FacBanco()
                    'primerafacbanco.Id = -2
                    'ChequeRecido.Banco = Nothing
                    'ChequeRecido.FechaDeposito = Date.Now
                    'ChequeRecido.Deposito = "1"
                    'ChequeRecido.FechaDeposito = CDate("01-01-1900")
                    Dim exitoso As Boolean = _ChequeRecidoServicios.InsertarOModificar(ChequeRecido, UsuarioLogeado.Hash)

                    If exitoso Then
                        Me.Navegar(Recursos.MensajesConElUsuario.fac_ChequeRecidoInsertado, False)
                    End If
                    'Else
                    'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorChequeRecidoRepetido)
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

        Public Sub Eliminar()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                If Me._ChequeRecidoServicios.Eliminar(DirectCast(Me._ventana.ChequeRecido, ChequeRecido), UsuarioLogeado.Hash) Then
                    _paginaPrincipal.MensajeUsuario = Recursos.MensajesConElUsuario.fac_ConceptoGestionEliminado
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

        Public Sub NuevaProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region            

            Me.Navegar(New AgregarChequeRecido())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

    End Class
End Namespace
