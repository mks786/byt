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
    Public Class DaoDesgloseServicioTarifaNHibernate
        Inherits DaoBaseNHibernate(Of FacDesgloseServicioTarifa, Char)
        Implements IDaoDesgloseServicioTarifa

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        ''' <summary>
        ''' Funcion que realiza la consulta de Desgloses de Servicio por Filtro
        ''' </summary>
        ''' <param name="FacDesgloseServicioTarifa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerFacDesgloseServiciosTarifaFiltro(FacDesgloseServicioTarifa As FacDesgloseServicioTarifa) As IList(Of FacDesgloseServicioTarifa) Implements Contrato.IDaoDesgloseServicioTarifa.ObtenerFacDesgloseServiciosTarifaFiltro

            Try

                Dim FacDesglosesServiciosTarifas As IList(Of FacDesgloseServicioTarifa) = Nothing
                Dim variosFiltros As Boolean = False
                Dim filtro As String = String.Empty
                Dim cabecera As String = String.Format(Recursos.ConsultasHQL.CabeceraObtenerFacDesgloseServicioTarifa)
                
                If (FacDesgloseServicioTarifa.Servicio IsNot Nothing) AndAlso (FacDesgloseServicioTarifa.Servicio.Id <> "") Then
                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioTarifaIdServicio, FacDesgloseServicioTarifa.Servicio.Id)
                    variosFiltros = True
                End If

                If (FacDesgloseServicioTarifa.Moneda IsNot Nothing) AndAlso (FacDesgloseServicioTarifa.Moneda.Id <> "") Then
                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioTarifaIdMoneda, FacDesgloseServicioTarifa.Moneda.Id)
                    variosFiltros = True
                End If

                If (FacDesgloseServicioTarifa.Tarifa IsNot Nothing) AndAlso (FacDesgloseServicioTarifa.Tarifa.Id <> "") Then
                    If variosFiltros Then
                        filtro += " and "
                    End If
                    filtro += String.Format(Recursos.ConsultasHQL.FiltroObtenerFacDesgloseServicioTarifaIdTarifa, FacDesgloseServicioTarifa.Tarifa.Id)
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
                FacDesglosesServiciosTarifas = query.List(Of FacDesgloseServicioTarifa)()

                Return FacDesglosesServiciosTarifas


            Catch ex As ApplicationException
                logger.[Error](ex.Message)
                Throw ex
            End Try

        End Function

        

        
    End Class

End Namespace
