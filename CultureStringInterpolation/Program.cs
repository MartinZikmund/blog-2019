using System;
using System.Globalization;

namespace CultureStringInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            var usCulture = new CultureInfo("en-US");
            var csCulture = new CultureInfo("cs-CZ");
            var number = 3.0m;

            Console.WriteLine(CultureInfo.CurrentCulture.Name + " culture (your system's setting):");
            Console.WriteLine($"${{number}}: {number}");

            CultureInfo.CurrentCulture = usCulture;
            Console.WriteLine("en-US culture:");
            Console.WriteLine($"${{number}}: {number}");

            CultureInfo.CurrentCulture = csCulture;
            Console.WriteLine("cs-CZ culture:");
            Console.WriteLine($"${{number}}: {number}");

            Console.WriteLine(FormattableString.Invariant($"FormattableString.Invariant(${{number}}): {number}"));

            //this doesn't work - calls string.ToString(IFormatProvider), not FormattableString.ToString(IFormatProvider)
            Console.WriteLine($"ToString(usCulture): {number}".ToString(usCulture));            
            
            //this works:
            FormattableString formattable = $"FormattableString.ToString(usCulture): {number}";
            Console.WriteLine(formattable.ToString(usCulture));
        }
    }
}
