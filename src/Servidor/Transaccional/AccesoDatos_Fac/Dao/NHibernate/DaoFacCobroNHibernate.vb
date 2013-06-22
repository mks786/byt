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
    Public Class DaoFacCobroNHibernate
        Inherits DaoBaseNHibernate(Of FacCobro, Integer)
        Implements IDaoFacCobro
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacCobrosFiltro(ByVal FacCobro As FacCobro) As System.Collections.Generic.IList(Of FacCobro) Implements Contrato.IDaoFacCobro.ObtenerFacCobrosFiltro
            Dim FacCobros As IList(Of FacCobro) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacCobro)


            If (FacCobro IsNot Nothing) AndAlso (FacCobro.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroId, FacCobro.Id)
                variosFiltros = True
            End If
            'If (FacCobro.Banco IsNot Nothing) AndAlso (Not FacCobro.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroBanco, FacCobro.Banco.Id)
            'End If
            If (FacCobro IsNot Nothing) AndAlso (FacCobro.EstadoCuenta IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroEstadoCuenta, FacCobro.EstadoCuenta)
                variosFiltros = True
            End If
            If (FacCobro.Asociado IsNot Nothing) AndAlso (Not FacCobro.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroAsociado, FacCobro.Asociado.Id)
                variosFiltros = True
            End If
            If (FacCobro.Banco IsNot Nothing) AndAlso (Not FacCobro.Banco.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroBanco, FacCobro.Banco.Id)
                variosFiltros = True
            End If
            If (FacCobro.Idioma IsNot Nothing) AndAlso (FacCobro.Idioma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroIdioma, FacCobro.Idioma.Id)
                variosFiltros = True
            End If
            If (FacCobro.Moneda IsNot Nothing) AndAlso (FacCobro.Moneda.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroMoneda, FacCobro.Moneda.Id)
                variosFiltros = True
            End If
            If (FacCobro.FechaCobro IsNot Nothing) AndAlso (Not FacCobro.FechaCobro.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacCobro.FechaCobro)
                Dim fechaday As DateTime = FacCobro.FechaCobro
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFechaCobro, fecha, fecha2)
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & "  order by c.Id desc")
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro & "  order by c.Id desc"
                query = Session.CreateQuery(cabecera)
            End If
            FacCobros = query.List(Of FacCobro)()

            Return FacCobros

        End Function


    End Class
End Namespace