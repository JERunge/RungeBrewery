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

        public static double TotalWater(double grainBill, double batchSize, TimeSpan boilTime)
        {
            // Grain absorption
            double grainAbsorption = grainBill / 1000 * Properties.Settings.Default.GrainAbsorption;

            // Boil off
            double boilOff = boilTime.TotalHours * 3.2;

            // Equipment loss
            double equipmentLoss = Properties.Settings.Default.EquipmentLoss;

            double totalWater = Math.Round(grainAbsorption + boilOff + equipmentLoss + batchSize, 1);

            return totalWater;
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

        public static TimeSpan SessionDuration(BreweryRecipe breweryRecipe, double currentTemp)
        {
            TimeSpan duration = TimeSpan.Zero;

            // Calculate mash duration
            MashStep previousMashStep = new MashStep();
            foreach(MashStep mashStep in breweryRecipe.mashSteps)
            {
                // Calculate preheating duration for each step
                if (previousMashStep == null)
                {
                    duration += HeatDuration(currentTemp, mashStep.stepTemp, breweryRecipe.sessionInfo.BatchSize);
                }
                else
                {
                    duration += HeatDuration(previousMashStep.stepTemp, mashStep.stepTemp, breweryRecipe.sessionInfo.BatchSize);
                }

                // Calculate duration for each step and add to total duration
                duration += mashStep.stepDuration;

                previousMashStep = mashStep;
            }

            // Add sparge duration
            //duration += breweryRecipe.spargeStep.spargeDur;

            // Calculate sparge preheating duration
            duration += HeatDuration(previousMashStep.stepTemp, breweryRecipe.spargeStep.spargeTemp, breweryRecipe.sessionInfo.BatchSize);

            // Calculate boil preheating duration
            duration += HeatDuration(breweryRecipe.spargeStep.spargeTemp, 100, breweryRecipe.sessionInfo.BatchSize);

            // Add boil duration
            TimeSpan boilDuration = TimeSpan.Zero;
            foreach(Hops hops in breweryRecipe.hopsList)
            {
                if(hops.BoilTime > boilDuration)
                {
                    boilDuration = hops.BoilTime;
                }
            }
            duration += boilDuration;

            return duration;
        }
    }
}
