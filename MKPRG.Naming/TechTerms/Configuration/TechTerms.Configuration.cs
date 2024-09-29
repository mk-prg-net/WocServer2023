using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Configuration
{
    public class Setting : NamingBase
    {
        public const long UID = 0x601E8A2;

        public Setting()
            : base(UID)
        {
        }

        public override string CNT => "setting";
        public override string CN => "设置";
        public override string DE => "Einstellung";
        public override string EN => "Setting";
        public override string ES => "Configuración";

        public override string Glyph => Glyphs.Engineering.LevelSlider;
    }



    public class Settings : PluralForm
    {
        public const long UID = 0x835A8A86;

        public Settings()
            : base(UID)
        {
        }

        public override string CNT => "settings";
        public override string CN => "设置";
        public override string DE => "Einstellungen";
        public override string EN => "Settings";
        public override string ES => "Ajustes";

        public override long PluralFormOfNameInSingluarNID => Setting.UID;

        public override string Glyph => Glyphs.Engineering.ControlKnobs;
    }


    public class ProgramSettings : PluralForm
    {
        public const long UID = 0x9EC0E892;

        public ProgramSettings()
            : base(UID)
        {
        }

        public override string CNT => "programSettings";
        public override string CN => "方案设置";
        public override string DE => "Programmeinstellungen";
        public override string EN => "Program Settings";
        public override string ES => "Ajustes del programa";

        public override long PluralFormOfNameInSingluarNID => Setting.UID;

        public override string Glyph => Glyphs.MechanicalEngineering.Gear;
    }

    public class UserSettings : PluralForm
    {
        public const long UID = 0x1AD66AED;

        public UserSettings()
            : base(UID)
        {
        }

        public override string CNT => "userSettings";
        public override string CN => "用户设置";
        public override string DE => "Benutzereinstellungen";
        public override string EN => "User Settings";
        public override string ES => "Configuración del usuario";

        public override long PluralFormOfNameInSingluarNID => Setting.UID;

        public override string Glyph => Glyphs.MechanicalEngineering.Gear;
    }
}
