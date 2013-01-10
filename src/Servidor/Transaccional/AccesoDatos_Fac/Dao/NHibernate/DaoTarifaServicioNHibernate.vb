Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Imports Trascend.Bolet.ObjetosComunes.Entidades
Namespace Dao.NHibernate
    Public Class DaoTarifaServicioNHibernate
        Inherits DaoBaseNHibernate(Of TarifaServicio, String)
        Implements IDaoTarifaServicio
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerTarifaServiciosFiltro(ByVal TarifaServicio As TarifaServicio) As System.Collections.Generic.IList(Of TarifaServicio) Implements Contrato.IDaoTarifaServicio.ObtenerTarifaServiciosFiltro
            Dim TarifaServicios As IList(Of TarifaServicio) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerTarifaServicio)
            'If (TarifaServicio IsNot Nothing) AndAlso (TarifaServicio.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerTarifaServicioId, TarifaServicio.Id)
            '    variosFiltros = True
            'End If


            If (TarifaServicio IsNot Nothing) AndAlso (TarifaServicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTarifaServicioId, TarifaServicio.Id)
                variosFiltros = True
            End If
            If (TarifaServicio.Tarifa IsNot Nothing) AndAlso (TarifaServicio.Tarifa.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If                                                              
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTarifaServicioTarifa, TarifaServicio.Tarifa.Id)
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            TarifaServicios = query.List(Of TarifaServicio)()

            Return TarifaServicios

        End Function


    End Class
End Namespace