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
    Public Class DaoFacOperacionProformaNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionProforma, Integer)
        Implements IDaoFacOperacionProforma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionProformasFiltro(ByVal FacOperacionProforma As FacOperacionProforma) As System.Collections.Generic.IList(Of FacOperacionProforma) Implements Contrato.IDaoFacOperacionProforma.ObtenerFacOperacionProformasFiltro
            Dim FacOperacionProformas As IList(Of FacOperacionProforma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionProforma)
            ''If (FacOperacionProforma IsNot Nothing) AndAlso (FacOperacionProforma.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaId, FacOperacionProforma.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacionProforma.Id IsNot Nothing) AndAlso (Not FacOperacionProforma.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaId, FacOperacionProforma.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacOperacionProforma.Banco IsNot Nothing) AndAlso (Not FacOperacionProforma.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaBanco, FacOperacionProforma.Banco.Id)
            'End If
            If (FacOperacionProforma IsNot Nothing) AndAlso (Not FacOperacionProforma.CodigoOperacion.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaCodigoOperacion, FacOperacionProforma.CodigoOperacion)
                variosFiltros = True
            End If
            If (FacOperacionProforma IsNot Nothing) AndAlso (FacOperacionProforma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaId, FacOperacionProforma.Id)
                variosFiltros = True
            End If

            'If (FacOperacionProforma.Fecha IsNot Nothing) AndAlso (Not FacOperacionProforma.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionProforma.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionProforma.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaFecha, fecha, fecha2)
            'End If
            'If (FacOperacionProforma.FechaReg IsNot Nothing) AndAlso (Not FacOperacionProforma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionProforma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionProforma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionProformaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionProformas = query.List(Of FacOperacionProforma)()

            Return FacOperacionProformas

        End Function


    End Class
End Namespace