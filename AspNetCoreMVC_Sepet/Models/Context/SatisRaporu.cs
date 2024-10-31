using System;
using System.Collections.Generic;

namespace AspNetCoreMVC_Sepet.Models.Context;

public partial class SatisRaporu
{
    public string Çalışan { get; set; } = null!;

    public string Müşteri { get; set; } = null!;

    public string Kategori { get; set; } = null!;

    public string? Açıklama { get; set; }

    public decimal Fiyat { get; set; }

    public short Adet { get; set; }
}
