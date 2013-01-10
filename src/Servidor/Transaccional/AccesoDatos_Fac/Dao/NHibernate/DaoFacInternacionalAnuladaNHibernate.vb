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
    Public Class DaoFacInternacionalAnuladaNHibernate
        Inherits DaoBaseNHibernate(Of FacInternacionalAnulada, Integer)
        Implements IDaoFacInternacionalAnulada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacInternacionalAnuladasFiltro(ByVal FacInternacionalAnulada As FacInternacionalAnulada) As System.Collections.Generic.IList(Of FacInternacionalAnulada) Implements Contrato.IDaoFacInternacionalAnulada.ObtenerFacInternacionalAnuladasFiltro
            Dim FacInternacionalAnuladaes As IList(Of FacInternacionalAnulada) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacInternacionalAnulada)


            If (FacInternacionalAnulada IsNot Nothing) AndAlso (Not FacInternacionalAnulada.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaId, FacInternacionalAnulada.Id)
                variosFiltros = True
            End If
            'If (FacInternacionalAnulada.Banco IsNot Nothing) AndAlso (Not FacInternacionalAnulada.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaBanco, FacInternacionalAnulada.Banco.Id)
            'End If
            'If (FacInternacionalAnulada IsNot Nothing) AndAlso (FacInternacionalAnulada.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaEstadoCuenta, FacInternacionalAnulada.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (FacInternacionalAnulada.Asociado IsNot Nothing) AndAlso (Not FacInternacionalAnulada.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaAsociado, FacInternacionalAnulada.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacionalAnulada.Banco IsNot Nothing) AndAlso (Not FacInternacionalAnulada.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaBanco, FacInternacionalAnulada.Banco.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacionalAnulada.Idioma IsNot Nothing) AndAlso (FacInternacionalAnulada.Idioma.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaIdioma, FacInternacionalAnulada.Idioma.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacionalAnulada.Moneda IsNot Nothing) AndAlso (FacInternacionalAnulada.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaMoneda, FacInternacionalAnulada.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacionalAnulada.FechaCobro IsNot Nothing) AndAlso (Not FacInternacionalAnulada.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacInternacionalAnulada.FechaCobro)
            '    Dim fechaday As DateTime = FacInternacionalAnulada.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAnuladaFechaCobro, fecha, fecha2)
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
            FacInternacionalAnuladaes = query.List(Of FacInternacionalAnulada)()

            Return FacInternacionalAnuladaes

        End Function


    End Class
End Namespace