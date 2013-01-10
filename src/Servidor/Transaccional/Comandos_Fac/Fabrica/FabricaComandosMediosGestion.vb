Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosMediosGestion
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosMediosGestion
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="MediosGestion">MediosGestion a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal MediosGestion As MediosGestion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarMediosGestion(MediosGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los MediosGestiones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los MediosGestiones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of MediosGestion))
            Return New ComandoConsultarTodosMediosGestiones()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un MediosGestion
        ' ''' </summary>
        ' ''' <param name="usuario">MediosGestion que se va a MediosGestion</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarMediosGestion(ByVal MediosGestion As MediosGestion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarMediosGestion(MediosGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un MediosGestion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal MediosGestion As MediosGestion) As ComandoBase(Of MediosGestion)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="MediosGestion">MediosGestion a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaMediosGestion(ByVal MediosGestion As MediosGestion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaMediosGestion(MediosGestion)
        End Function
    End Class
End Namespace