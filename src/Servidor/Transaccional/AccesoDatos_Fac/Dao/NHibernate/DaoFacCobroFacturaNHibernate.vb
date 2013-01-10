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
    Public Class DaoFacCobroFacturaNHibernate
        Inherits DaoBaseNHibernate(Of FacCobroFactura, Integer)
        Implements IDaoFacCobroFactura
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacCobroFacturasFiltro(ByVal FacCobroFactura As FacCobroFactura) As System.Collections.Generic.IList(Of FacCobroFactura) Implements Contrato.IDaoFacCobroFactura.ObtenerFacCobroFacturasFiltro
            Dim FacCobroFacturas As IList(Of FacCobroFactura) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacCobroFactura)
            If (FacCobroFactura IsNot Nothing) AndAlso (FacCobroFactura.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaId, FacCobroFactura.Id.Id)
                variosFiltros = True
            End If
            If (FacCobroFactura IsNot Nothing) AndAlso (FacCobroFactura.Factura IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaId, FacCobroFactura.Factura)
                variosFiltros = True
            End If
            'If (FacCobroFactura.Banco IsNot Nothing) AndAlso (Not FacCobroFactura.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaBanco, FacCobroFactura.Banco.Id)
            'End If
            'If (FacCobroFactura IsNot Nothing) AndAlso (FacCobroFactura.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaNCheque, FacCobroFactura.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacCobroFactura IsNot Nothing) AndAlso (FacCobroFactura.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaDeposito, FacCobroFactura.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacCobroFactura.Fecha IsNot Nothing) AndAlso (Not FacCobroFactura.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacCobroFactura.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacCobroFactura.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaFecha, fecha, fecha2)
            'End If
            'If (FacCobroFactura.FechaReg IsNot Nothing) AndAlso (Not FacCobroFactura.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacCobroFactura.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacCobroFactura.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacCobroFacturaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacCobroFacturas = query.List(Of FacCobroFactura)()

            Return FacCobroFacturas

        End Function


    End Class
End Namespace