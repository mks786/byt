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
    Public Class DaoCarpetaGestionAutomaticaNHibernate
        Inherits DaoBaseNHibernate(Of CarpetaGestionAutomatica, String)
        Implements IDaoCarpetaGestionAutomatica
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Public Function ObtenerCarpetasPorIniciales(ByVal Usuario As Trascend.Bolet.ObjetosComunes.Entidades.Usuario) As System.Collections.Generic.IList(Of ObjetosComunes.Entidades.CarpetaGestionAutomatica) Implements Contrato.IDaoCarpetaGestionAutomatica.ObtenerCarpetasPorIniciales
            Dim carpetas As IList(Of CarpetaGestionAutomatica) = Nothing
            Dim usuarioIniciales As String
            Dim usuarioNombreCompleto As String
            Dim query As IQuery

            usuarioIniciales = Usuario.Iniciales
            usuarioNombreCompleto = Usuario.NombreCompleto
            query = Session.CreateQuery(String.Format(Recursos.ConsultasHQL.ObtenerCarpetaGestionAutomatica, usuarioIniciales))
            carpetas = query.List(Of CarpetaGestionAutomatica)()

            Return carpetas

        End Function

        
    End Class
End Namespace

