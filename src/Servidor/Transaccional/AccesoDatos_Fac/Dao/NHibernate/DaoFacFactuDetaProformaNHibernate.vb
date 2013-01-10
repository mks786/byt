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
    Public Class DaoFacFactuDetaProformaNHibernate
        Inherits DaoBaseNHibernate(Of FacFactuDetaProforma, Integer)
        Implements IDaoFacFactuDetaProforma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFactuDetaProformasFiltro(ByVal FacFactuDetaProforma As FacFactuDetaProforma) As System.Collections.Generic.IList(Of FacFactuDetaProforma) Implements Contrato.IDaoFacFactuDetaProforma.ObtenerFacFactuDetaProformasFiltro
            Dim FacFactuDetaProformas As IList(Of FacFactuDetaProforma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFactuDetaProforma)
            ''If (FacFactuDetaProforma IsNot Nothing) AndAlso (FacFactuDetaProforma.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaId, FacFactuDetaProforma.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacFactuDetaProforma.Id IsNot Nothing) AndAlso (Not FacFactuDetaProforma.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaId, FacFactuDetaProforma.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacFactuDetaProforma.Servicio IsNot Nothing) AndAlso (Not FacFactuDetaProforma.Servicio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaServicio, FacFactuDetaProforma.Servicio.Id)
            End If

            If (FacFactuDetaProforma.Factura IsNot Nothing) AndAlso (Not FacFactuDetaProforma.Factura.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaFactura, FacFactuDetaProforma.Factura.Id)
                variosFiltros = True
            End If

            'If (FacFactuDetaProforma IsNot Nothing) AndAlso (FacFactuDetaProforma.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaNCheque, FacFactuDetaProforma.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacFactuDetaProforma IsNot Nothing) AndAlso (FacFactuDetaProforma.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaDeposito, FacFactuDetaProforma.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacFactuDetaProforma.Fecha IsNot Nothing) AndAlso (Not FacFactuDetaProforma.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaProforma.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaProforma.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaFecha, fecha, fecha2)
            'End If
            'If (FacFactuDetaProforma.FechaReg IsNot Nothing) AndAlso (Not FacFactuDetaProforma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaProforma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactuDetaProforma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFactuDetaProformaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFactuDetaProformas = query.List(Of FacFactuDetaProforma)()

            Return FacFactuDetaProformas

        End Function


    End Class
End Namespace