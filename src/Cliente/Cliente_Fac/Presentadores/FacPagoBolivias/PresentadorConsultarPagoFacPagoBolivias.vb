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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacPagoBolivias
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacPagoBolivias
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacPagoBolivias
    Class PresentadorConsultarPagoFacPagoBolivias
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarPagoFacPagoBolivias
        Private _FacPagoBoliviaServicios As IFacPagoBoliviaServicios
        Private _FacPagoBolivias As IList(Of FacPagoBolivia)
        Dim FacPagoBoliviaselect As IList(Of FacPagoBoliviaSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _bancogs As IFacBancoServicios
        Private _bancogsServicios As IBancoGServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarPagoFacPagoBolivias)
            Try
                Me._ventana = ventana
                Me._FacPagoBoliviaServicios = DirectCast(Activator.GetObject(GetType(IFacPagoBoliviaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacPagoBoliviaServicios")), IFacPagoBoliviaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._bancogsServicios = DirectCast(Activator.GetObject(GetType(IBancoGServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("BancoGServicios")), IBancoGServicios)
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

            Me.Navegar(New ConsultarPagoFacPagoBolivias())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacPagoBolivias, Recursos.Ids.fac_ConsultarFacPagoBolivia)
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


                consultar()
                Dim pagovacio As New FacPagoBolivia
                pagovacio.FechaPago = Date.Now
                Me._ventana.FacPagoBoliviaFiltrar = pagovacio


                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._ventana.Asociados = asociados

                Dim bancogs As IList(Of BancoG) = Me._bancogsServicios.ConsultarTodos()
                Dim primerbancog As New BancoG()
                primerbancog.Id = Integer.MinValue
                bancogs.Insert(0, primerbancog)
                Me._ventana.BancosPag = bancogs

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


        Public Sub consultar()
            Dim FacPagoBoliviaAuxiliar As New FacPagoBolivia()
            Dim filtroValido As Boolean = False

            If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                FacPagoBoliviaAuxiliar.Id = DirectCast(Me._ventana.Asociado, Asociado)
                filtroValido = True
            End If

            If (Me._ventana.BancoPag IsNot Nothing) AndAlso (DirectCast(Me._ventana.BancoPag, BancoG).Id <> Integer.MinValue) Then
                FacPagoBoliviaAuxiliar.BancoPag = DirectCast(Me._ventana.BancoPag, BancoG)
                filtroValido = True
            End If

            'If Not Me._ventana.FechaBanco.Equals("") Then
            '    Dim fechabancoFacPagoBolivia As DateTime = DateTime.Parse(Me._ventana.FechaBanco)
            '    filtroValido = True
            '    FacPagoBoliviaAuxiliar.FechaBanco = fechabancoFacPagoBolivia
            'End If

            'If Not Me._ventana.FechaReg.Equals("") Then
            '    Dim fechaFacPagoBolivia As DateTime = DateTime.Parse(Me._ventana.FechaReg)
            '    filtroValido = True
            '    FacPagoBoliviaAuxiliar.FechaReg = fechaFacPagoBolivia
            'End If

            If _ventana.FormaPago.Equals(" "c) Then
                FacPagoBoliviaAuxiliar.PagoRec = " "
            Else
                FacPagoBoliviaAuxiliar.PagoRec = _ventana.FormaPago
            End If

            FacPagoBoliviaAuxiliar.IPagado = "0"
            'FacPagoBoliviaAuxiliar.PagoRec = " "
            'Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ConsultarTodos()
            Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ObtenerFacPagoBoliviasFiltro(FacPagoBoliviaAuxiliar)
            FacPagoBoliviaselect = convertir_FacPagoBoliviaSelec(Me._FacPagoBolivias)
            Me._ventana.Count = FacPagoBoliviaselect.Count
            If FacPagoBoliviaselect.Count <= 0 Then
                MessageBox.Show("Mensaje: No se encontraron registros")
            End If
            Me._ventana.Resultados = FacPagoBoliviaselect
        End Sub

        ''' <summary>
        ''' Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        ''' por pantalla
        ''' </summary>
        Public Sub AplicarPago()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim v_FacPagoBolivia As List(Of FacPagoBoliviaSelec) = DirectCast(_ventana.Resultados, List(Of FacPagoBoliviaSelec))
                Dim v_FacPagoBolivia2 As FacPagoBolivia = DirectCast(_ventana.FacPagoBoliviaFiltrar, FacPagoBolivia)
                v_FacPagoBolivia2.BancoPag = If(Not DirectCast(Me._ventana.BancoPag, BancoG).Id.Equals("NGN"), DirectCast(Me._ventana.BancoPag, BancoG), Nothing)
                'Dim v_FacPagoBolivia2 As List(Of FacPagoBolivia) = DirectCast(_ventana.Resultados, List(Of FacPagoBolivia))
                v_FacPagoBolivia2.PagoPag = _ventana.FormaPago
                For i As Integer = 0 To v_FacPagoBolivia.Count - 1
                    If (v_FacPagoBolivia(i).Seleccion = True) Then
                        If (Me._ventana.BancoPag IsNot Nothing) AndAlso (DirectCast(Me._ventana.BancoPag, BancoG).Id <> Integer.MinValue) Then
                            v_FacPagoBolivia(i).IPagado = "1"
                            v_FacPagoBolivia(i).FechaPago = v_FacPagoBolivia2.FechaPago
                            v_FacPagoBolivia(i).PagoPag = v_FacPagoBolivia2.PagoPag
                            v_FacPagoBolivia(i).BancoPag = v_FacPagoBolivia2.BancoPag
                            v_FacPagoBolivia(i).NumeroPag = v_FacPagoBolivia2.NumeroPag
                            Dim a As New FacPagoBolivia
                            a = convertir_FacPagoBolivia(v_FacPagoBolivia(i))
                            Dim exitoso As Boolean = _FacPagoBoliviaServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacPagoBoliviaAuxiliar As New FacPagoBolivia()
                FacPagoBoliviaAuxiliar.IPagado = "0"
                FacPagoBoliviaAuxiliar.PagoRec = " "
                'Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ConsultarTodos()
                Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ObtenerFacPagoBoliviasFiltro(FacPagoBoliviaAuxiliar)
                FacPagoBoliviaselect = convertir_FacPagoBoliviaSelec(Me._FacPagoBolivias)
                Me._ventana.Resultados = FacPagoBoliviaselect

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
        ''' Método que invoca una nueva página "ConsultarFacPagoBolivia" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacPagoBolivia()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            Me.Navegar(New ConsultarFacPagoBolivia(Me._ventana.FacPagoBoliviaSeleccionado))
            'Me.Navegar(New ConsultarFacPagoBolivia())
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

        'para tildar  o destildar 
        Public Sub seleccionar(ByVal value As Boolean)

            Try
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If


                'Dim FacPagoBoliviaAuxiliar As New FacPagoBolivia()
                'FacPagoBoliviaAuxiliar.Deposito = "1"

                ''Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ConsultarTodos()
                'Me._FacPagoBolivias = Me._FacPagoBoliviaServicios.ObtenerFacPagoBoliviasFiltro(FacPagoBoliviaAuxiliar)

                'FacPagoBoliviaselect = convertir_FacPagoBoliviaSelec(Me._FacPagoBolivias)

                For i As Integer = 0 To FacPagoBoliviaselect.Count - 1
                    FacPagoBoliviaselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacPagoBoliviaselect

                Me._ventana.FocoPredeterminado()

                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region
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


        'para covertir una clase FacPagoBolivia a FacPagoBoliviaSelec
        Public Function convertir_FacPagoBoliviaSelec(ByVal v_FacPagoBolivia As IList(Of FacPagoBolivia)) As IList(Of FacPagoBoliviaSelec)
            Dim FacPagoBoliviaSelec As New List(Of FacPagoBoliviaSelec)

            For i As Integer = 0 To v_FacPagoBolivia.Count - 1
                FacPagoBoliviaSelec.Add(New FacPagoBoliviaSelec)
                FacPagoBoliviaSelec.Item(i).Seleccion = False
                FacPagoBoliviaSelec.Item(i).Id = v_FacPagoBolivia.Item(i).Id
                FacPagoBoliviaSelec.Item(i).FechaBanco = v_FacPagoBolivia.Item(i).FechaBanco
                FacPagoBoliviaSelec.Item(i).BancoRec = v_FacPagoBolivia.Item(i).BancoRec
                FacPagoBoliviaSelec.Item(i).BancoPag = v_FacPagoBolivia.Item(i).BancoPag                
                FacPagoBoliviaSelec.Item(i).PagoRec = v_FacPagoBolivia.Item(i).PagoRec
                FacPagoBoliviaSelec.Item(i).MontoRec = v_FacPagoBolivia.Item(i).MontoRec
                FacPagoBoliviaSelec.Item(i).MontoBol = v_FacPagoBolivia.Item(i).MontoBol
                FacPagoBoliviaSelec.Item(i).DescripcionRec = v_FacPagoBolivia.Item(i).DescripcionRec
                FacPagoBoliviaSelec.Item(i).IPagado = v_FacPagoBolivia.Item(i).IPagado               
                FacPagoBoliviaSelec.Item(i).PagoPag = v_FacPagoBolivia.Item(i).PagoPag
                FacPagoBoliviaSelec.Item(i).FechaReg = v_FacPagoBolivia.Item(i).FechaReg
                FacPagoBoliviaSelec.Item(i).NumeroPag = v_FacPagoBolivia.Item(i).NumeroPag
                FacPagoBoliviaSelec.Item(i).FechaPago = v_FacPagoBolivia.Item(i).FechaPago
            Next
            Return FacPagoBoliviaSelec
        End Function

        'para covertir una clase FacPagoBoliviaSelec a FacPagoBolivia
        Public Function convertir_FacPagoBolivia(ByVal v_FacPagoBoliviaSelec2 As FacPagoBoliviaSelec) As FacPagoBolivia
            Dim FacPagoBolivia2 As New FacPagoBolivia
            FacPagoBolivia2.Id = v_FacPagoBoliviaSelec2.Id
            FacPagoBolivia2.FechaBanco = v_FacPagoBoliviaSelec2.FechaBanco
            FacPagoBolivia2.BancoRec = v_FacPagoBoliviaSelec2.BancoRec
            FacPagoBolivia2.BancoPag = v_FacPagoBoliviaSelec2.BancoPag
            FacPagoBolivia2.PagoRec = v_FacPagoBoliviaSelec2.PagoRec
            FacPagoBolivia2.MontoRec = v_FacPagoBoliviaSelec2.MontoRec
            FacPagoBolivia2.MontoBol = v_FacPagoBoliviaSelec2.MontoBol
            FacPagoBolivia2.DescripcionRec = v_FacPagoBoliviaSelec2.DescripcionRec
            FacPagoBolivia2.IPagado = v_FacPagoBoliviaSelec2.IPagado
            FacPagoBolivia2.FechaReg = v_FacPagoBoliviaSelec2.FechaReg
            FacPagoBolivia2.PagoPag = v_FacPagoBoliviaSelec2.PagoPag
            FacPagoBolivia2.NumeroPag = v_FacPagoBoliviaSelec2.NumeroPag
            FacPagoBolivia2.FechaPago = v_FacPagoBoliviaSelec2.FechaPago
            Return FacPagoBolivia2
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

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacPagoBoliviaSelec
        Inherits FacPagoBolivia

        Private p_seleccion As Boolean

        Public Property Seleccion() As Boolean
            Get
                Return Me.p_seleccion
            End Get
            Set(ByVal Value As Boolean)
                Me.p_seleccion = Value
            End Set
        End Property

    End Class

End Namespace