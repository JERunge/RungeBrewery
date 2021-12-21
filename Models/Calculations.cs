using BrewUI.Items;
using System;
using Caliburn.Micro;
using System.Linq;
using System.Windows;

namespace BrewUI.Models
{
    public static class Calculations
    {
        public static TimeSpan HeatDuration(double startTemp, double goalTemp, double batchSize)
        {
            double Pt = 4.2 * batchSize * (goalTemp - startTemp) / 3600;
            TimeSpan duration = TimeSpan.FromHours(Pt / Properties.Settings.Default.HeaterEffect);
            return duration;
        }

        public static double StrikeTemp(double batchSize, double targetTemp, double grainWeight)
        {
            double R = batchSize / (grainWeight / 1000);
            double T1 = 22;
            double T2 = targetTemp;
            double strikeTemp = (0.41/R) * (T2-T1) + T2;

            strikeTemp = Math.Round(strikeTemp, 1);

            return strikeTemp;
        }

        public static double PostBoilVolume(double batchVolume, double cooldownShrinkage = -1)
        {
            if(cooldownShrinkage == -1)
            {
                cooldownShrinkage = Properties.Settings.Default.CooldownShrinkage;
            }
            double cl = batchVolume * (1 + cooldownShrinkage/100);
            return cl;
        }

        public static double BoilVolume(double postBoilVolume, double boilDuration, double evaporationRate = -1, double boilLoss = -1)
        {
            if(evaporationRate == -1)
            {
                evaporationRate = Properties.Settings.Default.EvaporationRate;
            }
            if(boilLoss == -1)
            {
                boilLoss = Properties.Settings.Default.BoilLoss;
            }
            double bv = postBoilVolume * boilDuration / 60 * evaporationRate/100 + postBoilVolume + boilLoss;
            return bv;
        }

        public static double MashVolume(double grainBill, double mashRatio = -1)
        {
            if(mashRatio == -1)
            {
                mashRatio = Properties.Settings.Default.MashRatio;
            }

            return grainBill / 1000 * mashRatio;
        }

        public static double TotalWater(double boilVolume, double grainBill, double grainAbsorption = -1)
        {
            if(grainAbsorption == -1)
            {
                grainAbsorption = Properties.Settings.Default.GrainAbsorption;
            }
            double tmw = boilVolume + grainBill/1000 * grainAbsorption;
            return tmw;
        }

        public static double MashWater(double grainBill)
        {
            double ms = Properties.Settings.Default.MashRatio * grainBill / 1000;
            return ms;
        }

        public static double SpargeWater(double grainBill)
        {
            double sw = Math.Round(grainBill * Properties.Settings.Default.MashRatio / 1000,1);
            return sw;
        }

        public static double StringToDouble(string message)
        {
            double result;

            try
            {
                if (message.Contains('.'))
                {
                    char decimalPNT = '.';
                    int pntIndex = message.IndexOf(decimalPNT);
                    result = Convert.ToDouble(message.Substring(0, pntIndex)) + Convert.ToDouble(message.Substring(pntIndex + 1, 1)) / 10;
                }
                else
                {
                    result = Convert.ToDouble(message);
                }
            }
            catch
            {
                result = 0;
            }

            return result;
        }

    }
}
