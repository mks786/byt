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
    Public Class DaoFacGestionNHibernate
        Inherits DaoBaseNHibernate(Of FacGestion, Integer)
        Implements IDaoFacGestion
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacGestionesFiltro(ByVal FacGestion As FacGestion) As System.Collections.Generic.IList(Of FacGestion) Implements Contrato.IDaoFacGestion.ObtenerFacGestionesFiltro
            Dim FacGestiones As IList(Of FacGestion) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacGestion)


            If (FacGestion IsNot Nothing) AndAlso (FacGestion.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionId, FacGestion.Id)
                variosFiltros = True
            End If

            If (FacGestion.Asociado IsNot Nothing) AndAlso (Not FacGestion.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionAsociado, FacGestion.Asociado.Id)
            End If

            'If (FacGestion IsNot Nothing) AndAlso (FacGestion.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionEstadoCuenta, FacGestion.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (FacGestion.Asociado IsNot Nothing) AndAlso (Not FacGestion.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionAsociado, FacGestion.Asociado.Id)
            '    variosFiltros = True
            'End If
            'If (FacGestion.Banco IsNot Nothing) AndAlso (Not FacGestion.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionBanco, FacGestion.Banco.Id)
            '    variosFiltros = True
            'End If
            If (FacGestion IsNot Nothing) AndAlso (FacGestion.Medio <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionMedio, FacGestion.Medio)
                variosFiltros = True
            End If

            If (FacGestion IsNot Nothing) AndAlso (FacGestion.ConceptoGestion <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionConceptoGestion, FacGestion.ConceptoGestion)
                variosFiltros = True
            End If

            If (FacGestion.FechaGestion IsNot Nothing) AndAlso (Not FacGestion.FechaGestion.Equals(DateTime.MinValue)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacGestion.FechaGestion)
                Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacGestion.FechaGestion)
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionFechaGestion, fecha, fecha2)
                variosFiltros = True
            End If
            'If (FacGestion.Moneda IsNot Nothing) AndAlso (FacGestion.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionMoneda, FacGestion.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (FacGestion.FechaCobro IsNot Nothing) AndAlso (Not FacGestion.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacGestion.FechaCobro)
            '    Dim fechaday As DateTime = FacGestion.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacGestionFechaCobro, fecha, fecha2)
            '    variosFiltros = True
            'End If

            If (FacGestion IsNot Nothing) AndAlso (FacGestion.Status IsNot Nothing) Then
                If FacGestion.Status = 1 Then
                    cabecera = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacGestiontoMax) & " " & filtro & " order by fg.Id desc"
                    filtro = ""
                End If
            End If

            Dim query As IQuery
            If (filtro = "") Then
                query = Session.CreateQuery(cabecera)
            Else
                cabecera = cabecera & " Where "
                cabecera = cabecera & filtro
                query = Session.CreateQuery(cabecera)
            End If
            FacGestiones = query.List(Of FacGestion)()

            Return FacGestiones

        End Function


    End Class
End Namespace