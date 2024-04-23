namespace Shared.Utils.Attributes;

/// <summary>
/// Mark class as excluded element. In this way, it is excluded while html rendering
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class ExcludeAttribute : Attribute
{

}