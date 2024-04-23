using System.ComponentModel;

namespace Shared.Domain.Entity;

public class Product
{

    [Description("#PRODUCT_NAME#")]
    public string Name { get; set; }

    [Description("#PRODUCT_CODE#")]
    public string Code { get; set; }

    [Description("#PRODUCT_DESCRIPTION#")]
    public string Description { get; set; }

    [Description("#PRODUCT_QUANTITY#")]
    public int Quantity { get; set; }

    [Description("#PRODUCT_UNIT_PRICE#")]
    public int UnitPrice { get; set; }

    [Description("#PRODUCT_SLUG#")]
    public string SlugUrl { get; set; }
}