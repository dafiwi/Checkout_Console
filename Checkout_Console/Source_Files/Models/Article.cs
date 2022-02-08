namespace CheckoutConsole.Models
{
    public class Article
    {
        //[Key]
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string? ArticleSize { get; set; }
        public string? ArticleColor { get; set; }
        public double ArticlePrice { get; set; }

        public Article(int articleId, string articleName, string articleSize, string articleColor, double articlePrice)
        {
            this.ArticleId = articleId;
            this.ArticleName = articleName;
            this.ArticleSize = articleSize;
            this.ArticleColor = articleColor;
            this.ArticlePrice = articlePrice;
        }
    }
}
