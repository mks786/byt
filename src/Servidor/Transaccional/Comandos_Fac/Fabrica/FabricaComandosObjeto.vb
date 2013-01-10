Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosObjeto
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Fabrica
    Public NotInheritable Class FabricaComandosObjeto
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para insertar o modificar un Objeto
        ''' </summary>
        ''' <param name="objeto">Objeto a intersar o modificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal objeto As Objeto) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarObjeto(objeto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para para eliminar un Objeto
        ''' </summary>
        ''' <param name="objeto">Objeto a eliminar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoEliminarObjeto(ByVal objeto As Objeto) As ComandoBase(Of Boolean)
            Return New ComandoEliminarObjeto(objeto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los objetos
        ''' </summary>
        ''' <returns>Lista con todos los objetos</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Objeto))
            Return New ComandoConsultarTodosObjetos()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="objeto">Objeto a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaObjeto(ByVal objeto As Objeto) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaObjeto(objeto)
        End Function
    End Class
End Namespace