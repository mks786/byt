Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacTarifaNHibernate
        Inherits DaoBaseNHibernate(Of FacTarifa, Integer)
        Implements IDaoFacTarifa
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Public Function ObtenerFacTarifasFiltro(ByVal FacTarifa As FacTarifa) As System.Collections.Generic.IList(Of FacTarifa) Implements Contrato.IDaoFacTarifa.ObtenerFacTarifasFiltro
            Dim FacTarifas As IList(Of FacTarifa) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacTarifa)

            If (FacTarifa IsNot Nothing) AndAlso (FacTarifa.Id <> 0) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacTarifaId, FacTarifa.Id)
                variosFiltros = True
            End If


            'If (FacTarifa IsNot Nothing) AndAlso (FacTarifa.Id IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacTarifaId, FacTarifa.Id)
            '    variosFiltros = True
            'End If


            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                'cabecera = cabecera & filtro & "  order by b.XBanco"
                cabecera += filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacTarifas = query.List(Of FacTarifa)()

            Return FacTarifas

        End Function
    End Class
End Namespace