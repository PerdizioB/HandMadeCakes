namespace HandMadeCakes.Dto
{
    public class CakeCreateDto
    {
        public string Flavor { get; set; } = string.Empty;
        public string Cover { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
