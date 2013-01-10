Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacGestion
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacGestion
        Private Sub New()
        End Sub
        ' ''' <summary>
        ' ''' Método que devuelve el Comando para agregar un país
        ' ''' </summary>
        ' ''' <param name="FacGestion">FacGestion a agregar en la base de datos</param>
        ' ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacGestion As FacGestion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacGestion(FacGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacGestiones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacGestiones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacGestion))
            Return New ComandoConsultarTodosFacGestiones()
        End Function

        '' ''' <summary>
        '' ''' Método que devuelve el Comando para elimnar un FacGestion
        '' ''' </summary>
        '' ''' <param name="usuario">FacGestion que se va a FacGestion</param>
        '' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacGestion(ByVal FacGestion As FacGestion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacGestion(FacGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacGestion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacGestion As FacGestion) As ComandoBase(Of FacGestion)
            Throw New NotImplementedException()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando verificar existencia
        ' ''' </summary>
        ' ''' <param name="FacGestion">FacGestion a verificar</param>
        ' ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacGestion(ByVal FacGestion As FacGestion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacGestion(FacGestion)
        End Function

        Public Shared Function ObtenerComandoConsultarFacGestionesFiltro(ByVal FacGestion As FacGestion) As ComandoBase(Of IList(Of FacGestion))
            Return New ComandoConsultarFacGestionesFiltro(FacGestion)
        End Function

    End Class
End Namespace