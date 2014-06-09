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
    Public Class DaoFacVistaFacturaServicioNHibernate
        Inherits DaoBaseNHibernate(Of FacVistaFacturaServicio, Integer)
        Implements IDaoFacVistaFacturaServicio
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacVistaFacturaServiciosFiltro(ByVal FacVistaFacturaServicio As FacVistaFacturaServicio) As System.Collections.Generic.IList(Of FacVistaFacturaServicio) Implements Contrato.IDaoFacVistaFacturaServicio.ObtenerFacVistaFacturaServiciosFiltro
            Dim FacVistaFacturaServicios As IList(Of FacVistaFacturaServicio) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacVistaFacturaServicio)

            If (FacVistaFacturaServicio IsNot Nothing) AndAlso (FacVistaFacturaServicio.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturaServicioId, FacVistaFacturaServicio.Id)
                variosFiltros = True
            End If

            If (FacVistaFacturaServicio IsNot Nothing) AndAlso (FacVistaFacturaServicio.Tipo <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturaServicioTipo, FacVistaFacturaServicio.Tipo)
                variosFiltros = True
            End If

            If (FacVistaFacturaServicio IsNot Nothing) AndAlso (FacVistaFacturaServicio.Factura IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturaIdFactura, FacVistaFacturaServicio.Factura)
                variosFiltros = True
            End If

            If (FacVistaFacturaServicio IsNot Nothing) AndAlso (FacVistaFacturaServicio.CodigoAlterno IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturaCodigoAlterno, FacVistaFacturaServicio.CodigoAlterno)
                variosFiltros = True
            End If

            If (FacVistaFacturaServicio IsNot Nothing) AndAlso (FacVistaFacturaServicio.Referencia IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturaOurref, FacVistaFacturaServicio.Ourref)
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
            FacVistaFacturaServicios = query.List(Of FacVistaFacturaServicio)()

            Return FacVistaFacturaServicios

        End Function


    End Class
End Namespace