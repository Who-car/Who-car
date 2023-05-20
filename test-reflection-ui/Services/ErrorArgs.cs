namespace test_reflection_ui;

public class ErrorArgs
{
    public string ErrorMessage { get; }
    public ErrorArgs(ErrorType type)
    {
        ErrorMessage = type switch
        {
            ErrorType.EmptyField => "Пожалуйста заполните все поля",
            ErrorType.WrongAssembly =>
                "Кажется, ваша сборка не содержит решения задачи. Вероятнее всего, вы загрузили " +
                "не ту сборку, либо не реализовали высланный мною контракт :D",
            ErrorType.NaN => "Пожалуйста, вводите только цифры",
            ErrorType.OutOfRange => "Цифры должны находиться в диапазоне от 1 до 30 ( простите у меня слабенький компухтер( )",
            _ => "Неопознанная ошибка. Запустите программу в режиме отладки или обратитесь к разаработчику"
        };
    }
}

public enum ErrorType
{
    EmptyField,
    WrongAssembly,
    NaN,
    OutOfRange
}