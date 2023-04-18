using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Tree
{
    /// <summary>
    /// mko, 19.11.2020
    /// Durchlaufen eines Baumes
    /// </summary>
    public class Traverse
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xABBF5154;

        public static Traverse I { get; } = new Traverse();

        public Traverse()
            : base(UID)
        {
        }

        public override string CNT => "traverse";
        public override string CN => "横穿";
        public override string DE => "durchquere";
        public override string EN => "traverse";
        public override string ES => "atravesando";
    }

    /// <summary>
    /// Kindknoten- Ebene öffnen/zuklappen
    /// </summary>
    public class Fold
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xABCB6F7A;

        public static Fold I { get; } = new Fold();

        public Fold()
            : base(UID)
        {
        }

        public override string CNT => "fold";
        public override string CN => "褶皱";
        public override string DE => "klappe zu/schließe";
        public override string EN => "fold";
        public override string ES => "cerrar";
    }

    public class WasFolded
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x4429CCD9;

        public WasFolded()
            : base(UID)
        {
        }

        public override string CNT => "fold";
        public override string CN => "被折叠起来";
        public override string DE => "wurde zugeklappt";
        public override string EN => "was folded up";
        public override string ES => "fue doblado";
    }

    public class CanBeFolded
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x2C9D6742;

        public CanBeFolded()
            : base(UID)
        {
        }

        public override string CNT => "canBeFolded";
        public override string CN => "可折叠";
        public override string DE => "kann zugeklappt werden";
        public override string EN => "can be folded up";
        public override string ES => "se puede plegar";
    }

    public class CantBeFolded
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x5D8BF726;

        public CantBeFolded()
            : base(UID)
        {
        }

        public override string CNT => "cantBeFolded";
        public override string CN => "关不上";
        public override string DE => "kann nicht zugeklappt werden";
        public override string EN => "cant be folded up";
        public override string ES => "no se puede cerrar";
    }

    /// <summary>
    /// Kindknoten- Ebene aufklappen/öffnen
    /// </summary>
    public class Unfold
    : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xED3514AD;

        public Unfold()
            : base(UID)
        {
        }

        public override string CNT => "unfold";
        public override string CN => "展开";
        public override string DE => "klappe auf/öffne";
        public override string EN => "unfold";
        public override string ES => "abrir";
    }


    public class WasUnfolded
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xB3D736B4;

        public WasUnfolded()
            : base(UID)
        {
        }

        public override string CNT => "wasUnfolded";
        public override string CN => "已开通";
        public override string DE => "wurde aufgeklappt";
        public override string EN => "was unfolded";
        public override string ES => "se ha abierto";
    }

    public class CanBeUnfolded
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x5D888222;

        public CanBeUnfolded()
            : base(UID)
        {
        }

        public override string CNT => "canBeUnfolded";
        public override string CN => "得以展开";
        public override string DE => "kann aufgeklappt werden";
        public override string EN => "can be unfolded";
        public override string ES => "puede desplegarse";
    }

    public class CantBeUnfolded
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x70B87800;

        public CantBeUnfolded()
            : base(UID)
        {
        }

        public override string CNT => "cantBeUnfolded";
        public override string CN => "得以展开";
        public override string DE => "kann nicht aufgeklappt werden";
        public override string EN => "cant be unfolded";
        public override string ES => "no se puede desplegar";
    }




}
