using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SseServer.Model
{
    public class FeatureFlagStore
    {
        private static FeatureFlagStore _instance;
        public event EventHandler OnDataChanged;
        private FeatureFlagStore()
        {
            
        }

        public static FeatureFlagStore GetFeatureFlagStore()
        {
            if (_instance == null)
            {
                _instance = new FeatureFlagStore();
            }

            return _instance;
        }

        public void FireDataChange()
        {
            OnOnDataChanged();
        }

        protected virtual void OnOnDataChanged()
        {
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
