﻿using System;
using System.Collections.Generic;

namespace Lab10_AnropaDB.Models.DBModels;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
