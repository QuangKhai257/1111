using System.ComponentModel.DataAnnotations;
namespace chinliuliu.Models;
public class Lophoc
{
    [Key]
    [Display (Name ="Mã Lớp")]
    public string Malop {get; set;}

    [Display (Name ="Tên Lớp")]
    public string Tenlop {get; set;}
}