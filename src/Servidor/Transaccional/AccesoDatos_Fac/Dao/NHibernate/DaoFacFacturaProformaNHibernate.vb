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
    Public Class DaoFacFacturaProformaNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaProforma, Integer)
        Implements IDaoFacFacturaProforma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaProformasFiltro(ByVal FacFacturaProforma As FacFacturaProforma) As System.Collections.Generic.IList(Of FacFacturaProforma) Implements Contrato.IDaoFacFacturaProforma.ObtenerFacFacturaProformasFiltro
            Dim FacFacturaProformas As IList(Of FacFacturaProforma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaProforma)

            If (FacFacturaProforma IsNot Nothing) AndAlso (FacFacturaProforma.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaId, FacFacturaProforma.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma.Carta IsNot Nothing) AndAlso (Not FacFacturaProforma.Carta.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaCarta, FacFacturaProforma.Carta.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma.InteresadoImp IsNot Nothing) AndAlso (Not FacFacturaProforma.InteresadoImp.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaInteresadoImp, FacFacturaProforma.InteresadoImp.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma.AsociadoImp IsNot Nothing) AndAlso (Not FacFacturaProforma.AsociadoImp.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaAsociadoImp, FacFacturaProforma.AsociadoImp.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma.Asociado IsNot Nothing) AndAlso (Not FacFacturaProforma.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaAsociado, FacFacturaProforma.Asociado.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma.DetalleEnvio IsNot Nothing) AndAlso (Not FacFacturaProforma.DetalleEnvio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaDetalleEnvio, FacFacturaProforma.DetalleEnvio.Id)
                variosFiltros = True
            End If
            If (FacFacturaProforma IsNot Nothing) AndAlso (FacFacturaProforma.Inicial <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaInicial, FacFacturaProforma.Inicial)
                variosFiltros = True
            End If
            If (FacFacturaProforma IsNot Nothing) AndAlso (FacFacturaProforma.CodigoDepartamento <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaCodigoDepartamento, FacFacturaProforma.CodigoDepartamento)
                variosFiltros = True
            End If

            If (FacFacturaProforma IsNot Nothing) Then
                If (FacFacturaProforma.Local.Equals(" "c)) Then
                Else
                    Dim valor As String = FacFacturaProforma.Local.ToString
                    If valor = "I" Or valor = "N" Then
                        If variosFiltros Then
                            filtro += " and "
                        End If
                        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaLocal, FacFacturaProforma.Local)
                        variosFiltros = True
                    End If
                    End If
            End If
            'If (FacFacturaProforma IsNot Nothing) AndAlso (FacFacturaProforma.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaDeposito, FacFacturaProforma.Deposito)
            '    variosFiltros = True
            'End If

            If (FacFacturaProforma.FechaFactura IsNot Nothing) AndAlso (Not FacFacturaProforma.FechaFactura.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaFactura)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaFactura)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaFechaFactura, fecha, fecha2)
                variosFiltros = True
            End If

            If (FacFacturaProforma.FechaDesde IsNot Nothing) And (FacFacturaProforma.FechaHasta IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaDesde)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaHasta)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaFechaFactura, fecha, fecha2)
                variosFiltros = True
            End If
            'If (FacFacturaProforma.FechaReg IsNot Nothing) AndAlso (Not FacFacturaProforma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaProforma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaFechaReg, fecha, fecha2)
            'End If

            'este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            If (FacFacturaProforma IsNot Nothing) AndAlso (FacFacturaProforma.Status IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim valor As String = ""
                If FacFacturaProforma.Status = 1 Then ' para las pendientes y rechazadas
                    valor = "0','3"
                ElseIf FacFacturaProforma.Status = 2 Then ' para las facturadas y Autorizado
                    valor = "1','2"
                ElseIf FacFacturaProforma.Status = 4 Then ' para las proformas Autorizadas
                    valor = "1"
                ElseIf FacFacturaProforma.Status = 5 Then ' para la proforma dependiendo de la busqueda
                    valor = FacFacturaProforma.Auto
                End If
                If FacFacturaProforma.Status <> 3 Then
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaAuto, valor)
                Else
                    valor = "2"
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProformaAuto2, valor) ' el tres es para consulta por departamento iauto<>2
                End If
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & " order by fp.Id desc")
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro & " order by fp.Id desc"
                query = Session.CreateQuery(cabecera)
            End If
            FacFacturaProformas = query.List(Of FacFacturaProforma)()

            Return FacFacturaProformas

        End Function


    End Class
End Namespace