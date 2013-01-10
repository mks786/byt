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
    Public Class DaoFacOperacionAnuladaNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionAnulada, Integer)
        Implements IDaoFacOperacionAnulada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionAnuladasFiltro(ByVal FacOperacionAnulada As FacOperacionAnulada) As System.Collections.Generic.IList(Of FacOperacionAnulada) Implements Contrato.IDaoFacOperacionAnulada.ObtenerFacOperacionAnuladasFiltro
            Dim FacOperacionAnuladas As IList(Of FacOperacionAnulada) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionAnulada)

            If (FacOperacionAnulada IsNot Nothing) AndAlso (FacOperacionAnulada.Id <> "") Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaId, FacOperacionAnulada.Id)
                variosFiltros = True
            End If
            'If (FacOperacionAnulada.Id IsNot Nothing) AndAlso (Not FacOperacionAnulada.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaId, FacOperacionAnulada.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacOperacionAnulada.Banco IsNot Nothing) AndAlso (Not FacOperacionAnulada.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaBanco, FacOperacionAnulada.Banco.Id)
            'End If
            'If (FacOperacionAnulada IsNot Nothing) AndAlso (FacOperacionAnulada.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaNCheque, FacOperacionAnulada.NCheque)
            '    variosFiltros = True
            'End If
            If (FacOperacionAnulada IsNot Nothing) AndAlso (FacOperacionAnulada.CodigoOperacion IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaCodigoOperacion, FacOperacionAnulada.CodigoOperacion)
                variosFiltros = True
            End If

            'If (FacOperacionAnulada.Fecha IsNot Nothing) AndAlso (Not FacOperacionAnulada.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionAnulada.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionAnulada.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaFecha, fecha, fecha2)
            'End If
            'If (FacOperacionAnulada.FechaReg IsNot Nothing) AndAlso (Not FacOperacionAnulada.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionAnulada.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionAnulada.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAnuladaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionAnuladas = query.List(Of FacOperacionAnulada)()

            Return FacOperacionAnuladas

        End Function


    End Class
End Namespace