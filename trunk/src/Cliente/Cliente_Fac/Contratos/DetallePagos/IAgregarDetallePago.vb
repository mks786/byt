﻿Imports Diginsoft.Bolet.Cliente.Fac.Contratos
Namespace Contratos.DetallePagos
    Interface IAgregarDetallePago
        Inherits IPaginaBaseFac
        Property DetallePago() As Object
        Sub Mensaje(ByVal mensaje__1 As String)
    End Interface
End Namespace