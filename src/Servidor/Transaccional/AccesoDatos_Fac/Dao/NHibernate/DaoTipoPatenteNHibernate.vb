Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoTipoPatenteNHibernate
        Inherits DaoBaseNHibernate(Of TipoPatente, String)
        Implements IDaoTipoPatente
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerTipoPatentesFiltro(ByVal TipoPatente As TipoPatente) As System.Collections.Generic.IList(Of TipoPatente) Implements Contrato.IDaoTipoPatente.ObtenerTipoPatentesFiltro
            Dim TipoPatentes As IList(Of TipoPatente) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerTipoPatente)


            If (TipoPatente IsNot Nothing) AndAlso (TipoPatente.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteId, TipoPatente.Id)
                variosFiltros = True
            End If
            'If (TipoPatente.Banco IsNot Nothing) AndAlso (Not TipoPatente.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteBanco, TipoPatente.Banco.Id)
            'End If
            'If (TipoPatente IsNot Nothing) AndAlso (TipoPatente.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteEstadoCuenta, TipoPatente.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (TipoPatente.Asociado IsNot Nothing) AndAlso (Not TipoPatente.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteAsociado, TipoPatente.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (TipoPatente.Banco IsNot Nothing) AndAlso (Not TipoPatente.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteBanco, TipoPatente.Banco.Id)
            '    variosFiltros = True
            'End If
            'If (TipoPatente.Idioma IsNot Nothing) AndAlso (TipoPatente.Idioma.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteIdioma, TipoPatente.Idioma.Id)
            '    variosFiltros = True
            'End If
            'If (TipoPatente.Moneda IsNot Nothing) AndAlso (TipoPatente.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteMoneda, TipoPatente.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (TipoPatente.FechaCobro IsNot Nothing) AndAlso (Not TipoPatente.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", TipoPatente.FechaCobro)
            '    Dim fechaday As DateTime = TipoPatente.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoPatenteFechaCobro, fecha, fecha2)
            '    variosFiltros = True
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            TipoPatentes = query.List(Of TipoPatente)()

            Return TipoPatentes

        End Function


    End Class
End Namespace