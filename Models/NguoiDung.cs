namespace QuanLyCongViec.Models
{
    public class NguoiDung
    {
        public NguoiDung() { }
        public NguoiDung(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}