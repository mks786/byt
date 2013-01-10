Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDepartamentoServicio
Imports Trascend.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.Comandos.Comandos
Namespace Fabrica
    Public NotInheritable Class FabricaComandosDepartamentoServicio
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un país
        ''' </summary>
        ''' <param name="DepartamentoServicio">DepartamentoServicio a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el país en la base de datos</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal DepartamentoServicio As FacDepartamentoServicio) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarDepartamentoServicio(DepartamentoServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los DepartamentoServicioes
        ''' </summary>
        ''' <returns>El Comando para consultar todos los DepartamentoServicioes</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of FacDepartamentoServicio))
            Return New ComandoConsultarTodosDepartamentoServicios()
        End Function

        ' ''' <summary>
        ' ''' Método que devuelve el Comando para elimnar un DepartamentoServicio
        ' ''' </summary>
        ' ''' <param name="usuario">DepartamentoServicio que se va a DepartamentoServicio</param>
        ' ''' <returns>Comando para eliminar</returns>
        Public Shared Function ObtenerComandoEliminarDepartamentoServicio(ByVal DepartamentoServicio As FacDepartamentoServicio) As ComandoBase(Of Boolean)
            Return New ComandoEliminarDepartamentoServicio(DepartamentoServicio)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar un DepartamentoServicio por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal DepartamentoServicio As FacDepartamentoServicio) As ComandoBase(Of FacDepartamentoServicio)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando verificar existencia
        ''' </summary>
        ''' <param name="DepartamentoServicio">DepartamentoServicio a verificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoVerificarExistenciaDepartamentoServicio(ByVal DepartamentoServicio As FacDepartamentoServicio) As ComandoBase(Of Boolean)
            Return New ComandoVerificarExistenciaDepartamentoServicio(DepartamentoServicio)
        End Function


        Public Shared Function ObtenerComandoConsultarFacDepartamentoServiciosFiltro(ByVal FacDepartamentoServicio As FacDepartamentoServicio) As ComandoBase(Of IList(Of FacDepartamentoServicio))
            Return New ComandoConsultarFacDepartamentoServiciosFiltro(FacDepartamentoServicio)
        End Function
    End Class
End Namespace