Imports System.Collections.Generic
Imports Diginsoft.Bolet.ObjetosComunes.Entidades

Namespace Contrato
    Public Interface IDaoRol
        Inherits IDaoBase(Of Rol, String)
        Function ObteneRolesYObjetos() As IList(Of Rol)
    End Interface
End Namespace
