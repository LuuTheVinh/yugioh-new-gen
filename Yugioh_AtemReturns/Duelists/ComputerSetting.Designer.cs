﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yugioh_AtemReturns.Duelists {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class ComputerSetting : global::System.Configuration.ApplicationSettingsBase {
        
        private static ComputerSetting defaultInstance = ((ComputerSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ComputerSetting())));
        
        public static ComputerSetting Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("265, 105")]
        public global::Microsoft.Xna.Framework.Vector2 MainDeck {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["MainDeck"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("265, 197")]
        public global::Microsoft.Xna.Framework.Vector2 GraveYard {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["GraveYard"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("640, 190")]
        public global::Microsoft.Xna.Framework.Vector2 MonsterField {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["MonsterField"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("640, 90")]
        public global::Microsoft.Xna.Framework.Vector2 SpellField {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["SpellField"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12, 0")]
        public global::Microsoft.Xna.Framework.Vector2 HandDistance {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["HandDistance"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("765, 45")]
        public global::Microsoft.Xna.Framework.Vector2 Hand {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["Hand"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("78, 100")]
        public global::Microsoft.Xna.Framework.Vector2 FieldSlot {
            get {
                return ((global::Microsoft.Xna.Framework.Vector2)(this["FieldSlot"]));
            }
        }
    }
}
