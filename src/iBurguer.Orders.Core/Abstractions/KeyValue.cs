using System.Reflection;

namespace iBurguer.Orders.Core.Abstractions;

public abstract class KeyValue<T> : IEquatable<KeyValue<T>> where T : KeyValue<T>
{
    private readonly int _id;
    private readonly string _name;

    protected KeyValue(int id, string name)
    {
        _id = id;
        _name = name;
    }

    public override string ToString() => _name;

    public int Id => _id;
    public string Name => _name;
    
    public bool Equals(KeyValue<T>? other)
    {
        throw new NotImplementedException();
    }

    public static void Throw(T value)
    {
        
    }
    
    public static T FromName(string name)
    {
        return Find(item => item._name == name);
    }

    public static T FromId(int id)
    {
        return Find(item => item._id == id);
    }
    
    protected static T Find<E>(Func<T, bool> predicate) where E : KeyValue<T>
    {
        var orderType = GetStaticFieldsOfType<T>().SingleOrDefault(field =>
        {
            if (field.FieldType == typeof(T))
            {
                T type = (T)field.GetValue(null)!;
                return predicate(type);
            }

            return false;
        });
        
        Throw((T)orderType.GetValue(null));

        return orderType != null ? (T)orderType.GetValue(null) : default;
    }
    
    private static FieldInfo[] GetStaticFieldsOfType<T>() => typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
    
    public static IEnumerable<string> ToList()
    {
        return GetStaticFieldsOfType<T>().Where(f => f.FieldType == typeof(T)).Select(f => ((T)f.GetValue(null)!)?._name)
            .ToList();
    }
}