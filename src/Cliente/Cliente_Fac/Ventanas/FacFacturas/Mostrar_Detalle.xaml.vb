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
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturas

Namespace Ventanas.FacFacturas
    Partial Public Class Mostrar_Detalle
        Inherits Window

        Private presentadorConsultarFacFactura As PresentadorConsultarFacFactura
        Private presentadorConsultarFacFacturaPase As PresentadorConsultarFacFacturaPase
        Private _tipo As Integer
        Private _detalle As FacFactuDetalle

        Public Sub New(ByVal detalle As Object, ByVal presenta As Object, ByVal tipo As Integer)
            InitializeComponent()
            _detalle = DirectCast(detalle, FacFactuDetalle)
            _txtDetalle_Ing.Text = DirectCast(detalle, FacFactuDetalle).XDetalle
            _txtDetalle_Esp.Text = DirectCast(detalle, FacFactuDetalle).XDetalleEs
            _tipo = tipo
            If tipo = 1 Then
                presentadorConsultarFacFactura = DirectCast(presenta, PresentadorConsultarFacFactura)
            Else
                presentadorConsultarFacFacturaPase = DirectCast(presenta, PresentadorConsultarFacFacturaPase)
            End If
        End Sub

        Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_guardar.Click
            _detalle.XDetalle = _txtDetalle_Ing.Text
            _detalle.XDetalleEs = _txtDetalle_Esp.Text

            If _tipo = 1 Then
                presentadorConsultarFacFactura.buscar_departamento_servicio_cambiar_español_ingles(_detalle)
            Else
                presentadorConsultarFacFacturaPase.buscar_departamento_servicio_cambiar_español_ingles(_detalle)
            End If
            Finalize()
            MyBase.Close()
        End Sub


        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace