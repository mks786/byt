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
    Public Class DaoFacFacturaTotalZNHibernate
        Inherits DaoBaseNHibernate(Of FacFacturaTotalZ, Integer)
        Implements IDaoFacFacturaTotalZ
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFacturaTotalZsFiltro(ByVal FacFacturaTotalZ As FacFacturaTotalZ) As System.Collections.Generic.IList(Of FacFacturaTotalZ) Implements Contrato.IDaoFacFacturaTotalZ.ObtenerFacFacturaTotalZsFiltro
            Dim FacFacturaTotalZs As IList(Of FacFacturaTotalZ) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacFacturaTotalZ)

            If (FacFacturaTotalZ IsNot Nothing) AndAlso (FacFacturaTotalZ.Id IsNot Nothing) Then
                filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZId, FacFacturaTotalZ.Id)
                variosFiltros = True
            End If
            'If (FacFacturaTotalZ.Carta IsNot Nothing) AndAlso (Not FacFacturaTotalZ.Carta.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZCarta, FacFacturaTotalZ.Carta.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ.InteresadoImp IsNot Nothing) AndAlso (Not FacFacturaTotalZ.InteresadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZInteresadoImp, FacFacturaTotalZ.InteresadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ.AsociadoImp IsNot Nothing) AndAlso (Not FacFacturaTotalZ.AsociadoImp.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZAsociadoImp, FacFacturaTotalZ.AsociadoImp.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ.Asociado IsNot Nothing) AndAlso (Not FacFacturaTotalZ.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZAsociado, FacFacturaTotalZ.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ.DetalleEnvio IsNot Nothing) AndAlso (Not FacFacturaTotalZ.DetalleEnvio.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZDetalleEnvio, FacFacturaTotalZ.DetalleEnvio.Id)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ IsNot Nothing) AndAlso (FacFacturaTotalZ.Inicial <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZInicial, FacFacturaTotalZ.Inicial)
            '    variosFiltros = True
            'End If
            'If (FacFacturaTotalZ IsNot Nothing) AndAlso (FacFacturaTotalZ.Terrero.ToString <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZTerrero, FacFacturaTotalZ.Terrero)
            '    variosFiltros = True
            'End If

            'If (FacFacturaTotalZ IsNot Nothing) AndAlso (FacFacturaTotalZ.Anulada <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZAnulada, FacFacturaTotalZ.Anulada)
            '    variosFiltros = True
            'End If

            If ((FacFacturaTotalZ.FechaDesde IsNot Nothing) AndAlso (Not FacFacturaTotalZ.FechaDesde.Equals(DateTime.MinValue)) And (FacFacturaTotalZ.FechaHasta IsNot Nothing) AndAlso (Not FacFacturaTotalZ.FechaHasta.Equals(DateTime.MinValue))) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotalZ.FechaDesde)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotalZ.FechaHasta)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZFechaDesdeHasta, fecha, fecha2)
                variosFiltros = True
            End If
            ''If (FacFacturaTotalZ.FechaReg IsNot Nothing) AndAlso (Not FacFacturaTotalZ.FechaReg.Equals(DateTime.MinValue)) Then
            ''    If variosFiltros Then
            ''        filtro += " and "
            ''    End If
            ''    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotalZ.FechaReg)
            ''    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacFacturaTotalZ.FechaReg)
            ''    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZFechaReg, fecha, fecha2)
            ''End If

            'este campo de estatus no esta en la BD se creo con el fin de poder utilizarlo cuando sea la pantalla consulta pendiente dado que se busca por Auto in('0','3')
            If (FacFacturaTotalZ IsNot Nothing) AndAlso (FacFacturaTotalZ.Status IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim valor As String = ""
                If FacFacturaTotalZ.Status = 1 Then ' 
                    valor = "('1','2')" ' order by fp.Seniat"
                ElseIf FacFacturaTotalZ.Status = 2 Then
                    valor = "('2','3')" ' order by fp.Id"
                End If
                If FacFacturaTotalZ.Status <> 3 Then
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFacturaTotalZTerrero, valor)
                End If
                variosFiltros = True
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFacturaTotalZs = query.List(Of FacFacturaTotalZ)()

            Return FacFacturaTotalZs

        End Function


    End Class
End Namespace