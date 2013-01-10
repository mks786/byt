Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosGuia
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosGuia
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Guia">Guia a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Guia As Guia) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarGuia(Guia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Guiaes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Guiaes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Guia))
            Return New ComandoConsultarTodosGuias()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Guia
        ' ''' </summary>
        ' ''' <param name="usuario">Guia que se va a Guia</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarGuia(ByVal Guia As Guia) As ComandoBase(Of Boolean)
            Return New ComandoEliminarGuia(Guia)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Guia por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Guia As Guia) As ComandoBase(Of Guia)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Guia">Guia a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaGuia(ByVal Guia As Guia) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaGuia(Guia)
        End Function
    End Class
End Namespace