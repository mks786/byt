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
    Public Class DaoDepartamentoServicioNHibernate
        Inherits DaoBaseNHibernate(Of FacDepartamentoServicio, String)
        Implements IDaoDepartamentoServicio
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacDepartamentoServiciosFiltro(ByVal FacDepartamentoServicio As FacDepartamentoServicio) As System.Collections.Generic.IList(Of FacDepartamentoServicio) Implements Contrato.IDaoDepartamentoServicio.ObtenerFacDepartamentoServiciosFiltro
            Dim FacDepartamentoServicios As IList(Of FacDepartamentoServicio) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacDepartamentoServicio)
            'If (FacDepartamentoServicio IsNot Nothing) AndAlso (FacDepartamentoServicio.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioId, FacDepartamentoServicio.Id)
            '    variosFiltros = True
            'End If


            If (FacDepartamentoServicio.Id IsNot Nothing) AndAlso (FacDepartamentoServicio.Id.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioDepartamento, FacDepartamentoServicio.Id.Id)
                variosFiltros = True
            End If
            If (FacDepartamentoServicio.Servicio IsNot Nothing) AndAlso (FacDepartamentoServicio.Servicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioServicio, FacDepartamentoServicio.Servicio.Id)
                variosFiltros = True
            End If
            If (FacDepartamentoServicio.Servicio IsNot Nothing) AndAlso (FacDepartamentoServicio.Servicio.Cod_Cont <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioServicioCod_Cont, FacDepartamentoServicio.Servicio.Cod_Cont)
                variosFiltros = True
            End If
            If (FacDepartamentoServicio.Servicio IsNot Nothing) AndAlso (FacDepartamentoServicio.Servicio.Xreferencia <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioServicioXreferencia, FacDepartamentoServicio.Servicio.Xreferencia)
                variosFiltros = True
            End If

            If (FacDepartamentoServicio.Servicio IsNot Nothing) Then
                If FacDepartamentoServicio.Servicio.Itipo.Equals(" "c) Then
                Else

                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioServicioItipo, FacDepartamentoServicio.Servicio.Itipo)
                    variosFiltros = True
                End If
            End If
            If (FacDepartamentoServicio.Servicio IsNot Nothing) Then
                If FacDepartamentoServicio.Servicio.Local.Equals(" "c) Then
                Else

                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDepartamentoServicioServicioLocal, FacDepartamentoServicio.Servicio.Local)
                    variosFiltros = True
                End If
            End If


            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & " Order By servicio.id asc")
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro & " Order By servicio.id asc"
                query = Session.CreateQuery(cabecera)
            End If
            FacDepartamentoServicios = query.List(Of FacDepartamentoServicio)()

            Return FacDepartamentoServicios

        End Function


    End Class
End Namespace