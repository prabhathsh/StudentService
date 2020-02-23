using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StudentService.Core.Models
{
    public abstract class Entity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Data time to live
        /// </summary>
        [JsonProperty(PropertyName = "ttl")]
        public int Ttl { get; set; }


        public Entity(bool generateId)
        {
           SetDefaultTimeToLive();

            if (generateId)
                this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Set a default data time to live
        /// </summary>
        public virtual void SetDefaultTimeToLive()
        {
            Ttl = -1;
        }
    }
}
