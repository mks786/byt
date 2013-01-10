Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosImpuesto
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosImpuesto
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="Impuesto">Impuesto a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal Impuesto As Impuesto) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarImpuesto(Impuesto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los Impuestoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Impuestoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Impuesto))
            Return New ComandoConsultarTodosImpuestos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un Impuesto
        ' ''' </summary>
        ' ''' <param name="usuario">Impuesto que se va a Impuesto</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarImpuesto(ByVal Impuesto As Impuesto) As ComandoBase(Of Boolean)
            Return New ComandoEliminarImpuesto(Impuesto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Impuesto por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal Impuesto As Impuesto) As ComandoBase(Of Impuesto)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="Impuesto">Impuesto a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaImpuesto(ByVal Impuesto As Impuesto) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaImpuesto(Impuesto)
        End Function
    End Class
End Namespace