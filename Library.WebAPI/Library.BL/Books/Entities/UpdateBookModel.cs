﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Books.Entities
{
    public class UpdateBookModel
    {
        public string Title { get; set; }

        public string Autor { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string SimilarWorks { get; set; }

        public DateTime PublicationYear { get; set; }
    }
}
