using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Entities
{
    public enum Genre
    {
        [Display(Name="Документальный")]
        NonFiction,
        Romance,
        [Display(Name="Боевик")]
        Action,
        [Display(Name="Научная фантастика")]
        ScienceFiction
    }
}
