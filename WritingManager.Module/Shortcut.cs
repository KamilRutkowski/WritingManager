using System;

namespace WritingManager.Module
{
    [Flags]
    public enum KeyboardFlags
    {
        Ctrl = 1,
        Alt = 2,
        Shift = 4,
        PlatformSpecyfic = 8
    }

    public class Shortcut<T>
    {
        public (KeyboardFlags, char, IControllerBase<T>) TriggerOn { get; set; }
        public Action TriggeredAction { get; set; }
    }
}