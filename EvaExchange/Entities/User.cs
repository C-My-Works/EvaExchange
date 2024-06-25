namespace EvaExchange.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
       

        //Relations
        public Portfolio Portfolio { get; set; }
        


    }
}
