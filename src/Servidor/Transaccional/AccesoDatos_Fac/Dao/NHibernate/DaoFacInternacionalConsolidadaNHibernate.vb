Imports NLog
Imports System.Configuration
Imports System
Imports NHibernate
Imports System.Collections.Generic
Imports Diginsoft.Bolet.AccesoDatos.Contrato
Imports Diginsoft.Bolet.ObjetosComunes.Entidades
Imports Trascend.Bolet.AccesoDatos.Dao.NHibernate
Imports Trascend.Bolet.ObjetosComunes.Entidades

Namespace Dao.NHibernate
    Public Class DaoFacInternacionalConsolidadaNHibernate
        Inherits DaoBaseNHibernate(Of FacInternacionalConsolidada, Integer)
        Implements IDaoFacInternacionalConsolidada
        Private Shared logger As Logger = LogManager.GetCurrentClassLogger()

    End Class
End Namespace

