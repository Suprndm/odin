﻿using System;
using System.Threading.Tasks;

namespace Odin.Navigation.Pages
{
    public interface IPage : IDisposable
    {
        PageType Type { get; set; }

        void Initialize();

        Task Show(object parameter = null);

        Task Hide();
    }
}
