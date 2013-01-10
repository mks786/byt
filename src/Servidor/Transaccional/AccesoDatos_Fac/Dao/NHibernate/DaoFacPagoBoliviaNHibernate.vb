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
    Public Class DaoFacPagoBoliviaNHibernate
        Inherits DaoBaseNHibernate(Of FacPagoBolivia, Integer)
        Implements IDaoFacPagoBolivia
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacPagoBoliviasFiltro(ByVal FacPagoBolivia As FacPagoBolivia) As System.Collections.Generic.IList(Of FacPagoBolivia) Implements Contrato.IDaoFacPagoBolivia.ObtenerFacPagoBoliviasFiltro
            Dim FacPagoBolivias As IList(Of FacPagoBolivia) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacPagoBolivia)
            'If (FacPagoBolivia IsNot Nothing) AndAlso (FacPagoBolivia.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaId, FacPagoBolivia.Id)
            '    variosFiltros = True
            'End If
            If (FacPagoBolivia.Id IsNot Nothing) AndAlso (Not FacPagoBolivia.Id.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaId, FacPagoBolivia.Id.Id)
                variosFiltros = True
            End If
            If (FacPagoBolivia.BancoRec IsNot Nothing) AndAlso (Not FacPagoBolivia.BancoRec.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaBancoRec, FacPagoBolivia.BancoRec.Id)
            End If
            If (FacPagoBolivia.BancoPag IsNot Nothing) AndAlso (Not FacPagoBolivia.BancoPag.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaBancoPag, FacPagoBolivia.BancoPag.Id)
            End If

            If (FacPagoBolivia.PagoRec.Equals(" "c)) Then
            Else

                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaPagoRec, FacPagoBolivia.PagoRec)
                variosFiltros = True
            End If
            If (FacPagoBolivia IsNot Nothing) AndAlso (FacPagoBolivia.IPagado <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaIPagado, FacPagoBolivia.IPagado)
                variosFiltros = True
            End If

            If (FacPagoBolivia.FechaBanco IsNot Nothing) AndAlso (Not FacPagoBolivia.FechaBanco.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacPagoBolivia.FechaBanco)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacPagoBolivia.FechaBanco)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaFechaBanco, fecha, fecha2)
            End If
            If (FacPagoBolivia.FechaReg IsNot Nothing) AndAlso (Not FacPagoBolivia.FechaReg.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacPagoBolivia.FechaReg)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacPagoBolivia.FechaReg)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacPagoBoliviaFechaReg, fecha, fecha2)
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacPagoBolivias = query.List(Of FacPagoBolivia)()

            Return FacPagoBolivias

        End Function


    End Class
End Namespace