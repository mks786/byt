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
    Public Class DaoFacDesgloseColeNHibernate
        Inherits DaoBaseNHibernate(Of FacDesgloseCole, Char)
        Implements IDaoFacDesgloseCole
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Public Function ObtenerFacDesgloseColesFiltro(ByVal FacDesgloseCole As FacDesgloseCole) As System.Collections.Generic.IList(Of FacDesgloseCole) Implements Contrato.IDaoFacDesgloseCole.ObtenerFacDesgloseColesFiltro
            Dim FacDesgloseColes As IList(Of FacDesgloseCole) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacDesgloseCole)
            'If (FacDesgloseCole IsNot Nothing) AndAlso (FacDesgloseCole.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeId, FacDesgloseCole.Id)
            '    variosFiltros = True
            'End If


            If (FacDesgloseCole IsNot Nothing) AndAlso (FacDesgloseCole.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeId, FacDesgloseCole.Id)
                variosFiltros = True
            End If
            If (FacDesgloseCole.Idioma IsNot Nothing) AndAlso (FacDesgloseCole.Idioma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeIdioma, FacDesgloseCole.Idioma.Id)
                variosFiltros = True
            End If
            'If (FacDesgloseCole.Servicio IsNot Nothing) AndAlso (FacDesgloseCole.Servicio.Cod_Cont <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeServicioCod_Cont, FacDesgloseCole.Servicio.Cod_Cont)
            '    variosFiltros = True
            'End If
            'If (FacDesgloseCole.Servicio IsNot Nothing) AndAlso (FacDesgloseCole.Servicio.Xreferencia <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeServicioXreferencia, FacDesgloseCole.Servicio.Xreferencia)
            '    variosFiltros = True
            'End If

            'If (FacDesgloseCole.Servicio IsNot Nothing) Then
            '    If FacDesgloseCole.Servicio.Itipo.Equals(" "c) Then
            '    Else

            '        If variosFiltros Then
            '            filtro += " and "
            '        End If
            '        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeServicioItipo, FacDesgloseCole.Servicio.Itipo)
            '        variosFiltros = True
            '    End If
            'End If
            'If (FacDesgloseCole.Servicio IsNot Nothing) Then
            '    If FacDesgloseCole.Servicio.Local.Equals(" "c) Then
            '    Else

            '        If variosFiltros Then
            '            filtro += " and "
            '        End If
            '        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseColeServicioLocal, FacDesgloseCole.Servicio.Local)
            '        variosFiltros = True
            '    End If
            'End If


            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacDesgloseColes = query.List(Of FacDesgloseCole)()

            Return FacDesgloseColes

        End Function
    End Class
End Namespace