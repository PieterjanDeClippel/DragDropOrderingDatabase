using System;
using System.Collections.Generic;
using System.Text;

namespace ReorderDatabase.Dtos
{
    public class NoteSwapResult
    {
        public Note Note { get; set; }
        public bool NeedsReindexing { get; set; }
    }
}
