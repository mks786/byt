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
    Public Class DaoFacOperacionDetaAnuladaNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionDetaAnulada, Integer)
        Implements IDaoFacOperacionDetaAnulada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionDetaAnuladasFiltro(ByVal FacOperacionDetaAnulada As FacOperacionDetaAnulada) As System.Collections.Generic.IList(Of FacOperacionDetaAnulada) Implements Contrato.IDaoFacOperacionDetaAnulada.ObtenerFacOperacionDetaAnuladasFiltro
            Dim FacOperacionDetaAnuladas As IList(Of FacOperacionDetaAnulada) = Nothing
            'Dim variosFiltros As Boolean = False
            'Dim filtro As String = ""
            'Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionDetaAnulada)
            ''If (FacOperacionDetaAnulada IsNot Nothing) AndAlso (FacOperacionDetaAnulada.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaId, FacOperacionDetaAnulada.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacionDetaAnulada.Id IsNot Nothing) AndAlso (Not FacOperacionDetaAnulada.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaId, FacOperacionDetaAnulada.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacOperacionDetaAnulada.Banco IsNot Nothing) AndAlso (Not FacOperacionDetaAnulada.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaBanco, FacOperacionDetaAnulada.Banco.Id)
            'End If
            'If (FacOperacionDetaAnulada IsNot Nothing) AndAlso (FacOperacionDetaAnulada.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaNCheque, FacOperacionDetaAnulada.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacOperacionDetaAnulada IsNot Nothing) AndAlso (FacOperacionDetaAnulada.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaDeposito, FacOperacionDetaAnulada.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacOperacionDetaAnulada.Fecha IsNot Nothing) AndAlso (Not FacOperacionDetaAnulada.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaAnulada.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaAnulada.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaFecha, fecha, fecha2)
            'End If
            'If (FacOperacionDetaAnulada.FechaReg IsNot Nothing) AndAlso (Not FacOperacionDetaAnulada.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaAnulada.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaAnulada.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaAnuladaFechaReg, fecha, fecha2)
            'End If

            'Dim query As IQuery
            'If (filtro = "") Then
            '    query = Session.CreateQuery(cabecera)
            'Else
            '    cabecera = cabecera & " Where "
            '    cabecera = cabecera & filtro
            '    query = Session.CreateQuery(cabecera)
            'End If
            'FacOperacionDetaAnuladas = query.List(Of FacOperacionDetaAnulada)()

            Return FacOperacionDetaAnuladas

        End Function


    End Class
End Namespace