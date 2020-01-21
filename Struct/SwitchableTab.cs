using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPage.Struct
{
    public struct SwitchableTab
    {
        private int _currentTab;
        private int _maximumTab;
        private readonly Tab[] _internalTab;
        public SwitchableTab(IEnumerable<Tab> data)
        {
            if (!data.Any()) throw new ArgumentNullException(nameof(data));
            if (data is Tab[] arr)
            {
                _internalTab = arr;
            }
            else
            {
                _internalTab = data.ToArray();
            }
            _maximumTab = data.Count();
            _currentTab = 0;
        }
        public Tab Current()
        {
            return _internalTab[_currentTab];
        }
        public void Next()
        {
            if (_currentTab < _maximumTab - 1)
                _currentTab++;
        }
        public void Previous()
        {
            if (_currentTab > 0)
                _currentTab--;
        }
        public void GoToFirst()
        {
            _currentTab = 0;
        }
        public void GoToLast()
        {
            _currentTab = _maximumTab - 1;
        }
    }
}
