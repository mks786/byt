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
    Public Class DaoFacOperacionPaisNHibernate
        Inherits DaoBaseNHibernate(Of FacOperacionPais, Integer)
        Implements IDaoFacOperacionPais
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacOperacionPaisesFiltro(ByVal FacOperacionPais As FacOperacionPais) As System.Collections.Generic.IList(Of FacOperacionPais) Implements Contrato.IDaoFacOperacionPais.ObtenerFacOperacionPaisesFiltro
            Dim FacOperacionPaises As IList(Of FacOperacionPais) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacOperacionPais)

            If (FacOperacionPais.Id IsNot Nothing) AndAlso (FacOperacionPais.Id <> "") Then
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisId, FacOperacionPais.Id)
                variosFiltros = True
            End If

            If (FacOperacionPais.Asociado IsNot Nothing) AndAlso (Not FacOperacionPais.Asociado.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisAsociado, FacOperacionPais.Asociado.Id)
                variosFiltros = True
            End If

            If (FacOperacionPais.Pais IsNot Nothing) AndAlso (Not FacOperacionPais.Pais.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisAsociado, FacOperacionPais.Pais.Id)
                variosFiltros = True
            End If

            If (FacOperacionPais.Idioma IsNot Nothing) AndAlso (FacOperacionPais.Idioma.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisIdioma, FacOperacionPais.Idioma.Id)
                variosFiltros = True
            End If
            If (FacOperacionPais.Moneda IsNot Nothing) AndAlso (FacOperacionPais.Moneda.Id <> "") Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisMoneda, FacOperacionPais.Moneda.Id)
                variosFiltros = True
            End If

            If (Not FacOperacionPais.Saldo.Equals("")) Then
                If (FacOperacionPais.Saldo = -1) Then
                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisSaldomayor)
                    variosFiltros = True
                Else

                End If
            End If

            If (FacOperacionPais IsNot Nothing) AndAlso (FacOperacionPais.CodigoOperacion IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisCodigoOperacion, FacOperacionPais.CodigoOperacion)
                variosFiltros = True
            End If

            ''If (FacOperacionPais IsNot Nothing) AndAlso (FacOperacionPais.Id <> 0) Then
            ''    filtro = String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisId, FacOperacionPais.Id)
            ''    variosFiltros = True
            ''End If
            'If (FacOperacionPais.Id IsNot Nothing) AndAlso (Not FacOperacionPais.Id.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisId, FacOperacionPais.Id.Id)
            '    variosFiltros = True
            'End If
            'If (FacOperacionPais.BancoRec IsNot Nothing) AndAlso (Not FacOperacionPais.BancoRec.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisBancoRec, FacOperacionPais.BancoRec.Id)
            'End If
            'If (FacOperacionPais.BancoPag IsNot Nothing) AndAlso (Not FacOperacionPais.BancoPag.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisBancoPag, FacOperacionPais.BancoPag.Id)
            'End If

            'If (FacOperacionPais.PagoRec.Equals(" "c)) Then
            'Else

            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisPagoRec, FacOperacionPais.PagoRec)
            '    variosFiltros = True
            'End If
            'If (FacOperacionPais IsNot Nothing) AndAlso (FacOperacionPais.IPagado <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisIPagado, FacOperacionPais.IPagado)
            '    variosFiltros = True
            'End If

            'If (FacOperacionPais.FechaBanco IsNot Nothing) AndAlso (Not FacOperacionPais.FechaBanco.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionPais.FechaBanco)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionPais.FechaBanco)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisFechaBanco, fecha, fecha2)
            'End If
            'If (FacOperacionPais.FechaReg IsNot Nothing) AndAlso (Not FacOperacionPais.FechaReg.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacOperacionPais.FechaReg)
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", FacOperacionPais.FechaReg)
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacOperacionPaisFechaReg, fecha, fecha2)
            'End If

            Dim query As IQuery
            If (FacOperacionPais IsNot Nothing) And FacOperacionPais.Seleccion = False Then
                If (filtro = "") Then
                    query = Session.CreateQuery(cabecera)
                Else
                    cabecera = cabecera & " Where "
                    cabecera = cabecera & filtro
                    query = Session.CreateQuery(cabecera)
                End If
            Else
                query = Session.CreateQuery(FacOperacionPais.ValorQuery)
            End If
            FacOperacionPaises = query.List(Of FacOperacionPais)()

            Return FacOperacionPaises

        End Function


    End Class
End Namespace