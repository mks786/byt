Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Namespace Dao.NHibernate
    Public Class DaoFacImpuestoNHibernate
        Inherits DaoBaseNHibernate(Of FacImpuesto, DateTime)
        Implements IDaoFacImpuesto
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacImpuestosFiltro(ByVal FacImpuesto As FacImpuesto) As System.Collections.Generic.IList(Of FacImpuesto) Implements Contrato.IDaoFacImpuesto.ObtenerFacImpuestosFiltro
            Dim FacImpuestos As IList(Of FacImpuesto) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacImpuesto)
            'If (FacImpuesto IsNot Nothing) AndAlso (FacImpuesto.Id <> 0) Then
            '    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacImpuestoId, FacImpuesto.Id)
            '    variosFiltros = True
            'End If
            'If (FacImpuesto.Id IsNot Nothing) AndAlso (Not FacImpuesto.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacImpuestoId, FacImpuesto.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacImpuesto.Banco IsNot Nothing) AndAlso (Not FacImpuesto.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacImpuestoBanco, FacImpuesto.Banco.Id)
            'End If


            If (FacImpuesto IsNot Nothing) AndAlso (Not FacImpuesto.Id.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacImpuesto.Id)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacImpuesto.Id.AddDays(1))
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacImpuestoId, fecha, fecha2)
            Else
                cabecera = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacImpuestoMax)
            End If
            'If (FacImpuesto.FechaReg IsNot Nothing) AndAlso (Not FacImpuesto.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacImpuesto.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacImpuesto.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacImpuestoFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacImpuestos = query.List(Of FacImpuesto)()

            Return FacImpuestos

        End Function


    End Class

End Namespace