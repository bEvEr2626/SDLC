using CakeCalculator.Models;

namespace CakeCalculator.Controllers;

public class MainController
{
    private readonly SmokingModel _model;

    public MainController(SmokingModel model)
    {
        _model = model;
    }

    public SmokingInputData? GetLastInput() => _model.LastInput;

    public bool TryProcessInput(
        string cigPerDayStr,
        string packPriceStr,
        string tarStr,
        string nicotineStr,
        string yearsStr,
        string cakePriceStr,
        string orangePriceStr,
        out string errorMessage)
    {
        errorMessage = string.Empty;

        if (!double.TryParse(cigPerDayStr, out double cigPerDay) || cigPerDay < 0)
        {
            errorMessage = "Ошибка: укажите корректное количество сигарет (0 или больше).";
            return false;
        }

        bool isNonSmoker = cigPerDay == 0;

        decimal packPrice = 0;
        double tar = 0;
        double nicotine = 0;
        double years = 0;
        decimal cakePrice = 0;
        decimal orangePrice = 0;

        if (!isNonSmoker)
        {
            if (!decimal.TryParse(packPriceStr, out packPrice) || packPrice <= 0)
            {
                errorMessage = "Ошибка: укажите корректную цену пачки сигарет.";
                return false;
            }
            if (!double.TryParse(tarStr, out tar) || tar < 0)
            {
                errorMessage = "Ошибка: некорректное содержание смолы.";
                return false;
            }
            if (!double.TryParse(nicotineStr, out nicotine) || nicotine < 0)
            {
                errorMessage = "Ошибка: некорректное содержание никотина.";
                return false;
            }
            if (!double.TryParse(yearsStr, out years) || years <= 0)
            {
                errorMessage = "Ошибка: стаж курения должен быть больше 0.";
                return false;
            }
            if (!decimal.TryParse(cakePriceStr, out cakePrice) || cakePrice <= 0)
            {
                errorMessage = "Ошибка: укажите корректную цену тортика.";
                return false;
            }
            if (!decimal.TryParse(orangePriceStr, out orangePrice) || orangePrice <= 0)
            {
                errorMessage = "Ошибка: укажите корректную цену апельсина.";
                return false;
            }
        }
        else
        {
            decimal.TryParse(packPriceStr, out packPrice);
            double.TryParse(tarStr, out tar);
            double.TryParse(nicotineStr, out nicotine);
            double.TryParse(yearsStr, out years);
            decimal.TryParse(cakePriceStr, out cakePrice);
            decimal.TryParse(orangePriceStr, out orangePrice);
        }

        var inputData = new SmokingInputData
        {
            CigarettesPerDay = cigPerDay,
            PackPrice = packPrice,
            TarPerCigaretteMg = tar,
            NicotinePerCigaretteMg = nicotine,
            YearsSmoking = years,
            CakePrice = cakePrice,
            OrangePrice = orangePrice
        };

        _model.UpdateData(inputData);
        return true;
    }
}