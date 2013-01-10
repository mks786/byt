Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosSociedad
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosSociedad
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Sociedad">Sociedad a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Sociedad As Sociedad) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarSociedad(Sociedad)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Sociedades
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Sociedades</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Sociedad))
            Return New ComandoConsultarTodosSociedades()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Sociedad
        ' ''' </summary>
        ' ''' <param name="usuario">Sociedad que se va a Sociedad</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarSociedad(ByVal Sociedad As Sociedad) As ComandoBase(Of Boolean)
            Return New ComandoEliminarSociedad(Sociedad)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Sociedad por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Sociedad As Sociedad) As ComandoBase(Of Sociedad)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Sociedad">Sociedad a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaSociedad(ByVal Sociedad As Sociedad) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaSociedad(Sociedad)
        End Function
    End Class
End Namespace