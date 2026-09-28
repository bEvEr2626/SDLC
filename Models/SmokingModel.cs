using System;

namespace CakeCalculator.Models;

public class SmokingInputData
{
    public double CigarettesPerDay { get; set; }
    public decimal PackPrice { get; set; }
    public double TarPerCigaretteMg { get; set; }
    public double NicotinePerCigaretteMg { get; set; }
    public double YearsSmoking { get; set; }
    public decimal CakePrice { get; set; }
    public decimal OrangePrice { get; set; }
}

public class SmokingCalculationResult
{
    public double TotalTarGrams { get; set; }
    public double TotalNicotineGrams { get; set; }
    public decimal TotalMoneySpent { get; set; }
    public int CakesCount { get; set; }
    public int OrangesCount { get; set; }
    public bool IsNonSmoker { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class SmokingModel
{
    public SmokingInputData? LastInput { get; private set; }
    public SmokingCalculationResult? CurrentResult { get; private set; }

    public event Action<SmokingCalculationResult>? CalculationUpdated;

    public void UpdateData(SmokingInputData input)
    {
        LastInput = input;

        bool isNonSmoker = input.CigarettesPerDay == 0;

        double totalDays = input.YearsSmoking * 365.25;
        double totalCigarettes = totalDays * input.CigarettesPerDay;
        double totalPacks = totalCigarettes / 20.0;

        decimal totalMoney = (decimal)totalPacks * input.PackPrice;
        double totalTarGrams = (totalCigarettes * input.TarPerCigaretteMg) / 1000.0;
        double totalNicotineGrams = (totalCigarettes * input.NicotinePerCigaretteMg) / 1000.0;

        int cakes = input.CakePrice > 0 ? (int)(totalMoney / input.CakePrice) : 0;
        int oranges = input.OrangePrice > 0 ? (int)(totalMoney / input.OrangePrice) : 0;

        CurrentResult = new SmokingCalculationResult
        {
            TotalTarGrams = totalTarGrams,
            TotalNicotineGrams = totalNicotineGrams,
            TotalMoneySpent = totalMoney,
            CakesCount = cakes,
            OrangesCount = oranges,
            IsNonSmoker = isNonSmoker,
            Message = isNonSmoker ? "Так держать! Вы не курите — ваши лёгкие и кошелёк в полной безопасности! 🎉" : string.Empty
        };

        CalculationUpdated?.Invoke(CurrentResult);
    }
}