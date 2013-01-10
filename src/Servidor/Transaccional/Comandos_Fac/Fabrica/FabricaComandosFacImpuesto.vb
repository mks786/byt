Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacImpuesto
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacImpuesto
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="FacImpuesto">FacImpuesto a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal FacImpuesto As FacImpuesto) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacImpuesto(FacImpuesto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los FacImpuestoes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los FacImpuestoes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacImpuesto))
            Return New ComandoConsultarTodosFacImpuestos()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un FacImpuesto
        ' ''' </summary>
        ' ''' <param name="usuario">FacImpuesto que se va a FacImpuesto</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarFacImpuesto(ByVal FacImpuesto As FacImpuesto) As ComandoBase(Of Boolean)
            Return New ComandoEliminarFacImpuesto(FacImpuesto)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un FacImpuesto por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal FacImpuesto As FacImpuesto) As ComandoBase(Of FacImpuesto)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="FacImpuesto">FacImpuesto a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaFacImpuesto(ByVal FacImpuesto As FacImpuesto) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaFacImpuesto(FacImpuesto)
        End Function

        Public Shared Function ObtenerComandoConsultarFacImpuestosFiltro(ByVal FacImpuesto As FacImpuesto) As ComandoBase(Of IList(Of FacImpuesto))
            Return New ComandoConsultarFacImpuestosFiltro(FacImpuesto)
        End Function
    End Class
End Namespace