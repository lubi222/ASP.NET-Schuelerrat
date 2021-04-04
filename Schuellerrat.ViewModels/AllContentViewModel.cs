namespace Schuellerrat.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AllContentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public string CreatedOn { get; set; }
    }
}
