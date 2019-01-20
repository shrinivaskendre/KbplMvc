using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KbplMvc.Models
{
    public class Materialissueslip
    {
        [DisplayName("Issue No")]
        [Required(ErrorMessage = "Please enter issue no")]
        public int IssueNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{dd-MM-yy}", ApplyFormatInEditMode = true)]
        [DisplayName("Issue Date")]
        [Required(ErrorMessage = "Please select Issue Date")]
        public DateTime? IssueDate { get; set; }

        
        //[Required(ErrorMessage = "Please enter Person Name")]
        //[DisplayName("Person Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter Person Name")]
        public string PersonName { get; set; }

        [validateFactory(ErrorMessage = "Please select Factory")]
        public int Factory { get; set; }

        [DisplayName("Cost Center")]
        [ValidCostCenter(ErrorMessage = "Please select Cost Center")]
        public int? costcenter { get; set; }

        [NotMapped]
        public List<SelectListItem> ListFactory { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListCostCenter { get; set; }

        [DisplayName("Remark")]
        [Required(ErrorMessage = "Please enter Remark")]
        public string Remark { get; set; }

        public List<ItemDetails> listitm { get; set; }

        public class validateFactory : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0)
                    return false;
                else
                    return true;
            }
        }

        public class ValidCostCenter : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0) // || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }
    }

    public class Factory
    {
        public int fId { get; set; }
        public string fName { get; set; }
    }

    public class COstCenter
    {
        public int cId { get; set; }
        public string cName { get; set; }
    }

    public class ItemDetails
    {
        public int code { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        public double availQty { get; set; }

        public double issueQty { get; set; }

        public string itemCode { get; set; }

        public string itemDesc { get; set; }

        public string unit { get; set; }

        public string issuePurpose { get; set; }
    }
}