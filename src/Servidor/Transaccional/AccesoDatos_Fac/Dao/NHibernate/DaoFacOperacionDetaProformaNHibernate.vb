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
    Public Class DaoFacOperacionDetaProformaNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionDetaProforma, Integer)
        Implements IDaoFacOperacionDetaProforma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionDetaProformasFiltro(ByVal FacOperacionDetaProforma As FacOperacionDetaProforma) As System.Collections.Generic.IList(Of FacOperacionDetaProforma) Implements Contrato.IDaoFacOperacionDetaProforma.ObtenerFacOperacionDetaProformasFiltro
            Dim FacOperacionDetaProformas As IList(Of FacOperacionDetaProforma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionDetaProforma)
            ''If (FacOperacionDetaProforma IsNot Nothing) AndAlso (FacOperacionDetaProforma.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaId, FacOperacionDetaProforma.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacionDetaProforma.Id IsNot Nothing) AndAlso (Not FacOperacionDetaProforma.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaId, FacOperacionDetaProforma.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacOperacionDetaProforma.Factura IsNot Nothing) AndAlso (Not FacOperacionDetaProforma.Factura.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaFactura, FacOperacionDetaProforma.Factura.Id)
            End If

            If (FacOperacionDetaProforma IsNot Nothing) AndAlso (FacOperacionDetaProforma.Detalle.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaDetalle, FacOperacionDetaProforma.Detalle)
                variosFiltros = True
            End If
            'If (FacOperacionDetaProforma IsNot Nothing) AndAlso (FacOperacionDetaProforma.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaDeposito, FacOperacionDetaProforma.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacOperacionDetaProforma.Fecha IsNot Nothing) AndAlso (Not FacOperacionDetaProforma.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaProforma.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaProforma.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaFecha, fecha, fecha2)
            'End If
            'If (FacOperacionDetaProforma.FechaReg IsNot Nothing) AndAlso (Not FacOperacionDetaProforma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaProforma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaProforma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaProformaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionDetaProformas = query.List(Of FacOperacionDetaProforma)()

            Return FacOperacionDetaProformas

        End Function


    End Class
End Namespace