﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace K2.Demo.CRM.Plugin.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("K2.Demo.CRM.Plugin.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to &lt;fetch version=&quot;1.0&quot; output-format=&quot;xml-platform&quot; mapping=&quot;logical&quot; distinct=&quot;false&quot;&gt;
        ///  &lt;entity name=&quot;k2_k2associations&quot;&gt;
        ///    &lt;attribute name=&quot;k2_k2associationsid&quot; /&gt;
        ///    &lt;attribute name=&quot;k2_name&quot; /&gt;
        ///    &lt;attribute name=&quot;createdon&quot; /&gt;
        ///    &lt;order attribute=&quot;k2_name&quot; descending=&quot;false&quot; /&gt;
        ///    &lt;filter type=&quot;and&quot;&gt;
        ///      &lt;condition attribute=&quot;k2_entitylogicalname&quot; operator=&quot;eq&quot; value=&quot;[entityname]&quot; /&gt;
        ///      &lt;condition attribute=&quot;k2_processfullname&quot; operator=&quot;eq&quot; value=&quot;[processname]&quot; /&gt;
        ///    &lt;/filter&gt;
        ///  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string K2AssociationsFetchXML {
            get {
                return ResourceManager.GetString("K2AssociationsFetchXML", resourceCulture);
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
