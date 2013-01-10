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
    Public Class DaoFacOperacionDetalleNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionDetalle, Integer)
        Implements IDaoFacOperacionDetalle
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionDetallesFiltro(ByVal FacOperacionDetalle As FacOperacionDetalle) As System.Collections.Generic.IList(Of FacOperacionDetalle) Implements Contrato.IDaoFacOperacionDetalle.ObtenerFacOperacionDetallesFiltro
            Dim FacOperacionDetalles As IList(Of FacOperacionDetalle) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionDetalle)

            If (FacOperacionDetalle.Factura IsNot Nothing) AndAlso (FacOperacionDetalle.Factura.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleFactura, FacOperacionDetalle.Factura.Id)
                variosFiltros = True
            End If
            If (FacOperacionDetalle IsNot Nothing) AndAlso (FacOperacionDetalle.Detalle IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleDetalle, FacOperacionDetalle.Detalle)
                variosFiltros = True
            End If
            'If (FacOperacionDetalle.Banco IsNot Nothing) AndAlso (Not FacOperacionDetalle.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleBanco, FacOperacionDetalle.Banco.Id)
            'End If
            'If (FacOperacionDetalle IsNot Nothing) AndAlso (FacOperacionDetalle.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleNCheque, FacOperacionDetalle.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacOperacionDetalle IsNot Nothing) AndAlso (FacOperacionDetalle.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleDeposito, FacOperacionDetalle.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacOperacionDetalle.Fecha IsNot Nothing) AndAlso (Not FacOperacionDetalle.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetalle.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetalle.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleFecha, fecha, fecha2)
            'End If
            'If (FacOperacionDetalle.FechaReg IsNot Nothing) AndAlso (Not FacOperacionDetalle.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetalle.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetalle.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetalleFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionDetalles = query.List(Of FacOperacionDetalle)()

            Return FacOperacionDetalles

        End Function


    End Class
End Namespace