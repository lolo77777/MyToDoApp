﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyToDo.Share.Parameters;

public class QueryParameter
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
}