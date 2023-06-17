﻿using Core.Data.Annotations;
using Mobalyz.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data.Entities
{
    public abstract class CoreEntity : IEntity
    {
        [Key]
        [PopulationJsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        [PopulationJsonIgnore]
        public bool IsActive { get; set; } = true;
    }
}
