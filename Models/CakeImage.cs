namespace HandMadeCakes.Models
{
    public class CakeImage
    {
        public int Id { get; set; }  // CHAVE PRIMÁRIA

        public string ImagePath { get; set; }  // caminho da imagem

        public int CakeId { get; set; }        // chave estrangeira para o Cake

        public CakeModel Cake { get; set; }    // navegação
    }

}
