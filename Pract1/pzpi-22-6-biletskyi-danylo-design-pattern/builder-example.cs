// Car - образ продукта який ми маємо отримати
public class Car
{
    public string Engine { get; set; }
    public string Wheels { get; set; }
    public string Body { get; set; }
    public void ShowSpecifications()
    {
        Console.WriteLine($"Engine: {Engine}");
        Console.WriteLine($"Wheels: {Wheels}");
        Console.WriteLine($"Body: {Body}");
    }
}

// ICarBuilder - будівельник, інтерфейс що визначає кроки для побудови продукту
public interface ICarBuilder
{
    void BuildEngine();
    void BuildWheels();
    void BuildBody();
    Car GetCar();
}

// JeepBuilder - конкретна реалізація кроків
// побудови продукту, в даному випадку джип
public class JeepBuilder : ICarBuilder
{
    private Car _car = new Car();
    public void BuildBody()
    {
        _car.Engine = "Diesel engine";
    }

    public void BuildEngine()
    {
        _car.Wheels = "All-Terrain wheels";
    }

    public void BuildWheels()
    {
        _car.Body = "Durable jeep body";
    }

    public Car GetCar()
    {
        return _car;
    }
}

// SportsCarBuilder - інша конкретна реалізація
// кроків побудови продукту, тепер спорткар
public class SportsCarBuilder : ICarBuilder
{
    private Car _car = new Car();
    public void BuildEngine()
    {
        _car.Engine = "V8 Engine";
    }

    public void BuildWheels()
    {
        _car.Wheels = "Sport wheels";
    }

    public void BuildBody()
    {
        _car.Body = "Aerodynamic body";
    }

    public Car GetCar()
    {
        return _car;
    }
}

// Director - керівник, котрий визначає
// в якій саме послідовності викликати кроки побудови так
// аби зібрати машину
public class Director
{
    private ICarBuilder _builder;
    public Director(ICarBuilder builder)
    {
        _builder = builder;
    }

    public void ConstructCar()
    {
        _builder.BuildEngine();
        _builder.BuildWheels();
        _builder.BuildBody();
    }
}

// Клієнт, котрий замовляє продукт, котрий йому необхідний
class Program
{
    static void Main()
    {
        // Замовлення спортивної машини
        ICarBuilder sportsCarBuilder = new SportsCarBuilder();
        Director director = new Director(sportsCarBuilder);

        director.ConstructCar();
        Car sportsCar = sportsCarBuilder.GetCar();
        sportsCar.ShowSpecifications();

        // Замовлення джипу
        ICarBuilder jeepBuilder = new JeepBuilder();
        director = new Director(jeepBuilder);

        director.ConstructCar();
        Car jeep = jeepBuilder.GetCar();
        jeep.ShowSpecifications();
    }
}
