﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trascend.Bolet.AccesoDatos.Recursos {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class NHibernateConfiguration {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NHibernateConfiguration() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Trascend.Bolet.AccesoDatos.Recursos.NHibernateConfiguration", typeof(NHibernateConfiguration).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to bolet.
        /// </summary>
        public static string NHibernateClaveBD {
            get {
                return ResourceManager.GetString("NHibernateClaveBD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NHibernate.Dialect.Oracle10gDialect.
        /// </summary>
        public static string NHibernateDialect {
            get {
                return ResourceManager.GetString("NHibernateDialect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NHibernate.Driver.OracleClientDriver.
        /// </summary>
        public static string NHibernateDriverClass {
            get {
                return ResourceManager.GetString("NHibernateDriverClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.2.4)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=xe))).
        /// </summary>
        public static string NHibernateRutaBDDataSource {
            get {
                return ResourceManager.GetString("NHibernateRutaBDDataSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to bolet.
        /// </summary>
        public static string NHibernateUsuarioBD {
            get {
                return ResourceManager.GetString("NHibernateUsuarioBD", resourceCulture);
            }
        }
    }
}
