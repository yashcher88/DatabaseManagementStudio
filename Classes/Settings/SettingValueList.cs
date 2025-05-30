using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseManagementStudio.Classes
{
    public class SettingValueList
    {
        private readonly Dictionary<string, SettingValueList> _children = new();
        private object? _value; // SettingValue<T>, но тип неизвестен на уровне дерева

        public void Add<T>(string path, SettingValue<T> value)
        {
            var parts = path.Split('.');
            SettingValueList current = this;

            foreach (var part in parts[..^1]) // Все кроме последнего
            {
                if (!current._children.TryGetValue(part, out var next))
                {
                    next = new SettingValueList();
                    current._children[part] = next;
                }
                current = next;
            }

            current._children[parts[^1]] = new SettingValueList { _value = value };
        }

        public SettingValue<T>? Get<T>(string path)
        {
            var parts = path.Split('.');
            SettingValueList? current = this;

            foreach (var part in parts)
            {
                if (!current._children.TryGetValue(part, out var next))
                    return null;

                current = next;
            }

            return current._value as SettingValue<T>;
        }

        public bool TrySetValue<T>(string path, T value)
        {
            var setting = Get<T>(path);
            return setting?.SetValue(value) ?? false;
        }

        public T? GetValue<T>(string path)
        {
            var setting = Get<T>(path);
            return setting != null ? setting.GetValue() : default;
        }
    }
}