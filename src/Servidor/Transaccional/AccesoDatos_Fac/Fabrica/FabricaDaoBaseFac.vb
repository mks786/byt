Imports System.Configuration
Imports Diginsoft.Bolet.AccesoDatos.Contrato

Namespace Fabrica
    Public MustInherit Class FabricaDaoBaseFac
        Implements IFabricaDaoBaseFac

        Private Shared ReadOnly _manejadores As String() = {"NHibernate"}
        ''' <summary>
        ''' Método que devuelve la FabricaDao del manejador que se esté usando
        ''' </summary>
        ''' <returns>FabricaDao del manejador que se esté usando</returns>
        Public Shared Function ObtenerFabricaDao() As FabricaDaoBaseFac
            Dim manejador As String = ConfigurationManager.AppSettings("ManejadorBD")

            If manejador = _manejadores(0) Then
                Return FabricaDaoNHibernateFac.ObtenerInstancia
            Else
                Return Nothing
            End If
        End Function

        Public MustOverride Function ObtenerDaoTasa() As Contrato.IDaoTasa Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoTasa

        Public MustOverride Function ObtenerDaoDocumentosTraduccion() As Contrato.IDaoDocumentosTraduccion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDocumentosTraduccion

        Public MustOverride Function ObtenerDaoDocumentosMarca() As Contrato.IDaoDocumentosMarca Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDocumentosMarca

        Public MustOverride Function ObtenerDaoDocumentosPatente() As Contrato.IDaoDocumentosPatente Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDocumentosPatente

        Public MustOverride Function ObtenerDaoTipoClase() As Contrato.IDaoTipoClase Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoTipoClase

        Public MustOverride Function ObtenerDaoTipoMarca() As Contrato.IDaoTipoMarca Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoTipoMarca

        Public MustOverride Function ObtenerDaoTipoPatente() As Contrato.IDaoTipoPatente Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoTipoPatente

        Public MustOverride Function ObtenerDaoFacAnualidad() As Contrato.IDaoFacAnualidad Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacAnualidad

        Public MustOverride Function ObtenerDaoMaterial() As Contrato.IDaoMaterial Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoMaterial

        Public MustOverride Function ObtenerDaoFacRecurso() As Contrato.IDaoFacRecurso Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacRecurso


        Public MustOverride Function ObtenerDaoMotivo() As Contrato.IDaoMotivo Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoMotivo

        Public MustOverride Function ObtenerDaoDetalleEnvio() As Contrato.IDaoDetalleEnvio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDetalleEnvio

        Public MustOverride Function ObtenerDaoDetallePago() As Contrato.IDaoDetallePago Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDetallePago

        Public MustOverride Function ObtenerDaoImpuesto() As Contrato.IDaoImpuesto Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoImpuesto

        Public MustOverride Function ObtenerDaoSociedad() As Contrato.IDaoSociedad Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoSociedad

        Public MustOverride Function ObtenerDaoGuia() As Contrato.IDaoGuia Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoGuia

        Public MustOverride Function ObtenerDaoServicio() As Contrato.IDaoServicio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoServicio

        Public MustOverride Function ObtenerDaoCorrespondencia() As Contrato.IDaoCorrespondencia Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoCorrespondencia

        Public MustOverride Function ObtenerDaoFacJustificacion() As Contrato.IDaoFacJustificacion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacJustificacion

        Public MustOverride Function ObtenerDaoEtiqueta() As Contrato.IDaoEtiqueta Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoEtiqueta

        Public MustOverride Function ObtenerDaoDepartamentoServicio() As Contrato.IDaoDepartamentoServicio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDepartamentoServicio

        Public MustOverride Function ObtenerDaoDesgloseServicio() As Contrato.IDaoDesgloseServicio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoDesgloseServicio

        Public MustOverride Function ObtenerDaoTarifaServicio() As Contrato.IDaoTarifaServicio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoTarifaServicio

        Public MustOverride Function ObtenerDaoConceptoGestion() As Contrato.IDaoConceptoGestion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoConceptoGestion

        Public MustOverride Function ObtenerDaoMediosGestion() As Contrato.IDaoMediosGestion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoMediosGestion

        Public MustOverride Function ObtenerDaoViGestionAsociado() As Contrato.IDaoViGestionAsociado Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoViGestionAsociado

        Public MustOverride Function ObtenerDaoBancoG() As Contrato.IDaoBancoG Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoBancoG

        Public MustOverride Function ObtenerDaoChequeRecido() As Contrato.IDaoChequeRecido Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoChequeRecido

        Public MustOverride Function ObtenerDaoFacBanco() As Contrato.IDaoFacBanco Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacBanco

        Public MustOverride Function ObtenerDaoFacPagoBolivia() As Contrato.IDaoFacPagoBolivia Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacPagoBolivia

        Public MustOverride Function ObtenerDaoFacOperacion() As Contrato.IDaoFacOperacion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacion

        Public MustOverride Function ObtenerDaoFacCobro() As Contrato.IDaoFacCobro Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacCobro

        Public MustOverride Function ObtenerDaoFacCredito() As Contrato.IDaoFacCredito Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacCredito

        Public MustOverride Function ObtenerDaoFacForma() As Contrato.IDaoFacForma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacForma

        Public MustOverride Function ObtenerDaoFacCobroFactura() As Contrato.IDaoFacCobroFactura Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacCobroFactura

        Public MustOverride Function ObtenerDaoFacOperacionDetalle() As Contrato.IDaoFacOperacionDetalle Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionDetalle

        Public MustOverride Function ObtenerDaoFacOperacionDetaAnulada() As Contrato.IDaoFacOperacionDetaAnulada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionDetaAnulada

        Public MustOverride Function ObtenerDaoFacOperacionDetaProforma() As Contrato.IDaoFacOperacionDetaProforma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionDetaProforma

        Public MustOverride Function ObtenerDaoFacOperacionDetalleTm() As Contrato.IDaoFacOperacionDetalleTm Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionDetalleTm

        Public MustOverride Function ObtenerDaoFacOperacionDetaTmProforma() As Contrato.IDaoFacOperacionDetaTmProforma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionDetaTmProforma

        Public MustOverride Function ObtenerDaoFacOperacionAnulada() As Contrato.IDaoFacOperacionAnulada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionAnulada

        Public MustOverride Function ObtenerDaoFacOperacionProforma() As Contrato.IDaoFacOperacionProforma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionProforma

        Public MustOverride Function ObtenerDaoFacFactuDetaAnulada() As Contrato.IDaoFacFactuDetaAnulada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFactuDetaAnulada

        Public MustOverride Function ObtenerDaoFacFactuDetaProforma() As Contrato.IDaoFacFactuDetaProforma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFactuDetaProforma

        Public MustOverride Function ObtenerDaoFacFactuDetalle() As Contrato.IDaoFacFactuDetalle Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFactuDetalle

        Public MustOverride Function ObtenerDaoFacFactura() As Contrato.IDaoFacFactura Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFactura

        Public MustOverride Function ObtenerDaoFacFacturaAnulada() As Contrato.IDaoFacFacturaAnulada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaAnulada

        Public MustOverride Function ObtenerDaoFacFacturaProforma() As Contrato.IDaoFacFacturaProforma Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaProforma

        Public MustOverride Function ObtenerDaoFacContadorPro() As Contrato.IDaoFacContadorPro Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacContadorPro

        Public MustOverride Function ObtenerDaoFacImpuesto() As Contrato.IDaoFacImpuesto Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacImpuesto

        Public MustOverride Function ObtenerDaoFacInternacional() As Contrato.IDaoFacInternacional Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacInternacional

        Public MustOverride Function ObtenerDaoFacAsociadoIntConsolidadoCxPInt() As Contrato.IDaoFacAsociadoIntConsolidadoCxPInt Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacAsociadoIntConsolidadoCxPInt

        Public MustOverride Function ObtenerDaoFacInternacionalConsolidada() As Contrato.IDaoFacInternacionalConsolidada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacInternacionalConsolidada

        Public MustOverride Function ObtenerDaoFacInternacionalAnulada() As Contrato.IDaoFacInternacionalAnulada Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacInternacionalAnulada

        Public MustOverride Function ObtenerDaoFacAnuladaFisica() As Contrato.IDaoFacAnuladaFisica Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacAnuladaFisica

        Public MustOverride Function ObtenerDaoFacFacturaTotalZ() As Contrato.IDaoFacFacturaTotalZ Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaTotalZ

        Public MustOverride Function ObtenerDaoFacFacturaTotal() As Contrato.IDaoFacFacturaTotal Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaTotal

        Public MustOverride Function ObtenerDaoFacOperacionPais() As Contrato.IDaoFacOperacionPais Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacOperacionPais

        Public MustOverride Function ObtenerDaoFacFacturaPendiente() As Contrato.IDaoFacFacturaPendiente Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaPendiente

        Public MustOverride Function ObtenerDaoFacStatementProcesar() As Contrato.IDaoFacStatementProcesar Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacStatementProcesar

        Public MustOverride Function ObtenerDaoFacDesgloseCole() As Contrato.IDaoFacDesgloseCole Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacDesgloseCole

        Public MustOverride Function ObtenerDaoFacGestion() As Contrato.IDaoFacGestion Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacGestion

        Public MustOverride Function ObtenerDaoFacFacturaPendienteConGru() As Contrato.IDaoFacFacturaPendienteConGru Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacFacturaPendienteConGru

        Public MustOverride Function ObtenerDaoFacVistaFacturacionCxpInterna() As Contrato.IDaoFacVistaFacturacionCxpInterna Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacVistaFacturacionCxpInterna

        Public MustOverride Function ObtenerDaoFacVistaFacturaServicio() As Contrato.IDaoFacVistaFacturaServicio Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacVistaFacturaServicio

        Public MustOverride Function ObtenerDaoFacTarifa() As Contrato.IDaoFacTarifa Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoFacTarifa

        Public MustOverride Function ObtenerDaoCarpetaGestionAutomatica() As Contrato.IDaoCarpetaGestionAutomatica Implements Contrato.IFabricaDaoBaseFac.ObtenerDaoCarpetaGestionAutomatica

    End Class
End Namespace