Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoTasaNHibernate
        Inherits DaoBaseNHibernate(Of Tasa, DateTime)
        Implements IDaoTasa
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerTasasFiltro(ByVal Tasa As Tasa) As System.Collections.Generic.IList(Of Tasa) Implements Contrato.IDaoTasa.ObtenerTasasFiltro
            Dim Tasas As IList(Of Tasa) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerTasa)
            'If (Tasa IsNot Nothing) AndAlso (Tasa.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaId, Tasa.Id)
            '    variosFiltros = True
            'End If
            'If (Tasa.Id IsNot Nothing) AndAlso (Not Tasa.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaId, Tasa.Id.Id)
            '    variosFiltros = True
            'End If
            'If (Tasa.Banco IsNot Nothing) AndAlso (Not Tasa.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaBanco, Tasa.Banco.Id)
            'End If
            If (Tasa IsNot Nothing) AndAlso (Tasa.Moneda <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaMoneda, Tasa.Moneda)
                variosFiltros = True
                'End If
                'If (Tasa IsNot Nothing) AndAlso (Tasa.Deposito <> "") Then
                '    If variosFiltros Then
                '        filtro += " and "
                '    End If
                '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaDeposito, Tasa.Deposito)
                '    variosFiltros = True
            End If

            If (Tasa.Id.ToString IsNot Nothing) AndAlso (Not Tasa.Id.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", Tasa.Id)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", Tasa.Id.AddDays(1))
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaId, fecha, fecha2)
            End If
            'If (Tasa.FechaReg IsNot Nothing) AndAlso (Not Tasa.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", Tasa.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", Tasa.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTasaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            Tasas = query.List(Of Tasa)()

            Return Tasas

        End Function


    End Class

End Namespace