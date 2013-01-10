Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Comandos.ComandosUsuario
    Public Class ComandoConsultarUsuarioPorID
        Inherits ComandoBase(Of Usuario)
        Private _usuario As Usuario

        ''' <summary>
        ''' Constructor predeterminado
        ''' </summary>
        ''' <param name="usuario">Usuario a consultar</param>
        Public Sub New(ByVal usuario As Usuario)
            Me._usuario = usuario
        End Sub

        ''' <summary>
        ''' Método que ejecuta el comando
        ''' </summary>
        Public Overrides Sub Ejecutar()
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace