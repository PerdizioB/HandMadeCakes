namespace HandMadeCakes.Models
{
    public class ProductImage
    {

        public int Id { get; set; }  // CHAVE PRIMÁRIA

        public string ImagePath { get; set; }  // caminho da imagem

        public int ProductId { get; set; }        // chave estrangeira para o Product

        public ProductModel Product { get; set; }    // navegação
    }
}
