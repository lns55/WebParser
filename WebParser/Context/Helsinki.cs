using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebParser.Context
{
    //Модель данных для города Хельсинки, мы парсили только температуру
    public class Helsinki
    {
        [Key]
        public int Id { get; set; }
        public string Degrees { get; set; }
    }
}
