Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacBancoNHibernate
        Inherits DaoBaseNHibernate(Of FacBanco, Integer)
        Implements IDaoFacBanco
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Public Function ObtenerFacBancosFiltro(ByVal FacBanco As FacBanco) As System.Collections.Generic.IList(Of FacBanco) Implements Contrato.IDaoFacBanco.ObtenerFacBancosFiltro
            Dim FacBancos As IList(Of FacBanco) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacBanco)
            'If (FacBanco IsNot Nothing) AndAlso (FacBanco.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacBancoId, FacBanco.Id)
            '    variosFiltros = True
            'End If


            'If (FacBanco IsNot Nothing) AndAlso (FacBanco.Id IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacBancoId, FacBanco.Id)
            '    variosFiltros = True
            'End If
           

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & "  order by b.XBanco")
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro & "  order by b.XBanco"
                query = Session.CreateQuery(cabecera)
            End If
            FacBancos = query.List(Of FacBanco)()

            Return FacBancos

        End Function
    End Class
End Namespace