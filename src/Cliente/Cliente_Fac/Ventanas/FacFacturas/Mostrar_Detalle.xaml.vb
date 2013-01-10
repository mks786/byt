Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace Ventanas.FacFacturas
    Partial Public Class Mostrar_Detalle
        Inherits Window

        Public Sub New(ByVal detalle As Object)
            InitializeComponent()
            _txtDetalle_Ing.Text = DirectCast(detalle, FacFactuDetalle).XDetalle
            _txtDetalle_Esp.Text = DirectCast(detalle, FacFactuDetalle).XDetalleEs
        End Sub

    End Class
End Namespace