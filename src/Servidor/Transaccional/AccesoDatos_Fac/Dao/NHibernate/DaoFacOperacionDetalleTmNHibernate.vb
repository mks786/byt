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
    Public Class DaoFacOperacionDetalleTmNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionDetalleTm, Integer)
        Implements IDaoFacOperacionDetalleTm
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionDetalleTmsFiltro(ByVal FacOperacionDetalleTm As FacOperacionDetalleTm) As System.Collections.Generic.IList(Of FacOperacionDetalleTm) Implements Contrato.IDaoFacOperacionDetalleTm.ObtenerFacOperacionDetalleTmsFiltro
            Dim FacOperacionDetalleTms As IList(Of FacOperacionDetalleTm) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionDetalleTm)
            'If (FacOperacionDetalleTm IsNot Nothing) AndAlso (FacOperacionDetalleTm.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmId, FacOperacionDetalleTm.Id)
            '    variosFiltros = True
            'End If
            If (FacOperacionDetalleTm IsNot Nothing) AndAlso (FacOperacionDetalleTm.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmId, FacOperacionDetalleTm.Id)
                variosFiltros = True
            End If
            If (FacOperacionDetalleTm.Usuario IsNot Nothing) AndAlso (FacOperacionDetalleTm.Usuario.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmUsuario, FacOperacionDetalleTm.Usuario.Id)
                variosFiltros = True
            End If
            If (FacOperacionDetalleTm IsNot Nothing) AndAlso (FacOperacionDetalleTm.Detalle IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmDetalle, FacOperacionDetalleTm.Detalle)
                variosFiltros = True
            End If
            If (FacOperacionDetalleTm IsNot Nothing) AndAlso (FacOperacionDetalleTm.Codigo IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmCodigo, FacOperacionDetalleTm.Codigo)
                variosFiltros = True
            End If

            If (FacOperacionDetalleTm.Servicio IsNot Nothing) AndAlso (Not FacOperacionDetalleTm.Servicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleTmServicio, FacOperacionDetalleTm.Servicio.Id)
            End If



            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionDetalleTms = query.List(Of FacOperacionDetalleTm)()

            Return FacOperacionDetalleTms

        End Function


    End Class
End Namespace