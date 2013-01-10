Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosCorrespondencia
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosCorrespondencia
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Correspondencia">Correspondencia a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Correspondencia As Correspondencia) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarCorrespondencia(Correspondencia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Correspondenciaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Correspondenciaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Correspondencia))
            Return New ComandoConsultarTodosCorrespondencias()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Correspondencia
        ' ''' </summary>
        ' ''' <param name="usuario">Correspondencia que se va a Correspondencia</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarCorrespondencia(ByVal Correspondencia As Correspondencia) As ComandoBase(Of Boolean)
            Return New ComandoEliminarCorrespondencia(Correspondencia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Correspondencia por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Correspondencia As Correspondencia) As ComandoBase(Of Correspondencia)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Correspondencia">Correspondencia a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaCorrespondencia(ByVal Correspondencia As Correspondencia) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaCorrespondencia(Correspondencia)
        End Function
    End Class
End Namespace