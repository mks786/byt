Imports System.Collections.Generic
Imports Trascend.Bolet.Comandos.Comandos
Imports Diginsoft.Bolet.Comandos.Comandos.ComandosFacContadorPro
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace Fabrica
    Public NotInheritable Class FabricaComandosFacContadorPro
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Método que devuelve el Comando para modificar una Contador
        ''' </summary>
        ''' <param name="contador">Contador a intersar o modificar</param>
        ''' <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        Public Shared Function ObtenerComandoInsertarOModificar(ByVal contador As FacContadorPro) As ComandoBase(Of Boolean)
            Return New ComandoInsertarOModificarFacContadorPro(contador)
        End Function

        ''' <summary>
        ''' Método que devuelve el Comando dado el dominio
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function ObtenerComandoConsultarPorId(ByVal id As String) As ComandoBase(Of FacContadorPro)
            Return New ComandoConsultarPorIdFacContadorPro(id)
        End Function
    End Class
End Namespace
