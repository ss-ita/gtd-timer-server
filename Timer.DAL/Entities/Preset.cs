//-----------------------------------------------------------------------
// <copyright file="Preset.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of preset table
    /// </summary>
    public class Preset
    {
        /// <summary>
        /// Gets or sets Id column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name column
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets UserId column
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to user table
        /// </summary>
        public User User { get; set; }      
    }
}
