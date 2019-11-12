using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Campus
    {
        /// <summary>
        /// ID of Campus
        /// </summary>
        /// <value>Id Autogenerado</value>
        public int CampusID { get; set; }

        /// <summary>
        /// Is the name o Campus
        /// </summary>
        /// <value>String</value>
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public ICollection<CampusCareer> CampusCareers { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}