﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareHouse.Contract.DataContracts.QueryClass
{

    public class EmployeesResourceParameter
    {
        private const int maxEmployeesSize = 20;

        public int pageNumber = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = value > maxEmployeesSize ? maxEmployeesSize : value;
        }
    }
}
