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
Imports Diginsoft.Bolet.Cliente.Fac.Contratos.FacAsociadoMarcaPatentes
Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacFacturas
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.FacAsociadoMarcaPatentes
'Imports Diginsoft.Bolet.Cliente.Fac.Ventanas.Principales
Imports Diginsoft.Bolet.ObjetosComunes.ContratosServicios
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.ContratosServicios
Imports Trascend.Bolet.Cliente.Presentadores
Imports Trascend.Bolet.Cliente.Ventanas.Principales

Namespace Presentadores.FacAsociadoMarcaPatentes
    Partial Public Class PresentadorSaldoAsociado

        Private _asociadosServicios As IAsociadoServicios
        Private _FacFacturaPendienteConGruServicios As IFacFacturaPendienteConGruServicios
        Private _FacVistaFacturaServicioservicios As IFacVistaFacturaServicioServicios
        Private _FacVistaFacturacionCxpInternaServicios As IFacVistaFacturacionCxpInternaServicios
        Private _id As String
        Private _tipo As String
        ''' <summary>
        ''' Constructor Predeterminado
        ''' </summary>
        ''' <param name="ventana">página que satisface el contrato</param>

        Public Sub CalcularSaldoAsociado(ByVal casociado As Integer?, ByVal p_dias As Integer?, ByRef p_venmay_B As Double?, ByRef p_venmay_D As Double?, ByRef p_venmen_B As Double?, ByRef p_venmen_D As Double?, ByRef p_total_B As Double?, ByRef p_total_D As Double?, ByRef msaldope As Double?, ByRef moneda As String)
            Mouse.OverrideCursor = Cursors.Wait


            Me._asociadosServicios = DirectCast(Activator.GetObject(GetType(IAsociadoServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("AsociadoServicios")), IAsociadoServicios)

            Dim asociadoaux As New Asociado
            Dim asociado As Asociado = Nothing
            Dim i As Boolean = False

            'Dim p_venmay_B, p_venmay_D, p_venmen_B, p_venmen_D, p_total_B, p_total_D
            p_venmay_B = 0
            p_venmay_D = 0
            p_venmen_B = 0
            p_venmen_D = 0
            p_total_B = 0
            p_total_D = 0
            moneda = ""
            Mouse.OverrideCursor = Cursors.Wait
            asociadoaux.Id = casociado
            asociado = Me._asociadosServicios.ObtenerAsociadosFiltro(asociadoaux)
            If asociado IsNot Nothing Then
                moneda = asociado.Moneda.Id

                Me._FacFacturaPendienteConGruServicios = DirectCast(Activator.GetObject(GetType(IFacFacturaPendienteConGruServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacFacturaPendienteConGruServicios")), IFacFacturaPendienteConGruServicios)
                Dim FacFacturaPendienteConGru As List(Of FacFacturaPendienteConGru) = Nothing
                Dim FacFacturaPendienteConGruaux As New FacFacturaPendienteConGru
                FacFacturaPendienteConGruaux.Id = asociado.Id
                FacFacturaPendienteConGru = Me._FacFacturaPendienteConGruServicios.ObtenerFacFacturaPendienteConGrusFiltro(FacFacturaPendienteConGruaux)
                If FacFacturaPendienteConGru IsNot Nothing Then
                    If FacFacturaPendienteConGru.Count > 0 Then
                        For j As Integer = 1 To FacFacturaPendienteConGru.Count - 1
                            If FacFacturaPendienteConGru(i).Dias > p_dias Then
                                p_venmay_D = p_venmay_D + FacFacturaPendienteConGru(j).Saldo '2
                                p_total_D = p_total_D + p_venmay_D

                                p_venmay_B = p_venmay_B + FacFacturaPendienteConGru(j).SaldoBf '1
                                p_total_B = p_total_B + p_venmay_B
                            Else
                                p_venmen_D = p_venmen_D + FacFacturaPendienteConGru(j).Saldo '4
                                p_total_D = p_total_D + p_venmen_D

                                p_venmen_B = p_venmen_B + FacFacturaPendienteConGru(j).SaldoBf '3
                                p_total_B = p_total_B + p_venmen_B
                            End If
                        Next
                    End If


                    Me._FacVistaFacturacionCxpInternaServicios = DirectCast(Activator.GetObject(GetType(IFacVistaFacturacionCxpInternaServicios), ConfigurationManager.AppSettings("RutaServidor") + ConfigurationManager.AppSettings("FacVistaFacturacionCxpInternaServicios")), IFacVistaFacturacionCxpInternaServicios)
                    Dim FacVistaFacturacionCxpInterna As List(Of FacVistaFacturacionCxpInterna) = Nothing
                    Dim FacVistaFacturacionCxpInternaaux As New FacVistaFacturacionCxpInterna
                    FacVistaFacturacionCxpInternaaux.Asociado_o = asociado
                    FacVistaFacturacionCxpInternaaux.Cobrada = "NO"
                    FacVistaFacturacionCxpInterna = Me._FacVistaFacturacionCxpInternaServicios.ObtenerFacVistaFacturacionCxpInternasFiltro(FacVistaFacturacionCxpInternaaux)

                    Dim monto As Double? = 0
                    If FacVistaFacturacionCxpInterna.Count > 0 Then
                        For j As Integer = 1 To FacVistaFacturacionCxpInterna.Count - 1
                            monto = monto + FacVistaFacturacionCxpInterna(j).Monto
                        Next
                    End If

                    msaldope = monto
                End If

            End If

            Mouse.OverrideCursor = Nothing
        End Sub

    End Class
End Namespace