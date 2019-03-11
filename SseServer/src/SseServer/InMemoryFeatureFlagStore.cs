using System;

namespace SseServer
{
    public class InMemoryFeatureFlagStore : IFeatureFlagStore
    {
        public event EventHandler OnDataChanged;
        
        public void FireDataChange()
        {
            OnDataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}