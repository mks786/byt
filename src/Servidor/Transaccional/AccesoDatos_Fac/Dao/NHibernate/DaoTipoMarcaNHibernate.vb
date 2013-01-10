Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoTipoMarcaNHibernate
        Inherits DaoBaseNHibernate(Of TipoMarca, String)
        Implements IDaoTipoMarca
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerTipoMarcasFiltro(ByVal TipoMarca As TipoMarca) As System.Collections.Generic.IList(Of TipoMarca) Implements Contrato.IDaoTipoMarca.ObtenerTipoMarcasFiltro
            Dim TipoMarcas As IList(Of TipoMarca) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerTipoMarca)


            If (TipoMarca IsNot Nothing) AndAlso (TipoMarca.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaId, TipoMarca.Id)
                variosFiltros = True
            End If
            'If (TipoMarca.Banco IsNot Nothing) AndAlso (Not TipoMarca.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaBanco, TipoMarca.Banco.Id)
            'End If
            'If (TipoMarca IsNot Nothing) AndAlso (TipoMarca.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaEstadoCuenta, TipoMarca.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (TipoMarca.Asociado IsNot Nothing) AndAlso (Not TipoMarca.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaAsociado, TipoMarca.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (TipoMarca.Banco IsNot Nothing) AndAlso (Not TipoMarca.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaBanco, TipoMarca.Banco.Id)
            '    variosFiltros = True
            'End If
            'If (TipoMarca.Idioma IsNot Nothing) AndAlso (TipoMarca.Idioma.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaIdioma, TipoMarca.Idioma.Id)
            '    variosFiltros = True
            'End If
            'If (TipoMarca.Moneda IsNot Nothing) AndAlso (TipoMarca.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaMoneda, TipoMarca.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (TipoMarca.FechaCobro IsNot Nothing) AndAlso (Not TipoMarca.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", TipoMarca.FechaCobro)
            '    Dim fechaday As DateTime = TipoMarca.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerTipoMarcaFechaCobro, fecha, fecha2)
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
            TipoMarcas = query.List(Of TipoMarca)()

            Return TipoMarcas

        End Function


    End Class
End Namespace