Imports System.Collections.Generic
Imports Diginsoft.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosDepartamento
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Fabrica
    Public NotInheritable Class FabricaComandosDepartamento
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para agregar un Departamento
        ''' </summary>
        ''' <param name="departamento">Departamento a agregar en la base de datos</param>
        ''' <returns>El Comando que permite agregar el departamento en la base de datos</returns>
        Public Shared Function ObtenerComandoAgregar(ByVal departamento As Departamento) As ComandoBase(Of String)
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando para consultar todos los departamentos
        ''' </summary>
        ''' <returns>El Comando para consultar todos los Departamentos</returns>
        Public Shared Function ObtenerComandoConsultarTodos() As ComandoBase(Of IList(Of Departamento))
            Return New ComandoConsultarTodosDepartamentos()
        End Function
        ''' <summary>
        ''' Método que devuelve el Comando para consultar un Departamento por su ID
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorID(ByVal departamento As Departamento) As ComandoBase(Of Departamento)        
            Return New ComandoConsultarDepartamentoPorId(departamento)
        End Function
    End Class
End Namespace
