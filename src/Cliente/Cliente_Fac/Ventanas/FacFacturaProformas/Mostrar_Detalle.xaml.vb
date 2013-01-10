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
Imports Diginsoft.Bolet.Cliente.Fac.Presentadores.FacFacturaProformas

Namespace Ventanas.FacFacturaProformas
    Partial Public Class Mostrar_Detalle
        Inherits Window
        Private presentadoragregar As PresentadorAgregarFacFacturaProforma
        Private presentadorconsultar As PresentadorConsultarFacFacturaProforma
        Private _tipo As Integer
        Private _detalle As FacFactuDetaProforma
        Public Sub New(ByVal detalle As Object, ByVal presenta As Object, ByVal tipo As Integer)
            InitializeComponent()
            _detalle = DirectCast(detalle, FacFactuDetaProforma)
            _txtDetalle_Ing.Text = _detalle.XDetalle
            _txtDetalle_Esp.Text = _detalle.XDetalleEs
            _tipo = tipo
            If tipo = 1 Then
                presentadoragregar = DirectCast(presenta, PresentadorAgregarFacFacturaProforma)
            Else
                presentadorconsultar = DirectCast(presenta, PresentadorConsultarFacFacturaProforma)
            End If
        End Sub

        Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_guardar.Click
            _detalle.XDetalle = _txtDetalle_Ing.Text
            _detalle.XDetalleEs = _txtDetalle_Esp.Text

            If _tipo = 1 Then
                presentadoragregar.buscar_departamento_servicio_cambiar_español_ingles(_detalle)
            Else
                presentadorconsultar.buscar_departamento_servicio_cambiar_español_ingles(_detalle)
            End If
            Finalize()
            MyBase.Close()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace