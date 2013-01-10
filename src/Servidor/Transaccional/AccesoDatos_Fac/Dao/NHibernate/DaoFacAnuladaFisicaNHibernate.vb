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
    Public Class DaoFacAnuladaFisicaNHibernate
        Inherits DaoBaseNHibernate(Of FacAnuladaFisica, Integer)
        Implements IDaoFacAnuladaFisica
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacAnuladaFisicasFiltro(ByVal FacAnuladaFisica As FacAnuladaFisica) As System.Collections.Generic.IList(Of FacAnuladaFisica) Implements Contrato.IDaoFacAnuladaFisica.ObtenerFacAnuladaFisicasFiltro
            Dim FacAnuladaFisicas As IList(Of FacAnuladaFisica) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            'Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacAnuladaFisica)
            ''If (FacAnuladaFisica IsNot Nothing) AndAlso (FacAnuladaFisica.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaId, FacAnuladaFisica.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacAnuladaFisica IsNot Nothing) AndAlso (FacAnuladaFisica.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaId, FacAnuladaFisica.Id)
            '    variosFiltros = True
            'End If
            'If (FacAnuladaFisica.Usuario IsNot Nothing) AndAlso (FacAnuladaFisica.Usuario.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaUsuario, FacAnuladaFisica.Usuario.Id)
            '    variosFiltros = True
            'End If
            'If (FacAnuladaFisica IsNot Nothing) AndAlso (FacAnuladaFisica.Detalle IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaDetalle, FacAnuladaFisica.Detalle)
            '    variosFiltros = True
            'End If
            'If (FacAnuladaFisica IsNot Nothing) AndAlso (FacAnuladaFisica.Codigo IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaCodigo, FacAnuladaFisica.Codigo)
            '    variosFiltros = True
            'End If

            'If (FacAnuladaFisica.Servicio IsNot Nothing) AndAlso (Not FacAnuladaFisica.Servicio.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacAnuladaFisicaServicio, FacAnuladaFisica.Servicio.Id)
            'End If



            'Dim query As IQuery
            'If (filtro = "") Then
            '    query = Session.CreateQuery(cabecera)
            'Else
            '    cabecera = cabecera & " Where "
            '    cabecera = cabecera & filtro
            '    query = Session.CreateQuery(cabecera)
            'End If
            'FacAnuladaFisicas = query.List(Of FacAnuladaFisica)()

            Return FacAnuladaFisicas

        End Function


    End Class
End Namespace