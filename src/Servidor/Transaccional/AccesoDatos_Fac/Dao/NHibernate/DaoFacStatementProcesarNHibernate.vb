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
    Public Class DaoFacStatementProcesarNHibernate

        Inherits DaoBaseNHibernate(Of FacStatementProcesar, Integer)
        Implements IDaoFacStatementProcesar
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacStatementProcesarsFiltro(ByVal FacStatementProcesar As FacStatementProcesar) As System.Collections.Generic.IList(Of FacStatementProcesar) Implements Contrato.IDaoFacStatementProcesar.ObtenerFacStatementProcesarsFiltro
            Dim FacStatementProcesars As IList(Of FacStatementProcesar) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacStatementProcesar)

            If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarId, FacStatementProcesar.Id)
                variosFiltros = True
            End If

            If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Cobro IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarCobro, FacStatementProcesar.Cobro)
                variosFiltros = True
            End If
            'If (FacStatementProcesar.Carta IsNot Nothing) AndAlso (Not FacStatementProcesar.Carta.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarCarta, FacStatementProcesar.Carta.Id)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar.InteresadoImp IsNot Nothing) AndAlso (Not FacStatementProcesar.InteresadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarInteresadoImp, FacStatementProcesar.InteresadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar.AsociadoImp IsNot Nothing) AndAlso (Not FacStatementProcesar.AsociadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarAsociadoImp, FacStatementProcesar.AsociadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar.Asociado IsNot Nothing) AndAlso (Not FacStatementProcesar.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarAsociado, FacStatementProcesar.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar.DetalleEnvio IsNot Nothing) AndAlso (Not FacStatementProcesar.DetalleEnvio.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarDetalleEnvio, FacStatementProcesar.DetalleEnvio.Id)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Inicial <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarInicial, FacStatementProcesar.Inicial)
            '    variosFiltros = True
            'End If
            'If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Terrero.ToString <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarTerrero, FacStatementProcesar.Terrero)
            '    variosFiltros = True
            'End If

            'If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Anulada <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarAnulada, FacStatementProcesar.Anulada)
            '    variosFiltros = True
            'End If

            'If ((FacStatementProcesar.FechaDesde IsNot Nothing) AndAlso (Not FacStatementProcesar.FechaDesde.Equals(DateTime.MinValue)) And (FacStatementProcesar.FechaHasta IsNot Nothing) AndAlso (Not FacStatementProcesar.FechaHasta.Equals(DateTime.MinValue))) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaDesde)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaHasta)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarFechaDesdeHasta, fecha, fecha2)
            '    variosFiltros = True
            'End If
            If (FacStatementProcesar.FechaFactura IsNot Nothing) AndAlso (Not FacStatementProcesar.FechaFactura.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaFactura)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaFactura)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarFechaFactura, fecha, fecha2)
            End If

            If (FacStatementProcesar.FechaCobro IsNot Nothing) AndAlso (Not FacStatementProcesar.FechaCobro.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaCobro)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacStatementProcesar.FechaCobro)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarFechaCobro, fecha, fecha2)
            End If

            'este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            'If (FacStatementProcesar IsNot Nothing) AndAlso (FacStatementProcesar.Status IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim valor As String = ""
            '    If FacStatementProcesar.Status = 1 Then ' 
            '        valor = "('1','2')" ' order by fp.Seniat"
            '    ElseIf FacStatementProcesar.Status = 2 Then
            '        valor = "('2','3')" ' order by fp.Id"
            '    End If
            '    If FacStatementProcesar.Status <> 3 Then
            '        filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacStatementProcesarTerrero, valor)
            '    End If
            '    variosFiltros = True
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacStatementProcesars = query.List(Of FacStatementProcesar)()

            Return FacStatementProcesars

        End Function


    End Class
End Namespace