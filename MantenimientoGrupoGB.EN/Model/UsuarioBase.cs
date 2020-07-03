﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MantenimientoGrupoGB.EN.Model
{
    public partial class UsuarioBase
    {
        [Key]
        public int IdUsuario { get; set; }
      
        [StringLength(50)]
        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombres", Prompt = "ROBERTO ESAU")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El Apellido es requerido")]
        [Display(Name = "Apellidos", Prompt = "PERDOMO ARAGON")]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "Fecha de nacimiento es requerido")]
        [Display(Name = "Fecha nacimiento", Prompt = "15/12/1994")]
        public DateTime FechaNacimiento { get; set; }
      
        [Column("DUI")]
        [StringLength(10)]
        [Required(ErrorMessage = "Número de DUI es requerido")]
        [Display(Name = "Número de DUI", Prompt = "00000000-0")]
        public string Dui { get; set; }
       
        [Column("NIT")]
        [Required(ErrorMessage = "Número de NIT es requerido")]
        [Display(Name = "Número de NIT", Prompt = "0000-000000-000-0")]
        [StringLength(17)]
        public string Nit { get; set; }
        
        [Column("ISSS")]
        [Required(ErrorMessage = "Número de ISSS es requerido")]
        [Display(Name = "Número de ISSS", Prompt = "000000000")]
        [StringLength(9)]

        public string Isss { get; set; }

        [Required(ErrorMessage = "Número de telefono es requerido")]
        [Display(Name = "Número de telefono", Prompt = "+503-7586-5911")]
        [StringLength(14)]
        public string Telefono { get; set; }

        [JsonIgnore]
        [Column(TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [JsonIgnore]
        [Column(TypeName = "datetime")]
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        public bool EstadoEliminado { get; set; }
        [JsonIgnore]
        [Column(TypeName = "datetime")]
        public DateTime? FechaEliminacion { get; set; }
    }
}