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
    Public Class DaoFacFactuDetaAnuladaNHibernate
        Inherits DaoBaseNHibernate(Of FacFactuDetaAnulada, Integer)
        Implements IDaoFacFactuDetaAnulada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFactuDetaAnuladasFiltro(ByVal FacFactuDetaAnulada As FacFactuDetaAnulada) As System.Collections.Generic.IList(Of FacFactuDetaAnulada) Implements Contrato.IDaoFacFactuDetaAnulada.ObtenerFacFactuDetaAnuladasFiltro
            Dim FacFactuDetaAnuladas As IList(Of FacFactuDetaAnulada) = Nothing
            'Dim variosFiltros As Boolean = False
            'Dim filtro As String = ""
            'Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFactuDetaAnulada)
            ''If (FacFactuDetaAnulada IsNot Nothing) AndAlso (FacFactuDetaAnulada.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaId, FacFactuDetaAnulada.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacFactuDetaAnulada.Id IsNot Nothing) AndAlso (Not FacFactuDetaAnulada.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaId, FacFactuDetaAnulada.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacFactuDetaAnulada.Banco IsNot Nothing) AndAlso (Not FacFactuDetaAnulada.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaBanco, FacFactuDetaAnulada.Banco.Id)
            'End If
            'If (FacFactuDetaAnulada IsNot Nothing) AndAlso (FacFactuDetaAnulada.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaNCheque, FacFactuDetaAnulada.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacFactuDetaAnulada IsNot Nothing) AndAlso (FacFactuDetaAnulada.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaDeposito, FacFactuDetaAnulada.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacFactuDetaAnulada.Fecha IsNot Nothing) AndAlso (Not FacFactuDetaAnulada.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaAnulada.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaAnulada.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaFecha, fecha, fecha2)
            'End If
            'If (FacFactuDetaAnulada.FechaReg IsNot Nothing) AndAlso (Not FacFactuDetaAnulada.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaAnulada.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaAnulada.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaAnuladaFechaReg, fecha, fecha2)
            'End If

            'Dim query As IQuery
            'If (filtro = "") Then
            '    query = Session.CreateQuery(cabecera)
            'Else
            '    cabecera = cabecera & " Where "
            '    cabecera = cabecera & filtro
            '    query = Session.CreateQuery(cabecera)
            'End If
            'FacFactuDetaAnuladas = query.List(Of FacFactuDetaAnulada)()

            Return FacFactuDetaAnuladas

        End Function


    End Class
End Namespace