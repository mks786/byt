Imports System.Configuration
Imports System.Net.Sockets
Imports System.Runtime.Remoting
Imports System.Windows.Input
Imports NLog
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacInternacionales
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacInternacionales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales
Imports System.Windows
Imports System.Windows.Controls
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports DataTable = System.Data.DataTable
Imports System.Data
Namespace Presentadores.FacInternacionales
    Class PresentadorFacInternacionalRegistro
        Inherits PresentadorBase

        Private _ventana As IFacInternacionalRegistro                    
        Private _PaisServicios As IPaisServicios
        Private _facOperacionProformasServicios As IFacOperacionProformaServicios
        Private _FacInternacionalesServicios As IFacInternacionalServicios
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios

        Private facfacturaproforma As FacFacturaProforma

        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _asociadosServicios As IAsociadoServicios

        Private existe As Boolean

        Public Sub New(ByVal ventana As IFacInternacionalRegistro, ByVal proforma As FacFacturaProforma)
            Try
                Me._ventana = ventana
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)                
                Me._PaisServicios = DirectCast(Activator.GetObject(GetType(IPaisServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("PaisServicios")), IPaisServicios)
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._facOperacionProformasServicios = DirectCast(Activator.GetObject(GetType(IFacOperacionProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacOperacionProformaServicios")), IFacOperacionProformaServicios)
                Me._FacInternacionalesServicios = DirectCast(Activator.GetObject(GetType(IFacInternacionalServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacInternacionalServicios")), IFacInternacionalServicios)

                facfacturaproforma = proforma
                Dim facinternacional As FacInternacional = buscar_facinternacional(proforma.Id)
                If facinternacional IsNot Nothing Then ' si ya se creo el registro 
                    Me._ventana.SetTipoPago = BuscarTipoPagoInternacional(facinternacional.TipoPago)
                    Me._ventana.FacInternacional = facinternacional
                    existe = True
                Else ' si el registro no se a creado

                    Dim facinternacionalnuevo As New FacInternacional
                    facinternacionalnuevo.Id = facfacturaproforma.Id
                    Me._ventana.SetTipoPago = "Vacio"
                    Me._ventana.FacInternacional = facinternacionalnuevo

                    Dim asociadoaux As New Asociado
                    Dim asociado As List(Of Asociado)
                    If facfacturaproforma.Asociado IsNot Nothing Then
                        asociadoaux.Id = facfacturaproforma.Asociado.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados = asociado
                        Me._ventana.Asociado = asociado(0)
                        Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
                    End If

                    existe = False
                End If

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

            Me.Navegar(New FacInternacionalRegistro(facfacturaproforma))
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
                'If (existe_tasa_dia(Date.Now, "US") = True) Then
                Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleFacInternacionalRegistro, Recursos.Ids.fac_FacInternacionalRegistro)

                If existe = False Then
                    Dim paises As IList(Of Pais) = Me._PaisServicios.ConsultarTodos()
                    Dim primerpais As New Pais()
                    primerpais.Id = Integer.MinValue
                    paises.Insert(0, primerpais)
                    Me._ventana.Paises = paises

                    'Me._ventana.FechaCorte = FormatDateTime(Date.Now, DateFormat.ShortDate)
                    'Me._ventana.FechaEnvio = FormatDateTime(Date.Now, DateFormat.ShortDate)

                Else
                    Dim internacional As FacInternacional = DirectCast(Me._ventana.FacInternacional, FacInternacional)

                    Dim asociadoaux As New Asociado
                    Dim asociado As List(Of Asociado)
                    If internacional.Asociado IsNot Nothing Then
                        asociadoaux.Id = internacional.Asociado.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados = asociado
                        Me._ventana.Asociado = asociado(0)
                        Me._ventana.NombreAsociado = asociado(0).Id & " - " & asociado(0).Nombre
                    End If

                    If internacional.Asociado_o IsNot Nothing Then
                        asociadoaux.Id = internacional.Asociado_o.Id
                        asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
                        Me._ventana.Asociados2 = asociado
                        Me._ventana.Asociado2 = asociado(0)
                        Me._ventana.NombreAsociado2 = asociado(0).Id & " - " & asociado(0).Nombre
                    End If

                    Dim Paises As IList(Of Pais) = Me._PaisServicios.ConsultarTodos()
                    Me._ventana.Paises = Paises
                    'Me._ventana.Pais = FacCredito.Pais
                    Me._ventana.Pais = Me.BuscarPais(Paises, internacional.Pais)

                End If
                Me._ventana.FocoPredeterminado()



                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                    '#End Region
                End If
                'Else
                'Me.Navegar(Recursos.MensajesConElUsuario.fac_error_tasa_dia, True)
                'End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            Finally
                Mouse.OverrideCursor = Nothing
            End Try
        End Sub

        Public Function BuscarTipoPagoInternacional(ByVal TipoPago As Char) As String
            Dim retorno As String
            If Recursos.Etiquetas.fac_cbiDeposito(0).Equals(TipoPago) Then
                retorno = Recursos.Etiquetas.fac_cbiDeposito
            ElseIf Recursos.Etiquetas.fac_cbiTransferencia(0).Equals(TipoPago) Then
                retorno = Recursos.Etiquetas.fac_cbiTransferencia
            ElseIf Recursos.Etiquetas.fac_cbiCheque(0).Equals(TipoPago) Then
                retorno = Recursos.Etiquetas.fac_cbiCheque
            ElseIf Recursos.Etiquetas.fac_cbiVacio(0).Equals(TipoPago) Then
                retorno = Recursos.Etiquetas.fac_cbiVacio
            Else
                retorno = Recursos.Etiquetas.cbiNinguno
            End If
            Return retorno
        End Function

        Public Sub Aceptar()
            Try
                Mouse.OverrideCursor = Cursors.Wait
                Dim Facinternacional As FacInternacional = DirectCast(_ventana.FacInternacional, FacInternacional)

                If DirectCast(Me._ventana.Asociado, Asociado) IsNot Nothing Then
                    Facinternacional.Asociado = If(Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado, Asociado), Nothing)                    
                Else
                    MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                    Mouse.OverrideCursor = Nothing
                    Exit Sub
                End If

                If DirectCast(Me._ventana.Asociado2, Asociado) IsNot Nothing Then
                    Facinternacional.Asociado_o = If(Not DirectCast(Me._ventana.Asociado2, Asociado).Id.Equals("NGN"), DirectCast(Me._ventana.Asociado2, Asociado), Nothing)
                Else
                    'MessageBox.Show("Ingrese Asociado", "Error", MessageBoxButton.OK)
                    'Exit Sub
                End If

                Facinternacional.TipoPago = Me._ventana.GetTipoPago

                If Facinternacional.Fecha IsNot Nothing Then
                    Facinternacional.Fecha = FormatDateTime(Facinternacional.Fecha, DateFormat.ShortDate)
                End If
                If Facinternacional.FechaRecepcion IsNot Nothing Then
                    Facinternacional.FechaRecepcion = FormatDateTime(Facinternacional.FechaRecepcion, DateFormat.ShortDate)
                End If

                If DirectCast(Me._ventana.Pais, Pais) IsNot Nothing Then
                    If Me._ventana.Pais.id <> Integer.MinValue Then
                        Facinternacional.Pais = DirectCast(Me._ventana.Pais, Pais)
                    Else
                        'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                        'Exit Sub
                    End If
                Else
                    'MessageBox.Show("Debe especificar Carta Orden", "Error", MessageBoxButton.OK)
                    'Exit Sub
                End If

                'If Me._ventana.BSustProforma Then
                '    Facinternacional.BSustProforma = Me._ventana.BSustProforma
                'End If

                If (Me._ventana.NumeroFactura IsNot Nothing) And (Not Me._ventana.NumeroFactura.Equals(String.Empty)) Then

                    Dim FacturaAuxiliar As FacInternacional = New FacInternacional()
                    FacturaAuxiliar.Numerofactura = Me._ventana.NumeroFactura
                    FacturaAuxiliar.Asociado_o = DirectCast(Me._ventana.Asociado2, Asociado)

                    Dim facturas As List(Of FacInternacional) = Me._FacInternacionalesServicios.ObtenerFacInternacionalesFiltro(FacturaAuxiliar)
                    If facturas.Count > 0 Then
                        If Not Me._ventana.BSustProforma Then
                            Mouse.OverrideCursor = Nothing
                            Me._ventana.Mensaje("El número de Factura ingresado ya se encuentra registrado")
                            Exit Sub
                        End If
                            'Mouse.OverrideCursor = Nothing
                            'Me._ventana.Mensaje("El número de Factura ingresado ya se encuentra registrado")
                        'Exit Sub
                    Else
                        If Not Me._ventana.BSustProforma Then
                            Mouse.OverrideCursor = Nothing
                            Me._ventana.Mensaje("El número de Factura ingresado ya se encuentra registrado")
                            Exit Sub
                        End If
                    End If
                End If

                Facinternacional.BSustProforma = False

                Dim exitoso As Boolean = _FacInternacionalesServicios.InsertarOModificar(Facinternacional, UsuarioLogeado.Hash)

                If exitoso Then
                    Mouse.OverrideCursor = Nothing
                    'Me.Navegar(Recursos.MensajesConElUsuario.fac_Facinternacional Insertado, False)
                    If existe = False Then
                        MessageBox.Show("Registro Internacional Creado", "Internacional", MessageBoxButton.OK)
                    Else
                        MessageBox.Show("Registro Internacional Modificado", "Internacional", MessageBoxButton.OK)
                    End If

                    'If MessageBoxResult.Yes = MessageBox.Show(Recursos.MensajesConElUsuario.fac_FacinternacionalInsertado & " Modificar Gestion?", "Modificar Gestion", MessageBoxButton.YesNo, MessageBoxImage.Question) Then
                    '    'IrConsultarFacinternacional(Facinternacional)
                    'Else
                    '    Me.Navegar(_paginaPrincipal)
                    'End If
                End If
                'Else
                'Me._ventana.Mensaje(Recursos.MensajesConElUsuario.fac_ErrorFacinternacional Repetido)
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

        Public Function buscar_facinternacional(ByVal proforma As Integer) As FacInternacional
            Dim facinternacionalaux As New FacInternacional
            facinternacionalaux.Id = proforma
            Dim facinternacionales As List(Of FacInternacional) = Me._FacInternacionalesServicios.ObtenerFacInternacionalesFiltro(facinternacionalaux)
            If facinternacionales IsNot Nothing Then
                If facinternacionales.Count > 0 Then
                    Return (facinternacionales(0))
                Else
                    Return (Nothing)
                End If
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
                ' Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado, Asociado))
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

        Public Sub BuscarAsociadofin()
            Dim asociadoaux As New Asociado
            Dim asociados As List(Of Asociado) = Nothing
            Dim i As Boolean = False
            'asociadoaux = Nothing
            'Dim asociadosFiltrados As IEnumerable(Of Asociado) = Me._asociados
            Mouse.OverrideCursor = Cursors.Wait

            If Not String.IsNullOrEmpty(Me._ventana.idAsociadoFiltrar2) And Me._ventana.idAsociadoFiltrar2 <> "0" Then
                asociadoaux.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar2)
                '
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Id = Integer.Parse(Me._ventana.idAsociadoFiltrar)
                i = True
            End If

            If Not String.IsNullOrEmpty(Me._ventana.NombreAsociadoFiltrar2) Then
                asociadoaux.Nombre = Me._ventana.NombreAsociadoFiltrar2
                '    asociadosFiltrados = From p In asociadosFiltrados Where p.Nombre IsNot Nothing AndAlso p.Nombre.ToLower().Contains(Me._ventana.NombreAsociadoFiltrar.ToLower())
                i = True
            End If


            'Me._asociados = Me._asociadosServicios.ConsultarAsociadoConTodo(asociadoaux)
            If i = True Then
                asociados = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            Else
                Me._ventana.Asociados2 = Nothing
                Mouse.OverrideCursor = Nothing
                MessageBox.Show("Error: No Existe Asociado Relacionado a la Búsqueda")                
                Exit Sub
            End If

            Dim primerasociado As New Asociado()
            primerasociado.Id = Integer.MinValue
            asociados.Insert(0, primerasociado)

            Me._ventana.Asociados2 = asociados
            Mouse.OverrideCursor = Nothing

            'If asociadosFiltrados.ToList.Count <> 0 Then
            '    Me._ventana.Asociados = asociadosFiltrados
            'Else
            '    Me._ventana.Asociados = Me._asociados
            'End If
        End Sub

        Public Sub CambiarAsociadofin()
            Try
                Dim asociado As Asociado = Me._asociadosServicios.ConsultarAsociadoConTodo(DirectCast(Me._ventana.Asociado2, Asociado))
                'asociado.Contactos = Me._contactoServicios.ConsultarContactosPorAsociado(asociado)
                Me._ventana.NombreAsociado2 = DirectCast(Me._ventana.Asociado2, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado2, Asociado).Nombre
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub


        Public Function busca_asociado_reporte() As List(Of Asociado)
            Dim asociadoaux As New Asociado
            'para generar el Facasociado
            Dim Facasociadoaux As New Asociado

            If Me._ventana.Asociado IsNot Nothing And Me._ventana.Asociado2 IsNot Nothing Then
                Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                Dim asociado2 As Asociado = DirectCast(Me._ventana.Asociado2, Asociado)
                asociadoaux.ValorQuery = asociadoaux.ValorQuery & " a.Id>= " & asociado.Id & " and  a.Id<= " & asociado2.Id & " and a.EdoCuenta = 'T'"
            Else
                If Me._ventana.Asociado IsNot Nothing Then
                    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)

                    asociadoaux.Id = asociado.Id
                End If
            End If
            'asociadoaux.EdoCuenta = "T"
            Dim asociados As List(Of Asociado) = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            If asociados.Count > 0 Then
                Return (asociados)
            Else
                Return (Nothing)
            End If
        End Function


    End Class
End Namespace
