using System;

public static class TypeExtension
{
    /// <summary>
    /// �������������� ������ <see cref="Type.IsSubclassOf"/> �������������� ������������� ���� ��� ����������.
    /// </summary>
    /// <param name="baseType">������� ���, ������� �������� �����������.</param>
    /// <param name="toCheck">���������� ��� (�������� �� ����������� �� <paramref name="baseType"/>).</param>
    /// <param name="allowGeneric">���������� ������ ���� <paramref name="toCheck"/> ������������� ���.</param>
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
    /// �������������� ������ <see cref="Type.IsAssignableFrom"/> �������������� ������������� ���� ��� ����������.
    /// </summary>
    /// <param name="baseType">������� ���, ������� �������� �����������.</param>
    /// <param name="toCheck">���������� ��� (�������� �� ����������� �� <paramref name="baseType"/>).</param>
    public static bool IsAssignableToRawInterface(this Type toCheck, Type baseType)
    {
        Type[] interfaceTypes = toCheck.GetInterfaces();

        foreach (Type it in interfaceTypes)
            if (it.IsGenericType && it.GetGenericTypeDefinition() == baseType)
                return true;

        return false;
    }
}
