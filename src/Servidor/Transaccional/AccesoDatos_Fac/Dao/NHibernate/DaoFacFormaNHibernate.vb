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
    Public Class DaoFacFormaNHibernate
        Inherits DaoBaseNHibernate(Of FacForma, Integer)
        Implements IDaoFacForma
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacFormasFiltro(ByVal FacForma As FacForma) As System.Collections.Generic.IList(Of FacForma) Implements Contrato.IDaoFacForma.ObtenerFacFormasFiltro
            Dim FacFormas As IList(Of FacForma) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacForma)
            'If (FacForma IsNot Nothing) AndAlso (FacForma.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaId, FacForma.Id)
            '    variosFiltros = True
            'End If
            'If (FacForma.Id IsNot Nothing) AndAlso (Not FacForma.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaId, FacForma.Id.Id)
            '    variosFiltros = True
            'End If
            If (FacForma.Credito IsNot Nothing) AndAlso (Not FacForma.Credito.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaCredito, FacForma.Credito.Id)
            End If

            If (FacForma.Cobro IsNot Nothing) AndAlso (Not FacForma.Cobro.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaCobro, FacForma.Cobro.Id)
            End If
            'If (FacForma IsNot Nothing) AndAlso (FacForma.NCheque <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaNCheque, FacForma.NCheque)
            '    variosFiltros = True
            'End If
            'If (FacForma IsNot Nothing) AndAlso (FacForma.Deposito <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaDeposito, FacForma.Deposito)
            '    variosFiltros = True
            'End If

            'If (FacForma.Fecha IsNot Nothing) AndAlso (Not FacForma.Fecha.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacForma.Fecha)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacForma.Fecha)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaFecha, fecha, fecha2)
            'End If
            'If (FacForma.FechaReg IsNot Nothing) AndAlso (Not FacForma.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacForma.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacForma.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacFormaFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacFormas = query.List(Of FacForma)()

            Return FacFormas

        End Function


    End Class
End Namespace