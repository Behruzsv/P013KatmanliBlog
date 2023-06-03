using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Core.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "İçerik")]
        public string? Content { get; set; }
        [Display(Name = "Yazar")]
        public string? Author { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Düzeltme Tarihi"), ScaffoldColumn(false)]
        public DateTime? DateModified { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }
        [Display(Name = "ResimUrl")]

        public string? ImageUrl { get; set; }
        [Display(Name = "Yorum")]
        public string? Comments { get; set; }

    }
}
