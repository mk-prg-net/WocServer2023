using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class Color : CssMeasure
    {
        public byte[] RGB = new byte[3];

        // RGB- Komponenten der Farbe
        public byte R
        {
            get
            {
                return RGB[0];
            }
            set
            {
                RGB[0] = value;
            }
        }

        public byte G
        {
            get
            {
                return RGB[1];
            }
            set
            {
                RGB[1] = value;
            }
        }

        public byte B
        {
            get
            {
                return RGB[2];
            }
            set
            {
                RGB[2] = value;
            }
        }

        public override bool Equals(object obj)
        {
            var sec = (Color)obj;

            return R == sec.R && B == sec.B && G == sec.G; 
        }
        public static bool operator ==(Color fst, Color sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Color fst, Color sec)
        {
            return !fst.Equals(sec);
        }


        // Farbe als RGBString
        public string ToHtmlColorString()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
        }

        public override string TextValue
        {
            get { return ToHtmlColorString(); }
        }

        public void FromRGBString(string htmlcolorString)
        {
            // Prüfen der Zeichenkette
            htmlcolorString = htmlcolorString.Trim();
            if (htmlcolorString[0] != '#' || htmlcolorString.Length != 7)
            {
                htmlcolorString = htmlcolorString.ToLower();

                if (htmlcolorString == "transparent")
                {
                    R = 255; G = 255; B = 255;
                }
                else if (htmlcolorString == "maroon")
                {
                    R = 0x80; G = 0x00; B = 0x00;
                }
                else if (htmlcolorString == "red")
                {
                    R = 0xFF; G = 0x00; B = 0x00;
                }
                else if (htmlcolorString == "darkred")
                {
                    R = DarkRed.R; G = DarkRed.G; B = DarkRed.B;
                }
                else if (htmlcolorString == "coral")
                {
                    R = Coral.R; G = Coral.G; B = Coral.B;
                }
                else if (htmlcolorString == "orange")
                {
                    R = 0xFF; G = 0xA5; B = 0x00;
                }
                else if (htmlcolorString == "coral")
                {
                    R = Coral.R; G = Coral.B; B = Coral.G;
                }
                else if (htmlcolorString == "gold")
                {
                    R = Gold.R; G = Gold.G; B = Gold.B;
                }
                else if (htmlcolorString == "goldenrod")
                {
                    R = GoldenRod.R; G = GoldenRod.G; B = GoldenRod.B;
                }
                else if (htmlcolorString == "yellow")
                {
                    R = 0xFF; G = 0xFF; B = 0x00;
                }
                else if (htmlcolorString == "olive")
                {
                    R = 0x80; G = 0x80; B = 0x00;
                }
                else if (htmlcolorString == "purple")
                {
                    R = 0x80; G = 0x00; B = 0x80;
                }
                else if (htmlcolorString == "fuchsia")
                {
                    R = 0xFF; G = 0x00; B = 0xFF;
                }
                else if (htmlcolorString == "white")
                {
                    R = 0xFF; G = 0xFF; B = 0xFF;
                }
                else if (htmlcolorString == "lime")
                {
                    R = 0x00; G = 0xFF; B = 0x00;
                }
                else if (htmlcolorString == "green")
                {
                    R = Green.R; G = Green.G; B = Green.B;
                }
                else if (htmlcolorString == "darkgreen")
                {
                    R = DarkGreen.R; G = DarkGreen.G; B = DarkGreen.B;
                }
                else if (htmlcolorString == "navy")
                {
                    R = 0x00; G = 0x00; B = 0x80;
                }
                else if (htmlcolorString == "blue")
                {
                    R = 0x00; G = 0x00; B = 0xFF;
                }
                else if (htmlcolorString == "darkblue")
                {
                    R = DarkBlue.R; G = DarkBlue.G; B = DarkBlue.B;
                }
                else if (htmlcolorString == "darkslateblue")
                {
                    R = DarkSlateBlue.R; G = DarkSlateBlue.G; B = DarkSlateBlue.B;
                }
                else if (htmlcolorString == "aqua")
                {
                    R = 0x00; G = 0xFF; B = 0xFF;
                }
                else if (htmlcolorString == "teal")
                {
                    R = 0x00; G = 0x80; B = 0x80;
                }
                else if (htmlcolorString == "black")
                {
                    R = 0x00; G = 0x00; B = 0x00;
                }
                else if (htmlcolorString == "silver")
                {
                    R = 0xC0; G = 0xC0; B = 0xC0;
                }
                else if (htmlcolorString == "gray")
                {
                    R = 0x80; G = 0x80; B = 0x80;
                }
                else
                    throw new FormatException("Ungültiger Farbwert " + htmlcolorString);

                return;
            }


            for (int i = 0; i < 3; i++)
            {
                var cp = htmlcolorString.Substring((2 * i) + 1, 2);
                byte val;
                if (!byte.TryParse(cp, System.Globalization.NumberStyles.HexNumber, System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat, out val))
                    throw new FormatException("Ungültiger Farbwert " + htmlcolorString);

                RGB[i] = val;
            }

        }

        public static Color Parse(string htmlcolorString)
        {
            if (string.IsNullOrEmpty(htmlcolorString))
                return null;
            return new Color(htmlcolorString);
        }


        public Color()
        {
        }


        public Color(string htmlColorString)
        {
            FromRGBString(htmlColorString);
        }

        public Color(byte R, byte G, byte B)
        {
            RGB[0] = R;
            RGB[1] = G;
            RGB[2] = B;
        }

        //------------------------------------------------------------------------------------------
        // Vordefinierte Farbkonstanten

        public static Color Maroon
        {
            get
            {
                return new Color(0x80, 0x00, 0x00);
            }
        }

        public static Color Red
        {
            get
            {
                return new Color(0xFF, 0x00, 0x00);
            }
        }

        public static Color DarkRed
        {
            get
            {
                return new Color(0x8B, 0x00, 0x00);
            }
        }

        public static Color Coral
        {
            get
            {
                return new Color(0xFF, 0x7F, 0x50);
            }
        }


        public static Color Orange
        {
            get
            {
                return new Color(0xFF, 0xA5, 0x00);
            }
        }

        public static Color Gold
        {
            get
            {
                return new Color(0xFF, 0xD7, 0x00);
            }
        }

        public static Color GoldenRod
        {
            get
            {
                return new Color(0xDA, 0xA5, 0x20);
            }
        }

        public static Color Yellow
        {
            get
            {
                return new Color(0xFF, 0xFF, 0x00);
            }
        }

        public static Color Olive
        {
            get
            {
                return new Color(0x80, 0x80, 0x00);
            }
        }

        public static Color Purple
        {
            get
            {
                return new Color(0x80, 0x00, 0x80);
            }
        }

        public static Color Fuchsia
        {
            get
            {
                return new Color(0xFF, 0x00, 0xFF);
            }
        }

        public static Color White
        {
            get
            {
                return new Color(0xFF, 0xFF, 0xFF);
            }
        }

        public static Color Lime
        {
            get
            {
                return new Color(0x00, 0xFF, 0x00);
            }
        }

        public static Color Green
        {
            get
            {
                return new Color(0x00, 0xFF, 0x00);
            }
        }

        public static Color DarkGreen
        {
            get
            {
                return new Color(0x00, 0x60, 0x00);
            }
        }


        public static Color Navy
        {
            get
            {
                return new Color(0x00, 0x00, 0x80);
            }
        }

        public static Color Blue
        {
            get
            {
                return new Color(0x00, 0x00, 0xff);
            }
        }

        public static Color DarkBlue
        {
            get
            {
                return new Color(0x00, 0x00, 0x8B);
            }
        }

        public static Color DarkSlateBlue
        {
            get
            {
                return new Color(0x48, 0x3D, 0x8B);
            }
        }



        public static Color Aqua
        {
            get
            {
                return new Color(0x00, 0xFF, 0xFF);
            }
        }

        public static Color Teal
        {
            get
            {
                return new Color(0x00, 0x80, 0x80);
            }
        }

        public static Color Black
        {
            get
            {
                return new Color(0x00, 0x00, 0x00);
            }
        }

        public static Color Silver
        {
            get
            {
                return new Color(0xC0, 0xC0, 0xC0);
            }
        }

        public static Color Gray
        {
            get
            {
                return new Color(0x80, 0x80, 0x80);
            }
        }

        protected override object _Clone()
        {
            return new Color(R, G, B);
        }
    }

}
