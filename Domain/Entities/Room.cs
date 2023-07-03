﻿namespace Domain.Entities
{
    public class Room
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Section> Section { get; set; }
    }
}
