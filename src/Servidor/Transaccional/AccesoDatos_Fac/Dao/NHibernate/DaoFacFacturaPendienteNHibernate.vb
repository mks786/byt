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
    Public Class DaoFacFacturaPendienteNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaPendiente, Integer)
        Implements IDaoFacFacturaPendiente
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaPendientesFiltro(ByVal FacFacturaPendiente As FacFacturaPendiente) As System.Collections.Generic.IList(Of FacFacturaPendiente) Implements Contrato.IDaoFacFacturaPendiente.ObtenerFacFacturaPendientesFiltro
            Dim FacFacturaPendientes As IList(Of FacFacturaPendiente) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaPendiente)

            If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteId, FacFacturaPendiente.Id)
                variosFiltros = True
            End If
            'If (FacFacturaPendiente.Carta IsNot Nothing) AndAlso (Not FacFacturaPendiente.Carta.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteCarta, FacFacturaPendiente.Carta.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente.InteresadoImp IsNot Nothing) AndAlso (Not FacFacturaPendiente.InteresadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteInteresadoImp, FacFacturaPendiente.InteresadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente.AsociadoImp IsNot Nothing) AndAlso (Not FacFacturaPendiente.AsociadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteAsociadoImp, FacFacturaPendiente.AsociadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente.Asociado IsNot Nothing) AndAlso (Not FacFacturaPendiente.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteAsociado, FacFacturaPendiente.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente.DetalleEnvio IsNot Nothing) AndAlso (Not FacFacturaPendiente.DetalleEnvio.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteDetalleEnvio, FacFacturaPendiente.DetalleEnvio.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.Inicial <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteInicial, FacFacturaPendiente.Inicial)
            '    variosFiltros = True
            'End If
            'If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.Terrero.ToString <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteTerrero, FacFacturaPendiente.Terrero)
            '    variosFiltros = True
            'End If

            'If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.Anulada <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteAnulada, FacFacturaPendiente.Anulada)
            '    variosFiltros = True
            'End If

            If ((FacFacturaPendiente.FechaDesde IsNot Nothing) AndAlso (Not FacFacturaPendiente.FechaDesde.Equals(DateTime.MinValue)) And (FacFacturaPendiente.FechaHasta IsNot Nothing) AndAlso (Not FacFacturaPendiente.FechaHasta.Equals(DateTime.MinValue))) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaPendiente.FechaDesde)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaPendiente.FechaHasta)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteFechaDesdeHasta, fecha, fecha2)
                variosFiltros = True
            End If
            ''If (FacFacturaPendiente.FechaReg IsNot Nothing) AndAlso (Not FacFacturaPendiente.FechaReg.Equals(DateTime.MinValue)) Then
            ''    If variosFiltros Then
            ''        filtro += " and "
            ''    End If
            ''    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaPendiente.FechaReg)
            ''    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaPendiente.FechaReg)
            ''    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteFechaReg, fecha, fecha2)
            ''End If

            'este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.Status IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim valor As String = ""
                If FacFacturaPendiente.Status = 1 Then ' 
                    valor = "('1','2')" ' order by fp.Seniat"
                ElseIf FacFacturaPendiente.Status = 2 Then
                    valor = "('2','3')" ' order by fp.Id"
                End If
                If FacFacturaPendiente.Status <> 3 Then
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaPendienteTerrero, valor)
                End If
                variosFiltros = True
            End If

            If (FacFacturaPendiente IsNot Nothing) AndAlso (FacFacturaPendiente.ValorQuery <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += FacFacturaPendiente.ValorQuery
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where"
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFacturaPendientes = query.List(Of FacFacturaPendiente)()

            Return FacFacturaPendientes

        End Function


    End Class
End Namespace