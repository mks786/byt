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
    Public Class DaoFacFactuDetalleNHibernate
        Inherits DaoBaseNHibernate(Of FacFactuDetalle, Integer)
        Implements IDaoFacFactuDetalle
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFactuDetallesFiltro(ByVal FacFactuDetalle As FacFactuDetalle) As System.Collections.Generic.IList(Of FacFactuDetalle) Implements Contrato.IDaoFacFactuDetalle.ObtenerFacFactuDetallesFiltro
            Dim FacFactuDetalles As IList(Of FacFactuDetalle) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFactuDetalle)
            ''If (FacFactuDetalle IsNot Nothing) AndAlso (FacFactuDetalle.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleId, FacFactuDetalle.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacFactuDetalle.Id IsNot Nothing) AndAlso (Not FacFactuDetalle.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleId, FacFactuDetalle.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacFactuDetalle.Servicio IsNot Nothing) AndAlso (Not FacFactuDetalle.Servicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleServicio, FacFactuDetalle.Servicio.Id)
            End If

            If (FacFactuDetalle.Factura IsNot Nothing) AndAlso (Not FacFactuDetalle.Factura.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleFactura, FacFactuDetalle.Factura.Id)
                variosFiltros = True
            End If

            'If (FacFactuDetalle IsNot Nothing) AndAlso (FacFactuDetalle.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleNCheque, FacFactuDetalle.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacFactuDetalle IsNot Nothing) AndAlso (FacFactuDetalle.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleDeposito, FacFactuDetalle.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacFactuDetalle.Fecha IsNot Nothing) AndAlso (Not FacFactuDetalle.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetalle.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetalle.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleFecha, fecha, fecha2)
            'End If
            'If (FacFactuDetalle.FechaReg IsNot Nothing) AndAlso (Not FacFactuDetalle.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetalle.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetalle.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetalleFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFactuDetalles = query.List(Of FacFactuDetalle)()

            Return FacFactuDetalles

        End Function


    End Class
End Namespace