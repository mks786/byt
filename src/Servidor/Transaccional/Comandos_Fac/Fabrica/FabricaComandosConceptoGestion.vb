Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosConceptoGestion
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosConceptoGestion
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="ConceptoGestion">ConceptoGestion a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal ConceptoGestion As ConceptoGestion) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarConceptoGestion(ConceptoGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los ConceptoGestiones
        ''' </summary>
        ''' <returns>El Comando para consultar todos los ConceptoGestiones</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of ConceptoGestion))
            Return New ComandoConsultarTodosConceptoGestiones()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un ConceptoGestion
        ' ''' </summary>
        ' ''' <param name="usuario">ConceptoGestion que se va a ConceptoGestion</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarConceptoGestion(ByVal ConceptoGestion As ConceptoGestion) As ComandoBase(Of Boolean)
            Return New ComandoEliminarConceptoGestion(ConceptoGestion)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un ConceptoGestion por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal ConceptoGestion As ConceptoGestion) As ComandoBase(Of ConceptoGestion)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="ConceptoGestion">ConceptoGestion a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaConceptoGestion(ByVal ConceptoGestion As ConceptoGestion) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaConceptoGestion(ConceptoGestion)
        End Function
    End Class
End Namespace