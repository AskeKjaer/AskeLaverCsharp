using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Transactions;

namespace RegAfgBeregner
{
    class Program
    {
        static int VærdiBeregner(int Value, int ChildSeat, int Airbags, int NCAPStars, int SeatBeltAlarms)
        {
            //Tilføj en midlertidlig float til regafg og en justeret værdi int
            int AdjValue = Value;
            float RegistrationTax = 0;
            //Begynd at justere værdien på baggrund af ekstra data
            AdjValue = AdjValue - (6000 * ChildSeat);
            Console.WriteLine($"Prisen justeres med {6000 * ChildSeat} kroner på grund af {ChildSeat} barnesæder");
            //Dum, dum, dum beregning på Airbags. Der er smartere måder at gøre det her på.
            if (Airbags == 0)
            {
                AdjValue = +7450;
                Console.WriteLine("Prisen justeres med +7450 kr på grund af ingen airbags");
            }
            else if (Airbags == 1)
            {
                AdjValue = +3725;
                Console.WriteLine("Prisen justeres med + 3725 på grund af kun én airbag");
            }
            else if (Airbags > 1 || Airbags < 3)
            {
                AdjValue = -(Airbags * 1280);
                Console.WriteLine($"Prisen justeres ned med {Airbags * 1280} kr. på grund af {Airbags} airbags.");
            }
            else if (Airbags < 6)
            {
                AdjValue = -(5120);
                Console.WriteLine("Prisen justeres ned med 5120 kr pga. maksimalt fradrag for airbags");
            }
            //Juster pga selealarmer
            if (SeatBeltAlarms >= 3)
            {
                AdjValue = +(1000 * 3);
                Console.WriteLine("Værdien justeres med 3000 kr på grund af selealarmer");
            }
            else
            {
                AdjValue = +(1000 * SeatBeltAlarms);
                Console.WriteLine($"Værdien justeres med {1000 * SeatBeltAlarms} kr. på grund af selealarmer");
            }

            //Juster hvis den har 5 NCAP-stjerner
            if (NCAPStars == 5)
            {
                AdjValue = -8000;
                Console.WriteLine("Prisen nedjusteres med 8000 kr pga. fem EuroNCAP stjerner.");
            }

            //Beregn hele svineriet
            if (AdjValue > 197700)
            {
                RegistrationTax = Convert.ToSingle(1977000 * 0.85);
                RegistrationTax = +Convert.ToSingle((AdjValue - 197700) * 1.5);
            }
            if (Value <= 197700)
            {
                RegistrationTax = Convert.ToSingle(AdjValue * 0.85);
            }
            return Convert.ToInt32(RegistrationTax);
        }
        static void Main(string[] args)
        {
            // Jeg definerer lige ti millioner værdier. Er det en dum måde at gøre det på? Helt sikkert.
            int vaerdi = 0;
            int barnesaede = 0;
            int airbags = 0;
            int NCAPStjerner = 0;
            bool Elbil = false;
            bool BenzinDreven = false;
            bool DieselDreven = false;
            float mpg = 0;
            int selealarmer = 0;
            int RegisteringsAfgift = 0;
            // nu kan vi rent faktisk kode noget. Starter med værdien, som får et sanity check.
            bool VærdiSanity = false;
            string VærdiString = "";
            while (VærdiSanity == false)
            {
                try
                {
                    Console.WriteLine("Hvad er bilens værdi i hele kroner?");
                    VærdiString = Console.ReadLine();
                    vaerdi = Int32.Parse(VærdiString);
                    VærdiSanity = true;
                }
                catch
                {
                    if ((float.TryParse(VærdiString, out float result)) == true)
                    {
                        Console.WriteLine("Indtast prisen uden decimaler");
                    }
                    else
                    {
                        Console.WriteLine("Indtast venligst kun et tal");
                    }

                }
            }
            //Tilføj Moms, hvis det ikke er tilfældet.
            char MomsRespons = ' ';
            while (MomsRespons != 'y' && MomsRespons != 'n' && MomsRespons != 'N' && MomsRespons != 'Y')
            {
                Console.WriteLine("Er den førnævnte pris med moms? Tryk y hvis ja, og n hvis nej.");
                MomsRespons = Console.ReadKey().KeyChar;
            }
            if (MomsRespons == 'n' || MomsRespons == 'N')
            {
                vaerdi = Convert.ToInt32(vaerdi * 1.25);
            }
            //indsamler flere data - barnesæder, airbags, selealarmer og stjerner.
            string BarneSæderInput = "";
            string AirbagsInput = "";
            string StjernerInput = "";
            string SelealarmerInput = "";
            while (Int32.TryParse(BarneSæderInput, out barnesaede) == false)
            {
                Console.WriteLine("Hvor mange indbyggede barnesæder har bilen? Hvis ingen, skriv 0.");
                BarneSæderInput = Console.ReadLine();
            }
            while (Int32.TryParse(AirbagsInput, out airbags) == false)
            {
                Console.WriteLine("Hvor mange airbags har bilen?");
                AirbagsInput = Console.ReadLine();
            }
            while ((Int32.TryParse(StjernerInput, out NCAPStjerner) == false) || (NCAPStjerner > 5 || NCAPStjerner < 0) == true)
            {
                Console.WriteLine("Hvor mange stjerner fik bilen ved sidste EuroNCAP-test?");
                StjernerInput = Console.ReadLine();
            }
            while (Int32.TryParse(SelealarmerInput, out selealarmer) == false)
            {
                Console.WriteLine("Hvor mange selealarmer har bilen?");
                SelealarmerInput = Console.ReadLine();
            }
            //Fastsæt Drivmiddel - og gem det i bools til senere.
            char FuelSelectChar = ' ';
            while (FuelSelectChar != 'b' && FuelSelectChar != 'B' && FuelSelectChar != 'd' && FuelSelectChar != 'D' && FuelSelectChar != 'e' && FuelSelectChar != 'E')
            {
                Console.WriteLine("Kører bilen på Benzin, Diesel eller El? Indtast venligst b, d eller e for at svare.");
                FuelSelectChar = Console.ReadKey().KeyChar;
            }
            if (FuelSelectChar == 'b' || FuelSelectChar == 'B')
            {
                Elbil = false;
                BenzinDreven = true;
                DieselDreven = false;
            }
            if (FuelSelectChar == 'D' || FuelSelectChar == 'd')
            {
                Elbil = false;
                BenzinDreven = false;
                DieselDreven = true;
            }
            if (FuelSelectChar == 'e' || FuelSelectChar == 'E')
            {
                Elbil = true;
                BenzinDreven = false;
                DieselDreven = false;
            }
            //Fastsæt energieffektivitet - hvis det altså er en benzin- eller dieselbil. 
            if (Elbil != true)
            {
                string MpgString = "";
                while (Single.TryParse(MpgString, out mpg) == false)
                {
                    Console.WriteLine("Hvor mange km/ltr kører bilen?");
                    MpgString = Console.ReadLine();
                    if ((Single.TryParse(MpgString, out float value) == false))
                    {
                        Console.WriteLine("Indtast venligst kun et tal");
                    }
                }
            }
            //Lav en beregning for elbiler som beskriver præcist hvor dum afgiftssystemet i Danmark er
            if (Elbil == true)
            {
                string WhPrKmString = "";
                double WhPrKm = 0;
                while ((Double.TryParse(WhPrKmString, out WhPrKm) == false)) 
                {
                    Console.WriteLine("Hvor mange Wh pr Km kører din elbil?");
                    WhPrKmString = Console.ReadLine();
                    if ((Double.TryParse(WhPrKmString, out double value) == false))
                    {
                        Console.WriteLine("Indtast venligst kun et tal");
                    }
                }
                mpg = Convert.ToSingle(100/(WhPrKm / 91.25));
            }
            vaerdi = VærdiBeregner(Value: vaerdi, ChildSeat: barnesaede, Airbags: airbags, NCAPStars: NCAPStjerner, SeatBeltAlarms: selealarmer);
            Console.WriteLine($"Bilens afgiftspligtige værdi fastsættes totalt som {vaerdi}");
        }
    }
}