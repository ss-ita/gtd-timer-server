//-----------------------------------------------------------------------
// <copyright file="TimerDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Timer model
    /// </summary>
    public class TimerDto
    {
        /// <summary>
        /// Gets or sets a value of Id property
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value of Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of Interval property
        /// </summary>
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets a value of preset id foreign key property
        /// </summary>
        public int PresetId { get; set; }
    }
}
