Imports System.Collections.Generic
Imports NHibernate
Imports NHibernate.Criterion
Imports NLog
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public MustInherit Class DaoBaseNHibernate(Of T, Id)
        Implements IDaoBase(Of T, Id)

        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()
        Private _session As ISession

        ''' <summary>
        ''' Propiedad para obtener la sesion
        ''' </summary>
        Public Property Session() As ISession
            Get
                If _session Is Nothing Then
                    _session = NHibernateHelper.OpenSession()
                End If
                Return _session
            End Get

            Set(ByVal value As ISession)
                _session = value
            End Set
        End Property



        Public Function Eliminar(ByVal entidad As T) As Boolean Implements Contrato.IDaoBase(Of T, Id).Eliminar
            Dim exitoso As Boolean

            Try
                Dim transaccion As ITransaction = Session.BeginTransaction()
                Session.Delete(entidad)
                transaccion.Commit()
                exitoso = transaccion.WasCommitted
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExEliminando)
            Finally
                Session.Close()
            End Try

            Return exitoso
        End Function

        Public Function InsertarOModificar(ByVal entidad As T) As Boolean Implements Contrato.IDaoBase(Of T, Id).InsertarOModificar
            Dim exitoso As Boolean

            Try
                Dim transaccion As ITransaction = Session.BeginTransaction()
                Session.SaveOrUpdate(entidad)
                transaccion.Commit()
                exitoso = transaccion.WasCommitted
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExInsertarOModificar)
            Finally
                Session.Close()
            End Try

            Return exitoso
        End Function

        Public Function ObtenerPorId(ByVal id As Id) As T Implements Contrato.IDaoBase(Of T, Id).ObtenerPorId
            Dim entidad As T

            Try
                entidad = Session.[Get](Of T)(id)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExObtenerPorId)
            Finally
                Session.Close()
            End Try

            Return entidad
        End Function

        Public Function ObtenerPorIdYBloquear(ByVal id As Id) As T Implements Contrato.IDaoBase(Of T, Id).ObtenerPorIdYBloquear
            Dim entidad As T

            Try
                entidad = Session.[Get](Of T)(id, LockMode.Upgrade)
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExObtenerPorId)
            Finally
                Session.Close()
            End Try

            Return entidad
        End Function

        Public Function ObtenerTodos() As System.Collections.Generic.IList(Of T) Implements Contrato.IDaoBase(Of T, Id).ObtenerTodos
            Dim listaEntidad As IList(Of T)

            Try
                listaEntidad = Session.CreateCriteria(GetType(T)).AddOrder(Order.Asc("Id")).List(Of T)()
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExConsultarTodos)
            Finally
                Session.Close()
            End Try

            Return listaEntidad
        End Function

        Public Function VerificarExistencia(ByVal id As Id) As Boolean Implements Contrato.IDaoBase(Of T, Id).VerificarExistencia
            Dim existe As Boolean = False
            Dim entidad As T

            Try
                entidad = Session.[Get](Of T)(id)

                If entidad IsNot Nothing Then
                    existe = True
                End If
            Catch ex As Exception
                logger.[Error](ex.Message)
                Throw New ApplicationException(Recursos.Errores.ExObtenerPorId)
            Finally
                Session.Close()
            End Try

            Return existe
        End Function
    End Class
End Namespace