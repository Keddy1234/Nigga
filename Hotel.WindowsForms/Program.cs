using System.Globalization;

namespace Hotel.WindowsForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        var cultura = new CultureInfo("es-CR");
        cultura.NumberFormat.CurrencySymbol = "₡";
        cultura.NumberFormat.CurrencyDecimalDigits = 2;
        cultura.NumberFormat.CurrencyDecimalSeparator = ".";
        cultura.NumberFormat.CurrencyGroupSeparator = ",";
        cultura.NumberFormat.NumberDecimalSeparator = ".";
        cultura.NumberFormat.NumberGroupSeparator = ",";

        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;

        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPrincipal());
    }
}
