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
    Public Class DaoDesgloseServicioNHibernate
        Inherits DaoBaseNHibernate(Of FacDesgloseServicio, Char)
        Implements IDaoDesgloseServicio
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Public Function ObtenerFacDesgloseServiciosFiltro(ByVal FacDesgloseServicio As FacDesgloseServicio) As System.Collections.Generic.IList(Of FacDesgloseServicio) Implements Contrato.IDaoDesgloseServicio.ObtenerFacDesgloseServiciosFiltro
            Dim FacDesgloseServicios As IList(Of FacDesgloseServicio) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacDesgloseServicio)
            'If (FacDesgloseServicio IsNot Nothing) AndAlso (FacDesgloseServicio.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioId, FacDesgloseServicio.Id)
            '    variosFiltros = True
            'End If


            'If (FacDesgloseServicio.Id IsNot Nothing) AndAlso (FacDesgloseServicio.Id.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioDesglose, FacDesgloseServicio.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacDesgloseServicio.Servicio IsNot Nothing) AndAlso (FacDesgloseServicio.Servicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioServicio, FacDesgloseServicio.Servicio.Id)
                variosFiltros = True
            End If
            'If (FacDesgloseServicio.Servicio IsNot Nothing) AndAlso (FacDesgloseServicio.Servicio.Cod_Cont <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioServicioCod_Cont, FacDesgloseServicio.Servicio.Cod_Cont)
            '    variosFiltros = True
            'End If
            'If (FacDesgloseServicio.Servicio IsNot Nothing) AndAlso (FacDesgloseServicio.Servicio.Xreferencia <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioServicioXreferencia, FacDesgloseServicio.Servicio.Xreferencia)
            '    variosFiltros = True
            'End If

            'If (FacDesgloseServicio.Servicio IsNot Nothing) Then
            '    If FacDesgloseServicio.Servicio.Itipo.Equals(" "c) Then
            '    Else

            '        If variosFiltros Then
            '            filtro += " and "
            '        End If
            '        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioServicioItipo, FacDesgloseServicio.Servicio.Itipo)
            '        variosFiltros = True
            '    End If
            'End If
            'If (FacDesgloseServicio.Servicio IsNot Nothing) Then
            '    If FacDesgloseServicio.Servicio.Local.Equals(" "c) Then
            '    Else

            '        If variosFiltros Then
            '            filtro += " and "
            '        End If
            '        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioServicioLocal, FacDesgloseServicio.Servicio.Local)
            '        variosFiltros = True
            '    End If
            'End If


            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacDesgloseServicios = query.List(Of FacDesgloseServicio)()

            Return FacDesgloseServicios

        End Function
    End Class
End Namespace