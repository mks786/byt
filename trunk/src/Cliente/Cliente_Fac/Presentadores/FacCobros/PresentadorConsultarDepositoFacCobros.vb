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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacCobros
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacCobros
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacCobros
    Class PresentadorConsultarDepositoFacCobros
        Inherits PresentadorBase
        Private Shared _paginaPrincipal As PaginaPrincipal = PaginaPrincipal.ObtenerInstancia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Private _ventana As IConsultarDepositoFacCobros
        Private _FacCobroServicios As IFacCobroServicios
        Private _FacCobros As IList(Of FacCobro)
        Dim FacCobroselect As IList(Of FacCobroSelec)
        'Private _asociados As IAsociadoServicios
        Private _asociadosServicios As IAsociadoServicios
        Private _asociados As IList(Of Asociado)
        Private _facbancos As IFacBancoServicios
        Private _facbancosServicios As IFacBancoServicios

        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>
        Public Sub New(ByVal ventana As IConsultarDepositoFacCobros)
            Try
                Me._ventana = ventana
                Me._FacCobroServicios = DirectCast(Activator.GetObject(GetType(IFacCobroServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacCobroServicios")), IFacCobroServicios)
                'Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)
                Me._facbancosServicios = DirectCast(Activator.GetObject(GetType(IFacBancoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacBancoServicios")), IFacBancoServicios)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Me.Navegar(Recursos.MensajesConElUsuario.ErrorInesperado, True)
            End Try
        End Sub

        Public Sub ActualizarTitulo()
            Me.ActualizarTituloVentanaPrincipal(Recursos.Etiquetas.fac_titleConsultarFacCobros, Recursos.Ids.fac_ConsultarFacCobro)
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


                Dim FacCobroAuxiliar As New FacCobro()
                FacCobroAuxiliar.Deposito = "1"
                'Me._FacCobros = Me._FacCobroServicios.ConsultarTodos()
                Me._FacCobros = Me._FacCobroServicios.ObtenerFacCobrosFiltro(FacCobroAuxiliar)
                FacCobroselect = convertir_FacCobroSelec(Me._FacCobros)
                Me._ventana.Resultados = FacCobroselect
                Dim chequevacio As New FacCobro
                chequevacio.FechaDeposito = Date.Now
                Me._ventana.FacCobroFiltrar = chequevacio


                'Dim asociados As IList(Of Asociado) = Me._asociadosServicios.ConsultarTodos()
                'Dim primeraasociado As New Asociado()
                'primeraasociado.Id = Integer.MinValue
                'asociados.Insert(0, primeraasociado)
                'Me._ventana.Asociados = asociados

                Dim facbancos As IList(Of FacBanco) = Me._facbancosServicios.ConsultarTodos()
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


        ''' <summary>
        ''' Método que realiza una consulta al servicio, con el fin de filtrar los datos que se muestran 
        ''' por pantalla
        ''' </summary>
        Public Sub AplicarDeposito()
            Try
                '#Region "trace"
                If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                    logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
                End If
                '#End Region

                Dim v_FacCobro As List(Of FacCobroSelec) = DirectCast(_ventana.Resultados, List(Of FacCobroSelec))
                Dim v_FacCobro2 As FacCobro = DirectCast(_ventana.FacCobroFiltrar, FacCobro)
                v_FacCobro2.Banco = If(Not DirectCast(Me._ventana.Banco, FacBanco).Id.Equals("NGN"), DirectCast(Me._ventana.Banco, FacBanco), Nothing)
                'Dim v_FacCobro2 As List(Of FacCobro) = DirectCast(_ventana.Resultados, List(Of FacCobro))

                For i As Integer = 0 To v_FacCobro.Count - 1
                    If (v_FacCobro(i).Seleccion = True) Then
                        If (Me._ventana.Banco IsNot Nothing) AndAlso (DirectCast(Me._ventana.Banco, FacBanco).Id <> Integer.MinValue) Then
                            v_FacCobro(i).Deposito = "2"
                            v_FacCobro(i).FechaDeposito = v_FacCobro2.FechaDeposito
                            v_FacCobro(i).NDeposito = v_FacCobro2.NDeposito
                            v_FacCobro(i).Banco = v_FacCobro2.Banco
                            Dim a As New FacCobro
                            a = convertir_FacCobro(v_FacCobro(i))
                            Dim exitoso As Boolean = _FacCobroServicios.InsertarOModificar(a, UsuarioLogeado.Hash)
                        End If
                    End If
                Next

                Dim FacCobroAuxiliar As New FacCobro()
                FacCobroAuxiliar.Deposito = "1"
                'Me._FacCobros = Me._FacCobroServicios.ConsultarTodos()
                Me._FacCobros = Me._FacCobroServicios.ObtenerFacCobrosFiltro(FacCobroAuxiliar)
                FacCobroselect = convertir_FacCobroSelec(Me._FacCobros)
                Me._ventana.Resultados = FacCobroselect

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
        ''' Método que invoca una nueva página "ConsultarFacCobro" y la instancia con el objeto seleccionado
        ''' </summary>
        Public Sub IrConsultarFacCobro()
            '#Region "trace"
            If ConfigurationManager.AppSettings("ambiente").ToString().Equals("desarrollo") Then
                logger.Debug("Entrando al metodo {0}", (New System.Diagnostics.StackFrame()).GetMethod().Name)
            End If
            '#End Region

            'Me.Navegar(New ConsultarFacCobro(Me._ventana.FacCobroSeleccionado))
            'Me.Navegar(New ConsultarFacCobro())
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
            '    newDir = ListSortDirection.Descending
            'End If

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


                'Dim FacCobroAuxiliar As New FacCobro()
                'FacCobroAuxiliar.Deposito = "1"

                ''Me._FacCobros = Me._FacCobroServicios.ConsultarTodos()
                'Me._FacCobros = Me._FacCobroServicios.ObtenerFacCobrosFiltro(FacCobroAuxiliar)

                'FacCobroselect = convertir_FacCobroSelec(Me._FacCobros)

                For i As Integer = 0 To FacCobroselect.Count - 1
                    FacCobroselect.Item(i).Seleccion = value
                Next
                Me._ventana.Resultados = Nothing
                Me._ventana.Resultados = FacCobroselect

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


        'para covertir una clase FacCobro a FacCobroSelec
        Public Function convertir_FacCobroSelec(ByVal v_FacCobro As IList(Of FacCobro)) As IList(Of FacCobroSelec)
            Dim FacCobroSelec As New List(Of FacCobroSelec)
            Dim monto As Double = 0
            Me._ventana.NReg = v_FacCobro.Count
            For i As Integer = 0 To v_FacCobro.Count - 1
                FacCobroSelec.Add(New FacCobroSelec)
                FacCobroSelec.Item(i).Seleccion = False
                FacCobroSelec.Item(i).Banco = v_FacCobro.Item(i).Banco
                FacCobroSelec.Item(i).BancoG = v_FacCobro.Item(i).BancoG
                FacCobroSelec.Item(i).Id = v_FacCobro.Item(i).Id
                FacCobroSelec.Item(i).NDeposito = v_FacCobro.Item(i).NDeposito
                FacCobroSelec.Item(i).Deposito = v_FacCobro.Item(i).Deposito
                FacCobroSelec.Item(i).NCheque = v_FacCobro.Item(i).NCheque
                FacCobroSelec.Item(i).Monto = v_FacCobro.Item(i).Monto
                monto = v_FacCobro.Item(i).Monto + monto
                FacCobroSelec.Item(i).Fecha = v_FacCobro.Item(i).Fecha
                FacCobroSelec.Item(i).FechaReg = v_FacCobro.Item(i).FechaReg
                FacCobroSelec.Item(i).FechaDeposito = v_FacCobro.Item(i).FechaDeposito
            Next
            Me._ventana.Tmonto = monto
            Return FacCobroSelec
        End Function

        'para covertir una clase FacCobroSelec a FacCobro
        Public Function convertir_FacCobro(ByVal v_FacCobroSelec2 As FacCobroSelec) As FacCobro
            Dim FacCobro2 As New FacCobro
            FacCobro2.Banco = v_FacCobroSelec2.Banco
            FacCobro2.BancoG = v_FacCobroSelec2.BancoG
            FacCobro2.Id = v_FacCobroSelec2.Id
            FacCobro2.NDeposito = v_FacCobroSelec2.NDeposito
            FacCobro2.Deposito = v_FacCobroSelec2.Deposito
            FacCobro2.NCheque = v_FacCobroSelec2.NCheque
            FacCobro2.Monto = v_FacCobroSelec2.Monto
            FacCobro2.Fecha = v_FacCobroSelec2.Fecha
            FacCobro2.FechaReg = v_FacCobroSelec2.FechaReg
            FacCobro2.FechaDeposito = v_FacCobroSelec2.FechaDeposito

            Return FacCobro2
        End Function

    End Class

    'este es para agregar el check para seleccionar las que se van a depositar
    Public Class FacCobroSelec
        Inherits FacCobro

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