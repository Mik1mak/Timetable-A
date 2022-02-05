using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace TimetableA.BlazorImporter
{
    public class ImporterForm
    {
        string parserId = nameof(IcsParserInfo);

        [Required]
        public string ParserId 
        { 
            get => parserId; 
            set 
            {
                parserId = value;
                Source = string.Empty;
                SourceFile = null;
            } 
        }

        public string SelectedSource { get; set; } = "url";

        [Range(1,10)]
        public int Cycles { get; set; } = 1;
        
        public string Source { get; set; } = string.Empty;

        public IBrowserFile? SourceFile { get; set; }

        [Required]
        public bool AsNew { get; set; } = true;

        //[Url]
        public string EditUrl { get; set; } = string.Empty;
        //public AuthenticateRequest LoginInfo { get; }
    }
}
