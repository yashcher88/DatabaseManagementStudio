using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DatabaseManagementStudio.Classes
{
    public class SettingValue<T>
    {
        private readonly T _defaultValue;
        private T _currentValue;
        private readonly Logger _logger;
        public List<T> Values { get; } = new();
        public bool IsList { get; private set; }
        public SettingValue(T defaultValue, JsonNode? allowedValues, Logger logger)
        {
            _defaultValue = defaultValue;
            _currentValue = defaultValue;
            _logger = logger;

            if (allowedValues is JsonArray jsonArray)
            {
                IsList = true;
                foreach (var item in jsonArray)
                {
                    if (item is not null)
                    {
                        try
                        {
                            T value = item.Deserialize<T>()!;
                            Values.Add(value);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error($"Ошибка при десериализации значения '{item}': {ex.Message}");
                        }
                    }
                }
            }
        }
        public bool SetValue(T value)
        {
            if (IsList && !Values.Contains(value))
                return false;

            _currentValue = value;
            return true;
        }
        public T GetValue() => _currentValue;
        public bool IsDefault() => EqualityComparer<T>.Default.Equals(_currentValue, _defaultValue);
    }
}