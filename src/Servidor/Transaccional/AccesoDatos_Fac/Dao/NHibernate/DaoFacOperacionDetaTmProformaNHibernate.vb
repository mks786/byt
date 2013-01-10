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
    Public Class DaoFacOperacionDetaTmProformaNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionDetaTmProforma, Integer)
        Implements IDaoFacOperacionDetaTmProforma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionDetaTmProformasFiltro(ByVal FacOperacionDetaTmProforma As FacOperacionDetaTmProforma) As System.Collections.Generic.IList(Of FacOperacionDetaTmProforma) Implements Contrato.IDaoFacOperacionDetaTmProforma.ObtenerFacOperacionDetaTmProformasFiltro
            Dim FacOperacionDetaTmProformas As IList(Of FacOperacionDetaTmProforma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionDetaTmProforma)
            ''If (FacOperacionDetaTmProforma IsNot Nothing) AndAlso (FacOperacionDetaTmProforma.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaId, FacOperacionDetaTmProforma.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacionDetaTmProforma.Id IsNot Nothing) AndAlso (Not FacOperacionDetaTmProforma.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaId, FacOperacionDetaTmProforma.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacOperacionDetaTmProforma.Factura IsNot Nothing) AndAlso (Not FacOperacionDetaTmProforma.Factura.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaFactura, FacOperacionDetaTmProforma.Factura.Id)
            End If

            If (FacOperacionDetaTmProforma.Usuario IsNot Nothing) AndAlso (Not FacOperacionDetaTmProforma.Usuario.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaUsuario, FacOperacionDetaTmProforma.Usuario.Id)
            End If

            'If (FacOperacionDetaTmProforma IsNot Nothing) AndAlso (FacOperacionDetaTmProforma.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaNCheque, FacOperacionDetaTmProforma.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacOperacionDetaTmProforma IsNot Nothing) AndAlso (FacOperacionDetaTmProforma.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaDeposito, FacOperacionDetaTmProforma.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacOperacionDetaTmProforma.Fecha IsNot Nothing) AndAlso (Not FacOperacionDetaTmProforma.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaTmProforma.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaTmProforma.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaFecha, fecha, fecha2)
            'End If
            'If (FacOperacionDetaTmProforma.FechaReg IsNot Nothing) AndAlso (Not FacOperacionDetaTmProforma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaTmProforma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionDetaTmProforma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionDetaTmProformaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacOperacionDetaTmProformas = query.List(Of FacOperacionDetaTmProforma)()

            Return FacOperacionDetaTmProformas

        End Function


    End Class
End Namespace