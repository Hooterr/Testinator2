﻿using Dna;
using Testinator.Core;

namespace Testinator.Client
{
    public class ViewModelProvider : IViewModelProvider
    {
        public T GetInjectedPageViewModel<T>() where T : BaseViewModel
        {
            return Framework.Service<T>();
        }
    }
}
