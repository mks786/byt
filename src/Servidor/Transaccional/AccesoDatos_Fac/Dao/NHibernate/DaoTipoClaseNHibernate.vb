Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoTipoClaseNHibernate
        Inherits DaoBaseNHibernate(Of TipoClase, String)
        Implements IDaoTipoClase
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerTipoClasesFiltro(ByVal TipoClase As TipoClase) As System.Collections.Generic.IList(Of TipoClase) Implements Contrato.IDaoTipoClase.ObtenerTipoClasesFiltro
            Dim TipoClases As IList(Of TipoClase) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerTipoClase)


            If (TipoClase IsNot Nothing) AndAlso (TipoClase.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseId, TipoClase.Id)
                variosFiltros = True
            End If
            'If (TipoClase.Banco IsNot Nothing) AndAlso (Not TipoClase.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseBanco, TipoClase.Banco.Id)
            'End If
            'If (TipoClase IsNot Nothing) AndAlso (TipoClase.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseEstadoCuenta, TipoClase.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (TipoClase.Asociado IsNot Nothing) AndAlso (Not TipoClase.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseAsociado, TipoClase.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (TipoClase.Banco IsNot Nothing) AndAlso (Not TipoClase.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseBanco, TipoClase.Banco.Id)
            '    variosFiltros = True
            'End If
            'If (TipoClase.Idioma IsNot Nothing) AndAlso (TipoClase.Idioma.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseIdioma, TipoClase.Idioma.Id)
            '    variosFiltros = True
            'End If
            'If (TipoClase.Moneda IsNot Nothing) AndAlso (TipoClase.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseMoneda, TipoClase.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (TipoClase.FechaCobro IsNot Nothing) AndAlso (Not TipoClase.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", TipoClase.FechaCobro)
            '    Dim fechaday As DateTime = TipoClase.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoClaseFechaCobro, fecha, fecha2)
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
            TipoClases = query.List(Of TipoClase)()

            Return TipoClases

        End Function
    End Class
End Namespace