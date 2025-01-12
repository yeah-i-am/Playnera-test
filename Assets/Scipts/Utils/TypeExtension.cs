using System;

public static class TypeExtension
{
    /// <summary>
    /// Альтернативная версия <see cref="Type.IsSubclassOf"/> поддерживающая универсальные типы без параметров.
    /// </summary>
    /// <param name="baseType">Базовый тип, наличие которого проверяется.</param>
    /// <param name="toCheck">Проверяемы тип (Является ли производным от <paramref name="baseType"/>).</param>
    /// <param name="allowGeneric">Возвращать истину если <paramref name="toCheck"/> универсальный тип.</param>
    public static bool IsSubclassOfRawGeneric(this Type toCheck, Type baseType, bool allowGeneric = false)
    {
        if (toCheck.IsInterface || (toCheck.IsGenericType && !allowGeneric))
            return false;

        while (toCheck != typeof(object))
        {
            Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

            if (baseType == cur)
                return true;

            toCheck = toCheck.BaseType;
        }

        return false;
    }

    /// <summary>
    /// Альтернативная версия <see cref="Type.IsAssignableFrom"/> поддерживающая универсальные типы без параметров.
    /// </summary>
    /// <param name="baseType">Базовый тип, наличие которого проверяется.</param>
    /// <param name="toCheck">Проверяемы тип (Является ли производным от <paramref name="baseType"/>).</param>
    public static bool IsAssignableToRawInterface(this Type toCheck, Type baseType)
    {
        Type[] interfaceTypes = toCheck.GetInterfaces();

        foreach (Type it in interfaceTypes)
            if (it.IsGenericType && it.GetGenericTypeDefinition() == baseType)
                return true;

        return false;
    }
}
