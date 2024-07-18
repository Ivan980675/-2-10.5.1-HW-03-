// Интерфейс для функции сложения
public interface IAddition
{
    int Add(int a, int b);
}

// Реализация интерфейса IAddition
public class Addition : IAddition
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

class Program
{
    private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        // Настройка логгера NLog
        ConfigureLogger();

        // Создание экземпляра класса Addition, реализующего интерфейс IAddition
        IAddition addition = new Addition();

        int num1, num2;

        try
        {
            // Ввод первого числа
            Console.Write("Введите первое число: ");
            num1 = int.Parse(Console.ReadLine());

            // Ввод второго числа
            Console.Write("Введите второе число: ");
            num2 = int.Parse(Console.ReadLine());

            // Вычисление суммы
            int result = addition.Add(num1, num2);
            Console.WriteLine($"Сумма чисел: {result}");
        }
        catch (FormatException ex)
        {
            // Вывод ошибки в логгер
            logger.Error(ex, "Ошибка ввода данных");
        }
        catch (Exception ex)
        {
            // Вывод общей ошибки в логгер
            logger.Error(ex, "Произошла ошибка");
        }
        finally
        {
            // Вывод сообщения о завершении работы
            logger.Info("Работа калькулятора завершена");
        }
    }

    private static void ConfigureLogger()
    {
        // Настройка логгера NLog
        var config = new NLog.Config.LoggingConfiguration();

        // Определение правил вывода сообщений
        var consoleTarget = new NLog.Targets.ColoredConsoleTarget();
        config.AddRule(LogLevel.Error, LogLevel.Fatal, consoleTarget);
        config.AddRule(LogLevel.Info, LogLevel.Info, consoleTarget);

        // Установка цветов для вывода
        consoleTarget.RowHighlightingRules.Add(new NLog.Targets.ConsoleRowHighlightingRule("level == LogLevel.Error", ConsoleOutputColor.Red, ConsoleOutputColor.White));
        consoleTarget.RowHighlightingRules.Add(new NLog.Targets.ConsoleRowHighlightingRule("level == LogLevel.Info", ConsoleOutputColor.Blue, ConsoleOutputColor.White));

        // Применение настроек
        LogManager.Configuration = config;
    }
}