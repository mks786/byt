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
    Public Class DaoFacCreditoNHibernate
        Inherits DaoBaseNHibernate(Of FacCredito, Integer)
        Implements IDaoFacCredito
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacCreditosFiltro(ByVal FacCredito As FacCredito) As System.Collections.Generic.IList(Of FacCredito) Implements Contrato.IDaoFacCredito.ObtenerFacCreditosFiltro
            Dim FacCreditos As IList(Of FacCredito) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacCredito)
            'If (FacCredito IsNot Nothing) AndAlso (FacCredito.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoId, FacCredito.Id)
            '    variosFiltros = True
            'End If


            If (FacCredito IsNot Nothing) AndAlso (FacCredito.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoId, FacCredito.Id)
                variosFiltros = True
            End If
            If (FacCredito IsNot Nothing) AndAlso (FacCredito.CreditoSent IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoCreditoSent, FacCredito.CreditoSent)
                variosFiltros = True
            End If
            If (FacCredito.Asociado IsNot Nothing) AndAlso (Not FacCredito.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoAsociado, FacCredito.Asociado.Id)
                variosFiltros = True
            End If
            If (FacCredito.Banco IsNot Nothing) AndAlso (Not FacCredito.Banco.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoBanco, FacCredito.Banco.Id)
                variosFiltros = True
            End If
            If (FacCredito.Idioma IsNot Nothing) AndAlso (FacCredito.Idioma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoIdioma, FacCredito.Idioma.Id)
                variosFiltros = True
            End If
            If (FacCredito.Moneda IsNot Nothing) AndAlso (FacCredito.Moneda.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoMoneda, FacCredito.Moneda.Id)
                variosFiltros = True
            End If
            If (FacCredito.FechaCredito IsNot Nothing) AndAlso (Not FacCredito.FechaCredito.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacCredito.FechaCredito)
                Dim fechaday As DateTime = FacCredito.FechaCredito
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCreditoFechaCredito, fecha, fecha2)
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & "  order by c.Id desc ")
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro & "  order by c.Id desc "
                query = Session.CreateQuery(cabecera)
            End If
            FacCreditos = query.List(Of FacCredito)()

            Return FacCreditos

        End Function


    End Class
End Namespace