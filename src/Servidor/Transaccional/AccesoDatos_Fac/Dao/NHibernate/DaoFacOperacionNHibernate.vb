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
    Public Class DaoFacOperacionNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacion, Integer)
        Implements IDaoFacOperacion
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionesFiltro(ByVal FacOperacion As FacOperacion) As System.Collections.Generic.IList(Of FacOperacion) Implements Contrato.IDaoFacOperacion.ObtenerFacOperacionesFiltro
            Dim FacOperaciones As IList(Of FacOperacion) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacion)

            If (FacOperacion.Id IsNot Nothing) AndAlso (FacOperacion.Id <> "") Then
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionId, FacOperacion.Id)
                variosFiltros = True
            End If

            If (FacOperacion.Asociado IsNot Nothing) AndAlso (Not FacOperacion.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionAsociado, FacOperacion.Asociado.Id)
                variosFiltros = True
            End If
            If (FacOperacion.Idioma IsNot Nothing) AndAlso (FacOperacion.Idioma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionIdioma, FacOperacion.Idioma.Id)
                variosFiltros = True
            End If
            If (FacOperacion.Moneda IsNot Nothing) AndAlso (FacOperacion.Moneda.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionMoneda, FacOperacion.Moneda.Id)
                variosFiltros = True
            End If

            If (Not FacOperacion.Saldo.Equals("")) Then
                If (FacOperacion.Saldo = -1) Then
                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionSaldomayor)
                    variosFiltros = True
                Else

                End If                
            End If


            If (FacOperacion IsNot Nothing) AndAlso (FacOperacion.CodigoOperacion IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionCodigoOperacion, FacOperacion.CodigoOperacion)
                variosFiltros = True
            End If

            ''If (FacOperacion IsNot Nothing) AndAlso (FacOperacion.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionId, FacOperacion.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacion.Id IsNot Nothing) AndAlso (Not FacOperacion.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionId, FacOperacion.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacOperacion.BancoRec IsNot Nothing) AndAlso (Not FacOperacion.BancoRec.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionBancoRec, FacOperacion.BancoRec.Id)
            'End If
            'If (FacOperacion.BancoPag IsNot Nothing) AndAlso (Not FacOperacion.BancoPag.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionBancoPag, FacOperacion.BancoPag.Id)
            'End If

            'If (FacOperacion.PagoRec.Equals(" "c)) Then
            'Else

            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPagoRec, FacOperacion.PagoRec)
            '    variosFiltros = True
            'End If
            'If (FacOperacion IsNot Nothing) AndAlso (FacOperacion.IPagado <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionIPagado, FacOperacion.IPagado)
            '    variosFiltros = True
            'End If

            'If (FacOperacion.FechaBanco IsNot Nothing) AndAlso (Not FacOperacion.FechaBanco.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacion.FechaBanco)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacion.FechaBanco)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionFechaBanco, fecha, fecha2)
            'End If
            'If (FacOperacion.FechaReg IsNot Nothing) AndAlso (Not FacOperacion.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacion.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacion.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (FacOperacion IsNot Nothing) And FacOperacion.Seleccion = False Then
                If (filtro = "") Then
                    query = Session.CreateQuery(cabecera)
                Else
                    cabecera = cabecera & " Where "
                    cabecera = cabecera & filtro
                    query = Session.CreateQuery(cabecera)
                End If
            Else
                query = Session.CreateQuery(FacOperacion.ValorQuery)
            End If
            FacOperaciones = query.List(Of FacOperacion)()

            Return FacOperaciones

        End Function


    End Class
End Namespace