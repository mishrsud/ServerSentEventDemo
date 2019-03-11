using System;

namespace SseServer
{
    public interface IFeatureFlagStore
    {
        event EventHandler OnDataChanged;
        void FireDataChange();
    }
}