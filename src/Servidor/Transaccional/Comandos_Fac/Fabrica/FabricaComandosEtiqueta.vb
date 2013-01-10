Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosEtiqueta
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosEtiqueta
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Etiqueta">Etiqueta a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Etiqueta As Etiqueta) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarEtiqueta(Etiqueta)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Etiquetaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Etiquetaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Etiqueta))
            Return New ComandoConsultarTodosEtiquetas()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Etiqueta
        ' ''' </summary>
        ' ''' <param name="usuario">Etiqueta que se va a Etiqueta</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarEtiqueta(ByVal Etiqueta As Etiqueta) As ComandoBase(Of Boolean)
            Return New ComandoEliminarEtiqueta(Etiqueta)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Etiqueta por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Etiqueta As Etiqueta) As ComandoBase(Of Etiqueta)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Etiqueta">Etiqueta a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaEtiqueta(ByVal Etiqueta As Etiqueta) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaEtiqueta(Etiqueta)
        End Function
    End Class
End Namespace