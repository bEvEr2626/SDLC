using Avalonia.Controls;
using Avalonia.Interactivity;
using CakeCalculator.Controllers;
using CakeCalculator.Models;

namespace CakeCalculator.Views;

public partial class MainWindow : Window
{
    private readonly SmokingModel _model;
    private readonly MainController _controller;

    public MainWindow()
    {
        InitializeComponent();

        _model = new SmokingModel();
        _controller = new MainController(_model);

        _model.CalculationUpdated += OnModelCalculationUpdated;
    }

    private void OnModelCalculationUpdated(SmokingCalculationResult result)
    {
        if (result.IsNonSmoker)
        {
            StatusMessageText.IsVisible = true;
            StatusMessageText.Text = result.Message;
        }
        else
        {
            StatusMessageText.IsVisible = false;
        }

        TarResultText.Text = $"Смолы прошло через легкие: {result.TotalTarGrams:F2} г.";
        NicotineResultText.Text = $"Никотина прошло через тело: {result.TotalNicotineGrams:F2} г.";
        MoneyResultText.Text = $"Потрачено денег: {result.TotalMoneySpent:N2} руб.";
        CakesResultText.Text = $"Можно было купить тортиков: {result.CakesCount} шт.";
        OrangesResultText.Text = $"Можно было купить апельсинов: {result.OrangesCount} шт.";
    }

    private async void OnOpenInputClick(object? sender, RoutedEventArgs e)
    {
        var inputWindow = new InputDialogWindow(_controller);
        await inputWindow.ShowDialog(this);
    }
}