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
    Public Class DaoFacFacturaAnuladaNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaAnulada, Integer)
        Implements IDaoFacFacturaAnulada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaAnuladasFiltro(ByVal FacFacturaAnulada As FacFacturaAnulada) As System.Collections.Generic.IList(Of FacFacturaAnulada) Implements Contrato.IDaoFacFacturaAnulada.ObtenerFacFacturaAnuladasFiltro
            Dim FacFacturaAnuladas As IList(Of FacFacturaAnulada) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaAnulada)
            If (FacFacturaAnulada IsNot Nothing) AndAlso (FacFacturaAnulada.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaId, FacFacturaAnulada.Id)
                variosFiltros = True
            End If
            'If (FacFacturaAnulada.Id IsNot Nothing) AndAlso (Not FacFacturaAnulada.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaId, FacFacturaAnulada.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacFacturaAnulada.Asociado IsNot Nothing) AndAlso (Not FacFacturaAnulada.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaAsociado, FacFacturaAnulada.Asociado.Id)
            End If
            If (FacFacturaAnulada IsNot Nothing) AndAlso (FacFacturaAnulada.Anulada <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaAnulada, FacFacturaAnulada.Anulada)
                variosFiltros = True
            End If
            'If (FacFacturaAnulada IsNot Nothing) AndAlso (FacFacturaAnulada.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaDeposito, FacFacturaAnulada.Deposito)
            '    variosFiltros = True
            'End If

            If (FacFacturaAnulada.FechaAnulacion IsNot Nothing) AndAlso (Not FacFacturaAnulada.FechaAnulacion.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaAnulada.FechaAnulacion)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaAnulada.FechaAnulacion)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaFechaAnulacion, fecha, fecha2)
            End If
            'If (FacFacturaAnulada.FechaReg IsNot Nothing) AndAlso (Not FacFacturaAnulada.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaAnulada.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaAnulada.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnuladaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFacturaAnuladas = query.List(Of FacFacturaAnulada)()

            Return FacFacturaAnuladas

        End Function


    End Class
End Namespace