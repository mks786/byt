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
    Public Class DaoChequeRecidoNHibernate
        Inherits DaoBaseNHibernate(Of ChequeRecido, Integer)
        Implements IDaoChequeRecido
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Public Function ObtenerChequeRecidosFiltro(ByVal ChequeRecido As ChequeRecido) As System.Collections.Generic.IList(Of ChequeRecido) Implements Contrato.IDaoChequeRecido.ObtenerChequeRecidosFiltro
            Dim ChequeRecidos As IList(Of ChequeRecido) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerChequeRecido)
            'If (ChequeRecido IsNot Nothing) AndAlso (ChequeRecido.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoId, ChequeRecido.Id)
            '    variosFiltros = True
            'End If
            If (ChequeRecido.Id IsNot Nothing) AndAlso (Not ChequeRecido.Id.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoId, ChequeRecido.Id.Id)
                variosFiltros = True
            End If
            If (ChequeRecido.Banco IsNot Nothing) AndAlso (Not ChequeRecido.Banco.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoBanco, ChequeRecido.Banco.Id)
            End If
            If (ChequeRecido IsNot Nothing) AndAlso (ChequeRecido.NCheque <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoNCheque, ChequeRecido.NCheque)
                variosFiltros = True
            End If
            If (ChequeRecido IsNot Nothing) AndAlso (ChequeRecido.Deposito <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoDeposito, ChequeRecido.Deposito)
                variosFiltros = True
            End If

            If (ChequeRecido.Fecha IsNot Nothing) AndAlso (Not ChequeRecido.Fecha.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", ChequeRecido.Fecha)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", ChequeRecido.Fecha)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoFecha, fecha, fecha2)
            End If
            If (ChequeRecido.FechaReg IsNot Nothing) AndAlso (Not ChequeRecido.FechaReg.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", ChequeRecido.FechaReg)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", ChequeRecido.FechaReg)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerChequeRecidoFechaReg, fecha, fecha2)
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            ChequeRecidos = query.List(Of ChequeRecido)()

            Return ChequeRecidos

        End Function


    End Class
End Namespace