Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoBancoGNHibernate
        Inherits DaoBaseNHibernate(Of BancoG, Integer)
        Implements IDaoBancoG
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerBancoGsFiltro(ByVal BancoG As BancoG) As System.Collections.Generic.IList(Of BancoG) Implements Contrato.IDaoBancoG.ObtenerBancoGsFiltro
            Dim BancoGs As IList(Of BancoG) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerBancoG)
            'If (BancoG IsNot Nothing) AndAlso (BancoG.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerBancoGId, BancoG.Id)
            '    variosFiltros = True
            'End If


            'If (BancoG IsNot Nothing) AndAlso (BancoG.Id IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerBancoGId, BancoG.Id)
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
            BancoGs = query.List(Of BancoG)()

            Return BancoGs

        End Function
    End Class
End Namespace