namespace MediStoS_MobileClient.Helpers;

using MediStoS_MobileClient.DataTransferModels;
using System.Globalization;

public class ValueToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SensorDto sensor && parameter is WarehouseDto warehouse)
        {
            if (sensor.Type == "Temperature")
            {
                return sensor.Value < warehouse.MinTemperature || sensor.Value > warehouse.MaxTemperature
                    ? Colors.Red
                    : Colors.Green;
            }
            else if (sensor.Type == "Humidity")
            {
                return sensor.Value < warehouse.MinHumidity || sensor.Value > warehouse.MaxHumidity
                    ? Colors.Red
                    : Colors.Green;
            }
        }
        return Colors.Gray; // Default color for unknown types
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
