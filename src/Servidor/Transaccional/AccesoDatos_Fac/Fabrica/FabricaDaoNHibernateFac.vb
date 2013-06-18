Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.AccesoDatos.Dao.NHibernate

Namespace Fabrica
    Public Class FabricaDaoNHibernateFac

        Inherits FabricaDaoBaseFac
        Private Shared _instancia As FabricaDaoNHibernateFac

        ''' <summary>
        ''' Propiedad que devuelve la instancia de la clase
        ''' </summary>
        Public Shared ReadOnly Property ObtenerInstancia() As FabricaDaoNHibernateFac
            Get
                If _instancia Is Nothing Then
                    _instancia = New FabricaDaoNHibernateFac()
                End If
                Return _instancia
            End Get
        End Property

        ' ''' <summary>
        ' ''' Constructor predeterminado
        ' ''' </summary>
        ''Singleton

        Private Sub New()
        End Sub


        Public Overrides Function ObtenerDaoTasa() As IDaoTasa
            Return New DaoTasaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDocumentosTraduccion() As IDaoDocumentosTraduccion
            Return New DaoDocumentosTraduccionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDocumentosMarca() As IDaoDocumentosMarca
            Return New DaoDocumentosMarcaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDocumentosPatente() As IDaoDocumentosPatente
            Return New DaoDocumentosPatenteNHibernate()
        End Function

        Public Overrides Function ObtenerDaoTipoClase() As IDaoTipoClase
            Return New DaoTipoClaseNHibernate()
        End Function

        Public Overrides Function ObtenerDaoTipoMarca() As IDaoTipoMarca
            Return New DaoTipoMarcaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoTipoPatente() As IDaoTipoPatente
            Return New DaoTipoPatenteNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacAnualidad() As IDaoFacAnualidad
            Return New DaoFacAnualidadNHibernate()
        End Function

        Public Overrides Function ObtenerDaoMaterial() As IDaoMaterial
            Return New DaoMaterialNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacRecurso() As IDaoFacRecurso
            Return New DaoFacRecursoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoMotivo() As IDaoMotivo
            Return New DaoMotivoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDetalleEnvio() As IDaoDetalleEnvio
            Return New DaoDetalleEnvioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDetallePago() As IDaoDetallePago
            Return New DaoDetallePagoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoImpuesto() As IDaoImpuesto
            Return New DaoImpuestoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoSociedad() As IDaoSociedad
            Return New DaoSociedadNHibernate()
        End Function

        Public Overrides Function ObtenerDaoGuia() As IDaoGuia
            Return New DaoGuiaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoServicio() As IDaoServicio
            Return New DaoServicioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoCorrespondencia() As IDaoCorrespondencia
            Return New DaoCorrespondenciaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacJustificacion() As IDaoFacJustificacion
            Return New DaoFacJustificacionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoEtiqueta() As IDaoEtiqueta
            Return New DaoEtiquetaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDepartamentoServicio() As IDaoDepartamentoServicio
            Return New DaoDepartamentoServicioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoDesgloseServicio() As IDaoDesgloseServicio
            Return New DaoDesgloseServicioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoTarifaServicio() As IDaoTarifaServicio
            Return New DaoTarifaServicioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoConceptoGestion() As IDaoConceptoGestion
            Return New DaoConceptoGestionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoMediosGestion() As IDaoMediosGestion
            Return New DaoMediosGestionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoViGestionAsociado() As IDaoViGestionAsociado
            Return New DaoViGestionAsociadoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoBancoG() As IDaoBancoG
            Return New DaoBancoGNHibernate()
        End Function

        Public Overrides Function ObtenerDaoChequeRecido() As IDaoChequeRecido
            Return New DaoChequeRecidoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacBanco() As IDaoFacBanco
            Return New DaoFacBancoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacPagoBolivia() As IDaoFacPagoBolivia
            Return New DaoFacPagoBoliviaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacion() As IDaoFacOperacion
            Return New DaoFacOperacionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacCobro() As IDaoFacCobro
            Return New DaoFacCobroNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacCredito() As IDaoFacCredito
            Return New DaoFacCreditoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacForma() As IDaoFacForma
            Return New DaoFacFormaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacCobroFactura() As IDaoFacCobroFactura
            Return New DaoFacCobroFacturaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionDetalle() As IDaoFacOperacionDetalle
            Return New DaoFacOperacionDetalleNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionDetaAnulada() As IDaoFacOperacionDetaAnulada
            Return New DaoFacOperacionDetaAnuladaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionDetaProforma() As IDaoFacOperacionDetaProforma
            Return New DaoFacOperacionDetaProformaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionDetalleTm() As IDaoFacOperacionDetalleTm
            Return New DaoFacOperacionDetalleTmNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionDetaTmProforma() As IDaoFacOperacionDetaTmProforma
            Return New DaoFacOperacionDetaTmProformaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionAnulada() As IDaoFacOperacionAnulada
            Return New DaoFacOperacionAnuladaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionProforma() As IDaoFacOperacionProforma
            Return New DaoFacOperacionProformaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFactuDetaAnulada() As IDaoFacFactuDetaAnulada
            Return New DaoFacFactuDetaAnuladaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFactuDetaProforma() As IDaoFacFactuDetaProforma
            Return New DaoFacFactuDetaProformaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFactuDetalle() As IDaoFacFactuDetalle
            Return New DaoFacFactuDetalleNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFactura() As IDaoFacFactura
            Return New DaoFacFacturaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaAnulada() As IDaoFacFacturaAnulada
            Return New DaoFacFacturaAnuladaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaProforma() As IDaoFacFacturaProforma
            Return New DaoFacFacturaProformaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacContadorPro() As IDaoFacContadorPro
            Return New DaoFacContadorProNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacImpuesto() As IDaoFacImpuesto
            Return New DaoFacImpuestoNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacInternacional() As IDaoFacInternacional
            Return New DaoFacInternacionalNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacInternacionalAnulada() As IDaoFacInternacionalAnulada
            Return New DaoFacInternacionalAnuladaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacAnuladaFisica() As IDaoFacAnuladaFisica
            Return New DaoFacAnuladaFisicaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaTotalZ() As IDaoFacFacturaTotalZ
            Return New DaoFacFacturaTotalZNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaTotal() As IDaoFacFacturaTotal
            Return New DaoFacFacturaTotalNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacOperacionPais() As IDaoFacOperacionPais
            Return New DaoFacOperacionPaisNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaPendiente() As IDaoFacFacturaPendiente
            Return New DaoFacFacturaPendienteNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacStatementProcesar() As IDaoFacStatementProcesar
            Return New DaoFacStatementProcesarNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacDesgloseCole() As IDaoFacDesgloseCole
            Return New DaoFacDesgloseColeNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacGestion() As IDaoFacGestion
            Return New DaoFacGestionNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacFacturaPendienteConGru() As IDaoFacFacturaPendienteConGru
            Return New DaoFacFacturaPendienteConGruNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacVistaFacturacionCxpInterna() As IDaoFacVistaFacturacionCxpInterna
            Return New DaoFacVistaFacturacionCxpInternaNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacVistaFacturaServicio() As IDaoFacVistaFacturaServicio
            Return New DaoFacVistaFacturaServicioNHibernate()
        End Function

        Public Overrides Function ObtenerDaoFacTarifa() As IDaoFacTarifa
            Return New DaoFacTarifaNHibernate()
        End Function
    End Class
End Namespace