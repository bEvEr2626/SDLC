using Avalonia.Controls;
using Avalonia.Interactivity;
using CakeCalculator.Controllers;
using CakeCalculator.Models;

namespace CakeCalculator.Views;

public partial class InputDialogWindow : Window
{
    private readonly MainController? _controller;

    public InputDialogWindow()
    {
        InitializeComponent();
    }

    public InputDialogWindow(MainController controller) : this()
    {
        _controller = controller;
        RestoreLastData();
    }

    private void RestoreLastData()
    {
        SmokingInputData? last = _controller?.GetLastInput();
        if (last != null)
        {
            CigarettesPerDayInput.Text = last.CigarettesPerDay.ToString();
            PackPriceInput.Text = last.PackPrice.ToString();
            TarInput.Text = last.TarPerCigaretteMg.ToString();
            NicotineInput.Text = last.NicotinePerCigaretteMg.ToString();
            YearsInput.Text = last.YearsSmoking.ToString();
            CakePriceInput.Text = last.CakePrice.ToString();
            OrangePriceInput.Text = last.OrangePrice.ToString();
        }
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        if (_controller == null) return;

        bool success = _controller.TryProcessInput(
            CigarettesPerDayInput.Text ?? "",
            PackPriceInput.Text ?? "",
            TarInput.Text ?? "",
            NicotineInput.Text ?? "",
            YearsInput.Text ?? "",
            CakePriceInput.Text ?? "",
            OrangePriceInput.Text ?? "",
            out string errorMessage
        );

        if (success)
        {
            Close();
        }
        else
        {
            ErrorTextBlock.Text = errorMessage;
        }
    }
}