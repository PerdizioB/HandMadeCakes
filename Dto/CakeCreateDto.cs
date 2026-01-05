public class CakeCreateDto
{
    public string Flavor { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0m;  // decimal
    public int ProductId { get; set; }


}
