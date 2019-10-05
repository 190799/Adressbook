
namespace AppAdress.Models
{
    public enum TypeContact
    {
        telefono,
        correo,
        facebook,
        twitter,
        instagram,

    }
    public class Book
    {
       
        public int BookID { get; set; }
        public string Name { get; set; }
        public TypeContact MyProperty { get; set; }
        public string Contact { get; set; }
    }
}
