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
    Public Class DaoFacInternacionalNHibernate
        Inherits DaoBaseNHibernate(Of FacInternacional, Integer)
        Implements IDaoFacInternacional
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerFacInternacionalesFiltro(ByVal FacInternacional As FacInternacional) As System.Collections.Generic.IList(Of FacInternacional) Implements Contrato.IDaoFacInternacional.ObtenerFacInternacionalesFiltro
            Dim FacInternacionales As IList(Of FacInternacional) = Nothing
            Dim variosFiltros As Boolean = False
            Dim filtro As String = ""
            Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacInternacional)


            'If (FacInternacional IsNot Nothing) AndAlso (Not FacInternacional.Id.Equals("")) Then
            If (FacInternacional IsNot Nothing) AndAlso (FacInternacional.Id IsNot Nothing) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalId, FacInternacional.Id)
                variosFiltros = True
            End If
            'If (FacInternacional.Banco IsNot Nothing) AndAlso (Not FacInternacional.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalBanco, FacInternacional.Banco.Id)
            'End If
            'If (FacInternacional IsNot Nothing) AndAlso (FacInternacional.EstadoCuenta IsNot Nothing) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalEstadoCuenta, FacInternacional.EstadoCuenta)
            '    variosFiltros = True
            'End If
            'If (FacInternacional.Asociado IsNot Nothing) AndAlso (Not FacInternacional.Asociado.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAsociado, FacInternacional.Asociado.Id)
            '    variosFiltros = True
            'End If

            If (FacInternacional.Asociado_o IsNot Nothing) AndAlso (Not FacInternacional.Asociado_o.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalAsociado_o, FacInternacional.Asociado_o.Id)
                variosFiltros = True
            End If


            If (Not String.IsNullOrEmpty(FacInternacional.Numerofactura)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalNumeroFactura, FacInternacional.Numerofactura)
                variosFiltros = True
            End If

            'If (FacInternacional.Monto <> 0) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalMonto, FacInternacional.Monto)
            '    variosFiltros = True
            'End If

            If (FacInternacional.Pais IsNot Nothing) AndAlso (Not FacInternacional.Pais.Id.Equals("")) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalPais, FacInternacional.Pais.Id)
                variosFiltros = True
            End If

            If (Not String.IsNullOrEmpty(FacInternacional.Detalle)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalDetalle, FacInternacional.Detalle)
                variosFiltros = True
            End If

            'Filtro nuevo para el campo ISEL
            If (Not String.IsNullOrEmpty(FacInternacional.RevisionAprobada)) Then
                If variosFiltros Then
                    filtro += " and "
                End If
                filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalRevisionAprobada, FacInternacional.RevisionAprobada)
                variosFiltros = True
            End If

            'If (FacInternacional.Banco IsNot Nothing) AndAlso (Not FacInternacional.Banco.Id.Equals("")) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalBanco, FacInternacional.Banco.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacional.Idioma IsNot Nothing) AndAlso (FacInternacional.Idioma.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalIdioma, FacInternacional.Idioma.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacional.Moneda IsNot Nothing) AndAlso (FacInternacional.Moneda.Id <> "") Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalMoneda, FacInternacional.Moneda.Id)
            '    variosFiltros = True
            'End If
            'If (FacInternacional.FechaCobro IsNot Nothing) AndAlso (Not FacInternacional.FechaCobro.Equals(DateTime.MinValue)) Then
            '    If variosFiltros Then
            '        filtro += " and "
            '    End If
            '    Dim fecha As String = [String].Format("{0:dd/MM/yy}", FacInternacional.FechaCobro)
            '    Dim fechaday As DateTime = FacInternacional.FechaCobro
            '    Dim fecha2 As String = [String].Format("{0:dd/MM/yy}", fechaday.AddDays(1))
            '    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacInternacionalFechaCobro, fecha, fecha2)
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
            FacInternacionales = query.List(Of FacInternacional)()

            Return FacInternacionales

        End Function


    End Class
End Namespace