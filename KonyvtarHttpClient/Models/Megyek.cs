using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KonyvtarHttpClient.Models;

public partial class Megyek
{
    public int Id { get; set; }

    public string MegyeNev { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Telepulesek> Telepuleseks { get; set; } = new List<Telepulesek>();
}
