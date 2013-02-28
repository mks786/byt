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
    Public Class DaoFacFacturaPendienteConGruNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaPendienteConGru, Integer)
        Implements IDaoFacFacturaPendienteConGru
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaPendienteConGrusFiltro(ByVal FacFacturaPendienteConGru As FacFacturaPendienteConGru) As System.Collections.Generic.IList(Of FacFacturaPendienteConGru) Implements Contrato.IDaoFacFacturaPendienteConGru.ObtenerFacFacturaPendienteConGrusFiltro
            Dim FacFacturaPendienteConGrus As IList(Of FacFacturaPendienteConGru) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaPendienteConGru)

            If (FacFacturaPendienteConGru IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteConGruId, FacFacturaPendienteConGru.Id)
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
            FacFacturaPendienteConGrus = query.List(Of FacFacturaPendienteConGru)()

            Return FacFacturaPendienteConGrus

        End Function


    End Class
End Namespace