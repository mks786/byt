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
    Public Class DaoFacVistaFacturacionCxpInternaNHibernate
        Inherits DaoBaseNHibernate(Of FacVistaFacturacionCxpInterna, Integer)
        Implements IDaoFacVistaFacturacionCxpInterna
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacVistaFacturacionCxpInternasFiltro(ByVal FacVistaFacturacionCxpInterna As FacVistaFacturacionCxpInterna) As System.Collections.Generic.IList(Of FacVistaFacturacionCxpInterna) Implements Contrato.IDaoFacVistaFacturacionCxpInterna.ObtenerFacVistaFacturacionCxpInternasFiltro
            Dim FacVistaFacturacionCxpInternas As IList(Of FacVistaFacturacionCxpInterna) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacVistaFacturacionCxpInterna)


            If (FacVistaFacturacionCxpInterna.Asociado IsNot Nothing) AndAlso (Not FacVistaFacturacionCxpInterna.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturacionCxpInternaAsociado, FacVistaFacturacionCxpInterna.Asociado.Id)
                variosFiltros = True
            End If

            If (FacVistaFacturacionCxpInterna.Asociado_o IsNot Nothing) AndAlso (Not FacVistaFacturacionCxpInterna.Asociado_o.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturacionCxpInternaAsociado_o, FacVistaFacturacionCxpInterna.Asociado_o.Id)
                variosFiltros = True
            End If


            If (FacVistaFacturacionCxpInterna IsNot Nothing) AndAlso (Not FacVistaFacturacionCxpInterna.Cobrada <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacVistaFacturacionCxpInternaCobrada, FacVistaFacturacionCxpInterna.Cobrada)
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


            FacVistaFacturacionCxpInternas = query.List(Of FacVistaFacturacionCxpInterna)()

            Return FacVistaFacturacionCxpInternas

        End Function


    End Class
End Namespace