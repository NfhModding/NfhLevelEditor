using System.Collections.Generic;
using System.Drawing;

namespace Nfh.Domain.Models.InGame
{
    public class Animation : IIdentifiable
    {
        public enum Kind
        {
            SingleShot,
            Loop,
        }

        public class Frame
        {
            public string ImagePath { get; set; } = string.Empty;
            public Point ImageOffset { get; set; }
            public string? SoundPath { get; set; }
        }

        public string Id { get; }
        public Kind Kind_ { get; set; }
        public IList<Frame> Frames { get; set; } = new List<Frame>();

        public Animation(string id)
        {
            Id = id;
        }
    }
}
