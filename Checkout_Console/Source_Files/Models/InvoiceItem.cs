namespace CheckoutConsole.Models
{
    public class InvoiceItem
    {
        public List<InvoiceItem> InvoiceItems = new List<InvoiceItem>();

        //[Key]
        public int ItemPosition { get; set; }
        public Article Article { get; set; }
        public int NumberOfArticles { get; set; }

        public InvoiceItem(int itemPosition, Article article, int numberOfArticles)
        {
            this.ItemPosition = itemPosition;
            this.Article = article;
            this.NumberOfArticles = numberOfArticles;
        }

       public override string ToString()
       {
            return string.Join(" ", this.InvoiceItems
                                        .Select(invoiceItem => $"{invoiceItem.ItemPosition} " +
                                                               $"{invoiceItem.Article.ArticleName} " +
                                                               $"{invoiceItem.NumberOfArticles} " +
                                                               $"{invoiceItem.Article.ArticlePrice} " +
                                                               $"{invoiceItem.Article.ArticlePrice * invoiceItem.NumberOfArticles}"));
       }
    }    
}
