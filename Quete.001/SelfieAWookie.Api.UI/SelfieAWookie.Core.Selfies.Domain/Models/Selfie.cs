using System.ComponentModel.DataAnnotations;

namespace SelfieAWookie.Core.Selfies.Domain.Models
{
    /// <summary>
    /// Représente un selfie avec un wookie lié
    /// </summary>
    public class Selfie
    {

        #region Properties

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string ImagePath { get; set; }


        public int WookieId { get; set; }
        public Wookie Wookie { get; set; }

        public  int PictureId { get; set; }
        public  Picture Picture { get; set; }
        #endregion
    }
}