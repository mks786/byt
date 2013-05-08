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
    Public Class DaoFacFacturaNHibernate
        Inherits DaoBaseNHibernate(Of FacFactura, Integer)
        Implements IDaoFacFactura
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturasFiltro(ByVal FacFactura As FacFactura) As System.Collections.Generic.IList(Of FacFactura) Implements Contrato.IDaoFacFactura.ObtenerFacFacturasFiltro
            Dim FacFacturas As IList(Of FacFactura) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFactura)

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaId, FacFactura.Id)
                variosFiltros = True
            End If
            If (FacFactura.Carta IsNot Nothing) AndAlso (Not FacFactura.Carta.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaCarta, FacFactura.Carta.Id)
                variosFiltros = True
            End If
            If (FacFactura.InteresadoImp IsNot Nothing) AndAlso (Not FacFactura.InteresadoImp.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaInteresadoImp, FacFactura.InteresadoImp.Id)
                variosFiltros = True
            End If
            If (FacFactura.AsociadoImp IsNot Nothing) AndAlso (Not FacFactura.AsociadoImp.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAsociadoImp, FacFactura.AsociadoImp.Id)
                variosFiltros = True
            End If
            If (FacFactura.Asociado IsNot Nothing) AndAlso (Not FacFactura.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAsociado, FacFactura.Asociado.Id)
                variosFiltros = True
            End If
            If (FacFactura.DetalleEnvio IsNot Nothing) AndAlso (FacFactura.DetalleEnvio.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaDetalleEnvio, FacFactura.DetalleEnvio.Id)
                variosFiltros = True
            End If
            If (FacFactura.DetalleEnvio IsNot Nothing) AndAlso (FacFactura.CodGuia <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaGuia, FacFactura.CodGuia)
                variosFiltros = True
            End If
            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Inicial <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaInicial, FacFactura.Inicial)
                variosFiltros = True
            End If
            If (FacFactura IsNot Nothing) AndAlso (FacFactura.NumeroControl IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaNumeroControl, FacFactura.NumeroControl)
                variosFiltros = True
            End If
            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Instruc <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaInstruc, FacFactura.Instruc)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.CodigoDepartamento <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaCodigoDepartamento, FacFactura.CodigoDepartamento)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Anulada <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAnulada, FacFactura.Anulada)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Ourref <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaOurref, FacFactura.Ourref)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Caso <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaCaso, FacFactura.Caso)
                variosFiltros = True
            End If

            If (FacFactura.Proforma IsNot Nothing) AndAlso (FacFactura.Proforma.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaProforma, FacFactura.Proforma.Id)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) AndAlso (FacFactura.Seniat IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaSeniat, FacFactura.Seniat)
                variosFiltros = True
            End If

            If (FacFactura.FechaFactura IsNot Nothing) AndAlso (FacFactura.FechaFactura IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaFactura)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaFactura)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaFechaFactura, fecha, fecha2)
                variosFiltros = True
            End If

            If (FacFactura.FechaFactura IsNot Nothing) AndAlso (FacFactura.FechaSeniat IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaSeniat)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaSeniat)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaFechaSeniat, fecha, fecha2)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) And (FacFactura.Status = 1) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaDesde)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaHasta)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaFechaSeniat, fecha, fecha2)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) And (FacFactura.Status = 2) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaDesde)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaHasta)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaFechaFactura, fecha, fecha2)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) And (FacFactura.Status = 3) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim desde As Integer = FacFactura.P_mip
                Dim hasta As String = FacFactura.Bst
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaIdDesdeHasta, desde, hasta)
                variosFiltros = True
            End If

            If (FacFactura IsNot Nothing) And (FacFactura.Status = 4) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim desde As Integer = FacFactura.P_mip
                Dim hasta As String = FacFactura.Bst
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaSeniatDesdeHasta, desde, hasta)
                variosFiltros = True
            End If

            'If (FacFactura.FechaReg IsNot Nothing) AndAlso (Not FacFactura.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFactura.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaFechaReg, fecha, fecha2)
            'End If

            'este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            'If (FacFactura IsNot Nothing) AndAlso (FacFactura.Status IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    'Dim valor As String = ""
            '    'If FacFactura.Status = 1 Then ' para las pendientes y rechazadas
            '    '    valor = "0','3"
            '    'ElseIf FacFactura.Status = 2 Then ' para las facturadas y Autorizado
            '    '    valor = "1','2"
            '    'ElseIf FacFactura.Status = 4 Then ' para las proformas Autorizadas
            '    '    valor = "1"
            '    'End If
            '    'If FacFactura.Status <> 3 Then
            '    '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAuto, valor)
            '    'Else
            '    '    valor = "2"
            '    '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaAuto2, valor) ' el tres es para consulta por departamento iauto<>2
            '    'End If
            '    'variosFiltros = True
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera & "  order by fp.Id desc")
            Else
                cabecera = cabecera & " Where "
                If FacFactura.Status Is Nothing Then
                    cabecera = cabecera & filtro & "  order by fp.Id desc"
                Else
                    cabecera = cabecera & filtro
                End If
                query = Session.CreateQuery(cabecera)
                End If
                FacFacturas = query.List(Of FacFactura)()

                Return FacFacturas

        End Function


    End Class
End Namespace