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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.ChequeRecidos
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.ChequeRecidos
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.ChequeRecidos
    Class PresentadorConsultarChequeRecidos
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarChequeRecidos
        Private _ChequeRecidoServicios As IChequeRecidoServicios
        Private _ChequeRecidos As IList(Of ChequeRecido)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarChequeRecidos)
            Try
                Me._ventana = ventana
                Me._ChequeRecidoServicios = DirectCast(Activator.GetObject(GetType(IChequeRecidoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("ChequeRecidoServicios")), IChequeRecidoServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarChequeRecidos, Recursos.Ids.fac_ConsultarChequeRecido)
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

                Dim ChequeRecidoAuxiliar As New ChequeRecido()
                ActualizarTitulo()

                'Me._ChequeRecidos = Me._ChequeRecidoServicios.ConsultarTodos()
                Me._ChequeRecidos = Me._ChequeRecidoServicios.ObtenerChequeRecidosFiltro(ChequeRecidoAuxiliar)

                Me._ventana.Count = Me._ChequeRecidos.Count
                Me._ventana.Resultados = Me._ChequeRecidos
                Me._ventana.ChequeRecidoFiltrar = New ChequeRecido()

                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._ventana.Asociados = asociados

                Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)
                Dim primerafacbanco As New FacBanco()
                primerafacbanco.Id = Integer.MinValue
                facbancos.Insert(0, primerafacbanco)
                Me._ventana.Bancos = facbancos

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

        Public Sub Limpiar()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarChequeRecidos())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
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

                'Dim ChequeRecido As ChequeRecido = DirectCast(Me._ventana.ChequeRecidoFiltrar, ChequeRecido)
                ''ChequeRecido.Region = If(Not Me._ventana.Region.Equals(""), Me._ventana.Region, Nothing)
                ''ChequeRecido.Id = Nothing
                'Dim ChequeRecidosFiltrados As IEnumerable(Of ChequeRecido) = Me._ChequeRecidos

                'If Me._ventana.Asociado IsNot Nothing AndAlso Not DirectCast(Me._ventana.Asociado, Asociado).Id.Equals(Integer.MinValue) Then
                '    Dim asociado As Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                '    'ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Id.Id = Integer.Parse(DirectCast(Me._ventana.Asociado, Asociado).Id)
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Id.Id.ToString().ToLower().Contains(asociado.Id.ToString().ToLower())
                'End If

                'If Me._ventana.Banco IsNot Nothing AndAlso Not DirectCast(Me._ventana.Banco, BancoG).Id.Equals(Integer.MinValue) Then
                '    Dim banco As BancoG = DirectCast(Me._ventana.Banco, BancoG)
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Banco.Id.ToString().ToLower().Contains(banco.Id.ToString().ToLower())
                'End If

                ''If Not String.IsNullOrEmpty(ChequeRecido.Monto) Then
                ''    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Monto = Integer.Parse(ChequeRecido.Monto)
                ''    'Where p.Id IsNot Nothing AndAlso p.Id.ToLower().Contains(ChequeRecido.Id.ToLower())
                ''End If

                'If Not String.IsNullOrEmpty(Me._ventana.Fecha) Then
                '    Dim FechaAux As DateTime = DateTime.Parse(Me._ventana.Fecha)
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Fecha.Equals(FechaAux)
                'End If

                ''If Not String.IsNullOrEmpty(Me._ventana.FechaDeposito) Then
                ''    Dim FechaDepositoAux As DateTime = DateTime.Parse(Me._ventana.FechaDeposito)
                ''    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.FechaDeposito.Equals(FechaDepositoAux)
                ''End If

                'If Not String.IsNullOrEmpty(Me._ventana.FechaReg) Then
                '    Dim FechaRegAux As DateTime = DateTime.Parse(Me._ventana.FechaReg)
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.FechaReg.Equals(FechaRegAux)
                'End If

                'If Not String.IsNullOrEmpty(ChequeRecido.NCheque) Then
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.NCheque IsNot Nothing AndAlso p.NCheque.ToLower().Contains(ChequeRecido.NCheque.ToLower())
                'End If


                'If Not String.IsNullOrEmpty(ChequeRecido.Deposito) Then
                '    ChequeRecidosFiltrados = From p In ChequeRecidosFiltrados Where p.Deposito IsNot Nothing AndAlso p.Deposito.ToLower().Contains(ChequeRecido.Deposito.ToLower())
                'End If

                'Me._ventana.Resultados = ChequeRecidosFiltrados.ToList()
                'Me._ventana.Resultados = ChequeRecidosFiltrados.ToList(IEnumerable(Of ChequeRecido))

                '#Region "trace"



                Dim filtroValido As Boolean = False
                'Dim filtroValido As Integer = 0
                'Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                'dos filtros sean utilizados
                Dim ChequeRecidoAuxiliar As New ChequeRecido()
                Dim ChequeRecidos As ChequeRecido = DirectCast(_ventana.ChequeRecidoFiltrar, ChequeRecido)

                If Not String.IsNullOrEmpty(ChequeRecidos.NCheque) Then
                    filtroValido = True
                    ChequeRecidoAuxiliar.NDeposito = ChequeRecidos.NDeposito
                End If

                If Not String.IsNullOrEmpty(ChequeRecidos.Deposito) Then
                    filtroValido = True
                    ChequeRecidoAuxiliar.Deposito = ChequeRecidos.Deposito
                End If

                If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                    ChequeRecidoAuxiliar.Id = DirectCast(Me._ventana.Asociado, Asociado)
                    filtroValido = True
                End If

                If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                    ChequeRecidoAuxiliar.Banco = DirectCast(Me._ventana.Banco, FacBanco)
                    filtroValido = True
                End If

                If Not Me._ventana.Fecha.Equals("") Then
                    Dim fechaChequeRecido As DateTime = DateTime.Parse(Me._ventana.Fecha)
                    filtroValido = True
                    ChequeRecidoAuxiliar.Fecha = fechaChequeRecido
                End If

                If Not Me._ventana.FechaReg.Equals("") Then
                    Dim fechaChequeRecido As DateTime = DateTime.Parse(Me._ventana.FechaReg)
                    filtroValido = True
                    ChequeRecidoAuxiliar.FechaReg = fechaChequeRecido
                End If

                'If (filtroValido = True) Then
                Me._ChequeRecidos = Me._ChequeRecidoServicios.ObtenerChequeRecidosFiltro(ChequeRecidoAuxiliar)
                Me._ventana.Count = Me._ChequeRecidos.Count
                Me._ventana.Resultados = Me._ChequeRecidos
                'Else
                '    Me._ventana.Mensaje(Recursos.MensajesConElUsuario.ErrorFiltroIncompleto)
                'End If



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
        ''' Método que invoca una nueva página "ConsultarChequeRecido" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarChequeRecido()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarChequeRecido(Me._ventana.ChequeRecidoSeleccionado))
            'Me.Navegar(New ConsultarChequeRecido())
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
                Me._ventana.NombreAsociado = DirectCast(Me._ventana.Asociado, Asociado).Id & " - " & DirectCast(Me._ventana.Asociado, Asociado).Nombre
                'Me._ventana.Personas = asociado.Contactos
            Catch e As ApplicationException
                'Me._ventana.Personas = Nothing
            End Try
        End Sub
    End Class
End Namespace