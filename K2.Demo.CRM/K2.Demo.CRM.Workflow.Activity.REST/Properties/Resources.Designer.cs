﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace K2.Demo.CRM.Workflow.Activity.REST.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("K2.Demo.CRM.Workflow.Activity.REST.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;w:ProcessInstance xmlns:w=&quot;http://schemas.k2.com/worklist/d1&quot;  xmlns:p=&quot;http://schemas.k2.com/process/d1&quot;
        /// FullName=&quot;[FULLNAME]&quot; Folio=&quot;[FOLIO]&quot; Priority=&quot;[PRIORITY]&quot;&gt;
        /// &lt;p:DataField Name=&quot;[ENTITY ID]&quot;&gt;[ENTITY ID VALUE]&lt;/p:DataField&gt;
        /// &lt;p:DataField Name=&quot;[ENTITY NAME]&quot;&gt;[ENTITY NAME VALUE]&lt;/p:DataField&gt;
        /// &lt;p:DataField Name=&quot;[CRM ORIGINATOR]&quot;&gt;[CRM ORIGINATOR VALUE]&lt;/p:DataField&gt;
        ///&lt;/w:ProcessInstance&gt;.
        /// </summary>
        internal static string K2ServicesStartProcess {
            get {
                return ResourceManager.GetString("K2ServicesStartProcess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;fetch version=&quot;1.0&quot; output-format=&quot;xml-platform&quot; mapping=&quot;logical&quot; distinct=&quot;false&quot;&gt;
        ///  &lt;entity name=&quot;k2_k2settings&quot;&gt;
        ///    &lt;attribute name=&quot;k2_name&quot; /&gt;
        ///    &lt;attribute name=&quot;k2_value&quot; /&gt;
        ///    &lt;attribute name=&quot;k2_k2settingsid&quot; /&gt;
        ///  &lt;/entity&gt;
        ///&lt;/fetch&gt;.
        /// </summary>
        internal static string K2SettingsFetchXML {
            get {
                return ResourceManager.GetString("K2SettingsFetchXML", resourceCulture);
            }
        }
    }
}
