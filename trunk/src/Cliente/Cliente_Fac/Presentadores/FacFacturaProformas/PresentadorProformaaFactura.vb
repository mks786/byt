﻿Imports System
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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacFacturaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturaProformas
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacFacturaProformas
    Class PresentadorProformaaFactura
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IProformaaFactura
        Private _FacFacturaProformaServicios As IFacFacturaProformaServicios
        'Private _FacFacturaProformas As IList(Of FacFacturaProforma)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios
        Private _idiomasServicios As IIdiomaServicios
        Private _monedasServicios As IMonedaServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IProformaaFactura)
            Try
                Me._ventana = ventana
                Me._FacFacturaProformaServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaProformaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaProformaServicios")), IFacFacturaProformaServicios)
                Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
                Me._idiomasServicios = DirectCast(Activator.GetObject(GetType(IIdiomaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("IdiomaServicios")), IIdiomaServicios)
                Me._monedasServicios = DirectCast(Activator.GetObject(GetType(IMonedaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("MonedaServicios")), IMonedaServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            'fac_titleFacturaaProforma()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleProformaaFactura, Recursos.Ids.fac_ConsultarFacFacturaProforma)
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

                'Me._FacFacturaProformas = Me._FacFacturaProformaServicios.ConsultarTodos()
                'Dim FacFacturaProformaAuxiliar As New FacFacturaProforma
                'FacFacturaProformaAuxiliar.Inicial = UsuarioLogeado.Iniciales
                ''FacFacturaProformaAuxiliar.Status = 1 ' esto es para el campo auto porque es in

                'Dim FacFacturaProformas As IList(Of FacFacturaProforma)
                'FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)

                'Me._ventana.Resultados = FacFacturaProformas
                'Consultar()
                Me._ventana.FacFacturaProformaFiltrar = New FacFacturaProforma
                'sumar(FacFacturaProformas)

                'Me._asociados = Me._asociadosServicios.ConsultarTodos()
                'Me._ventana.Asociados = Me._asociados

                'Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ObtenerFacBancosFiltro(Nothing)()
                'Dim primerafacbanco As New FacBanco()
                'primerafacbanco.Id = Integer.MinValue
                'facbancos.Insert(0, primerafacbanco)
                'Me._ventana.Bancos = facbancos

                'Dim idiomas As IList(Of Idioma) = Me._idiomasServicios.ConsultarTodos()
                'Dim primeridioma As New Idioma
                'primeridioma.Id = ""
                'idiomas.Insert(0, primeridioma)
                'Me._ventana.Idiomas = idiomas

                'Dim monedas As IList(Of Moneda) = Me._monedasServicios.ConsultarTodos()
                'Dim primeramoneda As New Moneda
                'primeramoneda.Id = ""
                'monedas.Insert(0, primeramoneda)
                'Me._ventana.Monedas = monedas

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

            Me.Navegar(New ProformaaFactura())
            'Me.Navegar(New ConsultarFacCobro())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub


        Public Sub sumar(ByVal proforma As IList(Of FacFacturaProforma))
            Dim cba, cda As Integer
            Dim Mba, Mda As Double
            Mba = 0
            cba = 0
            Mda = 0
            cda = 0


            For i As Integer = 0 To proforma.Count - 1
                If proforma(i).Moneda.Id = "BF" Then
                    If proforma(i).Auto = "1" Then
                        cba = cba + 1
                        Mba = Mba + proforma(i).Mttotal
                    End If
                Else
                    'If proforma(i).Moneda.Id = "US" Then
                    If proforma(i).Auto = "1" Then
                        cda = cda + 1
                        Mda = Mda + proforma(i).Mttotal
                    End If
                    'End If
                End If
            Next
            Me._ventana.Mba = Mba
            Me._ventana.Cba = cba
            Me._ventana.Mda = Mda
            'Me._ventana.Cbr = cbr
            'Me._ventana.Cdp = cdp
            Me._ventana.Cda = cda
            'Me._ventana.Cdf = cdf
            'Me._ventana.Cdr = cdr
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

                'Dim filtroValido As Boolean = False
                'Dim filtroValido As Integer = 0
                'Variable utilizada para limitar a que el filtro se ejecute solo cuando 
                'dos filtros sean utilizados
                Dim FacFacturaProformaAuxiliar As New FacFacturaProforma()
                'Dim FacFacturaProformas As FacFacturaProforma = DirectCast(_ventana.FacFacturaProformaFiltrar, FacFacturaProforma)

                If Not Me._ventana.Id.Equals("") Then
                    FacFacturaProformaAuxiliar.Id = Integer.Parse(Me._ventana.Id)
                End If

                'If Not Me._ventana.CreditoSent.Equals("") Then
                '    FacFacturaProformaAuxiliar.CreditoSent = Integer.Parse(Me._ventana.CreditoSent)
                'End If

                If (Me._ventana.Asociado IsNot Nothing) AndAlso (DirectCast(Me._ventana.Asociado, Asociado).Id <> Integer.MinValue) Then
                    FacFacturaProformaAuxiliar.Asociado = DirectCast(Me._ventana.Asociado, Asociado)
                End If

                'If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                '    'FacFacturaProformaAuxiliar.Banco = DirectCast(Me._ventana.Banco, FacBanco)
                'End If

                'If (Me._ventana.Idioma IsNot Nothing) AndAlso (DirectCast(Me._ventana.Idioma, Idioma).Id <> "") Then
                '    FacFacturaProformaAuxiliar.Idioma = DirectCast(Me._ventana.Idioma, Idioma)
                'End If

                'If (Me._ventana.Moneda IsNot Nothing) AndAlso (DirectCast(Me._ventana.Moneda, Moneda).Id <> "") Then
                '    FacFacturaProformaAuxiliar.Moneda = DirectCast(Me._ventana.Moneda, Moneda)
                'End If

                If Not Me._ventana.FechaFactura.Equals("") Then
                    Dim FechaFacFacturaProforma As DateTime = DateTime.Parse(Me._ventana.FechaFactura)
                    FacFacturaProformaAuxiliar.FechaFactura = FechaFacFacturaProforma
                End If

                'If (filtroValido = True) Then
                'FacFacturaProformaAuxiliar.Inicial = UsuarioLogeado.Iniciales
                FacFacturaProformaAuxiliar.Status = 4 'solo para las proformas Autorizadas
                Dim FacFacturaProformas As IList(Of FacFacturaProforma)
                FacFacturaProformas = Me._FacFacturaProformaServicios.ObtenerFacFacturaProformasFiltro(FacFacturaProformaAuxiliar)
                Me._ventana.Resultados = Nothing
                Me._ventana.Count = FacFacturaProformas.Count
                Me._ventana.Resultados = FacFacturaProformas
                sumar(FacFacturaProformas)
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

        Public Sub VerRechazo()
            Me._ventana.VerTipo = "1"
        End Sub

        Public Sub Rechazar()            
            'If UsuarioLogeado.BAutorizar = True Then
            Mouse.OverrideCursor = Cursors.Wait
            Dim proforma As List(Of FacFacturaProforma) = Me._ventana.Resultados
            For i As Integer = 0 To proforma.Count - 1
                If proforma(i).Seleccion = True Then
                    ' aqui va la funcion --> activate "fac_pro_causa".exec (cfactura.fac_facturas_pro)
                    proforma(i).XCausaRec = Me._ventana.Rechazar
                    proforma(i).Auto = "3"
                    If Me._FacFacturaProformaServicios.InsertarOModificar(proforma(i), UsuarioLogeado.Hash) Then
                        MessageBox.Show("Rechazo realizado con exito")
                    End If
                End If
            Next
            'MessageBox.Show("Autorizacion Satisfactoria")
            Consultar()
            Mouse.OverrideCursor = Nothing
            'Else
            'MessageBox.Show("No posee Privilegios para Autorizar")
            'End If
            Me._ventana.VerTipo = "2"
        End Sub

        Public Sub GenFactura()
            'If UsuarioLogeado.BAutorizar = True Then
            Mouse.OverrideCursor = Cursors.Wait
            Dim proforma As List(Of FacFacturaProforma) = Me._ventana.Resultados
            If proforma.Count > 0 Then
                For i As Integer = 0 To proforma.Count - 1
                    If proforma(i).Seleccion = True Then
                        '--> aqui llama a la pantalla de factura para pasar toda la informacion y el usuario decide si lo guarda y crea el nuevo numero de factura
                        ' Activate("fac_facturas_nx")
                        IrConsultarFacFactura(proforma(i))
                    End If
                Next
                Consultar()
            End If
            'MessageBox.Show("Autorizacion Satisfactoria")            
            Mouse.OverrideCursor = Nothing
            'Else
            'MessageBox.Show("No posee Privilegios para Autorizar")
            'End If
        End Sub

        Public Sub IrConsultarFacFactura(ByVal FacFacturaProforma As FacFacturaProforma)
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'FacFacturaProforma.Accion = 1 'modificar sin el boton regresar
            FacFacturaProforma.Status = 1
            Me.Navegar(New ConsultarFacFacturaPase(FacFacturaProforma))
            'Me.Navegar(New ConsultarFacFacturaProforma())
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Saliendo del metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
        End Sub


        ''' <summary>
        ''' Método que invoca una nueva página "ConsultarFacFacturaProforma" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacFacturaProforma()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region
            Me._ventana.FacFacturaProformaSeleccionado.Accion = 2 'no modificar
            Me.Navegar(New ConsultarFacFacturaProforma(Me._ventana.FacFacturaProformaSeleccionado))
            'Me.Navegar(New ConsultarFacFacturaProforma())
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
End Namespace