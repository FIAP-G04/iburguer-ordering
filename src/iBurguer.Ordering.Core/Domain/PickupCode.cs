using System.Text;
using static iBurguer.Ordering.Core.Exceptions;

namespace iBurguer.Ordering.Core.Domain;

public record PickupCode
{
    private static readonly Random random = new();
    private static readonly string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly StringBuilder codeBuilder = new(6);

    public string Code { get; private set; }
    
    private PickupCode() {}
    
    public PickupCode(string code)
    {
        Exceptions.ThePickupCodeCannotBeEmptyOrNull.ThrowIfEmpty(code);

        Code = code;
    }

    public static implicit operator string(PickupCode code) => code.Code;

    public static implicit operator PickupCode(string code) => new(code);

    public static PickupCode Generate()
    {
        codeBuilder.Clear();

        for (int i = 0; i < 6; i++)
        {
            int index = random.Next(characters.Length);
            codeBuilder.Append(characters[index]);
        }

        return new PickupCode(codeBuilder.ToString());
    }
    
}