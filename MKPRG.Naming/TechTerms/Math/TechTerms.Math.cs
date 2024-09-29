using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Math
{
	public class Definition : NamingBase
	{
		public const long UID = 0xE2AB867A;

		public Definition()
			: base(UID)
		{
		}

		public override string CNT => "def";
		public override string CN => "定义";
		public override string DE => "Definition";
		public override string EN => "Definition";
		public override string ES => "Definición";


		public override string Glyph => Glyphs.Math.Definition;
	}


	public class Define
		: NamingBase,
		Grammar.IInProgressActivity

	{
		public const long UID = 0x8B239F78;

		public Define()
			: base(UID)
		{
		}

		public override string CNT => "def";
		public override string CN => "定义";
		public override string DE => "definiere";
		public override string EN => "define";
		public override string ES => "definir";

		public override string Glyph => Glyphs.Math.Definition;
	}

	public class WasDefined
		: NamingBase,
		Grammar.IFinishedActivity

	{
		public const long UID = 0x835253DF;

		public WasDefined()
			: base(UID)
		{
		}

		public override string CNT => "wasDefined";
		public override string CN => "被定义为";
		public override string DE => "wurde definiert";
		public override string EN => "was defined";
		public override string ES => "se definió";

		public override string Glyph => Glyphs.Math.Definition;
	}

	public class WasNotDefined
		: NamingBase,
		Grammar.IFinishedActivity

	{
		public const long UID = 0x1687B08F;

		public WasNotDefined()
			: base(UID)
		{
		}

		public override string CNT => "wasNotDefined";
		public override string CN => "未被定义";
		public override string DE => "wurde nicht definiert";
		public override string EN => "was not defined";
		public override string ES => "no se definió";

		public override string Glyph => Glyphs.Math.Definition;
	}

	public class CanBeDefined
		: NamingBase,
		Grammar.IModalPhrase

	{
		public const long UID = 0x3C976221;

		public CanBeDefined()
			: base(UID)
		{
		}

		public override string CNT => "canBeDefined";
		public override string CN => "可以定义为";
		public override string DE => "kann definiert werden";
		public override string EN => "can be defined";
		public override string ES => "se puede definir";

		public override string Glyph => Glyphs.Math.Definition;
	}

	public class CantBeDefined
		: NamingBase,
		Grammar.IModalPhrase

	{
		public const long UID = 0x7FB37CFB;

		public CantBeDefined()
			: base(UID)
		{
		}

		public override string CNT => "canNotBeDefined";
		public override string CN => "可以定义为";
		public override string DE => "kann nicht definiert werden";
		public override string EN => "can't be defined";
		public override string ES => "no se puede definir";

		public override string Glyph => Glyphs.Math.Definition;
	}

	public class WillBeDefined
		: NamingBase,
		Grammar.IModalPhrase

	{
		public const long UID = 0x38010C28;

		public WillBeDefined()
			: base(UID)
		{
		}

		public override string CNT => "willBeDefined";
		public override string CN => "将被定义为";
		public override string DE => "wird definiert werden";
		public override string EN => "will be defined";
		public override string ES => "se definirá";

		public override string Glyph => Glyphs.Math.Definition;
	}

}
