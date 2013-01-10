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
    Public Class DaoFacFacturaTotalNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaTotal, Integer)
        Implements IDaoFacFacturaTotal
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaTotalsFiltro(ByVal FacFacturaTotal As FacFacturaTotal) As System.Collections.Generic.IList(Of FacFacturaTotal) Implements Contrato.IDaoFacFacturaTotal.ObtenerFacFacturaTotalsFiltro
            Dim FacFacturaTotals As IList(Of FacFacturaTotal) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            'Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaTotal)

            'If (FacFacturaTotal IsNot Nothing) AndAlso (FacFacturaTotal.Id IsNot Nothing) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalId, FacFacturaTotal.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal.Carta IsNot Nothing) AndAlso (Not FacFacturaTotal.Carta.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalCarta, FacFacturaTotal.Carta.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal.InteresadoImp IsNot Nothing) AndAlso (Not FacFacturaTotal.InteresadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalInteresadoImp, FacFacturaTotal.InteresadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal.AsociadoImp IsNot Nothing) AndAlso (Not FacFacturaTotal.AsociadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalAsociadoImp, FacFacturaTotal.AsociadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal.Asociado IsNot Nothing) AndAlso (Not FacFacturaTotal.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalAsociado, FacFacturaTotal.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal.DetalleEnvio IsNot Nothing) AndAlso (Not FacFacturaTotal.DetalleEnvio.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalDetalleEnvio, FacFacturaTotal.DetalleEnvio.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal IsNot Nothing) AndAlso (FacFacturaTotal.Inicial <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalInicial, FacFacturaTotal.Inicial)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotal IsNot Nothing) AndAlso (FacFacturaTotal.CodigoDepartamento <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalCodigoDepartamento, FacFacturaTotal.CodigoDepartamento)
            '    variosFiltros = True
            'End If

            'If (FacFacturaTotal IsNot Nothing) AndAlso (FacFacturaTotal.Anulada <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalAnulada, FacFacturaTotal.Anulada)
            '    variosFiltros = True
            'End If

            'If (FacFacturaTotal.FechaFactura IsNot Nothing) AndAlso (Not FacFacturaTotal.FechaFactura.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotal.FechaFactura)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotal.FechaFactura)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalFechaFactura, fecha, fecha2)
            '    variosFiltros = True
            'End If
            ''If (FacFacturaTotal.FechaReg IsNot Nothing) AndAlso (Not FacFacturaTotal.FechaReg.Equals(DateTime.MinValue)) Then
            ''    If variosFiltros Then
            ''        filtro += " and "
            ''    End If
            ''    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotal.FechaReg)
            ''    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotal.FechaReg)
            ''    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalFechaReg, fecha, fecha2)
            ''End If

            ''este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            'If (FacFacturaTotal IsNot Nothing) AndAlso (FacFacturaTotal.Status IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    'Dim valor As String = ""
            '    'If FacFacturaTotal.Status = 1 Then ' para las pendientes y rechazadas
            '    '    valor = "0','3"
            '    'ElseIf FacFacturaTotal.Status = 2 Then ' para las facturadas y Autorizado
            '    '    valor = "1','2"
            '    'ElseIf FacFacturaTotal.Status = 4 Then ' para las proformas Autorizadas
            '    '    valor = "1"
            '    'End If
            '    'If FacFacturaTotal.Status <> 3 Then
            '    '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalAuto, valor)
            '    'Else
            '    '    valor = "2"
            '    '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalAuto2, valor) ' el tres es para consulta por departamento iauto<>2
            '    'End If
            '    'variosFiltros = True
            'End If

            'Dim query As IQuery
            'If (filtro = "") Then
            '    query = Session.CreateQuery(cabecera)
            'Else
            '    cabecera = cabecera & " Where "
            '    cabecera = cabecera & filtro
            '    query = Session.CreateQuery(cabecera)
            'End If
            'FacFacturaTotals = query.List(Of FacFacturaTotal)()

            Return FacFacturaTotals

        End Function


    End Class
End Namespace