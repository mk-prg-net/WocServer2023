using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glyphs = MKPRG.Naming.Glyphs;

namespace MKPRG.HTML
{
    /// <summary>
    /// Glyphen (miniaturen mit fester, allgemeingültiger Semantik)
    /// </summary>
    partial class HTMLDocument
    {

        /// <summary>
        /// Pfeil nach Norden
        /// </summary>
        public HTMLDocument glyphArrN
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrN);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordost
        /// </summary>
        public HTMLDocument glyphArrNO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrNO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Ost
        /// </summary>
        public HTMLDocument glyphArrO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Südost
        /// </summary>
        public HTMLDocument glyphArrSO
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrSO);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Süden
        /// </summary>
        public HTMLDocument glyphArrS
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrS);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Südwesten
        /// </summary>
        public HTMLDocument glyphArrSW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrSW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Westen
        /// </summary>
        public HTMLDocument glyphArrW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphArrNW
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.arrNW);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRotAnticlockwise
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.rotAnticlockwise);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRotClockwise
        {
            get
            {
                bldDoc.Append(Glyphs.ArrowsAndLines.rotClockwise);
                return this;
            }
        }

        /// <summary>
        /// Pfeil zurück
        /// </summary>
        public HTMLDocument glyphUndo
        {
            get
            {
                bldDoc.Append(Glyphs.Transactions.Undo);
                return this;
            }
        }


        /// <summary>
        /// Informationszeichen
        /// </summary>
        public HTMLDocument glyphInformation
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.InfoCircled);
                return this;
            }
        }

        /// <summary>
        /// Zustand, Status
        /// </summary>
        public HTMLDocument glyphStatus
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Status);
                return this;
            }
        }

        /// <summary>
        /// Symbol für aktiven Zustand in einem Zustadsgraphen
        /// </summary>
        public HTMLDocument glyphAutomatonActiveState
        {
            get
            {
                bldDoc.Append(Glyphs.Automaton.ActiveState);
                return this;
            }
        }

        public HTMLDocument glyphAutomatonInactiveState
        {
            get
            {
                bldDoc.Append(Glyphs.Automaton.InactiveState);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphGlobe
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.Globe);
                return this;
            }
        }

        public HTMLDocument glyphGlobeEuropeAfrica
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeEuropeAfrica);
                return this;
            }
        }

        public HTMLDocument glyphGlobeAsiaAustralia
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeAsiaAustralia);
                return this;
            }
        }

        public HTMLDocument glyphGlobeAmerica
        {
            get
            {
                bldDoc.Append(Glyphs.Geographic.EarthGlobeAmerica);
                return this;
            }
        }



        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphFlash
        {
            get
            {
                bldDoc.Append(Glyphs.Weather.flash);
                return this;
            }
        }

        /// <summary>
        /// Fehlersymbol
        /// </summary>
        public HTMLDocument glyphError
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Error);
                return this;
            }
        }


        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphWhiteSquare
        {
            get
            {
                bldDoc.Append(Glyphs.Shapes.WhiteSquare);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphProject
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Project);
                return this;
            }
        }

        public HTMLDocument glyphProjectWithBaseLine
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Project);
                return this;
            }
        }


        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphStation
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Station);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphProcessmodul
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Processmodule);
                return this;
            }
        }

        /// <summary>
        /// Gebiet der Mechanik- Konstruktion
        /// </summary>
        public HTMLDocument glyphMechArea
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.MechBom);
                return this;
            }
        }

        /// <summary>
        /// Gebiet der Electro- Konstruktion
        /// </summary>
        public HTMLDocument glyphElectroArea
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ElectroBom);
                return this;
            }
        }

        /// <summary>
        /// DFC Projektsymbol
        /// </summary>
        public HTMLDocument glyphAssy
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Assy);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphSinglePart
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.SinglePart);
                return this;
            }
        }

        public HTMLDocument glyphATD
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATD);
                return this;
            }
        }

        public HTMLDocument glyphATZ
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATZ);
                return this;
            }
        }

        public HTMLDocument glyphAT3
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.AT3);
                return this;
            }
        }

        public HTMLDocument glyphTDP
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.TDP);
                return this;
            }
        }

        public HTMLDocument glyphEPlan
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.Eplan);
                return this;
            }
        }

        public HTMLDocument glyphCTS
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.CTS);
                return this;
            }
        }

        public HTMLDocument glyphEDC
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }

        public HTMLDocument glyphSFC
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }



        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRecycling
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Recycling);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphRescuer
        {
            get
            {
                bldDoc.Append(Glyphs.Support.Rescuer);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphReject
        {
            get
            {
                bldDoc.Append(Glyphs.Transactions.reject);
                return this;
            }
        }

        /// <summary>
        /// Pfeil nach Nordwesten
        /// </summary>
        public HTMLDocument glyphReturnKey
        {
            get
            {
                bldDoc.Append(Glyphs.Computer.ReturnKey);
                return this;
            }
        }

        public HTMLDocument glyphSkull
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Skull);
                return this;
            }
        }

        public HTMLDocument glyphSmiley
        {
            get
            {
                bldDoc.Append(Glyphs.Smileys.Smiley);
                return this;
            }
        }

        public HTMLDocument glyphNonSmiley
        {
            get
            {
                bldDoc.Append(Glyphs.Smileys.NonSmiley);
                return this;
            }
        }

        public HTMLDocument glyphAnchor
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Anchor);
                return this;
            }
        }

        public HTMLDocument glyphBoxedKey
        {
            get
            {
                //bldDoc.Append(Glyphs.VariousSigns.BoxedKey);
                return this;
            }
        }

        public HTMLDocument glyphCloud
        {
            get
            {
                bldDoc.Append(Glyphs.Net.Cloud);
                return this;
            }
        }

        public HTMLDocument glyphCollision
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Collision);
                return this;
            }
        }

        public HTMLDocument glyphCrossedSwords
        {
            get
            {
                bldDoc.Append(Glyphs.War.CrossedSwords);
                return this;
            }
        }

        /// <summary>
        /// Ordner
        /// </summary>
        public HTMLDocument glyphFolder
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Folder);
                return this;
            }
        }

        /// <summary>
        /// Fabrik
        /// </summary>
        public HTMLDocument glyphFactory
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Factory);
                return this;
            }
        }

        /// <summary>
        /// ATMO- Standort
        /// </summary>
        public HTMLDocument glyphATMOSite
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.ATMOSite);
                return this;
            }
        }

        /// <summary>
        /// Zeigefinger nach links
        /// </summary>
        public HTMLDocument glyphForefingerLeft
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ForefingerLeft);
                return this;
            }
        }

        /// <summary>
        /// Zeigefinger nach rechts
        /// </summary>
        public HTMLDocument glyphForefingerRight
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ForefingerRight);
                return this;
            }
        }

        /// <summary>
        /// Daumen hoch
        /// </summary>
        public HTMLDocument glyphThumbsUp
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ThumbsUp);
                return this;
            }
        }

        /// <summary>
        /// Anzeige von Erfolg
        /// </summary>
        public HTMLDocument glyphSuccess
        {
            get
            {
                bldDoc.Append(Glyphs.Events.Success);
                return this;
            }
        }

        /// <summary>
        /// Daumen runter
        /// </summary>
        public HTMLDocument glyphThumbsDown
        {
            get
            {
                bldDoc.Append(Glyphs.Gestures.ThumbsDown);
                return this;
            }
        }



        /// <summary>
        /// Zahnrad
        /// </summary>
        public HTMLDocument glyphGear
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.Gear);
                return this;
            }
        }

        /// <summary>
        /// Geist/Gespenst
        /// </summary>
        public HTMLDocument glyphGhost
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Ghost);
                return this;
            }
        }

        public HTMLDocument glyphHammerAndWrench
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.HammerAndWrench);
                return this;
            }
        }

        public HTMLDocument glyphKey
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Key);
                return this;
            }
        }

        public HTMLDocument glyphLink
        {
            get
            {
                bldDoc.Append(Glyphs.DataAndDocuments.Hyperlinks.Link);
                return this;
            }
        }

        public HTMLDocument glyphLock
        {
            get
            {
                bldDoc.Append(Glyphs.Authorization.Lock);
                return this;
            }
        }

        public HTMLDocument glyphUnLock
        {
            get
            {
                bldDoc.Append(Glyphs.Authorization.UnLock);
                return this;
            }
        }

        public HTMLDocument glyphTrashcan
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.Trashcan);
                return this;
            }
        }

        public HTMLDocument glyphTriangularRuler
        {
            get
            {
                bldDoc.Append(Glyphs.Metrology.TriangularRuler);
                return this;
            }
        }

        public HTMLDocument glyphWarningSignRadioactivity
        {
            get
            {
                bldDoc.Append(Glyphs.VariousSigns.warningSignRadioactivity);
                return this;
            }
        }

        public HTMLDocument glyphToolbox
        {
            get
            {
                bldDoc.Append(Glyphs.Tools.Toolbox);
                return this;
            }
        }

        public HTMLDocument SquaredPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Groups.SquaredPlus);
                return this;
            }
        }

        public HTMLDocument CircledPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Groups.SquaredPlus);
                return this;
            }
        }

        public HTMLDocument HeavyPlus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Groups.HeavyPlus);
                return this;
            }
        }

        public HTMLDocument CircledMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Groups.CircledMinus);
                return this;
            }
        }

        public HTMLDocument CircledS
        {
            get
            {
                bldDoc.Append(Glyphs.Shapes.Circled_S);
                return this;
            }
        }

        public HTMLDocument SquaredMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Groups.SquaredMinus);
                return this;
            }
        }


        public HTMLDocument HeavyMinus
        {
            get
            {
                bldDoc.Append(Glyphs.Math.heavyMiuns);
                return this;
            }
        }


        public HTMLDocument glyphSigmaSumme
        {
            get
            {
                bldDoc.Append(Glyphs.Math.SigmaSumme);
                return this;
            }
        }



        /// <summary>
        /// Menge, Quantität
        /// </summary>
        public HTMLDocument glyphQty
        {
            get
            {
                bldDoc.Append(Glyphs.Math.Quantity);
                return this;
            }
        }


        public HTMLDocument glyphCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.checkMark);
                return this;
            }
        }

        public HTMLDocument glyphHeavyCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.heavyCheckMark);
                return this;
            }
        }

        public HTMLDocument glyphLightCheckMark
        {
            get
            {
                bldDoc.Append(Glyphs.Sets.Selections.lightCheckMark);
                return this;
            }
        }

        public HTMLDocument glyphDokuHaken
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.DokuHaken);
                return this;
            }
        }

        public HTMLDocument glyphSparePart
        {
            get
            {
                bldDoc.Append(Glyphs.DFC.SparePart);
                return this;
            }
        }


    }
}
